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
    public partial class frmDebitNoteEntries : Form
    {
        private List<long> entries = new List<long>();

        public frmDebitNoteEntries(List<long> entries, bool isNote)
        {
            InitializeComponent();
            olvColumnDate.AspectToStringConverter = (c) =>
            {
                return ((DateTime)c).ToShortDateString();
            };

            olvColumnAmount.AspectGetter = (r) =>
            {
                classes.Payment payment = (classes.Payment)r;
                if (payment != null)
                {
                    if (isNote)
                    {
                        if (payment.tag == null)
                        {
                            classes.Client client = Job.Clients.get(payment.client_id);
                            payment.tag = client.cutoffdays;
                        }
                        return payment.grade.getAmount(payment.date) * (long)payment.tag * payment.mt;
                    }
                    else
                    {
                        return payment.debit_amount;
                    }
                }
                return 0;
            };

            olvColumnAmount.AspectToStringConverter = (c) =>
            {
                return Job.Functions.AmountToString(((double)c));
            };

            this.entries = entries;
        }

        private void frmDebitNoteEntries_Load(object sender, EventArgs e)
        {

        }

        private void frmDebitNoteEntries_Shown(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Action act = () =>
                {
                    lvEntries.ClearObjects();
                };
                Invoke(act);

                List<classes.Payment> payments = new List<classes.Payment>();
                foreach (long id in entries)
                {
                    classes.Payment p = Job.Payments.get(id);
                    if (p != null)
                        payments.Add(p);
                }

                act = () =>
                {
                    lvEntries.SetObjects(payments);
                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            thread.Name = "Thread: LoadingEntries";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading entries...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void lvEntries_DoubleClick(object sender, EventArgs e)
        {
            if (lvEntries.SelectedObjects.Count == 1)
            {
                classes.Payment p = lvEntries.SelectedObject as classes.Payment;
                frmNewEntry f = new frmNewEntry(p.client_id);
                f.setEditMode(ref p);
                f.ShowDialog(this);
                frmDebitNoteEntries_Shown(this, new EventArgs());
            }
        }

        private void lvEntries_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && lvEntries.SelectedObjects.Count > 0)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add("&Delete entries", lvEntries_deleteEntries);
                cm.Show(sender as Control, e.Location);
            }
        }

        private void lvEntries_deleteEntries(object sender, EventArgs e)
        {
            if (lvEntries.SelectedObjects.Count > 0)
            {
                if (MessageBox.Show(this, "Are you sure to delete all selected entries ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
                foreach (classes.Payment p in lvEntries.SelectedObjects)
                {
                    p.Delete();
                }

                lvEntries.RemoveObjects(lvEntries.SelectedObjects);
            }
        }
    }
}
