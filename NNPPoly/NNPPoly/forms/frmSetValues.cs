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
    public partial class frmSetValues : Form
    {
        private String[] ids;
        private String field;
        public frmSetValues(String[] ids,String field)
        {
            InitializeComponent();
            this.ids = ids;
            this.field = field;
        }

        private void frmSetValues_Load(object sender, EventArgs e)
        {
            Text = "Set "+field;
            label1.Text = field+" :";
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                String value = textBox1.Text.Trim();
                if (value.Length == 0)
                {
                    textBox1.BackColor = Color.Red;
                    textBox1.Focus();
                    return;
                }
                double mt = 0;
                if (field.Contains("MT") && !double.TryParse(value, out mt))
                {
                    textBox1.BackColor = Color.Red;
                    textBox1.Focus();
                    return;
                }

                foreach (String id in ids)
                {
                    NNPPoly.classes.Payment p = Job.Payments.get(long.Parse(id));
                    if (p != null)
                    {
                        p.SetDataReflector = true;
                        if (field.Contains("Invoice no"))
                        {
                            p.invoice = value;
                        }
                        if (field.Contains("Type"))
                        {
                            p.type = value;
                        }
                        if (field.Contains("Particulars"))
                        {
                            p.particulars = value;
                        }
                        if (field.Contains("MT"))
                        {
                            p.mt = mt;
                        }
                        p.SetDataReflector = false;
                        p = null;
                    }
                }

                /*List<Payment> pays = Datastore.current.Payments.FindAll(x => (allids.Contains(" " + x.ID.ToString() + " ")));
                foreach (Payment p in pays)
                {
                    if (field.Contains("Invoice no"))
                    {
                        p.DocChqNo = value;
                    }
                    if (field.Contains("Type"))
                    {
                        p.Type = value;
                    }
                    if (field.Contains("Particulars"))
                    {
                        p.Particulars = value;
                    }
                    if (field.Contains("MT"))
                    {
                        p.MT = mt.ToString();
                    }
                }*/

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform save_setvalues operation.";
                Job.Log(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = Color.White;
        }
    }
}
