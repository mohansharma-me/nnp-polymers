using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NNPPoly.classes;
using System.Threading;

namespace NNPPoly.forms
{
    public partial class frmNewClient : Form
    {
        public bool _isClose = false;
        public delegate bool UpdateClientUI(classes.Client c);
        public UpdateClientUI updateClientUI;
        private bool editMode = false;
        private Client editClient = null;
        private bool askCompanyProfile = false;
        private long customCompanyId = 0;

        public frmNewClient(UpdateClientUI uci)
        {
            InitializeComponent();
            this.updateClientUI = uci;
            cmbOBType.Items.Add("Credit");
            cmbOBType.Items.Add("Debit");
            cmbOBType.SelectedIndex = 0;
        }

        public frmNewClient(UpdateClientUI uci, bool askCompany)
        {
            InitializeComponent();
            this.updateClientUI = uci;
            cmbOBType.Items.Add("Credit");
            cmbOBType.Items.Add("Debit");
            cmbOBType.SelectedIndex = 0;
            this.askCompanyProfile = askCompany;
        }

        public frmNewClient(UpdateClientUI uci, long companyId)
        {
            InitializeComponent();
            this.updateClientUI = uci;
            cmbOBType.Items.Add("Credit");
            cmbOBType.Items.Add("Debit");
            cmbOBType.SelectedIndex = 0;
            this.askCompanyProfile = false;
            this.customCompanyId = companyId;
        }

        public void setEditMode(ref Client c)
        {
            editMode = true;
            editClient = c;
            txtName.Text = c.name;
            txtAbout.Text = c.about;
            txtMobile.Text = c.mobiles;
            txtEmail.Text = c.emails;
            txtReportFooter.Text = c.footext;
            txtOBal.Text = c.obalance.ToString();
            cmbOBType.Text = c.obalance_type.ToString();
            txtIntRate1.Text = c.intrate1.ToString();
            txtIntRate2.Text = c.intrate2.ToString();
            txtCutoffDays.Text = c.cutoffdays.ToString();
            txtLessDays.Text = c.lessdays.ToString();
            btnSubmitNext.Visible = false;
            Text = "Edit '" + c.name + "' profile";
        }

