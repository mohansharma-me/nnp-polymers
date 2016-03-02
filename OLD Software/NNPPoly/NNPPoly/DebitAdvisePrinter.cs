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

namespace NNPPoly
{
    public partial class DebitAdvisePrinter : Form
    {
        private Thread thProcess;
        private bool finalCloser = false, isDirect = false;
        private List<DebitNotePrint> debitNotes;
        public List<PaymentDetail> paymentDetails;
        public List<Decimal> listOfDNoteIDs;


        public DebitAdvisePrinter(List<PaymentDetail> paymentDetails,List<Decimal> listOfDNoteIDs)
        {
            InitializeComponent();
            this.paymentDetails = paymentDetails;
            this.listOfDNoteIDs = listOfDNoteIDs;
        }

        public DebitAdvisePrinter(List<DebitNotePrint> debitAdvises)
        {
            InitializeComponent();
            this.paymentDetails = null;
            this.listOfDNoteIDs = null;
            this.debitNotes = debitAdvises;
            isDirect = true;
        }



        private void DebitAdvisePrinter_Load(object sender, EventArgs e)
        {
            if(!isDirect)
            debitNotes = new List<DebitNotePrint>();
        }

        private void DebitAdvisePrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!finalCloser)
            {
                e.Cancel = true;
            }
            else
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure to stop debit advise printing process ?", "Cancel printing job", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void DebitAdvisePrinter_Shown(object sender, EventArgs e)
        {
            lblPrintingStatus.Text = "Initilizing debit advises ...";
            lblDebitNotes.Text = "0/0";

            thProcess = new Thread(new ThreadStart(threadDebitAdvisePrinter));
            thProcess.Name = "GenerateDebitAdvises";
            thProcess.Start();
        }

