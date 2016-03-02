using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly.forms
{
    public partial class frmSchemes : Form
    {
        private System.Collections.Hashtable mouRows = new System.Collections.Hashtable();

        public frmSchemes()
        {
            InitializeComponent();

            BrightIdeasSoftware.AspectToStringConverterDelegate cmt = (r) =>
            {
                if (r == null || (r != null && !(r is double))) return "#ERR";
                return Job.Functions.MTToString(((double)r));
            };

            this.olvColumnMoUQty.AspectToStringConverter = cmt;
            this.olvColumnMonthAvg.AspectToStringConverter = cmt;
            this.olvColumnMonthMin.AspectToStringConverter = cmt;
            this.olvColumnQuarterAvg.AspectToStringConverter = cmt;
            this.olvColumnQuarterMin.AspectToStringConverter = cmt;
            this.olvColumnYearMin.AspectToStringConverter = cmt;
            this.olvColumnMonthPending.AspectToStringConverter = cmt;
            this.olvColumnQuarterPending.AspectToStringConverter = cmt;
            this.olvColumnAnnualPending.AspectToStringConverter = cmt;
            this.olvColumnApril.AspectToStringConverter = cmt;
            this.olvColumnMay.AspectToStringConverter = cmt;
            this.olvColumnJun.AspectToStringConverter = cmt;
            this.olvColumnQ1Total.AspectToStringConverter = cmt;
            this.olvColumnJuly.AspectToStringConverter = cmt;
            this.olvColumnAug.AspectToStringConverter = cmt;
            this.olvColumnSep.AspectToStringConverter = cmt;
            this.olvColumnQ2Total.AspectToStringConverter = cmt;
            this.olvColumnOct.AspectToStringConverter = cmt;
            this.olvColumnNov.AspectToStringConverter = cmt;
            this.olvColumnDec.AspectToStringConverter = cmt;
            this.olvColumnQ3Total.AspectToStringConverter = cmt;
            this.olvColumnJan.AspectToStringConverter = cmt;
            this.olvColumnFeb.AspectToStringConverter = cmt;
            this.olvColumnMarch.AspectToStringConverter = cmt;
            this.olvColumnQ4Total.AspectToStringConverter = cmt;
            this.olvColumnYearlyTotal.AspectToStringConverter = cmt;
            this.olvColumnYearAvg.AspectToStringConverter = cmt;

            this.lvData.UseCellFormatEvents = true;
            this.lvData.FormatCell += lvData_FormatCell;
        }

        void lvData_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            if (e.Column == olvColumnMonthAvg || e.Column == olvColumnQuarterAvg || e.Column == olvColumnYearAvg)
            {
                e.SubItem.BackColor = Color.DarkOrange;
                e.SubItem.ForeColor = Color.White;
                e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Regular);
                return;
            }

            if (e.Column == olvColumnMonthMin || e.Column==olvColumnQuarterMin || e.Column==olvColumnYearMin)
            {
                e.SubItem.BackColor = Color.Blue;
                e.SubItem.ForeColor = Color.White;
                e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                return;
            }

            if (e.Column == olvColumnMonthPending || e.Column == olvColumnQuarterPending || e.Column == olvColumnAnnualPending)
            {
                e.SubItem.BackColor = Color.Yellow;
                e.SubItem.ForeColor = Color.Black;
                e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                return;
            }

            classes.mou_report.Row row = (classes.mou_report.Row)e.Model;

            if (row == null) return;

            if (e.Column == olvColumnYearlyTotal)
            {
                double yearQty = (double)e.CellValue;

                if (yearQty < row.year_min)
                {
                    e.SubItem.BackColor = Color.Red;
                    e.SubItem.ForeColor = Color.White;
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold | FontStyle.Italic);
                    return;
                }
                else
                {
                    e.SubItem.BackColor = Color.Green;
                    e.SubItem.ForeColor = Color.White;
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold | FontStyle.Italic);
                    return;
                }
            }


            int activeMonth = row.current_month;
            int firstMonth = row.fromDate.Month;
            int lastMonth = row.toDate == row.lastDate ? 3 : row.toDate.AddMonths(-1).Month;

            if (e.Column == olvColumnQ1Total || e.Column == olvColumnQ2Total || e.Column == olvColumnQ3Total || e.Column == olvColumnQ4Total)
            {
                double qtrQty = (double)e.CellValue;

                int quarterOfFirstMonth = row.quarterOfMonth(row.fromDate.Month);
                int quarterOfLastMonth = row.quarterOfMonth(activeMonth);//row.quarterOfMonth(row.toDate.AddMonths(-1).Month);

                switch (quarterOfFirstMonth)
                {
                    case 4:
                        if (e.Column == olvColumnQ1Total || e.Column == olvColumnQ2Total || e.Column == olvColumnQ3Total)
                            return;
                        break;
                    case 3:
                        if (e.Column == olvColumnQ1Total || e.Column == olvColumnQ2Total)
                            return;
                        break;
                    case 2:
                        if (e.Column == olvColumnQ1Total)
                            return;
                        break;
                }

                switch (quarterOfLastMonth)
                {
                    case 1:
                        if (e.Column == olvColumnQ2Total || e.Column == olvColumnQ3Total || e.Column == olvColumnQ4Total)
                            return;
                        break;
                    case 2:
                        if (e.Column == olvColumnQ3Total || e.Column == olvColumnQ4Total)
                            return;
                        break;
                    case 3:
                        if (e.Column == olvColumnQ4Total)
                            return;
                        break;
                }

                if (qtrQty < row.qtr_min)
                {
                    e.SubItem.BackColor = Color.Red;
                    e.SubItem.ForeColor = Color.White;
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    return;
                }
                else
                {
                    e.SubItem.BackColor = Color.Green;
                    e.SubItem.ForeColor = Color.White;
                    e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    return;
                }
            }

            if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
            {
                double monthQty = (double)e.CellValue;

                switch (firstMonth)
                {
                    case 3:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb)
                            return;
                        break;
                    case 2:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan)
                            return;
                        break;
                    case 1:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec)
                            return;
                        break;

                    case 12:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov)
                            return;
                        break;
                    case 11:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct)
                            return;
                        break;
                    case 10:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep)
                            return;
                        break;
                    case 9:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug)
                            return;
                        break;
                    case 8:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly)
                            return;
                        break;
                    case 7:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay || e.Column == olvColumnJun)
                            return;
                        break;
                    case 6:
                        if (e.Column == olvColumnApril || e.Column == olvColumnMay)
                            return;
                        break;
                    case 5:
                        if (e.Column == olvColumnApril)
                            return;
                        break;

                }

                switch (activeMonth)
                {
                    case 4:
                        if (e.Column == olvColumnMay || e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 5:
                        if (e.Column == olvColumnJun || e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 6:
                        if (e.Column == olvColumnJuly || e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 7:
                        if (e.Column == olvColumnAug || e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 8:
                        if (e.Column == olvColumnSep || e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 9:
                        if (e.Column == olvColumnOct || e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 10:
                        if (e.Column == olvColumnNov || e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 11:
                        if (e.Column == olvColumnDec || e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 12:
                        if (e.Column == olvColumnJan || e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 1:
                        if (e.Column == olvColumnFeb || e.Column == olvColumnMarch)
                            return;
                        break;
                    case 2:
                        if (e.Column == olvColumnMarch)
                            return;
                        break;
                }

                if (monthQty < row.month_min)
                {
                    e.SubItem.BackColor = Color.Red;
                    e.SubItem.ForeColor = Color.White;
                    //e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    return;
                }
                else
                {
                    e.SubItem.BackColor = Color.Green;
                    e.SubItem.ForeColor = Color.White;
                    //e.SubItem.Font = new Font(e.SubItem.Font, FontStyle.Bold);
                    return;
                }
            }
        }

        private void btnAddScheme_Click(object sender, EventArgs e)
        {
            new forms.frmNewScheme().ShowDialog(this);
            loadSchemes();
        }

        public void loadSchemes()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {

                Action act = () => { cmbSchemes.Items.Clear(); };
                Invoke(act);

                List<String> year = new List<String>();

                Job.Schemes.allSchemes(false, 0, (classes.Scheme s) =>
                {
                    act = () =>
                    {
                        if (!year.Contains(s.year.ToString()))
                        {
                            cmbSchemes.Items.Add(new ComboItem(s.year + "-" + (s.year + 1), s.year));
                            year.Add(s.year.ToString());
                        }
                    };
                    Invoke(act);
                });

                act = () =>
                {
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading schemes...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void frmSchemes_Load(object sender, EventArgs e)
        {

        }

        private void frmSchemes_Shown(object sender, EventArgs e)
        {
            loadSchemes();
        }

        private void btnEditScheme_Click(object sender, EventArgs e)
        {
            new forms.frmModifySchemes().ShowDialog(this);
            loadSchemes();
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            new forms.frmSchemeData().ShowDialog(this);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            if (cmbSchemes.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select scheme to generate APP/MoU report.", "No Scheme", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                cmbSchemes.Focus();
                return;
            }

            ComboItem item = cmbSchemes.SelectedItem as ComboItem;
            lvGroups.ClearObjects();
            lvData.ClearObjects();
            mouRows.Clear();
            int year = (int)item.Value;
            DateTime month = dtAMonth.Value;

            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                #region Calculation Process

                List<classes.GradeGroup> gradeGroups = new List<classes.GradeGroup>();
                List<classes.Scheme> schemes = Job.Schemes.getSchemesFromYear(year);

                DateTime validateDate = new DateTime(month.Year, month.Month, 1, 12, 0, 0);
                validateDate = validateDate.AddMonths(1);

                foreach (classes.Scheme scheme in schemes) // it may also client wise
                {
                    scheme.parameters = Job.Schemes.getSchemeParameters(scheme.id, validateDate);

                    Hashtable tblOfGroups = new Hashtable();

                    #region Grade-Group Separation
                    foreach (classes.Scheme.Params param in scheme.parameters)
                    {
                        if (!tblOfGroups.ContainsKey(param.group_id))
                        {
                            tblOfGroups.Add(param.group_id, new List<classes.Scheme.Params>());
                        }

                        if (tblOfGroups.ContainsKey(param.group_id))
                        {
                            ((List<classes.Scheme.Params>)tblOfGroups[param.group_id]).Add(param);
                        }
                    }
                    #endregion

                    #region Process for each gradeGroup as well as initiate groupGap

                    foreach (long groupId in tblOfGroups.Keys)
                    {
                        classes.GradeGroup gradeGroup=Job.GradeGroups.get(groupId);

                        if(gradeGroup!=null) 
                        {
                            if (gradeGroups.Find(x => (x.id == gradeGroup.id))==null) //if(!gradeGroups.Contains(gradeGroup))
                            {
                                gradeGroups.Add(gradeGroup);
                            }

                            List<classes.Scheme.Params> parameters = (List<classes.Scheme.Params>)tblOfGroups[groupId];
                            classes.mou_report.Row row = null;
                            foreach (classes.Scheme.Params parameter in parameters)
                            {
                                classes.mou_report.Gap lastGap = new classes.mou_report.Gap(year, month.Month, parameter, gradeGroup, parameter.month, parameter.toMonth);
                                lastGap.getMonthCounts(scheme.client_id);

                                row = new classes.mou_report.Row(ref row, scheme.client_name, lastGap);
                                row.client_id = scheme.client_id;

                                if (!mouRows.ContainsKey(gradeGroup.id))
                                {
                                    mouRows.Add(gradeGroup.id, new List<classes.mou_report.Row>());
                                }

                                ((List<classes.mou_report.Row>)mouRows[gradeGroup.id]).Add(row);

                            }
                        }
                        
                    }

                    #endregion

                }

                #endregion

                Invoke(new Action(() => {
                    lvGroups.SetObjects(gradeGroups);
                    frmProcess.publicClose();
                    lvGroups.Focus();
                    if (lvGroups.GetItemCount() > 0)
                    {
                        lvGroups.SelectedIndex = 0;
                    }
                }));
            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("APP/MoU Report", "Initializing...", true, (fc) => { }).ShowDialog(this);
        }

        private void frmSchemes_SizeChanged(object sender, EventArgs e)
        {
            if (panForm.Visible && panForm.Controls.Count == 1)
            {
                if (panForm.Controls[0] is Form)
                {
                    Form form = panForm.Controls[0] as Form;
                    form.WindowState = FormWindowState.Normal;
                    form.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void lvGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvGroups.SelectedObjects.Count == 1)
            {
                classes.GradeGroup group = lvGroups.SelectedObjects[0] as classes.GradeGroup;

                if (group != null)
                {
                    List<classes.mou_report.Row> selRows = null;
                    try
                    {
                        selRows = (List<classes.mou_report.Row>)mouRows[group.id];
                    }
                    catch (Exception ex) { }

                    if (selRows != null)
                    {
                        lvData.SetObjects(selRows);
                        #region column-adjustment
                        olvColumnMoUQty.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnMonthAvg.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnMonthMin.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQuarterAvg.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQuarterMin.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnYearAvg.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnYearMin.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnMonthPending.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQuarterPending.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnAnnualPending.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        
                        olvColumnApril.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnMay.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnJun.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQ1Total.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                        olvColumnJuly.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnAug.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnSep.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQ2Total.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                        olvColumnOct.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnNov.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnDec.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQ3Total.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                        olvColumnJan.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnFeb.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnMarch.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        olvColumnQ4Total.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);

                        olvColumnYearlyTotal.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        #endregion
                    }

                }

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            List<classes.MessageHolder> holders = new List<classes.MessageHolder>();
            foreach (long groupId in mouRows.Keys)
            {
                classes.GradeGroup gradeGroup = Job.GradeGroups.get(groupId);

                if (gradeGroup == null) continue;

                List<classes.mou_report.Row> rows = (List<classes.mou_report.Row>)mouRows[groupId];

                if (rows != null)
                {
                    List<long> clientIds = new List<long>();
                    
                    foreach (classes.mou_report.Row row in rows)
                    {
                        if (clientIds.Contains(row.client_id)) continue;
                        clientIds.Add(row.client_id);
                        classes.mou_report.Row lastRow = rows.FindLast(x => (x.client_id == row.client_id));
                        if (lastRow != null)
                        {
                            classes.MessageHolder holder = getMoU_MessageHolder(lastRow, row.client_id, gradeGroup.name);
                            if (holder != null)
                                holders.Add(holder);
                        }
                    }

                }

            }

            new forms.frmMessageSender(holders).ShowDialog(this);

        }

        public classes.MessageHolder getMoU_MessageHolder(classes.mou_report.Row lastRow, long clientId, string gradeGroup)
        {
            if (lastRow != null)
            {
                DateTime today = dtAMonth.Value;

                double monthPending = lastRow.month_pending;
                double quarterPending = lastRow.quarter_pending;
                double yearPending = lastRow.annual_pending;

                double selectedAmount = monthPending;

                if (today.Month == 6 || today.Month == 9 || today.Month == 12)
                {
                    if (quarterPending > selectedAmount)
                    {
                        selectedAmount = quarterPending;
                    }
                }
                else if (today.Month == 3)
                {
                    if (quarterPending > selectedAmount)
                    {
                        selectedAmount = quarterPending;
                    }
                    if (yearPending > selectedAmount)
                    {
                        selectedAmount = yearPending;
                    }
                }

                return Job.Messages.prepareMOU(clientId, gradeGroup, selectedAmount);
                //selRows.Add(lastRow);
            }
            return null;
        }

        private void lvData_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
            }
        }

        private void lvData_CellClick(object sender, BrightIdeasSoftware.CellClickEventArgs e)
        {
            if (e.ClickCount > 1 && e.ColumnIndex > 1 && e.RowIndex > -1 && e.Model!=null)
            {
                classes.mou_report.Row row = (classes.mou_report.Row)e.Model;
                int month = 0;
                switch (e.ColumnIndex)
                {
                    case 11: month = 4; break;
                    case 12: month = 5; break;
                    case 13: month = 6; break;
                        //14
                    case 15: month = 7; break;
                    case 16: month = 8; break;
                    case 17: month = 9; break;
                        //18
                    case 19: month = 10; break;
                    case 20: month = 11; break;
                    case 21: month = 12; break;
                        //22
                    case 23: month = 1; break;
                    case 24: month = 2; break;
                    case 25: month = 3; break;
                }

                if (month > 0)
                {
                    DateTime nowDate = row.nowDate;
                    frmSchemeData sd = new frmSchemeData();
                    //sd.cmbClients.SelectedItem = new ComboItem(row.client_name, row.client_id);
                    sd.cmbClients.Text = row.client_name;
                    sd.cmbGroups.Text = row.gradeGroup == null ? "" : row.gradeGroup.name;
                    sd.dtFrom.Value = new DateTime(nowDate.Year, month, 1, 12, 0, 0);
                    sd.dtTo.Value = sd.dtFrom.Value.AddMonths(1);
                    sd.autoLoad = true;
                    sd.ShowDialog(this);
                }

            }
        }

        private void lvData_CellRightClick(object sender, BrightIdeasSoftware.CellRightClickEventArgs e)
        {
            if (lvData.SelectedObjects.Count > 0 && e.Model!=null)
            {
                ContextMenu cm = new ContextMenu();
                MenuItem item = new MenuItem("&Send individual messages...", reportRightClick_SendMessage);
                item.Tag = e.Model;
                cm.MenuItems.Add(item);
                cm.Show(sender as Control, e.Location);
            }          
        }

        private void reportRightClick_SendMessage(object sender, EventArgs e)
        {
            MenuItem item = sender as MenuItem;
            if (item != null && item.Tag != null && item.Tag is classes.mou_report.Row)
            {
                classes.mou_report.Row row=(classes.mou_report.Row)item.Tag;
                new frmMessageSender(new List<classes.MessageHolder>() { getMoU_MessageHolder(row, row.client_id, row.gradeGroup.name) }).ShowDialog(this);
            }
        }

    }
}