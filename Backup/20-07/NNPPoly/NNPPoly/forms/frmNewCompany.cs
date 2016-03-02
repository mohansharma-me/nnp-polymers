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
    public partial class frmNewCompany : Form
    {
        private bool editMode = false;
        private long companyId = 0;

        public frmNewCompany()
        {
            InitializeComponent();
        }

        public void setEditMode(long id)
        {
            companyId = id;
            editMode = true;
            Text = "Edit company profile";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (validated())
            {
                submit();
            }
            else
            {
                MessageBox.Show(this, "Please enter valid company data.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit()
        {
            bool succ = editMode ? Job.Companies.update(companyId, txtCompanyName.Text.Trim(), txtAddress.Text.Trim()) : Job.Companies.add(txtCompanyName.Text.Trim(), txtAddress.Text.Trim());
            if (succ)
            {
                MessageBox.Show(this, "New company successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(this, "Unexpected error occured, please contact software developer team.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validated()
        {
            TextBox_TextChanged(txtCompanyName, new EventArgs());

            return txtCompanyName.BackColor == Color.White;
        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = Job.Validation.ValidateString(txtCompanyName.Text) ? Color.White : Color.Red;
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void txtAddress_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnAdd;
        }

        private void frmNewCompany_Shown(object sender, EventArgs e)
        {
            if (editMode)
            {
                Thread thread = new Thread(() =>
                {
                    Action act;
                    classes.Company comp = Job.Companies.getCompany(companyId);
                    if (comp != null)
                    {
                        act = () =>
                        {
                            txtCompanyName.Text = comp.name;
                            txtAddress.Text = comp.address;
                        };
                        Invoke(act);
                    }
                    else
                    {
                        act = () =>
                        {
                            MessageBox.Show(this, "Sorry, unable to load company profile, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            frmProcess.publicClose();
                            Close();
                        };
                        Invoke(act);
                        return;
                    }

                    act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Name = "Thread: Companyprofile";
                thread.Start();
                new frmProcess("Loading profile...", "", true, (fc) => { }).ShowDialog(this);
            }
        }
    }
}
