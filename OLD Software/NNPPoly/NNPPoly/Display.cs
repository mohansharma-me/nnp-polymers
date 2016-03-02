using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace NNPPoly
{
    public partial class Display : Form
    {
        private Main main;
        private ListViewColumnSorter cSorter=null;
        public Display(Main main)
        {
            InitializeComponent();
            this.main = main;
        }

        public void searchPayments(String search,bool strictSQ)
        {
            try
            {
                String sq = search.Trim().ToLower();
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    ListViewItem li = lv.Items[i];
                    li.UseItemStyleForSubItems = false;
                    if (sq.Length == 0)
                    {
                        li.SubItems[1].BackColor = Color.White;
                        li.SubItems[1].ForeColor = Color.Black;
                    }
                    else
                    {
                        String comb = "";
                        foreach (ListViewItem.ListViewSubItem sli in li.SubItems)
                            comb += sli.Text.Trim().ToLower() + " ";
                        bool condition = false;
                        if (strictSQ)
                        {
                            condition = comb.IndexOf(" " + sq + " ") > -1;
                            if (!condition)
                                condition = comb.IndexOf(" " + sq) > -1;
                        }
                        else
                        {
                            condition = comb.IndexOf(sq) > -1;
                        }
                        if (condition)//comb.IndexOf(searchTerm) > -1)
                        {
                            li.SubItems[1].BackColor = Color.GreenYellow;
                            li.SubItems[1].ForeColor = Color.White;
                            var item = lv.Items[li.Index];
                            lv.Items.RemoveAt(li.Index);
                            lv.Items.Insert(0, item);
                        }
                        else
                        {
                            li.SubItems[1].BackColor = Color.White;
                            li.SubItems[1].ForeColor = Color.Black;
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform search_payment operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadPayments()
        {
            try 
            {
                #region loadPaymentsCode
                if (Datastore.current.Payments.Count == 0) return;
                lv.Visible = false;
                pb.Visible = true;
                pb.Value = 0;
                if (Datastore.current.Payments.Count != 0)
                    pb.Maximum = Datastore.current.Payments.Count+1;
                if (Datastore.current != null)
                {
                    lv.Items.Clear();
                    double lastAmt = 0;

                    Datastore.current.Payments.Sort(new Comparison<Payment>(paymentSorter));
                    Payment obp = new Payment();
                    obp.Date = new DateTime(Datastore.current.Payments[0].Date.Year, 4, 1, 12, 0, 0);//"01-04-" + Datastore.current.Payments[0].Date.Year;
                    if (Datastore.current.OBType.ToLower().Contains("debit"))
                        obp.Debit = Datastore.current.OpeningBalance;
                    else
                        obp.Credit = Datastore.current.OpeningBalance;
                    obp.DocChqNo = "Opening balance";
                    obp.MT = "0";
                    obp.Particulars = "O.B.";
                    obp.Remain = 0;
                    obp.Type = "Sale";
                    List<Payment> payments = Datastore.current.Payments.ToList<Payment>();
                    payments.Insert(0, obp);

                    foreach (Payment pay in payments)
                    {
                        if (pay.Credit == 0)
                        {
                            lastAmt -= pay.Debit;
                        }
                        else if (pay.Debit == 0)
                        {
                            lastAmt += pay.Credit;
                        }

                        if (Main.filterPeriod)
                        {
                            DateTime dtP = new DateTime(pay.Date.Year, pay.Date.Month, pay.Date.Day, 12, 0, 0);
                            DateTime dtF = new DateTime(Main.ffromDate.Year, Main.ffromDate.Month, Main.ffromDate.Day, 12, 0, 0);
                            DateTime dtT = new DateTime(Main.ftoDate.Year, Main.ftoDate.Month, Main.ftoDate.Day, 12, 0, 0);
                            int start = (int)dtF.Subtract(dtP).TotalDays;
                            int to = (int)dtT.Subtract(dtP).TotalDays;

                            if (start > 0 || to < 0)
                                continue;
                        }

                        ListViewItem li = new ListViewItem(new String[] { pay.ID.ToString(), pay.Date.ToString(Program.SystemSDFormat), pay.DocChqNo, pay.Type, pay.Particulars, pay.MT, pay.Grade.Trim().Length == 0 ? "Default" : pay.Grade, pay.Credit.ToString(), pay.Debit.ToString(), lastAmt.ToString("0")+".00" });
                        li.UseItemStyleForSubItems = false;
                        if (lastAmt < 0)
                        {
                            li.SubItems[9].BackColor = Color.OrangeRed;
                        }
                        else if (lastAmt == 0)
                        {
                            li.SubItems[9].BackColor = Color.White;
                        }
                        else if(lastAmt>0)
                        {
                            li.SubItems[9].BackColor = Color.LawnGreen;
                        }
                        li.Tag = pay;
                        lv.Items.Add(li);
                        pb.Value++;
                    }
                    lv.Columns[0].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lv.Columns[1].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                    lv.Columns[2].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    lv.Columns[3].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    lv.Columns[4].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);

                    for (int i = 5; i <= 8; i++)
                    {
                        lv.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                        int wid = lv.Columns[i].Width;
                        lv.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (lv.Columns[i].Width < wid)
                            lv.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                    }

                    lv.Columns[4].Width = 200;
                
                    cSorter = new ListViewColumnSorter();
                    lv.ListViewItemSorter = cSorter;
                    cSorter.ColumnType = ColumnDataType.DateTime;
                    cSorter.Order = SortOrder.Ascending;
                    cSorter.SortColumn = 1;
                    lv.Sort();
                    lv.SetSortIcon(1, SortOrder.Descending);
                }
                pb.Visible = false;
                lv.Visible = true;
                #endregion

            }
            catch (Exception excep)
            {
                String err = "Unable to perform load_payments operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                #region duplicatePaymentFinder
                bool removedAny = false;
                foreach (ListViewItem li in lv.Items)
                {
                    //exact same payments
                    Payment pay = li.Tag as Payment;
                    Payment found = Datastore.current.Payments.Find(x =>
                    {
                        return x.Credit == pay.Credit && x.Date.Equals(pay.Date) && x.Debit == pay.Debit && x.DocChqNo.Equals(pay.DocChqNo) && x.Remain == pay.Remain && x.Type.Equals(pay.Type) && x.ID != pay.ID;
                    });
                    if (found != null)
                    {
                        if (found.ID > pay.ID)
                        {
                            removedAny = true;
                        }
                        else if (found.ID < pay.ID)
                        {
                            removedAny = true;
                        }
                    }

                    if (removedAny) { break; }
                    List<Payment> founds = Datastore.current.Payments.FindAll(x =>
                    {
                        return x.Date.Equals(pay.Date) && x.DocChqNo.Equals(pay.DocChqNo) && x.Type.Equals(pay.Type) && x.ID != pay.ID;
                    });

                    if (founds != null && founds.Count > 0)
                    {
                        removedAny = true;
                        break;
                    }
                }

                if (removedAny)
                {
                    main.setConflicChecker();
                }
                else
                {
                    main.clearConflicChecker();
                }
                #endregion

            }
            catch (Exception excep)
            {
                String err = "Unable to perform conflict_enabler operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void conflicCheck()
        {
            try
            {
                #region duplicatePaymentFinder
                bool removedAny = false;
                double removeCounter = 0;
                foreach (ListViewItem li in lv.Items)
                {
                    //exact same payments
                    Payment pay = li.Tag as Payment;
                    /*Payment found = Datastore.current.Payments.Find(x =>
                    {
                        return x.Credit == pay.Credit && x.Date.Equals(pay.Date) && x.Debit == pay.Debit && x.DocChqNo.Equals(pay.DocChqNo) && x.Remain == pay.Remain && x.Type.Equals(pay.Type) && x.ID != pay.ID;
                    });
                    if (found != null)
                    {
                        if (found.ID > pay.ID)
                        {
                            Datastore.current.Payments.Remove(found);
                            removeCounter++;
                            removedAny = true;
                        }
                        else if (found.ID < pay.ID)
                        {
                            Datastore.current.Payments.Remove(pay);
                            removeCounter++;
                            removedAny = true;
                        }
                    }

                    if (removedAny) { continue; }*/
                    List<Payment> founds = Datastore.current.Payments.FindAll(x =>
                    {
                        bool cmpDate = x.Date.ToString(Program.SystemSDFormat).Equals(pay.Date.ToString(Program.SystemSDFormat));
                        bool cmpDoc = x.DocChqNo.Equals(pay.DocChqNo);
                        bool cmpType = x.Type.Equals(pay.Type);
                        bool cmpID=x.ID != pay.ID;
                        return 
                            (cmpDate && cmpDoc && cmpType && cmpID) 
                            ||
                            (x.Credit == pay.Credit && cmpDate && x.Debit == pay.Debit && cmpDoc && x.Remain == pay.Remain && cmpType && cmpID);
                    });

                    if (founds != null && founds.Count > 0)
                    {
                        founds.Add(pay);
                        DeleteConflicPayments dcp = new DeleteConflicPayments(founds);
                        if (dcp.ShowDialog(this) == DialogResult.Yes)
                        {
                            removeCounter += dcp.removeCounter;
                            if (removeCounter > 0)
                                removedAny = true;
                        }
                    }
                }

                if (removedAny)
                {
                    MessageBox.Show(this, "Total " + removeCounter + " payment conflicts auto filtered.", "Duplication remover", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    loadPayments();
                }
                #endregion

            }
            catch (Exception excep)
            {
                String err = "Unable to perform conflicCheck operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static int paymentSorter(Payment p1,Payment p2)
        {
            try
            {
                DateTime d1 = new DateTime(p1.Date.Year, p1.Date.Month, p1.Date.Day, 12, 0, 0);
                DateTime d2 = new DateTime(p2.Date.Year, p2.Date.Month, p2.Date.Day, 12, 0, 0);
                int c = d1.CompareTo(d2);
                if (c == 0)
                {
                    c = p1.ID.CompareTo(p2.ID);
                }
                return c;

            }
            catch (Exception excep)
            {
                String err = "Unable to perform payment_sorter operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
        }

        private void lv_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lv.SelectedItems.Count > 0 && e.Button == MouseButtons.Right)
                {
                    ContextMenu cm = new ContextMenu();
                    if (lv.SelectedItems.Count == 1)
                        cm.MenuItems.Add(new MenuItem("&Edit selected payment", lv_RightClickAction));
                    cm.MenuItems.Add(new MenuItem("&Delete selected payments", lv_RightClickAction));

                    MenuItem hmenu = new MenuItem("Highlights >>");
                    hmenu.MenuItems.Add(new MenuItem("&Add to Highlights", lv_RightClickAction));
                    hmenu.MenuItems.Add(new MenuItem("&Remove from Highlights", lv_RightClickAction));
                    hmenu.MenuItems.Add(new MenuItem("&Clear All Highlights", lv_RightClickAction));
                    cm.MenuItems.Add(hmenu);

                    if (lv.SelectedItems.Count > 1)
                    {
                        MenuItem menu = new MenuItem("&Set > ");
                        menu.MenuItems.Add(new MenuItem("Invoice no", setValues));
                        menu.MenuItems.Add(new MenuItem("Type", setValues));
                        menu.MenuItems.Add(new MenuItem("Particulars", setValues));
                        menu.MenuItems.Add(new MenuItem("MT", setValues));
                        cm.MenuItems.Add(menu);
                    }

                    cm.Show(sender as Control, e.Location);
                }

            }
            catch (Exception excep)
            {
                String err = "Unable to perform MouseClick_DisplayLV operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setValues(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                List<String> ids = new List<String>();
                foreach (ListViewItem li in lv.SelectedItems)
                {
                    Payment p = li.Tag as Payment;
                    if (p.ID != 0)
                        ids.Add(p.ID.ToString());
                }
                frmSetValues fsv = new frmSetValues(ids.ToArray<String>(), mi.Text);
                if (fsv.ShowDialog(this) == DialogResult.OK)
                {
                    lv.Items.Clear();
                    loadPayments();
                }

            }
            catch (Exception excep)
            {
                String err = "Unable to perform set_values operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lv_RightClickAction(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                if (mi.Text.StartsWith("&Edit"))
                {
                    lv_DoubleClick(sender, e);
                }
                else if (mi.Text.StartsWith("&Delete"))
                {
                    DialogResult dr = MessageBox.Show(this, "Are you sure to delete all selected payment permanent ?", "Delete payments", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int deleted = 0;
                        foreach (ListViewItem li in lv.SelectedItems)
                        {
                            deleted += Datastore.current.Payments.RemoveAll(x => (x.ID.ToString().Equals(li.SubItems[0].Text)));
                        }
                        Datastore.dataFile.Save();
                        lv.Items.Clear();
                        loadPayments();
                        MessageBox.Show(this, "Total " + deleted + " payments are deleted permanently.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (mi.Text.StartsWith("&Add to Highlights"))
                {
                    foreach (ListViewItem li in lv.SelectedItems)
                    {
                        Payment p = li.Tag as Payment;
                        if (p != null)
                        {
                            p.HighlightThis = true;
                        }
                    }
                }
                else if (mi.Text.StartsWith("&Remove from Highlights"))
                {
                    foreach (ListViewItem li in lv.SelectedItems)
                    {
                        Payment p = li.Tag as Payment;
                        if (p != null)
                        {
                            p.HighlightThis = false;
                        }
                    }
                }
                else if (mi.Text.StartsWith("&Clear All Highlights"))
                {
                    foreach (ListViewItem li in lv.Items)
                    {
                        Payment p = li.Tag as Payment;
                        if (p != null)
                        {
                            p.HighlightThis = false;
                        }
                    }
                }

            }
            catch (Exception excep)
            {
                String err = "Unable to perform rightclick_on_displayLV operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void callMenuItemFromName(MenuItem mItem)
        {
            lv_RightClickAction(mItem, new EventArgs());
        }

        private void __SortOnClick_ListView_onColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;

                switch (e.Column)
                {
                    case 0:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                        cSorter.ColumnType = ColumnDataType.Number;
                        break;
                    case 1:
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
                String err = "Unable to perform columnsort_displaylv operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lv_MouseDown(object sender, MouseEventArgs e)
        {
            lv.Focus();
            
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 0)
            {
                main.setEditDeleteVisibility(false, false);
            }
            else if (lv.SelectedItems.Count == 1)
            {
                main.setEditDeleteVisibility(true, true);
            }
            else if (lv.SelectedItems.Count > 1)
            {
                main.setEditDeleteVisibility(false, true);
            }
        }

        private void lv_DoubleClick(object sender, EventArgs e)
        {
            if(lv.SelectedItems.Count>0) {
                NewPayment editP = new NewPayment();
                editP.setEditMode(lv.SelectedItems[0].SubItems[0].Text.Trim());
                editP.ShowDialog(this);
                loadPayments();
            }
        }

        private void lv_KeyDown(object sender, KeyEventArgs e)
        {
            if (lv.SelectedItems.Count == 1 && e.KeyCode==Keys.Enter)
            {
                lv_DoubleClick(sender, new EventArgs());
            }
        }
    }
}