        private void frmNewClient_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            _isClose = true;
            if (validate())
            {
                MessageBox.Show(this, "Please enter required data in proper format. All red colored entries are required as well as well formatted.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (editMode && editClient!=null)
                {
                    editClient.SetDataReflector = true; editClient.name = txtName.Text.Trim();
                    editClient.SetDataReflector = true; editClient.about = txtAbout.Text.Trim();
                    editClient.SetDataReflector = true; editClient.cutoffdays = long.Parse(txtCutoffDays.Text.Trim());
                    editClient.SetDataReflector = true; editClient.emails = txtEmail.Text.Trim();
                    editClient.SetDataReflector = true; editClient.footext = txtReportFooter.Text.Trim();
                    editClient.SetDataReflector = true; editClient.intrate1 = double.Parse(txtIntRate1.Text.Trim());
                    editClient.SetDataReflector = true; editClient.intrate2 = double.Parse(txtIntRate2.Text.Trim());
                    editClient.SetDataReflector = true; editClient.lessdays = long.Parse(txtLessDays.Text.Trim());
                    editClient.SetDataReflector = true; editClient.mobiles = txtMobile.Text.Trim();
                    editClient.SetDataReflector = true; editClient.obalance = double.Parse(txtOBal.Text.Trim());
                    editClient.SetDataReflector = true; editClient.obalance_type = cmbOBType.SelectedItem.ToString().Equals("Credit") ? Client.OpeningBalanceType.Credit : Client.OpeningBalanceType.Debit;
                    MessageBox.Show(this, "All updated details are reflected to client profile database.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    submit(true);
                }
            }
        }

        private bool validate()
        {
            TB_ValidateString(txtName, new EventArgs());
            TB_ValidateMultipleLong(txtMobile, new EventArgs());
            TB_ValidateMultipleEmails(txtEmail, new EventArgs());
            TB_ValidateDouble(txtOBal, new EventArgs());
            TB_ValidateDouble(txtIntRate1, new EventArgs());
            TB_ValidateDouble(txtIntRate2, new EventArgs());
            TB_ValidateLong(txtCutoffDays, new EventArgs());
            TB_ValidateLong(txtLessDays, new EventArgs());

            return txtName.BackColor == Color.Red ||
                txtMobile.BackColor == Color.Red ||
                txtEmail.BackColor == Color.Red ||
                txtOBal.BackColor == Color.Red ||
                txtIntRate1.BackColor == Color.Red ||
                txtIntRate2.BackColor == Color.Red ||
                txtCutoffDays.BackColor == Color.Red ||
                txtLessDays.BackColor == Color.Red || 
                cmbOBType.SelectedIndex == -1;
        }

        private void submit(bool closeIt)
        {
            String name = txtName.Text.Trim();
            String mobile = txtMobile.Text.Trim();
            String email = txtEmail.Text.Trim();
            String about = txtAbout.Text.Trim();
            String footext = txtReportFooter.Text.Trim();
            String obtype = cmbOBType.SelectedItem.ToString();
            String ob = txtOBal.Text.Trim();
            String intRate1 = txtIntRate1.Text.Trim();
            String intRate2 = txtIntRate2.Text.Trim();
            String cutoff = txtCutoffDays.Text.Trim();
            String lessdays = txtLessDays.Text.Trim();
            Thread thread = new Thread(() => {
                try
                {
                    List<double> rates = new List<double>();
                    List<long> cods = new List<long>();
                    rates.Add(double.Parse(intRate1));
                    cods.Add(0);
                    rates.Add(double.Parse(intRate2));
                    cods.Add(long.Parse(cutoff));
                    long newId = 0;
                    bool added = Job.Clients.add(ref newId, name, mobile, email, about, footext, obtype, double.Parse(ob), rates, cods, long.Parse(lessdays), false, customCompanyId);
                    if (added)
                    {
                        Client c = new Client(newId);
                        c.name = name;
                        c.mobiles = mobile;
                        c.emails = email;
                        c.about = about;
                        c.footext = footext;
                        c.cutoffdays = long.Parse(cutoff);
                        c.intrate1 = double.Parse(intRate1);
                        c.intrate2 = double.Parse(intRate2);
                        c.lessdays = long.Parse(lessdays);
                        c.obalance = double.Parse(ob);
                        c.obalance_type = Client.OpeningBalanceType.Debit.ToString().ToLower().Equals(obtype) ? Client.OpeningBalanceType.Debit : Client.OpeningBalanceType.Credit;
                        bool reload=false;
                        if (updateClientUI != null)
                            reload = updateClientUI(c);
                        if (closeIt)
                        {
                            DialogResult = reload ? DialogResult.OK : DialogResult.Cancel;
                            Close();
                        }
                        else
                        {
                            Action act = () =>
                            {
                                txtName.Text = txtMobile.Text = txtEmail.Text = txtAbout.Text = txtReportFooter.Text = "";
                                txtName.BackColor = txtMobile.BackColor = txtEmail.BackColor = Color.White;
                                txtOBal.Text = "0.00";
                                txtIntRate1.Text = "20";
                                txtIntRate2.Text = "24";
                                txtCutoffDays.Text = "20";
                                txtLessDays.Text = "10";
                                txtName.Focus();
                            };
                            Invoke(act);
                        }
                    }
                    else
                    {
                        MessageBox.Show(this, "Unexpected error occured. Please contact software developer team with log file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    Job.Log("Error[Submit - frmNewClient]", ex);
                }
                finally
                {
                    try
                    {
                        Action act1 = () =>
                        {
                            frmProcess.publicClose();
                        };
                        Invoke(act1);
                    }
                    catch (Exception) { }
                }

            });
            thread.Name = "Thread: Submit frmNewClient";
            thread.Start();
            new frmProcess("Saving client...", "", true, delegate(frmProcess fp)
            {
                thread.Abort();
                MessageBox.Show(this, "You have canceled the previous job.", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }).ShowDialog(this);
        }

        private void TB_ValidateString(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (Job.Validation.ValidateString(tb.Text))
                {
                    tb.BackColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.Red;
                }
            }
            catch (Exception ex) { Job.Log("Error[TB_ValidateString - frmNewClient]", ex); }
        }

        private void TB_ValidateMultipleLong(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (Job.Validation.ValidateMobiles(tb.Text))
                {
                    tb.BackColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.Red;
                }
            }
            catch (Exception ex) { Job.Log("Error[TB_ValidateMultipleLong - frmNewClient]", ex); }
        }

        private void TB_ValidateMultipleEmails(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (Job.Validation.ValidateEmails(tb.Text))
                {
                    tb.BackColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.Red;
                }
            }
            catch (Exception ex) { Job.Log("Error[TB_ValidateMultipleEmails - frmNewClient]", ex); }
        }

        private void TB_ValidateDouble(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (Job.Validation.ValidateDouble(tb.Text))
                {
                    tb.BackColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.Red;
                }
            }
            catch (Exception ex) { Job.Log("Error[TB_ValidateDouble - frmNewClient]", ex); }
        }

