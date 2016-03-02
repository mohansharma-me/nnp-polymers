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
    public partial class frmNewSchemeData : Form
    {
        public frmNewSchemeData()
        {
            InitializeComponent();
        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            txtQty.BackColor = Job.Validation.ValidateDouble(txtQty.Text) ? Color.White : Color.Red;
        }

        private void frmNewSchemeData_Load(object sender, EventArgs e)
        {
            loadClientsGrades();
        }

        private void loadClientsGrades()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {

                Invoke(new Action(() => {
                    frmProcess.getInstance().lblMsg.Text = "Loading clients...";
                }));

                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    Invoke(new Action(() => {
                        cmbClients.Items.Add(new ComboItem(c.name, c.id));
                    }));
                });

                Invoke(new Action(() =>
                {
                    frmProcess.getInstance().lblMsg.Text = "Loading grades...";
                    cmbGrades.Items.Add(new ComboItem("Default", 0));
                }));

                Job.Grades.getAllGrades(0, true, true, (classes.Grade grade) =>
                {
                    Invoke(new Action(() => {
                        cmbGrades.Items.Add(new ComboItem(grade.code, grade.id));
                    }));
                });

                Invoke(new Action(() => {
                    frmProcess.publicClose();
                }));

            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmbClients.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select atleast one client profile to add new scheme data.", "No Client Profile", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbClients.Focus();
                return;
            }

            if (cmbGrades.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Please select atleast one grade to add new scheme data.", "No Grades", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbGrades.Focus();
                return;
            }

            txtQty_TextChanged(txtQty, new EventArgs());

            if (txtQty.BackColor == Color.Red)
            {
                MessageBox.Show(this, "Please enter valid quantity to add new scheme data.", "No Quantity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtQty.Focus();
                return;
            }

            long clientId = (long)(cmbClients.SelectedItem as ComboItem).Value;
            long gradeId = (long)(cmbGrades.SelectedItem as ComboItem).Value;
            DateTime date = dtDate.Value;
            double qty = double.Parse(txtQty.Text);

            if(Job.Schemes.addSchemeDataEntry(clientId,gradeId,date,qty))
            {
                if (MessageBox.Show(this, "This entry successfully submited, are you want to add one more entry ?", "New Entry ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    cmbClients.Focus();
                }
            }
            else
            {
                MessageBox.Show(this, "Sorry, unable to add new scheme entry, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
