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
    public partial class WaitingDialog : Form
    {
        private bool isVisible = true;
        public WaitingDialog()
        {
            InitializeComponent();
        }

        public WaitingDialog(bool isVisible)
        {
            InitializeComponent();
            this.isVisible = isVisible;
        }

        private void WaitingDialog_Load(object sender, EventArgs e)
        {
            
        }

        private void WaitingDialog_Shown(object sender, EventArgs e)
        {
            
        }

        public ProgressBar getProgressBar()
        {
            return progressBar1;
        }

        public Label getLabel()
        {
            return label1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public Button getCloseButton()
        {
            return btnClose;
        }
    }
}
