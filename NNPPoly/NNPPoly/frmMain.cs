using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;
using NNPPoly.classes;
using System.Threading;
using BrightIdeasSoftware;
using NNPPoly.forms;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Net;

namespace NNPPoly
{
    public partial class frmMain : Form
    {
        private bool flagClose = false;
        public delegate void updateMethod(IWin32Window owner, String uid, String version);
        private updateMethod updater;

        private bool isFromInterestAdviseList = false;
        
        public forms.frmClient fClients = new forms.frmClient();
        public forms.frmCollectionList fCollectionList = new frmCollectionList();
        public forms.frmInterestAdvise fInterestAdvises = new frmInterestAdvise();
        public forms.frmRecords fRecords = new frmRecords();
        public forms.frmDebitNotes fDNotes = new frmDebitNotes();
        public forms.frmSchemes fSchemes = new frmSchemes();

        public const int SCREEN_CLIENTS = 0;
        public const int SCREEN_LEDGER = 1;
        public const int SCREEN_COLLECTIONLIST = 2;

        private int previousScreenCode = SCREEN_CLIENTS;

        public frmMain(updateMethod um)
        {
            InitializeComponent();
            this.updater = um;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            openClients();
        }

        public void closeNow()
        {
            flagClose = true;
            Close();
        }

        public void refreshClient()
        {
            loadClients(false);
        }

        public static void Log(String msg, Exception ex)
        {
            Job.Log(msg, ex);
        }

