using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmPrintFormatSettings : Form
    {
        public frmPrintFormatSettings()
        {
            InitializeComponent();
        }

        private void frmPrintFormatSettings_Load(object sender, EventArgs e)
        {

        }

        private void loadPrinters()
        {
            Thread thread = new Thread(() =>
            {
                Action act = () =>
                {
                    cmbPrinters.Items.Clear();
                    cmbPrinters.SelectedIndex = -1;
                };
                Invoke(act);

                foreach (String printerName in PrinterSettings.InstalledPrinters)
                {
                    int index = -1;
                    act = () =>
                    {
                        index = cmbPrinters.Items.Add(printerName);
                    };
                    Invoke(act);

                    if (printerName.Equals(Job.GeneralSettings.printer()))
                    {
                        act = () =>
                        {
                            cmbPrinters.SelectedIndex = index;
                        };
                        Invoke(act);
                    }
                }
                act = () =>
                {
                    if (cmbPrinters.SelectedIndex == -1)
                        cmbPrinters.SelectedIndex = cmbPrinters.Items.Count > 0 ? 0 : -1;
                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            thread.Name = "Thread: loadPrinters";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading printers...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            loadPrinters();
        }

        private void frmPrintFormatSettings_Shown(object sender, EventArgs e)
        {
            loadPrinters();
            txtMidRow.Text = Job.GeneralSettings.mid_row_text();
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
                    int i1 = cmbDATray.Items.Add(new ComboItem(psource.Kind.ToString(), psource));
                    int i2 = cmbDNTray.Items.Add(new ComboItem(psource.Kind.ToString(), psource));
                    int i3 = cmbETray.Items.Add(new ComboItem(psource.Kind.ToString(), psource));
                    if (Job.GeneralSettings.dnote_tray() != null && psource.Kind.ToString().Equals(Job.GeneralSettings.dnote_tray()))
                        cmbDNTray.SelectedIndex = i2;
                    if (Job.GeneralSettings.dadvise_tray() != null && psource.Kind.ToString().Equals(Job.GeneralSettings.dadvise_tray()))
                        cmbDATray.SelectedIndex = i1;
                    if (Job.GeneralSettings.envlope_tray() != null && psource.Kind.ToString().Equals(Job.GeneralSettings.envlope_tray()))
                        cmbETray.SelectedIndex = i3;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String printer = cmbPrinters.SelectedIndex > -1 ? cmbPrinters.SelectedItem.ToString() : "";
            String dnote = cmbDNTray.SelectedIndex > -1 ? cmbDNTray.SelectedItem.ToString() : "";
            String dadvise = cmbDATray.SelectedIndex > -1 ? cmbDATray.SelectedItem.ToString() : "";
            String denvelope = cmbETray.SelectedIndex > -1 ? cmbETray.SelectedItem.ToString() : "";
            String midrow = txtMidRow.Text.Trim();

            Thread thread = new Thread(() =>
            {
                Job.GeneralSettings.printer(printer);
                Job.GeneralSettings.dnote_tray(dnote);
                Job.GeneralSettings.dadvise_tray(dadvise);
                Job.GeneralSettings.envlope_tray(denvelope);
                Job.GeneralSettings.mid_row_text(midrow);
                Action act = () =>
                {
                    frmProcess.publicClose();
                    Close();
                };
                Invoke(act);
            });
            thread.Name = "Thread: savePrintFormatSetting";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Saving", "", true, (f) => { }).ShowDialog(this);
        }
    }
}
