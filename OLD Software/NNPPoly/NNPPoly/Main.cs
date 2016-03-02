using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            menubar.Visible = statusbar.Visible = false;
            menubar.Enabled = false;
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            loadDataFile();
        }

        private void loadDataFile()
        {
            try
            {
                DatafileLoader dfLoader = new DatafileLoader();
                setForm(dfLoader);
                dfLoader.FormClosed += new FormClosedEventHandler(dfLoader_FormClosed);
            }
            catch (Exception excep)
            {
                String err = "Unable to perform loaddatafile_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void dfLoader_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Form form = sender as Form;
                if (form.DialogResult == DialogResult.OK)
                {
                    loadUserLogin();
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform dfLoader_close operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private UserLogin userLogin = null;
        private void loadUserLogin()
        {
            try
            {
                menubar.Visible = statusbar.Visible = false;
                menubar.Enabled = false;
                if (userLogin == null || userLogin.IsDisposed)
                    userLogin = new UserLogin();
                Text = "NNP Poly";
                setForm(userLogin);
                userLogin.FormClosed += new FormClosedEventHandler(userLogin_FormClosed);
                if (WindowState != FormWindowState.Maximized)
                    WindowState = FormWindowState.Maximized;
            }
            catch (Exception excep)
            {
                String err = "Unable to perform loaduserlogin_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void setForm(Form form)
        {
            try
            {
                panel.Controls.Clear();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                panel.Visible = true;
                form.TopLevel = false;
                panel.Controls.Add(form);
                if (!form.IsDisposed)
                    form.Show();
                form.FormClosed += new FormClosedEventHandler(form_FormClosed);
            }
            catch (Exception excep)
            {
                String err = "Unable to perform setform_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void form_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        void userLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Datastore.current == null)
            {
                loadUserLogin();
            }
            else
            {
                menubar.Visible = statusbar.Visible = true;
                menubar.Enabled = true;
                loadDisplay();
            }
        }

        private Display displayForm = null;
        private void loadDisplay()
        {
            try
            {
                if (Datastore.current == null) return;
                if (displayForm == null || displayForm.IsDisposed)
                    displayForm = new Display(this);
                lblOpeningBalance.Text = "Opening balance: Rs. " + Datastore.current.OpeningBalance + " /-";
                lblLoggedUser.Text = Datastore.current.ClientName;
                Text = Datastore.current.ClientName+" - NNP Poly";
                lblLoader.Text = "Total payments: " + Datastore.current.Payments.Count;
                setForm(displayForm);
                displayForm.WindowState = FormWindowState.Maximized;
                displayForm.loadPayments();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform loadDisplay_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void setEditDeleteVisibility(bool edit,bool delete)
        {
            editSelectedPaymentToolStripMenuItem.Visible = edit;
            deleteSelectedPaymentsToolStripMenuItem.Visible = delete;
        }

        private void panel_Resize(object sender, EventArgs e)
        {
            foreach (Control ctrl in panel.Controls)
            {
                Form form = ctrl as Form;
                if (form != null)
                {
                    form.WindowState = FormWindowState.Normal;
                    form.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            if (displayForm != null && !displayForm.IsDisposed)
                displayForm.Close();
            displayForm = null;
            loadUserLogin();
        }

        private void newEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            NewPayment newPayment = new NewPayment();
            newPayment.ShowDialog(this);
            loadDisplay();
        }

        private bool sqFlag = false;
        private void txtSearch_Enter(object sender, EventArgs e)
        {
            sqFlag = false;
            if (txtSearch.Text.Trim().Equals("Search payment..."))
                txtSearch.Text = "";
            sqFlag = true;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            sqFlag = false;
            if (txtSearch.Text.Trim().Length==0)
                txtSearch.Text = "Search payment...";
            sqFlag = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if(sqFlag)
                displayForm.searchPayments(txtSearch.Text.Trim(),strictSearch);
        }

        private bool strictSearch = false;
        private void txtSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                MenuItem mitem = new MenuItem("&Strict search",txtSearch_RightClickAction);
                mitem.Checked = strictSearch;
                cm.MenuItems.Add(mitem);
                cm.Show(txtSearch.Control, new Point(0,txtSearch.Height));
            }
        }
        private void txtSearch_RightClickAction(object sender,EventArgs e)
        {
            strictSearch = !strictSearch;
            txtSearch.Text = txtSearch.Text+" ";
        }

        private void txtSearch_MouseEnter(object sender, EventArgs e)
        {
            tooltip.SetToolTip(txtSearch.Control, "Strict search is " + (strictSearch ? "enabled." : "disabled.") + "!");
        }

        private void editSelectedPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            displayForm.callMenuItemFromName(new MenuItem("&Edit"));
        }

        private void deleteSelectedPaymentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            displayForm.callMenuItemFromName(new MenuItem("&Delete"));
        }

        private void newUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            NewClients newclients = new NewClients();
            newclients.ShowDialog(this);
        }

        private void editCurrentLoggedUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            EditUserAccount edit = new EditUserAccount(Datastore.current.ID.ToString());
            edit.ShowDialog(this);
            loadDisplay();
        }

        private void importPaymentsFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
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
                MessageBox.Show(this,"Microsoft Office Interop library is not instaled, please install microsoft office 2010 or leter.","MS Office not found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                lblLoader.Text = "Importing file ...";
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadImporter));
                th.Name = "PaymentImporter";
                th.Start(od.FileName);
            }
        }

        private void threadImporter(object fname)
        {
            try
            {
                String log = "";
                String filename = fname.ToString();
                int count = 0;
                #region old-code
                /*Net.SourceForge.Koogra.IWorkbook wb = Net.SourceForge.Koogra.WorkbookFactory.GetExcel2007Reader(fname.ToString());

            Net.SourceForge.Koogra.IWorksheet ws = wb.Worksheets.GetWorksheetByIndex(0);

            Decimal max = 0;
            foreach (Payment p in Datastore.current.Payments)
                if (p.ID > max)
                    max = p.ID;
            max++;

            bool flag = true, flag1 = true, flag2 = false;
            int count = 0;

            for (uint r = ws.FirstRow; r <= ws.LastRow && flag; ++r)
            {
                Net.SourceForge.Koogra.IRow row = ws.Rows.GetRow(r);

                Payment pay = new Payment();
                pay.ID = max;
                pay.DocChqNo = "";
                flag1 = true;
                for (uint c = ws.FirstCol; c <= ws.LastCol && flag; ++c)
                {
                    if (r == ws.FirstRow)
                    {
                        flag1 = false;
                        for (uint i = 0; i <= 5; i++)
                            if (!("Date Doc/Chq. No. Type Particulars Credit Debit").ToLower().Contains(row.GetCell(i).Value.ToString().ToLower()))
                            {
                                flag = false;
                                break;
                            }
                        break;
                    }
                    else
                    {
                        object val = row.GetCell(c).GetFormattedValue();
                        if (val != null && val.ToString().Length!=0)
                        {
                            if (c == 0)
                                pay.Date = val.ToString();
                            if (c == 1)
                            {
                                if (val.ToString().StartsWith("Opening Balance"))
                                    flag2 = true;
                                pay.DocChqNo = val.ToString();
                            }
                            if (c == 2)
                                pay.Type = val.ToString();
                            if (c == 3)
                                pay.Particulars = val.ToString();
                            if (c == 4)
                            {
                                double tmp = 0;
                                if (!double.TryParse(val.ToString(), out tmp))
                                {
                                    flag1 = false;
                                }
                                else
                                {
                                    flag1 = true;
                                }
                                pay.Credit = tmp;
                            }
                            if (c == 5)
                            {
                                double tmp = 0;
                                if (!double.TryParse(val.ToString(), out tmp))
                                {
                                    flag1 = false;
                                }
                                else
                                {
                                    flag1 = true;
                                }
                                pay.Debit = tmp;
                                if (flag2)
                                {
                                    Datastore.current.OpeningBalance = tmp;
                                    flag2 = false;
                                }
                            }

                        }
                        else
                        {
                            if (c == 0 || c == 2 || c == 3)
                            {
                                flag1 = false;
                            }
                            else if (c == 4 && c == 5)
                            {
                                flag1 = false;
                            }
                        }
                    }
                }

                if (flag1 && !pay.DocChqNo.StartsWith("Opening Balance"))
                {
                    Datastore.current.Payments.Add(pay);
                    max++;
                    count++;
                }
            }*/
                #endregion

                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Open(filename);

                Decimal max = ++Datastore.current.PaymentIDManager;
                /*foreach (Payment p in Datastore.current.Payments)
                    if (p.ID > max)
                        max = p.ID;
                max++;*/
                //log += Environment.NewLine + "MAX:" + max;

                foreach (Excel.Worksheet ws in wb.Worksheets)
                {
                    Excel.Range range = ws.UsedRange;
                    //log += Environment.NewLine + "ROW-COUNT:" + range.Rows.Count;
                    for (int i = 0; i < range.Rows.Count; i++)
                    {
                        //log += Environment.NewLine + "ROW-ID:" + i;
                        Payment pay = new Payment();
                        pay.ID = max;
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
                                    Datastore.current.OpeningBalance = dd;
                                }
                                log += Environment.NewLine + '\t' + "OB:" + dd;
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

                            //log += Environment.NewLine + '\t' + date + ", " + docno + ", " + type + ", " + part + ", " + credit + ", " + debit;

                            /*date = date.Replace("12:00:00 AM", "");
                            date = date.Replace("12:00:00 PM", "");
                            int len = "00-00-0000".Length;
                            if (date.Length > len)
                            {
                                date = date.Substring(0, len);
                                if (date.IndexOf(" ")>-1)
                                    date = date.Substring(0,date.IndexOf(" "));
                            }*/
                            DateTime dt;
                            if (DateTime.TryParse(date,out dt))
                            {
                                //dt = DateTime.ParseExact(dt.ToString("dd-MM-yyyy"),"dd-MM-yyyy",Program.provider);
                                if (!(credit.Length == 0 && debit.Length == 0))
                                {
                                    double amt = 0;
                                    if (!credit.Equals("0") && double.TryParse(credit, out amt))
                                    {
                                        pay.Credit = amt;
                                        pay.Remain = amt;
                                    }
                                    else if (!debit.Equals("0") && double.TryParse(debit, out amt))
                                    {
                                        pay.Debit = amt;
                                        if (part.Trim().LastIndexOf(" ") > -1)
                                        {
                                            String mtnumber = part.Trim().Substring(part.LastIndexOf(" ") + 1);
                                            double tempNo=0;
                                            if (double.TryParse(mtnumber, out tempNo))
                                                pay.MT = mtnumber.Trim();
                                            else
                                                pay.MT = "0";
                                        }
                                        else
                                        {
                                            pay.MT = "0";
                                        }
                                        pay.Remain = 0;
                                    }

                                    pay.Date = new DateTime(dt.Year, dt.Month, dt.Day, 12, 0, 0);
                                    pay.Type = type;
                                    pay.Particulars = part;
                                    pay.DocChqNo = docno;

                                    addPay(pay);
                                    //log += Environment.NewLine + '\t' + "ADDED";
                                    max++;
                                    Datastore.current.PaymentIDManager = max;
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

                setLabel(count + " payments imported.");
                setLoadDisplay();
                MessageBox.Show("Successfully imported selected ledger file.","Done",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception excep)
            {
                String err = "Unable to perform threadImporter_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addPay(Payment pay)
        {
            try
            {
                Action a = () =>
                {
                    Datastore.current.Payments.Add(pay);
                    Record.RecordNow(Datastore.current, pay);
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform addpay_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void setLabel(String txt)
        {
            try
            {
                Action a = () =>
                {
                    lblLoader.Text = txt;
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform setlabel_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setLoadDisplay()
        {
            try
            {
                Action a = () =>
                {
                    Datastore.dataFile.Save();
                    loadDisplay();
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform setloaddisplay_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportPaymentsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
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
                lblLoader.Text = "Exporting file ...";
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadExporter));
                th.Name = "PaymentExporter";
                th.Start(od.FileName);
            }
        }

        private void threadExporter(object fname)
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
                foreach (Payment pay in Datastore.current.Payments)
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
                }

                wb.Close(true, filename);
                app.Quit();
                app.Quit();
                app = null;

                setLabel(count + " payments exported.");
                setLoadDisplay();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform threadExporter_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void createNewPaymentRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Filter = "Payment recovery file|*.par";
                sd.FileName = "Payments_" + Datastore.current.ClientName + "_" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Year;
                if (sd.ShowDialog(this) == DialogResult.OK)
                {
                    eXML xml = new eXML();
                    String bk = xml.PathAppend;
                    xml.PathAppend = "";
                    if (xml.Write(sd.FileName, typeof(List<Payment>), Datastore.current.Payments, true))
                    {
                        MessageBox.Show(this, "Payment recovery is created at " + sd.FileName + "!", "Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(this, "Sorry, software isn't able to create new payment recovery file." + Environment.NewLine + Environment.NewLine + "Error:" + xml.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    xml.PathAppend = bk;
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform paymentRecovery_click operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void restoreRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog sd = new OpenFileDialog();
                sd.Filter = "Payment recovery file|*.par";
                sd.FileName = "";
                if (sd.ShowDialog(this) == DialogResult.OK)
                {
                    DialogResult dr = MessageBox.Show(this, "Are you sure to restore this selected recovery over current payment records, because when you restore this your current payment record is replaced by new one ?", "Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr != DialogResult.Yes) return;
                    eXML xml = new eXML();
                    String bk = xml.PathAppend;
                    xml.PathAppend = "";
                    if (xml.Read<List<Payment>>(sd.FileName, typeof(List<Payment>), ref Datastore.current.Payments, true))
                    {
                        if(MessageBox.Show(this,"Do you want to record these payments in records as a new entries ?","Record now",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                        {
                            foreach (Payment payment in Datastore.current.Payments)
                                Record.RecordNow(Datastore.current, payment);
                        }
                        Datastore.dataFile.Save();
                        MessageBox.Show(this, "Payment recovery is restored from " + sd.FileName + "!", "Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadDisplay();
                    }
                    else
                    {
                        MessageBox.Show(this, "Sorry, software isn't able to restore payment recovery file." + Environment.NewLine + Environment.NewLine + "Error:" + xml.Error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    xml.PathAppend = bk;
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform restoreRecovery_main operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void priorityTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PriorityTypes pt = new PriorityTypes();
            pt.ShowDialog(this);
        }

        private void generalReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            loadDisplay();
        }

        private void paymentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!menubar.Visible) return;
            GC.Collect();
            RepMonthSel rms = new RepMonthSel();
            if (rms.ShowDialog(this) == DialogResult.OK)
            {
                GeneralReport genReport = new GeneralReport(rms.selMonth,rms.selYear);
                setForm(genReport);
            }
        }

        public static bool simpleReport = false;

        private void simpleReportStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleReport = !simpleReport;
            simpleReportStyleToolStripMenuItem.Checked = simpleReport;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!Datastore.dataFile.Save())
            {

            }
        }

        public static bool filterPeriod=false;
        public static DateTime ffromDate, ftoDate;
        private void filterPeriodMenuItem5_Click(object sender, EventArgs e)
        {
            filterPeriod = !filterPeriod;
            if (filterPeriod)
            {
                FilterPeriod fp = new FilterPeriod();
                if (fp.ShowDialog(this) == DialogResult.OK)
                {
                    ffromDate = fp.fromDate;
                    ftoDate = fp.toDate;
                    filterPeriodMenuItem5.Font = new Font(filterPeriodMenuItem5.Font, FontStyle.Bold);
                    loadDisplay();
                }
                else
                {
                    filterPeriod = false;
                }
            }
            else
            {
                filterPeriodMenuItem5.Font = new Font(filterPeriodMenuItem5.Font, FontStyle.Regular);
                loadDisplay();
            }
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            simpleReportStyleToolStripMenuItem.Checked = simpleReport;
        }

        

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "Zero";

            if (number < 0)
                return "- " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 10000000) > 0)
            {
                words += NumberToWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " Lakhs ";
                number %= 1000000;
            }

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " Lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " Hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words.Replace("-"," ");
        }

        private void gradesSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GradesSettings gs = new GradesSettings();
            gs.ShowDialog(this);
        }

        public void setConflicChecker()
        {
            conflicCheckToolStripMenuItem.Font = new Font(conflicCheckToolStripMenuItem.Font, FontStyle.Bold);
        }
        public void clearConflicChecker()
        {
            conflicCheckToolStripMenuItem.Font = new Font(conflicCheckToolStripMenuItem.Font, FontStyle.Regular);
        }

        private void conflicCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (displayForm != null && !displayForm.IsDisposed)
            {
                displayForm.conflicCheck();
            }
        }

        private void specialTypesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpecialTypes specialTypes = new SpecialTypes();
            specialTypes.ShowDialog(this);
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

        private void collectionListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CollectionList collectionList = new CollectionList();
            collectionList.ShowDialog(this);
        }

        private void recordsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Records records = new Records();
            records.ShowDialog(this);
        }

        private void printFormatSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PrintFormatSettings().ShowDialog(this);
        }

        private void debitNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DebitNotes().ShowDialog(this);
            logoutToolStripMenuItem_Click(sender, e);
        }

        private void sMSSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SMSSettings().ShowDialog(this);
        }

        private void emailSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new MailSettings().ShowDialog(this);
        }

        private void rILDispatchToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
