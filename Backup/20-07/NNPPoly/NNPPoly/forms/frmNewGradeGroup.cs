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
    public partial class frmNewGradeGroup : Form
    {
        public frmNewGradeGroup()
        {
            InitializeComponent();
        }

        private void frmAddGradeGroup_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtGroupName_TextChanged(object sender, EventArgs e)
        {
            (sender as Control).BackColor = Job.Validation.ValidateString((sender as Control).Text) ? Color.White : Color.Red;
        }

        private void txtMonthlyQty_TextChanged(object sender, EventArgs e)
        {
            (sender as Control).BackColor = Job.Validation.ValidateDouble((sender as Control).Text) ? Color.White : Color.Red;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                submit();
            }
            else
            {
                MessageBox.Show(this, "Please enter valid group details to add new group.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit(bool closeNow=true)
        {
            if (Job.GradeGroups.add(txtGroupName.Text.Trim(), double.Parse(txtMonthlyQty.Text.Trim()), double.Parse(txtMonthMin.Text.Trim()), double.Parse(txtQuaterMin.Text.Trim()), double.Parse(txtYearMin.Text.Trim())))
            {
                if (!closeNow)
                {
                    MessageBox.Show(this, "Successfully added!!", "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                MessageBox.Show(this, "Sorry, unable to add new group, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool validate()
        {
            txtGroupName_TextChanged(txtGroupName, new EventArgs());
            txtMonthlyQty_TextChanged(txtMonthlyQty, new EventArgs());
            txtMonthlyQty_TextChanged(txtMonthMin, new EventArgs());
            txtMonthlyQty_TextChanged(txtQuaterMin, new EventArgs());
            txtMonthlyQty_TextChanged(txtYearMin, new EventArgs());

            return txtGroupName.BackColor == Color.White &&
                txtMonthlyQty.BackColor == Color.White && txtMonthMin.BackColor == Color.White && txtQuaterMin.BackColor == Color.White
                && txtYearMin.BackColor == Color.White;
        }
    }
}
