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
    public partial class GetValue : Form
    {
        public String Value = "";
        public GetValue(String title,String label,bool multipleLines,String defaultValue="")
        {
            InitializeComponent();
            this.Text = title;
            label1.Text = label;
            this.textBox1.Multiline = multipleLines;
            this.textBox1.Text = defaultValue;
            this.Value = defaultValue;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Value = textBox1.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
