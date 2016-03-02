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
    public partial class NewClients : Form
    {
        public NewClients()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void NewClients_Load(object sender, EventArgs e)
        {
            //gv.Columns[0].ValueType = typeof(String);
            //gv.Columns[2].ValueType = typeof(double);
            
        }
        

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                bool flag = true;
                foreach (DataGridViewRow row in gv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        bool noSecondInt = false;
                        if (row.Cells[0].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter valid client name", "Invalid client name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[0];
                            break;
                        }
                        else if (row.Cells[2].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter valid opening balance", "Invalid balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[2];
                            break;
                        }
                        else if (row.Cells[3].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please select valid opening balance type", "Invalid OB Type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[3];
                            break;
                        }
                        else if (row.Cells[4].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter valid interest rate 1.", "Invalid interest rate 1", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[4];
                            break;
                        }
                        else if (row.Cells[5].Value == null)
                        {
                            /*flag = false;
                            MessageBox.Show(this, "Please enter valid interest rate 2", "Invalid interest rate 2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[5];
                            break;*/
                            noSecondInt = true;
                        }
                        else if (row.Cells[7].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter valid foo text description", "Invalid Foo Text", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[7];
                            break;
                        }
                        else if (row.Cells[8].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter less days amount", "Invalid Less Days", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[8];
                            break;
                        }
                        else if (row.Cells[9].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter valid mobile number", "Invalid Mobile number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[9];
                            break;
                        }
                        else if (row.Cells[10].Value == null)
                        {
                            flag = false;
                            MessageBox.Show(this, "Please enter valid email address", "Invalid Email address", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            gv.CurrentCell = row.Cells[10];
                            break;
                        }
                        else
                        {
                            if (!noSecondInt)
                            {
                                if (row.Cells[6].Value == null)
                                {
                                    flag = false;
                                    MessageBox.Show(this, "Please enter valid cutoff days number", "Invalid CutOff days", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    gv.CurrentCell = row.Cells[6];
                                    break;
                                }
                            }
                            double tmp = 0;
                            String dou = row.Cells[2].Value.ToString();
                            if (!double.TryParse(dou, out tmp))
                            {
                                MessageBox.Show(this, "Please enter valid opening balance", "Invalid balance", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                gv.CurrentCell = row.Cells[2];
                                flag = false;
                                break;
                            }
                            dou = row.Cells[4].Value.ToString();
                            if (!double.TryParse(dou, out tmp))
                            {
                                MessageBox.Show(this, "Please enter valid interest rate 1 amount", "Invalid interest rate 1", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                gv.CurrentCell = row.Cells[4];
                                flag = false;
                                break;
                            }
                            dou = row.Cells[8].Value.ToString();
                            if (!double.TryParse(dou, out tmp))
                            {
                                MessageBox.Show(this, "Please enter valid less days amount", "Invalid less days", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                gv.CurrentCell = row.Cells[8];
                                flag = false;
                                break;
                            }

                            if (!noSecondInt)
                            {
                                dou = row.Cells[5].Value.ToString();
                                if (!double.TryParse(dou, out tmp))
                                {
                                    MessageBox.Show(this, "Please enter valid interest rate 2 amount", "Invalid interest rate 2", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    gv.CurrentCell = row.Cells[5];
                                    flag = false;
                                    break;
                                }
                                dou = row.Cells[6].Value.ToString();
                                if (!double.TryParse(dou, out tmp))
                                {
                                    MessageBox.Show(this, "Please enter valid cutoff days amount", "Invalid cutoff days", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    gv.CurrentCell = row.Cells[6];
                                    flag = false;
                                    break;
                                }
                            }

                           
                            if (row.Cells[9].Value != null)
                            {
                                bool invalidNos = false;
                                String[] nos = row.Cells[9].Value.ToString().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (String number in nos)
                                {
                                    long tempNo = 0;
                                    if (!long.TryParse(number.Trim(), out tempNo))
                                    {
                                        invalidNos = true;
                                        break;
                                    }
                                }
                                if (invalidNos)
                                {
                                    MessageBox.Show(this, "Please enter valid mobile numbers separated by ','.", "Invalid mobile numbers", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    gv.CurrentCell = row.Cells[9];
                                    flag = false;
                                    return;
                                }
                            }

                            if (row.Cells[10].Value != null)
                            {
                                bool invalid = false;
                                String[] mails = row.Cells[10].Value.ToString().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (String mail in mails)
                                {
                                    if (!Datastore.isEmail(mail))
                                    {
                                        invalid = true;
                                        break;
                                    }
                                }

                                if (invalid)
                                {
                                    MessageBox.Show(this, "Please enter valid email address separated by comma.", "Invalid email address", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    gv.CurrentCell = row.Cells[10];
                                    flag = false;
                                    return;
                                }
                            }
                        }
                    }
                }
                if (!flag || gv.Rows.Count == 1) { gv.Focus(); return; }

                Decimal max = ++Datastore.dataFile.UserAccountIDManager;
                /*foreach (UserAccount ua in Datastore.dataFile.UserAccounts)
                    if (ua.ID > max)
                        max = ua.ID;
                max++;*/
                foreach (DataGridViewRow row in gv.Rows)
                {
                    if (row.IsNewRow) continue;
                    UserAccount ua = new UserAccount();
                    ua.ID = max;
                    ua.ClientName = row.Cells[0].Value.ToString();
                    if (row.Cells[1].Value != null)
                    {
                        ua.ClientDescription = row.Cells[1].Value.ToString();
                    }
                    else
                    {
                        ua.ClientDescription = "";
                    }
                    ua.OpeningBalance = double.Parse(row.Cells[2].Value.ToString());
                    ua.InterestRate1 = double.Parse(row.Cells[4].Value.ToString());
                    if (row.Cells[5].Value != null)
                    {
                        ua.InterestRate2 = double.Parse(row.Cells[5].Value.ToString());
                        ua.CutOffDays = double.Parse(row.Cells[6].Value.ToString());
                    }
                    else
                    {
                        ua.InterestRate2 = ua.InterestRate1;
                        ua.CutOffDays = 0;
                    }
                    ua.OBType = row.Cells[3].Value.ToString();
                    ua.LessDays = double.Parse(row.Cells[8].Value.ToString());

                    if (row.Cells[7].Value != null)
                        ua.FooText = row.Cells[7].Value.ToString();
                    else
                        ua.FooText = "";

                    if (row.Cells[9].Value != null)
                    {
                        ua.mobileNumber = row.Cells[9].Value.ToString();
                    }

                    if (row.Cells[10].Value != null)
                    {
                        ua.emailAddress = row.Cells[10].Value.ToString();
                    }

                    ua.Payments = new List<Payment>();
                    Datastore.dataFile.UserAccounts.Add(ua);
                    max++;
                }
                Datastore.dataFile.Save();
                Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform save_newclients operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
