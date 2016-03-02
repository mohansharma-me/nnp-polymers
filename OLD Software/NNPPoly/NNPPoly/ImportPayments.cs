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
    public partial class ImportPayments : Form
    {
        public List<UserAccount> clients;
        public ImportPayments(List<UserAccount> clients)
        {
            InitializeComponent();
            this.clients = clients;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dv.Rows)
            {
                if (row.IsNewRow) continue;
                String paymentDate = (String)row.Cells[0].Value;
                String chequeNo = (String)row.Cells[1].Value;
                String clientName = (String)row.Cells[2].Value;
                String amount = (String)row.Cells[3].Value;

                DateTime _Date=DateTime.Now;
                if (paymentDate == null || (paymentDate != null && !DateTime.TryParse(paymentDate, out _Date)))
                {
                    MessageBox.Show(this,"Please enter valid payment date.","No valid payment date",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[0];
                    return;
                }
                if (clientName == null || (clientName != null && clientName.Trim().Length == 0))
                {
                    MessageBox.Show(this,"Please enter valid client name.","No valid client name",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[2];
                    return;
                }
                double paymentAmount=0;
                if (amount == null || (amount != null && !double.TryParse(amount, out paymentAmount)))
                {
                    MessageBox.Show(this,"Please enter valid payment amount","No valid payment amount",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    dv.CurrentCell = row.Cells[3];
                    return;
                }
                Payment payment = new Payment();
                payment.Credit = paymentAmount;
                payment.Date = _Date;
                payment.Debit = 0;
                payment.DocChqNo = chequeNo;
                payment.Grade = "";
                payment.MT = "";
                payment.Particulars = "ImportedPayments";
                payment.Remain = payment.Credit;
                payment.Type = "BRct";
                payment.ID = 0;

                UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.ToLower().Replace(" ", "").Trim().Equals(clientName.ToLower().Replace(" ", "").Trim())));
                if (client == null)
                {
                    client = new UserAccount();
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
                    client.Payments = new List<Payment>();
                    payment.ID = ++client.PaymentIDManager;
                    client.Payments.Add(payment);

                    //Datastore.dataFile.UserAccounts.Add(client);
                }
                else
                {
                    /*foreach (Payment _payment in client.Payments)
                        if (_payment.ID > payment.ID)
                            payment.ID = _payment.ID;
                    payment.ID++;*/
                    payment.ID = ++client.PaymentIDManager;
                    client.Payments.Add(payment);
                }
                Record.RecordNow(client, payment);
            }
            Datastore.dataFile.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ImportPayments_Load(object sender, EventArgs e)
        {
            if (clients == null) clients = new List<UserAccount>();
            foreach (UserAccount client in clients)
            {
                foreach (Payment payment in client.Payments)
                {
                    int index=dv.Rows.Add(payment.Date.ToString(Program.SystemSDFormat), payment.DocChqNo, client.ClientName, payment.Credit.ToString());
                    UserAccount ua = Datastore.dataFile.UserAccounts.Find(x => (x.ClientName.ToLower().Replace(" ", "").Trim().Equals(client.ClientName.ToLower().Replace(" ", "").Trim())));
                    if (ua == null)
                    {
                        dv.Rows[index].DefaultCellStyle.Font = new Font(dv.DefaultCellStyle.Font, FontStyle.Underline);
                    }
                }
            }
        }

        private void dv_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                if (dv.CurrentCell.ColumnIndex == 2)
                {
                    TextBox tb = e.Control as TextBox;
                    AutoCompleteStringCollection auto = new AutoCompleteStringCollection();
                    foreach (UserAccount ua in Datastore.dataFile.UserAccounts)
                        auto.Add(ua.ClientName);
                    tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                    tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    tb.AutoCompleteCustomSource = auto;
                }
            }
        }
    }
}
