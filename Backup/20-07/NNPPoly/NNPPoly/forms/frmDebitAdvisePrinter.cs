using NNPPoly.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace NNPPoly.forms
{
    public partial class frmDebitAdvisePrinter : Form
    {
        private static Decimal noOfCopies = 1;
        private bool isDirect = false;
        private List<long> paymentIds = null;
        private List<long> dnoteIds = null;

        public frmDebitAdvisePrinter(List<long> paymentIds, List<long> dnoteIds)
        {
            InitializeComponent();
            this.paymentIds = paymentIds;
            this.dnoteIds = dnoteIds;
        }

        public frmDebitAdvisePrinter(List<long> dnoteIds, bool isDirect)
        {
            InitializeComponent();
            this.paymentIds = dnoteIds;
            this.isDirect = isDirect;
        }

        private void frmDebitAdvisePrinter_Load(object sender, EventArgs e)
        {

        }

        private void frmDebitAdvisePrinter_Shown(object sender, EventArgs e)
        {
            frmAskUser ask = new frmAskUser("How many copies of Debit Advise print ?", "0 means skip", "1", frmAskUser.ValueType.Long);
            ask.ShowDialog(this);
            Decimal.TryParse(ask.getText(), out noOfCopies);
            Thread thread = new Thread(() =>
            {
                Action act = () => {  };

                List<long> ids = new List<long>();

                if (!isDirect)
                {
                    List<IndividualGroup> groups = getIndividualGroups();

                    foreach (IndividualGroup group in groups)
                    {
                        Client client = Job.Clients.get(group.client_id);
                        //attaching debit note entry
                        foreach (long dnid in dnoteIds)
                        {
                            Payment p = Job.DebitNotes.getPayment(dnid);
                            if (p != null && p.client_id == group.client_id)
                            {
                                if (p.date == group.date)
                                {
                                    group.entries.Add(p.id);
                                }
                            }
                        }

                        long newId = 0;
                        Job.DebitNotes.add(ref newId, group.client_id, false, 0, group.date, group.entries);


                        //printDebitAdvise(newId);
                        ids.Add(newId);
                    }
                }

                act = () =>
                {
                    pb.Style = ProgressBarStyle.Blocks;
                    pb.Maximum = isDirect ? paymentIds.Count : ids.Count; 
                    pb.Value = 0;
                    lblStatus.Text = "Printing...";
                    lblProcess.Text = "[0/" + pb.Maximum + "]";
                };
                Invoke(act);

                if (!isDirect)
                    printDebitAdvise(ids);
                else
                    printDebitAdvise(paymentIds);

                act = () => { Close(); };
                Invoke(act);

            });
            thread.Name = "Thread:DebitNotePrinter";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            pb.Style = ProgressBarStyle.Marquee;
            lblProcess.Text = "";
            lblStatus.Text = "Initiliazing...";
        }

        private void printDebitAdvise(List<long> ids)
        {
            String tempFile = Application.StartupPath + "\\a5_debitadvise_template.docx";
            try
            {
                if (!File.Exists(tempFile))
                {
                    try
                    {
                        try
                        {
                            File.Delete(tempFile);
                        }
                        catch (Exception) { }
                        File.WriteAllBytes(tempFile, Properties.Resources.DebitAdvisePrintFormat);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in writing document file to saving location." + Environment.NewLine + "Error message:" + Environment.NewLine + ex.Message, "Document write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize template_a5_debitadvise operation.";
                Job.Log(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            try
            {
                if (System.IO.File.Exists(tempFile))
                {
                    int counter = 0;
                    String printerName = Job.GeneralSettings.printer();
                    printerName = printerName == null ? "" : printerName;
                    Word.Application appWord = new Word.Application();
                    bool backPrintingBG = appWord.Application.Options.PrintBackground;
                    appWord.Application.Options.PrintBackground = false;

                    List<MessageHolder> holders = new List<MessageHolder>();

                    foreach (long id in ids)
                    {
                        DebitNote dn = Job.DebitNotes.get(id);
                        if (dn == null)
                        {
                            continue;
                        }

                        //UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.Trim().ToLower().Replace(" ", "").Equals(dnote.toClientName.Trim().ToLower().Replace(" ", ""))));
                        Client client = Job.Clients.get(dn.client_id);

                        MessageHolder holder = Job.Messages.prepareDespatch(dn);
                        if (holder != null)
                            holders.Add(holder);

                        if (!isDirect)
                        {
                            //Datastore.dataFile.DebitAdvises.Add(dnote);
                        }

                        if (noOfCopies > 0)
                        {
                            Action action = () =>
                            {
                                lblStatus.Text = "Printing ...";
                            };
                            Invoke(action);
                            try
                            {
                                #region init-word-doc
                                Word.Document doc = (Word.Document)appWord.Documents.Open(tempFile);
                                
                                Job.Functions.FindAndReplace(appWord, "%title%", Job.Companies.currentCompany.name, true, doc);
                                Job.Functions.FindAndReplace(appWord, "%address%", Job.Companies.currentCompany.address, true, doc);
                                Job.Functions.FindAndReplace(appWord, "%to%", client.name, true, doc);
                                //Job.Functions.FindAndReplace(appWord, "%debitno%", id, true, doc);
                                //Job.Functions.FindAndReplace(appWord, "%date%", dn.date.ToShortDateString(), true, doc);
                                //Job.Functions.FindAndReplace(appWord, "%description_tablerow%", Job.GeneralSettings.mid_row_text());

                                double totalAmount = 0, totalMt=0;


                                dn.entries.Sort(new Comparison<DebitNote.PaymentEntry>((pe1, pe2) =>
                                {
                                    classes.Payment p1 = Job.Payments.get(pe1.paymentId);
                                    classes.Payment p2 = Job.Payments.get(pe2.paymentId);
                                    if (p1 == null || p2 == null) return 0;
                                    return (int)(p2.mt - p1.mt);
                                }));

                                int no = 0;
                                totalAmount = Job.DebitNotes.countDebitNoteRows(dn, (double rowAmount, classes.Payment payment) =>
                                {
                                    no++;
                                    Job.Functions.FindAndReplace(appWord, "%in" + no + "%", payment.invoice);
                                    Job.Functions.FindAndReplace(appWord, "%date" + no + "%", payment.date.ToShortDateString());
                                    Job.Functions.FindAndReplace(appWord, "%grade" + no + "%", payment.grade.code);
                                    if (payment.mt > 0)
                                    {
                                        Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", Job.Functions.MTToString(payment.mt));
                                    }
                                    else
                                    {
                                        Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", "");
                                    }
                                    totalMt += payment.mt;

                                    //double rowAmount = payment.debit_amount;

                                    Job.Functions.FindAndReplace(appWord, "%amt" + no + "%", Job.Functions.AmountToString(rowAmount));
                                });

                                /*for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITNOTE && i < dn.entries.Count; i++)
                                {
                                    int no = i + 1;
                                    //DebitNoteRow row = dnote.debitNoteRows[i];
                                    Payment payment = Job.Payments.get(dn.entries[i].paymentId);
                                    if (payment == null)
                                    {
                                        Job.Log("Payment ignorance in debitnoteprinter.", new Exception("Something gonna wrong!!"));
                                        continue;
                                    }
                                    Job.Functions.FindAndReplace(appWord, "%in" + no + "%", payment.invoice);
                                    Job.Functions.FindAndReplace(appWord, "%date" + no + "%", payment.date.ToShortDateString());
                                    Job.Functions.FindAndReplace(appWord, "%grade" + no + "%", payment.grade.code);
                                    if (payment.mt > 0)
                                    {
                                        Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", Job.Functions.MTToString(payment.mt));
                                    }
                                    else
                                    {
                                        Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", "");
                                    }
                                    totalMt += payment.mt;

                                    double rowAmount = payment.debit_amount;

                                    Job.Functions.FindAndReplace(appWord, "%amt" + no + "%", Job.Functions.AmountToString(rowAmount));
                                    totalAmount += rowAmount;
                                }*/

                                Job.Functions.FindAndReplace(appWord, "%total%", totalAmount.ToString("0.00"));
                                Job.Functions.FindAndReplace(appWord, "%mttotal%", totalMt.ToString("0.000"));
                                //Job.Functions.FindAndReplace(appWord, "%words%", Job.Functions.NumberToWords((int)totalAmount));

                                for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITADVISE; i++)
                                {
                                    no = i + 1;
                                    Job.Functions.FindAndReplace(appWord, "%in" + no + "%", "");
                                    Job.Functions.FindAndReplace(appWord, "%date" + no + "%", "");
                                    Job.Functions.FindAndReplace(appWord, "%grade" + no + "%", "");
                                    
                                    Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", "");
                                    Job.Functions.FindAndReplace(appWord, "%amt" + no + "%", "");
                                }

                                //doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA5;
                                //doc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;

                                //doc.PageSetup
                                if (printerName.Trim().Length > 0)
                                {
                                    doc.Application.ActivePrinter = printerName;
                                    if (Job.GeneralSettings.dadvise_tray() != null)
                                    {
                                        System.Drawing.Printing.PaperSource ps = new System.Drawing.Printing.PaperSource();
                                        ps.SourceName = Job.GeneralSettings.dadvise_tray();
                                        doc.PageSetup.FirstPageTray = (Word.WdPaperTray)ps.RawKind;
                                        doc.PageSetup.OtherPagesTray = (Word.WdPaperTray)ps.RawKind;
                                    }
                                }

                                /*doc.PageSetup.BottomMargin = 0.2f;
                                doc.PageSetup.LeftMargin = 1f;
                                doc.PageSetup.RightMargin = 0.2f;
                                doc.PageSetup.TopMargin = 0.3f;*/

                                object background = false;
                                object missing = Type.Missing;
                                doc.PrintOut(background, missing, missing, missing, missing, missing, missing, (int)noOfCopies);

                                doc.Close(false);
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                Job.Log("printingDebitNote", ex);
                            }
                            counter++;
                            action = () =>
                            {
                                this.pb.Value++;
                                lblProcess.Text = "[" + pb.Value + "/" + pb.Maximum + "]";
                                this.Activate();
                                this.Focus();
                            };
                            Invoke(action);
                        }
                        else
                        {
                            Action action = () =>
                            {
                                this.Activate();
                                this.Focus();
                            };
                            Invoke(action);
                        }
                    }
                    appWord.Application.Options.PrintBackground = backPrintingBG;
                    appWord.Quit();
                    appWord.Quit();
                    appWord = null;
                    System.GC.WaitForPendingFinalizers();
                    System.GC.Collect();

                    Action sendMsg = () =>
                    {
                        new forms.frmMessageSender(holders).ShowDialog(this);
                    };
                    Invoke(sendMsg);

                }
            }
            catch (Exception ex)
            {
                Job.Log("Error in printing system of debitnoteprinter.", ex);
            }
        }

        private List<IndividualGroup> getIndividualGroups()
        {
            List<IndividualGroup> groups = new List<IndividualGroup>();
            foreach (long paymentId in paymentIds)
            {
                Payment p = Job.Payments.get(paymentId);
                if (p != null)
                {
                    DateTime pDate = new DateTime(p.date.Year, p.date.Month, p.date.Day, 12, 0, 0);
                    IndividualGroup group = groups.Find(x => (x.client_id == p.client_id && x.date == pDate));
                    if (group != null)
                    {
                        group.entries.Add(p.id);
                    }
                    else
                    {
                        group = new IndividualGroup();
                        group.client_id = p.client_id;
                        group.date = pDate;
                        group.entries = new List<long>();
                        group.entries.Add(p.id);
                        groups.Add(group);
                    }
                }
            }
            return groups;
        }

        private class IndividualGroup
        {
            public long client_id;
            public DateTime date;
            public List<long> entries = new List<long>();
        }
    }
}