        private static bool flagSearching = false;
        public void loadClients(bool showWaiting=true)
        {
            Thread thread = new Thread((lv) =>
            {
                try
                {
                    if (flagSearching) return;
                    flagSearching = true;
                    Action act = () => { lvClients.Visible = false; lvClients.ClearObjects(); };
                    Invoke(act);
                    if (lvClients.Columns.Count == 0)
                    {
                        Job.Clients.generateColumns(lvClients, (OLVColumn oc) =>
                        {
                            act = () =>
                            {
                                lvClients.Columns.Add(oc);
                            };
                            Invoke(act);
                        });
                        //Job.DB.initiateDatabaseConnection();
                    }
                    Job.Clients.search(Job.Clients.CURRENT_SEARCH_FOR, Job.Clients.CURRENT_PAGE, Job.Clients.ROWS_PER_PAGE, delegate(Client c)
                    {
                        act = () =>
                        {
                            lvClients.AddObject(c);
                        };
                        Invoke(act);
                    }, true, false, 0, true);
                    act = () =>
                    {
                        if (showWaiting)
                            frmProcess.publicClose();
                        lvClients.Visible = true;
                        //lvClients.Refresh();
                    };
                    Invoke(act);
                    flagSearching = false;
                }
                catch (ThreadAbortException) { }
                catch (Exception) { }
            });
            thread.Name = "Thread: LoadClient";
            thread.Start(lvClients);
            if (showWaiting)
                new frmProcess("Loading clients", "querying database...", true, delegate(frmProcess fp)
                {
                    thread.Abort();
                    MessageBox.Show(this, "You have canceled the previous job.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }).ShowDialog(this);
        }
        
        public static String getAppUID() { return Properties.Resources.uid; }
        public static String getAppVERSION() { return Properties.Resources.version; }
        public static String getAppActivationFile() { return Job.ACTIVATION_FILE; }

        public void incomingEntries(String savepoint, bool skipConfirmation=false)
        {
            Action act = () =>
            {
                this.Activate();
                if (skipConfirmation || MessageBox.Show(this, "There is data request from upload manager of another instance of application.\nWould you like to open it now ?", "Upload Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnImports_Click(btnImports, new EventArgs(), true, savepoint);
                }
                else
                {
                    if (MessageBox.Show(this, "Are you sure to reject this incoming request ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            System.IO.File.Delete("request.mgr");
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        incomingEntries(savepoint);
                    }
                }
            };
            Invoke(act);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {


            try
            {
                Thread thread = new Thread(() =>
                {

                    try
                    {
                        WebClient wc = new WebClient();
                        String dt = wc.DownloadString("http://update.wcodez.com/updates.php?uid=" + Properties.Resources.uid + "&version=" + Properties.Resources.version);

                        if (dt != null && dt.ToString().Trim().EndsWith("1"))
                        {
                            Action act = () =>
                            {
                                updater(this, Properties.Resources.uid, Properties.Resources.version);
                                flagClose = true;
                                Application.Exit();
                            };
                            Invoke(act);
                        }
                    }
                    catch (Exception) { }

                    while (!IsDisposed)
                    {
                        try
                        {
                            if (Job.Functions.validateActivationFlag())
                            {
                                Action act = () =>
                                {
                                    closeNow();
                                };
                                Invoke(act);
                                break;
                            }
                        }
                        catch (Exception) { }
                        Thread.Sleep(5000);
                    }

                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
            }
            catch (Exception ex)
            {
                Job.Log("ActivationLoad", ex);
            }

            changeCompany();
        }

        public void changeCompany(bool autoClose=true)
        {
            if (!loadCompanies())
            {
                if (autoClose)
                    Application.Exit();
                return;
            }
            if (Job.Companies.currentCompany != null)
                Text = Job.Companies.currentCompany.name + " - WebcodeZ Infoway";
            btnCloseLedger_Click(btnCloseLedger, new EventArgs());
            loadClients();
        }

        private bool loadCompanies()
        {
            frmCompany fc = new frmCompany();
            return fc.ShowDialog(this) == DialogResult.OK;
        }

        private void btnNewClient_Click(object sender, EventArgs e)
        {
            DialogResult dr=new forms.frmNewClient((client) => {
                lvClients.AddObject(client);
                return true;
            }).ShowDialog(this);

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Job.DB.databaseConnection.ReleaseMemory();
                Job.DB.databaseConnection.Close();
            }
            catch (Exception) { }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (lvClients.Items.Count != 0)
            {
                Job.Clients.CURRENT_PAGE++;
                loadClients();
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (Job.Clients.CURRENT_PAGE > 1)
            {
                Job.Clients.CURRENT_PAGE--;
                loadClients();
            }
        }

        private void txtSearchClients_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSearchClients_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void txtSearchClients_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void lvClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnLedgerReport.Visible = lvClients.SelectedItems.Count == 1;
        }

        public void closeAllPan()
        {
            if (panClients.Visible)
            {
                previousScreenCode = SCREEN_CLIENTS;
            }
            else if (panCollectionList.Visible)
            {
                previousScreenCode = SCREEN_COLLECTIONLIST;
            }
            else if (panLedger.Visible)
            {
                previousScreenCode = SCREEN_LEDGER;
            }
            panDebitNotes.Visible = false;
            panRecords.Visible = false;
            panClients.Visible = btnNewClient.Visible = false;
            btnLedgerReport.Visible = fClients.getCurrentClient() != null;
            panLedger.Visible = btnCloseLedger.Visible = false;
            panCollectionList.Visible = false;
            panInterestAdvise.Visible = false;
            panSchemes.Visible = false;
            lvClients.HideOverlays();
        }

        public void openClients()
        {
            closeAllPan();
            //panLedger.Visible = btnCloseLedger.Visible = false;
            btnNewClient.Visible = true;
            btnLedgerReport.Visible = lvClients.SelectedItems.Count == 1;
            panClients.Dock = DockStyle.Fill;
            panClients.Visible = true;
            if (Job.Companies.currentCompany != null)
                Text = Job.Companies.currentCompany.name + " - WebcodeZ Infoway";
            else
                Text = "NNP Poly - WebcodeZ Infoway";
            lvClients.ShowOverlays();
        }

        public void openLedger(Client client=null, int year=0, int month=0)
        {
            if ((panLedger.Visible==false && lvClients.SelectedObjects.Count == 1) || client!=null)
            {
                closeAllPan();
                previousScreenCode = SCREEN_CLIENTS;
                lvClients.HideOverlays();
                //panClients.Visible = false;
                //btnNewClient.Visible = btnImports.Visible = btnLedgerReport.Visible = false;
                btnLedgerReport.Visible = true;
                btnCloseLedger.Visible = true;
                panLedger.Dock = DockStyle.Fill;
                panLedger.Visible = true;
                if (panLedger.Controls.Count == 0)
                {
                    fClients.TopLevel = false;
                    fClients.Dock = DockStyle.Fill;
                    panLedger.Controls.Add(fClients);
                    fClients.Show();
                }
                fClients.WindowState = FormWindowState.Normal;
                fClients.WindowState = FormWindowState.Maximized;
                if (client == null)
                    fClients.callClient((lvClients.SelectedObject as Client).id);
                else
                {
                    fClients.callClient(client.id, true, year, month);
                }
            }
            else if (panLedger.Visible)
            {
                fClients.callClient((lvClients.SelectedObject as Client).id);
            }
            else
            {
                MessageBox.Show(this, "Please select client profile to show client panel, please try again.", "No Client", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void openCollectionList()
        {
            if (panCollectionList.Visible) return;
            closeAllPan();
            btnCloseLedger.Visible = true;
            panCollectionList.Dock = DockStyle.Fill;
            panCollectionList.Visible = true;
            if (panCollectionList.Controls.Count == 0)
            {
                fCollectionList.TopLevel = false;
                fCollectionList.Dock = DockStyle.Fill;
                panCollectionList.Controls.Add(fCollectionList);
                fCollectionList.Show();
            }
            fCollectionList.WindowState = FormWindowState.Normal;
            fCollectionList.WindowState = FormWindowState.Maximized;
            //fClien.callClient((lvClients.SelectedObject as Client).id);
        }

        public void openInterestAdvise()
        {
            if (panInterestAdvise.Visible) return;
            closeAllPan();
            btnCloseLedger.Visible = true;
            panInterestAdvise.Dock = DockStyle.Fill;
            panInterestAdvise.Visible = true;
            if (panInterestAdvise.Controls.Count == 0)
            {
                fInterestAdvises.TopLevel = false;
                fInterestAdvises.Dock = DockStyle.Fill;
                panInterestAdvise.Controls.Add(fInterestAdvises);
                fInterestAdvises.Show();
            }
            fInterestAdvises.WindowState = FormWindowState.Normal;
            fInterestAdvises.WindowState = FormWindowState.Maximized;
            //fClien.callClient((lvClients.SelectedObject as Client).id);
        }

        public void openRecords()
        {
            if (panRecords.Visible) return;
            closeAllPan();
            btnCloseLedger.Visible = true;
            panRecords.Dock = DockStyle.Fill;
            panRecords.Visible = true;
            if (panRecords.Controls.Count == 0)
            {
                fRecords.TopLevel = false;
                fRecords.Dock = DockStyle.Fill;
                panRecords.Controls.Add(fRecords);
                fRecords.Show();
            }
            fRecords.WindowState = FormWindowState.Normal;
            fRecords.WindowState = FormWindowState.Maximized;
            fRecords.loadClients();
            //fClien.callClient((lvClients.SelectedObject as Client).id);
        }

        public void openDebitNotes()
        {
            if (panDebitNotes.Visible) return;
            closeAllPan();
            btnCloseLedger.Visible = true;
            panDebitNotes.Dock = DockStyle.Fill;
            panDebitNotes.Visible = true;
            if (panDebitNotes.Controls.Count == 0)
            {
                fDNotes.TopLevel = false;
                fDNotes.Dock = DockStyle.Fill;
                panDebitNotes.Controls.Add(fDNotes);
                fDNotes.Show();
            }
            fDNotes.WindowState = FormWindowState.Normal;
            fDNotes.WindowState = FormWindowState.Maximized;
            fDNotes.loadClients();
        }

        public void openClientForIntersetAdivseList(Client client, int year, int month)
        {
            isFromInterestAdviseList = true;
            changeClient(client,year,month);
        }

        public void openSchemes()
        {
            if (panSchemes.Visible) return;
            closeAllPan();
            btnCloseLedger.Visible = true;
            panSchemes.Dock = DockStyle.Fill;
            panSchemes.Visible = true;
            if (panSchemes.Controls.Count == 0)
            {
                fSchemes.TopLevel = false;
                fSchemes.Dock = DockStyle.Fill;
                panSchemes.Controls.Add(fSchemes);
                fSchemes.Show();
            }
            fSchemes.WindowState = FormWindowState.Normal;
            fSchemes.WindowState = FormWindowState.Maximized;
            //fSchemes.loadClients();
        }

        private void btnRecords_Click(object sender, EventArgs e)
        {
            openRecords();
        }

        private void btnCloseLedger_Click(object sender, EventArgs e)
        {
            //if (previousScreenCode == SCREEN_CLIENTS)
            if (isFromInterestAdviseList)
            {
                openInterestAdvise();
                isFromInterestAdviseList = false;
            }
            else
            {
                openClients();
            }
            //else if (previousScreenCode == SCREEN_COLLECTIONLIST)
                //openCollectionList();
            //else if (previousScreenCode == SCREEN_LEDGER)
                //openLedger();
        }

        private void btnLedgerReport_Click(object sender, EventArgs e)
        {
            if (lvClients.SelectedObjects.Count == 1)
            {
                try
                {
                    changeClient((lvClients.SelectedObjects[0] as Client));
                }
                catch (Exception ex)
                {
                    Job.Log("Error[lvClients_ENTER]", ex);
                    MessageBox.Show(this, "Unable to proceed change client request, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void lvClients_CellEditStarting(object sender, CellEditEventArgs e)
        {
            (e.RowObject as Client).SetDataReflector = true;
        }

        private void lvClients_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            //(e.RowObject as Client).SetDataReflector = false;
        }

        private void lvClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLedgerReport_Click(sender, e);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                deleteClients();
            }
        }

        private void deleteClients()
        {
            if (lvClients.SelectedObjects.Count > 0)
            {
                DialogResult dr = MessageBox.Show(this, "Are you sure to delete all selected clients with all its data ?", "Delete Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    List<long> clientIds = new List<long>();

                    foreach (classes.Client client in lvClients.SelectedObjects)
                        clientIds.Add(client.id);

                    Thread thread = new Thread(() =>
                    {
                        Action act = () =>
                        {
                            frmProcess.getInstance().pbProcess.Maximum = clientIds.Count;
                            frmProcess.getInstance().pbProcess.Value = 0;
                        };
                        Invoke(act);

                        bool flag = true;

                        foreach (long id in clientIds)
                        {
                            flag = flag && Job.Clients.delete(id);
                            act = () =>
                            {
                                frmProcess.getInstance().pbProcess.Value++;
                            };
                            Invoke(act);
                        }

                        if (flag)
                        {
                            act = () =>
                            {
                                MessageBox.Show(this, "All selected client profile is deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            Invoke(act);
                        }
                        else
                        {
                            act = () =>
                            {
                                MessageBox.Show(this, "Can't delete client profile, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            Invoke(act);
                        }

                        act = () =>
                        {
                            frmProcess.publicClose();
                        };
                        Invoke(act);

                    });
                    thread.Name = "Thread: DeleteClients";
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                    new frmProcess("Delete Clients", "Deleting...", false, (fc) => { }).ShowDialog(this);
                    loadClients();
                }
            }
        }

        public void changeClient(Client c, int year=0, int month=0)
        {
            openLedger(c, year, month);
            Text = c.name + " - " + Job.Companies.currentCompany.name;
        }

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            if (panLedger.Controls.Count == 1)
            {
                fClients.WindowState = FormWindowState.Normal;
                fClients.WindowState = FormWindowState.Maximized;
            }
            if (panCollectionList.Controls.Count == 1)
            {
                fCollectionList.WindowState = FormWindowState.Normal;
                fCollectionList.WindowState = FormWindowState.Maximized;
            }

            if (panInterestAdvise.Controls.Count == 1)
            {
                fInterestAdvises.WindowState = FormWindowState.Normal;
                fInterestAdvises.WindowState = FormWindowState.Maximized;
            }

            if (panRecords.Controls.Count == 1)
            {
                fRecords.WindowState = FormWindowState.Normal;
                fRecords.WindowState = FormWindowState.Maximized;
            }

            if (panSchemes.Controls.Count == 1)
            {
                fSchemes.WindowState = FormWindowState.Normal;
                fSchemes.WindowState = FormWindowState.Maximized;
            }
        }

        private void btnNewPaymentEntry_Click(object sender, EventArgs e)
        {
            new frmNewEntry(fClients.getCurrentClientId()).ShowDialog(this);
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void gradeSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGrades fg = new frmGrades();
            fg.ShowDialog(this);
        }

        private void lvClients_DoubleClick(object sender, EventArgs e)
        {
            if (lvClients.SelectedObjects.Count == 1)
            {
                try
                {
                    changeClient((lvClients.SelectedObjects[0] as Client));
                }
                catch (Exception ex)
                {
                    Job.Log("Error[lvClients_ENTER]", ex);
                    MessageBox.Show(this, "Unable to proceed change client request, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnChangeCompany_Click(object sender, EventArgs e)
        {
            changeCompany(false);
        }

        private void debitPrioritiesTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDebiPrioritiesAndTypes fdpt = new frmDebiPrioritiesAndTypes();
            fdpt.ShowDialog(this);
        }

        private void importFromRecoveryOldToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Title = "Import Old Recovery file...";
            od.Filter = "NNP Poly Recovery (Old)|*.npr";
            od.CheckFileExists = true;
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                String filename = od.FileName;
                Thread thread = new Thread(() =>
                {
                    try {
                        Action act = () =>
                        {

                        };

                        classes.old_version.DataFile df = classes.old_version.DataFile.Read(filename);
                        if (df == null)
                        {
                            act = () =>
                            {
                                MessageBox.Show(this, "Sorry, old data-recovery is currepted or invalid data file.", "Invalid Recovery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            Invoke(act);
                        }
                        else
                        {
                            double clientCount = 0, paymentCount = 0;

                            act = () =>
                            {
                                frmProcess.getInstance().lblMsg.Text = "Importing grades...";
                                frmProcess.getInstance().pbProcess.Maximum = df.Grades.Count;
                                frmProcess.getInstance().pbProcess.Value = 0;
                            };
                            Invoke(act);

                            foreach (classes.old_version.Grade grade in df.Grades)
                            {
                                Grade old = Job.Grades.getGradeByCode(grade.GradeName, false, Job.Companies.currentCompany.id);
                                if (old == null)
                                    Job.Grades.add(0, grade.GradeName.Trim(), grade.Amount);
                                act = () => { frmProcess.getInstance().pbProcess.Value++; };
                                Invoke(act);
                            }

                            act = () =>
                            {
                                frmProcess.getInstance().lblMsg.Text = "Importing client profiles...";
                                frmProcess.getInstance().pbProcess.Maximum = df.UserAccounts.Count;
                                frmProcess.getInstance().pbProcess.Value = 0;
                            };
                            Invoke(act);

                            foreach (classes.old_version.UserAccount ua in df.UserAccounts)
                            {
                                long clientId = 0;
                                if (ua.emailAddress.Trim().Length == 0) ua.emailAddress = "fakemail@domain.com";
                                if (ua.mobileNumber.Trim().Length == 0) ua.mobileNumber = "9999999999";
                                if (Job.Clients.add(ref clientId, ua.ClientName, ua.mobileNumber, ua.emailAddress, ua.ClientDescription, ua.FooText, ua.OBType, ua.OpeningBalance, new List<double>() { ua.InterestRate1, ua.InterestRate2 }, new List<long>() { 0, (long)ua.CutOffDays }, (long)ua.LessDays))
                                {
                                    foreach (classes.old_version.Payment pay in ua.Payments)
                                    {
                                        long paymentId = 0;
                                        double mt = 0;
                                        double.TryParse(pay.MT, out mt);
                                        Grade grade = pay.Grade.Trim().Length == 0 ? null : Job.Grades.getGradeByCode(pay.Grade, false,Job.Companies.currentCompany.id);
                                        long gradeId = 0;
                                        if (grade != null)
                                            gradeId = grade.id;
                                        if (Job.Payments.add(ref paymentId, clientId, new DateTime(pay.Date.Year, pay.Date.Month, pay.Date.Day, 12, 0, 0), pay.DocChqNo, pay.Type, pay.Particulars, pay.Credit > 0 ? Payment.PaymentMode.Credit : Payment.PaymentMode.Debit, pay.Credit > 0 ? pay.Credit : pay.Debit, mt, gradeId, 0))
                                        {
                                            paymentCount++;
                                        }
                                    }
                                    clientCount++;
                                }
                                act = () =>
                                {
                                    frmProcess.getInstance().pbProcess.Value++;
                                };
                                Invoke(act);
                            }


                            act = () =>
                            {
                                frmProcess.getInstance().lblMsg.Text = "Importing debit priorities and types...";
                                frmProcess.getInstance().pbProcess.Maximum = df.SpecialTypes.Count + df.PriorityTypes.Count;
                                frmProcess.getInstance().pbProcess.Value = 0;
                            };
                            Invoke(act);

                            foreach (String stype in df.SpecialTypes)
                            {
                                if (!Job.PrioritiesAndTypes.find(stype, true))
                                    Job.PrioritiesAndTypes.add(stype, true);
                                act = () =>
                                {
                                    frmProcess.getInstance().pbProcess.Value++;
                                };
                                Invoke(act);
                            }

                            foreach (String stype in df.PriorityTypes)
                            {
                                if (!Job.PrioritiesAndTypes.find(stype, false))
                                    Job.PrioritiesAndTypes.add(stype, false);
                                act = () =>
                                {
                                    frmProcess.getInstance().pbProcess.Value++;
                                };
                                Invoke(act);
                            }



                            act = () =>
                            {
                                MessageBox.Show(this, "Total " + clientCount + " client(s) and " + paymentCount + " payment(s) are imported.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            Invoke(act);
                        }

                        act = () =>
                        {

                            frmProcess.publicClose();
                        };
                        Invoke(act);
                    }
                    catch (Exception) { }
                });
                thread.Name = "Thread: ImportFromOldRecovery";
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                new frmProcess("Importing company profile...", "Using old recovery format", false, (c) => { thread.Abort(); }).ShowDialog(this);
                loadClients();
            }
        }

        private void btnCollectionList_Click(object sender, EventArgs e)
        {
            openCollectionList();
        }

        private void btnInterestAdviseList_Click(object sender, EventArgs e)
        {
            openInterestAdvise();
        }

        private void btnImports_Click(object sender, EventArgs e)
        {
            btnImports_Click(sender, e, true, "default");
        }

        private void btnImports_Click(object sender, EventArgs e, bool isMaster=true,String savePoint="default")
        {
            frmUploadMgr um = new frmUploadMgr(isMaster);
            um.setSavePoint(savePoint);
            um.ShowDialog(this);
            if (um.isUpdated)
            {
                openClients();
                loadClients();
            }
        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updater(this, Properties.Resources.uid, Properties.Resources.version);
        }

        private void importGradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Title = "Import Old Recovery file...";
            od.Filter = "NNP Poly Recovery (Old)|*.npr";
            od.CheckFileExists = true;
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                String filename = od.FileName;
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        Action act = () =>
                        {

                        };

                        classes.old_version.DataFile df = classes.old_version.DataFile.Read(filename);
                        if (df == null)
                        {
                            act = () =>
                            {
                                MessageBox.Show(this, "Sorry, old data-recovery is currepted or invalid data file.", "Invalid Recovery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            Invoke(act);
                        }
                        else
                        {
                            act = () =>
                            {
                                frmProcess.getInstance().pbProcess.Maximum = df.Grades.Count;
                                frmProcess.getInstance().pbProcess.Value = 0;
                            };
                            Invoke(act);


                            foreach (classes.old_version.Grade grade in df.Grades)
                            {
                                Grade old = Job.Grades.getGradeByCode(grade.GradeName, false, Job.Companies.currentCompany.id);
                                if (old == null)
                                    Job.Grades.add(0, grade.GradeName.Trim(), grade.Amount);
                                act = () =>
                                {
                                    frmProcess.getInstance().pbProcess.Value++;
                                };
                                Invoke(act);
                            }


                            act = () =>
                            {
                                MessageBox.Show(this, "All grades are imported.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            Invoke(act);
                        }

                        act = () =>
                        {

                            frmProcess.publicClose();
                        };
                        Invoke(act);
                    }
                    catch (Exception) { }
                });
                thread.Name = "Thread: ImportFromOldRecovery";
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                new frmProcess("Importing company profile...", "Using old recovery format", false, (c) => { thread.Abort(); }).ShowDialog(this);
                loadClients();
            }
        }

        private void printFormatSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrintFormatSettings f = new frmPrintFormatSettings();
            f.ShowDialog(this);
        }

        private void companyProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNewCompany comp = new frmNewCompany();
            comp.setEditMode(Job.Companies.currentCompany.id);
            comp.ShowDialog(this);
        }

        private void btnDNotesAdvises_Click(object sender, EventArgs e)
        {
            openDebitNotes();
        }

        private void exportPaymentTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Save template file";
            sd.Filter = "Excel Files|*.xls;*.xlsx|Macro-Excel files|*.xlsm";
            if (sd.ShowDialog(this) == DialogResult.OK)
            {
                String fname = sd.FileName;
                System.IO.File.WriteAllBytes(fname, Properties.Resources.PaymentEntryTemplateMacroEnabled);
                Thread thread = new Thread(() =>
                {
                    #region thread-code

                    try
                    {
                        String filename = fname.ToString();
                        int count = 0;

                        Excel.Application app = new Excel.Application();
                        Excel.Workbook wb = app.Workbooks.Open(fname);
                        Excel.Worksheet ws = wb.Worksheets[1];

                        StringBuilder stringBuilder = new StringBuilder();
                        int flag=0;
                        Job.Clients.search("", 0, 0, (Client c) =>
                        {
                            ws.get_Range("Z" + (++flag)).set_Value(Type.Missing, c.name);
                        });

                        ws.get_Range("Z1").EntireColumn.Name = "ClientNames";
                        ws.get_Range("Z1").EntireColumn.Hidden = true;
                        ws.get_Range("A1").EntireColumn.Validation.Add(Excel.XlDVType.xlValidateList, Type.Missing, Excel.XlFormatConditionOperator.xlBetween, "=ClientNames");

                        wb.Close(true);
                        app.Quit();
                        app.Quit();
                        app = null;
                        Action act = () => { frmProcess.publicClose(); };
                        Invoke(act);
                    }
                    catch (Exception excep)
                    {
                        String err = "Unable to perform threadExporter_main operation.";
                        Job.Log(err, excep);
                        MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    #endregion
                });
                thread.Start();
                new frmProcess("Exporting template...", "Creating data validation list...", true, (fc) => { }).ShowDialog(this);
            }
        }
        private void __threadExportEntries(object fname)
        {
            
        }

        private void aboutNNPPolymersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new frmSplash(false).ShowDialog(this);
        }

        private void sMSSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new forms.frmSMSSettings().ShowDialog(this);
        }

        private void emailSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new forms.frmEmailSettings().ShowDialog(this);
        }

        private void requestOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                List<MessageHolder> holders = new List<MessageHolder>();
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    Payment p = Job.Payments.getLastPayment(c.id, Payment.PaymentMode.Debit, "sale");
                    if (p != null)
                    {
                        DateTime curDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);
                        double totalDays = curDate.Subtract(p.date).TotalDays;
                        if (totalDays > double.Parse(Job.GeneralSettings.sms_nod_requestorder()))
                        {
                            MessageHolder holder = Job.Messages.prepareRequestOrder(c.id, (long)totalDays);
                            if (holder != null)
                                holders.Add(holder);
                        }
                    }
                });

                Action act = () =>
                {
                    if (holders.Count == 0)
                    {
                        MessageBox.Show(this, "No client(s) founds who has no debits since last " + Job.GeneralSettings.sms_nod_requestorder() + " days...", "Zero Clients", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        frmProcess.getInstance().lblMsg.Text = "Opening Message Sender...";
                        new forms.frmMessageSender(holders).ShowDialog(this);
                    }
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Searching for debit entries...", "", true, (fc) => { }).ShowDialog();
        }

        private void lvClients_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void lvClients_RightMouseDown(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;

            if (mi.Text.StartsWith("&Send Collection"))
            {
                if (lvClients.SelectedObjects.Count > 0)
                {
                    List<MessageHolder> holders = new List<MessageHolder>();

                    foreach (classes.Client client in lvClients.SelectedObjects)
                    {
                        if (client != null)
                        {
                            classes.collection_list.Collection c = new classes.collection_list.Collection();
                            c.client = client;
                            c.CollectingAmount = client.cbalance;
                            c.Rows = null;

                            MessageHolder holder = Job.Messages.prepareCollection(c);
                            if (holder != null)
                            {
                                holders.Add(holder);
                            }

                        }
                    }

                    new forms.frmMessageSender(holders).ShowDialog(this);
                }
            }
            else if (mi.Text.StartsWith("&Delete clients"))
            {
                deleteClients();
            }
            else if (mi.Text.StartsWith("&Edit client"))
            {
                if (lvClients.SelectedObjects.Count == 1)
                {
                    Client c = lvClients.SelectedObjects[0] as Client;
                    if(c!=null)
                    {
                        forms.frmNewClient nc = new frmNewClient((Client cc) =>
                        {
                            lvClients.RefreshObject(cc);
                            return true;
                        });
                        nc.setEditMode(ref c);
                        nc.ShowDialog(this);
                    }
                }
            }
            else if (mi.Text.StartsWith("&Print Envelope"))
            {
                if (lvClients.SelectedObjects.Count > 0)
                {
                    List<long> ids = new List<long>();

                    foreach (classes.Client cl in lvClients.SelectedObjects)
                    {
                        if (!ids.Contains(cl.id))
                        {
                            ids.Add(cl.id);
                        }
                    }

                    forms.frmEnvelopePrinter ep = new frmEnvelopePrinter(ids);
                    ep.ShowDialog(this);
                }
            }
        }

        private void lvClients_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lvClients.SelectedObjects.Count > 0)
            {
                ContextMenu cm = new ContextMenu();
                if (lvClients.SelectedObjects.Count == 1)
                    cm.MenuItems.Add(new MenuItem("&Edit client profile", lvClients_RightMouseDown));
                cm.MenuItems.Add(new MenuItem("&Delete clients", lvClients_RightMouseDown));
                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Print Envelope", lvClients_RightMouseDown));
                cm.MenuItems.Add(new MenuItem("&Send Collection Message", lvClients_RightMouseDown));
                cm.Show(sender as Control, e.Location);
            }
        }

        private void holidaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new forms.frmHolidays().ShowDialog(this);
        }

        private void btnPrintClientlist_Click(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Save template file";
            sd.Filter = "Excel Files|*.xls;*.xlsx";
            if (sd.ShowDialog(this) == DialogResult.OK)
            {
                String fname = sd.FileName;

                try
                {
                    if (System.IO.File.Exists(fname))
                    {
                        System.IO.File.Delete(fname);
                    }
                }
                catch (Exception) { }

                Thread thread = new Thread(() =>
                {
                    #region thread-code

                    try
                    {
                        String filename = fname.ToString();

                        Excel.Application app = new Excel.Application();
                        Excel.Workbook wb = app.Workbooks.Add();
                        Excel.Worksheet ws = wb.Worksheets.Add();

                        ws.get_Range("A1").set_Value(Type.Missing, "ID");
                        ws.get_Range("B1").set_Value(Type.Missing, "Client Name");
                        ws.get_Range("C1").set_Value(Type.Missing, "Opening Balance");
                        ws.get_Range("D1").set_Value(Type.Missing, "Closing Balance");

                        int row = 1;
                        ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row + ":F" + row + ":G" + row).Cells.Interior.Color = System.Drawing.Color.LightGray.ToArgb();

                        row++;
                        Job.Clients.search("", 0, 0, (Client c) =>
                        {
                            ws.get_Range("A" + row).set_Value(Type.Missing, c.id);
                            ws.get_Range("B" + row).set_Value(Type.Missing, c.name);
                            ws.get_Range("C" + row).set_Value(Type.Missing, Job.Functions.AmountToString(c.obalance));
                            ws.get_Range("D" + row).set_Value(Type.Missing, Job.Functions.AmountToString(c.cbalance));
                            row++;
                        }, true, false, 0, true);

                        ws.get_Range("A1").EntireColumn.AutoFit();
                        ws.get_Range("B1").EntireColumn.AutoFit();
                        ws.get_Range("C1").EntireColumn.AutoFit();
                        ws.get_Range("D1").EntireColumn.AutoFit();

                        ws.get_Range("C1").EntireColumn.NumberFormat = "0.00";
                        ws.get_Range("D1").EntireColumn.NumberFormat = "0.00";

                        Decimal copies = 1;
                        Action act = () =>
                        {
                            frmProcess.getInstance().lblMsg.Text = "Printing...";
                            frmAskUser ask = new frmAskUser("No of copies of client list ?", "0 means no printing", "1", frmAskUser.ValueType.Long);
                            ask.ShowDialog(this);
                            Decimal.TryParse(ask.getText(), out copies);
                        };
                        Invoke(act);

                        if (copies > 0)
                        {
                            wb.PrintOutEx(Type.Missing, Type.Missing, (int)copies);
                        }

                        wb.Close(true, filename);
                        app.Quit();
                        app.Quit();
                        app = null;
                        act = () => { frmProcess.publicClose(); };
                        Invoke(act);
                    }
                    catch (Exception excep)
                    {
                        String err = "Unable to perform threadExporter_main operation.";
                        Job.Log(err, excep);
                        MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    #endregion
                });
                thread.Start();
                new frmProcess("Exporting client list...", "Creating list...", true, (fc) => { }).ShowDialog(this);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new forms.frmSendCustomMessages().ShowDialog(this);
        }

        private void changeDatabaseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new forms.frmDatabaseSelection().ShowDialog(this);
        }

        private void btnRefreshClients_Click(object sender, EventArgs e)
        {
            loadClients();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagClose) return;

            if (MessageBox.Show(this, "Are you sure to quit application ?", "Close Application ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;

            if (!e.Cancel)
            {

                Thread thread = new Thread(() =>
                {
                    Action act = () => { };

                    String filePath = Job.DB.getDatabaseHolder();
                    if (filePath != null)
                    {
                        try
                        {
                            System.IO.File.Copy(filePath, "DB_BACKUP_" + Properties.Resources.uid + ".db", true);
                        }
                        catch (Exception ex) {
                            Job.Log("AutoBackup", ex);
                        }
                    }
                    else
                    {
                        act = () =>
                        {
                            MessageBox.Show(this, "Unable to create database backup due to database missing error.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        };
                        Invoke(act);
                    }

                    act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);

                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();

                new frmProcess("Creating database backup...", "Please wait...", true, (fc) => { }).ShowDialog(this);

            }
        }

        private void gradegToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forms.frmGradeGroups fgg = new frmGradeGroups();
            fgg.ShowDialog(this);
        }

        private void btnSchemes_Click(object sender, EventArgs e)
        {
            openSchemes();
        }

    }
}
