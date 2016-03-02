using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmCompany : Form
    {
        private bool isCompanySelectionForm = false;

        public classes.Company selectedCompany = null;

        public frmCompany()
        {
            InitializeComponent();
        }

        public frmCompany(bool companySelectionForm)
        {
            InitializeComponent();
            isCompanySelectionForm = companySelectionForm;
        }

        private void frmCompany_Load(object sender, EventArgs e)
        {
            
        }

        private void frmCompany_Shown(object sender, EventArgs e)
        {
            loadCompanies();
            
            
        }

        private void loadCompanies()
        {
            Thread th = new Thread(() =>
            {
                Action act = () =>
                {
                    lvCompanies.Visible = false;
                    lvCompanies.ClearObjects();
                };
                Invoke(act);

                if (lvCompanies.Columns.Count == 0)
                {
                    Job.Companies.generateColumns((c) => {
                        act = () => { lvCompanies.Columns.Add(c); };
                        Invoke(act);
                    });
                }

                List<classes.Company> cs=new List<classes.Company>();
                Job.Companies.search("", ref cs);

                act = () =>
                {
                    lvCompanies.SetObjects(cs);
                    lvCompanies.Visible = true;
                    lcProcess.Active = lcProcess.Visible = false;
                    lvCompanies.Focus();
                };
                Invoke(act);
            });
            th.Start();
            lcProcess.Visible = lcProcess.Active = true;
        }

        public void reloadYears()
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void btnNewCompany_Click(object sender, EventArgs e)
        {
            frmNewCompany fnc = new frmNewCompany();
            if (fnc.ShowDialog(this)==DialogResult.OK)
            {
                loadCompanies();
            }
        }

        private void lvCompanies_DoubleClick(object sender, EventArgs e)
        {
            if (lvCompanies.SelectedObjects.Count == 1)
            {
                classes.Company c = (classes.Company)lvCompanies.SelectedObjects[0];
                selectCompany(ref c);
            }
        }

        private void selectCompany(ref classes.Company c)
        {
            selectedCompany = c;
            if (!isCompanySelectionForm)
            {
                Job.GeneralSettings.initGS(c.id);
                c.selectedYear = dtYear.Value.Year;
                Job.Companies.currentCompany = c;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvCompanies_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void lvCompanies_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnNext.Enabled = lvCompanies.SelectedIndex >= 0;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            lvCompanies_DoubleClick(lvCompanies, new EventArgs());
        }

        private void lvCompanies_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.RowObject != null)
            {
                ((classes.Company)e.RowObject).SetDataReflector = true;
            }
        }

        private void lvCompanies_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.RowObject != null)
            {
                //((classes.Company)e.RowObject).SetDataReflector = false;
            }
        }

        private void lvCompanies_KeyDown(object sender, KeyEventArgs e)
        {
            if (!lvCompanies.Focused) return;

            if (e.KeyCode == Keys.Enter && lvCompanies.SelectedObjects.Count == 1)
                lvCompanies_DoubleClick(lvCompanies, new EventArgs());

            if (e.KeyCode == Keys.Delete && lvCompanies.SelectedObjects.Count > 0)
            {
                DialogResult dr = MessageBox.Show(this, "Are you sure to delete selected company along with all client data ?", "Delete Company", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    List<long> compIds = new List<long>();
                    foreach (classes.Company comp in lvCompanies.SelectedObjects)
                    {
                        compIds.Add(comp.id);
                    }

                    Thread thread = new Thread(() =>
                    {
                        Action act = () =>
                        {
                            frmProcess.getInstance().pbProcess.Maximum = compIds.Count;
                            frmProcess.getInstance().pbProcess.Value = 0;
                        };
                        Invoke(act);
                        bool finalResult = true;

                        foreach (long id in compIds)
                        {
                            act = () =>
                            {
                                frmProcess.getInstance().pbProcess.Value++;
                            };
                            Invoke(act);
                            finalResult = finalResult && Job.Companies.deleteCompany(id);
                        }

                        act = () =>
                        {
                            if (finalResult)
                            {
                                MessageBox.Show(this, "All selected companies are deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(this, "Some of selected companies are deleted successfully others are not.", "Partial Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            frmProcess.publicClose();
                        };
                        Invoke(act);

                    });
                    thread.Name = "Thread: DeleteCompanies";
                    thread.Priority = ThreadPriority.Highest;
                    thread.Start();
                    new frmProcess("Delete Company", "Deleting all data of selected companies...", false, (c) => { }).ShowDialog(this);
                    loadCompanies();
                }
            }
        }

        private void frmCompany_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Job.Companies.currentCompany == null)
            {
                try
                {
                    Application.Exit();
                }
                catch (Exception) { }
            }
        }
    }
}
