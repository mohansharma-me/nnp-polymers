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
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly
{
    public partial class UserLogin : Form
    {
        private ListViewColumnSorter cSorter = null;
        private WaitingDialog waitingDialog = null;

        public UserLogin()
        {
            InitializeComponent();
        }

        private void UserLogin_Shown(object sender, EventArgs e)
        {
            Datastore.current = null;
            loadClients();
            if (Datastore.dataFile.mail_Password.Trim().Length == 0)
            {
                new MailSettings().ShowDialog(this);
            }
        }

        private void loadClients()
        {
            loadClients("");
        }

        private void loadClients(String searchString)
        {
            try
            {
                lv.Items.Clear();
                foreach (UserAccount ua in Datastore.dataFile.UserAccounts)
                {
                    String comb = ua.ClientName + " " + ua.ClientDescription + " " + ua.OpeningBalance;
                    bool flag = false;
                    if (searchString.Trim().Length > 0 && comb.ToLower().Trim().Contains(searchString.Trim().ToLower()))
                    {
                        flag = true;
                    }
                    ListViewItem li = new ListViewItem(new String[] { ua.ID.ToString(), ua.ClientName, ua.ClientDescription, ua.OpeningBalance.ToString("0.00"), "Calculating ..." });
                    li.Tag = ua;
                    if (searchString.Trim().Length > 0 && flag)
                        lv.Items.Add(li);
                    else if (searchString.Trim().Length == 0)
                        lv.Items.Add(li);

                    loadClientClosingBalance(ua.ID);
                }

                cSorter = new ListViewColumnSorter();
                lv.ListViewItemSorter = cSorter;
                lv.ColumnClick += __SortOnClick_ListView_onColumnClick;
                cSorter.ColumnType = ColumnDataType.String;
                cSorter.Order = SortOrder.Ascending;
                cSorter.SortColumn = 1;
                lv.Sort();
                lv.SetSortIcon(1, SortOrder.Descending);
            }
            catch (Exception excep)
            {
                String err = "Unable to perform loadClients_clients operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadClientClosingBalance(Decimal clientID)
        {
            Thread thr = new Thread(new ParameterizedThreadStart(threadLoadClosingBalance));
            thr.Name = "Load Client Closing Balance [" + clientID + "]";
            thr.Priority = ThreadPriority.Highest;
            thr.Start(clientID);
        }

        private void threadLoadClosingBalance(object obj)
        {
            Decimal clientID = (Decimal)obj;
            
            String output = "-";

            UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == clientID));
            if (client != null)
            {
                double cb = 0;
                if (client.OBType.ToLower().Trim().Equals("credit"))
                {
                    cb += client.OpeningBalance;
                }
                else if(client.OBType.ToLower().Trim().Equals("debit"))
                {
                    cb -= client.OpeningBalance;
                }
                foreach (Payment payment in client.Payments)
                {
                    if (payment.Credit > 0)
                    {
                        cb += payment.Credit;
                    }
                    else if (payment.Debit > 0) 
                    {
                        cb -= payment.Debit;
                    }
                }
                output = cb.ToString("0")+".00";
            }

            Action action = () => {
                foreach (ListViewItem li in lv.Items)
                {
                    if (li.Tag != null && li.Tag is UserAccount)
                    {
                        if ((li.Tag as UserAccount).ID == clientID)
                        {
                            li.SubItems[4].Text = output;
                            break;
                        }
                    }
                }
                //lv.Items[indexOfLI].SubItems[4].Text = output;
            };
            Invoke(action);
        }

        private void __SortOnClick_ListView_onColumnClick(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;

                switch (e.Column)
                {
                    case 0:
                    case 3:
                        cSorter.ColumnType = ColumnDataType.Number;
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
                String err = "Unable to perform columnsort_clients operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UserLogin_Load(object sender, EventArgs e)
        {
            Datastore.current = null;
        }

        private void lv_MouseEnter(object sender, EventArgs e)
        {
            if(this.Focused)
            lv.Focus();
        }

        private void newClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewClients newClients = new NewClients();
            newClients.ShowDialog(this);
            loadClients();
        }

        private void lv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lv.SelectedItems.Count>0)
            {
                ContextMenu cm = new ContextMenu();
                if(lv.SelectedItems.Count==1)
                    cm.MenuItems.Add(new MenuItem("&Edit client details",lv_RightClickAction));
                if (lv.SelectedItems.Count == 1)
                    cm.MenuItems.Add(new MenuItem("&Delete selected client", lv_RightClickAction));
                else
                    cm.MenuItems.Add(new MenuItem("&Delete selected clients", lv_RightClickAction));

                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Send Collection Message", lv_RightClickAction));

                cm.Show(sender as Control, e.Location);
            }
        }

        private void lv_RightClickAction(object o,EventArgs e)
        {
            try
            {
                MenuItem mi = o as MenuItem;
                if (mi.Text.StartsWith("&Edit"))
                {
                    EditUserAccount edit = new EditUserAccount(lv.SelectedItems[0].SubItems[0].Text.Trim());
                    edit.ShowDialog(this);
                    loadClients();
                }
                else if (mi.Text.StartsWith("&Delete"))
                {
                    String clients = "";
                    foreach (ListViewItem li in lv.SelectedItems)
                        clients += (clients.Length == 0 ? "" : ",") + li.SubItems[1].Text;

                    DialogResult dr = MessageBox.Show(this, "Are you sure to delete all selected clients with its payment datastore ??" + Environment.NewLine + "Selected clients:" + Environment.NewLine + clients, "Delete clients", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        foreach (ListViewItem li in lv.SelectedItems)
                        {
                            UserAccount userAccount = li.Tag as UserAccount;
                            Datastore.dataFile.DebitNotes.RemoveAll(x => (x.clientID == userAccount.ID));
                            Datastore.dataFile.DebitAdvises.RemoveAll(x => (x.clientID == userAccount.ID));
                            Datastore.dataFile.UserAccounts.RemoveAll(x => (x.ID == userAccount.ID));
                        }
                        Datastore.dataFile.Save();
                        loadClients();
                    }
                }
                else if (mi.Text.StartsWith("&Send Collection"))
                {
                    if (lv.SelectedItems.Count > 0)
                    {
                        List<Message> messages = new List<Message>();
                        foreach (ListViewItem li in lv.SelectedItems)
                        {
                            if (li.Tag != null && li.Tag is UserAccount)
                            {
                                UserAccount user = li.Tag as UserAccount;
                                if (true)
                                {
                                    double collectingAmount = 0;
                                    if (double.TryParse(li.SubItems[4].Text.Trim(), out collectingAmount))
                                    {
                                        collectingAmount = Math.Abs(collectingAmount);
                                        String msg = Datastore.dataFile.msg_Collection.Replace("%amt%", collectingAmount.ToString("0.00"));
                                        msg = Uri.EscapeDataString(msg);
                                        String link = Datastore.dataFile.sms_API.Replace("%numbers%", user.mobileNumber).Replace("%msg%", msg);
                                        Message.Mail mail = null;
                                        if (user.emailAddress.Trim().Length > 0)
                                        {
                                            String html = Datastore.dataFile.mail_Collection.Replace("%amt%", collectingAmount.ToString("0.00")) + "<br/><br/>" + Datastore.dataFile.mail_From;
                                            mail = new Message.Mail(user.emailAddress, "Due Amount - NNP Polymers", html);
                                        }
                                        messages.Add(new Message(user.ClientName, user.mobileNumber, MessageType.Collection, link, mail, user.ID));
                                    }
                                }
                            }
                        }
                        if (messages.Count > 0)
                        {
                            MSGSender msgsender = new MSGSender(messages);
                            msgsender.ShowDialog(this);
                        }
                        else
                        {
                            MessageBox.Show(this, "Sorry, no client to send collection message.", "No message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform lvclients_rightclickaction operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 0)
            {
                editClientToolStripMenuItem.Visible = deleteClientsToolStripMenuItem.Visible = false;
            }
            else if (lv.SelectedItems.Count == 1)
            {
                editClientToolStripMenuItem.Visible = deleteClientsToolStripMenuItem.Visible = true;
            }
            else if (lv.SelectedItems.Count > 1)
            {
                editClientToolStripMenuItem.Visible = false;
                deleteClientsToolStripMenuItem.Visible = true;
            }
        }

        private void editClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_RightClickAction(new MenuItem("&Edit"), new EventArgs());
        }

        private void deleteClientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_RightClickAction(new MenuItem("&Delete"), new EventArgs());
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lv_SelectedIndexChanged(sender, e);
        }

        private void lv_DoubleClick(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 1)
            {
                Datastore.current = (lv.SelectedItems[0].Tag as UserAccount);
                Close();
            }
        }

        private void createNewRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Filter = "NNP Recovery file|*.npr";
            sd.FileName = "Recovery" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year;
            if (sd.ShowDialog(this) == DialogResult.OK)
            {
                eXML xml = new eXML();
                String backup = xml.PathAppend;
                xml.PathAppend = "";
                if (xml.Write(sd.FileName, typeof(DataFile), Datastore.dataFile, true))
                {
                    MessageBox.Show(this, "Recovery file generated at " + sd.FileName + "!", "Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this,"Sorry, software is not able to generate recovery file.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                xml.PathAppend = backup;
            }            
        }

        private void restoreRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "NNP Recovery file|*.npr";
            od.FileName = "";
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                DialogResult dr = MessageBox.Show(this, "Are you sure to restore this selected recovery over current npp records, because when you restore this your current npp record is replaced by new one ?", "Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr != DialogResult.Yes) return;
                eXML xml = new eXML();
                String backup = xml.PathAppend;
                xml.PathAppend = "";
                if (xml.Read<DataFile>(od.FileName, typeof(DataFile), ref Datastore.dataFile, true))
                {
                    // re-adjust clientCounter, paymentCounter, records
                    Datastore.dataFile.Records.Clear(); // clear records to rebuild it
                    foreach (UserAccount userAcc in Datastore.dataFile.UserAccounts)
                    {
                        List<Record> records = Datastore.dataFile.Records.FindAll(x => (x.ClientID == userAcc.ID));
                        if (userAcc.ID > Datastore.dataFile.UserAccountIDManager)
                            Datastore.dataFile.UserAccountIDManager = userAcc.ID;
                        foreach (Payment pay in userAcc.Payments)
                        {
                            //Record record = records.Find(x => (x.payment != null && x.payment.Compare(pay)));
                            //if (records == null)
                            //{
                                Record.RecordNow(userAcc, pay);
                            //}
                            if (pay.ID > userAcc.PaymentIDManager)
                                userAcc.PaymentIDManager = pay.ID;
                        }
                    }

                    loadClients();
                    MessageBox.Show(this,"Recovery file loaded.","Loaded",MessageBoxButtons.OK,MessageBoxIcon.Information);
                } else {
                    MessageBox.Show(this,"Selected file is not recovery file.","Not recovery file",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    MessageBox.Show(this,""+xml.Error);
                }
                xml.PathAppend = backup;
                Datastore.dataFile.Save();
            }
        }

        private void importSupplyExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool exclFlag = false;
            try
            {
                Excel.Application appexcl = new Excel.Application();
                Excel.Workbook wb = appexcl.Workbooks.Add();
                wb.Close(false);
                appexcl.Quit();
                exclFlag = true;
            }
            catch (Exception) { }
            if (!exclFlag)
            {
                MessageBox.Show(this, "Microsoft Office Interop library is not instaled, please install microsoft office 2010 or leter.", "MS Office not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Supply Excel file...";
            ofd.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            ofd.CheckFileExists=true;
            if (ofd.ShowDialog(this) == DialogResult.OK || true)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(importSupply));
                thread.Name = "ImportSupplyExcelThread";
                thread.Start(ofd.FileName);
            }
        }

        private void importSupply(object filename)
        {
            try
            {
                Excel.Application app=null;
                Excel.Workbook wb = null;
                List<UserAccount> newclients = null;

                String file = filename.ToString();
                if (file.Trim().Length != 0)
                {
                    app = new Excel.Application();
                    wb = app.Workbooks.Open(file);

                    newclients = new List<UserAccount>();

                    #region fetchFromExcel
                    foreach (Excel.Worksheet ws in wb.Worksheets)
                    {
                        Excel.Range range = ws.UsedRange;
                        for (int i = 0; i < range.Rows.Count; i++)
                        {
                            if (i == 0) continue;
                            int row = i + 1;
                            object _clientname, _invoiceno, _date, _mt, _amount, _finv, _famt, _grade;
                            _clientname = ws.get_Range("A" + row).Value2;
                            _invoiceno = ws.get_Range("B" + row).Value2;
                            _date = ws.get_Range("C" + row).get_Value();
                            _grade = ws.get_Range("D" + row).Value2;
                            _mt = ws.get_Range("E" + row).Value2;
                            _amount = ws.get_Range("F" + row).Value2;
                            _finv = ws.get_Range("G" + row).Value2;
                            _famt = ws.get_Range("H" + row).Value2;

                            if (_clientname != null && _invoiceno != null && _date != null && _mt != null && _amount != null && _grade != null || (_clientname != null && _date != null && _famt != null && _finv != null))
                            {
                                String clientname = _clientname.ToString();
                                String invoiceno = _invoiceno == null ? "" : _invoiceno.ToString();
                                String date = _date.ToString();
                                String grade = _grade == null ? "" : _grade.ToString();
                                String mt = _mt == null ? "" : _mt.ToString();
                                String amount = _amount == null ? "" : _amount.ToString();
                                String finv = "", famt = "";

                                if (_finv != null && _famt != null)
                                {
                                    finv = _finv.ToString();
                                    famt = _famt.ToString();
                                }

                                double samount = 0, smt = 0;
                                if (!double.TryParse(amount, out samount) || !double.TryParse(mt, out smt))
                                {
                                    if (finv.Trim().Length == 0)
                                        continue;
                                }

                                Grade gr = Datastore.dataFile.Grades.Find(x => (x.GradeName.Replace(" ", "").ToLower().Equals(grade.Replace(" ", "").ToLower())));
                                if (grade.Trim().Length > 0 && gr == null)
                                {
                                    /*Action myAct = () => {
                                        EditGrade editGrade = new EditGrade(grade);
                                        editGrade.ShowDialog(this);
                                    };
                                    Grade newGrade = new Grade();
                                    newGrade.GradeName = grade;
                                    newGrade.Amount = 100;
                                    Datastore.dataFile.Grades.Add(newGrade);*/
                                }

                                bool oldClient = false;
                                UserAccount client = null;
                                client = newclients.Find(x =>
                                {
                                    String cname = x.ClientName.ToLower().Trim().Replace(" ", "");
                                    String temp = clientname.ToLower().Trim().Replace(" ", "");
                                    oldClient = cname.Equals(temp);
                                    return oldClient;
                                });

                                Payment sale = new Payment();
                                sale.Credit = 0;
                                sale.Date = DateTime.Parse(date);
                                sale.Date = new DateTime(sale.Date.Year, sale.Date.Month, sale.Date.Day, 12, 0, 0);
                                sale.Debit = samount;
                                sale.DocChqNo = invoiceno;
                                sale.MT = smt.ToString();
                                sale.Particulars = "";
                                sale.Remain = 0;
                                sale.Type = "Sale";
                                sale.ID = 0;
                                sale.Grade = grade;

                                Payment gen = new Payment();
                                gen.Credit = 0;
                                gen.Date = sale.Date;
                                gen.MT = "0";
                                gen.Particulars = "";
                                gen.Remain = 0;
                                if (Datastore.dataFile.PriorityTypes.Count > 0)
                                    gen.Type = Datastore.dataFile.PriorityTypes[0][0].ToString().ToUpper() + Datastore.dataFile.PriorityTypes[0].Substring(1);
                                else
                                    gen.Type = "Jrnl";
                                gen.ID = 0;
                                bool jrnlAdded = false;

                                if (finv.Length > 0 && famt.Length > 0)
                                {
                                    gen.DocChqNo = finv;
                                    double amt = 0;
                                    if (double.TryParse(famt, out amt))
                                    {
                                        gen.Debit = amt;
                                        jrnlAdded = true;
                                    }
                                }

                                if (oldClient)
                                {
                                    if (amount.Trim().Length > 0)
                                        client.Payments.Add(sale);
                                    if (jrnlAdded)
                                        client.Payments.Add(gen);
                                }
                                else
                                {
                                    client = new UserAccount();
                                    client.ClientDescription = "";
                                    client.ClientName = clientname;
                                    client.FooText = "";
                                    client.ID = 0;
                                    client.Payments = new List<Payment>();
                                    if (amount.Trim().Length > 0)
                                        client.Payments.Add(sale);
                                    if (jrnlAdded)
                                        client.Payments.Add(gen);

                                    newclients.Add(client);
                                }
                            }
                        }
                    }
                    #endregion
                }

                importSupplyWindowForm(newclients);

                #region assignClients
                /*foreach (UserAccount client in newclients)
                {
                    UserAccount oldclient = Datastore.dataFile.UserAccounts.Find(x =>
                    {
                        String cname = x.ClientName.ToLower().Trim().Replace(" ", "");
                        String temp = client.ClientName.ToLower().Trim().Replace(" ", "");
                        return cname.Equals(temp);
                    });

                    bool isNewClient = oldclient == null ? true : false;

                    if (isNewClient)
                    {
                        Decimal idcounter = 1;
                        foreach (Payment p in client.Payments)
                        {
                            p.ID = idcounter;
                            idcounter++;
                        }

                        Decimal max = 0;
                        foreach (UserAccount ua in Datastore.dataFile.UserAccounts)
                            if (ua.ID > max)
                                max = ua.ID;
                        max++;

                        client.ID = max;
                        Datastore.dataFile.UserAccounts.Add(client);
                    }
                    else
                    {
                        Decimal max = 0;
                        foreach (Payment p in oldclient.Payments)
                            if (p.ID > max)
                                max = p.ID;
                        max++;
                        Decimal idcounter = max;
                        foreach (Payment p in client.Payments)
                        {
                            p.ID = idcounter;
                            idcounter++;
                            oldclient.Payments.Add(p);
                        }
                    }
                }*/
                #endregion

                if (file.Trim().Length != 0)
                {
                    wb.Close(false);
                    app.Quit();
                }
                Datastore.dataFile.Save();
                thLoadClients();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform importsupply operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importSupplyWindowForm(List<UserAccount> clients)
        {
            try
            {
                Action act = () => {
                    ImportSupply imps = new ImportSupply(clients);
                    if (imps.ShowDialog(this) == DialogResult.OK)
                    {

                    }
                };
                if (this.InvokeRequired)
                {
                    Invoke(act);
                }
                else
                {
                    act();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(""+ex);
            }
        }

        private void thLoadClients()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    Action a = () =>
                    {
                        loadClients();
                    };
                    Invoke(a);
                }
                else
                {
                    loadClients();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform thloadclients operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importPaymentExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool exclFlag = false;
            try
            {
                Excel.Application appexcl = new Excel.Application();
                Excel.Workbook wb = appexcl.Workbooks.Add();
                wb.Close(false);
                appexcl.Quit();
                exclFlag = true;
            }
            catch (Exception) { }
            if (!exclFlag)
            {
                MessageBox.Show(this, "Microsoft Office Interop library is not instaled, please install microsoft office 2010 or leter.", "MS Office not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open Payment Excel file...";
            ofd.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog(this) == DialogResult.OK || true)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(importPayments));
                thread.Name = "ImportPaymentExcelThread";
                thread.Start(ofd.FileName);
            }
        }

        private void importPayments(object fname)
        {
            try
            {
                List<UserAccount> newclients = null;
                Excel.Application app = null;
                Excel.Workbook wb = null;
                String file = fname.ToString();
                if (file.Trim().Length != 0)
                {
                    app = new Excel.Application();
                    wb = app.Workbooks.Open(file);

                    newclients = new List<UserAccount>();

                    #region fetchFromExcel
                    foreach (Excel.Worksheet ws in wb.Worksheets)
                    {
                        Excel.Range range = ws.UsedRange;
                        for (int i = 0; i < range.Rows.Count; i++)
                        {
                            if (i == 0) continue;
                            int row = i + 1;
                            object _clientname, _chno, _date, _amount;
                            _clientname = ws.get_Range("C" + row).Value2;
                            _chno = ws.get_Range("B" + row).Value2;
                            _date = ws.get_Range("A" + row).get_Value();
                            _amount = ws.get_Range("D" + row).Value2;

                            if (_clientname != null && _chno != null && _date != null && _amount != null)
                            {
                                String clientname = _clientname.ToString();
                                String chno = _chno.ToString();
                                String date = _date.ToString();
                                String amount = _amount.ToString();

                                double samount = 0;
                                if (!double.TryParse(amount, out samount))
                                {
                                    continue;
                                }

                                bool oldClient = false;
                                UserAccount client = null;
                                client = newclients.Find(x =>
                                {
                                    String cname = x.ClientName.ToLower().Trim().Replace(" ", "");
                                    String temp = clientname.ToLower().Trim().Replace(" ", "");
                                    oldClient = cname.Equals(temp);
                                    return oldClient;
                                });

                                Payment sale = new Payment();
                                sale.Debit = 0;
                                sale.Date = DateTime.Parse(date);
                                sale.Date = new DateTime(sale.Date.Year, sale.Date.Month, sale.Date.Day, 12, 0, 0);
                                sale.Credit = samount;
                                sale.DocChqNo = chno;
                                sale.MT = "";
                                sale.Particulars = "";
                                sale.Remain = samount;
                                sale.Type = "BRct";
                                sale.ID = 0;

                                if (oldClient)
                                {
                                    client.Payments.Add(sale);
                                }
                                else
                                {
                                    client = new UserAccount();
                                    client.ClientDescription = "";
                                    client.ClientName = clientname;
                                    client.FooText = "";
                                    client.ID = 0;
                                    client.Payments = new List<Payment>();
                                    client.Payments.Add(sale);

                                    newclients.Add(client);
                                }
                            }
                        }
                    }
                    #endregion
                }
                showImportPayments(newclients);

                #region assignClients

                /*foreach (UserAccount client in newclients)
                {
                    UserAccount oldclient = Datastore.dataFile.UserAccounts.Find(x =>
                    {
                        String cname = x.ClientName.ToLower().Trim().Replace(" ", "");
                        String temp = client.ClientName.ToLower().Trim().Replace(" ", "");
                        return cname.Equals(temp);
                    });

                    bool isNewClient = oldclient == null ? true : false;

                    if (isNewClient)
                    {
                        Decimal idcounter = 1;
                        foreach (Payment p in client.Payments)
                        {
                            p.ID = idcounter;
                            idcounter++;
                        }

                        Decimal max = 0;
                        foreach (UserAccount ua in Datastore.dataFile.UserAccounts)
                            if (ua.ID > max)
                                max = ua.ID;
                        max++;

                        client.ID = max;
                        Datastore.dataFile.UserAccounts.Add(client);
                    }
                    else
                    {
                        Decimal max = 0;
                        foreach (Payment p in oldclient.Payments)
                            if (p.ID > max)
                                max = p.ID;
                        max++;
                        Decimal idcounter = max;
                        foreach (Payment p in client.Payments)
                        {
                            p.ID = idcounter;
                            idcounter++;
                            oldclient.Payments.Add(p);
                        }
                    }
                }*/
                #endregion

                if (file.Trim().Length != 0)
                {
                    wb.Close(false);
                    app.Quit();
                }
                Datastore.dataFile.Save();
                thLoadClients();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform importpayments operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void showImportPayments(List<UserAccount> clients)
        {
            Action act = () => {
                ImportPayments impPays = new ImportPayments(clients);
                if (impPays.ShowDialog(this) == DialogResult.OK)
                {

                }
            };
            if (this.InvokeRequired)
            {
                Invoke(act);
            }
            else
            {
                act();
            }
        }

        private void gradeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GradesSettings gs = new GradesSettings();
                gs.ShowDialog(this);
            }
            catch (Exception) { }
        }

        private bool SearchFlag = true;
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            SearchFlag = false;
            if (txtSearch.Text.Trim().Equals("Search client..."))
                txtSearch.Text = "";
            SearchFlag = true;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            SearchFlag = false;
            if (txtSearch.Text.Trim().Length==0)
                txtSearch.Text = "Search client...";
            SearchFlag = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!SearchFlag) return;
            loadClients(txtSearch.Text.Trim());
        }

        private void lv_KeyDown(object sender, KeyEventArgs e)
        {
            if (lv.SelectedItems.Count == 1 && e.KeyCode == Keys.Enter)
            {
                lv_DoubleClick(sender, new EventArgs());
            }
        }

        private void recordsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Records records = new Records();
            records.Show();
        }

        private void collectionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CollectionList collectionList = new CollectionList();
            collectionList.ShowDialog(this);
        }

        private void printFormatSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PrintFormatSettings().ShowDialog(this);
        }

        private void debitNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DebitNotes().ShowDialog(this);
        }

        private void sMSSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SMSSettings().ShowDialog(this);
        }

        private void toolstripRequestForAnOrder_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(threadToRequstOrder));
            thread.Name = "Thread: ToRequestOrder";
            thread.Priority = ThreadPriority.Highest;
            waitingDialog = new WaitingDialog();
            waitingDialog.Text = "Searching last debits";
            thread.Start();
            waitingDialog.ShowDialog(this);
        }

        private void threadToRequstOrder()
        {
            List<Message> messages = new List<Message>();
            foreach (UserAccount user in Datastore.dataFile.UserAccounts)
            {
                user.Payments.Sort(Display.paymentSorter);
                Payment lastPayment = user.Payments.FindLast(x => (x.Type.ToLower().Trim().Equals("sale") && x.Credit == 0 && x.Debit > 0));
                DateTime curDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);
                DateTime lpDate = new DateTime(lastPayment.Date.Year, lastPayment.Date.Month, lastPayment.Date.Day, 12, 0, 0);
                double totalDays = curDate.Subtract(lpDate).TotalDays;
                if (totalDays > (double)Datastore.dataFile.sms_NoOfRequestDays)
                {
                    String msg = Datastore.dataFile.msg_Request.Replace("%days%", totalDays.ToString("0"));
                    msg = Uri.EscapeDataString(msg);
                    String link = Datastore.dataFile.sms_API.Replace("%numbers%", user.mobileNumber).Replace("%msg%", msg);
                    Message.Mail mail = null;
                    if (user.emailAddress.Trim().Length > 0)
                    {
                        String html = Datastore.dataFile.mail_Request.Replace("%days%", totalDays.ToString("0")) + "<br/><br/>" + Datastore.dataFile.mail_From;
                        mail = new Message.Mail(user.emailAddress, "REQUEST FOR AN ORDER", html);
                    }
                    messages.Add(new Message(user.ClientName, user.mobileNumber, MessageType.RequestToOrder, link, mail, user.ID));
                }
            }

            if (messages.Count > 0)
            {
                Action action = () => {
                    MSGSender msgsender = new MSGSender(messages);
                    msgsender.ShowDialog(this);
                    waitingDialog.Close();
                    waitingDialog = null;
                };
                Invoke(action);
            }
            else
            {
                Action action = () => {
                    MessageBox.Show(this, "No client found having no debit since last 3 months.", "No clients", MessageBoxButtons.OK, MessageBoxIcon.Information);
                };
                Invoke(action);
            }
        }

        private void tempToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exception ex = Datastore.SendMail("iammegamohan@gmail.com", "iammegamohan@gmail.com", "Something", "Something", "iammegamohan", "??33Ramji");
            Console.WriteLine("" + ex);
        }

        private void emailSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MailSettings().ShowDialog(this);
        }

        private void printClientListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel files|*.xls;*.xlsx";
            sfd.CheckFileExists = false;
            sfd.OverwritePrompt = true;
            if (sfd.ShowDialog(this) != DialogResult.OK)
                return;

            String filename = sfd.FileName;
            try
            {
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }
            }
            catch (Exception) { }

            bool impFlag = true;
            try
            {
                Excel.Application app = new Excel.Application();
                app.Quit(); app.Quit();
                impFlag = true;
            }
            catch (Exception) { impFlag = false; }
            if (!impFlag)
            {
                MessageBox.Show(this, "Microsoft Office Interop library is not instaled, please install microsoft office 2010 or leter.", "MS Office not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Thread thread = new Thread(new ParameterizedThreadStart(threadPrintClientList));
            thread.Name = "Thread: Export2Excel";
            thread.Priority = ThreadPriority.Highest;
            waitingDialog = new WaitingDialog();
            waitingDialog.Text = "Exporting client list to excel";
            thread.Start(filename);
            waitingDialog.ShowDialog(this);
        }

        private void threadPrintClientList(object filename)
        {
            try
            {
                int count = 0;

                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet ws = wb.Worksheets.Add();

                ws.get_Range("A1").set_Value(Type.Missing, "ID");
                ws.get_Range("B1").set_Value(Type.Missing, "Client Name");
                ws.get_Range("C1").set_Value(Type.Missing, "Opening Balance");
                ws.get_Range("D1").set_Value(Type.Missing, "Closing Balance");

                int row = 1;
                ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row + ":F" + row + ":G" + row).Cells.Interior.Color = System.Drawing.Color.LightGray.ToArgb();

                int totalClients = 0;
                row = 2;
                Action action = () => { totalClients = lv.Items.Count; };
                Invoke(action);

                for (int i = 0; i < totalClients; i++)
                {
                    String id = "", name = "", ob = "", cb = "";
                    action = () => {
                        id = lv.Items[i].SubItems[0].Text;
                        name = lv.Items[i].SubItems[1].Text;
                        ob = lv.Items[i].SubItems[3].Text;
                        cb = lv.Items[i].SubItems[4].Text;
                    };
                    Invoke(action);

                    ws.get_Range("A" + row).set_Value(Type.Missing, id);
                    ws.get_Range("B" + row).set_Value(Type.Missing, name);
                    ws.get_Range("C" + row).set_Value(Type.Missing, ob);
                    ws.get_Range("D" + row).set_Value(Type.Missing, cb);
                    row++;
                }
                ws.get_Range("A1").EntireColumn.AutoFit();
                ws.get_Range("B1").EntireColumn.AutoFit();
                ws.get_Range("C1").EntireColumn.AutoFit();
                ws.get_Range("D1").EntireColumn.AutoFit();

                ws.get_Range("C1").EntireColumn.NumberFormat = "0.00";
                ws.get_Range("D1").EntireColumn.NumberFormat = "0.00";

                bool print = false;
                action = () => {
                    print = MessageBox.Show(this, "Are you sure to print client list now ?", "Print client list", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                };
                Invoke(action);

                if (print)
                {
                    ws.PrintOutEx();
                }

                wb.Close(true, filename);
                app.Quit();
                app.Quit();
                app = null;

                action = () => {
                    waitingDialog.Close();
                    waitingDialog = null;
                };
                Invoke(action);
            }
            catch (Exception excep)
            {
                String err = "Unable to perform threadPrintClientList operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void sendGeneralMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SendCustomMessage().ShowDialog(this);
        }

        private void interestAdviseListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RepMonthSel rep = new RepMonthSel();
            if (rep.ShowDialog(this) == DialogResult.OK)
            {
                InterestAdviseList idl = new InterestAdviseList(rep.selMonth, rep.selYear);
                idl.ShowDialog(this);
            }
        }
    }
}
