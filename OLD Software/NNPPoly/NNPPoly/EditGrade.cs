using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly
{
    public partial class EditGrade : Form
    {
        private String gradeName="";
        public EditGrade(String gradeName)
        {
            InitializeComponent();
            this.gradeName = gradeName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Grade grade = Datastore.dataFile.Grades.Find(x => (x.GradeName.Trim().ToLower().Replace(" ", "").Equals(gradeName.Trim().ToLower().Replace(" ", ""))));
            if (grade == null && MessageBox.Show(this, "Are you sure to do not add this grade in the system ??", "Ignore Grade ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                //nothing 
            }
            else
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool canClose = true;
            Grade grade = Datastore.dataFile.Grades.Find(x => (x.GradeName.Trim().ToLower().Replace(" ", "").Equals(gradeName.Trim().ToLower().Replace(" ", ""))));
            if (grade == null)
            {
                grade = new Grade();
                grade.GradeName = gradeName.Trim().Length == 0 ? "" : gradeName;
                grade.Amount = 100;
                double gradeAmt = 0;
                if (!double.TryParse(txtAmount.Text.Trim(), out gradeAmt))
                {
                    canClose = false;
                    MessageBox.Show(this, "Please enter valid amount.", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                grade.Amount = gradeAmt;
                Datastore.dataFile.Grades.Add(grade);
            }
            else
            {
                grade.Amount = 100;
                double gradeAmt = 0;
                if (!double.TryParse(txtAmount.Text.Trim(), out gradeAmt))
                {
                    canClose = false;
                    MessageBox.Show(this, "Please enter valid amount.", "Invalid amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                grade.Amount = gradeAmt;
            }



            if (canClose)
            {
                Datastore.dataFile.Save();
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void EditGrade_Load(object sender, EventArgs e)
        {
            
        }

        private void EditGrade_Shown(object sender, EventArgs e)
        {
            if (gradeName.Trim().Length == 0)
                txtGrade.Text = "Default";
            else
                txtGrade.Text = gradeName;
            txtGrade.ReadOnly = true;
            txtAmount.Text = "100";
            txtAmount.Focus();
            txtAmount.Select(0, txtAmount.Text.Trim().Length);
        }
    }
}
