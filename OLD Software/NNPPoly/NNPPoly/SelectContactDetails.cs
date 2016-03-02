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
    public partial class SelectContactDetails : Form
    {
        public List<String> MobileNumbers = new List<String>();
        public List<String> EmailAddress = new List<String>();

        public SelectContactDetails()
        {
            InitializeComponent();
        }

        private void SelectContactDetails_Load(object sender, EventArgs e)
        {
            foreach (UserAccount user in Datastore.dataFile.UserAccounts)
            {
                ListViewItem li = new ListViewItem(new String[] { user.ClientName, user.mobileNumber, user.emailAddress });
                lv.Items.Add(li);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSelects_Click(object sender, EventArgs e)
        {
            if (lv.CheckedItems.Count == 0)
            {
                MessageBox.Show(this, "You didn's selected any of clients, please select one of client.", "No client selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (ListViewItem li in lv.CheckedItems)
            {
                MobileNumbers.Add(li.SubItems[1].Text.Trim());
                EmailAddress.Add(li.SubItems[2].Text.Trim());
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                foreach (ListViewItem li in lv.Items)
                    li.Checked = true;
            }
        }
    }
}
