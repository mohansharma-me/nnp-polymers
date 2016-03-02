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
    public partial class frmProcess : Form
    {
        private static frmProcess instance;
        private bool isClose = false;
        public delegate void OperationCanceled(frmProcess fp);
        public OperationCanceled cancelOperation;
        public frmProcess(String title, String msg, bool waitState, OperationCanceled oc)
        {
            InitializeComponent();
            Text = lblTitle.Text = title;
            lblMsg.Text = msg;
            lcProcess.Visible = waitState;
            lcProcess.Active = waitState;
            pbProcess.Visible = !waitState;
            cancelOperation = oc;
            instance = this;
        }

        public static frmProcess getInstance()
        {
            return instance;
        }

        public static void publicClose()
        {

            try
            {
                ((frmProcess)frmProcess.ActiveForm).isClose = true;
                ((frmProcess)frmProcess.ActiveForm).Close();
            }
            catch (Exception) { }
        }

        private void frmProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !isClose;
        }

        private void frmProcess_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void frmProcess_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (!isClose)
            {
                if (MessageBox.Show(this, "Are you sure to cancel this operation ?", "Cancel operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (cancelOperation != null)
                    {
                        try
                        {
                            cancelOperation(this);
                        }
                        catch (Exception) { }
                        lcProcess.Active = false;
                        isClose = true;
                        Close();
                    }
                }
            }
        }

        public void cancelThis()
        {
            if (cancelOperation != null)
            {
                try
                {
                    cancelOperation(this);
                }
                catch (Exception) { }
                lcProcess.Active = false;
                isClose = true;
                Close();
            }
        }
    }
}
