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
    public partial class Records : Form
    {
        private ListViewColumnSorter cSorter = null;
        public Records()
        {
            InitializeComponent();
        }

        private void Records_Load(object sender, EventArgs e)
        {
            cmbEntries.SelectedIndex = 0;
            foreach (UserAccount client in Datastore.dataFile.UserAccounts)
            {
                CMBItem item = new CMBItem(client.ClientName, client.ID.ToString());
                cmbClients.Items.Add(item);
            }
            dtRecordDate.Focus();
        }

        private void __SortOnClick_ListView_onColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;

                switch (e.Column)
                {
                    case 5:
                    case 7:
                        cSorter.ColumnType = ColumnDataType.Number;
                        break;
                    case 3:
                        cSorter.ColumnType = ColumnDataType.DateTime;
                        break;
                    default:
                        cSorter.ColumnType = ColumnDataType.String;
                        break;
                }

                if (e.Column == cSorter.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (cSorter.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        lv.SetSortIcon(e.Column, SortOrder.Ascending);
                        cSorter.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        lv.SetSortIcon(e.Column, SortOrder.Descending);
                        cSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    cSorter.SortColumn = e.Column;
                    cSorter.Order = System.Windows.Forms.SortOrder.Ascending;
                    lv.SetSortIcon(e.Column, SortOrder.Descending);
                }

                // Perform the sort with these new sort options.
                lv.Sort();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform columnsort_records operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            lv.Items.Clear();
            DateTime dtValue = new DateTime(dtRecordDate.Value.Year, dtRecordDate.Value.Month, dtRecordDate.Value.Day, 12, 0, 0);
            Decimal selectedClientID = -1;
            bool isCreditOnly = false;
            if (cmbClients.SelectedIndex > -1)
            {
                selectedClientID=Decimal.Parse((cmbClients.SelectedItem as CMBItem).Value.ToString());
            }
            if (cmbEntries.SelectedIndex == 0)
                isCreditOnly = false;
            else if (cmbEntries.SelectedIndex == 1)
                isCreditOnly = true;

            List<Record> records = Datastore.dataFile.Records.FindAll(x => {
                if (x.payment != null)
                {
                    bool isExists = false;
                    UserAccount client = Datastore.dataFile.UserAccounts.Find(x1 => (x1.ID == x.ClientID));
                    if (client != null)
                    {
                        isExists = client.Payments.Find(x1 => (x1.Compare(x.payment,false))) != null;//client.Payments.Find(x1 => (x1.ID == x.payment.ID)) != null;
                    }
                    if (!isExists && !chkShowDeleted.Checked) return false;
                    bool clientMatch = (selectedClientID == -1 || (selectedClientID != -1 && x.ClientID == selectedClientID));
                    bool dateMatch = (x.payment.Date.Year == dtValue.Year && x.payment.Date.Month == dtValue.Month && x.payment.Date.Day == dtValue.Day) || chkALL.Checked;//x.payment.Date.CompareTo(dtValue) == 0 || chkALL.Checked;
                    if (isCreditOnly)
                        return (dateMatch && (x.payment.Debit == 0 && x.payment.Credit > 0) && clientMatch);
                    else if (!isCreditOnly)
                        return (dateMatch && (x.payment.Debit > 0 && x.payment.Credit == 0) && clientMatch);
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            });

            if (records != null)
            {
                double totalAmount = 0, totalMT = 0;

                foreach (Record record in records)
                {
                    if (record.payment == null) continue;



                    UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == record.ClientID));

                    String clientName = record.ClientName;
                    if (client != null)
                    {
                        clientName = client.ClientName;
                        ListViewItem li = new ListViewItem(new String[] { clientName, record.payment.Type, record.payment.DocChqNo, record.payment.Date.ToString(Program.SystemSDFormat), record.payment.MT, record.payment.Grade, record.payment.Credit == 0 ? record.payment.Debit.ToString() : record.payment.Credit.ToString(), record.payment.ID.ToString() });
                        li.Tag = record.ID;
                        lv.Items.Add(li);
                        if (record.payment.Credit == 0)
                        {
                            totalAmount += record.payment.Debit;
                        }
                        else
                        {
                            totalAmount += record.payment.Credit;
                        }
                        double tempMt = 0;
                        if (double.TryParse(record.payment.MT, out tempMt))
                            totalMT += tempMt;
                    }
                }
                lblTotalAmount.Text = "Total Amount: " + totalAmount.ToString("0.00") + " , Total MT: " + totalMT.ToString("0.00");
            }

            for (int i = 0; i < lv.Columns.Count; i++)
                if (i == 0 || i==1 || i==3 || i==5 || i==7)
                    lv.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

            cSorter = new ListViewColumnSorter();
            lv.ListViewItemSorter = cSorter;
            lv.ColumnClick += __SortOnClick_ListView_onColumnClick;
            cSorter.ColumnType = ColumnDataType.String;
            cSorter.Order = SortOrder.Ascending;
            cSorter.SortColumn = 0;
            lv.Sort();
            lv.SetSortIcon(0, SortOrder.Descending);
        }

        private void lv_DoubleClick(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 1)
            {
                Record record = Datastore.dataFile.Records.Find(x => (x.ID == (Decimal)lv.SelectedItems[0].Tag));
                if (record != null)
                {
                    UserAccount client = Datastore.dataFile.UserAccounts.Find(x=>(x.ID==record.ClientID));
                    if (client != null)
                    {
                        Payment payment = client.Payments.Find(x=>(x.ID==record.payment.ID));
                        if (payment != null)
                        {
                            UserAccount tempClient = Datastore.current;
                            Datastore.current = client;
                            NewPayment editPayment = new NewPayment();
                            editPayment.setEditMode(payment.ID.ToString());
                            editPayment.ShowDialog(this);
                            Datastore.current = tempClient;
                            payment = client.Payments.Find(x => (x.ID == record.payment.ID));
                            if (payment != null)
                                record.payment = payment;
                            Datastore.dataFile.Save();
                            btnShow_Click(sender, e);
                        }
                        else
                        {
                            DialogResult dr=MessageBox.Show(this, "Sorry, this entry is deleted from client database, are you need to restore this entry ?", "Deleted entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.Yes)
                            {
                                client.Payments.Add(record.payment);
                                UserAccount tempClient = Datastore.current;
                                Datastore.current = client;
                                NewPayment editPayment = new NewPayment();
                                editPayment.setEditMode(record.payment.ID.ToString());
                                editPayment.ShowDialog(this);
                                Datastore.current = tempClient;
                                payment = client.Payments.Find(x => (x.ID == record.payment.ID));
                                if (payment != null)
                                    record.payment = payment;
                                Datastore.dataFile.Save();
                                btnShow_Click(sender, e);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(this,"Sorry, this entry isn't belong to any client.","Client not found",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show(this, "Record not found.", "No record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void chkShowDeleted_CheckedChanged(object sender, EventArgs e)
        {
            btnShow_Click(sender, e);
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lv.SelectedItems.Count==1)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("&Edit entry",lv_RightClickAction));
                cm.MenuItems.Add(new MenuItem("&Delete entry", lv_RightClickAction));
                cm.Show(sender as Control, e.Location);
            }
        }

        private void lv_RightClickAction(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi.Text.StartsWith("&Edit entry"))
            {
                lv_DoubleClick(sender, e);
            }
            else if (mi.Text.StartsWith("&Delete entry"))
            {
                if (lv.SelectedItems.Count == 1)
                {
                    Record record = Datastore.dataFile.Records.Find(x => (x.ID == (Decimal)lv.SelectedItems[0].Tag));
                    if (record != null)
                    {
                        UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == record.ClientID));
                        if (client != null)
                        {
                            Payment payment = client.Payments.Find(x => (x.ID == record.payment.ID));
                            if (payment != null)
                            {
                                DialogResult dr = MessageBox.Show(this, "Are you sure to delete selected entry ?", "Delete Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (dr == DialogResult.Yes)
                                {
                                    client.Payments.Remove(payment);
                                    if (Datastore.dataFile.Save())
                                    {
                                        MessageBox.Show(this, "Successfully deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        btnShow_Click(sender, e);
                                    }
                                    else
                                    {
                                        MessageBox.Show(this, "Unable to delete selected entry, please try again.", "Can't Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show(this, "Sorry, selected record entry is already deleted from client ledger.", "Already deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            MessageBox.Show(this, "Sorry, this entry isn't belong to any client.", "Client not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Record not found.", "No record", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
