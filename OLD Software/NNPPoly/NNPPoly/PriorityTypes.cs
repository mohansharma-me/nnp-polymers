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
    public partial class PriorityTypes : Form
    {
        public PriorityTypes()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                txtName.BackColor = Color.Red;
                txtName.Focus();
                return;
            }
            lb.Items.Add(txtName.Text.Trim().ToLower());
            txtName.Text = "";
            txtName.Focus();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length != 0)
            {
                btnAdd_Click(sender, e);
            }
        }

        private void PriorityTypes_Load(object sender, EventArgs e)
        {

        }

        private void PriorityTypes_Shown(object sender, EventArgs e)
        {
            foreach (String type in Datastore.dataFile.PriorityTypes)
                lb.Items.Add(type);
            txtLessDays.Text = Datastore.dataFile.DefualtLessDays.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Datastore.dataFile.PriorityTypes = new List<String>();
            foreach (String item in lb.Items)
            {
                Datastore.dataFile.PriorityTypes.Add(item);
            }
            double ld=0;
            if (txtLessDays.Text.Trim().Length == 0 || !double.TryParse(txtLessDays.Text.Trim(), out ld))
            {
                txtLessDays.Focus();
                return;
            }
            Datastore.dataFile.DefualtLessDays = ld;
            Datastore.dataFile.Save();
            Close();
        }

        private void lb_DoubleClick(object sender, EventArgs e)
        {
            if (lb.SelectedIndex>-1)
            {
                lb.Items.RemoveAt(lb.SelectedIndex);
            }
        }
    }
}
