using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly
{
    public partial class PrintFormatSettings : Form
    {
        public PrintFormatSettings()
        {
            InitializeComponent();
        }

        private void PrintFormatSettings_Load(object sender, EventArgs e)
        {
            txtCurrentDNo.Text = Datastore.dataFile.currentDebitNoteID.ToString();
            txtTitle.Text = Datastore.dataFile.titleOfaDNote;
            txtDNAddress.Text = Datastore.dataFile.addressOfaDNote;
            txtDNDescRow.Text = Datastore.dataFile.descriptionRowOfaDNote;
            txtDAAddress.Text = Datastore.dataFile.addressOfaDAdvise;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            long curDNo = 0;
            if (!long.TryParse(txtCurrentDNo.Text, out curDNo))
            {
                MessageBox.Show(this, "Please enter valid current debit number.", "Debit number", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Datastore.dataFile.currentDebitNoteID = curDNo;
            Datastore.dataFile.titleOfaDNote = txtTitle.Text;
            Datastore.dataFile.addressOfaDAdvise = txtDAAddress.Text;
            Datastore.dataFile.addressOfaDNote = txtDNAddress.Text;
            Datastore.dataFile.descriptionRowOfaDNote = txtDNDescRow.Text;
            Datastore.dataFile.Save();
            Close();
        }

        private void btnTraySelection_Click(object sender, EventArgs e)
        {
            PrinterTraySelection pts = new PrinterTraySelection();
            pts.ShowDialog(this);
        }
    }
}
