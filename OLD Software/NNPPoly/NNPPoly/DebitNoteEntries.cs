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
    public partial class DebitNoteEntries : Form
    {
        private double cutOffDays=20;
        private DebitNotePrint dnote;
        private UserAccount backupOfClient = null;

        public DebitNoteEntries(DebitNotePrint dnote)
        {
            InitializeComponent();
            this.dnote = dnote;
        }

        private void DebitNoteEntries_Load(object sender, EventArgs e)
        {
            loadRows();
        }

        private void loadRows() {
            UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == dnote.clientID));
            if(client!=null) {
                cutOffDays=client.CutOffDays;
            }
            if (backupOfClient == null)
            {
                backupOfClient = Datastore.current;
                Datastore.current = client;
            }
            lv.Items.Clear();
            foreach (DebitNoteRow row in dnote.debitNoteRows)
            {
                Payment payment = null;
                if (client != null)
                {
                    payment = client.Payments.Find(x => (x.ID == (Decimal)row.ID));
                    if (payment != null)
                    {
                        ListViewItem li = new ListViewItem(new String[] { payment.DocChqNo, payment.Date.ToString(Program.SystemSDFormat), payment.Grade, payment.MT, row.Amount.ToString("0.00") });
                        li.Tag = payment;
                        lv.Items.Add(li);
                    }
                    else
                    {
                        ListViewItem li = new ListViewItem(new String[] { "--", "--", "--", "--", row.Amount.ToString("0.00") });
                        li.Tag = null;
                        li.UseItemStyleForSubItems = true;
                        li.BackColor = Color.IndianRed;
                        lv.Items.Add(li);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lv_DoubleClick(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 1)
            {
                String invoiceNo=lv.SelectedItems[0].SubItems[0].Text.Trim().ToLower();
                String date=lv.SelectedItems[0].SubItems[1].Text.Trim().ToLower();
                String grade=lv.SelectedItems[0].SubItems[2].Text.Trim().ToLower();
                String qty=lv.SelectedItems[0].SubItems[3].Text.Trim().ToLower();

                Payment actPayment = lv.SelectedItems[0].Tag as Payment;
                if (actPayment == null)
                {
                    MessageBox.Show(this, "Sorry, this entry was deleted from client panel.", "Payment not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    NewPayment np = new NewPayment();
                    np.setEditMode(actPayment.ID.ToString());
                    if (np.ShowDialog(this) == DialogResult.OK)
                    {
                        DebitNotePrint _dnote = null;
                        bool isDebitNote = false;
                        if (dnote.ID == null)
                        {
                            isDebitNote = false;
                            _dnote = Datastore.dataFile.DebitAdvises.Find(x => (x == dnote));
                        }
                        else
                        {
                            _dnote = Datastore.dataFile.DebitNotes.Find(x => ((Decimal)x.ID == (Decimal)dnote.ID));
                            isDebitNote = true;
                        }
                        
                        if (_dnote != null)
                        {
                            //DebitNoteRow dr = _dnote.debitNoteRows.Find(x => (x.InvoiceNo.Trim().ToLower().Equals(invoiceNo) && x.Date.ToString(Program.SystemSDFormat).Trim().ToLower().Equals(date) && x.Qty.Trim().ToLower().Equals(qty)));
                            DebitNoteRow dr = _dnote.debitNoteRows.Find(x => ((Decimal)x.ID == actPayment.ID));
                            /*dr.InvoiceNo = actPayment.DocChqNo;
                            dr.Qty = actPayment.MT;
                            dr.Grade = actPayment.Grade;*/
                            double mtVal = 0;
                            double.TryParse(actPayment.MT, out mtVal);
                            Grade _grade = Datastore.dataFile.Grades.Find(x => (x.GradeName.Trim().ToLower().Equals(actPayment.Grade.ToLower().Trim())));
                            double gradeAmt = 100;
                            if (_grade != null)
                                gradeAmt = _grade.Amount;

                            if (isDebitNote)
                            {
                                dr.Amount = gradeAmt * cutOffDays * mtVal;
                            }
                            else
                            {
                                dr.Amount = actPayment.Debit;
                            }

                            _dnote.totalAmount = 0;
                            foreach (DebitNoteRow row in _dnote.debitNoteRows)
                            {
                                _dnote.totalAmount += row.Amount;
                            }

                            Datastore.dataFile.Save();
                            loadRows();
                        }
                        else
                        {
                            MessageBox.Show("Debit Note not found");
                        }
                    }
                }
                
            }
        }

        private void DebitNoteEntries_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backupOfClient != null)
            {
                Datastore.current = backupOfClient;
            }
        }
    }
}
