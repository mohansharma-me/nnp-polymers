using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly.forms
{
    public partial class frmInterestAdvise : Form
    {
        public frmInterestAdvise()
        {
            InitializeComponent();
            olvColumnInterestAmount.AspectToStringConverter = (c) =>
            {
                return Job.Functions.AmountToString(((double)c));
            };

            olvColumnClosingBalance.AspectToStringConverter = (c) =>
            {
                return Job.Functions.AmountToString(((double)c));
            };

            olvColumnOpeningBalance.AspectToStringConverter = (c) =>
            {
                return Job.Functions.AmountToString(((double)c));
            };
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            classes.InterestAdviseRow.forMonth = dtMonth.Value;
            Thread thread = new Thread(() =>
            {
                Action act = null;
                classes.Client clientBackup = Job.mainForm.fClients.getCurrentClient();
                DateTime todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);

                long totalClients = Job.Clients.getCountClients();
                act = () =>
                {
                    lvInterests.ClearObjects();
                    pbProcess.Maximum = 100;
                };
                Invoke(act);

                List<Color> colors = new List<Color>();
                try
                {
                    colors = new List<Color>();
                    Decimal colorCounter = 0;
                    foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
                        if (property.PropertyType == typeof(System.Drawing.Color))
                        {
                            colors.Add(Color.FromName(property.Name));
                            colorCounter++;
                        }
                }
                catch (Exception excep)
                {
                    String err = "Unable to collect colors_enumeration operation.";
                    Job.Log(err, excep);
                }

                long currentClientCount = 0;
                Panel panTemp = new Panel();
                forms.reports.frmReport1 report = new reports.frmReport1(colors);
                report.TopLevel = false;
                panTemp.Controls.Add(report);
                report.Show();
                double totalInterestAmount = 0;
                int perc = 0;
                List<classes.InterestAdviseRow> collections = new List<classes.InterestAdviseRow>();
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    currentClientCount++;
                    Job.mainForm.fClients.setCurrentClient(c);
                    act = () =>
                    {
                        lblProcess.Text = "Processing \"" + c.name + "\"...";
                    };
                    Invoke(act);
                    report.showReportOf(classes.InterestAdviseRow.forMonth.Month, classes.InterestAdviseRow.forMonth.Year, c.id, false);
                    report.showReport(false);

                    if (report.interestDue > 0)
                    {
                        classes.InterestAdviseRow coll = new classes.InterestAdviseRow();
                        coll.client = c;
                        coll.interest_amount = report.interestDue;
                        coll.closing_balance = report.amountDue;
                        if (c.obalance != 0)
                        {
                            if (c.obalance_type == classes.Client.OpeningBalanceType.Credit)
                            {
                                coll.opening_balance = Job.Payments.getOpeningBalance(c.id, classes.InterestAdviseRow.forMonth, 0 - c.obalance);
                            }
                            else if (c.obalance_type == classes.Client.OpeningBalanceType.Debit)
                            {
                                coll.opening_balance = Job.Payments.getOpeningBalance(c.id, classes.InterestAdviseRow.forMonth, c.obalance);
                            }
                            else
                            {
                                coll.opening_balance = Job.Payments.getOpeningBalance(c.id, classes.InterestAdviseRow.forMonth, c.obalance);
                            }
                        }
                        else
                        {
                            coll.opening_balance = Job.Payments.getOpeningBalance(c.id, classes.InterestAdviseRow.forMonth);
                        }
                        totalInterestAmount += coll.interest_amount;
                        collections.Add(coll);
                    }

                    perc = (int)((currentClientCount * 100) / totalClients);
                    act = () =>
                    {
                        pbProcess.Value = perc;
                        txtTotalInterestAmount.Text = Job.Functions.AmountToString(totalInterestAmount);
                    };
                    Invoke(act);
                });
                Job.mainForm.fClients.setCurrentClient(clientBackup);
                act = () =>
                {
                    lvInterests.SetObjects(collections);
                    //txtTotalCollectingAmount.Text = totalInterestAmount.ToString("0.00");
                    closeLoading();
                };
                Invoke(act);
            });
            thread.Name = "Thread: RefreshInterstAdviseList";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            showLoading("Scanning reports...");
        }

        public void showLoading(String msg,bool showLoading=true)
        {
            lvInterests.Visible = false;
            lcProcess.Visible = showLoading;
            lcProcess.Active = showLoading;
            lblProcess.Text = msg;
            pbProcess.Visible = true;
            pbProcess.Style = ProgressBarStyle.Blocks;
            panel3.Enabled = false;
        }

        public void closeLoading()
        {
            lvInterests.Visible = true;
            lcProcess.Visible = false;
            lcProcess.Active = false;
            lblProcess.Text = "";
            pbProcess.Visible = false;
            panel3.Enabled = true;
            lvInterests.BringToFront();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            func_generateReport(false, true);
        }

        public void func_generateReport(bool print, bool export)
        {
            if (lvInterests.GetItemCount() <= 0)
            {
                MessageBox.Show(this, "Please refresh interest advise list before printing/exporting any list.", "Interest Advise List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            String filename = "";
            if (export)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "Export interest advise list...";
                sd.Filter = "Excel files|*.xls;*.xlsx";
                if (sd.ShowDialog() != DialogResult.OK) return;
                filename = sd.FileName;
            }
            else if (!print) return;

            Thread thread = new Thread(() =>
            {

                #region thread-code
                Action action = null;

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

                        ws.get_Range("A1").set_Value(Type.Missing, "Client name");
                        ws.get_Range("B1").set_Value(Type.Missing, "Opening Balance");
                        ws.get_Range("C1").set_Value(Type.Missing, "Closing Balance");
                        ws.get_Range("D1").set_Value(Type.Missing, "Interest Amount");

                        int row = 1;
                        ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row).Cells.Interior.Color = System.Drawing.Color.LightGray.ToArgb();
                        DateTime today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);

                        List<classes.InterestAdviseRow> collections = null;

                        action = () =>
                        {
                            collections = (List<classes.InterestAdviseRow>)lvInterests.Objects;
                            frmProcess.getInstance().pbProcess.Style = ProgressBarStyle.Blocks;
                            frmProcess.getInstance().pbProcess.Maximum = collections.Count;
                            frmProcess.getInstance().pbProcess.Value = 0;
                        };
                        Invoke(action);

                        row = 2;
                        double totalInterestAmount=0;
                        if (collections != null)
                            foreach (classes.InterestAdviseRow coll in collections)
                            {
                                ws.get_Range("A" + row).set_Value(Type.Missing, coll.client.name);
                                ws.get_Range("B" + row).set_Value(Type.Missing, Job.Functions.AmountToString(coll.opening_balance));
                                ws.get_Range("C" + row).set_Value(Type.Missing, Job.Functions.AmountToString(coll.closing_balance));
                                ws.get_Range("D" + row).set_Value(Type.Missing, Job.Functions.AmountToString(coll.interest_amount));
                                row++;
                                totalInterestAmount+=coll.interest_amount;
                                action = () =>
                                {
                                    //waitingDialog.getProgressBar().Value++;
                                    frmProcess.getInstance().pbProcess.Value++;
                                };
                                Invoke(action);
                            }

                        ws.get_Range("A" + row).set_Value(Type.Missing, "Interest Advise List of " + classes.InterestAdviseRow.forMonth.ToString("MM-yyyy"));
                        ws.get_Range("B" + row).set_Value(Type.Missing, "");
                        ws.get_Range("C" + row).set_Value(Type.Missing, "");
                        ws.get_Range("D" + row).set_Value(Type.Missing, Job.Functions.AmountToString(totalInterestAmount));
                        ws.get_Range("A" + row + ":D" + row).Font.Bold = true;
                        row++;

                        ws.get_Range("A1").EntireColumn.AutoFit();
                        ws.get_Range("B1").EntireColumn.AutoFit();
                        ws.get_Range("C1").EntireColumn.AutoFit();
                        ws.get_Range("D1").EntireColumn.AutoFit();

                        ws.get_Range("B1").EntireColumn.NumberFormat = "0.00";
                        ws.get_Range("C1").EntireColumn.NumberFormat = "0.00";
                        ws.get_Range("D1").EntireColumn.NumberFormat = "0.00";

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
                            frmProcess.publicClose();
                        };
                        Invoke(action);
                    }
                    catch (Exception excep)
                    {
                        String err = "Unable to perform threadPrintInterestAdviseList operation.";
                        Job.Log(err, excep);
                        MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                }

                #endregion

            });
            thread.Name = "Thread: generareReport-InterestAdviseList";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            String title = "Export to excel file...";
            String msg = "Generating excel file...";
            if (print)
            {
                title = "Print Interest Advise List";
                msg = "Preparing interest advise list...";
            }

            new frmProcess(title, msg, false, (c) => { }).ShowDialog(this);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            func_generateReport(true, false);
        }

        private void lvInterests_DoubleClick(object sender, EventArgs e)
        {

            classes.Client c = (lvInterests.SelectedObjects[0] as classes.InterestAdviseRow).client;
            Job.mainForm.openClientForIntersetAdivseList(c, dtMonth.Value.Year, dtMonth.Value.Month);

            #region old-region
            /*
            if (lvInterests.SelectedObjects.Count == 1)
            {
                forms.reports.frmReport1 report = null;
                classes.Client c = (lvInterests.SelectedObjects[0] as classes.InterestAdviseRow).client;
                classes.InterestAdviseRow.forMonth = dtMonth.Value;
                classes.Client clientBackup = Job.mainForm.fClients.getCurrentClient();
                DateTime todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);

                List<Color> colors = new List<Color>();
                try
                {
                    colors = new List<Color>();
                    Decimal colorCounter = 0;
                    foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
                        if (property.PropertyType == typeof(System.Drawing.Color))
                        {
                            colors.Add(Color.FromName(property.Name));
                            colorCounter++;
                        }
                }
                catch (Exception excep)
                {
                    String err = "Unable to collect colors_enumeration operation.";
                    Job.Log(err, excep);
                }
                Job.mainForm.fClients.setCurrentClient(c);
                report = new reports.frmReport1(colors, classes.InterestAdviseRow.forMonth.Month, classes.InterestAdviseRow.forMonth.Year, c.id);
                //report.Show();

                //report.showReportOf(classes.InterestAdviseRow.forMonth.Month, classes.InterestAdviseRow.forMonth.Year, c.id, false);
                //report.showReport(true);
                report.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                report.WindowState = FormWindowState.Maximized;
                report.Text = "Monthly Report";
                report.ShowDialog(this);
                Job.mainForm.fClients.setCurrentClient(clientBackup);
            }
            */
            #endregion
        }

        private void lvInterests_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                lvInterests_DoubleClick(sender, e);
        }

        private void lvInterests_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


    }
}
