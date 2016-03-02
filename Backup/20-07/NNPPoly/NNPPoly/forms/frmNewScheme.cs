using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmNewScheme : Form
    {
        private long editSchemeId = 0;
        private bool editMode = false;

        public frmNewScheme()
        {
            InitializeComponent();

            olvColumnQty.AspectToStringConverter = (r) =>
            {
                return Job.Functions.MTToString(((double)r));
            };

            olvColumnMonth.AspectToStringConverter = (m) =>
            {
                return Job.Functions.monthToString((int)m);
            };
        }

        public void setEditMode(long editId)
        {
            this.editMode = true;
            this.editSchemeId = editId;

            Text = "Edit Scheme";
        }

        private void frmNewScheme_Load(object sender, EventArgs e)
        {
            loadClients();
            loadGroups();
            if(editMode)
                cmbClient.Enabled = false;
        }

        private void loadClients()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                Action act;
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    act = () =>
                    {
                        cmbClient.Items.Add(new ComboItem(c.name, c.id));
                    };
                    Invoke(act);
                });

                act = () =>
                {
                    if (cmbClient.Items.Count > 0)
                    {
                        cmbClient.SelectedIndex = 0;
                    }

                    for (int i = 1; i <= 12; i++)
                        cmbMonths.Items.Add(new ComboItem(Job.Functions.monthToString(i), i));
                    

                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading clients...", "", true, (f) => { }).ShowDialog(this);
        }

        private void loadGroups()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {

                Action act = () => {
                    lvGroups.ClearObjects();
                };
                Invoke(act);

                if (editMode)
                {
                    List<classes.Scheme.Params> parList = Job.Schemes.getSchemeParameters(editSchemeId);
                    if (parList!=null)
                    {
                        act = () =>
                        {
                            lvGroups.SetObjects(parList);
                        };
                        Invoke(act);
                    }
                }

                Job.GradeGroups.getAll((classes.GradeGroup gg) =>
                {

                    act = () =>
                    {
                        //lvGroups.AddObject(new classes.Scheme.Params(gg.id, gg.qty, gg.name));
                        cmbGroups.Items.Add(new ComboItem(gg.name, gg.id));
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

            new frmProcess("Loading groups...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbClient.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select atleast one client.", "No client", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<classes.Scheme.Params> pList = new List<classes.Scheme.Params>();

            foreach (classes.Scheme.Params gg in lvGroups.Objects)
            {
                pList.Add(new classes.Scheme.Params(gg.group_id, gg.qty, gg.month_no));
            }



            bool flag = editMode ? Job.Schemes.update((long)(cmbClient.SelectedItem as ComboItem).Value,editSchemeId, dtYear.Value.Year, pList) : Job.Schemes.add((long)(cmbClient.SelectedItem as ComboItem).Value, dtYear.Value.Year, pList);

            if (flag)
            {
                MessageBox.Show(this, "Successfully submitted!!", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show(this, "Unable to submit scheme because of following reasons: \n 1. This scheme attributes may create conflict with other schemes of same client.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void lvGroups_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((classes.Scheme.Params)e.RowObject).SetInitMode = true;            
        }

        private void lvGroups_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Column == olvColumnMonth)
            {
                if (e.NewValue != null)
                {
                    int val = (int)e.NewValue;
                    if (val < 1 || val > 12)
                    {
                        e.Cancel = true;
                        MessageBox.Show(this, "Please enter valid month", "Invalid Month", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
            }
            //((classes.GradeGroup)e.RowObject).SetInitMode = false;
        }

        private void frmNewScheme_Shown(object sender, EventArgs e)
        {
            if (editMode)
            {
                System.Threading.Thread thread = new System.Threading.Thread(() =>
                {
                    Action act = () => {  };

                    classes.Scheme scheme = Job.Schemes.get(editSchemeId);
                    if (scheme != null)
                    {
                        classes.Client client = Job.Clients.get(scheme.client_id);
                        act = () =>
                        {
                            dtYear.Value = new DateTime(scheme.year, 1, 1);
                            Text = "Edit Scheme: " + client.name + ", (" + scheme.year + "-" + (scheme.year + 1) + ")";
                            cmbClient.Text = client.name;
                        };
                        Invoke(act);

                        /*
                        System.Collections.IEnumerable groups=null;

                        act = () =>
                        {
                            groups = lvGroups.Objects;
                            lvGroups.ClearObjects();
                        };
                        Invoke(act);

                        if (groups != null)
                        {
                            foreach (classes.Scheme.Params gg in groups)
                            {
                                classes.Scheme.Params par = scheme.parameters.Find(x => (x.group_id == gg.group_id));

                                gg.SetInitMode = true;
                                if (par != null)
                                {
                                    gg.qty = par.qty;
                                }
                                else
                                {
                                    gg.qty = 0;
                                }
                                gg.SetInitMode = false;

                                act = () =>
                                {
                                    lvGroups.AddObject(gg);
                                };
                                Invoke(act);
                            }
                        }*/

                    }

                    act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);
                });
                thread.Priority = System.Threading.ThreadPriority.Highest;
                thread.Start();

                new frmProcess("Loading scheme details...", "", true, (c) => { }).ShowDialog(this);
            }
        }

        private void txtMonthQty_TextChanged(object sender, EventArgs e)
        {
            txtMonthQty.BackColor = Job.Validation.ValidateDouble(txtMonthQty.Text) ? Color.White : Color.Red;
        }

        private void btnAddParam_Click(object sender, EventArgs e)
        {
            if (cmbGroups.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select valid grade group to add in scheme.", "No Group Selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                cmbGroups.Focus();
                return;
            }

            if (cmbMonths.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select valid month to add in scheme.", "Month not selected", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                cmbMonths.Focus();
                return;
            }

            txtMonthQty_TextChanged(txtMonthQty, new EventArgs());

            if (txtMonthQty.BackColor == Color.Red)
            {
                MessageBox.Show(this, "Please enter valid monthly MoU quantity.", "Monthly Qty isn't valid", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                txtMonthQty.Focus();
                return;
            }

            String name = (cmbGroups.SelectedItem as ComboItem).Name;
            long groupId = (long)(cmbGroups.SelectedItem as ComboItem).Value;
            int month = (int)(cmbMonths.SelectedItem as ComboItem).Value;
            double qty = double.Parse(txtMonthQty.Text.Trim());

            if (lvGroups.Objects != null)
            {
                foreach (classes.Scheme.Params par in lvGroups.Objects)
                {
                    if (par != null && par.group_id == groupId && par.month_no==month)
                    {
                        MessageBox.Show(this, "Same month and group is already selected, please choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmbGroups.Focus();
                        return;
                    }
                }
            }


            lvGroups.AddObject(new classes.Scheme.Params(groupId, qty, month, name));

            cmbGroups.Focus();
        }

        private void lvGroups_KeyDown(object sender, KeyEventArgs e)
        {
            if (MessageBox.Show(this, "Are you sure to delete all selected scheme-parameters ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            lvGroups.RemoveObjects(lvGroups.SelectedObjects);
        }
    }
}
