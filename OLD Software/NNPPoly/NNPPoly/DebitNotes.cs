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
    public partial class DebitNotes : Form
    {
        private ListViewColumnSorter _cSorter;

        public DebitNotes()
        {
            InitializeComponent();
        }

        private void DebitNotes_Load(object sender, EventArgs e)
        {
            loadClients();
            cmbType.SelectedIndex = 0;
            _cSorter = new ListViewColumnSorter();
            lv.ListViewItemSorter = _cSorter;
            _cSorter.ColumnType = ColumnDataType.String;
            _cSorter.Order = SortOrder.Ascending;
            _cSorter.SortColumn = 0;
            lv.SetSortIcon(0, SortOrder.Descending);
            lv.Sort();
            lv.ColumnClick += __SortOnClick_ListView_onColumnClick;
        }

        private void loadClients()
        {
            cmbClients.Items.Clear();
            foreach (UserAccount account in Datastore.dataFile.UserAccounts)
            {
                cmbClients.Items.Add(new CMBItem(account.ClientName, account.ID));
            }
            cmbClients.Text = "";
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            lv.Items.Clear();
            List<DebitNotePrint> debits=null;
            if (cmbType.SelectedItem.ToString().Equals("Debit notes"))
                debits = Datastore.dataFile.DebitNotes;
            else if (cmbType.SelectedItem.ToString().Equals("Debit advises"))
                debits = Datastore.dataFile.DebitAdvises;

            foreach (DebitNotePrint dnote in debits)
            {
                if ((dnote.debitDate.Day == dtDateDate.Value.Day && dnote.debitDate.Month == dtDateDate.Value.Month && dnote.debitDate.Year == dtDateDate.Value.Year) || chkALL.Checked)
                {
                    String findName = cmbClients.SelectedIndex > -1 ? cmbClients.SelectedItem.ToString() : "";
                    if ((findName.Trim().Length > 0 && dnote.clientID == (Decimal)(cmbClients.SelectedItem as CMBItem).Value) || findName.Length == 0)
                    {
                        ListViewItem li = new ListViewItem(new String[] { dnote.toClientName, dnote.debitNoteNo.ToString(), dnote.debitDate.ToString(Program.SystemSDFormat), dnote.debitNoteRows.Count + "", dnote.totalAmount.ToString("0.00") });
                        li.Tag = dnote;
                        lv.Items.Add(li);
                    }
                }
            }
            lv.Sort();
        }

        private void __SortOnClick_ListView_onColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;
                ListViewColumnSorter cSorter = (ListViewColumnSorter)lv.ListViewItemSorter;

                switch (e.Column)
                {
                    case 1:
                    case 3:
                    case 4:
                        cSorter.ColumnType = ColumnDataType.Number;
                        break;
                    case 2:
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

        private void lv_MouseClick(object sender, MouseEventArgs e)
        {
            if (lv.SelectedItems.Count > 0 && e.Button == System.Windows.Forms.MouseButtons.Right) 
            {
                ContextMenu cm = new ContextMenu();
                if (lv.SelectedItems.Count == 1)
                    cm.MenuItems.Add(new MenuItem("&Show entries (Edit Mode)", ListView_RightClickAction));
                cm.MenuItems.Add(new MenuItem("&Print directly", ListView_RightClickAction));
                if (lv.SelectedItems.Count >= 1)
                {
                    cm.MenuItems.Add("-");
                    cm.MenuItems.Add(new MenuItem("&Delete (Entry+DNote)", ListView_RightClickAction));
                }
                cm.Show(sender as Control, e.Location);
            }
        }

        private void ListView_RightClickAction(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count > 0) 
            {
                MenuItem mi = sender as MenuItem;
                if (mi.Text.StartsWith("&Show entries") && lv.SelectedItems.Count == 1)
                {
                    new DebitNoteEntries(lv.SelectedItems[0].Tag as DebitNotePrint).ShowDialog(this);
                    btnShow_Click(sender, e);
                }
                else if (mi.Text.StartsWith("&Print directly"))
                {
                    List<DebitNotePrint> dnotes = new List<DebitNotePrint>();
                    foreach (ListViewItem li in lv.SelectedItems)
                    {
                        dnotes.Add(li.Tag as DebitNotePrint);
                    }
                    if (cmbType.SelectedItem.ToString().Equals("Debit notes"))
                        new DebitNotePrinter(dnotes).ShowDialog(this);
                    if (cmbType.SelectedItem.ToString().Equals("Debit advises"))
                        new DebitAdvisePrinter(dnotes).ShowDialog(this);
                }
                else if (mi.Text.StartsWith("&Delete"))
                {
                    if (MessageBox.Show(this, "Are you sure to delete all selected debit notes/advises and its entries from system ?", "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                    foreach (ListViewItem li in lv.SelectedItems)
                    {
                        DebitNotePrint dnp = li.Tag as DebitNotePrint;
                        if (cmbType.SelectedItem.ToString().Equals("Debit notes"))
                        {
                            UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == dnp.clientID));
                            if (client != null)
                            {
                                int deletedCount = client.Payments.RemoveAll(x => {
                                    bool retFlag = x.ID == (Decimal)dnp.ID;
                                    retFlag = retFlag && x.Date.CompareTo(dnp.debitDate) == 0;
                                    retFlag = retFlag && x.DocChqNo.Trim().ToLower().Equals(("DN No." + dnp.debitNoteNo).Trim().ToLower());
                                    return retFlag;
                                });
                                Console.WriteLine("DeletedCount::" + deletedCount);
                                foreach (DebitNoteRow row in dnp.debitNoteRows)
                                {
                                    deletedCount += client.Payments.RemoveAll(x => (x.ID == (Decimal)row.ID));
                                }
                                Console.WriteLine("Total payment removed:"+deletedCount--);
                            }
                            Datastore.dataFile.DebitNotes.Remove(dnp);
                        }
                        else
                        {
                            UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == dnp.clientID));
                            if (client != null)
                            {
                                int deletedCount = 0;//client.Payments.RemoveAll(x => (x.ID == (Decimal)dnp.ID && x.Date.CompareTo(dnp.debitDate) == 0 && x.DocChqNo.Trim().ToLower().Equals(dnp.debitNoteNo.ToString())));
                                foreach (DebitNoteRow row in dnp.debitNoteRows)
                                {
                                    deletedCount += client.Payments.RemoveAll(x => (x.ID == (Decimal)row.ID));
                                }
                                Console.WriteLine("Total payment removed:" + deletedCount--);
                            }
                            Datastore.dataFile.DebitAdvises.Remove(dnp);
                        }
                    }
                    Datastore.dataFile.Save();
                    MessageBox.Show(this, "All selected debit notes/advises deleted successfully.", "Delete Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnShow_Click(sender, e);
                }
            }
        }

        private void lv_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lv.SelectedItems.Count == 1)
            {
                ListView_RightClickAction(new MenuItem("&Show entries"), new EventArgs());

            }
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
