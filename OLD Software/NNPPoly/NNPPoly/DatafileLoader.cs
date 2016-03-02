using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;

namespace NNPPoly
{
    public partial class DatafileLoader : Form
    {
        public DatafileLoader()
        {
            InitializeComponent();
        }

        private void DatafileLoader_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(threadCall);
            thread.Start();
        }

        private void threadCall()
        {
            try
            {
                DataFile df = new DataFile();
                df = DataFile.Read();
                if (DataFile.Error != null)
                {
                    if (DataFile.Error.Message.Equals("File not found"))
                    {
                        setLabel("Initiating first startup ...");
                        Thread.Sleep(300);
                        df = new DataFile();
                        df.Save();
                    }
                    else
                    {
                        setError("Error occured, please restart software and if error is persist than contact software developer team." + Environment.NewLine + Environment.NewLine + "Error message: " + Environment.NewLine + DataFile.Error.Message);
                    }
                }
                if (df != null)
                {
                    Datastore.dataFile = df;
                    setLabel("Starting interface ...");
                    Thread.Sleep(1000);
                    goClose(System.Windows.Forms.DialogResult.OK);
                }
                else
                {
                    Thread.Sleep(5000);
                    goClose(System.Windows.Forms.DialogResult.Cancel);
                }
            }
            catch (Exception excep) {
                Log.output("Unable to load data file.",excep);
                MessageBox.Show("Unable to load data file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void goClose(DialogResult dr)
        {
            try
            {
                Action a = () =>
                {
                    DialogResult = dr;
                    if (dr == DialogResult.OK)
                        Close();
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep) {
                Log.output("Unable to process dialog operation.",excep);
                MessageBox.Show("Unable to process dialog operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setLabel(String text)
        {
            try
            {
                Action a = () =>
                {
                    label1.Text = text;
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep) {
                String err = "Unable to perform set_label1 operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setError(String error)
        {
            try
            {
                Action a = () => {
                    lblError.Visible = true;
                    lblError.Text = error;
                    loadingCircle1.Active = false;
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform set_error_mode operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DatafileLoader_Load(object sender, EventArgs e)
        {

        }
    }
}
