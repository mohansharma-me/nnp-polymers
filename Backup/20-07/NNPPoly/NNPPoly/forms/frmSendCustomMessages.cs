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
    public partial class frmSendCustomMessages : Form
    {
        public frmSendCustomMessages()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void loadClients()
        {
            lvClients.ClearObjects();

            Thread thread = new Thread(() =>
            {

                List<classes.Client> clients = new List<classes.Client>();

                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    clients.Add(c);
                }, true, false, Job.Companies.currentCompany.id);

                Action act = () =>
                {
                    lvClients.SetObjects(clients);
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading clients...", "", true, (fc) => { }).ShowDialog(this);

        }

        private void frmSendCustomMessages_Shown(object sender, EventArgs e)
        {
            loadClients();
        }

        private void btnSendBoth_Click(object sender, EventArgs e)
        {
            send(true, true);
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            send(true, false);
        }

        private void btnSendMail_Click(object sender, EventArgs e)
        {
            send(false, true);
        }



        private void send(bool sms, bool mail)
        {
            if (lvClients.CheckedObjects.Count == 0)
            {
                MessageBox.Show(this, "Please select at least one client to send sms/mail to them.", "No client selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            mail = txtMailSubject.Text.Trim().Length != 0;
            sms = txtSMSMessage.Text.Trim().Length != 0;

            String smsContent = sms ? txtSMSMessage.Text.Trim().Length == 0 ? null : txtSMSMessage.Text.Trim() : null;
            String mailSubject = mail ? txtMailSubject.Text.Trim().Length == 0 ? null : txtMailSubject.Text.Trim() : null;
            String mailContent = mail ? txtMailMessage.Text.Trim().Length == 0 ? null : txtMailMessage.Text.Trim() : null;

            Thread thread = new Thread(() =>
            {

                System.Collections.IList list = null;

                Action act = () =>
                {
                    list = lvClients.CheckedObjects;
                };
                Invoke(act);

                List<classes.MessageHolder> holders = new List<classes.MessageHolder>();
                if (list != null)
                {
                    foreach (classes.Client c in list)
                    {
                        classes.MessageHolder holder = Job.Messages.prepareCustomMessage(c.id, classes.MessageHolder.Types.CUSTOM, smsContent, mailSubject, mailContent);
                        if (holder != null)
                            holders.Add(holder);
                    }
                }

                act = () =>
                {
                    if (holders.Count > 0)
                    {
                        new forms.frmMessageSender(holders).ShowDialog(this);
                    }
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Preparing messages...", "", true, (fc) => { }).ShowDialog(this);

        }

    }
}
