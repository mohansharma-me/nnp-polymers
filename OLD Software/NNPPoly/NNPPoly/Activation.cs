using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace NNPPoly
{
    public partial class Activation : Form
    {
        public bool Suc = false;
        public Activation()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Suc = false;
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private bool isControl = false;
        private void btnGO_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                txtName.Focus();
                return;
            }
            if (txtKey.Text.Trim().Length == 0)
            {
                txtKey.Focus();
                return;
            }
            String name = txtName.Text.Trim().ToLower();
            String key = txtKey.Text.Trim().ToLower();

            WebClient wc=new WebClient();
            Uri uri = new Uri("http://activation.wcodez.com/nnppoly.php?name=" + name + "&key=" + key);
            if (isControl)
                uri = new Uri("http://localhost/nnppoly.php?name=" + name + "&key=" + key);
            wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(wc_DownloadStringCompleted);
            btnGO.Enabled = false;
            wc.DownloadStringAsync(uri);
        }

        void wc_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null || e.Cancelled)
            {
                btnGO.Enabled = true;
            }
            else
            {
                String data = e.Result;
                if (data.ToLower().Equals(txtName.Text.Trim().ToLower() + " "))
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(File.Open(Application.ExecutablePath + ".act", FileMode.Create));
                        sw.WriteLine(StringSecurity.Encrypt(File.GetCreationTime(Application.ExecutablePath).ToOADate().ToString(), "9722505033"));
                        sw.Close();
                        Application.Restart();
                    }
                    catch (Exception excep) {
                        Log.output("Activation error", excep);
                        Suc = false;
                    }
                }
            }
        }

        private void btnGO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                isControl = true;
            else
                isControl = false;
        }

    }
}
