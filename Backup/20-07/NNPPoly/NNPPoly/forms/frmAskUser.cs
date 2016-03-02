using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmAskUser : Form
    {
        public enum ValueType
        {
            String, Long, Double, Emails, Mobiles, None
        }

        private ValueType _vt = ValueType.String;
        private String lblSave = null;

        public frmAskUser(String title, String msg, String value, ValueType vt)
        {
            InitializeComponent();
            lblTitle.Text = title;
            lblMsg.Text = msg;
            txt.Text = value;
            _vt = vt;
        }

        public frmAskUser(ref String saveAddress,String title, String msg, String value, ValueType vt)
        {
            InitializeComponent();
            lblTitle.Text = title;
            lblMsg.Text = msg;
            txt.Text = value;
            _vt = vt;
            lblSave = saveAddress;
        }

        private void frmAskUser_Load(object sender, EventArgs e)
        {
            
        }

        public String getText() { return txt.Text; }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (_vt == ValueType.String)
                {
                    txt.BackColor = Job.Validation.ValidateString(txt.Text) ? Color.White : Color.Red;
                }
                else if (_vt == ValueType.Double)
                {
                    txt.BackColor = Job.Validation.ValidateDouble(txt.Text) ? Color.White : Color.Red;
                }
                else if (_vt == ValueType.Emails)
                {
                    txt.BackColor = Job.Validation.ValidateEmails(txt.Text) ? Color.White : Color.Red;
                }
                else if (_vt == ValueType.Long)
                {
                    txt.BackColor = Job.Validation.ValidateLong(txt.Text) ? Color.White : Color.Red;
                }
                else if (_vt == ValueType.Mobiles)
                {
                    txt.BackColor = Job.Validation.ValidateMobiles(txt.Text) ? Color.White : Color.Red;
                }
                else if (_vt == ValueType.None)
                {
                    
                }

                btnSubmit.Enabled = txt.BackColor == Color.White;
            }
            catch (Exception ex)
            {
                Job.Log("Error[AskUser_TextChanged]", ex);
                MessageBox.Show(this, "Unexpected error occured, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txt.BackColor == Color.Red)
            {
                MessageBox.Show(this, "Please enter valid information, please try again.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (lblSave != null)
                    lblSave = txt.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
