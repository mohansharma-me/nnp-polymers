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
    public partial class FilterPeriod : Form
    {
        public DateTime fromDate, toDate;
        public FilterPeriod()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            fromDate = dtFrom.Value;
            toDate = dtTo.Value;
            Close();
        }

        private void FilterPeriod_Load(object sender, EventArgs e)
        {
            dtTo.Value = dtFrom.Value = DateTime.Now;
        }
    }
}
