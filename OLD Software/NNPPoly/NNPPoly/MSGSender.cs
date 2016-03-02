using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPoly
{
    public partial class MSGSender : Form
    {
        private static Decimal sentCounter = 0;
        private Thread thrSender;
        private List<Message> messages=null;
        private bool sendSMS = false, sendMAIL = false;

        public MSGSender(List<Message> messages)
        {
            InitializeComponent();
            this.messages = messages;
            thrSender = new Thread(new ParameterizedThreadStart(threadMessageSender));
            thrSender.Name = "Thread: Message sender";
            thrSender.Priority = ThreadPriority.Highest;
            sentCounter = this.messages.Count;
        }

        private void threadMessageSender(object obj)
        {
            bool ss = false, sm = false, failed = false;
            object[] objs = (object[])obj;
            int index = (int)objs[0];
            Message msg = (Message)objs[1];
            Action action = () => { 
                lblStatus.Text = "Sending";
                lblStatus.Enabled = false;
                btnCancel.Text = "&Close";
                ss = sendSMS;
                sm = sendMAIL;


                lv.Items[index].SubItems[3].BackColor = Color.White;
                lv.Items[index].SubItems[3].ForeColor = Color.Black;

                lv.Items[index].SubItems[4].BackColor = Color.White;
                lv.Items[index].SubItems[4].ForeColor = Color.Black;

                if (ss)
                {
                    lv.Items[index].SubItems[3].Text = "Sending";
                }
                else
                {
                    lv.Items[index].SubItems[3].Text = "Skiped";
                }

                if (msg.mail != null && sm)
                {
                    lv.Items[index].SubItems[4].Text = "Sending";
                }
                else if (msg.mail != null && !sm)
                {
                    lv.Items[index].SubItems[4].Text = "Skiped";
                }
            };
            

            try
            {
                Invoke(action);
                if (ss)
                {
                    object tn = Datastore.DownloadString(msg.Link);
                    if (tn is Exception)
                    {
                        action = () =>
                        {
                            lv.Items[index].UseItemStyleForSubItems = false;
                            lv.Items[index].SubItems[5].Text = "ERR: " + (tn as Exception).Message;
                            lv.Items[index].SubItems[3].Text = "Not Sent!";
                            lv.Items[index].SubItems[3].BackColor = Color.Red;
                            lv.Items[index].SubItems[3].ForeColor = Color.White;
                        };
                        Invoke(action);
                        failed = true;
                    }
                    else
                    {
                        action = () =>
                        {
                            lv.Items[index].UseItemStyleForSubItems = false;
                            lv.Items[index].SubItems[5].Text = "SMS Sent";
                            lv.Items[index].SubItems[3].Text = "Sent";
                            lv.Items[index].SubItems[3].BackColor = Color.Green;
                            lv.Items[index].SubItems[3].ForeColor = Color.White;
                        };
                        Invoke(action);
                    }
                }

                if (msg.mail != null && sm)
                {
                    Exception ex = Datastore.SendMail(msg.mail.From, msg.mail.To, msg.mail.Subject, msg.mail.HTML, msg.mail.username, msg.mail.password);
                    if (ex != null)
                    {
                        action = () =>
                        {
                            lv.Items[index].UseItemStyleForSubItems = false;
                            lv.Items[index].SubItems[5].Text = "ERR: " + (ex).Message;
                            lv.Items[index].SubItems[4].Text = "Not Sent!";
                            lv.Items[index].SubItems[4].BackColor = Color.Red;
                            lv.Items[index].SubItems[4].ForeColor = Color.White;
                        };
                        Invoke(action);
                        failed = true;
                    }
                    else
                    {
                        action = () =>
                        {
                            lv.Items[index].UseItemStyleForSubItems = false;
                            lv.Items[index].SubItems[5].Text = "Done";
                            lv.Items[index].SubItems[4].Text = "Sent!";
                            lv.Items[index].SubItems[4].BackColor = Color.Green;
                            lv.Items[index].SubItems[4].ForeColor = Color.White;
                        };
                        Invoke(action);
                    }
                }
                sentCounter--;
                if (sentCounter == 0)
                {
                    action = () => {
                        if (failed)
                            lblStatus.Text = "&Send Now";
                        else
                            lblStatus.Text = "All Sent!";
                        btnCancel.Text = "&Close";
                        lblStatus.Enabled = true;

                        if (failed)
                        {
                            MessageBox.Show(this, "Some sms(s) or email(s) are failed to sent, please verify these.", "Failurer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    Invoke(action);
                }
            }
            catch (Exception) { }
        }

        private void MSG_Collection_Load(object sender, EventArgs e)
        {
            foreach (Message msg in messages)
            {
                if ((msg.Mobiles.Trim().Length == 0 && msg.mail == null) || (msg.mail != null && msg.mail.To.Trim().Length == 0))
                {
                    sentCounter--;
                    continue;
                }
                if (msg.mail != null && (msg.mail.From.Trim().Length == 0 || msg.mail.password.Trim().Length == 0 || msg.mail.username.Trim().Length == 0))
                {
                    sentCounter--;
                    continue;
                }

                ListViewItem li = new ListViewItem(new String[] { msg.ClientName, msg.Mobiles, msg.Type.ToString(), msg.SMS, msg.EMAIL, "--" });
                li.Tag = msg;
                lv.Items.Add(li);
            }
            lblStatus.Text = "&Send Now";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (btnCancel.Text.Equals("&Close"))
            {
                Close();
            }
            else
            {

            }
        }

        private void MSG_Collection_Shown(object sender, EventArgs e)
        {
            
        }

        private void lblStatus_Click(object sender, EventArgs e)
        {
            sendSMS = chkSMS.Checked;
            sendMAIL = chkEMAIL.Checked;
            if (sendSMS)
            {
                DialogResult dr1 = MessageBox.Show(this, "Are you sure to send SMS(s) to all clients mobiles ?", "Send SMS(s) ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr1 != DialogResult.Yes)
                    sendSMS = false;
            }
            if (sendMAIL)
            {
                DialogResult dr2 = MessageBox.Show(this, "Are you sure to send EMAIL(s) to all clients mailboxs ?", "Send EMAIL(s) ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr2 != DialogResult.Yes)
                    sendMAIL = false;
            }

            if (!sendSMS && !sendMAIL)
            {
                Close();
                return;
            }

            lblStatus.Text = "Sending";
            lblStatus.Enabled = false;
            sentCounter = lv.Items.Count;
            foreach (ListViewItem li in lv.Items)
            {
                object[] obj = new object[2];
                obj[0] = li.Index;
                obj[1] = li.Tag;
                Thread thr = new Thread(new ParameterizedThreadStart(threadMessageSender));
                thr.Name = "Thread: Message Sender";
                thr.Priority = ThreadPriority.Highest;
                thr.Start(obj);
            }
        }

        private void lv_MouseClick(object sender, MouseEventArgs e)
        {
            if (lv.SelectedItems.Count == 1 && lblStatus.Enabled) 
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("&Delete entry", lv_RightClickAction));
                cm.MenuItems.Add(new MenuItem("&Edit client details", lv_RightClickAction));
                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Show details", lv_RightClickAction));
                cm.Show(sender as Control, e.Location);
            }
        }

        private void lv_RightClickAction(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi.Text.StartsWith("&Delete entry"))
            {
                if (MessageBox.Show(this, "Are you sure to delete selected entry ?", "Delete Entry", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (lv.SelectedItems.Count > 0)
                    {
                        lv.Items.RemoveAt(lv.SelectedIndices[0]);
                    }
                }
            }
            else if (mi.Text.StartsWith("&Edit client detail"))
            {
                if (lv.SelectedItems.Count > 0)
                {
                    ListViewItem li=lv.SelectedItems[0];
                    Message msg = (li.Tag as Message);
                    Decimal cid = msg.ClientID;
                    EditUserAccount eua = new EditUserAccount(cid.ToString());
                    eua.ShowDialog(this);
                    UserAccount ua = eua.getUA();
                    msg.Mobiles = ua.mobileNumber;
                    if (msg.mail != null)
                    {
                        msg.mail.To = ua.emailAddress;
                    }
                    msg.Link = msg.Link.Replace(li.SubItems[1].Text.Trim(), msg.Mobiles);
                    li.SubItems[1].Text = msg.Mobiles;
                    lv.SelectedItems[0].Tag = msg;
                }
            }
            else if (mi.Text.StartsWith("&Show details"))
            {
                if (lv.SelectedItems.Count > 0)
                {
                    Message mg = lv.SelectedItems[0].Tag as Message;
                    String msg = "SMS" + Environment.NewLine + "-------";
                    msg += Environment.NewLine + "Mobiles: " + mg.Mobiles;
                    msg += Environment.NewLine + "Content: " + mg.Link;
                    msg += Environment.NewLine + Environment.NewLine;
                    if (mg.mail != null)
                    {
                        msg += "EMAIL" + Environment.NewLine + "---------";
                        msg += Environment.NewLine + "Subject: " + mg.mail.Subject;
                        msg += Environment.NewLine + "To     : " + mg.mail.To;
                    }
                    MessageBox.Show(this, msg, "Details", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void lv_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void lv_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lblStatus.Enabled)
            {
                lv_RightClickAction(new MenuItem("&Delete entry"), new EventArgs());
            }
        }
    }

    public class Message
    {
        public String ClientName;
        public String Mobiles;
        public MessageType Type;
        public String SMS;
        public String EMAIL;
        public String Status;
        public String Link;
        public Decimal ClientID;
        public Mail mail;

        public Message(String client,String mobiles,MessageType type,String link)
        {
            this.ClientName = client;
            this.Mobiles = mobiles;
            this.Type = type;
            this.Link = link;
            this.SMS = "Sending";
            this.EMAIL = "Sending";
            this.Status = "Processing";
            
            //Datastore.SendMail(from,to,subject,html,username,pass)
            logCurrentConfig();
        }

        public Message(String client, String mobiles, MessageType type, String link, Mail mail)
        {
            this.ClientName = client;
            this.Mobiles = mobiles;
            this.Type = type;
            this.Link = link;
            this.SMS = "-";
            this.EMAIL = "-";
            this.Status = "Processing";

            this.mail = mail;

            logCurrentConfig();
        }
        public Message(String client, String mobiles, MessageType type, String link, Mail mail, Decimal ClientID)
        {
            this.ClientName = client;
            this.Mobiles = mobiles;
            this.Type = type;
            this.Link = link;
            this.SMS = "-";
            this.EMAIL = "-";
            this.Status = "Processing";

            this.mail = mail;
            this.ClientID = ClientID;

            logCurrentConfig();
        }

        private void logCurrentConfig()
        {
            Log.output("Message: " + Environment.NewLine + "SMS:" + this.Link);
            if (this.mail != null)
            {
                Log.output("Mail: " + Environment.NewLine + "HTML:" + this.mail.HTML + Environment.NewLine + "Subject:" + this.mail.Subject + ", To:" + this.mail.To);
            }
        }

        public class Mail
        {
            public Mail()
            {

            }

            public Mail(String to,String subject,String html)
            {
                From = Datastore.dataFile.mail_MyMail;
                To = to;
                Subject = subject;
                HTML = html;
                username = Datastore.dataFile.mail_Username;
                password = Datastore.dataFile.mail_Password;
            }

            public String From;
            public String To;
            public String Subject;
            public String HTML;
            public String username;
            public String password;
        }
    }

    public enum MessageType
    {
        Collection,
        Despatch,
        Stock,
        RequestToOrder
    }
}
