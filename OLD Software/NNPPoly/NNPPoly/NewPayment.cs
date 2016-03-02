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
    public partial class NewPayment : Form
    {
        public NewPayment()
        {
            InitializeComponent();
        }

        private void NewPayment_Load(object sender, EventArgs e)
        {

        }

        private void NewPayment_Shown(object sender, EventArgs e)
        {
            loadAutoCollection();
            if (!editMode)
            {
                dtPicker.Value = DateTime.Now;
                lblAmt.Visible = txtAmount.Visible = lblMT.Visible = txtMT.Visible = lblGrade.Visible = cmbGrade.Visible = radCredit.Checked;
                loadGrades();
            }

        }

        private bool editMode = false;
        public void setEditMode(String pid)
        {
            try
            {
                Payment p = Datastore.current.Payments.Find(x => (x.ID.ToString().Equals(pid)));
                if (p != null)
                {
                    Tag = p.ID.ToString();
                    //dtPicker.Text = p.Date;
                    dtPicker.Value = p.Date;//DateTime.Parse(p.Date,Program.provider);
                    txtDocChqNo.Text = p.DocChqNo;
                    txtType.Text = p.Type;
                    txtParticulars.Text = p.Particulars;
                    if (p.Credit != 0)
                    {
                        radCredit.Checked = true;
                        lblAmt.Visible = txtAmount.Visible = true;
                        txtAmount.Text = p.Credit.ToString();
                    }
                    else if (p.Debit != 0)
                    {
                        radDebit.Checked = true;
                        lblAmt.Visible = txtAmount.Visible = lblMT.Visible = txtMT.Visible = lblGrade.Visible = cmbGrade.Visible = true;
                        loadGrades();
                        int index = -1;
                        for (int i = 0; i < cmbGrade.Items.Count; i++)
                        {
                            CMBItem item = cmbGrade.Items[i] as CMBItem;
                            if (item.Value.ToString().Replace(" ", "").ToLower().Equals(p.Grade.Replace(" ", "").ToLower()))
                            {
                                index = i;
                                break;
                            }
                        }
                        cmbGrade.SelectedIndex = index;
                        txtAmount.Text = p.Debit.ToString();
                    }
                    txtMT.Text = p.MT;
                    editMode = true;
                    Text = "Edit payment...";
                    btnOK.Text = "&Save";
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform seteditmode_newpayment operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void radCredit_CheckedChanged(object sender, EventArgs e)
        {
            lblMT.Visible = txtMT.Visible = lblGrade.Visible=cmbGrade.Visible = false;
            lblAmt.Visible = txtAmount.Visible = radCredit.Checked;
        }

        private void radDebit_CheckedChanged(object sender, EventArgs e)
        {
            lblAmt.Visible = txtAmount.Visible = lblMT.Visible = txtMT.Visible = lblGrade.Visible = cmbGrade.Visible = radDebit.Checked;
            
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                String date = dtPicker.Text.Trim();
                String docno = txtDocChqNo.Text.Trim();
                String type = txtType.Text.Trim();
                String particulars = txtParticulars.Text.Trim();
                String amount = txtAmount.Text.Trim();
                String mt = txtMT.Text.Trim();
                String grade = "";

                //if (docno.Length == 0) { txtDocChqNo.BackColor = Color.Red; txtDocChqNo.Focus(); return; }
                if (type.Length == 0) { txtType.BackColor = Color.Red; txtType.Focus(); return; }
                if (particulars.Length == 0) { txtParticulars.BackColor = Color.Red; txtParticulars.Focus(); return; }


                if (!radCredit.Checked && !radDebit.Checked)
                {
                    MessageBox.Show(this, "Please check valid amount type whether credit or debit.", "Amount type", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    radCredit.Focus();
                    return;
                }

                double dTemp = 0, mtTemp = 0;
                if (amount.Length == 0 || !double.TryParse(amount, out dTemp))
                {
                    txtAmount.BackColor = Color.Red; txtAmount.Focus(); return;
                }

                if (radDebit.Checked && (mt.Length == 0 || !double.TryParse(mt, out mtTemp)))
                {
                    txtMT.BackColor = Color.Red; txtMT.Focus(); return;
                }


                if (radDebit.Checked && txtType.Text.Trim().ToLower().Equals("sale") && cmbGrade.SelectedIndex == -1)
                {
                    cmbGrade.Focus();
                    MessageBox.Show(this, "Please select grade from grade list.", "Grade", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else if (radDebit.Checked && txtType.Text.Trim().ToLower().Equals("sale"))
                {
                    grade = (cmbGrade.SelectedItem as CMBItem).Value.ToString();
                }

                Payment pay = new Payment();
                pay.ID = 1;
                pay.Date = new DateTime(dtPicker.Value.Year, dtPicker.Value.Month, dtPicker.Value.Day, 12, 0, 0);
                pay.DocChqNo = docno;
                pay.Type = type;
                pay.Particulars = particulars;
                pay.MT = mt;
                pay.Grade = grade;
                if (radCredit.Checked)
                {
                    pay.Credit = dTemp;
                    pay.Remain = dTemp;
                }
                if (radDebit.Checked)
                {
                    pay.Debit = dTemp;
                    pay.Remain = 0;
                }

                if (Datastore.current != null)
                {
                    if (editMode)
                    {
                        Payment p = Datastore.current.Payments.Find(x => (x.ID.ToString().Equals(Tag.ToString())));
                        if (p != null)
                        {
                            p.Date = new DateTime(dtPicker.Value.Year, dtPicker.Value.Month, dtPicker.Value.Day, 12, 0, 0);
                            p.DocChqNo = docno;
                            p.Type = type;
                            p.Particulars = particulars;
                            p.MT = mt;
                            p.Grade = grade;
                            if (radCredit.Checked)
                            {
                                p.Debit = 0;
                                p.Credit = dTemp;
                                p.Remain = dTemp;
                            }
                            if (radDebit.Checked)
                            {
                                p.Credit = 0;
                                p.Debit = dTemp;
                                p.Remain = 0;
                                //p.MT = "0";
                            }
                        }
                    }
                    else
                    {
                        Decimal max = ++Datastore.current.PaymentIDManager;
                        /*foreach (Payment p in Datastore.current.Payments)
                            if (p.ID > max) max = p.ID;
                        max++;*/
                        pay.ID = max;
                        Datastore.current.Payments.Add(pay);
                        Record.RecordNow(Datastore.current, pay);
                    }
                    if (Datastore.dataFile.Save())
                    {
                        if (editMode)
                        {
                            DialogResult = DialogResult.OK;
                            Close();
                        }
                        //dtPicker.Value = DateTime.Now;
                        txtAmount.Text = txtDocChqNo.Text = txtMT.Text = txtParticulars.Text = txtType.Text = "";
                        dtPicker.Focus();
                        loadAutoCollection();
                    }
                    else
                    {
                        MessageBox.Show(this, "Sorry, software is not able to save current record to datastore.", "Error: DATASTORE", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform saveon_newpayment operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            (sender as Control).BackColor = Color.White;
        }

        private void loadAutoCollection()
        {
            AutoCompleteStringCollection a1 = new AutoCompleteStringCollection(), a2 = new AutoCompleteStringCollection();
            foreach (Payment p in Datastore.current.Payments)
            {
                a1.Add(p.Particulars);
                a2.Add(p.Type);
            }
            foreach (String strType in Datastore.dataFile.PriorityTypes)
                a2.Add(strType);
            foreach (String strType in Datastore.dataFile.SpecialTypes)
                a2.Add(strType);

            txtParticulars.AutoCompleteCustomSource = a1;
            txtType.AutoCompleteCustomSource = a2;
        }

        private void loadGrades()
        {

            cmbGrade.Items.Clear();
            cmbGrade.Items.Add(new CMBItem("Default", ""));
            foreach (Grade gr in Datastore.dataFile.Grades)
            {
                CMBItem item = new CMBItem(gr.GradeName, gr.GradeName);
                cmbGrade.Items.Add(item);
            }
        }
    }
}
