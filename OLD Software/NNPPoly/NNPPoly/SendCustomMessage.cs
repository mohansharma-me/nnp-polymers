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
    public partial class SendCustomMessage : Form
    {
        public SendCustomMessage()
        {
            InitializeComponent();
        }

        private void SendCustomMessage_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            String mobiles = txtMobiles.Text.Trim();
            String sms = txtSMS.Text.Trim();

            if (mobiles.Length == 0)
            {
                MessageBox.Show(this, "Please enter valid mobile numbers, separated by comma.", "Invalid mobile number", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMobiles.Focus();
                return;
            }

            if (sms.Length == 0)
            {
                MessageBox.Show(this,"Please enter valid sms content.","No sms",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtSMS.Focus();
                return;
            }
            btnSendSMS.Enabled = false;
            sms = Uri.EscapeDataString(sms);
            String link = Datastore.dataFile.sms_API.Replace("%numbers%", mobiles).Replace("%msg%", sms);
            object obj = Datastore.DownloadString(link);
            if (obj is Exception)
            {
                MessageBox.Show(this, "Unable to send sms." + Environment.NewLine + "Error: " + (obj as Exception).Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show(this, "Message sent." + Environment.NewLine + "Reply: " + obj.ToString(), "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            btnSendSMS.Enabled = true;
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            String to = txtTo.Text.Trim();
            String subject = txtSubject.Text.Trim();
            String msg = txtMessage.Text.Trim();

            if (to.Length == 0)
            {
                MessageBox.Show(this, "Please enter valid email address.", "Invalid email address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTo.Focus();
                return;
            }

            if (subject.Length == 0)
            {
                MessageBox.Show(this, "Please enter valid subject for mail.", "No subject", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubject.Focus();
                return;
            }

            if (msg.Length == 0)
            {
                MessageBox.Show(this, "Please enter valid message for mail.", "Empty message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMessage.Focus();
                return;
            }
            btnSendMail.Enabled = false;
            Exception ex=Datastore.SendMail(Datastore.dataFile.mail_MyMail,to,subject,msg,Datastore.dataFile.mail_Username,Datastore.dataFile.mail_Password);
            if (ex == null)
            {
                MessageBox.Show(this, "Mail sent.", "Mail sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(this, "Cant sent mail at time." + Environment.NewLine + "Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnSendMail.Enabled = true;
        }

        private void lblSelNumbers_Click(object sender, EventArgs e)
        {
            SelectContactDetails scd = new SelectContactDetails();
            DialogResult dr = scd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                txtMobiles.Text += ",";
                foreach (String mobile in scd.MobileNumbers)
                {
                    if (mobile.Trim().Length > 0)
                        txtMobiles.Text += mobile + ",";
                }
                if (txtMobiles.Text.StartsWith(","))
                    txtMobiles.Text = txtMobiles.Text.Substring(1, txtMobiles.Text.Length-1);
                if (txtMobiles.Text.EndsWith(","))
                    txtMobiles.Text = txtMobiles.Text.Substring(0, txtMobiles.Text.Length - 1);
            }
        }

        private void lblSelEmails_Click(object sender, EventArgs e)
        {
            SelectContactDetails scd = new SelectContactDetails();
            DialogResult dr = scd.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                txtTo.Text += ",";
                foreach (String mobile in scd.EmailAddress)
                {
                    if (mobile.Trim().Length > 0)
                        txtTo.Text += mobile + ",";
                }
                if (txtTo.Text.StartsWith(","))
                    txtTo.Text = txtTo.Text.Substring(1, txtTo.Text.Length-1);
                if (txtTo.Text.EndsWith(","))
                    txtTo.Text = txtTo.Text.Substring(0, txtTo.Text.Length - 1);

            }
        }
    }
}
