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
    public partial class DeleteConflicPayments : Form
    {
        public int removeCounter = 0;
        public List<Payment> payments;
        public DeleteConflicPayments(List<Payment> pays)
        {
            InitializeComponent();
            payments = pays;
        }

        private void DeleteConflicPayments_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Payment pay in payments)
                {
                    ListViewItem li = new ListViewItem(new String[] { pay.ID.ToString(), pay.DocChqNo, pay.Type, pay.Particulars, pay.MT, pay.Credit.ToString(), pay.Debit.ToString() });
                    li.Tag = pay.ID;
                    lv.Items.Add(li);
                }

                foreach (ColumnHeader ch in lv.Columns)
                {
                    if (ch.Index != 4)
                        ch.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    else
                        ch.AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to load conflict payments.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform cancel_close operation on conflict payments.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try {
                if (lv.CheckedItems.Count == lv.Items.Count)
                {
                    if (MessageBox.Show(this, "You have selected all the conflicts payments and if you proceed to remove these you will have also lost your particular payment which may have correct data." + Environment.NewLine + "Are you sure to proceed to next step ?", "Remove all !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                }
                else if (lv.CheckedItems.Count == 0)
                {
                    if (MessageBox.Show(this, "You have selected nothing from conflicts payments and if you proceed to next step even the payment which have wrong data resides in the payment list." + Environment.NewLine + "Are you sure to proceed to next step ?", "Remove nothing !!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        DialogResult = DialogResult.No;
                        Close();
                    }
                    else
                    {
                        return;
                    }
                }

                foreach (ListViewItem li in lv.CheckedItems)
                {
                    Decimal pid = (Decimal)li.Tag;
                    removeCounter += Datastore.current.Payments.RemoveAll(x => (x.ID == pid));
                }
                DialogResult = DialogResult.Yes;
                Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform delete_conflicpayment operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
