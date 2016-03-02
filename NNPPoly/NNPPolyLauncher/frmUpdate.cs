using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPolyLauncher
{
    public partial class frmUpdate : Form
    {
        private const String UPDATE_SERVER = "http://update.wcodez.com/updates.php";
        //private const String UPDATE_SERVER = "http://localhost/";
        private bool isChecking = false, isDownloading = false, isDownloaded = false;
        private WebClient wc = new WebClient();
        private String currentFileDownloading = "";

        private String uid = null, version = null;
        public frmUpdate(String uid, String version)
        {
            InitializeComponent();
            this.uid = uid;
            this.version = version;
        }

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            wc.DownloadStringCompleted += wc_DownloadStringCompleted;
            wc.DownloadProgressChanged += wc_DownloadProgressChanged;
            wc.DownloadFileCompleted += wc_DownloadFileCompleted;
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (btnCheck.Text.Equals("&Check"))
            {
                pb.Value = 0;
                isChecking = true;
                wc.DownloadStringAsync(new Uri(UPDATE_SERVER + "?uid=" + uid + "&version=" + version));
                btnCheck.Enabled = false;
                btnCheck.Text = "...";
            }
            else if (btnCheck.Text.Equals("&Update Now"))
            {
                pb.Value = 0;
                isDownloading = true;
                Thread thread = new Thread(() => {
                    String url = btnCheck.Tag.ToString().Trim();
                    Uri uri=new Uri(url);
                    currentFileDownloading = "update." + uri.Segments[uri.Segments.Length - 1];
                    wc.DownloadFileAsync(uri, currentFileDownloading);
                });
                thread.Start();
                btnCheck.Enabled = false;
                btnCheck.Text = "...";
            }
        }

        void wc_DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            
        }

        void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Action act = () =>
            {
                if (e.Cancelled || e.Error!=null) 
                { 
                    isDownloading = false;
                    MessageBox.Show(this, "There is problem in internet connection, please try again.", "Connectivity problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCheck.Enabled = true;
                    btnCheck.Text = "&Check";
                    return; 
                }
                System.IO.File.Replace(currentFileDownloading, currentFileDownloading.Substring(7), "backup." + currentFileDownloading.Substring(7) + "." + version + ".bkp");
                Application.Restart();
            };
            Invoke(act);
        }
        
        void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Action act = () => {
                btnCheck.Enabled = false;
                btnCheck.Text = e.ProgressPercentage + "% ...";
                pb.Value = e.ProgressPercentage;
            };
            Invoke(act);
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            Action act = () => {
                if (e.Cancelled || e.Error != null) { 
                    isChecking = false;
                    MessageBox.Show(this, "There is problem in internet connection, please try again.", "Connectivity problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnCheck.Enabled = true;
                    btnCheck.Text = "&Check";
                    return; 
                }
                String data = e.Result;
                if (data.Trim().Length > 0)
                {
                    String[] lines = data.Split(new String[] { "<br>" }, StringSplitOptions.None);
                    if (lines.Length == 4)
                    {
                        txtLog.Text = "New Version: "+lines[0];
                        txtLog.Text += Environment.NewLine + Environment.NewLine + "[ CHANGE LOG ]" + Environment.NewLine + lines[2].Replace("<br/>", Environment.NewLine);
                        btnCheck.Text = "&Update Now";
                        btnCheck.Enabled = true;
                        btnCheck.Tag = lines[1].Trim();
                    }
                    else
                    {
                        MessageBox.Show(this, "Update server is not responding, please contact software developer team.", "Not Responding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnCheck.Enabled = true;
                        btnCheck.Text = "&Check";
                    }
                }
                else
                {
                    MessageBox.Show(this, "No updates found.", "No updates", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnCheck.Enabled = true;
                    btnCheck.Text = "&Check";
                }
                isChecking = false;
            };
            Invoke(act);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isChecking || isDownloading)
            {
                DialogResult dr = MessageBox.Show(this, "Are you sure to abort current operation ?", "Abort Connection", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    wc.CancelAsync();
                    Close();
                }
            }
        }
    }
}