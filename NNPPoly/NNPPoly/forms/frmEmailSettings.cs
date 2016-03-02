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
    public partial class frmEmailSettings : Form
    {
        public frmEmailSettings()
        {
            InitializeComponent();
        }

        private void frmEmailSettings_Load(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                String email = Job.GeneralSettings.mail_myMail();
                String footer = Job.GeneralSettings.mail_footer();
                String username = Job.GeneralSettings.mail_username();
                String password = Job.GeneralSettings.mail_password();

                String coll = Job.GeneralSettings.mail_msg_collection();
                String despatch = Job.GeneralSettings.mail_msg_despatch();
                String stock = Job.GeneralSettings.mail_msg_stock();
                String oReq = Job.GeneralSettings.mail_msg_orderRequest();

                Action act = () =>
                {
                    txtEmail.Text = email;
                    txtFooter.Text = footer;
                    txtUsername.Text = username;
                    txtPassword.Text = password;
                    txtCollection.Text = coll;
                    txtDespatch.Text = despatch;
                    txtStock.Text = stock;
                    txtOrderRequest.Text = oReq;

                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading settings...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = checkBox1.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!Job.Validation.ValidateEmails(txtEmail.Text.Trim()))
            {
                MessageBox.Show(this, "Please enter valid e-mail address.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtEmail.Focus();
                return;
            }

            if (!Job.Validation.ValidateEmails(txtUsername.Text.Trim()))
            {
                MessageBox.Show(this, "Please enter valid username e-mail address.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text.Trim().Length==0)
            {
                MessageBox.Show(this, "Please enter valid gmail account password.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPassword.Focus();
                return;
            }

            String email = txtEmail.Text.Trim();
            String footer = txtFooter.Text.Trim();
            String username = txtUsername.Text.Trim();
            String password = txtPassword.Text.Trim();

            String coll = txtCollection.Text.Trim();
            String des = txtDespatch.Text.Trim();
            String stock = txtStock.Text.Trim();
            String oreq = txtOrderRequest.Text.Trim();

            if (!coll.Contains("%amt%"))
            {
                MessageBox.Show(this, "Please enter valid collection message which contain any amount holder %amt%..", "No Holder Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtCollection.Focus();
                return;
            }

            if (!stock.Contains("%grade%"))
            {
                MessageBox.Show(this, "Please enter valid stock message which contain any grade holder %grade%..", "No Holder Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtStock.Focus();
                return;
            }

            if (!oreq.Contains("%days%"))
            {
                MessageBox.Show(this, "Please enter valid request order message which contain any days holder %days%..", "No Holder Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtDespatch.Focus();
                return;
            }

            Thread thread = new Thread(() =>
            {

                Job.GeneralSettings.mail_myMail(email);
                Job.GeneralSettings.mail_username(username);
                Job.GeneralSettings.mail_password(password);
                Job.GeneralSettings.mail_footer(footer);

                Job.GeneralSettings.mail_msg_collection(coll);
                Job.GeneralSettings.mail_msg_despatch(des);
                Job.GeneralSettings.mail_msg_stock(stock);
                Job.GeneralSettings.mail_msg_orderRequest(oreq);

                Action act = () =>
                {
                    frmProcess.publicClose();
                    Close();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Saving...", "", true, (fc) => { }).ShowDialog(this);
        }
    }
}
