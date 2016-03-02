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
    public partial class frmDebitNotePrinter : Form
    {
        private Decimal noOfCopies = 1;
        public List<long> debitNoteIDs = new List<long>();
        private bool isDirect = false;
        private List<long> paymentIds = null;

        public frmDebitNotePrinter(List<long> paymentIds)
        {
            InitializeComponent();
            this.paymentIds = paymentIds;
        }

        public frmDebitNotePrinter(List<long> dnoteIDs, bool isDirect)
        {
            InitializeComponent();
            this.isDirect = isDirect;
            this.paymentIds = dnoteIDs;
        }


        private void frmDebitNotePrinter_Load(object sender, EventArgs e)
        {

        }

        private void frmDebitNotePrinter_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Action act = () =>
                {
                    frmAskUser ask = new frmAskUser("Number of copies required of Debit Note ?", "0 means skip printing debit notes", noOfCopies + "", frmAskUser.ValueType.Long);
                    ask.ShowDialog(this);
                    Decimal.TryParse(ask.getText(), out noOfCopies);
                };
                Invoke(act);

                if (!isDirect)
                {
                    List<IndividualGroup> groups = getIndividualGroups();
                    act = () => { pb.Maximum = groups.Count; pb.Value = 0; };
                    Invoke(act);
                    foreach (IndividualGroup group in groups)
                    {
                        // debit note generation,,, currently skiped
                        long newId = 0;
                        Job.DebitNotes.add(ref newId, group.client_id, true, 0, group.date, group.entries);
                        debitNoteIDs.Add(newId);
                        //printDebitNote(newId);
                        //act = () => { pb.Value++; };
                        //Invoke(act);
                    }
                }

                act = () =>
                {
                    pb.Style = ProgressBarStyle.Blocks;
                    pb.Maximum = isDirect ? paymentIds.Count : debitNoteIDs.Count;
                    pb.Value = 0;
                    lblProcess.Text = "[0/" + pb.Maximum + "]";
                };
                Invoke(act);

                if (!isDirect)
                    printDebitNote(debitNoteIDs);
                else
                    printDebitNote(paymentIds);

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

        private void printDebitNote(List<long> ids)
        {
            String tempFile = Application.StartupPath + "\\a5_debitnote_template.docx";
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
                        File.WriteAllBytes(tempFile, Properties.Resources.DebitNotePrintFormat);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error in writing document file to saving location." + Environment.NewLine + "Error message:" + Environment.NewLine + ex.Message, "Document write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize template_a5_debitnote operation.";
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

                    /*Decimal noOfCopies = 1;
                    Action myAction = () =>
                    {
                        frmAskUser ask = new frmAskUser("How many copies of Debit Note print ?", "0 means skip", "1", frmAskUser.ValueType.Long);
                        ask.ShowDialog(this);
                        Decimal.TryParse(ask.getText(), out noOfCopies);
                    };
                    Invoke(myAction);*/


                    foreach (long id in ids)
                    {
                        DebitNote dn = Job.DebitNotes.get(id);
                        if (dn == null)
                        {
                            continue;
                        }

                        //UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.Trim().ToLower().Replace(" ", "").Equals(dnote.toClientName.Trim().ToLower().Replace(" ", ""))));
                        Client client = Job.Clients.get(dn.client_id);
                        if (client != null && !isDirect)
                        {
                            /*Payment payment = new Payment(0);
                            payment.CollectingAmount = 0;
                            payment.Credit = 0;
                            payment.Date = dnote.debitDate;
                            payment.Debit = dnote.totalAmount;
                            payment.DocChqNo = "DN No." + dnote.debitNoteNo.ToString();
                            payment.Grade = "";
                            payment.ID = 0;
                            payment.MT = "0";
                            payment.Particulars = "Debit Note Print";
                            payment.Remain = 0;
                            payment.Type = "Jrnl";
                            payment.ID = ++client.PaymentIDManager;
                            client.Payments.Add(payment);
                            dnote.ID = payment.ID;*/

                            double totalAmount = Job.DebitNotes.countDebitNoteRows(dn);

                            /*for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITNOTE && i < dn.entries.Count; i++)
                            {
                                Payment payment = Job.Payments.get(dn.entries[i].paymentId);
                                double rowAmount = payment.grade.getAmount(payment.date) * client.cutoffdays * payment.mt;
                                totalAmount += rowAmount;
                            }*/

                            long newId=0;
                            Job.Payments.add(ref newId, dn.client_id, dn.date, "" + dn.id, "Jrnl", "Debit Note Print", Payment.PaymentMode.Debit, totalAmount, 0, 0, dn.id);

                        }

                        if (!isDirect)
                        {
                            //Datastore.dataFile.DebitNotes.Add(dnote);
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
                                Job.Functions.FindAndReplace(appWord, "%debitno%", id, true, doc);
                                Job.Functions.FindAndReplace(appWord, "%date%", dn.date.ToShortDateString(), true, doc);
                                Job.Functions.FindAndReplace(appWord, "%description_tablerow%", Job.GeneralSettings.mid_row_text());

                                double totalAmount = 0;

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
                                    Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", Job.Functions.MTToString(payment.mt));

                                    //double rowAmount = payment.grade.getAmount(payment.date) * client.cutoffdays * payment.mt;

                                    Job.Functions.FindAndReplace(appWord, "%amt" + no + "%", Job.Functions.AmountToString(rowAmount));
                                    
                                });

                                /*
                                for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITNOTE && i < dn.entries.Count; i++)
                                {



                                    //DebitNoteRow row = dnote.debitNoteRows[i];
                                    Payment payment = Job.Payments.get(dn.entries[i].paymentId);
                                    if (payment == null)
                                    {
                                        Job.Log("Payment ignorance in debitnoteprinter.", new Exception("Something gonna wrong!!"));
                                        continue;
                                    }

                                    if (payment.mt == 0) continue;

                                    int no = i + 1;

                                    Job.Functions.FindAndReplace(appWord, "%in" + no + "%", payment.invoice);
                                    Job.Functions.FindAndReplace(appWord, "%date" + no + "%", payment.date.ToShortDateString());
                                    Job.Functions.FindAndReplace(appWord, "%grade" + no + "%", payment.grade.code);
                                    Job.Functions.FindAndReplace(appWord, "%mt" + no + "%", Job.Functions.MTToString(payment.mt));

                                    double rowAmount = payment.grade.getAmount(payment.date) * client.cutoffdays * payment.mt;

                                    Job.Functions.FindAndReplace(appWord, "%amt" + no + "%", Job.Functions.AmountToString(rowAmount));
                                    totalAmount += rowAmount;
                                }*/

                                Job.Functions.FindAndReplace(appWord, "%total%", totalAmount.ToString("0.00"));
                                Job.Functions.FindAndReplace(appWord, "%words%", Job.Functions.NumberToWords((int)totalAmount));

                                for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITNOTE; i++)
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
                                    if (Job.GeneralSettings.dnote_tray() != null)
                                    {
                                        System.Drawing.Printing.PaperSource ps = new System.Drawing.Printing.PaperSource();
                                        ps.SourceName = Job.GeneralSettings.dnote_tray();
                                        doc.PageSetup.FirstPageTray = (Word.WdPaperTray)ps.RawKind;
                                        doc.PageSetup.OtherPagesTray = (Word.WdPaperTray)ps.RawKind;
                                    }
                                }



                                doc.PageSetup.BottomMargin = 0.2f;
                                doc.PageSetup.LeftMargin = 1f;
                                doc.PageSetup.RightMargin = 0.2f;
                                doc.PageSetup.TopMargin = 0.3f;

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
                if (p != null && p.mt != 0)
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
