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
    public partial class MailSettings : Form
    {
        public MailSettings()
        {
            InitializeComponent();
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPass.Checked)
            {
                txtGmailPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtGmailPass.UseSystemPasswordChar = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MailSettings_Load(object sender, EventArgs e)
        {
            txtMyEmail.Text = Datastore.dataFile.mail_MyMail;
            txtGmailUser.Text = Datastore.dataFile.mail_Username;
            txtGmailPass.Text = Datastore.dataFile.mail_Password;

            lblFCol.Tag = Datastore.dataFile.mail_Collection;
            lblFDes.Tag = Datastore.dataFile.mail_Despatch;
            lblFSto.Tag = Datastore.dataFile.mail_Stock;
            lblFReq.Tag = Datastore.dataFile.mail_Request;

            txtFooterMSG.Text = Datastore.dataFile.mail_From;
        }

        private void lblFCol_Click(object sender, EventArgs e)
        {
            GetValue gv = new GetValue("Collection message format", "Collection message:", true, lblFCol.Tag.ToString());
            if (gv.ShowDialog(this) == DialogResult.OK)
            {
                String value = gv.Value;
                if (!value.Contains("%amt%"))
                {
                    MessageBox.Show(this, "Please enter valid collection message format which has %amt% placeholder.", "No placeholder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblFCol_Click(sender, e);
                    return;
                }
                lblFCol.Tag = gv.Value;
            }
        }

        private void lblFSto_Click(object sender, EventArgs e)
        {
            GetValue gv = new GetValue("Stock message format", "Stock message:", true, lblFSto.Tag.ToString());
            if (gv.ShowDialog(this) == DialogResult.OK)
            {
                String value = gv.Value;
                if (!value.Contains("%grade%"))
                {
                    MessageBox.Show(this, "Please enter valid stock message format which has %grade% placeholder.", "No placeholder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblFSto_Click(sender, e);
                    return;
                }
                lblFSto.Tag = gv.Value;
            }
        }

        private void lblFDes_Click(object sender, EventArgs e)
        {
            GetValue gv = new GetValue("Despatch message format", "Despatch message:", true, lblFDes.Tag.ToString());
            if (gv.ShowDialog(this) == DialogResult.OK)
            {
                lblFDes.Tag = gv.Value;
            }
        }

        private void lblFReq_Click(object sender, EventArgs e)
        {
            GetValue gv = new GetValue("Request order message format", "Request order message:", true, lblFReq.Tag.ToString());
            if (gv.ShowDialog(this) == DialogResult.OK)
            {
                String value = gv.Value;
                if (!value.Contains("%days%"))
                {
                    MessageBox.Show(this, "Please enter valid request order message format which has %days% placeholder.", "No placeholder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblFReq_Click(sender, e);
                    return;
                }
                lblFReq.Tag = gv.Value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Datastore.isEmail(txtMyEmail.Text.Trim()))
            {
                MessageBox.Show(this,"Please enter valid my email address.","Invalid mail address",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtMyEmail.Focus();
                return;
            }

            if (!Datastore.isEmail(txtGmailUser.Text.Trim()))
            {
                MessageBox.Show(this, "Please enter valid gmail user email address.", "Invalid mail address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGmailUser.Focus();
                return;
            }

            if (txtGmailPass.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, "Please enter valid gmail password.", "Invalid password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGmailPass.Focus();
                return;
            }

            Datastore.dataFile.mail_MyMail = txtMyEmail.Text.Trim();
            Datastore.dataFile.mail_Username = txtGmailUser.Text.Trim();
            Datastore.dataFile.mail_Password = txtGmailPass.Text.Trim();
            
            Datastore.dataFile.mail_Collection = lblFCol.Tag.ToString();
            Datastore.dataFile.mail_Despatch = lblFDes.Tag.ToString();
            Datastore.dataFile.mail_Request = lblFReq.Tag.ToString();
            Datastore.dataFile.mail_Stock = lblFSto.Tag.ToString();

            Datastore.dataFile.mail_From = txtFooterMSG.Text.Trim();

            Datastore.dataFile.Save();
            Close();
        }
    }
}
