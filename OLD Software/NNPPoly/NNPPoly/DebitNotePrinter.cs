using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Printing;
using Word = Microsoft.Office.Interop.Word;
using System.IO;

namespace NNPPoly
{
    public partial class DebitNotePrinter : Form
    {
        private bool finalCloser = false;
        private List<PaymentDetail> paymentDetails = new List<PaymentDetail>();
        private List<DebitNotePrint> debitNotes=null;
        private Thread genThread,priThread;
        private bool isDirect = false;
        public delegate void _ListOfDNotes(List<Decimal> listOfDNumbers);
        public event _ListOfDNotes ListOfDNotes;

        public DebitNotePrinter(List<PaymentDetail> paymentDetails)
        {
            InitializeComponent();
            this.paymentDetails = paymentDetails;
        }

        public DebitNotePrinter(List<DebitNotePrint> dnotes)
        {
            InitializeComponent();
            this.debitNotes = dnotes;
            this.isDirect = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure to stop debit note printing process ?", "Cancel printing job", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                finalCloser = true;
                DialogResult = DialogResult.Cancel;
                Close();
            }
            else
            {
                finalCloser = false;
            }
        }

        private void DebitNotePrinter_Load(object sender, EventArgs e)
        {
            if (debitNotes == null)
                debitNotes = new List<DebitNotePrint>();
        }

        private void DebitNotePrinter_Shown(object sender, EventArgs e)
        {
            lblPrintingStatus.Text = "Initilizing debit notes ...";
            lblDebitNotes.Text = "-/-";
                        
            genThread = new Thread(new ThreadStart(threadDebitNotePrinter));
            genThread.Name = "GenerateThread";
            genThread.Start();
        }

        public static PaperSize GetPaperSize(string Name)
        {
            PaperSize size1 = null;
            Name = Name.ToUpper();
            PrinterSettings settings = new PrinterSettings();
            foreach (PaperSize size in settings.PaperSizes)
                if (size.Kind.ToString().ToUpper() == Name)
                {
                    size1 = size;
                    break;
                }
            return size1;
        }