        private void TB_ValidateLong(object sender, EventArgs e)
        {
            try
            {
                TextBox tb = sender as TextBox;
                if (Job.Validation.ValidateLong(tb.Text))
                {
                    tb.BackColor = Color.White;
                }
                else
                {
                    tb.BackColor = Color.Red;
                }
            }
            catch (Exception ex) { Job.Log("Error[TB_ValidateLong - frmNewClient]", ex); }
        }

        private void btnSubmitNext_Click(object sender, EventArgs e)
        {
            _isClose = false;
            if (validate())
            {
                MessageBox.Show(this, "Please enter required data in proper format. All red colored entries are required as well as well formatted.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                submit(false);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _isClose = true;
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void frmNewClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !_isClose;
        }

        private void txtAbout_TextChanged(object sender, EventArgs e)
        {

        }

        private void frmNewClient_Shown(object sender, EventArgs e)
        {
            if (!askCompanyProfile && customCompanyId > 0)
            {
                if (customCompanyId != 0)
                {
                    if (txtName.Text.Trim().Length > 0)
                    {
                        Client c = Job.Clients.findClientByName(txtName.Text, customCompanyId);
                        if (c != null)
                        {
                            _isClose = true;
                            updateClientUI(c);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }


                    return;
                }
            }

            if (askCompanyProfile)
            {

                

                frmCompany fc = new frmCompany(true);
                if (fc.ShowDialog(this) == DialogResult.OK && fc.selectedCompany!=null)
                {
                    customCompanyId = fc.selectedCompany.id;
                    if (txtName.Text.Trim().Length > 0)
                    {
                        Client c = Job.Clients.findClientByName(txtName.Text, customCompanyId);
                        if (c != null)
                        {
                            _isClose = true;
                            updateClientUI(c);
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                    }
                }
                else
                {
                    _isClose = true;
                    DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    Close();
                }
            }
        }

        private void btnLink_Click(object sender, EventArgs e)
        {
            frmClientSelection fcs = new frmClientSelection(customCompanyId);
            fcs.ShowDialog(this);

            if (fcs.selectedClientProfile != null)
            {
                if (updateClientUI != null)
                {
                    _isClose = true;
                    updateClientUI(fcs.selectedClientProfile);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }

        }

        private void txtAbout_Enter(object sender, EventArgs e)
        {
            AcceptButton = null;
        }

        private void txtAbout_Leave(object sender, EventArgs e)
        {
            AcceptButton = btnSubmit;
        }
    }
}
