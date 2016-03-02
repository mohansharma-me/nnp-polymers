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
    public partial class frmSMSSettings : Form
    {
        public frmSMSSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmSMSSettings_Load(object sender, EventArgs e)
        {
            String api = Job.GeneralSettings.sms_api();
            String nod = Job.GeneralSettings.sms_nod_requestorder();
            String coll = Job.GeneralSettings.sms_msg_collection();
            String des = Job.GeneralSettings.sms_msg_despatch();
            String stock = Job.GeneralSettings.sms_msg_stock();
            String oReq = Job.GeneralSettings.sms_msg_orderRequest();
            String mou = Job.GeneralSettings.sms_msg_mou();

            Decimal dec = 1;
            if (nod != null)
                Decimal.TryParse(nod, out dec);

            txtAPI.Text = api == null ? "" : api;
            txtDays.Value = dec;
            txtCollection.Text = coll == null ? "" : coll;
            txtDespatch.Text = des == null ? "" : des;
            txtOrderRequest.Text = oReq == null ? "" : oReq;
            txtStock.Text = stock == null ? "" : stock;
            txtMoU.Text = mou == null ? "" : mou;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String api = txtAPI.Text.Trim();
            String nod = txtDays.Value.ToString();
            String coll = txtCollection.Text.Trim();
            String des = txtDespatch.Text.Trim();
            String stock = txtStock.Text.Trim();
            String oReq = txtOrderRequest.Text.Trim();
            String mou = txtMoU.Text.Trim();

            Thread thread = new Thread(() =>
            {

                Job.GeneralSettings.sms_api(api);
                Job.GeneralSettings.sms_nod_requestorder(nod);
                Job.GeneralSettings.sms_msg_collection(coll);
                Job.GeneralSettings.sms_msg_despatch(des);
                Job.GeneralSettings.sms_msg_stock(stock);
                Job.GeneralSettings.sms_msg_orderRequest(oReq);
                Job.GeneralSettings.sms_msg_mou(mou);

                Action act = () =>
                {
                    frmProcess.publicClose();
                    Close();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Saving...", "", true, (fc) => { }).ShowDialog(this);

        }
    }
}
