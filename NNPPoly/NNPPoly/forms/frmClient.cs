using BrightIdeasSoftware;
using NNPPoly.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly.forms
{
    public partial class frmClient : Form
    {
        private bool isReady = false;
        private Client curClient = null;
        private int curMonth = DateTime.Now.Month, curYear = DateTime.Now.Year;
        private bool flagSearching;

        // forms used
        private forms.reports.frmReport1 report1=null;

        public frmClient()
        {
            InitializeComponent();
        }


        private void frmClient_Shown(object sender, EventArgs e)
        {
            initReport1();
        }

        #region General

        public long getCurrentClientId() { return curClient == null ? -1 : curClient.id; }

        public Client getCurrentClient() { return curClient; }

        public void setCurrentClient(Client c) { curClient = c; }

        public void callClient(long clientId)
        {
            callClient(clientId, true);
        }

        public void callClient(long clientId, bool isReloadClients, int year=0, int month=0)
        {
            isReady = false;
            if (report1 != null)
                report1.debitCount = report1.mtCount = report1.cdCount = report1.interestCount = report1.interestDue = report1.takenAmountCount = report1.amountDue = 0;
            Thread thread = new Thread(() => {
                Action act = () => { };
                curClient = Job.Clients.get(clientId);
                if (curClient == null)
                {
                    act = () => {
                        MessageBox.Show(this, "Client not found, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                    Invoke(act);
                    return;
                }
                refreshClientDetails();
                if (isReloadClients)
                    reloadClients();

                // monthly report
                act = () => {
                    frmProcess.publicClose();
                };
                Invoke(act);
                isReady = true;
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Name = "Thread: callClient";
            thread.Start();
            new frmProcess("Processing", "client details...", true, (t) => { }).ShowDialog(this);
            if (year == 0)
                year = DateTime.Now.Year;
            if (month == 0)
                month = DateTime.Now.Month;

            curYear = year;
            curMonth = month;
            disableMonthButton(curMonth);
            dtReportYear.Value = new DateTime(curYear, curMonth, 1);
            Job.Payments.CURRENT_PAGE = 1;
            Job.Payments.CURRENT_SEARCH_FOR = "";
            loadPayments();
            txtCutOffDays.Text = curClient.cutoffdays+"";
            txtIntRate1.Text = curClient.intrate1+"";
            txtIntRate2.Text = curClient.intrate2+"";
            txtLessDays.Text = curClient.lessdays + "";
            btnShowReport1_Click(btnShowReport1, new EventArgs());

        }

        public void changeYear(int year)
        {
            dtReportYear.Value = new DateTime(year, curMonth, 1, 12, 0, 0);
        }

        public void disableMonthButton(int month)
        {
            enableAllMonths();
            curMonth = month;
            switch (month)
            {
                case 1:
                    btnJan.Enabled = false;
                    break;
                case 2:
                    btnFeb.Enabled = false;
                    break;
                case 3:
                    btnMar.Enabled = false;
                    break;
                case 4:
                    btnApr.Enabled = false;
                    break;
                case 5:
                    btnMay.Enabled = false;
                    break;
                case 6:
                    btnJun.Enabled = false;
                    break;
                case 7:
                    btnJul.Enabled = false;
                    break;
                case 8:
                    btnAug.Enabled = false;
                    break;
                case 9:
                    btnSep.Enabled = false;
                    break;
                case 10:
                    btnOct.Enabled = false;
                    break;
                case 11:
                    btnNov.Enabled = false;
                    break;
                case 12:
                    btnDec.Enabled = false;
                    break;
            }
        }

        public void refreshClientDetails()
        {
            if (curClient == null) return;
            Action act = () => {
                lblClientName.Text = curClient.name;
            };
            Invoke(act);
        }

        public void reloadClients()
        {
            Thread thread = new Thread(() =>
            {
                int curItem = -1;
                Action act = () => { cmbClients.Items.Clear(); };
                Invoke(act);
                Job.Clients.search("", 0, 0, (c) => {
                    act = () => {
                        int i = cmbClients.Items.Add(new ComboItem((c as Client).name, (c as Client).id));
                        if (curClient != null && (c as Client).id == curClient.id)
                            curItem = i;
                    };
                    Invoke(act);
                });
                act = () => {
                    isReady = false;
                    cmbClients.SelectedIndex = curItem;
                    isReady = true;
                };
                Invoke(act);
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }

        private void cmbClients_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lblClientName_DoubleClick(object sender, EventArgs e)
        {

        }

        private void lblClientName_Click(object sender, System.EventArgs e)
        {
           
        }

        private void ClientEditClick(object sender, EventArgs e)
        {
            frmNewClient nc = new frmNewClient(null);
            nc.setEditMode(ref curClient);
            if (nc.ShowDialog(this) == DialogResult.OK)
            {
                refreshClientDetails();
            }
        }

        private void btnJan_Click(object sender, EventArgs e)
        {
            disableMonthButton(int.Parse((sender as Button).Tag.ToString()));
        }

        private void enableAllMonths()
        {
            btnJan.Enabled =
                btnFeb.Enabled =
                btnMar.Enabled =
                btnApr.Enabled =
                btnMay.Enabled =
                btnJul.Enabled =
                btnJun.Enabled =
                btnAug.Enabled =
                btnSep.Enabled =
                btnOct.Enabled =
                btnNov.Enabled =
                btnDec.Enabled = true;
        }

        private void cmbClients_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbClients.SelectedIndex > -1 && isReady)
            {
                callClient((long)(cmbClients.SelectedItem as ComboItem).Value, false);
            }
        }

        #endregion

        #region Ledger Report

        private void lvPayments_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            (e.RowObject as Payment).SetDataReflector = true;
        }

        private void txtSearchPayments_TextChanged(object sender, EventArgs e)
        {
            if (txtSearchPayments.Text.Trim().Length == 0)
            {
                Job.Clients.CURRENT_SEARCH_FOR = "";
                loadPayments();
            }
        }

        public void loadPayments(bool showWaiting)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    if (flagSearching || curClient == null || !isReady) return;
                    flagSearching = true;

                    List<Payment> payments = new List<Payment>();

                    Action act = () => { lvPayments.Visible = false; lvPayments.ClearObjects(); };
                    Invoke(act);

                    if (lvPayments.Columns.Count == 0)
                    {
                        Job.Payments.generateColumns(lvPayments, (BrightIdeasSoftware.OLVColumn c) =>
                        {
                            act = () =>
                            {
                                lvPayments.Columns.Add(c);
                            };
                            Invoke(act);
                        });
                    }
                    Client client = null;
                    act = () => { client = getCurrentClient(); };
                    Invoke(act);

                    double initialBalance = 0;

                    if (client != null)
                    {
                        if (client.obalance_type == Client.OpeningBalanceType.Debit && client.obalance != 0)
                        {
                            Payment currentDebit = new Payment(0);
                            currentDebit.SetInitMode = true;

                            currentDebit.client_id = client.id;
                            currentDebit.amount = client.obalance;
                            currentDebit.grade = Job.Grades.getGrade(0, false);
                            currentDebit.invoice = "Opening Balance";
                            currentDebit.mode = Payment.PaymentMode.Debit;
                            currentDebit.mt = 0;
                            currentDebit.particulars = "";
                            currentDebit.remainBalance = client.obalance;
                            currentDebit.type = "Sale";
                            DateTime mdate = Job.Payments.getMinimumDateOf(client.id, Payment.PaymentMode.Debit);
                            currentDebit.date = new DateTime(mdate.Year, 4, 1, 12, 0, 0);
                            currentDebit.SetInitMode = false;
                            initialBalance = client.obalance;
                            currentDebit.closing_balance = initialBalance;
                            payments.Add(currentDebit);
                        }
                        else if (client.obalance_type == Client.OpeningBalanceType.Credit && client.obalance != 0)
                        {
                            Payment currentDebit = new Payment(0);
                            currentDebit.SetInitMode = true;

                            currentDebit.client_id = client.id;
                            currentDebit.amount = client.obalance;
                            currentDebit.grade = Job.Grades.getGrade(0, false);
                            currentDebit.invoice = "Opening Balance";
                            currentDebit.mode = Payment.PaymentMode.Credit;
                            currentDebit.mt = 0;
                            currentDebit.particulars = "";
                            currentDebit.remainBalance = client.obalance;
                            currentDebit.type = "Sale";
                            DateTime mdate = Job.Payments.getMinimumDateOf(client.id, Payment.PaymentMode.Credit);
                            currentDebit.date = new DateTime(mdate.Year, 4, 1, 12, 0, 0);
                            currentDebit.SetInitMode = false;
                            initialBalance = 0 - client.obalance;
                            currentDebit.closing_balance = initialBalance;
                            payments.Add(currentDebit);

                        }
                    }

                    Job.Payments.search(curClient.id, Job.Payments.CURRENT_SEARCH_FOR, (Payment ob) =>
                    {
                        payments.Add(ob);
                    }, initialBalance);

                    act = () =>
                    {
                        lvPayments.SetObjects(payments);
                        if (showWaiting)
                            frmProcess.publicClose();
                        lvPayments.Visible = true;
                    };
                    Invoke(act);
                    flagSearching = false;
                }
                catch (ThreadAbortException) { }
            });
            thread.Name = "Thread: LoadPayments";
            thread.Start();
            if (showWaiting)
                new frmProcess("Loading payments", "querying database...", false, delegate(frmProcess fp)
                {
                    thread.Abort();
                    MessageBox.Show(this, "You have canceled the previous job.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }).ShowDialog(this);
        }

        public void loadPayments()
        {
            loadPayments(true);
        }

        private void txtSearchPayments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtSearchPayments.Text.ToLower().Trim().Equals(Job.Payments.CURRENT_SEARCH_FOR.ToLower().Trim()))
                    Job.Payments.CURRENT_PAGE = 1;
                Job.Payments.CURRENT_SEARCH_FOR = txtSearchPayments.Text.Trim();
                loadPayments();
            }
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (lvPayments.Items.Count != 0)
            {
                Job.Payments.CURRENT_PAGE++;
                loadPayments();
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (Job.Payments.CURRENT_PAGE > 1)
            {
                Job.Payments.CURRENT_PAGE--;
                loadPayments();
            }
        }

        private void lvPayments_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (lvPayments.SelectedItems.Count > 0)
            {
                ContextMenu cm = new ContextMenu();
                if (lvPayments.SelectedItems.Count == 1)
                    cm.MenuItems.Add(new MenuItem("&Edit selected payment", lv_RightClickAction));
                cm.MenuItems.Add(new MenuItem("&Delete selected payments", lv_RightClickAction));

                MenuItem hmenu = new MenuItem("Highlights >>");
                hmenu.MenuItems.Add(new MenuItem("&Add to Highlights", lv_RightClickAction));
                hmenu.MenuItems.Add(new MenuItem("&Remove from Highlights", lv_RightClickAction));
                hmenu.MenuItems.Add(new MenuItem("&Clear All Highlights", lv_RightClickAction));
                cm.MenuItems.Add(hmenu);

                if (lvPayments.SelectedItems.Count > 1)
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

        private void setValues(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                List<String> ids = new List<String>();
                foreach (Payment p in lvPayments.SelectedObjects)
                {
                    //Payment p = (Payment)li.RowObject;
                    if (p.id != 0)
                        ids.Add(p.id.ToString());
                }
                frmSetValues fsv = new frmSetValues(ids.ToArray<String>(), mi.Text);
                if (fsv.ShowDialog(this) == DialogResult.OK)
                {
                    loadPayments();
                }

            }
            catch (Exception excep)
            {
                String err = "Unable to perform set_values operation.";
                Job.Log(err, excep);
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
                    if (lvPayments.SelectedObjects.Count != 1) return;
                    frmNewEntry fne = new frmNewEntry(curClient.id);
                    Payment p = (Payment)lvPayments.SelectedObject;
                    fne.setEditMode(ref p);
                    if (fne.ShowDialog(this) == DialogResult.Ignore)
                    {
                        loadPayments();
                    }
                }
                else if (mi.Text.StartsWith("&Delete"))
                {
                    DialogResult dr = MessageBox.Show(this, "Are you sure to delete all selected payment permanent ?", "Delete payments", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        int deleted = 0;
                        foreach (Payment p in lvPayments.SelectedObjects)
                        {
                            
                                deleted += p.Delete() ? 1 : 0;
                            
                        }
                        MessageBox.Show(this, "Total " + deleted + " payments are deleted permanently.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadPayments(true);
                    }
                }
                else if (mi.Text.StartsWith("&Add to Highlights"))
                {
                    foreach (Payment p in lvPayments.SelectedObjects)
                    {
                        if (p != null)
                        {
                            p.SetDataReflector = true;
                            p.highlighted = true;
                        }
                    }
                }
                else if (mi.Text.StartsWith("&Remove from Highlights"))
                {
                    foreach (Payment p in lvPayments.SelectedObjects)
                    {
                        if (p != null)
                        {
                            p.SetDataReflector = true;
                            p.highlighted = false;
                        }
                    }
                }
                else if (mi.Text.StartsWith("&Clear All Highlights"))
                {
                    bool b = Job.DB.executeQuery("update payment set payment_highlighted=0 where payment_client_id=@cid", new System.Data.SQLite.SQLiteParameter[] { new System.Data.SQLite.SQLiteParameter("@cid", curClient.id) }) > 0;
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform rightclick_on_displayLV operation.";
                Job.Log(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(__threadImportEntries));
                th.Name = "PaymentImporter";
                th.Start(od.FileName);
                new frmProcess("Importing entries", "Reading spread-sheet...", true, (c) => { 
                    
                }).ShowDialog(this);
            }
        }

        private void __threadImportEntries(object fname)
        {
            try
            {
                String log = "";
                String filename = fname.ToString();
                int count = 0;
                
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Open(filename);

                foreach (Excel.Worksheet ws in wb.Worksheets)
                {
                    Excel.Range range = ws.UsedRange;
                    for (int i = 0; i < range.Rows.Count; i++)
                    {
                        Payment pay = new Payment(0);
                        int cr = i + 1;
                        object odt, odocno, otype, opart, ocredit, odebit;
                        odt = range.get_Range("A" + cr).get_Value();
                        odocno = range.get_Range("B" + cr).Value2;
                        otype = range.get_Range("C" + cr).Value2;
                        opart = range.get_Range("D" + cr).Value2;
                        ocredit = range.get_Range("E" + cr).Value2;
                        odebit = range.get_Range("F" + cr).Value2;

                        if (odocno != null && odebit != null)
                        {
                            String docno = odocno == null ? "" : odocno.ToString();
                            String debit = odebit == null ? "" : odebit.ToString();

                            if (docno.ToLower().Contains("opening balance") && odebit != null)
                            {
                                double dd = -1;
                                if (double.TryParse(debit, out dd))
                                {
                                    curClient.SetDataReflector = true;
                                    curClient.obalance = dd;
                                }
                                continue;
                            }
                        }

                        if (odt != null && otype != null && opart != null && !(ocredit == null && odebit == null))
                        {
                            String date = odt.ToString();
                            String docno = odocno == null ? "" : odocno.ToString();
                            String type = otype.ToString();
                            String part = opart.ToString();
                            String credit = ocredit == null ? "0" : ocredit.ToString();
                            String debit = odebit == null ? "0" : odebit.ToString();

                            DateTime dt;
                            if (DateTime.TryParse(date, out dt))
                            {
                                //dt = DateTime.ParseExact(dt.ToString("dd-MM-yyyy"),"dd-MM-yyyy",Program.provider);
                                if (!(credit.Length == 0 && debit.Length == 0))
                                {
                                    double amt = 0;
                                    if (!credit.Equals("0") && double.TryParse(credit, out amt))
                                    {
                                        /*pay.Credit = amt;
                                        pay.Remain = amt;*/
                                        pay.amount = amt;
                                        pay.mode = Payment.PaymentMode.Credit;
                                    }
                                    else if (!debit.Equals("0") && double.TryParse(debit, out amt))
                                    {
                                        pay.amount = amt;
                                        pay.mode = Payment.PaymentMode.Debit;
                                        if (part.Trim().LastIndexOf(" ") > -1)
                                        {
                                            String mtnumber = part.Trim().Substring(part.LastIndexOf(" ") + 1);
                                            double tempNo = 0;
                                            if (double.TryParse(mtnumber, out tempNo))
                                                pay.mt = tempNo;
                                            else
                                                pay.mt = 0;
                                        }
                                        else
                                        {
                                            pay.mt = 0;
                                        }
                                        //pay.Remain = 0;
                                    }

                                    pay.date = new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0);
                                    pay.type = type;
                                    pay.particulars = part;
                                    pay.invoice = docno;
                                    long t=0;
                                    Job.Payments.add(ref t, curClient.id, pay.date, pay.invoice, pay.type, pay.particulars, pay.mode, pay.amount, pay.mt, 0, 0);
                                    count++;
                                }
                                else
                                {
                                    //log += Environment.NewLine + "ERR2";
                                }
                            }
                            else
                            {
                                //log += Environment.NewLine + "ERR1";
                            }
                        }
                        else
                        {
                            //log += Environment.NewLine + "ERR3";
                        }
                    }
                }

                //StreamWriter sw = File.CreateText("import_log.txt");
                //sw.Write(log);
                //sw.Close();

                wb.Close(false);
                app.Quit();
                app.Quit();
                app = null;

                //setLabel(count + " payments imported.");
                //setLoadDisplay();
                Action act = () => {
                    frmProcess.publicClose();
                    MessageBox.Show(this,"Successfully imported ("+count+" entries) selected ledger file.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (count > 0)
                    {
                        loadPayments();
                    }
                };
                Invoke(act);
            }
            catch (Exception excep)
            {
                String err = "Unable to perform threadImporter_main operation.";
                Job.Log(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportEntriesToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            SaveFileDialog od = new SaveFileDialog();
            od.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(__threadExportEntries));
                th.Name = "PaymentExporter";
                th.Start(od.FileName);
                new frmProcess("Exporting entries", "Creating spreadsheet...", true, (c) => { }).ShowDialog(this);
            }
        }

        private void __threadExportEntries(object fname)
        {
            try
            {
                String filename = fname.ToString();
                int count = 0;

                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet ws = wb.Worksheets.Add();

                ws.get_Range("A1").set_Value(Type.Missing, "Date");
                ws.get_Range("B1").set_Value(Type.Missing, "Doc/Chq. No.");
                ws.get_Range("C1").set_Value(Type.Missing, "Type");
                ws.get_Range("D1").set_Value(Type.Missing, "Particulars");
                ws.get_Range("E1").set_Value(Type.Missing, "Credit");
                ws.get_Range("F1").set_Value(Type.Missing, "Debit");
                ws.get_Range("G1").set_Value(Type.Missing, "Closing balance");
                int row = 1;
                ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row + ":F" + row + ":G" + row).Cells.Interior.Color = System.Drawing.Color.LightGray.ToArgb();

                row = 2;
                double amt = 0;
                double initialBalance = curClient.obalance == 0 ? 0 : (curClient.obalance_type == Client.OpeningBalanceType.Debit ? curClient.obalance : 0 - curClient.obalance);

                Job.Payments.search(curClient.id, "", (Payment pay) => {
                    ws.get_Range("A" + row).set_Value(Type.Missing, pay.date);
                    ws.get_Range("B" + row).set_Value(Type.Missing, pay.invoice);
                    ws.get_Range("C" + row).set_Value(Type.Missing, pay.type);
                    ws.get_Range("D" + row).set_Value(Type.Missing, pay.particulars);
                    ws.get_Range("E" + row).set_Value(Type.Missing, pay.mode == Payment.PaymentMode.Credit ? pay.amount : 0);
                    ws.get_Range("F" + row).set_Value(Type.Missing, pay.mode == Payment.PaymentMode.Debit ? pay.amount : 0);
                    ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row + ":F" + row + ":G" + row).EntireColumn.AutoFit();
                    /*amt += pay.Credit;
                    amt -= pay.Debit;*/
                    if (pay.mode == Payment.PaymentMode.Credit)
                    {
                        amt += pay.amount;
                    }
                    else
                    {
                        amt -= pay.amount;
                    }
                    ws.get_Range("G" + row).set_Value(Type.Missing, amt);
                    row++;
                    count++;
                });
                /*foreach (Payment pay in Datastore.current.Payments)
                {
                    ws.get_Range("A" + row).set_Value(Type.Missing, pay.Date);
                    ws.get_Range("B" + row).set_Value(Type.Missing, pay.DocChqNo);
                    ws.get_Range("C" + row).set_Value(Type.Missing, pay.Type);
                    ws.get_Range("D" + row).set_Value(Type.Missing, pay.Particulars);
                    ws.get_Range("E" + row).set_Value(Type.Missing, pay.Credit);
                    ws.get_Range("F" + row).set_Value(Type.Missing, pay.Debit);
                    ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row + ":F" + row + ":G" + row).EntireColumn.AutoFit();
                    amt += pay.Credit;
                    amt -= pay.Debit;
                    ws.get_Range("G" + row).set_Value(Type.Missing, amt);
                    row++;
                    count++;
                }*/

                wb.Close(true, filename);
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
        }

        #endregion

        #region Report 1
       
        private void initReport1()
        {
            if (report1 == null)
            {
                report1 = new reports.frmReport1();
                report1.TopLevel = false;
                report1.Dock = DockStyle.Fill;
            }
            if (panMonthlyReport.Controls.Count == 0)
            {
                panMonthlyReport.Controls.Add(report1);
            }
            report1.Show();
            if (report1.WindowState == FormWindowState.Normal)
                report1.WindowState = FormWindowState.Maximized;
        }

        public void loadReport1()
        {
            tabControl.SelectedTab = tabReport1;
        }


        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {

        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tabLedger)
            {

            }
            else if (e.TabPage == tabReport1)
            {
                
            }
        }

        private void tabControl_TabIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tabControl_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabReport1)
            {
                setReport();
            }
        }

        public void setReport()
        {
            initReport1();
            report1.showReportOf(curMonth, dtReportYear.Value.Year, curClient.id);
            report1.showReport();
        }

        private void tabReport1_Resize(object sender, EventArgs e)
        {
            if (tabControl.SelectedTab == tabReport1 && report1 != null)
            {
                maximizeReport1();
            }
        }

        private void maximizeReport1()
        {
            if (report1 != null)
            {
                report1.WindowState = FormWindowState.Normal;
                report1.WindowState = FormWindowState.Maximized;
            }
        }
        private void dtReportYear_ValueChanged(object sender, EventArgs e)
        {
            
        }

        #endregion

        private void label1_Click(object sender, EventArgs e)
        {
            setReport();
        }

        private void btnShowReport1_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabReport1;
            setReport();
        }

        private void chkSimpleReportView_CheckedChanged(object sender, EventArgs e)
        {
            if (report1 != null)
            {
                if (chkSimpleReportView.Checked)
                {
                    report1.setReportView(NNPPoly.forms.reports.frmReport1.ReportView.SimpleView);
                }
                else
                {
                    report1.setReportView(NNPPoly.forms.reports.frmReport1.ReportView.HTMLView);
                }
                //setReport();
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNextMonth_Click(object sender, EventArgs e)
        {
            if (report1 != null)
                report1.func_nextMonth(sender, e);
        }

        private void btnPreviousMonth_Click(object sender, EventArgs e)
        {
            if (report1 != null)
                report1.func_prevMonth(sender, e);
        }

        private void btnPrintReport1_Click(object sender, EventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("&Report + Debit Note", report1.func_printAll));
            cm.MenuItems.Add(new MenuItem("-"));
            cm.MenuItems.Add(new MenuItem("&Directly (via Default Printer)", report1.func_printDirectly));
            cm.MenuItems.Add(new MenuItem("&Preview", report1.func_printPreview));
            cm.MenuItems.Add(new MenuItem("-"));
            cm.MenuItems.Add(new MenuItem("&Debit Note", report1.func_printDebitNote));
            cm.Show(sender as Control, new Point(0, (sender as Control).Height));
        }

        private void btnExportReport1_Click(object sender, EventArgs e)
        {
            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add(new MenuItem("&Excel (.xls)", report1.func_exportToExcel));
            cm.MenuItems.Add(new MenuItem("&Web Page (.html)", report1.func_exportToHtml));
            cm.Show(sender as Control, new Point(0, (sender as Control).Height));
        }

        private void ReportParameters_TextChanged(object sender, EventArgs e)
        {
            (sender as Control).BackColor = Job.Validation.ValidateLong((sender as Control).Text) ? Color.White : Color.Red;
        }

        private void btnParameters_Click(object sender, EventArgs e)
        {
            if (panReportParameters.Visible)
            {
                panReportParameters.Visible = false;
                panFooter.Height = 62;
            }
            else
            {
                panFooter.Height = 129;
                panReportParameters.Visible = true;
            }
        }

        private void ReportParameters_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnUpdateReportParameters_Click(sender, e);
            }
        }

        private void btnUpdateReportParameters_Click(object sender, EventArgs e)
        {
            if (txtCutOffDays.BackColor == Color.White)
            {
                curClient.SetDataReflector = true;
                curClient.cutoffdays = long.Parse(txtCutOffDays.Text.Trim());
            }
            curClient.threadUpdateForIntRates = false;
            if (txtIntRate1.BackColor == Color.White)
            {
                curClient.SetDataReflector = true;
                curClient.intrate1 = double.Parse(txtIntRate1.Text.Trim());
            }
            if (txtIntRate2.BackColor == Color.White)
            {
                curClient.SetDataReflector = true;
                curClient.intrate2 = double.Parse(txtIntRate2.Text.Trim());
            }
            curClient.threadUpdateForIntRates = true;
            if (txtLessDays.BackColor == Color.White)
            {
                curClient.SetDataReflector = true;
                curClient.lessdays = long.Parse(txtLessDays.Text.Trim());
            }
            Thread thread = new Thread(() =>
            {
                Action act = () =>
                {
                    report1.lvSimpleView.SetObjects(report1.lvSimpleView.Objects);
                    report1.htmlViewer.Text = report1.getHTML();
                };
                Invoke(act);
            });
            thread.Start();
        }

        private void lvPayments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lvPayments.SelectedObjects.Count > 0)
            {
                DialogResult dr=MessageBox.Show(this, "Are you sure to delete all selected payments ?", "Delete Payments", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    Thread thread = new Thread(() =>
                    {
                        Action act;
                        Client c = Job.mainForm.fClients.getCurrentClient();
                        if (c != null)
                        {
                            String message = "", title="";
                            if (Job.Payments.deletePaymentsOfClient(c.id))
                            {
                                message = "All selected payments deleted successfully.";
                                title = "Success";
                            }
                            else
                            {
                                message = "Can't delete payments, please try again.";
                                title = "Error";
                            }

                            act = () =>
                            {
                                MessageBoxIcon icon=MessageBoxIcon.Warning;
                                if(title.Equals("Success")) {
                                    icon=MessageBoxIcon.Information;
                                } else if(title.Equals("Error")) {
                                    icon= MessageBoxIcon.Error;
                                }
                                MessageBox.Show(this, message, title, MessageBoxButtons.OK, icon);
                            };
                            Invoke(act);
                        }
                        act = () => { frmProcess.publicClose(); };
                        Invoke(act);
                    });
                    thread.Name = "Thread: DeletePayments";
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                    new frmProcess("Delete Payments", "Deleting...", true, (fc) => { }).ShowDialog(this);
                    btnRefreshPayments_Click(btnRefreshPayments, new EventArgs());
                }
            }
        }

        private void btnRefreshPayments_Click(object sender, EventArgs e)
        {
            loadPayments(true);
        }

        private void lvPayments_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void lvPayments_editEntry(object sender, EventArgs e)
        {

        }

        private void frmClient_Load(object sender, EventArgs e)
        {

        }

        public bool isReport2
        {
            get
            {
                return chkReportFormat.Checked;
            }

            set
            {
                chkReportFormat.Checked = value;
            }
        }

        private void chkReportFormat_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }
}
