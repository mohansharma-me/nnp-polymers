using NNPPoly.classes;
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
    public partial class frmMessageSender : Form
    {
        private Thread runningThread = null;
        private bool isThreadRunning = false, easyClose = false;
        private List<MessageHolder> holders = null;

        public frmMessageSender(List<MessageHolder> holders)
        {
            InitializeComponent();
            this.holders = holders;
            lvHolders.SetObjects(holders);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
                Close();
        }

        private void btnSendBoth_Click(object sender, EventArgs e)
        {
            send(true,true);
        }

        private void btnSendMailOnly_Click(object sender, EventArgs e)
        {
            send(true, false);
        }

        private void btnSendSMSOnly_Click(object sender, EventArgs e)
        {
            send(false, true);
        }
        private void send(bool mail, bool sms)
        {
            runningThread = new Thread(() =>
            {
                Action act = () =>
                {
                    isThreadRunning = true;
                    lvHolders.Enabled = btnSendBoth.Enabled = btnSendMailOnly.Enabled = btnSendSMSOnly.Enabled = false;
                    pb.Maximum = holders.Count;
                    pb.Value = 0;
                };
                Invoke(act);

                foreach (MessageHolder holder in holders)
                {
                    Action actUpdate = () =>
                    {
                        lvHolders.RefreshObject(holder);
                    };

                    if (sms && holder.mobiles != null)
                    {
                        holder.sms_status = "|||";
                        Invoke(actUpdate);
                        String link = Job.Messages.parseAPI(holder);
                        if (link != null)
                        {
                            holder.sms_status = "Sending";
                            Invoke(actUpdate);
                            object obj = Job.Functions.DownloadString(link);
                            if (obj is Exception)
                            {
                                holder.sms_status = "Err#2: " + (obj as Exception).Message;
                            }
                            else
                            {
                                holder.sms_status = "Sent";
                            }
                            Invoke(actUpdate);
                        }
                        else
                        {
                            holder.sms_status = "Err#1";
                            Invoke(actUpdate);
                        }
                    }
                    else
                    {
                        holder.sms_status = sms ? "No Msg!" : "Skiped!";
                        Invoke(actUpdate);
                    }

                    if (mail && holder.emails != null)
                    {
                        holder.email_status = "Sending";
                        Invoke(actUpdate);

                        Exception ex = Job.Functions.SendMail(holder.emails, holder.mail_subject, holder.mail_content);

                        if (ex == null)
                        {
                            holder.email_status = "Sent";
                        }
                        else
                        {
                            holder.email_status = "Err#1:" + ex.Message;
                        }
                        Invoke(actUpdate);
                    }
                    else
                    {
                        holder.email_status = mail ? "No Mail!" : "Skiped!";
                        Invoke(actUpdate);
                    }



                    Invoke(actUpdate);
                    act = () => { pb.Value++; };
                    Invoke(act);
                }

                act = () =>
                {
                    isThreadRunning = false;
                    lvHolders.Enabled = btnSendBoth.Enabled = btnSendMailOnly.Enabled = btnSendSMSOnly.Enabled = true;
                };
                Invoke(act);

            });
            runningThread.Priority = ThreadPriority.Highest;
            runningThread.Start();
        }

        private void frmMessageSender_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isThreadRunning)
            {
                if (MessageBox.Show(this, "Are you sure to cancel current process ?", "Close Sender", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
                else
                {
                    try
                    {
                        runningThread.Abort();
                        e.Cancel = true;
                    }
                    catch (Exception) { }
                }
            }
            else if(!easyClose)
            {
                if (MessageBox.Show(this, "Are you sure to cancel this dialog ?", "Close Sender", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
            }
        }

        private void lvHolders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvHolders.SelectedObjects.Count == 1)
            {
                MessageHolder holder = (MessageHolder)lvHolders.SelectedObjects[0];
                txtSMSContent.Text = holder.sms_content;
                btnSaveMsg.Tag = holder;
                btnSaveMsg.Enabled = txtSMSContent.Enabled = true;
            }
            else
            {
                txtSMSContent.Text = "";
                btnSaveMsg.Tag = null;

                btnSaveMsg.Enabled = txtSMSContent.Enabled = false;
            }
        }

        private void btnSaveMsg_Click(object sender, EventArgs e)
        {
            if (btnSaveMsg.Tag != null)
            {
                MessageHolder holder = (MessageHolder)btnSaveMsg.Tag;
                holder.sms_content = txtSMSContent.Text.Trim();
                txtSMSContent.Text = "";
                btnSaveMsg.Tag = null;

                btnSaveMsg.Enabled = txtSMSContent.Enabled = false;
            }
        }

        private void lvHolders_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvHolders.SelectedObjects.Count == 1 && e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show(this, "Are you sure to remove all selected messages ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                holders.Remove((MessageHolder)lvHolders.SelectedObjects[0]);
                lvHolders.RemoveObject(lvHolders.SelectedObjects[0]);
            }
        }

        private void frmMessageSender_Load(object sender, EventArgs e)
        {

        }

        private void frmMessageSender_Shown(object sender, EventArgs e)
        {
            new Thread(() =>
            {


                Invoke(new Action(() => {
                    frmProcess.publicClose();
                }));

            }).Start();
            new frmProcess("Removing duplicates messages...", "", true, (fc) => { }).ShowDialog(this);
        }
    }
}
