 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly
{
    public partial class ImportSupply : Form
    {
        public List<Decimal> listOfDNotes=new List<Decimal>();
        public List<UserAccount> clients;

        public ImportSupply(List<UserAccount> clients)
        {
            InitializeComponent();
            this.clients = clients;
        }

        private void ImportSupply_Load(object sender, EventArgs e)
        {
            if(Datastore.dataFile.DebitNotes==null) 
                Datastore.dataFile.DebitNotes = new List<DebitNotePrint>();
            if (clients == null) clients = new List<UserAccount>();
            foreach (UserAccount client in clients)
            {
                foreach (Payment payment in client.Payments)
                {
                    int index=dv.Rows.Add("1. NONE",client.ClientName,payment.DocChqNo,payment.Date.ToString(Program.SystemSDFormat),payment.Grade,payment.MT,payment.Debit.ToString());
                    dv.Rows[index].Tag = client.ID;
                    UserAccount ua = Datastore.dataFile.UserAccounts.Find(x=>(x.ClientName.ToLower().Trim().Replace(" ","").Equals(client.ClientName.ToLower().Trim().Replace(" ",""))));
                    if (ua == null)
                        dv.Rows[index].DefaultCellStyle.Font = new Font(dv.DefaultCellStyle.Font, FontStyle.Underline);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            List<PaymentDetail> debitNotePayments = new List<PaymentDetail>();
            List<PaymentDetail> debitAdvicePayments = new List<PaymentDetail>();
            foreach (DataGridViewRow row in dv.Rows)
            {
                if (row.IsNewRow) continue;
                bool isDebitAdvice = true, isDebitNote = false, isEnvelope = false, isNone = false;
                
                try
                {
                    String cmbStr = (String)row.Cells[0].Value;
                    if (cmbStr == null)
                    {
                        isNone = true;
                    }
                    else if (cmbStr.StartsWith("1"))
                    {
                        isDebitNote = isEnvelope = false;
                        isNone = true;
                    }
                    else if (cmbStr.StartsWith("2"))
                    {
                        isEnvelope = isNone = false;
                        isDebitNote = true;
                    }
                    else if (cmbStr.StartsWith("3"))
                    {
                        isDebitNote = isNone = false;
                        isEnvelope = true;
                    }
                    else if (cmbStr.StartsWith("4"))
                    {
                        isNone = false;
                        isEnvelope = isDebitNote = true;
                    }


                    //isDebitNote = (bool)row.Cells[0].Value;
                }
                catch (Exception) { }
                String clientName = (String)row.Cells[1].Value;
                String invoiceNo = (String)row.Cells[2].Value;
                String pDate = (String)row.Cells[3].Value;
                String pGrade = (String)row.Cells[4].Value;
                pGrade = pGrade == null ? "" : pGrade;
                String pQty = (String)row.Cells[5].Value;
                String pAmount = (String)row.Cells[6].Value;

                double pMT = 0, pAmt = 0;
                if (clientName==null || (clientName!=null && clientName.Trim().Length == 0))
                {
                    MessageBox.Show(this,"Please enter valid client name.","No client name",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[1];
                    return;
                }
                if (invoiceNo==null ||  (invoiceNo!=null && invoiceNo.Trim().Length == 0))
                {
                    MessageBox.Show(this,"Please enter valid invoice number.","No invoice number",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[2];
                    return;
                }
                DateTime payDate=DateTime.Today;
                if (pDate == null || (pDate != null && !DateTime.TryParse(pDate, out payDate)))
                {
                    MessageBox.Show(this,"Please enter valid date.","No valid date",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[3];
                    return;
                }
                if (pQty == null || (pQty != null && !double.TryParse(pQty, out pMT)))
                {
                    MessageBox.Show(this,"Please enter valid quantity( MT ).","No valid quantity",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[5];
                    return;
                }
                if (pAmount == null || (pAmount != null && !double.TryParse(pAmount, out pAmt)))
                {
                    MessageBox.Show(this,"Please enter valid amount.","No valid amount",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[6];
                    return;
                }   
                Payment payment = new Payment();
                payment.Credit = 0;
                payment.Date = payDate;
                payment.Debit = pAmt;
                payment.DocChqNo = invoiceNo;
                payment.Grade = pGrade == null ? "" : pGrade;
                payment.HighlightThis = false;
                payment.MT = pMT.ToString();
                payment.Remain = 0;
                if (pMT == 0 && pGrade.Trim().Length == 0)
                {
                    payment.Type = "Frt";
                    payment.Particulars = "DebitImported-Frieght";
                }
                else
                {
                    payment.Type = "Sale";
                    payment.Particulars = "DebitImported";
                }

                UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.ToLower().Replace(" ", "").Trim().Equals(clientName.ToLower().Replace(" ", "").Trim())));
                if (client == null)
                {
                    client = new UserAccount();
                    client.ClientName = clientName;
                    client.ID = ++Datastore.dataFile.UserAccountIDManager;
                    /*foreach (UserAccount ua in Datastore.dataFile.UserAccounts)
                        if (ua.ID > client.ID)
                            client.ID = ua.ID;
                    client.ID++;*/
                    Datastore.dataFile.UserAccounts.Add(client);
                    Action myAction = () =>
                    {
                        EditUserAccount eua = new EditUserAccount(client.ID.ToString());
                        if (eua.ShowDialog(this) != DialogResult.OK)
                        {
                            
                        }
                    };
                    Invoke(myAction);
                    client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == client.ID));
                    /*client.CutOffDays = 20;
                    client.FooText = "About client";
                    client.InterestRate1 = 20;
                    client.InterestRate2 = 24;
                    client.LessDays = 10;
                    client.OBType = "Debit";
                    client.OpeningBalance = 0;*/
                    payment.ID = ++client.PaymentIDManager;
                    client.Payments = new List<Payment>();                    
                    client.Payments.Add(payment);
                    
                }
                else 
                {
                    payment.ID = ++client.PaymentIDManager;
                    /*foreach (Payment pay in client.Payments)
                        if (pay.ID > payment.ID)
                            payment.ID = pay.ID;
                    payment.ID++;*/
                    client.Payments.Add(payment);
                }
                if (isDebitAdvice)
                    debitAdvicePayments.Add(new PaymentDetail(payment,client.ID));
                if (isDebitNote)
                    debitNotePayments.Add(new PaymentDetail(payment, client.ID));

                Grade grade = Datastore.dataFile.Grades.Find(x => (x.GradeName.ToLower().Trim().Replace(" ", "").Equals(payment.Grade.ToLower().Replace(" ", "").Trim())));
                if (grade == null)
                {
                    EditGrade editGrade = new EditGrade(payment.Grade);
                    editGrade.ShowDialog(this);
                    /*grade = new Grade();
                    grade.GradeName = payment.Grade;
                    grade.Amount = 100;
                    Datastore.dataFile.Grades.Add(grade);*/
                }
                Record.RecordNow(client, payment);

                System.GC.Collect();
            }


            if (debitNotePayments.Count > 0)
            {
                DebitNotePrinter debitNotePrinter = new DebitNotePrinter(debitNotePayments);
                debitNotePrinter.ListOfDNotes += debitNotePrinter_ListOfDNotes;
                debitNotePrinter.ShowDialog(this);
            }

            if (debitAdvicePayments.Count > 0)
            {
                DebitAdvisePrinter debitAdvisePrinter = new DebitAdvisePrinter(debitAdvicePayments, listOfDNotes);
                debitAdvisePrinter.ShowDialog(this);
            }

            
            Datastore.dataFile.Save();

            DialogResult = DialogResult.OK;
            Close();
        }

        void debitNotePrinter_ListOfDNotes(List<Decimal> listOfDNumbers)
        {
            listOfDNotes = listOfDNumbers.ToList<Decimal>();
        }

        private void dv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                if (dv.CurrentCell.ColumnIndex == 1)
                {
                    TextBox tb = (TextBox)e.Control;
                    AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                    foreach (UserAccount client in Datastore.dataFile.UserAccounts)
                    {
                        auto.Add(client.ClientName);
                    }
                    tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    tb.AutoCompleteCustomSource = auto;
                }
                else if (dv.CurrentCell.ColumnIndex == 4)
                {
                    TextBox tb = (TextBox)e.Control;
                    AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                    foreach (Grade grade in Datastore.dataFile.Grades)
                    {
                        auto.Add(grade.GradeName);
                    }
                    tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    tb.AutoCompleteCustomSource = auto;
                }
            }
        }

        private void dv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == 1)
            {
                /*bool isAllChecked = true;
                foreach (DataGridViewRow row in dv.Rows)
                {
                    isAllChecked = isAllChecked && (bool)row.Cells[e.ColumnIndex].Value;
                }
                foreach (DataGridViewRow row in dv.Rows)
                {
                    row.Cells[e.ColumnIndex].Value = !isAllChecked;
                }*/
            }
        }

        
    }

    public class PaymentDetail
    {
        public Payment payment;
        public Decimal clientId;

        public PaymentDetail(Payment payment, Decimal clientId)
        {
            this.payment = payment;
            this.clientId = clientId;
        }
    }
}