        private void threadDebitAdvisePrinter() {
            try
            {
                try
                {
                    if (!isDirect)
                    {
                        List<ClientWisePayments> clientWisePayments = getClientWisePayments();
                        generateDebitNotes(ref clientWisePayments);
                    }
                    List<DebitNotePrint> _debitNotes = new List<DebitNotePrint>();
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
                            /*List<Decimal> decs = new List<Decimal>();
                            foreach (DebitNotePrint dnote in _debitNotes)
                                decs.Add(dnote.debitNoteNo);*/
                            action = () => { finalCloser = true; DialogResult = DialogResult.OK; Close(); };
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
                    //appWord.Visible = true;
                    bool backPrintingBG = appWord.Application.Options.PrintBackground;
                    appWord.Application.Options.PrintBackground = false;

                    Decimal noOfCopies = Datastore.dataFile.noOfCopies;
                    List<Message> messages = new List<Message>();
                    //bool sendDispatchMsgs = false;
                    Action myAction = () =>
                    {
                        NoOfPrints nop = new NoOfPrints();
                        nop.Text = "How many copy of Advises printed ?";
                        nop.ShowDialog(this);
                        noOfCopies = nop.ValueFromUser;

                        /*if (!isDirect)
                        {
                            DialogResult dr = MessageBox.Show(this, "Do you need send despatch message for this all debit advises ?", "Send Despatch ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                                sendDispatchMsgs = true;
                            else
                                sendDispatchMsgs = false;
                        }*/
                    };
                    Invoke(myAction);

                    foreach (DebitNotePrint dnote in dnotes)
                    {
                        UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.Trim().ToLower().Replace(" ", "").Equals(dnote.toClientName.Trim().ToLower().Replace(" ", ""))));
                        #region MyCommentedRegion
                        /*if (client != null)
                    {
                        Payment payment = new Payment();
                        payment.CollectingAmount = 0;
                        payment.Credit = 0;
                        payment.Date = dnote.debitDate;
                        payment.Debit = dnote.totalAmount;
                        payment.DocChqNo = dnote.debitNoteNo.ToString();
                        payment.Grade = "";
                        payment.ID = 0;
                        payment.MT = "0";
                        payment.Particulars = "Debit Note Print";
                        payment.Remain = 0;
                        payment.Type = "Jrnl";
                        foreach (Payment p in client.Payments)
                            if (p.ID > payment.ID)
                                payment.ID = p.ID;
                        payment.ID++;
                        client.Payments.Add(payment);
                    }*/

                        #endregion

                        if (!isDirect)
                            Datastore.dataFile.DebitAdvises.Add(dnote);

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
                                #region init-word
                                Word.Document doc = (Word.Document)appWord.Documents.Open(tempFile);

                                GeneralReport.FindAndReplace(appWord, "%title%", Datastore.dataFile.titleOfaDNote, true, doc);
                                GeneralReport.FindAndReplace(appWord, "%address%", Datastore.dataFile.addressOfaDAdvise, true, doc);
                                GeneralReport.FindAndReplace(appWord, "%to%", dnote.toClientName, true, doc);
                                //GeneralReport.FindAndReplace(appWord, "%debitno%", dnote.debitNoteNo.ToString("0"), true, doc);
                                //GeneralReport.FindAndReplace(appWord, "%date%", dnote.debitDate.ToString(Program.SystemSDFormat), true, doc);
                                //GeneralReport.FindAndReplace(appWord, "%description_tablerow%", Datastore.dataFile.descriptionRowOfaDNote);
                                GeneralReport.FindAndReplace(appWord, "%total%", dnote.totalAmount.ToString("0.00"));
                                //GeneralReport.FindAndReplace(appWord, "%words%", Main.NumberToWords((int)dnote.totalAmount));
                                List<String> grades = new List<String>();
                                double mtTotal = 0;
                                for (int i = 0; i < Datastore.dataFile.noOfDebitAdviseRows && i < dnote.debitNoteRows.Count; i++)
                                {
                                    int no = i + 1;
                                    DebitNoteRow row = dnote.debitNoteRows[i];
                                    Payment payment = client.Payments.Find(x => (x.ID == (Decimal)row.ID));
                                    if (payment == null)
                                    {
                                        Log.output("Payment ignorance in debitadviseprinter.", new Exception("Something gonna wrong!!"));
                                        continue;
                                    }
                                    GeneralReport.FindAndReplace(appWord, "%in" + no + "%", payment.DocChqNo.Trim());
                                    GeneralReport.FindAndReplace(appWord, "%date" + no + "%", payment.Date.ToString(Program.SystemSDFormat));
                                    GeneralReport.FindAndReplace(appWord, "%grade" + no + "%", payment.Grade);
                                    if (payment.Grade != null && payment.Grade.Trim().Length > 0)
                                    {
                                        if(!grades.Contains(payment.Grade.Trim())) 
                                        {
                                            grades.Add(payment.Grade.Trim());
                                        }
                                    }

                                    double mt = 0;
                                    double.TryParse(payment.MT, out mt);
                                    if (mt == 0)
                                    {
                                        if (!payment.DocChqNo.ToLower().Trim().StartsWith("dn no."))
                                            GeneralReport.FindAndReplace(appWord, "%mt" + no + "%", "Freight");
                                        else
                                            GeneralReport.FindAndReplace(appWord, "%mt" + no + "%", "");
                                    }
                                    else
                                    {
                                        GeneralReport.FindAndReplace(appWord, "%mt" + no + "%", mt.ToString("0.000"));
                                    }
                                    mtTotal += mt;
                                    GeneralReport.FindAndReplace(appWord, "%amt" + no + "%", row.Amount.ToString("0.00"));
                                }

                                GeneralReport.FindAndReplace(appWord, "%mttotal%", mtTotal.ToString("0.000"));

                                for (int i = 0; i < Datastore.dataFile.noOfDebitAdviseRows; i++)
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
                                    if (Datastore.dataFile.debitAdvisePaperSource != null)
                                    {
                                        doc.PageSetup.FirstPageTray = (Word.WdPaperTray)Datastore.dataFile.debitAdvisePaperSource.RawKind;
                                        doc.PageSetup.OtherPagesTray = (Word.WdPaperTray)Datastore.dataFile.debitAdvisePaperSource.RawKind;
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
                                Console.WriteLine("myexcep:" + ex);
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
                                lblPrintingStatus.Text = "Not printing anything...";
                                lblDebitNotes.Text = "-/-";
                                pb.Style = ProgressBarStyle.Blocks;
                                pb.Maximum = 100;
                                pb.Value = 100;
                                this.Activate();
                                this.Focus();
                            };
                            Invoke(action);
                        }

                        #region send-despatch-message
                        if (client != null)
                        {
                            try
                            {
                                if (client.mobileNumber.Trim().Length > 0)
                                {
                                    String date = dnote.debitDate.ToString("dd.MM.yy");
                                    String _grades = "";

                                    double mtTotal = 0;
                                    String trs = "";
                                    foreach (DebitNoteRow row in dnote.debitNoteRows)
                                    {
                                        Payment payment = client.Payments.Find(x => (x.ID == (Decimal)row.ID));
                                        if (payment != null)
                                        {
                                            if (payment.Grade.Trim().Length > 0)
                                            {
                                                if (!_grades.ToLower().Trim().Contains(payment.Grade.Trim().ToLower()))
                                                {
                                                    _grades = _grades + " " + payment.Grade;
                                                }
                                            }
                                            double mtVal = 0;
                                            if (payment.MT != null && double.TryParse(payment.MT, out mtVal))
                                            {
                                                mtTotal += mtVal;
                                            }

                                            trs += "<tr><td>" + row.Amount.ToString("0.00") + "</td><td>" + payment.DocChqNo + "</td><td>" + payment.Date.ToString(Program.SystemSDFormat) + "</td><td>" + payment.Grade + "</td><td>" + mtVal.ToString("0.000") + "</td></tr>";
                                        }
                                    }

                                    String qty = mtTotal.ToString("0.000");
                                    String amt = dnote.totalAmount.ToString("0.00");

                                    String url = Datastore.dataFile.msg_Dispatch;
                                    url = url.Replace("%date%", date);
                                    url = url.Replace("%grade%", _grades);
                                    url = url.Replace("%qty%", qty);
                                    url = url.Replace("%amt%", amt);
                                    url = Uri.EscapeDataString(url);
                                    url = Datastore.dataFile.sms_API.Replace("%numbers%", client.mobileNumber).Replace("%msg%", url);

                                    Message.Mail mail = null;
                                    if (client.emailAddress.Trim().Length > 0)
                                    {
                                        String html = Datastore.dataFile.mail_Despatch+"<br/><br/>";
                                        html += "<center><table cellspacing=2 cellpadding=2 border=1>";
                                        html += "<tr><th>INVOICE AMOUNT</th><TH>INVOICE NUMBER</TH><TH>INVOICE DATE</TH><TH>GRADE</TH><TH>QUANTITY</TH></tr>";
                                        html += trs;
                                        html += "<tr><td>" + dnote.totalAmount.ToString("0.00") + "</td><td colspan=3></td><td>" + mtTotal.ToString("0.000") + "</td></tr>";
                                        html += "</table></center>";
                                        mail = new Message.Mail(client.emailAddress, "MATERIAL DESPATCH ADVISE", html);
                                    }

                                    messages.Add(new Message(client.ClientName, client.mobileNumber, MessageType.Despatch, url, mail,client.ID));
                                    /*object obj = Datastore.DownloadString(url);
                                    if (obj is Exception)
                                    {
                                        Log.output("Can't send despatch message.", obj as Exception);
                                    }
                                    else
                                    {
                                        Console.WriteLine("TN:" + obj);
                                    }*/
                                    
                                }
                            }
                            catch (Exception ex)
                            {
                                Log.output("Can't send despatch message.", ex);
                            }
                        }
                        #endregion
                    }