        private void threadDebitNotePrinter()
        {
            try
            {
                try
                {
                    List<ClientWisePayments> clientWisePayments;
                    if (!isDirect)
                    {
                        clientWisePayments = getClientWisePayments();
                        generateDebitNotes(ref clientWisePayments);
                    }
                    List<DebitNotePrint> _debitNotes=new List<DebitNotePrint>();
                    Action action = () => { _debitNotes = debitNotes; lblDebitNotes.Text = "0/" + debitNotes.Count; lblPrintingStatus.Text = "Initilizing printing ..."; btnCancel.Text = "&Stop"; };
                    Invoke(action);
                    if (_debitNotes.Count > 0)
                    {
                        bool isWordAvails = false;
                        try
                        {
                            Word.Application wordApp = new Word.Application();
                            wordApp.Quit();
                            wordApp.Quit();
                            wordApp = null;
                            isWordAvails = true;
                        }
                        catch (Exception) { isWordAvails = false; }
                        if (isWordAvails)
                        {
                            printDebitNotes(ref _debitNotes);
                            List<Decimal> decs = new List<Decimal>();
                            foreach (DebitNotePrint dnote in _debitNotes)
                                decs.Add(dnote.debitNoteNo);
                            action = () => { if (ListOfDNotes != null) ListOfDNotes(decs); finalCloser = true; DialogResult = DialogResult.OK; Close(); };
                            Invoke(action);
                        }
                        else
                        {
                            action = () => { lblPrintingStatus.Text = "Please install Microsoft Word properly."; pb.Style = ProgressBarStyle.Blocks; finalCloser = true; btnCancel.Text = "&Close"; };
                            Invoke(action);
                        }
                    }
                    else
                    {
                        Action _action = () => { pb.Style = ProgressBarStyle.Blocks; lblPrintingStatus.Text = "Nothing to print!!"; btnCancel.Text = "&Close"; finalCloser = true; };
                        Invoke(_action);
                    }
                }
                catch (ThreadAbortException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {

                }
            }
            catch (ThreadAbortException) { }
        }

        private void printDebitNotes(ref List<DebitNotePrint> dnotes)
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
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                if (System.IO.File.Exists(tempFile))
                {
                    int counter = 0, totalNotess = dnotes.Count;
                    String printerName = Datastore.dataFile.printerName;
                    Word.Application appWord = new Word.Application();
                    bool backPrintingBG = appWord.Application.Options.PrintBackground;
                    appWord.Application.Options.PrintBackground = false;

                    Decimal noOfCopies = Datastore.dataFile.noOfCopies;
                    Action myAction = () =>
                    {
                        NoOfPrints nop = new NoOfPrints();
                        nop.Text = "How many copy of Notes printed ?";
                        nop.ShowDialog(this);
                        noOfCopies = nop.ValueFromUser;
                    };
                    Invoke(myAction);


                    foreach (DebitNotePrint dnote in dnotes)
                    {
                        UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.Trim().ToLower().Replace(" ", "").Equals(dnote.toClientName.Trim().ToLower().Replace(" ", ""))));
                        if (client != null && !isDirect)
                        {
                            Payment payment = new Payment();
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
                            /*foreach (Payment p in client.Payments)
                                if (p.ID > payment.ID)
                                    payment.ID = p.ID;
                            payment.ID++;*/
                            payment.ID = ++client.PaymentIDManager;
                            client.Payments.Add(payment);
                            dnote.ID = payment.ID;
                        }

                        if (!isDirect)
                            Datastore.dataFile.DebitNotes.Add(dnote);

                        if (noOfCopies > 0)
                        {
                            Action action = () =>
                            {
                                lblPrintingStatus.Text = "Printing ...";
                                lblDebitNotes.Text = counter + "/" + totalNotess;
                                pb.Style = ProgressBarStyle.Blocks;
                                pb.Maximum = totalNotess;
                                pb.Value = counter;
                            };
                            Invoke(action);
                            try
                            {
                                #region init-word-doc
                                Word.Document doc = (Word.Document)appWord.Documents.Open(tempFile);

                                GeneralReport.FindAndReplace(appWord, "%title%", Datastore.dataFile.titleOfaDNote, true, doc);
                                GeneralReport.FindAndReplace(appWord, "%address%", Datastore.dataFile.addressOfaDNote, true, doc);
                                GeneralReport.FindAndReplace(appWord, "%to%", dnote.toClientName, true, doc);
                                GeneralReport.FindAndReplace(appWord, "%debitno%", dnote.debitNoteNo.ToString("0"), true, doc);
                                GeneralReport.FindAndReplace(appWord, "%date%", dnote.debitDate.ToString(Program.SystemSDFormat), true, doc);
                                GeneralReport.FindAndReplace(appWord, "%description_tablerow%", Datastore.dataFile.descriptionRowOfaDNote);
                                GeneralReport.FindAndReplace(appWord, "%total%", dnote.totalAmount.ToString("0.00"));
                                GeneralReport.FindAndReplace(appWord, "%words%", Main.NumberToWords((int)dnote.totalAmount));

                                for (int i = 0; i < Datastore.dataFile.noOfDebitNoteRows && i < dnote.debitNoteRows.Count; i++)
                                {
                                    int no = i + 1;
                                    DebitNoteRow row = dnote.debitNoteRows[i];
                                    Payment payment = client.Payments.Find(x => (x.ID == (Decimal)row.ID));
                                    if (payment == null) 
                                    {
                                        Log.output("Payment ignorance in debitnoteprinter.",new Exception("Something gonna wrong!!"));
                                        continue;
                                    }
                                    GeneralReport.FindAndReplace(appWord, "%in" + no + "%", payment.DocChqNo.Trim());
                                    GeneralReport.FindAndReplace(appWord, "%date" + no + "%", payment.Date.ToString(Program.SystemSDFormat));
                                    GeneralReport.FindAndReplace(appWord, "%grade" + no + "%", payment.Grade);
                                    double mt = 0;
                                    double.TryParse(payment.MT, out mt);
                                    GeneralReport.FindAndReplace(appWord, "%mt" + no + "%", mt.ToString("0.000"));
                                    GeneralReport.FindAndReplace(appWord, "%amt" + no + "%", row.Amount.ToString("0.00"));
                                }

                                for (int i = 0; i < Datastore.dataFile.noOfDebitNoteRows; i++)
                                {
                                    int no = i + 1;
                                    GeneralReport.FindAndReplace(appWord, "%in" + no + "%", "");
                                    GeneralReport.FindAndReplace(appWord, "%date" + no + "%", "");
                                    GeneralReport.FindAndReplace(appWord, "%grade" + no + "%", "");
                                    GeneralReport.FindAndReplace(appWord, "%mt" + no + "%", "");
                                    GeneralReport.FindAndReplace(appWord, "%amt" + no + "%", "");
                                }

                                /*doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA5;
                                doc.PageSetup.Orientation = Word.WdOrientation.wdOrientLandscape;
                                */
                                //doc.PageSetup
                                if (printerName.Trim().Length > 0)
                                {
                                    doc.Application.ActivePrinter = printerName;
                                    if (Datastore.dataFile.debitNotePaperSource != null)
                                    {
                                        doc.PageSetup.FirstPageTray = (Word.WdPaperTray)Datastore.dataFile.debitNotePaperSource.RawKind;
                                        doc.PageSetup.OtherPagesTray = (Word.WdPaperTray)Datastore.dataFile.debitNotePaperSource.RawKind;
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
                                Console.WriteLine("MYEXCEP:" + ex);
                            }
                            counter++;
                            action = () =>
                            {
                                lblPrintingStatus.Text = "Printing ...";
                                lblDebitNotes.Text = counter + "/" + totalNotess;
                                pb.Style = ProgressBarStyle.Blocks;
                                pb.Maximum = totalNotess;
                                pb.Value = counter;
                                this.Activate();
                                this.Focus();
                            };
                            Invoke(action);
                        }
                        else
                        {
                            Action action = () =>
                            {
                                lblPrintingStatus.Text = "Not printing anything ...";
                                lblDebitNotes.Text = "-/-";
                                pb.Style = ProgressBarStyle.Blocks;
                                pb.Maximum = 100;
                                pb.Value = 100;
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
               Log.output("Error in printing system of debitnoteprinter.", ex);
            }
        }

        private void generateDebitNotes(ref List<ClientWisePayments> clientWiseReports)
        {
            foreach (ClientWisePayments client in clientWiseReports)
            {
                UserAccount account = Datastore.dataFile.UserAccounts.Find(x => (x.ID == client.clientId));
                if (account != null)
                {
                    DebitNotePrint debitNotePrint = new DebitNotePrint();
                    debitNotePrint.clientID = client.clientId;
                    debitNotePrint.debitDate = client.Date;
                    debitNotePrint.debitNoteNo = ++Datastore.dataFile.currentDebitNoteID;
                    debitNotePrint.toClientName = account.ClientName;
                    if (client.payments.Count <= Datastore.dataFile.noOfDebitNoteRows)
                    {
                        debitNotePrint.debitNoteRows = getDebitNoteRows(client.payments,account.CutOffDays);
                    }
                    else
                    {
                        //Not working
                        int noOfLaps = (int)Math.Ceiling((double)(client.payments.Count / Datastore.dataFile.noOfDebitNoteRows));
                        debitNotePrint.debitNoteRows = new List<DebitNoteRow>();
                        for (int i = 0; i < noOfLaps; i++)
                        {
                            Payment[] tmpPayments=new Payment[Datastore.dataFile.noOfDebitNoteRows];
                            client.payments.CopyTo(0*Datastore.dataFile.noOfDebitNoteRows, tmpPayments, 0, Datastore.dataFile.noOfDebitNoteRows);
                            List<DebitNoteRow> tempRows = getDebitNoteRows(tmpPayments.ToList<Payment>(), account.CutOffDays);
                            debitNotePrint.debitNoteRows.AddRange(tempRows);
                        }
                    }
                    if(debitNotePrint.debitNoteRows.Count>0)
                    {
                        debitNotePrint.totalAmount = 0;
                        foreach (DebitNoteRow row in debitNotePrint.debitNoteRows)
                            debitNotePrint.totalAmount += row.Amount;
                        Action action = () => { debitNotes.Add(debitNotePrint); };
                        Invoke(action);
                    }
                }
            }
        }

        private List<DebitNoteRow> getDebitNoteRows(List<Payment> payments, double cutOffDays)
        {
            List<DebitNoteRow> rows = new List<DebitNoteRow>();
            for (int i = 0; i < Datastore.dataFile.noOfDebitNoteRows && i<payments.Count; i++)
            {
                DebitNoteRow row = new DebitNoteRow();
                Payment payment = payments[i];

                /*row.InvoiceNo = payment.DocChqNo;
                row.Date = payment.Date;
                row.Grade = payment.Grade;
                row.Qty = payment.MT;*/
                row.ID = payment.ID;
                double mtVal=0;
                double.TryParse(payment.MT,out mtVal);
                Grade grade=Datastore.dataFile.Grades.Find(x => (x.GradeName.Trim().ToLower().Equals(payment.Grade.ToLower().Trim())));
                double gradeAmt = 100;
                if (grade != null)
                    gradeAmt = grade.Amount;
                row.Amount = gradeAmt * cutOffDays * mtVal;
                if (mtVal != 0)
                    rows.Add(row);
            }
            return rows;
        }

        private List<ClientWisePayments> getClientWisePayments()
        {
            List<PaymentDetail> tempPDLists=new List<PaymentDetail>();
            List<ClientWisePayments> listOfClients = new List<ClientWisePayments>();
            Action action = () => {
                tempPDLists = paymentDetails;
            };
            Invoke(action);
            foreach (PaymentDetail pd in tempPDLists)
            {
                ClientWisePayments cwp = listOfClients.Find(x => (x.clientId == pd.clientId && x.Date.CompareTo(pd.payment.Date) == 0));
                if (cwp == null)
                {
                    cwp = new ClientWisePayments();
                    cwp.clientId = pd.clientId;
                    cwp.Date = pd.payment.Date;
                    cwp.payments = new List<Payment>();
                    cwp.payments.Add(pd.payment);
                    listOfClients.Add(cwp);
                }
                else
                {
                    cwp.payments.Add(pd.payment);
                }
            }
            return listOfClients;
        }

        private void DebitNotePrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finalCloser)
            {
                e.Cancel = true;
            }
            else
            {
                if (genThread != null && genThread.IsAlive)
                    genThread.Abort();
                if (priThread != null && priThread.IsAlive)
                    priThread.Abort();
            }
        }

    }

    public class ClientWisePayments
    {
        public Decimal clientId;
        public DateTime Date;
        public List<Payment> payments;
    }

}
