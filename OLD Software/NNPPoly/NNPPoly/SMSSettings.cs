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
    public partial class SMSSettings : Form
    {
        public SMSSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String api = txtAPILink.Text.Trim();
            String msgcoll = txtCollectionSMS.Text.Trim();
            String msgdesp = txtDespatchSMS.Text.Trim();
            String msgsto = txtStockSMS.Text.Trim();
            String msgreq = txtRequestSMS.Text.Trim();

            if (api.Contains("%msg%") && api.Contains("%numbers%"))
            {
                if (!api.ToLower().StartsWith("http://"))
                {
                    api = "http://" + api;
                }
                Datastore.dataFile.sms_API = api;

                if (msgcoll.Length == 0)
                {
                    MessageBox.Show(this, "Please enter valid collection message format.", "Collection SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCollectionSMS.Focus();
                    return;
                }
                if (!msgcoll.Contains("%amt%"))
                {
                    MessageBox.Show(this, "Please enter valid collection message format, user should include %amt% as placeholder.", "No %amt% placeholder found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCollectionSMS.Focus();
                    return;
                }
                Datastore.dataFile.msg_Collection = msgcoll;

                if (msgdesp.Length == 0)
                {
                    MessageBox.Show(this, "Please enter valid despatch message format.", "Despatch SMS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDespatchSMS.Focus();
                    return;
                }
                if (!msgdesp.Contains("%amt%") || !msgdesp.Contains("%date%") || !msgdesp.Contains("%grade%") || !msgdesp.Contains("%qty%"))
                {
                    MessageBox.Show(this, "Please enter valid despatch message format, user should include following as placeholder :"+Environment.NewLine+"%date%"+Environment.NewLine+"%grade%"+Environment.NewLine+"%qty%"+Environment.NewLine+"%amt%", "No placeholder found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtDespatchSMS.Focus();
                    return;
                }
                Datastore.dataFile.msg_Dispatch = msgdesp;

                if (!msgsto.Contains("%grade%")) 
                {
                    MessageBox.Show(this, "Please enter valid stock message having %grade% placeholder.", "No placeholder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtStockSMS.Focus();
                    return;
                }
                Datastore.dataFile.msg_Stock = msgsto;

                if (!msgreq.Contains("%days%"))
                {
                    MessageBox.Show(this,"Please enter valid order request message having %days% placeholder.","No placeholder",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtRequestSMS.Focus();
                    return;
                }
                Datastore.dataFile.msg_Request = msgreq;

                Datastore.dataFile.sms_NoOfRequestDays = namDays.Value;

                Datastore.dataFile.Save();
                Close();
            }
            else
            {
                MessageBox.Show(this, "Sorry, given api link is not supported.", "Not Supported", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SMSSettings_Load(object sender, EventArgs e)
        {
            txtAPILink.Text = Datastore.dataFile.sms_API;
            txtCollectionSMS.Text = Datastore.dataFile.msg_Collection;
            txtDespatchSMS.Text = Datastore.dataFile.msg_Dispatch;
            txtStockSMS.Text = Datastore.dataFile.msg_Stock;
            txtRequestSMS.Text = Datastore.dataFile.msg_Request;
            namDays.Value = Datastore.dataFile.sms_NoOfRequestDays;
        }
    }
}
