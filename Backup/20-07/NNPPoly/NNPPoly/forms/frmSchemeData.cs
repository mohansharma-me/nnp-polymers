using System;
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
    public partial class frmSchemeData : Form
    {
        private List<ComboItem> clientItems = new List<ComboItem>();
        private List<ComboItem> gradeItems = new List<ComboItem>();

        public frmSchemeData()
        {
            InitializeComponent();

            olvColumn2.AspectToStringConverter = (d) =>
            {
                return ((DateTime)d).ToShortDateString();
            };

            olvColumn4.AspectToStringConverter = (q) =>
            {
                return Job.Functions.MTToString(((double)q));
            };
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnImportData_Click(object sender, EventArgs e)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Filter = "Excel Files|*.xls;*.xlsx|All files|*.*";
            od.CheckFileExists = true;
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                String filename = od.FileName;

                System.Threading.Thread thread = new System.Threading.Thread(() =>
                {

                    Action act;
                    try
                    {
                        #region ExcelWorld
                        Excel.Application app = new Excel.Application();
                        Excel.Workbook wb = app.Workbooks.Open(filename);

                        System.Collections.Specialized.StringDictionary sdic = new System.Collections.Specialized.StringDictionary();

                        List<String> skipedClients = new List<String>();
                        int clientSkipCount = 0;
                        bool breakAll = false;

                        foreach (Excel.Worksheet ws in wb.Worksheets)
                        {
                            try
                            {
                                if (breakAll) break;

                                #region SheetExtraction
                                Excel.Range range = ws.UsedRange;
                                for (int i = 1; i < range.Rows.Count; i++)
                                {
                                    if (clientSkipCount > 3)
                                    {
                                        bool flag = false;
                                        Invoke(new Action(() =>
                                        {
                                            flag = MessageBox.Show(this, "You have skiped last three clients, are you want to cancel current operation at all ?", "Cancel Importing", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes;
                                        }));

                                        if (flag)
                                        {
                                            breakAll = true;
                                            break;
                                        }
                                    }

                                    int curRow = i + 1;
                                    object partyName, date, gradeTitle, qty;
                                    partyName = range.get_Range("A" + curRow).get_Value();
                                    date = range.get_Range("B" + curRow).get_Value();
                                    gradeTitle = range.get_Range("C" + curRow).get_Value();
                                    qty = range.get_Range("D" + curRow).get_Value();

                                    if (partyName != null && date != null && gradeTitle != null && qty != null)
                                    {
                                        String clientName = partyName.ToString();

                                        if (skipedClients.Contains(clientName)) continue;

                                        DateTime eDate = Convert.ToDateTime(date.ToString());
                                        String gradeName = gradeTitle.ToString();
                                        double mtAmount = double.Parse(qty.ToString());

                                        #region validateClientDetails
                                    fetchClientDetails:
                                        if (sdic.ContainsKey(clientName))
                                        {
                                            clientName = sdic[clientName];
                                        }

                                        classes.Client clientProfile = Job.Clients.findClientByName(clientName, Job.Companies.currentCompany.id);

                                        if (clientProfile == null)
                                        {
                                            bool dialogFlag = false;
                                            //new client : ask for information
                                            act = () =>
                                            {
                                                forms.frmNewClient nc = new frmNewClient((classes.Client c) =>
                                                {
                                                    clientProfile = c;
                                                    sdic.Add(clientName, c.name);
                                                    clientName = c.name;
                                                    return true;
                                                }, Job.Companies.currentCompany.id);
                                                nc.txtName.Text = clientName;
                                                nc.btnLink.Visible = true;
                                                dialogFlag = nc.ShowDialog(this) == DialogResult.Cancel;
                                            };
                                            Invoke(act);

                                            if (dialogFlag)
                                            {
                                                bool flag = false;
                                                act = () =>
                                                {
                                                    flag = MessageBox.Show(this, "Are you sure to not add this client information into scheme database ?", "Skip Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                                                };
                                                Invoke(act);
                                                if (flag)
                                                {
                                                    skipedClients.Add(clientName);
                                                    clientSkipCount++;
                                                    continue;
                                                }
                                                else
                                                {
                                                    goto fetchClientDetails;
                                                }
                                            }
                                            else
                                            {
                                                goto fetchClientDetails;
                                            }
                                        }
                                        #endregion

                                        #region validateGrade
                                    fetchGradeDetails:
                                        classes.Grade grade = Job.Grades.getGradeByCode(gradeName, true, Job.Companies.currentCompany.id);

                                        if (grade == null)
                                        {
                                            bool dialogFlag = false;
                                            act = () =>
                                            {
                                                forms.frmGrade ng = new frmGrade(Job.Companies.currentCompany.id);
                                                ng.setGradeCode(gradeName);
                                                dialogFlag = ng.ShowDialog(this) == DialogResult.Cancel;
                                            };
                                            Invoke(act);

                                            if (dialogFlag)
                                            {
                                                bool flag = false;
                                                act = () =>
                                                {
                                                    flag = MessageBox.Show(this, "Are you sure not to add this grade into scheme database ?", "Skip Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                                                };
                                                Invoke(act);

                                                if (flag)
                                                {
                                                    skipedClients.Add(gradeName);
                                                    continue;
                                                }
                                                else
                                                {
                                                    goto fetchGradeDetails;
                                                }
                                            }
                                            else
                                            {
                                                goto fetchGradeDetails;
                                            }
                                        }
                                        #endregion

                                        if (clientProfile != null && grade != null)
                                        {
                                            bool flag = Job.Schemes.addSchemeDataEntry(clientProfile.id, grade.id, eDate, mtAmount);
                                        }
                                    }
                                }
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                Invoke(new Action(() => {
                                    MessageBox.Show(this, "Error while importing data from this particular sheet:" + ws.Name + ", this sheet skiped for importing data.", "Invalid Sheet", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }));
                            }

                        }

                        wb.Close(false);
                        app.Quit();
                        app.Quit();
                        app = null;
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        Job.Log("errorWhileImportingSchemeData", ex);
                    }

                    act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);

                });
                thread.Priority = System.Threading.ThreadPriority.Highest;
                thread.Start();

                new frmProcess("Importing scheme data...", "Reading excel file...", true, (fc) => { }).ShowDialog(this);

                loadSchemeData();

            }
        }

        private void loadSchemeData()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {

                Action act = () =>
                {
                    lvData.ClearObjects();
                };
                Invoke(act);

                List<classes.SchemeData> datas = Job.Schemes.getAllSchemeData(true);

                Invoke(new Action(() =>
                {
                    lvData.SetObjects(datas);
                    frmProcess.publicClose();
                }));

            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading Scheme Data...", "", true, (f) => { }).ShowDialog(this);
        }

        private void loadComboItems()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {

                Invoke(new Action(() =>
                {
                    clientItems.Clear();
                    frmProcess.getInstance().lblMsg.Text = "Loading client details...";
                }));
                
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    Invoke(new Action(() =>
                    {
                        clientItems.Add(new ComboItem(c.name, c.id));
                    }));
                });

                Invoke(new Action(() =>
                {
                    gradeItems.Clear();
                    frmProcess.getInstance().lblMsg.Text = "Loading grade details...";
                }));

                Job.Grades.getAllGrades(0, true, true, (classes.Grade grade) =>
                {
                    Invoke(new Action(() => {
                        gradeItems.Add(new ComboItem(grade.code, grade.id));
                    }));
                });

                Invoke(new Action(() => {
                    frmProcess.publicClose();
                }));

            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading ...", "Loading client details...", true, (c) => { }).ShowDialog(this);
        }

        private void frmSchemeData_Shown(object sender, EventArgs e)
        {
            loadComboItems();
            loadSchemeData();
        }

        private void lvData_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((classes.SchemeData)e.RowObject).SetDataReflector = true;

            #region ClientColumn
            if (e.Column == olvColumn1)
            {
                ComboBox cmb = new ComboBox();
                cmb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                cmb.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb.Bounds = e.CellBounds;
                cmb.Items.AddRange(clientItems.ToArray());
                if (e.Value != null)
                    cmb.Text = e.Value.ToString();
                else
                    cmb.Text = "Select Client";
                e.Control = cmb;

            }
            #endregion

            #region GradeColumn

            if (e.Column == olvColumn3)
            {
                ComboBox cmb = new ComboBox();
                cmb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                cmb.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmb.Bounds = e.CellBounds;
                cmb.Items.AddRange(gradeItems.ToArray());
                if (e.Value != null)
                    cmb.Text = e.Value.ToString();
                else
                    cmb.Text = "Select Grade";
                e.Control = cmb;
            }

            #endregion
        }

        private void lvData_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            #region ClientColumn
            if (e.Column == olvColumn1)
            {
                if (e.Control is ComboBox)
                {
                    ComboBox cmb = e.Control as ComboBox;
                    if (cmb.SelectedIndex > -1)
                    {
                        long newId = (long)(cmb.SelectedItem as ComboItem).Value;

                        ((classes.SchemeData)e.RowObject).client_id = newId;
                        ((classes.SchemeData)e.RowObject).client_name = (cmb.SelectedItem as ComboItem).Name;
                        e.Cancel = true;
                        lvData.RefreshObject(e.RowObject);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            #endregion

            #region GradeColumn
            if (e.Column == olvColumn3)
            {
                if (e.Control is ComboBox)
                {
                    ComboBox cmb = e.Control as ComboBox;
                    if (cmb.SelectedIndex > -1)
                    {
                        long newId = (long)(cmb.SelectedItem as ComboItem).Value;

                        ((classes.SchemeData)e.RowObject).grade_id = newId;
                        ((classes.SchemeData)e.RowObject).grade_name = (cmb.SelectedItem as ComboItem).Name;
                        e.Cancel = true;
                        lvData.RefreshObject(e.RowObject);
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
            #endregion
        }

        private void lvData_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show(this, "Are you sure to delete all selected entries ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                foreach (classes.SchemeData sd in lvData.SelectedObjects)
                {
                    sd.Delete();
                }

                loadSchemeData();
            }
        }

        private void btnDeleteEntries_Click(object sender, EventArgs e)
        {
            lvData_KeyDown(sender, new KeyEventArgs(Keys.Delete));
        }

        private void btnNewEntry_Click(object sender, EventArgs e)
        {
            new forms.frmNewSchemeData().ShowDialog(this);
            loadSchemeData();
        }
    }
}
