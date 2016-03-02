using BrightIdeasSoftware;
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
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace NNPPoly.forms.reports
{
    public partial class frmReport1 : Form
    {
        private DateTime selectedMonth;
        private long clientId = -1;
        private ReportView reportView = ReportView.HTMLView;
        public List<Color> colors = new List<Color>();        
        private bool autoDisplay = false;

        public bool Report2
        {
            get
            {
                if (Job.mainForm != null)
                {
                    if (Job.mainForm.fClients != null)
                        return Job.mainForm.fClients.isReport2;
                }
                return false;
            }

            set
            {
                if (Job.mainForm != null)
                {
                    if (Job.mainForm.fClients != null)
                        Job.mainForm.fClients.isReport2 = value;
                }
            }
        }

        public enum ReportView
        {
            SimpleView,
            HTMLView
        }

        public frmReport1()
        {
            InitializeComponent();
        }

        public frmReport1(List<Color> colors)
        {
            InitializeComponent();
            if (colors != null)
                this.colors = colors;
        }

        public frmReport1(List<Color> colors, int month, int year, long clientId)
        {
            InitializeComponent();
            if (colors != null)
                this.colors = colors;
            showReportOf(month, year, clientId, false);
            this.autoDisplay = true;
        }

        private void frmReport1_Load(object sender, EventArgs e)
        {
            hideAllOthers();
        }

        private void frmReport1_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(() => {
                Action a;
                Job.Report1.generateColumns((OLVColumn c) => {
                    a = () =>
                    {
                        lvSimpleView.Columns.Add(c);
                    };
                    Invoke(a);
                });

                if (colors.Count == 0)
                {
                    #region generate color enumaration
                    try
                    {
                        Decimal colorCounter = 0;
                        foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
                            if (property.PropertyType == typeof(System.Drawing.Color))
                            {
                                a = () =>
                                {
                                    colors.Add(Color.FromName(property.Name));
                                };
                                Invoke(a);
                                colorCounter++;
                            }
                    }
                    catch (Exception excep)
                    {
                        String err = "Unable to collect colors_enumeration operation.";
                        Job.Log(err, excep);
                        MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    #endregion
                }

                a = () => {
                    //showThisPan(panSimpleView);
                    if (panProcess.Visible)
                        showLoading("Generating Report...");

                    if (autoDisplay)
                    {
                        showReport(false);
                    }

                };
                Invoke(a);
            });
            thread.Start();
            showLoading("Initializing report viewer...");
            assignContextMenu();
        }

        private void assignContextMenu()
        {
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add(new MenuItem("&Print All (Report+Debit Note)", func_printAll));
            cm.MenuItems.Add(new MenuItem("-"));
            cm.MenuItems.Add(new MenuItem("&Print directly (via Default Printer)",func_printDirectly));
            cm.MenuItems.Add(new MenuItem("&Print Preview", func_printPreview));
            cm.MenuItems.Add(new MenuItem("-"));
            MenuItem menu1 = new MenuItem("&Export to");
            menu1.MenuItems.Add(new MenuItem("&Excel (.xls)", func_exportToExcel));
            menu1.MenuItems.Add(new MenuItem("&Web Page (.html)", func_exportToHtml));
            menu1.MenuItems.Add(new MenuItem("-"));
            cm.MenuItems.Add(menu1);
            cm.MenuItems.Add(new MenuItem("-"));
            cm.MenuItems.Add(new MenuItem("&Next Month >> ", func_nextMonth));
            cm.MenuItems.Add(new MenuItem("<< &Previous Month", func_prevMonth));
            cm.MenuItems.Add(new MenuItem("-"));

            MenuItem menu = new MenuItem("&Debit note");
            menu.MenuItems.Add(new MenuItem("&Print directly (via Default Printer)",func_printDebitNote));
            menu.MenuItems.Add(new MenuItem("-"));
            menu.MenuItems.Add(new MenuItem("&Save as", func_saveAsDebitNote));
            cm.MenuItems.Add(menu);

            htmlViewer.ContextMenu = cm;
        }

        public void func_exportToHtml(object sender, EventArgs e)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.Title = "Export to Web page";
            sd.Filter = "Web Page|*.html";
            if (sd.ShowDialog(this) == DialogResult.No)
                return;
            bool suc = false;
            try
            {
                File.WriteAllText(sd.FileName, htmlViewer.Text);
                suc = true;
            }
            catch (Exception ex)
            {
                Job.Log("Unable to write Web Page", ex);
            }
            finally
            {
                if (suc)
                {
                    MessageBox.Show(this, "Web page exported.", "Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Sorry, unexpected error occured, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        public void func_exportToExcel(object sender, EventArgs e)
        {
            printReport(false, false, true);
        }

        public void func_printAll(object sender, EventArgs e)
        {
            func_printDirectly(sender, e);
            func_printDebitNote(sender, e);
        }

        public void func_printDirectly(object sender, EventArgs e)
        {
            printReport(false, true,false);
        }

        public void func_printPreview(object sender, EventArgs e)
        {
            printReport(true, false, false);
        }

        private void printReport(bool printPreviewWindow, bool printDirectly,bool exportExcel)
        {

            String filename = "";
            if (exportExcel)
            {
                SaveFileDialog sd = new SaveFileDialog();
                sd.Title = "Export to Excel";
                sd.Filter = "Microsoft Excel|*.xls;*.xlsx";
                sd.FileName = "Report_" + Job.mainForm.fClients.getCurrentClient().name + "_" + selectedMonth.ToString("MMMM-yyyy");
                if (sd.ShowDialog(this) == DialogResult.No)
                    return;
                filename = sd.FileName;
            }
            Thread thread = new Thread(() =>
            {
                #region Excel Initiation
                try
                {
                    Excel.Application app = new Excel.Application();

                    Excel.Workbook wb = app.Workbooks.Add();
                    Excel.Worksheet ws = wb.Worksheets.Add();
                    ws.Name = selectedMonth.ToString("MMMM yyyy");

                    int count = 1;

                    //header
                    String range = "A1:O2";
                    var mv = Type.Missing;
                    ws.get_Range(range, mv).Merge();
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, Job.mainForm.fClients.getCurrentClient().name + " - " + selectedMonth.ToString("MMMM yyyy"));
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "A3:B3";
                    ws.get_Range(range, mv).Merge();
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "INTEREST UPTO");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "C3:F3";
                    ws.get_Range(range, mv).Merge();
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "RIL DESPATCH");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "G3:J3";
                    ws.get_Range(range, mv).Merge();
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "ACTUAL PAYMENTS");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "K3";
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "TOTAL");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "L3";
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "LESS");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "M3:N3";
                    ws.get_Range(range, mv).Merge();
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "INTEREST");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    range = "O3";
                    ws.get_Range(range, mv).Merge();
                    ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    ws.get_Range(range, mv).Font.Bold = true;
                    ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                    ws.get_Range(range, mv).set_Value(mv, "CD");
                    ws.get_Range(range, mv).EntireColumn.AutoFit();

                    //ws.get_Range("A1:O2").Font.Size = 18;
                    //ws.get_Range("A3:O3").Font.Size = 15;

                    ws.get_Range("A4").set_Value(Type.Missing, "NEW DATE");
                    ws.get_Range("B4").set_Value(Type.Missing, "LAST DATE");
                    ws.get_Range("C4").set_Value(Type.Missing, "DATE");
                    ws.get_Range("D4").set_Value(Type.Missing, "INVOICE NO");
                    ws.get_Range("E4").set_Value(Type.Missing, "MT");
                    ws.get_Range("F4").set_Value(Type.Missing, "AMT");
                    ws.get_Range("G4").set_Value(Type.Missing, "CRDT AMT");
                    ws.get_Range("h4").set_Value(Type.Missing, "ADJ. AMT");
                    ws.get_Range("i4").set_Value(Type.Missing, "CH NO");
                    ws.get_Range("j4").set_Value(Type.Missing, "DATE");
                    ws.get_Range("k4").set_Value(Type.Missing, "DAYS");
                    ws.get_Range("l4").set_Value(Type.Missing, "DAYS");
                    ws.get_Range("m4").set_Value(Type.Missing, "DAYS");
                    ws.get_Range("n4").set_Value(Type.Missing, "AMT");
                    ws.get_Range("o4").set_Value(Type.Missing, "REVERSAL");

                    ws.get_Range("A1:O4").Font.Bold = true;
                    //ws.get_Range("A4:O4").Font.Size = 15;
                    ws.get_Range("A3:O4").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range("A3:O4").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    ws.get_Range("a3:o4").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    ws.get_Range("a3:o4").Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ws.get_Range("a3:o4").Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                    ws.get_Range("a3:o4").Borders.Color = Excel.XlRgbColor.rgbBlack;
                    ws.get_Range("A3:O4").BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlMedium,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);
                    ws.get_Range("A4:O4").BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlMedium,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);

                    int rowid = 5; // done
                    #region assignee loop
                    foreach (classes.report1.Row row in lvSimpleView.Objects)
                    {
                        if (row == null)
                        {
                            //ws.get_Range("A" + rowid + ":O" + rowid, mv).Merge();
                            //ws.get_Range("A" + rowid + ":O" + rowid, mv).RowHeight = 10;
                            rowid++;
                            continue;
                        }
                        try
                        {
                            object obj = row.newdate;
                            if (row.newdate != null)
                                obj = row.newdate.Value.ToOADate();
                            ws.get_Range("A" + rowid).set_Value(Type.Missing, obj);
                            ws.get_Range("A" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        }
                        catch (Exception) { }
                        try
                        {
                            object obj = row.lastdate;
                            if (row.lastdate != null)
                                obj = row.lastdate.Value.ToOADate();
                            ws.get_Range("B" + rowid).set_Value(Type.Missing, obj);
                            ws.get_Range("b" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        }
                        catch (Exception) { }



                        if (row.rowtype != classes.report1.RowType.PartialCreditAdjustment)
                        {

                            try
                            {
                                ws.get_Range("C" + rowid).set_Value(Type.Missing, row.debit_date);
                                ws.get_Range("c" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            }
                            catch (Exception) { }

                            try
                            {
                                object obj = row.debit_date;
                                if (row.debit_date != null)
                                    obj = ((DateTime)row.debit_date).ToOADate();
                                ws.get_Range("C" + rowid).set_Value(Type.Missing, obj);
                                ws.get_Range("c" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            }
                            catch (Exception) { }

                            if (row.rowtype == classes.report1.RowType.RemainingDebitBalanceRow)
                            {
                                ws.get_Range("D" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                ws.get_Range("D" + rowid).Font.Bold = true;
                            }
                            else
                            {
                                ws.get_Range("D" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                            }
                            ws.get_Range("D" + rowid).set_Value(Type.Missing, row.debit_invoice);
                        
                            ws.get_Range("E" + rowid).set_Value(Type.Missing, row.debit_mt);
                            ws.get_Range("e" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                            ws.get_Range("F" + rowid).set_Value(Type.Missing, row.debit_amount);
                            ws.get_Range("f" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        
                        }
    
                        ws.get_Range("G" + rowid).set_Value(Type.Missing, row.credit_amt);
                        ws.get_Range("g" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        ws.get_Range("h" + rowid).set_Value(Type.Missing, row.credt_adjusted_amt);
                        ws.get_Range("h" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        ws.get_Range("i" + rowid).set_Value(Type.Missing, row.credit_invoice);
                        ws.get_Range("i" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                        try
                        {
                            ws.get_Range("j" + rowid).set_Value(Type.Missing, row.credit.date.ToOADate());
                            ws.get_Range("j" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        }
                        catch (Exception) { }

                        ws.get_Range("k" + rowid).set_Value(Type.Missing, row.totaldays);
                        ws.get_Range("k" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        ws.get_Range("l" + rowid).set_Value(Type.Missing, row.lessdays);
                        ws.get_Range("l" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        ws.get_Range("m" + rowid).set_Value(Type.Missing, row.duedays);
                        ws.get_Range("m" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        ws.get_Range("n" + rowid).set_Value(Type.Missing, row.intamt);
                        ws.get_Range("n" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                        if (!row.isRegularIntRateUsed)
                            ws.get_Range("n" + rowid).Font.Underline = true;
                        ws.get_Range("o" + rowid).set_Value(Type.Missing, row.cd);
                        ws.get_Range("o" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                        for (int ii = (int)'A'; ii <= (int)'O'; ii++)
                            ws.get_Range(("" + (char)ii).ToString() + rowid).EntireColumn.AutoFit();

                        if (!printPreviewWindow)
                        {
                            range = "G" + rowid + ":J" + rowid;

                            /*ws.get_Range(range).Font.Color = System.Drawing.ColorTranslator.ToOle(li.SubItems[6].ForeColor);
                            ws.get_Range(range).Font.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                            if (li.SubItems[6].Text.Length > 0)
                                ws.get_Range(range).Interior.Color = System.Drawing.ColorTranslator.ToOle(li.SubItems[6].BackColor);
                            ws.get_Range(range).Interior.Pattern = Excel.XlPattern.xlPatternSolid;*/
                        }

                        if (row.rowtype == classes.report1.RowType.RemainingPayments)
                        {
                            range = "G" + rowid + ":J" + rowid;
                            ws.get_Range(range).Font.Italic = true;
                            ws.get_Range(range).Font.Underline = true;
                            ws.get_Range(range).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
                            ws.get_Range(range).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
                            //ws.get_Range(range).Interior.Pattern = Excel.XlPattern.xlPatternSolid;
                        }

                        rowid++;
                    }
                    #endregion
                    
                    ws.get_Range("a5:o" + (rowid + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    ws.get_Range("a5:o" + (rowid + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;
                    ws.get_Range("a5:o" + (rowid + 1)).Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                    ws.get_Range("a5:o" + (rowid + 1)).Borders.Color = Excel.XlRgbColor.rgbBlack;

                    int backupRowId = rowid;
                    range = "A5:O" + rowid;
                    //ws.get_Range(range).Font.Size = 15;
                    range = "A" + rowid + ":D" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "TOTAL DR.");
                    ws.get_Range("E" + rowid).set_Value(Type.Missing, "=SUM(E5:E" + (rowid - 1) + ")");
                    ws.get_Range("E" + rowid).NumberFormat = "0.00";
                    ws.get_Range("E" + rowid).Font.Bold = true;
                    ws.get_Range("F" + rowid).set_Value(Type.Missing, "=SUM(F1:F" + (rowid - 1) + ")");
                    ws.get_Range("F" + rowid).NumberFormat = "0.00";
                    ws.get_Range("F" + rowid).Font.Bold = true;
                    ws.get_Range("A" + rowid + ":F" + rowid).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlThin,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);

                    range = "H" + rowid;//"G" + rowid + ":H" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "=SUM(H1:H" + (rowid - 1) + ")");
                    ws.get_Range(range).NumberFormat = "0.00";
                    ws.get_Range("G" + rowid + ":O" + rowid).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlThin,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);


                    range = "I" + rowid + ":M" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "TOTAL CR.");
                    ws.get_Range("N" + rowid).set_Value(Type.Missing, "=SUM(N1:N" + (rowid - 1) + ")");
                    ws.get_Range("N" + rowid).NumberFormat = "0.00";
                    ws.get_Range("O" + rowid).set_Value(Type.Missing, "=SUM(O1:O" + (rowid - 1) + ")");
                    ws.get_Range("O" + rowid).NumberFormat = "0.00";

                    //ws.get_Range("A" + rowid + ":O" + rowid).Font.Size = 12;
                    ws.get_Range("A" + rowid + ":O" + rowid).Font.Bold = true;

                    rowid++;
                    ws.get_Range("A" + rowid + ":O" + rowid).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlThin,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);

                    range = "A" + rowid + ":E" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "AMT DUE RS.");
                    range = "F" + rowid + ":G" + rowid;// +":H" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "=F" + (rowid - 1) + "-H" + (rowid - 1));
                    ws.get_Range(range).NumberFormat = "0.00";

                    range = "H" + rowid + ":M" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "INTEREST AMT RS.");
                    range = "N" + rowid + ":O" + rowid;
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, "=N" + (rowid - 1) + "+O" + (rowid - 1));
                    ws.get_Range(range).NumberFormat = "0.00";

                    //ws.get_Range("A" + rowid + ":O" + rowid).Font.Size = 12;
                    ws.get_Range("A" + rowid + ":O" + rowid).Font.Bold = true;

                    rowid++;
                    range = "A" + rowid + ":O" + (rowid + 1);
                    ws.get_Range(range).Merge();
                    ws.get_Range(range).set_Value(Type.Missing, Job.mainForm.fClients.getCurrentClient().footext);
                    //ws.get_Range(range).Font.Size = 12;
                    ws.get_Range(range).Font.Bold = true;

                    range = "A" + backupRowId + ":O" + rowid;
                    //ws.get_Range(range).Font.Size = 18;
                    ws.get_Range(range).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range(range).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                    //ws.get_Range("e1").EntireColumn.AutoFit();
                    ws.get_Range("D1").EntireColumn.ColumnWidth = 15;
                    //ws.get_Range("F1:G1:N1:O1").EntireColumn.AutoFit();
                    ws.get_Range("A1:O" + (rowid + 1)).EntireColumn.AutoFit();
                    ws.get_Range("A1:O" + (rowid + 1)).EntireRow.AutoFit();
                    //ws.get_Range("A1:O" + rowid).Font.Name = "Arial";                
                    //ws.get_Range("A1:O" + rowid).Font.Size = 9;
                    ws.get_Range("a3:o3").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    ws.get_Range("a3:o3").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                    //ws.get_Range("D3").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                    ws.get_Range("a1:o" + (rowid + 1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlThin,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);
                    //ws.get_Range("a1:o" + (rowid + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    //ws.get_Range("a1:o" + (rowid + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;
                    //ws.get_Range("a1:o" + (rowid + 1)).Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                    //ws.get_Range("a1:o" + (rowid + 1)).Borders.Color = Excel.XlRgbColor.rgbBlack;

                    ws.get_Range("a1:b" + (rowid - 1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlMedium,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);
                    ws.get_Range("c1:f" + (rowid - 1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlMedium,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);
                    ws.get_Range("g1:j" + (rowid - 1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlMedium,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);
                    ws.get_Range("k1:o" + (rowid - 1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                            Excel.XlBorderWeight.xlMedium,
                                                            Excel.XlColorIndex.xlColorIndexAutomatic,
                                                            Excel.XlRgbColor.rgbBlack);

                    ws.get_Range("F1").EntireColumn.NumberFormat = "0.00";
                    ws.get_Range("G1").EntireColumn.NumberFormat = "0.00";
                    ws.get_Range("H1").EntireColumn.NumberFormat = "0.00";
                    ws.get_Range("N1").EntireColumn.NumberFormat = "0.00";
                    ws.get_Range("O1").EntireColumn.NumberFormat = "0.00";

                    ws.get_Range("A5").EntireColumn.NumberFormat = Job.FMT_SYSTEM_SHORTDATE;
                    ws.get_Range("B5").EntireColumn.NumberFormat = Job.FMT_SYSTEM_SHORTDATE;
                    ws.get_Range("C5").EntireColumn.NumberFormat = Job.FMT_SYSTEM_SHORTDATE;
                    ws.get_Range("J5").EntireColumn.NumberFormat = Job.FMT_SYSTEM_SHORTDATE;
                    ws.get_Range("E5").EntireColumn.NumberFormat = "0.000";


                    /*
                    ws.get_Range("A1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("b1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("c1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("e1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("f1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("g1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("h1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("j1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("k1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("l1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("m1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("n1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("o1").EntireColumn.HorizontalAlignment =
                        Excel.XlHAlign.xlHAlignRight;

                    ws.get_Range("d1").EntireColumn.HorizontalAlignment =
                        ws.get_Range("i1").EntireColumn.HorizontalAlignment =
                        Excel.XlHAlign.xlHAlignLeft;*/

                    ws.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                    ws.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                    ws.PageSetup.Zoom = 100;
                    ws.PageSetup.BottomMargin = 0.25;
                    ws.PageSetup.LeftMargin = 0.25;
                    ws.PageSetup.RightMargin = 0.25;
                    ws.PageSetup.TopMargin = 0.25;
                    ws.PageSetup.CenterHorizontally = true;
                    ws.PageSetup.CenterVertically = false;
                    ws.PageSetup.Zoom = false;
                    ws.PageSetup.FitToPagesWide = 1;
                    ws.PageSetup.FitToPagesTall = 10;

                    if (!exportExcel)
                    {
                        if (printDirectly)
                        {
                            //ws.PrintOutEx();
                            Decimal vfu = 0;
                            Action act = () =>
                            {
                                frmAskUser ask = new frmAskUser("Print Copies", "How many copies of report needed ?", "0", frmAskUser.ValueType.Long);
                                if (ask.ShowDialog(this) == DialogResult.OK)
                                {
                                    vfu = Decimal.Parse(ask.getText());
                                }
                            };
                            Invoke(act);

                            if (vfu > 0)
                            {
                                //doc.PrintOut();
                                //object background = false;
                                object missing = Type.Missing;
                                ws.PrintOutEx(missing, missing, (int)vfu, missing, missing, missing, missing, missing, missing);
                            }
                        }
                        else if (printPreviewWindow)
                        {
                            app.Visible = true;
                            bool userDidntCancel = app.Dialogs[Excel.XlBuiltInDialog.xlDialogPrintPreview].Show(
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                                Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        }
                        wb.Close(false);
                    }
                    else
                    {
                        if (filename.Trim().Length > 0)
                            wb.Close(true, filename);
                        else
                            wb.Close(false);
                    }


                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    app.Quit();

                    app.Quit();
                    app = null;
                    /*if (printDirectly == false && printPreviewWindow == false)
                        sucExportExcel();
                    printPreviewWindow = printDirectly = false;*/

                }
                catch (Exception excep)
                {
                    String err = "Unable to perform threadEXP_monthlyreport operation.";
                    Job.Log(err, excep);
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Action ac = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(ac);
                }
                #endregion
            });
            thread.Name = "Thread: printReport";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            String msg = "";
            if (printPreviewWindow)
                msg = "Generating preview...";
            else if (printDirectly)
                msg = "Printing report...";
            else
                msg = "Exporting to Excel";
            new frmProcess(msg, "", true, (c) => { }).ShowDialog(this);
        }

        public List<classes.report1.Row> getAllRows()
        {
            return lvSimpleView.Objects == null ? null : (List<classes.report1.Row>)lvSimpleView.Objects;
        }

        public void func_saveAsDebitNote(object sender, EventArgs e)
        {
            func_debitNote(false, true);
        }

        public void func_printDebitNote(object sender, EventArgs e)
        {
            func_debitNote(true, false);
        }

        private void func_debitNote(bool print, bool save)
        {
            classes.Client c = Job.Clients.get(clientId);
            String outputFilename="";
            if (save)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Save as Debit Note...";
                sfd.Filter = "Microsoft Word|*.docx";
                sfd.FileName = "DebitNote_" + c.name + "_" + DateTime.Now.ToString("dd-MM-yyyy");
                if (sfd.ShowDialog(this) == DialogResult.No)
                    return;
                outputFilename = sfd.FileName;
            }
            Thread threadPrintDD = new Thread(() =>
            {
                String tempFile = System.Windows.Forms.Application.StartupPath + "\\a4_debitnote_template.docx";
                try
                {
                    if (!File.Exists(tempFile))
                    {
                        try
                        {
                            System.IO.File.WriteAllBytes(tempFile, Properties.Resources.DebitNoteFormat);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(this, "Error in writing document file to saving location." + Environment.NewLine + "Error message:" + Environment.NewLine + ex.Message, "Document write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception excep)
                {
                    String err = "Unable to initilize template_debitnote operation.";
                    Job.Log(err, excep);
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (System.IO.File.Exists(tempFile))
                {
                    try
                    {
                        Word.Application appWord = new Word.Application();

                        Object filename = outputFilename;
                        Object boolFalse = false;
                        Word.Document doc = (Word.Document)appWord.Documents.Open(tempFile);

                        DateTime dt = new DateTime(selectedMonth.Year, selectedMonth.Month, 1, 12, 0, 0);
                        dt = dt.AddMonths(1);
                        dt = dt.AddDays(-1);
                        
                        Job.Functions.FindAndReplace(appWord, "%day%", dt.ToString("dd").ToUpper());
                        Job.Functions.FindAndReplace(appWord, "%monthno%", selectedMonth.ToString("MM").ToUpper());
                        Job.Functions.FindAndReplace(appWord, "%month%", selectedMonth.ToString("MMMM").ToUpper());
                        Job.Functions.FindAndReplace(appWord, "%year%", selectedMonth.ToString("yyyy"));
                        Job.Functions.FindAndReplace(appWord, "%name%", c.name);
                        Job.Functions.FindAndReplace(appWord, "%address%", c.about);
                        Job.Functions.FindAndReplace(appWord, "%amt%", interestDue.ToString("0") + ".00");
                        Job.Functions.FindAndReplace(appWord, "%words%", Job.Functions.NumberToWords((long)interestDue));

                        if (outputFilename.Length == 0)
                        {
                            doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4;
                            doc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;

                            if (print)
                            {
                                Decimal vfu = 0;
                                Action act = () =>
                                {
                                    frmAskUser ask = new frmAskUser("Print Copies", "How many copies of Debit Note needed ?", "0", frmAskUser.ValueType.Long);
                                    if (ask.ShowDialog(this) == DialogResult.OK)
                                    {
                                        vfu = Decimal.Parse(ask.getText());
                                    }
                                };
                                Invoke(act);

                                if (vfu > 0)
                                {
                                    //doc.PrintOut();
                                    object background = false;
                                    object missing = Type.Missing;
                                    doc.PrintOut(background, missing, missing, missing, missing, missing, missing, (int)vfu);
                                }

                                /*for (int i = 0; i < 10; i++)
                                {
                                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                                    System.Windows.Forms.Application.DoEvents();
                                }*/
                            }
                        }

                        if (outputFilename.Length > 0)
                        {
                            doc.SaveAs(outputFilename);
                        }

                        doc.Close(false);
                        appWord.Quit(false);
                    }
                    catch (Exception excep)
                    {
                        String err = "Unable to generate debitnote_exportDD operation.";
                        Job.Log(err, excep);
                        MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        Action act = () =>
                        {
                            frmProcess.publicClose();
                        };
                        Invoke(act);
                    }
                }
            });
            threadPrintDD.Name = "Thread: PrintDebitNote";
            threadPrintDD.Priority = ThreadPriority.Highest;
            threadPrintDD.Start();
            new frmProcess("Printing Debit Note", "Generating word file...", true, (c1) => { }).ShowDialog(this);
        }

        public void func_nextMonth(object sender, EventArgs e)
        {
            selectedMonth = selectedMonth.AddMonths(1);
            showReportOf(selectedMonth.Month, selectedMonth.Year, clientId);
            showReport();
        }

        public void func_prevMonth(object sender, EventArgs e)
        {
            selectedMonth = selectedMonth.AddMonths(-1);
            showReportOf(selectedMonth.Month, selectedMonth.Year, clientId);
            showReport();
        }

        public void showLoading(String txtMsg)
        {
            lblProcess.Text = txtMsg;
            lcProcess.Active = true;
            showThisPan(panProcess);
        }

        public void showThisPan(Panel pan)
        {
            hideAllOthers();
            pan.Visible = false;
            pan.Dock = DockStyle.Fill;
            pan.Visible = true;
        }

        public void hideAllOthers()
        {
            BackColor = Color.White;
            panHTMLView.Visible =
            panProcess.Visible =
            panSimpleView.Visible = false;
        }

        public bool isReportShown { get; set; }

        public void showReportOf(int month, int year, long clientId,bool reflectMonths=true)
        {
            selectedMonth = new DateTime(year, month, 1, 12, 0, 0);
            if (reflectMonths)
            {
                Job.mainForm.fClients.disableMonthButton(selectedMonth.Month);
                Job.mainForm.fClients.changeYear(selectedMonth.Year);
            }
            this.clientId = clientId;
            isReportShown = false;
        }

        public void showReport(bool onThread=true)
        {
            mtCount = debitCount = takenAmountCount = interestCount = cdCount = 0;
            amountDue = interestDue = 0;
            isReportShown = false;
            if (onThread)
            {
                Thread thread = new Thread(generateReport);
                thread.Name = "Thread: Report Generation Fork";
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                lvSimpleView.ClearObjects();
                System.GC.Collect();
                /*new frmProcess("Monthly report", "generating report...", false, (frmProcess fp) => {
                    try
                    {
                        thread.Abort();
                    }
                    catch (Exception) { }
                }).ShowDialog(this);*/
                showLoading("Generating Report ...");
            }
            else
            {
                generateReport();
            }
        }

        public void setReportView(frmReport1.ReportView view)
        {
            this.reportView = view;
            if (reportView == ReportView.HTMLView)
            {
                showThisPan(panHTMLView);
            }
            else if (reportView == ReportView.SimpleView)
            {
                showThisPan(panSimpleView);
            }
        }

        #region Report Generation

        private void generateReport()
        {
            try
            {
                try
                {
                    Action act = () => { isReportShown = true; };
                    Invoke(act);
                    List<classes.report1.Row> rows = new List<classes.report1.Row>();
                    System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                    sw.Start();
                    int selMonth=selectedMonth.Month;
                    int selYear=selectedMonth.Year;
                    classes.report1.AutoRow ar = new classes.report1.AutoRow(clientId, selMonth, selYear, ref colors);
                    ar.Report2 = false;
                    if (Job.mainForm != null)
                    {
                        if (Job.mainForm.fClients != null)
                        {
                            ar.Report2 = Job.mainForm.fClients.isReport2;
                        }
                    }


                    if (ar.Report2)
                    {
                        Invoke(new Action(() => {
                            if (lvSimpleView.Columns.Count > 0)
                                lvSimpleView.Columns[11].Width = lvSimpleView.Columns[12].Width = lvSimpleView.Columns[13].Width = 0;
                        }));
                    }
                    else
                    {
                        Invoke(new Action(() =>
                        {
                            if (lvSimpleView.Columns.Count > 0)
                            {
                                lvSimpleView.Columns[11].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                                lvSimpleView.Columns[12].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                                lvSimpleView.Columns[13].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                            }
                        }));
                    }

                    bool priorityFlag = true;
                    int indexOfLastPDebit = -1;
                    syncPoint:
                    int totalDebits=ar.startSync(priorityFlag);
                    Console.WriteLine("Total Debits:" + totalDebits);
                    long prevDebitId = -1;
                    classes.report1.Row prevRow = null;
                    while (ar.hasNext())
                    {    
                        classes.report1.Row r = ar.next();
                        if (r == null)
                        {
                            if (ar.hasNext())
                            {
                                r = ar.next(true);
                            }
                        }
                        if (r != null)
                        {
                            if (prevDebitId == -1)
                            {
                                prevDebitId = r.debit.id;
                                prevRow = r;
                            }
                            else
                            {
                                if (r.debit.id != prevDebitId && prevRow!=null)
                                {
                                    if (prevRow.debit.remainBalance != 0 && prevRow.debit.debit_amount!=prevRow.debit.remainBalance)
                                    {
                                        classes.Payment debit = prevRow.debit;
                                        classes.report1.Row row = new classes.report1.Row(ref debit, selMonth, selYear, ref ar, true);
                                        try
                                        {
                                            classes.report1.Row nrow = rows[rows.Count - 1];
                                            //Console.WriteLine("\nData:" + nrow.rowtype);
                                            if ((nrow.rowtype == classes.report1.RowType.NotAdjusted || nrow.rowtype == classes.report1.RowType.RemainingDebitBalanceRow) && nrow.credit == null)
                                                rows.RemoveAt(rows.Count - 1);
                                        }
                                        catch (Exception) { }
                                        rows.Add(row);
                                    }
                                    rows.Add(null);
                                }
                                prevDebitId = r.debit.id;
                                prevRow = r;
                            }
                            if (r.rowtype == classes.report1.RowType.RemainingDebitBalanceRow && r.debit.remainBalance != 0)
                                rows.Add(r);
                            else if (r.rowtype != classes.report1.RowType.RemainingDebitBalanceRow)
                                rows.Add(r);
                        }
                    }
                    if (priorityFlag)
                    {
                        priorityFlag = false;
                        rows.Add(null);
                        indexOfLastPDebit = rows.Count - 1;
                        goto syncPoint;
                    }

                    List<classes.report1.Row> finalRows = new List<classes.report1.Row>();

                    if (indexOfLastPDebit > -1)
                    {
                        int startIndex = indexOfLastPDebit + 1;
                        finalRows.AddRange(rows.GetRange(startIndex, rows.Count - startIndex));
                        for (int index = 0; index < startIndex; index++)
                        {
                            classes.report1.Row row1 = rows[index];
                            if (row1 != null)
                            {
                                DateTime date = row1.debit.date;

                                int foundIndex = finalRows.FindIndex(x =>
                                {
                                    if (x != null)
                                    {
                                        if (row1.debit.date < x.debit.date)//[changed because dis-order of jrnl payments in report1] && row1.debit.id < x.debit.id)
                                        {
                                            return true;
                                        }
                                    }
                                    return false;
                                });

                                // calculation code comes here for sorting problem of same 13-11-'14 of client Amar Polyfils Aug2014

                                if (foundIndex > -1)
                                {
                                    finalRows.Insert(foundIndex, row1);
                                }
                                else
                                {
                                    //if (finalRows[finalRows.Count - 1] != null)
                                        finalRows.Add(null);
                                    finalRows.Add(row1);
                                    foundIndex=finalRows.Count-1;
                                }
                                int i = index;
                                for (i = index + 1; i < startIndex; i++)
                                {
                                    classes.report1.Row tmpRow = rows[i];
                                    finalRows.Insert(++foundIndex, tmpRow);
                                    if (tmpRow == null)
                                        break;
                                }
                                index = i;
                            }
                        }
                    }
                    else
                    {
                        finalRows = rows;
                    }

                    Console.WriteLine(selMonth + ", " + selYear);

                    //int removed = finalRows.RemoveAll(x => (x != null && x.debit.date.Month != selMonth && x.debit.date.Year != selYear && (x.credit == null || (x.credit != null && x.credit.date.Month != selMonth && x.credit.date.Year != selYear))));
                    //extracting valid rows as per current month selection.
                    rows = new List<classes.report1.Row>();
                    for (int i = 0; i < finalRows.Count; i++)
                    {
                        classes.report1.Row row = finalRows[i];
                        if (row == null)
                        {
                            if (rows.Count > 0 && rows[rows.Count - 1] != null) // if last added row isn't null than add one null
                                rows.Add(null);
                            continue; // continue whenever null occures
                        }

                        if (row.credit == null) // if there is no credit, means that debitamount=remainbalance
                            rows.Add(row);
                        else if (row.debit.date.Month == selMonth && row.debit.date.Year == selYear) // if debit is of selMonth
                        {
                            rows.Add(row);
                        }
                        else if (row.debit.remainBalance > 0) // if debit isn't fully paid and remaining balance row also
                        {
                            rows.Add(row);
                        }
                        else if (row.credit != null) // if contains credit
                        {
                            if ((row.credit.date.Month == selMonth && row.credit.date.Year == selYear)) // if credit matchs to selMonth&selYear
                            {
                                //find start point of that debit and also count how many rows this debit haves
                                int rangeStart = i - 1, rangeCount = 0;
                                for (int k = i; k > -1; k--)
                                {
                                    if (finalRows[k] == null) break;
                                    rangeStart = k;
                                    rangeCount++;
                                }
                                for (int k = i + 1; k < finalRows.Count; k++)
                                {
                                    if (finalRows[k] == null) break;
                                    rangeCount++;
                                }
                                i = rangeStart + rangeCount;
                                rows.AddRange(finalRows.GetRange(rangeStart, rangeCount)); // add that debit's all rows
                                rows.Add(null);//add one blank

                                /*rows.Add(row);
                                if (row.rowtype == classes.report1.RowType.PartialCreditAdjustment || row.rowtype == classes.report1.RowType.RemainingDebitBalanceRow)
                                {
                                    int lastIndex = rows.Count - 1;
                                    for (int k = i - 1; k > -1; k--)
                                    {
                                        if (finalRows[k] == null) break;
                                        //rows.Add(finalRows[k]);
                                        rows.Insert(lastIndex, finalRows[k]);
                                    }
                                }*/
                            }
                        }

                    }

                    rows.AddRange(ar.getRemainingPayments());


                    // remove all empty space from starting of list
                    

                        /*rows.Sort((classes.report1.Row r1, classes.report1.Row r2) =>
                        {
                            if (r1 != null && r2 != null)
                            {
                                if (r1.debit.date > r2.debit.date) return 1;
                                if (r1.debit.date < r2.debit.date) return -1;
                            }
                            return -1;
                        });

                        /*
                         * idea to handle jrnl debits is to first traversal all jrnl debits and than all others.
                         * and after all that sort rows list by comparision according to date of Row of debit.
                         * 
                        rows.Sort((classes.report1.Row r1, classes.report1.Row r2) => { 
                            return 0; });
                        */
                    sw.Stop();

                    Console.WriteLine("\nTotalTime:" + sw.ElapsedMilliseconds + "(" + sw.Elapsed + ")");
                    Console.WriteLine("TotalTicks:" + sw.ElapsedTicks);

                    Job.Log("Report1 Generated in " + sw.ElapsedMilliseconds + " milis (" + sw.Elapsed + ")", new Exception("Report1Report"));

                    act = () =>
                    {
                        lvSimpleView.ClearObjects();
                        lvSimpleView.SetObjects(rows);
                        lvSimpleView.Visible = true;
                        //lvSimpleView.Sort(2);

                        htmlViewer.Text = getHTML();
                        htmlViewer.PerformLayout();
                    };
                    Invoke(act);
                }
                catch (ThreadAbortException tae) { throw tae; }
                catch (Exception ex) {
                    Job.Log("GenerateReport", ex);
                }
            }
            catch (ThreadAbortException tae)
            {
                Job.Log("Thread[GenerateReport]:Abort", tae);
            }
            finally
            {
                Action a = () =>
                {
                    frmProcess.publicClose();
                    if (reportView == ReportView.SimpleView)
                    {
                        showThisPan(panSimpleView);
                    }
                    else if (reportView == ReportView.HTMLView)
                    {
                        showThisPan(panHTMLView);
                    }
                };
                Invoke(a);
            }
        }

        public double mtCount = 0, debitCount = 0, takenAmountCount = 0, interestCount = 0, cdCount = 0;
        public double amountDue = 0, interestDue = 0;

        public String getHTML()
        {
            mtCount = debitCount = takenAmountCount = interestCount = cdCount = 0;
            amountDue = interestDue = 0;
            String html = Properties.Resources.htmlReportCode;
            if (lvSimpleView.Objects != null && lvSimpleView.Items.Count > 0)
            {
                System.Collections.IEnumerator enrs = lvSimpleView.Objects.GetEnumerator();
               
                long prevDebitId = -1;
                while (enrs.MoveNext())
                {
                    classes.report1.Row row=(classes.report1.Row)enrs.Current;
                    addTR(ref html, row);
                    if (row != null && row.rowtype != classes.report1.RowType.RemainingPayments)
                    {
                        if (prevDebitId != row.debit.id)
                        {
                            mtCount += row.debit.mt;
                            debitCount += row.debit.debit_amount;    
                        }
                        prevDebitId = row.debit.id;
                        interestCount += row.intamt == null || !(row.intamt is double?) ? 0 : Math.Round(((double?)row.intamt).Value, 0);
                        double tmp = 0;
                        if (row.cd != null && double.TryParse(row.cd, out tmp))
                        {
                            cdCount += tmp;
                        }
                        takenAmountCount += row.taken_amount == null ? 0 : row.taken_amount.Value;
                    } 
                    else if (row != null && row.rowtype==classes.report1.RowType.RemainingPayments)
                    {
                        takenAmountCount += row.taken_amount == null ? 0 : row.taken_amount.Value;
                    }
                }
            }
            classes.Client c = Job.Clients.get(clientId);
            if (c != null)
            {
                html = html.Replace("%title%", c.name + " - " + selectedMonth.ToString("MMMM yyyy"));
                html = html.Replace("%footext%", c.footext);
            }
            else
            {
                html = html.Replace("%title%", selectedMonth.ToString("MMMM yyyy"));
                html = html.Replace("%footext%", "");
            }

            html = html.Replace("%mttotal%", Job.Functions.MTToString(mtCount));
            html = html.Replace("%dtotal%", Job.Functions.AmountToString(debitCount));
            html = html.Replace("%ctotal%", Job.Functions.AmountToString(takenAmountCount));
            html = html.Replace("%itotal%", Job.Functions.FullyRoundAmount(interestCount));
            html = html.Replace("%cdtotal%", Job.Functions.FullyRoundAmount(cdCount));

            amountDue = debitCount - takenAmountCount;
            interestDue = interestCount + cdCount;

            html = html.Replace("%amtdue%", Job.Functions.FullyRoundAmount(amountDue));
            html = html.Replace("%intdue%", Job.Functions.FullyRoundAmount(interestDue));

            html = html.Replace("%newtr%", "");
            return html;
        }

        private void addTR(ref String html, classes.report1.Row row)
        {
            try
            {
                if (row==null)
                {
                    html = html.Replace("%newtr%", "<tr><td bgcolor='white' colspan='15'></td></tr>" + Environment.NewLine + "%newtr%");
                }
                else
                {
                    html = html.Replace("%newtr%", Properties.Resources.htmlTRCode);
                    html = html.Replace("%NEW DATE%", row.newdate == null ? "" : row.newdate.Value.ToShortDateString());
                    html = html.Replace("%LAST DATE%", row.lastdate == null ? "" : row.lastdate.Value.ToShortDateString());
                    
                    // debit details
                    html = html.Replace("%DATE1%", row.debit_date == null || row.rowtype==classes.report1.RowType.PartialCreditAdjustment ? "" : ((DateTime)row.debit_date).ToShortDateString());
                    if (row.rowtype==classes.report1.RowType.RemainingDebitBalanceRow)
                    {
                        html = html.Replace("%INVOICE NO%", "<center><font size='20'>" + row.debit_invoice + "</font></center>");
                    }
                    else
                    {
                        html = html.Replace("%INVOICE NO%", row.rowtype == classes.report1.RowType.PartialCreditAdjustment ? "" : row.debit_invoice);
                    }
                    html = html.Replace("%MT%", row.debit_mt == null || row.rowtype == classes.report1.RowType.PartialCreditAdjustment ? "" : Job.Functions.MTToString(row.debit.mt));
                    html = html.Replace("%AMT1%", row.debit_amount == null || row.rowtype == classes.report1.RowType.PartialCreditAdjustment ? "" : Job.Functions.AmountToString(row.debit.debit_amount));
                    
                    if(row.credit!=null)
                    {
                        String start = "", end = "";
                        if (row.rowtype == classes.report1.RowType.RemainingPayments)
                        {
                            start = "<i><u>";
                            end = "</u></i>";
                        }

                        html = html.Replace("%AMT4%", start + Job.Functions.AmountToString(row.credit.credit_amount) + end);
                        html = html.Replace("%AMT2%", start + (row.taken_amount == null ? "" : Job.Functions.AmountToString(row.taken_amount.Value)) + end);
                        html = html.Replace("%CH NO%", start + row.credit.invoice + end);
                        html = html.Replace("%DATE2%", start + row.credit.date.ToShortDateString() + end);

                        if (row.credit.highlighted)
                        {

                        }
                        else
                        {
                            Color fg = Job.Functions.GetContrastedColor(row.credit.color);
                            html = html.Replace("%hclass%", "");
                            html = html.Replace("%bg%", "rgb(" + row.credit.color.R + "," + row.credit.color.G + "," + row.credit.color.B + ")");
                            html = html.Replace("%fg%", "rgb(" + fg.R + "," + fg.G + "," + fg.B + ")");
                        }
                    } 
                    else 
                    {
                        html = html.Replace("%AMT4%", "");
                        html = html.Replace("%AMT2%", "");
                        html = html.Replace("%CH NO%", "");
                        html = html.Replace("%DATE2%", "");

                        Color fg = Color.Black;
                        html = html.Replace("%hclass%", "");
                        html = html.Replace("%bg%", "rgb(" + 255 + "," + 255 + "," + 255 + ")");
                        html = html.Replace("%fg%", "rgb(" + fg.R + "," + fg.G + "," + fg.B + ")");
                    }

                    #region HTML Color
                    /*if (li.SubItems[13].Tag != null && li.SubItems[13].Tag.ToString().Equals("LATE"))
                            html = html.Replace("%AMT3%", "<u>" + li.SubItems[13].Text + "</u>");
                        else
                            html = html.Replace("%AMT3%", li.SubItems[13].Text);
                        html = html.Replace("%CD%", row.cd == null ? "" : row.cd);
                        //Color bg = li.SubItems[7].BackColor;
                        //Color fg = GetContrastedColor(bg);*/
                    /*Payment p1 = null, p2 = null;
                    p1 = li.Tag as Payment;
                    p2 = li.SubItems[6].Tag as Payment;
                    if ((p1 != null && p1.HighlightThis) || (p2 != null && p2.HighlightThis))
                    {
                        if (p1 != null && p1.HighlightThis)
                            html = html.Replace("%hclass%", "highlight1");
                        if (p2 != null && p2.HighlightThis)
                        {
                            html = html.Replace("%hclass%", "");
                            html = html.Replace("%bg%", "rgb(255,0,0)");
                            html = html.Replace("%fg%", "rgb(255,255,255)");
                        }

                    }
                    else
                    {
                        html = html.Replace("%hclass%", "");
                        html = html.Replace("%bg%", "rgb(" + bg.R + "," + bg.G + "," + bg.B + ")");
                        html = html.Replace("%fg%", "rgb(" + fg.R + "," + fg.G + "," + fg.B + ")");
                    }*/
                    #endregion

                    html = html.Replace("%TOTAL DAYS%", row.totaldays == null ? "" : row.totaldays.Value.ToString("0"));
                    html = html.Replace("%LESS DAYS%", row.lessdays == null ? "" : row.lessdays.Value.ToString("0"));
                    html = html.Replace("%DUE DAYS%", row.duedays == null ? "" : row.duedays.Value.ToString("0"));
                    String start1 = "", end1 = "";
                    if (!row.isRegularIntRateUsed)
                    {
                        start1 = "<u>";
                        end1 = "</u>";
                    }
                    html = html.Replace("%AMT3%", start1 + (row.intamt == null || !(row.intamt is double?) ? (row.intamt != null ? row.intamt + "" : "") : Job.Functions.FullyRoundAmount(((double?)row.intamt).Value)) + end1);
                    html = html.Replace("%CD%", row.cd == null ? "" : row.cd);
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform addtr_html operation.";
                Job.Log(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvSimpleView_CellEditStarting(object sender, CellEditEventArgs e)
        {
            if (e.RowObject != null)
            {
                classes.report1.Row row = ((classes.report1.Row)e.RowObject);
                if (row.debit != null)
                    row.debit.SetDataReflector = true;
                if (row.credit != null)
                    row.credit.SetDataReflector = true;
            }
        }

        private void lvSimpleView_CellEditFinishing(object sender, CellEditEventArgs e)
        {
            if (e.SubItemIndex == 2 || e.SubItemIndex == 5 || e.SubItemIndex == 6 || e.SubItemIndex == 9)
            {
                if (clientId != -1)
                {
                    //showReportOf(selectedMonth.Month, selectedMonth.Year, clientId);
                    //showReport();
                }
            }
        }

        #endregion

        private void lvSimpleView_MouseDown(object sender, MouseEventArgs e)
        {
            if (lvSimpleView.SelectedItems.Count > 0 && e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                if (lvSimpleView.SelectedItems.Count > 0)
                {
                    classes.report1.Row row=(lvSimpleView.SelectedObjects[0] as classes.report1.Row);
                    if(row==null) return;
                    int mx = Cursor.Position.X;
                    int my = Cursor.Position.Y;
                    int x = lvSimpleView.Items[0].SubItems[6].Bounds.Location.X;
                    if (mx < x && row.debit!=null)
                    {
                        //debit
                        //Payment p = lvUser.SelectedItems[0].Tag as Payment;
                        cm.MenuItems.Add(new MenuItem("&Edit entry", lvUser_RightClickAction_EditEntry_Debit));
                        cm.MenuItems.Add(new MenuItem("-"));
                        cm.MenuItems.Add(new MenuItem("&Delete selected 'Debit' entry", lvUser_RightClickAction_DebitDelete));
                    }
                    else if (mx > x && row.credit != null)
                    {
                        //credit
                        //Payment p = lvUser.SelectedItems[0].SubItems[6].Tag as Payment;
                        cm.MenuItems.Add(new MenuItem("&Edit entry", lvUser_RightClickAction_EditEntry_Credit));
                        cm.MenuItems.Add(new MenuItem("-"));
                        cm.MenuItems.Add(new MenuItem("&Delete selected 'Credit' entry", lvUser_RightClickAction_CreditDelete));
                    }
                }
                cm.Show(sender as Control, e.Location);
            }
        }

        public void lvUser_RightClickAction_EditEntry_Debit(object sender, EventArgs e)
        {
            if (lvSimpleView.SelectedObjects.Count == 1)
            {
                classes.report1.Row row = lvSimpleView.SelectedObjects[0] as classes.report1.Row;
                if (row != null && row.debit != null)
                {
                    classes.Payment p = row.debit;
                    forms.frmNewEntry ne = new frmNewEntry(row.debit.client_id);
                    ne.setEditMode(ref p);
                    ne.ShowDialog(this);
                    showReport();
                }
            }
        }

        public void lvUser_RightClickAction_EditEntry_Credit(object sender, EventArgs e)
        {
            if (lvSimpleView.SelectedObjects.Count == 1)
            {
                classes.report1.Row row = lvSimpleView.SelectedObjects[0] as classes.report1.Row;
                if (row != null && row.credit != null)
                {
                    classes.Payment p = row.credit;
                    forms.frmNewEntry ne = new frmNewEntry(row.credit.client_id);
                    ne.setEditMode(ref p);
                    ne.ShowDialog(this);
                    showReport();
                }
            }
        }

        private void lvUser_RightClickAction_DebitDelete(object sender, EventArgs e)
        {
            if (lvSimpleView.SelectedObjects.Count > 0)
            {
                if (MessageBox.Show(this, "Are you sure to delete all selected debit entries ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                foreach (classes.report1.Row row in lvSimpleView.SelectedObjects)
                {
                    //classes.report1.Row row = lvSimpleView.SelectedObjects[0] as classes.report1.Row;
                    if (row != null && row.debit != null)
                    {
                        if (true)
                        {
                            if (row.debit.Delete())
                            {
                                
                            }
                        }
                    }
                }

                showReport();
            }
        }

        private void lvUser_RightClickAction_CreditDelete(object sender, EventArgs e)
        {
            if (lvSimpleView.SelectedObjects.Count > 0)
            {
                if (MessageBox.Show(this, "Are you sure to delete all selected debit entries ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                foreach (classes.report1.Row row in lvSimpleView.SelectedObjects)
                {
                    //classes.report1.Row row = lvSimpleView.SelectedObjects[0] as classes.report1.Row;
                    if (row != null && row.credit != null)
                    {
                        if (true)
                        {
                            if (row.credit.Delete())
                            {

                            }
                        }
                    }
                }

                showReport();
            }
        }

    }
}
