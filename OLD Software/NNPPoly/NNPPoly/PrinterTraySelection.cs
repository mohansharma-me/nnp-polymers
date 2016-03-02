using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
//using Word = Microsoft.Office.Interop.Word;

namespace NNPPoly
{
    public partial class PrinterTraySelection : Form
    {
        public PrinterTraySelection()
        {
            InitializeComponent();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadPrinters();
        }

        private void loadPrinters()
        {
            cmbPrinters.Items.Clear();
            cmbPrinters.SelectedIndex = -1;
            foreach (String printerName in PrinterSettings.InstalledPrinters)
            {
                int index=cmbPrinters.Items.Add(printerName);
                if (printerName.Equals(Datastore.dataFile.printerName))
                {
                    cmbPrinters.SelectedIndex = index;
                }
            }
            if (cmbPrinters.SelectedIndex == -1)
                cmbPrinters.SelectedIndex = cmbPrinters.Items.Count > 0 ? 0 : -1;
        }

        private void PrinterTraySelection_Load(object sender, EventArgs e)
        {
            
            loadPrinters();
        }

        private void cmbPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDATray.Items.Clear();
            cmbDNTray.Items.Clear();
            cmbETray.Items.Clear();
            if (cmbPrinters.SelectedIndex > -1)
            {
                String printerName = cmbPrinters.SelectedItem.ToString();
                PrinterSettings printerSettings = new PrinterSettings();
                printerSettings.PrinterName = printerName;
                foreach (PaperSource psource in printerSettings.PaperSources)
                {
                    int i1 = cmbDATray.Items.Add(new CMBItem(psource.Kind.ToString(), psource));
                    int i2 = cmbDNTray.Items.Add(new CMBItem(psource.Kind.ToString(), psource));
                    int i3 = cmbETray.Items.Add(new CMBItem(psource.Kind.ToString(), psource));
                    if (Datastore.dataFile.debitNotePaperSource!=null && psource.ToString().Equals(Datastore.dataFile.debitNotePaperSource.ToString()))
                        cmbDNTray.SelectedIndex = i2;
                    if (Datastore.dataFile.debitAdvisePaperSource!=null && psource.ToString().Equals(Datastore.dataFile.debitAdvisePaperSource.ToString()))
                        cmbDATray.SelectedIndex = i1;
                    if (Datastore.dataFile.envelopePaperSource!=null && psource.ToString().Equals(Datastore.dataFile.envelopePaperSource.ToString()))
                        cmbETray.SelectedIndex = i3;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Datastore.dataFile.printerName = cmbPrinters.SelectedItem.ToString();
            if (cmbDNTray.SelectedIndex > -1)
                Datastore.dataFile.debitNotePaperSource = (cmbDNTray.SelectedItem as CMBItem).Value as PaperSource;
            if (cmbDATray.SelectedIndex > -1)
                Datastore.dataFile.debitAdvisePaperSource = (cmbDATray.SelectedItem as CMBItem).Value as PaperSource;
            if (cmbETray.SelectedIndex > -1)
                Datastore.dataFile.envelopePaperSource = (cmbETray.SelectedItem as CMBItem).Value as PaperSource;
            Datastore.dataFile.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnRefresh_Click_1(object sender, EventArgs e)
        {

        }
    }
}
