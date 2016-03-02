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

                String allids = " ";
                foreach (String id in ids)
                    allids += id + " ";

                List<Payment> pays = Datastore.current.Payments.FindAll(x => (allids.Contains(" " + x.ID.ToString() + " ")));
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
                }

                Datastore.dataFile.Save();
                DialogResult = DialogResult.OK;
                Close();

            }
            catch (Exception excep)
            {
                String err = "Unable to perform save_setvalues operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).BackColor = Color.White;
        }
    }
}
