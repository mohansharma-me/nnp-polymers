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
    public partial class EditUserAccount : Form
    {
        private UserAccount ua;
        public EditUserAccount(String uid)
        {
            try
            {
                InitializeComponent();
                ua = Datastore.dataFile.UserAccounts.Find(x => (x.ID.ToString().Equals(uid)));
                if (ua == null)
                    Close();
                else
                {
                    txtName.Text = ua.ClientName;
                    txtAbout.Text = ua.ClientDescription;
                    txtBalance.Text = ua.OpeningBalance.ToString();
                    txtFooText.Text = ua.FooText;
                    txtIntRate1.Text = ua.InterestRate1.ToString();
                    txtIntRate2.Text = ua.InterestRate2.ToString();
                    txtCutOffDays.Text = ua.CutOffDays.ToString();
                    txtLessDays.Text = ua.LessDays.ToString();
                    txtMobile.Text = ua.mobileNumber;
                    txtEmails.Text = ua.emailAddress;
                    if (ua.OBType.ToLower().Equals("debit"))
                    {
                        radDebit.Checked = true;
                        radCredit.Checked = false;
                    }
                    else
                    {
                        radDebit.Checked = false;
                        radCredit.Checked = true;
                    }
                    txtName.Focus();
                }

            }
            catch (Exception excep)
            {
                String err = "Unable to initilize accountdetail_editclient operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBalance_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            (sender as Control).BackColor = Color.White;
        }

        private void EditUserAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                String name = txtName.Text.Trim();
                String desc = txtAbout.Text.Trim();
                String bal = txtBalance.Text.Trim();
                String intrate1 = txtIntRate1.Text.Trim();
                String intrate2 = txtIntRate2.Text.Trim();
                String cutoffdays = txtCutOffDays.Text.Trim();
                String footext = txtFooText.Text.Trim();
                String obtype = radCredit.Checked ? "Credit" : "Debit";
                String lessdays = txtLessDays.Text.Trim();
                String mobile = txtMobile.Text.Trim();
                String email = txtEmails.Text.Trim();

                if (name.Length == 0) { txtName.BackColor = Color.Red; txtName.Focus(); return; }
                double tmp = 0;
                if (bal.Length == 0 || !double.TryParse(bal, out tmp))
                {
                    txtBalance.BackColor = Color.Red;
                    txtBalance.Focus();
                    return;
                }
                double tmp1 = 0;
                if (intrate1.Length == 0 || !double.TryParse(intrate1, out tmp1))
                {
                    txtIntRate1.BackColor = Color.Red;
                    txtIntRate1.Focus();
                    return;
                }
                double tmp2 = 0;
                if (intrate2.Length == 0 || !double.TryParse(intrate2, out tmp2))
                {
                    txtIntRate2.BackColor = Color.Red;
                    txtIntRate2.Focus();
                    return;
                }
                double cod = 0;
                if (cutoffdays.Length == 0 || !double.TryParse(cutoffdays, out cod))
                {
                    txtCutOffDays.BackColor = Color.Red;
                    txtCutOffDays.Focus();
                    return;
                }
                double lessd = 0;
                if (lessdays.Length == 0 || !double.TryParse(lessdays, out lessd))
                {
                    txtLessDays.BackColor = Color.Red;
                    txtLessDays.Focus();
                    return;
                }
                if (mobile.Trim().Length == 0)
                {
                    txtMobile.BackColor = Color.Red;
                    txtMobile.Focus();
                    return;
                }
                if (email.Trim().Length == 0)
                {
                    txtEmails.BackColor = Color.Red;
                    txtEmails.Focus();
                    return;
                }

                String finalMobile = "";
                String[] numbers = mobile.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                bool invalidNumber = false;
                foreach (String number in numbers)
                {
                    long tempNo = 0;
                    if (long.TryParse(number, out tempNo))
                    {
                        finalMobile = tempNo + "," + finalMobile;
                    }
                    else
                    {
                        invalidNumber = true;
                    }
                }
                finalMobile = finalMobile.Substring(0, finalMobile.Length - 1);
                if (mobile.Length > 0 && invalidNumber)
                {
                    MessageBox.Show(this, "Sorry, you have to enter perfect mobile numbers.", "Invalid Mobile Numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMobile.BackColor = Color.Red;
                    txtMobile.Focus();
                    return;
                }

                String finalEmails = "";
                if (email.Length > 0)
                {
                    bool invalidMail = false;
                    String[] mails = email.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String mail in mails)
                    {
                        if (Datastore.isEmail(mail))
                        {
                            finalEmails = mail + "," + finalEmails;
                        }
                        else
                        {
                            invalidMail = true;
                            break;
                        }
                    }
                    if (invalidMail)
                    {
                        MessageBox.Show(this, "Please enter valid email address.", "Invalid email address", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmails.BackColor = Color.Red;
                        txtEmails.Focus();
                        return;
                    }
                    finalEmails = finalEmails.Substring(0, finalEmails.Length - 1);
                }


                ua.ClientName = name;
                ua.ClientDescription = desc;
                ua.OpeningBalance = tmp * 1.00;
                ua.InterestRate1 = tmp1 * 1.00;
                ua.InterestRate2 = tmp2 * 1.00;
                ua.CutOffDays = cod;
                ua.FooText = footext;
                ua.OBType = obtype;
                ua.LessDays = lessd;
                ua.mobileNumber = finalMobile;
                ua.emailAddress = finalEmails;

                Datastore.dataFile.Save();

                if (Datastore.current != null)
                {
                    Datastore.current.ClientName = name;
                    Datastore.current.ClientDescription = desc;
                    Datastore.current.OpeningBalance = tmp * 1.00;
                }

                DialogResult = DialogResult.OK;
                Close();

            }
            catch (Exception excep)
            {
                String err = "Unable to perform save_editclient operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform cancel_editclient operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public UserAccount getUA()
        {
            return ua;
        }
    }
}
