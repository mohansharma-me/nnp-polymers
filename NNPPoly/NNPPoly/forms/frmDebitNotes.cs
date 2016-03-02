using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmDebitNotes : Form
    {
        private int lastClientId = -1;

        public frmDebitNotes()
        {
            InitializeComponent();

            olvColumnAmount.AspectToStringConverter = (c) =>
            {
                if (c == null) return "";
                return Job.Functions.AmountToString((double)c);
            };

            olvColumnDate.AspectToStringConverter = (c) =>
            {
                if (c == null) return "";
                return ((DateTime)c).ToShortDateString();
            };
        }

        public void loadClients()
        {
            cmbClients.Items.Clear();
            Thread thread = new Thread(() =>
            {
                int index = -1;
                Action act = null;
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    act = () =>
                    {
                        if (c.id == lastClientId)
                        {
                            index = cmbClients.Items.Add(new ComboItem(c.name, c.id));
                        }
                        else
                        {
                            cmbClients.Items.Add(new ComboItem(c.name, c.id));
                        }
                    };
                    Invoke(act);
                });
                act = () =>
                {
                    if (lastClientId > -1 && index > -1 && index < cmbClients.Items.Count - 1)
                    {
                        cmbClients.SelectedIndex = index;
                    }
                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            thread.Name = "Thread: LoadClientsinRrcords";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Fetching client list...", "Querying database...", true, (c) => { }).ShowDialog(this);
            if (cmbPaymentType.SelectedIndex == -1)
            {
                cmbPaymentType.SelectedIndex = 0;
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            long clientId = 0;
            if (cmbClients.SelectedIndex > -1)
            {
                clientId = (long)(cmbClients.SelectedItem as ComboItem).Value;
            }
            findNotes(chkAllDates.Checked, dtDate.Value, clientId, cmbPaymentType.SelectedItem.ToString());
        }

        private void findNotes(bool allDates, DateTime date, long clientId, String isNote)
        {
            lvPayments.ClearObjects();
            Thread thread = new Thread(() =>
            {
                Action act = null;
                List<classes.DebitNote> dnotes = new List<classes.DebitNote>();
                Job.DebitNotes.find(allDates, date, clientId, isNote.ToString().Equals("Debit Notes") ? true : false, (classes.DebitNote r) =>
                {
                    dnotes.Add(r);
                });
                act = () =>
                {
                    lvPayments.SetObjects(dnotes);
                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            thread.Name = "Thread: findDebitNotes";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Debit Notes & Advises", "searching records...", true, (c) => { }).ShowDialog(this);
        }

        private void lvPayments_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void lvPayments_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lvPayments_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void printDebitNotes(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                System.Collections.IList list = null;

                Action act = () =>
                {
                    list = lvPayments.SelectedObjects;
                };
                Invoke(act);

                List<long> dnotes = new List<long>();
                List<long> dadvises = new List<long>();

                foreach (classes.DebitNote dn in list)
                {
                    if (dn.isDNote) dnotes.Add(dn.id);
                    if (!dn.isDNote) dadvises.Add(dn.id);
                }

                if (dnotes.Count > 0)
                {
                    act = () =>
                    {
                        //new forms.frmDebitNotePrinter(dnotes, true).ShowDialog(this);
                        forms.frmDebitNotePrinter dp = new forms.frmDebitNotePrinter(dnotes, true);
                        dp.ShowDialog(this);
                    };
                    Invoke(act);
                }

                if (dadvises.Count > 0)
                {
                    act = () =>
                    {
                        //new forms.frmDebitAdvisePrinter(dadvises, true).ShowDialog(this);
                        forms.frmDebitAdvisePrinter dp = new forms.frmDebitAdvisePrinter(dadvises, true);
                        dp.ShowDialog(this);
                    };
                    Invoke(act);
                }

                act = () =>
                {
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading Debit Notes...", "", true, (f) => { }).ShowDialog(this);

        }

        private void lvPayments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button==MouseButtons.Left)
            {
                showEntries(sender, e);
            }
        }

        private void showEntries(object sender, EventArgs e)
        {
            if (lvPayments.SelectedObjects.Count == 1)
            {
                classes.DebitNote dn = ((classes.DebitNote)lvPayments.SelectedObjects[0]);
                List<classes.DebitNote.PaymentEntry> entries = dn.entries;
                List<long> pids = new List<long>();
                foreach (classes.DebitNote.PaymentEntry pe in entries)
                    pids.Add(pe.paymentId);
                new frmDebitNoteEntries(pids, dn.isDNote).ShowDialog(this);
            }
        }

        private void lvPayments_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lvPayments.SelectedObjects.Count > 0)
            {
                ContextMenu cm = new System.Windows.Forms.ContextMenu();
                cm.MenuItems.Add(new MenuItem("&Show entries", showEntries));
                cm.MenuItems.Add(new MenuItem("&Print It", printDebitNotes));
                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Delete It", deleteDebitNotes));
                cm.Show(sender as Control, e.Location);
            }
        }

        private void deleteDebitNotes(object sender, EventArgs e)
        {
            if (lvPayments.SelectedObjects.Count == 0) return;

            if (MessageBox.Show(this, "Are you sure to delete all selected debit notes/advises with all its entries in system ?", "Delete DNote/Advise ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;



            Thread thread = new Thread(() =>
            {
                System.Collections.IList list = null;

                Action act = () =>
                {
                    list = lvPayments.SelectedObjects;
                };
                Invoke(act);

                if (list == null)
                {
                    act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);
                    return;
                }

                foreach (classes.DebitNote dn in list)
                {
                    Job.DebitNotes.delete(dn);
                }

                act = () =>
                {
                    MessageBox.Show(this, "All selected debit note/advise(s) are removed from system.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Deleting debit notes/advises", "", true, (fc) => { }).ShowDialog(this);
        }

    }
}
