using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPoly
{
    public partial class InterestAdviseList : Form
    {
        private int month=-1, year=-1;
        private WaitingDialog waitingDialog = null;
        private bool ignoreZeroAmt = false, ignoreZeroIntAmt = true;

        public InterestAdviseList(int month,int year)
        {
            InitializeComponent();
            this.month = month;
            this.year = year;
            chkZeroInt.Checked = ignoreZeroIntAmt;
            Text += " [ " + this.month + "/" + this.year + " ]";
        }

        private void InterestAdviseList_Load(object sender, EventArgs e)
        {

        }

        private void InterestAdviseList_Shown(object sender, EventArgs e)
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            Thread thread = new Thread(new ThreadStart(threadScanAllReports));
            thread.Name = "Thread: ScanAllReports - List Interest Advise";
            thread.Priority = ThreadPriority.Highest;
            waitingDialog = new WaitingDialog();
            waitingDialog.Text = "Scanning reports ...";
            thread.Start();
            waitingDialog.ShowDialog(this);
        }

        private void threadScanAllReports()
        {
            try
            {
                Action action;
                int selMonth=-1, selYear=-1;
                action = () => {
                    lv.Items.Clear();
                    selMonth = month;
                    selYear = year;
                };
                Invoke(action);

                Panel myPanel = new Panel();
                foreach (UserAccount client in Datastore.dataFile.UserAccounts)
                {
                    Datastore.current = client;
                    GeneralReport genReport = new GeneralReport(selMonth, selYear);
                    genReport.isWaitingEnabled = false;
                    genReport.amountDone += genReport_amountDone;
                    genReport.TopLevel = false;
                    myPanel.Controls.Clear();
                    myPanel.Controls.Add(genReport);
                    genReport.Show();
                }
                Datastore.current = null;

                action = () => { 
                    waitingDialog.Close();
                    waitingDialog = null;
                };
                Invoke(action);
            }
            catch (Exception ex)
            {
                String err = "Unable to perform ScanAllReport[InterestAdviseList] operation.";
                Log.output(err, ex);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void genReport_amountDone(decimal clientID, double amtDue, double intDue)
        {
            Action action = () => {
                UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == clientID));
                if (client != null)
                {
                    bool flag = true;

                    if (ignoreZeroAmt)
                    {
                        if (amtDue == 0)
                        {
                            flag = false;
                        }
                    }

                    if (ignoreZeroIntAmt)
                    {
                        if (intDue == 0)
                        {
                            flag = false;
                        }
                    }

                    if (flag)
                    {
                        ListViewItem li = new ListViewItem(new String[] { client.ClientName, "Disabled", amtDue.ToString("0.00"), intDue.ToString("0.00") });
                        li.Tag = clientID;
                        lv.Items.Add(li);
                    }
                }
            };
            Invoke(action);
        }

        private void chkZeroInt_CheckedChanged(object sender, EventArgs e)
        {
            ignoreZeroIntAmt = chkZeroInt.Checked;
            InterestAdviseList_Shown(this, new EventArgs());
        }

        private void chkZeroAmt_CheckedChanged(object sender, EventArgs e)
        {
            ignoreZeroAmt = chkZeroAmt.Checked;
            InterestAdviseList_Shown(this, new EventArgs());
        }

        private void lv_DoubleClick(object sender, EventArgs e)
        {
            if (lv.SelectedItems.Count == 1)
            {
                Decimal clientID = (Decimal)lv.SelectedItems[0].Tag;
                UserAccount client = Datastore.dataFile.UserAccounts.Find(x => (x.ID == clientID));
                if (client != null)
                {
                    Datastore.current = client;
                    GeneralReport genReport = new GeneralReport(month, year);
                    genReport.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                    genReport.Text = client.ClientName+" - Monthly Report";
                    genReport.ShowDialog(this);
                    Datastore.current = null;
                }
            }
        }

    }
}
