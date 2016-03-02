using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Web;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly
{
    public partial class CollectionList : Form
    {
        private bool isProcessExecuted = false;
        private WaitingDialog waitingDialog;
        private List<ClientReport> clientReports = new List<ClientReport>();

        private ListViewColumnSorter cSorter1, cSorter2;

        private List<ClientCollection> clientCollection = new List<ClientCollection>();

        public CollectionList()
        {
            InitializeComponent();
        }

        private void CollectionList_Load(object sender, EventArgs e)
        {
            cSorter1 = new ListViewColumnSorter();
            lvClients.ListViewItemSorter = cSorter1;
            lvClients.ColumnClick += __SortOnClick_ListView_onColumnClick_Sorter1;
            cSorter1.ColumnType = ColumnDataType.String;
            cSorter1.Order = SortOrder.Ascending;
            cSorter1.SortColumn = 0;
            lvClients.Sort();
            lvClients.SetSortIcon(0, SortOrder.Descending);

            cSorter2 = new ListViewColumnSorter();
            lv.ListViewItemSorter = cSorter2;
            lv.ColumnClick += __SortOnClick_ListView_onColumnClick_Sorter2;
            cSorter2.ColumnType = ColumnDataType.String;
            cSorter2.Order = SortOrder.Ascending;
            cSorter2.SortColumn = 0;
            lv.Sort();
            lv.SetSortIcon(0, SortOrder.Descending);

        }

        private void CollectionList_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(threadGetAllCurrentMonthReport));
            thread.Name = "GetAllCurrentMonthReport";
            waitingDialog = new WaitingDialog();
            waitingDialog.Text = "Getting reports ...";
            waitingDialog.ControlBox = true;
            waitingDialog.FormClosed += waitingDialog_FormClosed;
            thread.Start();
            isProcessExecuted = false;
            waitingDialog.ShowDialog(this);
            waitingDialog = new WaitingDialog();
            waitingDialog.ControlBox = true;
            waitingDialog.Text = "Finding debits ...";
            thread = new Thread(new ThreadStart(threadFindingDebits));
            thread.Name = "FindingDebits";
            thread.Start();
            isProcessExecuted = false;
            waitingDialog.ShowDialog(this);
        }

        private void threadFindingDebits()
        {
            DateTime todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
            foreach (ClientReport cr in clientReports)
            {
                UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == cr.clientId));
                if (client != null)
                {
                    ClientCollection clientColl = new ClientCollection();
                    clientColl.clientId = client.ID;
                    clientColl.clientName = client.ClientName;
                    clientColl.totalCollectingAmount = 0;
                    clientColl.cutOffDays = client.CutOffDays;
                    clientColl.payments = new List<Payment>();
                    foreach (ListViewItem li in cr.lvReport.Items)
                    {
                        Payment debit = li.Tag as Payment;
                        Payment credit = li.SubItems[6].Tag as Payment;

                        if (debit != null && credit == null)
                        {
                            String pType = Datastore.dataFile.PriorityTypes.Find(x => (x.Trim().ToLower().Equals(debit.Type.ToLower().Trim())));
                            bool isPType = !(pType == null);
                            double dayGap = todayDate.Subtract(debit.Date).TotalDays;
                            if (dayGap >= client.CutOffDays-1 || isPType)
                            {
                                debit.Tag = client.ID;
                                debit.CollectingAmount = debit.Debit;
                                clientColl.totalCollectingAmount += debit.CollectingAmount;
                                clientColl.payments.Add(debit);
                            }
                        }
                        else if (debit == null && li.Tag != null && li.Tag.ToString().Equals("stop"))
                        {
                            Payment orgDebit = li.SubItems[0].Tag as Payment;
                            if (orgDebit != null)
                            {
                                String pType = Datastore.dataFile.PriorityTypes.Find(x => (x.Trim().ToLower().Equals(orgDebit.Type.ToLower().Trim())));
                                bool isPType = !(pType == null);
                                double dayGap = todayDate.Subtract(orgDebit.Date).TotalDays;
                                if (dayGap >= client.CutOffDays-1 || isPType)
                                {
                                    Payment payment = new Payment();
                                    payment.Credit = 0;
                                    payment.Date = orgDebit.Date;
                                    payment.Debit = orgDebit.Debit;
                                    payment.CollectingAmount = double.Parse(li.SubItems[3].Text.Trim());
                                    payment.DocChqNo = orgDebit.DocChqNo+" - Part";
                                    payment.Grade = orgDebit.Grade;
                                    payment.ID = -1;
                                    payment.MT = orgDebit.MT;
                                    payment.Particulars = orgDebit.Particulars;
                                    payment.Remain = 0;
                                    payment.Type = orgDebit.Type;
                                    payment.Tag = client.ID;
                                    clientColl.totalCollectingAmount += payment.CollectingAmount;
                                    clientColl.payments.Add(payment);
                                }
                            }
                        }
                    }
                    if (clientColl.totalCollectingAmount > 0)
                        clientCollection.Add(clientColl);
                    System.GC.Collect();
                }
            }
            List<Decimal> counterCIDs = new List<Decimal>();
            foreach (ClientCollection cClient in clientCollection)
            {
                /*UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == (Decimal)payment.Tag));
                String clientName = client.ClientName;
                DateTime lstDebitDate = new DateTime(payment.Date.Year, payment.Date.Month, payment.Date.Day, 12, 0, 0);
                lstDebitDate=lstDebitDate.AddDays(client.CutOffDays);*/
                ListViewItem li = new ListViewItem(new String[] { cClient.clientName,cClient.totalCollectingAmount.ToString("0.00")  });//payment.DocChqNo + " (" + payment.Date.ToString(Program.SystemSDFormat) + ", " + payment.MT + ")", lstDebitDate.ToString(Program.SystemSDFormat), payment.CollectingAmount.ToString("0.00") });
                li.Tag = cClient.clientId;
                Action act = () => { lvClients.Items.Add(li); };
                Invoke(act);
            }
            isProcessExecuted = true;
            closeWaitingDialog();
        }

        private void threadGetAllCurrentMonthReport()
        {
            try
            {
                Panel myPanel = new Panel();
                UserAccount backupCurrentUser = Datastore.current;
                foreach (UserAccount client in Datastore.dataFile.UserAccounts)
                {
                    Datastore.current = client;
                    GeneralReport genReport = new GeneralReport(DateTime.Now.Month, DateTime.Now.Year);
                    genReport.isWaitingEnabled = false;
                    genReport.processDone += genReport_processDone;
                    genReport.TopLevel = false;
                    myPanel.Controls.Clear();
                    myPanel.Controls.Add(genReport);
                    genReport.Show();
                }
                Datastore.current = backupCurrentUser;

                closeWaitingDialog();
            }
            catch (Exception ex)
            {
                String err = "Unable to perform GetAllCurrentMonthReport operation.";
                Log.output(err, ex);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            isProcessExecuted = true;
        }

        private void closeWaitingDialog()
        {
            Action action = () => {
                if (waitingDialog != null)
                {
                    waitingDialog.Close();
                    waitingDialog = null;
                }
            };
            if (this.InvokeRequired)
                Invoke(action);
            else
                action();
        }

        void genReport_processDone(Decimal clientID,ListView lv)
        {
            threadSafe_AddToClientRecords(clientID, lv);
        }

        private void threadSafe_AddToClientRecords(Decimal cid,ListView lv)
        {
            Action action = () =>
            {
                ClientReport cr = new ClientReport();
                cr.clientId = cid;
                cr.lvReport = new ListView();
                foreach (ListViewItem li in lv.Items)
                {
                    cr.lvReport.Items.Add(li.Clone() as ListViewItem);
                }
                clientReports.Add(cr);
            };
            if (this.InvokeRequired)
                Invoke(action);
            else
                action();
        }

        void waitingDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (!isProcessExecuted)
                //Close();
        }

        public class ClientReport
        {
            public Decimal clientId;
            public ListView lvReport;
        }

        public class ClientCollection
        {
            public Decimal clientId;
            public String clientName;
            public double cutOffDays;
            public double totalCollectingAmount;
            public List<Payment> payments;
        }

        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count > 0)
            {
                Payment payment = lv.SelectedItems[0].Tag as Payment;
                txtDebitDetails.Text = "Debit date: " + payment.Date.ToString(Program.SystemSDFormat);
                txtDebitDetails.Text += Environment.NewLine + "Debit amount: " + payment.Debit.ToString("0.00");
                txtDebitDetails.Text += Environment.NewLine + "Invoice no: " + payment.DocChqNo;
                txtDebitDetails.Text += Environment.NewLine + "Debit type: " + payment.Type;
                txtDebitDetails.Text += Environment.NewLine + "MT: " + payment.MT;
                txtDebitDetails.Text += Environment.NewLine + "Grade: " + payment.Grade;
                txtDebitDetails.Text += Environment.NewLine + "Particulars: " + Environment.NewLine +payment.Particulars;


                UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == (Decimal)payment.Tag));
                if (client != null)
                {
                    lblClientName.Text = client.ClientName;
                }
                else
                {
                    lblClientName.Text = "Client not found!";
                }

                lblCollectingAmount.Text = lv.SelectedItems[0].SubItems[2].Text;
                String pType = Datastore.dataFile.PriorityTypes.Find(x => (x.Trim().ToLower().Equals(payment.Type.ToLower().Trim())));
                bool isPType = !(pType == null);
                DateTime lstDebitDate = payment.Date.AddDays(client.CutOffDays);
                lblLastDateWas.Text = lstDebitDate.ToString(Program.SystemSDFormat);

                lvUser.Items.Clear();
                if (payment.ID > -1)
                {
                    ClientReport cr = clientReports.Find(x => (x.clientId == client.ID));
                    if (cr != null)
                    {
                        foreach (ListViewItem li in cr.lvReport.Items)
                        {
                            Payment _debitPayment = li.Tag as Payment;
                            if (_debitPayment != null && _debitPayment.ID == payment.ID)
                            {
                                lvUser.Items.Add(li.Clone() as ListViewItem);
                            }
                        }
                    }
                }
                else
                {
                    ClientReport cr = clientReports.Find(x => (x.clientId == client.ID));
                    if (cr != null)
                    {
                        foreach (ListViewItem li in cr.lvReport.Items)
                        {
                            if (li.SubItems[3].Text.Trim().Equals(payment.CollectingAmount.ToString()))
                            {
                                int firstIndex=-1;
                                for (int i = li.Index; i >= 0; i--)
                                {
                                    if (cr.lvReport.Items[i].Tag == null)
                                    {
                                        firstIndex = i;
                                        break;
                                    }
                                }
                                firstIndex++;
                                for (int i = firstIndex; i <= li.Index; i++)
                                    lvUser.Items.Add(cr.lvReport.Items[i].Clone() as ListViewItem);
                            }
                        }
                    }
                }

                groupBox1.Visible = true;
            }
            else
            {
                groupBox1.Visible = false;
            }
        }

        private void btnAdjustmentInfo_Click(object sender, EventArgs e)
        {

        }

        private void lvClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvClients.SelectedItems.Count == 1)
            {
                lv.Items.Clear();
                groupBox1.Visible = false;
                ClientCollection client = clientCollection.Find(x => (x.clientId == (Decimal)lvClients.SelectedItems[0].Tag));
                if (client != null)
                {
                    DateTime todayDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);
                    foreach (Payment payment in client.payments)
                    {
                        //DateTime lstDebitDate=payment.Date.AddDays(client.cutOffDays);
                        DateTime pDate = new DateTime(payment.Date.Year, payment.Date.Month, payment.Date.Day, 12, 0, 0);
                        double totalDays = todayDate.Subtract(pDate).TotalDays;
                        ListViewItem li = new ListViewItem(new String[] { payment.DocChqNo, payment.Date.ToString(Program.SystemSDFormat), payment.CollectingAmount.ToString("0.00"), totalDays.ToString("0") });
                        li.Tag = payment;
                        lv.Items.Add(li);
                    }
                }
                lv.Tag = lvClients.SelectedIndices[0];
            }
        }

        private void __SortOnClick_ListView_onColumnClick_Sorter1(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;
                switch (e.Column)
                {
                    case 1:
                        cSorter1.ColumnType = ColumnDataType.String;
                        break;
                    case 2:
                        cSorter1.ColumnType = ColumnDataType.Number;
                        break;
                    default:
                        cSorter1.ColumnType = ColumnDataType.String;
                        break;
                }

                if (e.Column == cSorter1.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (cSorter1.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        lv.SetSortIcon(e.Column, SortOrder.Ascending);
                        cSorter1.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        lv.SetSortIcon(e.Column, SortOrder.Descending);
                        cSorter1.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    cSorter1.SortColumn = e.Column;
                    cSorter1.Order = System.Windows.Forms.SortOrder.Ascending;
                    lv.SetSortIcon(e.Column, SortOrder.Descending);
                }

                // Perform the sort with these new sort options.
                lv.Sort();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform columnsort_records_cSorter1 operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void __SortOnClick_ListView_onColumnClick_Sorter2(object sender, ColumnClickEventArgs e)
        {
            try
            {
                ListView lv = sender as ListView;
                switch (e.Column)
                {
                    case 1:
                        cSorter2.ColumnType = ColumnDataType.String;
                        break;
                    case 2:
                        cSorter2.ColumnType = ColumnDataType.DateTime;
                        break;
                    case 3:
                        cSorter2.ColumnType = ColumnDataType.Number;
                        break;
                    default:
                        cSorter2.ColumnType = ColumnDataType.String;
                        break;
                }

                if (e.Column == cSorter2.SortColumn)
                {
                    // Reverse the current sort direction for this column.
                    if (cSorter2.Order == System.Windows.Forms.SortOrder.Ascending)
                    {
                        lv.SetSortIcon(e.Column, SortOrder.Ascending);
                        cSorter2.Order = System.Windows.Forms.SortOrder.Descending;
                    }
                    else
                    {
                        lv.SetSortIcon(e.Column, SortOrder.Descending);
                        cSorter2.Order = System.Windows.Forms.SortOrder.Ascending;
                    }
                }
                else
                {
                    // Set the column number that is to be sorted; default to ascending.
                    cSorter2.SortColumn = e.Column;
                    cSorter2.Order = System.Windows.Forms.SortOrder.Ascending;
                    lv.SetSortIcon(e.Column, SortOrder.Descending);
                }

                // Perform the sort with these new sort options.
                lv.Sort();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform columnsort_records_cSorter1 operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSClient_TextChanged(object sender, EventArgs e)
        {
            if (!txtSClient.Text.Trim().ToLower().Equals("filter clients"))
            {
                lvClients.Items.Clear();
                foreach (ClientCollection client in clientCollection)
                {
                    String search = txtSClient.Text.Trim().ToLower();
                    if (client.clientName.ToLower().Trim().Contains(search) || search.Length==0)
                    {
                        ListViewItem li = new ListViewItem(new String[] { client.clientName, client.totalCollectingAmount.ToString("0.00") });
                        li.Tag = client.clientId;
                        lvClients.Items.Add(li);
                    }
                }
            }
        }

        private void txtSPays_TextChanged(object sender, EventArgs e)
        {
            if (!txtSPays.Text.Trim().ToLower().Equals("filter entries"))
            {
                lv.Items.Clear();
                Decimal idOfClient = (Decimal)lvClients.Items[(int)lv.Tag].Tag;
                ClientCollection client = clientCollection.Find(x => (x.clientId == idOfClient));
                if(client!=null)
                foreach (Payment payment in client.payments)
                {
                    DateTime lstDebitDate = payment.Date.AddDays(client.cutOffDays);
                    String search = txtSPays.Text.Trim().ToLower();
                    String searchSource= payment.CollectingAmount+" "+payment.CollectingAmount+" "+payment.Debit+" "+payment.DocChqNo+" "+payment.Grade+" "+payment.MT+" "+payment.Particulars+" "+payment.Type+" "+payment.Date.ToString(Program.SystemSDFormat)+" "+lstDebitDate.ToString(Program.SystemSDFormat);
                    if (searchSource.ToLower().Trim().Contains(search) || search.Length == 0)
                    {
                        ListViewItem li = new ListViewItem(new String[] { payment.DocChqNo, payment.Date.ToString(Program.SystemSDFormat), payment.CollectingAmount.ToString("0.00") });
                        li.Tag = payment;
                        lv.Items.Add(li);
                    }
                }
            }
        }

        private void lvClients_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lvClients.CheckedItems.Count > 0)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("&Send Collection Message(s)", lvClients_RightClickAction));
                cm.Show(sender as Control, e.Location);
            }
        }

        private void lvClients_RightClickAction(object sender,EventArgs e)
        {
            #region old-code
            /*List<CollectionMSG> msgs = new List<CollectionMSG>();

            if (lvClients.CheckedItems.Count > 0)
            {
                foreach (ListViewItem li in lvClients.CheckedItems)
                {
                    Decimal clientID = -1;
                    if (li.Tag is Decimal)
                    {
                        clientID = (Decimal)li.Tag;
                        UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == clientID));
                        if (client != null)
                        {
                            long mobileNumber = -1;
                            double collectingAmount = 0;
                            if (client.mobileNumber.Trim().Length > 0 && long.TryParse(client.mobileNumber, out mobileNumber) && double.TryParse(li.SubItems[1].Text.Trim(), out collectingAmount))
                            {
                                msgs.Add(new CollectionMSG(mobileNumber, collectingAmount));
                            }
                        }
                    }
                }
            }

            if (msgs.Count <= 0)
            {
                MessageBox.Show(this, "Sorry, there is not any collection message.", "Nothing", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Thread threadSendMsgs = new Thread(new ParameterizedThreadStart(threadSendCollectionMsgs));
                waitingDialog = new WaitingDialog();
                waitingDialog.Text = "Sending collection messages ...";
                threadSendMsgs.Start(msgs);
                waitingDialog.ShowDialog(this);
            }*/
            #endregion

            List<Message> messages = new List<Message>();
            foreach (ListViewItem li in lvClients.CheckedItems)
            {
                Decimal clientID = -1;
                if (li.Tag is Decimal)
                {
                    clientID = (Decimal)li.Tag;
                    UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == clientID));
                    if (client != null && (client.mobileNumber.Trim().Length > 0 || client.emailAddress.Trim().Length > 0)) 
                    {
                        String msg = Datastore.dataFile.msg_Collection;
                        double collectingAmount = 0;
                        if (double.TryParse(li.SubItems[1].Text.Trim(), out collectingAmount))
                        {
                            msg = msg.Replace("%amt%", collectingAmount.ToString("0.00"));
                            String link = Datastore.dataFile.sms_API.Replace("%numbers%", client.mobileNumber);
                            link = link.Replace("%msg%", Uri.EscapeDataString(msg));

                            //mail generation
                            Message.Mail mail = null;
                            if (client.emailAddress.Trim().Length > 0)
                            {
                                String htmlTable = "<br/><br/><center><table style='' cellspacing=2 cellpadding=2 border=1><tr><th>DATE</th><th>INV./DN No</th><th>AMT</th></tr>";
                                ClientCollection cclient = clientCollection.Find(x => (x.clientId == clientID));
                                if (cclient != null)
                                {
                                    foreach (Payment payment in cclient.payments)
                                    {
                                        String tr = "<tr>";
                                        tr += "<td>" + payment.Date.ToString(Program.SystemSDFormat) + "</td><td>" + payment.DocChqNo + "</td><td>" + payment.CollectingAmount.ToString("0.00") + "</td>";
                                        tr += "</tr>";
                                        htmlTable += tr;
                                    }
                                }
                                htmlTable = htmlTable + "</table></center><br/>" + Datastore.dataFile.mail_From;

                                mail = new Message.Mail();
                                mail.From = Datastore.dataFile.mail_MyMail;
                                mail.HTML = Datastore.dataFile.mail_Collection.Replace("%amt%", collectingAmount.ToString("0.00")) + Environment.NewLine + Environment.NewLine + htmlTable;
                                mail.To = client.emailAddress;
                                mail.username = Datastore.dataFile.mail_Username;
                                mail.password = Datastore.dataFile.mail_Password;
                                mail.Subject = "Due amount - NNP Polymers";
                            }
                            messages.Add(new Message(client.ClientName, client.mobileNumber, MessageType.Collection, link, mail,client.ID));
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
                MessageBox.Show(this, "No client found to send messages.", "No messages to send", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void threadSendCollectionMsgs(object list)
        {
            try
            {
                Action action;
                List<CollectionMSG> msgs = (List<CollectionMSG>)list;
                action = () => {
                    waitingDialog.getProgressBar().Style = ProgressBarStyle.Blocks;
                    waitingDialog.getProgressBar().Maximum = msgs.Count;
                    waitingDialog.getProgressBar().Value = 0;
                };
                Invoke(action);

                foreach (CollectionMSG msg in msgs)
                {
                    try
                    {
                        String _msg = Datastore.dataFile.msg_Collection.Replace("%amt%", msg.Amount.ToString("0.00"));
                        _msg = Uri.EscapeDataString(_msg);
                        object obj = Datastore.DownloadString(Datastore.dataFile.sms_API.Replace("%numbers%", msg.Mobile.ToString()).Replace("%msg%", _msg));
                        if (obj is Exception)
                        {
                            throw (Exception)obj;
                        }
                        else
                        {
                            Console.WriteLine("TN:" + obj);
                        }
                        /*WebClient webClient = new WebClient();
                        String _msg = Datastore.dataFile.msg_Collection.Replace("%amt%", msg.Amount.ToString("0.00"));
                        _msg = Uri.EscapeDataString(_msg);
                        String retTN = webClient.DownloadString(Datastore.dataFile.sms_API.Replace("%numbers%", msg.Mobile.ToString()).Replace("%msg%", _msg));*/
                        action = () => {
                            waitingDialog.getProgressBar().Value++;
                        };
                        Invoke(action);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can't send collection message to " + msg.Mobile, "Network Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.output("Can't send collection messages (Thread).", ex);
                MessageBox.Show("Can't send collection messages.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            closeWaitingDialog();
        }

        public class CollectionMSG
        {
            public CollectionMSG(long number,double amount)
            {
                this.Mobile = number;
                this.Amount = amount;
            }

            public long Mobile;
            public double Amount;
        }

        private bool isAllChecked = false;
        private void lvClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (isAllChecked)
                {
                    foreach (ListViewItem li in lvClients.Items)
                        li.Checked = false;
                    isAllChecked = false;
                }
                else
                {
                    foreach (ListViewItem li in lvClients.Items)
                        li.Checked = true;
                    isAllChecked = true;
                }
            }
        }

        private void btnPrintCollecionList_Click(object sender, EventArgs e)
        {
            waitingDialog = new WaitingDialog();
            waitingDialog.Text = "Printing collection list...";
            Thread thread = new Thread(new ThreadStart(threadPrintCollectionList));
            thread.Name = "Thread: Print Collection List";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            waitingDialog.ShowDialog(this);
        }

        private void threadPrintCollectionList()
        {
            List<ClientCollection> reports = new List<ClientCollection>();
            Action action = () => {
                reports = clientCollection;
            };
            Invoke(action);

            String filename = "";
            action = () => {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel files|*.xls;*.xlsx";
                sfd.OverwritePrompt = true;
                sfd.ShowDialog(this);
                filename = sfd.FileName == null ? "" : sfd.FileName;
                if (filename.Trim().Length > 0)
                {
                    try
                    {
                        if (File.Exists(filename))
                        {
                            File.Delete(filename);
                        }
                    }
                    catch (Exception) { }
                }
            };
            Invoke(action);

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
                action = () =>
                {
                    MessageBox.Show(this, "Microsoft Office Interop library is not instaled, please install microsoft office 2010 or leter.", "MS Office not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                };
                Invoke(action);
            }
            else
            {
                #region EXCEL GENERATION
                try
                {
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook wb = app.Workbooks.Add();
                    Excel.Worksheet ws = wb.Worksheets.Add();

                    ws.get_Range("A1").set_Value(Type.Missing, "Party Name");
                    ws.get_Range("B1").set_Value(Type.Missing, "Inv. Detail");
                    ws.get_Range("C1").set_Value(Type.Missing, "Date");
                    ws.get_Range("D1").set_Value(Type.Missing, "Days");
                    ws.get_Range("E1").set_Value(Type.Missing, "Amt");

                    int row = 1;
                    ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row).Cells.Interior.Color = System.Drawing.Color.LightGray.ToArgb();
                    DateTime today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);

                    action = () => {
                        waitingDialog.getProgressBar().Style = ProgressBarStyle.Blocks;
                        waitingDialog.getProgressBar().Maximum = reports.Count;
                        waitingDialog.getProgressBar().Value = 0;
                    };
                    Invoke(action);

                    row = 2;
                    foreach(ClientCollection client in reports)
                    {
                        int startingRow = row;
                        foreach (Payment p in client.payments)
                        {
                            DateTime date = new DateTime(p.Date.Year, p.Date.Month, p.Date.Day, 12, 0, 0);
                            double days = Math.Abs(date.Subtract(today).TotalDays);

                            ws.get_Range("A" + row).set_Value(Type.Missing, client.clientName);
                            ws.get_Range("B" + row).set_Value(Type.Missing, p.DocChqNo);
                            ws.get_Range("C" + row).set_Value(Type.Missing, p.Date.ToOADate());
                            ws.get_Range("D" + row).set_Value(Type.Missing, days.ToString("0"));
                            ws.get_Range("E" + row).set_Value(Type.Missing, p.CollectingAmount.ToString("0.00"));
                            row++;
                        }

                        ws.get_Range("A" + row).set_Value(Type.Missing, client.clientName+" Total");
                        ws.get_Range("B" + row).set_Value(Type.Missing, "");
                        ws.get_Range("C" + row).set_Value(Type.Missing, "");
                        ws.get_Range("D" + row).set_Value(Type.Missing, "");
                        ws.get_Range("E" + row).set_Value(Type.Missing, "=SUBTOTAL(9,E" + startingRow + ":E" + (row - 1) + ")");
                        ws.get_Range("A" + row + ":E" + row).Font.Bold = true;
                        row++;
                        action = () => {
                            waitingDialog.getProgressBar().Value++;
                        };
                        Invoke(action);
                    }

                    ws.get_Range("A" + row).set_Value(Type.Missing, "Grand Total");
                    ws.get_Range("B" + row).set_Value(Type.Missing, "");
                    ws.get_Range("C" + row).set_Value(Type.Missing, "");
                    ws.get_Range("D" + row).set_Value(Type.Missing, "");
                    ws.get_Range("E" + row).set_Value(Type.Missing, "=SUBTOTAL(9,E2:E" + (row - 1) + ")");
                    ws.get_Range("A" + row + ":E" + row).Font.Bold = true;

                    ws.get_Range("A1").EntireColumn.AutoFit();
                    ws.get_Range("B1").EntireColumn.AutoFit();
                    ws.get_Range("C1").EntireColumn.AutoFit();
                    ws.get_Range("C1").EntireColumn.NumberFormat = Program.SystemSDFormat;
                    ws.get_Range("D1").EntireColumn.AutoFit();
                    ws.get_Range("E1").EntireColumn.AutoFit();

                    ws.get_Range("D1").EntireColumn.NumberFormat = "0.00";
                    ws.get_Range("E1").EntireColumn.NumberFormat = "0.00";

                    bool print = false;
                    action = () =>
                    {
                        print = MessageBox.Show(this, "Are you sure to print client list now ?", "Print client list", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    };
                    Invoke(action);

                    if (print)
                    {
                        ws.PrintOutEx();
                    }

                    if (filename.Trim().Length == 0)
                    {
                        wb.Close(false);
                    }
                    else
                    {
                        wb.Close(true, filename);
                    }
                    app.Quit();
                    app.Quit();
                    app = null;

                    action = () =>
                    {
                        waitingDialog.Close();
                        waitingDialog = null;
                    };
                    Invoke(action);
                }
                catch (Exception excep)
                {
                    String err = "Unable to perform threadPrintCollectionList operation.";
                    Log.output(err, excep);
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion
            }

        }
    }
}
