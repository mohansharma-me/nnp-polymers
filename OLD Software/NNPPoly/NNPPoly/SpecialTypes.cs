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
    public partial class SpecialTypes : Form
    {
        public SpecialTypes()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtST.Text.Trim().Length != 0)
            {
                if (!lb.Items.Contains(txtST.Text.Trim().ToLower()))
                {
                    lb.Items.Add(txtST.Text.Trim().ToLower());
                    txtST.Text = "";
                    txtST.Focus();
                }
            }
        }

        private void lb_DoubleClick(object sender, EventArgs e)
        {
            if (lb.SelectedIndex != -1)
            {
                txtST.Text = lb.SelectedItem.ToString();
                lb.Items.RemoveAt(lb.SelectedIndex);
                txtST.Focus();
            }
        }

        private void SpecialTypes_Load(object sender, EventArgs e)
        {
            foreach (String st in Datastore.dataFile.SpecialTypes)
                lb.Items.Add(st.ToLower().Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Datastore.dataFile.SpecialTypes.Clear();
            foreach (String st in lb.Items)
            {
                Datastore.dataFile.SpecialTypes.Add(st.ToLower().Trim());
            }
            Datastore.dataFile.Save();
            Close();
        }
    }
}
