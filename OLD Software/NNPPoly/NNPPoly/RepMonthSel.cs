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
    public partial class RepMonthSel : Form
    {
        public int selMonth = 0;
        public int selYear = 0000;
        public RepMonthSel()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            enableAll();
            Button btn=(sender as Button);
            btn.Enabled = false;
            if (btn == button1) selMonth = 1;
            if (btn == button2) selMonth = 2;
            if (btn == button3) selMonth = 3;
            if (btn == button4) selMonth = 4;
            if (btn == button5) selMonth = 5;
            if (btn == button6) selMonth = 6;
            if (btn == button7) selMonth = 7;
            if (btn == button8) selMonth = 8;
            if (btn == button9) selMonth = 9;
            if (btn == button10) selMonth = 10;
            if (btn == button11) selMonth = 11;
            if (btn == button12) selMonth = 12;
            dtY.Focus();
        }

        private void enableAll()
        {
            button1.Enabled = button2.Enabled = button3.Enabled = button4.Enabled = button5.Enabled = button6.Enabled = button7.Enabled = button8.Enabled = button9.Enabled = button10.Enabled = button11.Enabled = button12.Enabled = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            selYear = dtY.Value.Year;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void RepMonthSel_Load(object sender, EventArgs e)
        {
            dtY.Value = DateTime.Now;
        }
    }
}
