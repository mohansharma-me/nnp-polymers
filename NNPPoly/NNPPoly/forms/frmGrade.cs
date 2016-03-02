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
    public partial class frmGrade : Form
    {
        private long companyId = 0;
        private long groupId = 0;

        public frmGrade(long compId=0, long groupId=0)
        {
            InitializeComponent();
            this.companyId = compId;
            this.groupId = groupId;
        }

        public void setGradeCode(String gradeCode)
        {
            txtCode.Text = gradeCode.Trim();
            txtCode.ReadOnly = true;
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            txtCode.BackColor = Job.Validation.ValidateString(txtCode.Text) ? Color.White : Color.Red;
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            txtAmount.BackColor = Job.Validation.ValidateDouble(txtAmount.Text) ? Color.White : Color.Red;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                submit(true);
            }
            else
            {
                MessageBox.Show(this, "Please enter grade code as well as amount to submit new grade.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void submit(bool closeIt)
        {
            long groupId = (long)(cmbGroups.SelectedItem as ComboItem).Value;
            String code = txtCode.Text.Trim();
            double amount = double.Parse(txtAmount.Text.Trim());
            Thread th = new Thread(() => {
                Action act;

                if (!Job.Grades.add(groupId, code, amount, companyId))
                {
                    act = () =>
                    {
                        frmProcess.publicClose();
                        MessageBox.Show(this, "Unable to add new grade to database, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                    Invoke(act);
                    return;
                }

                act = () => {
                    frmProcess.publicClose();
                    if (closeIt)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        txtAmount.Text = txtCode.Text = "";
                        txtAmount.BackColor = txtCode.BackColor = Color.White;
                        MessageBox.Show(this, "Successfully added new grade.\nGrade Code:" + code + "\nAmount:" + amount, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
                Invoke(act);
            });
            th.Start();
            new frmProcess("Adding new grade", "querying database...", true, (c) => { }).ShowDialog(this);
        }

        public bool validate()
        {
            if (cmbGroups.SelectedIndex == -1) return false;
            txtCode_TextChanged(txtCode, new EventArgs());
            txtAmount_TextChanged(txtAmount, new EventArgs());

            return txtAmount.BackColor == Color.White && txtCode.BackColor == Color.White;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmGrade_Load(object sender, EventArgs e)
        {
            loadGroups();
        }

        private void loadGroups()
        {
            System.Threading.Thread thread = new Thread(() =>
            {
                List<classes.GradeGroup> groups = Job.GradeGroups.getAll(null, companyId);
                if (groups == null)
                    groups = new List<classes.GradeGroup>();
                groups.Insert(0, Job.GradeGroups.get(0));
                
                Action act = () =>
                {
                    cmbGroups.Items.Clear();
                };
                Invoke(act);
                
                if (groups != null)
                {
                    foreach (classes.GradeGroup gg in groups)
                    {
                        act = () =>
                        {
                            try
                            {
                                int index = cmbGroups.Items.Add(new ComboItem(gg.name, gg.id));

                                if (groupId == gg.id)
                                {
                                    cmbGroups.SelectedIndex = index;
                                }
                            }
                            catch (Exception ex) { }
                            
                        };
                        Invoke(act);
                    }              
                }


                act = () =>
                {
                    if (cmbGroups.SelectedIndex == -1 && cmbGroups.Items.Count > 0)
                        cmbGroups.SelectedIndex = 0;
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading Groups...", "", true, (fc) => { }).ShowDialog(this);
        }
    }
}