                    if (messages.Count > 0)
                    {
                        Action act = () => {
                            MSGSender msgsender = new MSGSender(messages);
                            msgsender.ShowDialog(this);
                        };
                        Invoke(act);
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
                Log.output("Error in printing system of debitadviseprinter.", ex);
            }
        }

        private void generateDebitNotes(ref List<ClientWisePayments> clientWisePayments)
        {
            foreach (ClientWisePayments client in clientWisePayments)
            {
                UserAccount account = Datastore.dataFile.UserAccounts.Find(x => (x.ID == client.clientId));
                if (account != null)
                {
                    DebitNotePrint debitNotePrint = new DebitNotePrint();
                    debitNotePrint.debitDate = client.Date;
                    debitNotePrint.clientID = client.clientId;
                    debitNotePrint.debitNoteNo = ++Datastore.dataFile.currentDebitAdviseID;
                    debitNotePrint.toClientName = account.ClientName;
                    if (client.payments.Count <= Datastore.dataFile.noOfDebitAdviseRows)
                    {
                        debitNotePrint.debitNoteRows = getDebitNoteRows(client.payments, account.CutOffDays);
                    }
                    else
                    {
                        //Not Working
                        int noOfLaps = (int)Math.Ceiling((double)(client.payments.Count / Datastore.dataFile.noOfDebitAdviseRows));
                        debitNotePrint.debitNoteRows = new List<DebitNoteRow>();
                        for (int i = 0; i < noOfLaps; i++)
                        {
                            Payment[] tmpPayments = new Payment[Datastore.dataFile.noOfDebitAdviseRows];
                            client.payments.CopyTo(0 * Datastore.dataFile.noOfDebitAdviseRows, tmpPayments, 0, Datastore.dataFile.noOfDebitAdviseRows);
                            List<DebitNoteRow> tempRows = getDebitNoteRows(tmpPayments.ToList<Payment>(), account.CutOffDays);
                            debitNotePrint.debitNoteRows.AddRange(tempRows);
                        }
                    }

                    //List<DebitNotePrint> tempDNS = Datastore.dataFile.DebitNotes.FindAll(x => (x.toClientName.Equals(account.ClientName)));
                    List<DebitNotePrint> tempDNS = Datastore.dataFile.DebitNotes.FindAll(x => (x.clientID == account.ID));
                    if (tempDNS != null)
                    {
                        foreach (DebitNotePrint dnp in tempDNS)
                        {
                            Decimal dnpID = -1;
                            dnpID = listOfDNoteIDs.Find(x => (x == dnp.debitNoteNo));
                            if (dnpID > -1 && dnp.debitDate.CompareTo(debitNotePrint.debitDate)==0)
                            {
                                DebitNoteRow row = new DebitNoteRow();
                                /*row.InvoiceNo = "DN No."+dnpID.ToString();
                                row.Date = dnp.debitDate;
                                row.Grade = "";
                                row.Qty = "";*/
                                row.ID = (Decimal)dnp.ID;
                                row.Amount = dnp.totalAmount;
                                debitNotePrint.debitNoteRows.Add(row);
                            }
                        }
                    }

                    if (debitNotePrint.debitNoteRows.Count > 0)
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
                if (mtVal == 0)
                {
                    //row.Grade = "Freight";
                    // do something better.
                }
                row.Amount = payment.Debit;

                rows.Add(row);
            }

            return rows;
        }

        private List<ClientWisePayments> getClientWisePayments()
        {
            List<PaymentDetail> tempPDLists = new List<PaymentDetail>();
            List<ClientWisePayments> listOfClients = new List<ClientWisePayments>();
            Action action = () =>
            {
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

        private void lblDebitNotes_Click(object sender, EventArgs e)
        {

        }
    }

}
