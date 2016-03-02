using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly.forms
{
    public partial class frmCollectionList : Form
    {
        public frmCollectionList()
        {
            InitializeComponent();
            olvColumnDebitInvoice.AspectGetter = (r) =>
            {
                classes.Payment row = (classes.Payment)r;
                return row.invoice;
            };

            olvColumnDate.AspectGetter = (r) =>
            {
                classes.Payment row = (classes.Payment)r;
                return row.date;
            };

            olvColumnDate.AspectToStringConverter = (r) =>
            {
                return ((DateTime)r).ToShortDateString();
            };

            olvColumnAmount.AspectGetter = (r) =>
            {
                classes.Payment row = (classes.Payment)r;
                return row.remainBalance;
            };

            olvColumnAmount.AspectToStringConverter = (r) =>
            {
                return ((double)r).ToString("0.00");
            };

            olvColumnCollectingAmount.AspectToStringConverter = (r) =>
            {
                return ((double)r).ToString("0.00");
            };



            olvColumnDays.AspectGetter = (r) =>
            {
                classes.Payment row = (classes.Payment)r;
                DateTime tD=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day,12,0,0);
                return tD.Subtract(row.date).TotalDays;
            };
        }

        private void frmCollectionList_Shown(object sender, EventArgs e)
        {
            
        }

        public void showLoading()
        {
            panProcess.Dock = DockStyle.Fill;
            panProcess.Visible = true;
            panProcess.BringToFront();
            lcProcess.Active = true;
            lvClients.Enabled =
            btnRegenerate.Enabled = false;
        }

        public void closeLoading()
        {
            panProcess.Visible = false;
            panProcess.SendToBack();
            lcProcess.Active = false;
            lvClients.Enabled =
            btnRegenerate.Enabled = true;
        }

        public void startProcess()
        {
            showLoading();
            Thread thread = new Thread(() =>
            {
                List<classes.collection_list.Collection> collections = new List<classes.collection_list.Collection>();
                Action act = null;
                classes.Client clientBackup = Job.mainForm.fClients.getCurrentClient();
                DateTime todayDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);

                long totalClients = Job.Clients.getCountClients();
                act = () =>
                {
                    lvDebits.ClearObjects();
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
                double totalCollectingAmount = 0;
                int perc = 0;
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    currentClientCount++;
                    Job.mainForm.fClients.setCurrentClient(c);
                    act = () =>
                    {
                        lblProcess.Text = "Processing \"" + c.name + "\"...";
                    };
                    Invoke(act);
                    report.showReportOf(todayDate.Month, todayDate.Year, c.id, false);
                    report.showReport(false);

                    if (report.amountDue > 0)
                    {
                        classes.collection_list.Collection coll = new classes.collection_list.Collection();
                        coll.client = c;
                        coll.CollectingAmount = 0;//report.amountDue;
                        coll.Rows = report.getAllRows();
                        long prevId = 0;
                        double overDueAmount = 0;
                        foreach (classes.report1.Row row in coll.Rows)
                        {

                            if (row != null && row.debit != null)
                            {
                                double dayGap = todayDate.Subtract(row.debit.date).TotalDays;
                                double clientCutOffDays = c.cutoffdays - 1;
                                if (row.debit != null && row.credit == null)
                                {
                                    if ((dayGap >= clientCutOffDays && row.debit.remainBalance > 0) || row.debit.isPriority)
                                    {
                                        coll.CollectingAmount += row.debit.remainBalance;
                                    }
                                }
                                else if (row.rowtype == classes.report1.RowType.RemainingDebitBalanceRow)
                                {
                                    coll.CollectingAmount += double.Parse(row.debit_invoice);
                                }
                            }

                            /*if (row != null && row.debit != null)
                            {
                                double dayGap = todayDate.Subtract(row.debit.date).TotalDays;
                                double clientCutOffDays = c.cutoffdays - 1;
                                if ((prevId != row.debit.id && row.debit.remainBalance > 0 && dayGap > clientCutOffDays))
                                {
                                    overDueAmount += row.debit.amount;
                                    prevId = row.debit.id;
                                }
                            }*/
                        }

                        totalCollectingAmount += coll.CollectingAmount;
                        coll.overdue = overDueAmount;

                        if (coll.CollectingAmount > 0)
                            collections.Add(coll);
                    }

                    perc = (int)((currentClientCount * 100) / totalClients);
                    act = () =>
                    {
                        pbProcess.Value = perc;
                    };
                    Invoke(act);
                });
                Job.mainForm.fClients.setCurrentClient(clientBackup);
                act = () =>
                {
                    lvClients.SetObjects(collections);
                    txtTotalCollectingAmount.Text = totalCollectingAmount.ToString("0.00");
                    closeLoading();

                    calculateOverDueAmount();
                };
                Invoke(act);
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Name = "Thread: CollectionList";
            thread.Start();
        }

        private void btnRegenerate_Click(object sender, EventArgs e)
        {
            startProcess();
        }

        public void calculateOverDueAmount()
        {
            
        }

        private void lvClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvClients.SelectedObjects.Count == 1)
            {
                classes.collection_list.Collection coll = (classes.collection_list.Collection)lvClients.SelectedObjects[0];
                double clientCutOffDays = coll.client.cutoffdays - 1;
                Thread thread = new Thread(() =>
                {
                    DateTime todayDate = DateTime.Today;
                    List<classes.Payment> selDebits = new List<classes.Payment>();
                    long prevId = -1;
                    foreach (classes.report1.Row row in coll.Rows)
                    {
                        if (row != null && row.debit != null)
                        {
                            double dayGap = todayDate.Subtract(row.debit.date).TotalDays;
                            if (row.debit != null && row.credit == null)
                            {
                                if ((dayGap >= clientCutOffDays && row.debit.remainBalance > 0) || row.debit.isPriority)
                                {
                                    //coll.CollectingAmount += row.debit.remainBalance;
                                    selDebits.Add(row.debit);
                                }
                            }
                            else if (row.rowtype == classes.report1.RowType.RemainingDebitBalanceRow)
                            {
                                //coll.CollectingAmount += double.Parse(row.debit_invoice);
                                selDebits.Add(row.debit);
                            }
                        }

                        /*if (row != null && row.debit != null)
                        {
                            double dayGap = todayDate.Subtract(row.debit.date).TotalDays;
                            if ((prevId != row.debit.id && row.debit.remainBalance > 0 && dayGap >= clientCutOffDays))
                            {
                                selDebits.Add(row.debit);
                                prevId = row.debit.id;
                            }
                        }*/
                    }
                    Action act = () =>
                    {
                        lvDebits.SetObjects(selDebits);
                        closeLoading();
                    };
                    Invoke(act);
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                showLoading();
            }
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (lvClients.GetItemCount() > 0)
            {
                func_collectionList(false, true);
            }
            else
            {
                MessageBox.Show(this, "Please generate collection list first and after that you can print or export collection list report.", "Collection List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void func_collectionList(bool print, bool export)
        {
            String filename = "";
            if (export)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel files|*.xls;*.xlsx";
                sfd.OverwritePrompt = true;
                if (sfd.ShowDialog(this) == DialogResult.OK)
                {
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
                }
                else
                {
                    return;
                }
            }
            else if (!print)
            {
                return;
            }

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

                        ws.get_Range("A1").set_Value(Type.Missing, "Party Name");
                        ws.get_Range("B1").set_Value(Type.Missing, "Inv. Detail");
                        ws.get_Range("C1").set_Value(Type.Missing, "Date");
                        ws.get_Range("D1").set_Value(Type.Missing, "Days");
                        ws.get_Range("E1").set_Value(Type.Missing, "Amt");

                        int row = 1;
                        ws.get_Range("A" + row + ":B" + row + ":C" + row + ":D" + row + ":E" + row).Cells.Interior.Color = System.Drawing.Color.LightGray.ToArgb();
                        DateTime today = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 0, 0);

                        List<classes.collection_list.Collection> collections = null;

                        action = () =>
                        {
                            collections = (List<classes.collection_list.Collection>)lvClients.Objects;
                            frmProcess.getInstance().pbProcess.Style = ProgressBarStyle.Blocks;
                            frmProcess.getInstance().pbProcess.Maximum = collections.Count;
                            frmProcess.getInstance().pbProcess.Value = 0;
                        };
                        Invoke(action);

                        row = 2;
                        if (collections != null)
                        foreach (classes.collection_list.Collection coll in collections)
                        {
                            int startingRow = row;

                            long prevId = -1;
                            foreach (classes.report1.Row r in coll.Rows)
                            {
                                if (r != null && r.debit != null)
                                {
                                    if (prevId != r.debit.id && r.debit.remainBalance > 0)
                                    {
                                        //selDebits.Add(r.debit);

                                        //DateTime date = new DateTime(r.debit.date.Year, r.debit.date.Month, r.debit.date.Day, 12, 0, 0);
                                        double days = Math.Abs(r.debit.date.Subtract(today).TotalDays);

                                        ws.get_Range("A" + row).set_Value(Type.Missing, coll.client.name);
                                        ws.get_Range("B" + row).set_Value(Type.Missing, r.debit.invoice);
                                        ws.get_Range("C" + row).set_Value(Type.Missing, r.debit.date.ToShortDateString());
                                        ws.get_Range("D" + row).set_Value(Type.Missing, days.ToString("0"));
                                        ws.get_Range("E" + row).set_Value(Type.Missing, Job.Functions.AmountToString(r.debit.remainBalance));
                                        row++;

                                        prevId = r.debit.id;
                                    }
                                }
                            }
                    

                            /*foreach (Payment p in client.payments)
                            {
                                DateTime date = new DateTime(p.Date.Year, p.Date.Month, p.Date.Day, 12, 0, 0);
                                double days = Math.Abs(date.Subtract(today).TotalDays);

                                ws.get_Range("A" + row).set_Value(Type.Missing, client.clientName);
                                ws.get_Range("B" + row).set_Value(Type.Missing, p.DocChqNo);
                                ws.get_Range("C" + row).set_Value(Type.Missing, p.Date.ToOADate());
                                ws.get_Range("D" + row).set_Value(Type.Missing, days.ToString("0"));
                                ws.get_Range("E" + row).set_Value(Type.Missing, p.CollectingAmount.ToString("0.00"));
                                row++;
                            }*/

                            ws.get_Range("A" + row).set_Value(Type.Missing, coll.client.name + " Total");
                            ws.get_Range("B" + row).set_Value(Type.Missing, "");
                            ws.get_Range("C" + row).set_Value(Type.Missing, "");
                            ws.get_Range("D" + row).set_Value(Type.Missing, "");
                            ws.get_Range("E" + row).set_Value(Type.Missing, "=SUBTOTAL(9,E" + startingRow + ":E" + (row - 1) + ")");
                            ws.get_Range("A" + row + ":E" + row).Font.Bold = true;
                            row++;
                            action = () =>
                            {
                                //waitingDialog.getProgressBar().Value++;
                                frmProcess.getInstance().pbProcess.Value++;
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
                        ws.get_Range("C1").EntireColumn.NumberFormat = Job.FMT_SYSTEM_SHORTDATE;
                        ws.get_Range("D1").EntireColumn.AutoFit();
                        ws.get_Range("E1").EntireColumn.AutoFit();

                        ws.get_Range("D1").EntireColumn.NumberFormat = "0.00";
                        ws.get_Range("E1").EntireColumn.NumberFormat = "0.00";

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
                        String err = "Unable to perform threadPrintCollectionList operation.";
                        Job.Log(err, excep);
                        MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    #endregion
                }

                #endregion
            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            String title = "Exporting to Excel";
            String msg = "Generating excel file...";
            if (print)
            {
                title = "Printing collection list...";
                msg = "Preparing list...";
            }
            new frmProcess(title, msg, false, (c) => { }).ShowDialog(this);

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lvClients.GetItemCount() > 0)
            {
                func_collectionList(true, false);
            }
            else
            {
                MessageBox.Show(this, "Please generate collection list first and after that you can print or export collection list report.", "Collection List", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSendCollectionSMS_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                System.Collections.IList colls = null;

                Action act = () =>
                {
                    colls = lvClients.CheckedObjects;
                };
                Invoke(act);

                if (colls.Count == 0)
                {
                    act = () =>
                    {
                        MessageBox.Show(this, "Sorry, no client selected.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        frmProcess.publicClose();
                    };
                    Invoke(act);
                }
                else
                {
                    List<classes.MessageHolder> holders = new List<classes.MessageHolder>();
                    DateTime todayDate = DateTime.Today;
                    foreach (classes.collection_list.Collection c in colls)
                    {
                        holders.Add(Job.Messages.prepareCollection(c));
                    }

                    act = () =>
                    {
                        frmProcess.getInstance().lblMsg.Text = "Opening Message Sender...";
                        new forms.frmMessageSender(holders).ShowDialog(this);
                        frmProcess.publicClose();
                    };
                    Invoke(act);
                }

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Collection SMS/E-mails", "Initializing...", true, (fc) => { }).ShowDialog();
        }

    }
}
