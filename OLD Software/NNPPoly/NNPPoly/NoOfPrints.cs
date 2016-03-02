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
    public partial class NoOfPrints : Form
    {
        public Decimal ValueFromUser = 0;
        public NoOfPrints()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ValueFromUser = numCopies.Value;
            if (numCopies.Value != 0)
            {
                Datastore.dataFile.noOfCopies = numCopies.Value;
                Datastore.dataFile.Save();
            }
            DialogResult = DialogResult.OK;
            Close();    
        }

        private void NoOfPrints_Load(object sender, EventArgs e)
        {
            numCopies.Value = Datastore.dataFile.noOfCopies;
            ValueFromUser = numCopies.Value;
        }

        private void NoOfPrints_Shown(object sender, EventArgs e)
        {
            numCopies.Focus();
            numCopies.Select(0, numCopies.Value.ToString().Length);
        }
    }
}
