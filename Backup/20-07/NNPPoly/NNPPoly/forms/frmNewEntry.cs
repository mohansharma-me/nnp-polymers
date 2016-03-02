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
    public partial class frmNewEntry : Form
    {
        private long defaultClientId = -1;
        private bool editMode = false;
        private NNPPoly.classes.Payment editPayment = null;

        public frmNewEntry(long defaultClientId)
        {
            InitializeComponent();
            this.defaultClientId = defaultClientId;
        }

        public void setEditMode(ref NNPPoly.classes.Payment p)
        {
            editMode = true;
            editPayment = p;
        }

        private void frmNewEntry_Load(object sender, EventArgs e)
        {
            dtPicker.Value = DateTime.Now;
            cmbClients.Items.Clear();
            cmbGrades.Items.Clear();
            long ID = 0;
            cmbGrades.Items.Add(new ComboItem("Default", ID));
            Thread thread = new Thread(() => {
                try
                {
                    int currentClientIndex = 0;
                    Action act;
                    Job.Clients.search("", 0, 0, (NNPPoly.classes.Client c) => {
                        act = () => {
                            int index=cmbClients.Items.Add(new ComboItem(c.name, c.id));
                            if (c.id == defaultClientId)
                                currentClientIndex = index;
                        };
                        Invoke(act);
                    });
                    int gradeIndex = 0;
                    Job.Grades.getAllGrades(0,false,true,(NNPPoly.classes.Grade g) => {
                        act = () => {
                            int tmp=cmbGrades.Items.Add(new ComboItem(g.code, g.id));
                            if (editPayment != null && editPayment.grade.id == g.id)
                                gradeIndex = tmp;
                        };
                        Invoke(act);
                    });

                    act = () => {
                        if (cmbClients.Items.Count == 0)
                        {
                            MessageBox.Show(this, "Sorry, this company didn't have any clients yet, please add at least one client profile.", "No Clients", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Close();
                        }
                        else
                        {
                            cmbClients.SelectedIndex = currentClientIndex;
                            cmbGrades.SelectedIndex = gradeIndex;
                        }
                    };
                    Invoke(act);
                }
                catch (Exception ex)
                {
                    Job.Log("Error[NewEntry_LoadThread]", ex);
                }
                finally
                {
                    Action act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);
                }
            });
            thread.Name = "Thread: LoadClient-frmNewEntry";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Processing", "Gathering client details...", true, (frmProcess t) => {

            }).ShowDialog(this);

            if (editMode && editPayment != null)
            {
                txtAmount.Text = editPayment.amount.ToString("0.00");
                txtInvoice.Text = editPayment.invoice;
                if (editPayment.isDebitNote)
                    txtInvoice.ReadOnly = true;
                txtParticulars.Text = editPayment.particulars;
                txtType.Text = editPayment.type;
                dtPicker.Value = editPayment.date;
                if (editPayment.mode == classes.Payment.PaymentMode.Debit)
                {
                    chkDebit.Checked = gbDebit.Enabled = true;
                    txtMT.Text = editPayment.mt.ToString("0.000");
                }
            }
        }

        private void txtInvoice_TextChanged(object sender, EventArgs e)
        {
            if (Job.Validation.ValidateString((sender as TextBox).Text.Trim()))
            {
                (sender as TextBox).BackColor = Color.White;
            }
            else
            {
                (sender as TextBox).BackColor = Color.Red;
            }
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            if (Job.Validation.ValidateDouble((sender as TextBox).Text))
            {
                (sender as TextBox).BackColor = Color.White;
            }
            else
            {
                (sender as TextBox).BackColor = Color.Red;
            }
        }

        private void chkDebit_CheckedChanged(object sender, EventArgs e)
        {
            gbDebit.Enabled = chkDebit.Checked;
            if (chkDebit.Checked)
            {
                txtMT.Focus();
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                if (editMode && editPayment != null)
                {
                    editPayment.SetDataReflector = true; editPayment.amount = double.Parse(txtAmount.Text.Trim());
                    editPayment.SetDataReflector = true; editPayment.client_id = (long)(cmbClients.SelectedItem as ComboItem).Value;
                    editPayment.SetDataReflector = true; editPayment.date = dtPicker.Value;
                    editPayment.SetDataReflector = true; editPayment.grade = Job.Grades.getGrade(long.Parse((cmbGrades.SelectedItem as ComboItem).Value.ToString()), false);
                    editPayment.SetDataReflector = true; editPayment.invoice = txtInvoice.Text.Trim();
                    editPayment.SetDataReflector = true; editPayment.mode = chkDebit.Checked ? NNPPoly.classes.Payment.PaymentMode.Debit : NNPPoly.classes.Payment.PaymentMode.Credit;
                    if (chkDebit.Checked)
                    {
                        editPayment.SetDataReflector = true; editPayment.mt = double.Parse(txtMT.Text.Trim());
                    }
                    MessageBox.Show(this, "Saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = defaultClientId == editPayment.client_id ? DialogResult.OK : DialogResult.Ignore;
                    Close();
                }
                else
                {
                    submit(true);
                }
            }
            else
            {
                MessageBox.Show(this, "Please enter all required data, please try again.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void submit(bool closeIt)
        {
            long clientId = (long)((ComboItem)cmbClients.SelectedItem).Value;
            DateTime date = dtPicker.Value;
            String invoice = txtInvoice.Text.Trim();
            String type = txtType.Text.Trim();
            String parts = txtParticulars.Text.Trim();
            double amount = double.Parse(txtAmount.Text.Trim());
            double mt = 0;
            long gradeId = 0;
            if (chkDebit.Checked)
            {
                mt = double.Parse(txtMT.Text.Trim());
                gradeId = (long)(cmbGrades.SelectedItem as ComboItem).Value;
            }

            Thread th = new Thread(() => {
                Action act;
                long newid=0;
                if (Job.Payments.add(ref newid, clientId, date, invoice, type, parts, chkDebit.Checked ? NNPPoly.classes.Payment.PaymentMode.Debit : NNPPoly.classes.Payment.PaymentMode.Credit, amount, mt, gradeId, 0))
                {
                    act = () => {
                        txtAmount.Text = "";
                        txtInvoice.Text = "";
                        txtMT.Text = "";
                        txtParticulars.Text = "";
                        txtType.Text = "";
                        cmbGrades.SelectedIndex = 0;
                        dtPicker.Focus();
                        txtAmount.BackColor =
                            txtInvoice.BackColor =
                            txtMT.BackColor =
                            txtParticulars.BackColor =
                            txtType.BackColor = Color.White;
                    };
                    Invoke(act);
                }
                act = () => { frmProcess.publicClose(); };
                Invoke(act);
            });
            th.Start();
            new frmProcess("Adding new entry", "updating database...", true, (c) => { }).ShowDialog(this);
        }

        private bool validate()
        {
            txtInvoice_TextChanged(txtInvoice, new EventArgs());
            txtInvoice_TextChanged(txtType, new EventArgs());
            txtInvoice_TextChanged(txtParticulars, new EventArgs());
            txtAmount_TextChanged(txtAmount, new EventArgs());
            if (chkDebit.Checked)
            {
                txtAmount_TextChanged(txtMT, new EventArgs());
            }

            return txtAmount.BackColor == Color.White &&
                txtInvoice.BackColor == Color.White &&
                (!chkDebit.Checked || (chkDebit.Checked && txtMT.BackColor == Color.White && cmbGrades.SelectedIndex > -1)) &&
                txtParticulars.BackColor == Color.White &&
                txtType.BackColor == Color.White;
        }
    }
}
