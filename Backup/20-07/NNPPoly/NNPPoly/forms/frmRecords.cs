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
    public partial class frmRecords : Form
    {
        private int lastClientId = -1;

        public frmRecords()
        {
            InitializeComponent();

            olvColumnAmount.AspectToStringConverter = (c) =>
            {
                return Job.Functions.AmountToString((double)c);
            };

            olvColumnDate.AspectToStringConverter = (c) =>
            {
                if (c == null) return "";
                return ((DateTime)c).ToShortDateString();
            };

            olvColumnGrade.AspectToStringConverter = (c) =>
            {
                if (c == null) return "";
                return ((classes.Grade)c).code;
            };

            olvColumnMT.AspectToStringConverter = (c) =>
            {
                if (c == null) return "";
                return Job.Functions.MTToString(((double)c));
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
                            index=cmbClients.Items.Add(new ComboItem(c.name, c.id));
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
                    if (lastClientId > -1 && index>-1 && index < cmbClients.Items.Count - 1)
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
            findRecords(chkAllDates.Checked, dtDate.Value, clientId, cmbPaymentType.SelectedItem.ToString());
        }

        public void findRecords(bool allDates, DateTime date, long clientId, String type)
        {
            lvPayments.ClearObjects();
            Thread thread = new Thread(() =>
            {
                Action act = null;
                List<classes.Record> records = new List<classes.Record>();
                double totalAmount = 0, totalMT=0;
                Job.Payments.records(allDates, date, clientId, type, (classes.Record r) =>
                {
                    records.Add(r);
                    totalAmount += r.payment.amount;
                    totalMT += r.payment.mt;
                });
                act = () =>
                {
                    lvPayments.SetObjects(records);
                    lvPayments.OverlayText.Text = "Rs. " + Job.Functions.AmountToString(totalAmount) + ". (MT: " + Job.Functions.MTToString(totalMT) + ")";
                    lvPayments.RefreshOverlays();
                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            thread.Name = "Thread: findRecords";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Records", "searching records...", true, (c) => { }).ShowDialog(this);
        }

        private void lvPayments_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((classes.Record)e.RowObject).payment.SetDataReflector = true;
        }

        private void lvPayments_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvPayments.SelectedObjects.Count > 0 && e.KeyCode==Keys.Delete)
            {
                deleteRecords();
            }
        }

        private void deleteRecords()
        {
            DialogResult dr = MessageBox.Show(this, "Are you sure to delete all selected record entries ?", "Delete Record Entries", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                List<long> paymentIds = new List<long>();
                foreach (classes.Record record in lvPayments.SelectedObjects)
                    if (record.payment != null)
                        paymentIds.Add(record.payment.id);

                Thread thread = new Thread(() =>
                {
                    Action act = () =>
                    {
                        frmProcess.getInstance().pbProcess.Maximum = paymentIds.Count;
                        frmProcess.getInstance().pbProcess.Value = 0;
                    };
                    Invoke(act);

                    bool flag = true;

                    foreach (long id in paymentIds)
                    {
                        flag = flag && Job.Payments.deletePayment(id);

                        act = () =>
                        {
                            frmProcess.getInstance().pbProcess.Value++;
                        };
                        Invoke(act);

                    }

                    if (flag)
                    {
                        act = () =>
                        {
                            MessageBox.Show(this, "All selected records successfully deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        };
                        Invoke(act);
                    }
                    else
                    {
                        act = () =>
                        {
                            MessageBox.Show(this, "Can't delete records, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        };
                        Invoke(act);
                    }

                    act = () =>
                    {
                        frmProcess.publicClose();
                    };
                    Invoke(act);

                });
                thread.Name = "Thread: DeleteRecords";
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                new frmProcess("Delete Record Entries", "Deleting...", true, (fc) => { }).ShowDialog(this);
                btnShow_Click(btnShow, new EventArgs());

            }
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

        private void lvPayments_CellClick(object sender, BrightIdeasSoftware.CellClickEventArgs e)
        {
            if (lvPayments.SelectedObjects.Count == 1 && e.ClickCount == 2 && e.ColumnIndex == 0)
            {
                classes.Record record = (classes.Record)lvPayments.SelectedObjects[e.RowIndex];
                frmNewEntry newEntry = new frmNewEntry(record.client_id);
                newEntry.setEditMode(ref record.payment);
                newEntry.ShowDialog(this);
            }
        }

        private void lvPayments_MouseDown(object sender, MouseEventArgs e)
        {
            if (lvPayments.SelectedObjects.Count > 0 && e.Button==System.Windows.Forms.MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                if (lvPayments.SelectedObjects.Count == 1)
                    cm.MenuItems.Add(new MenuItem("&Edit entry", lvPayments_RightClickDown));
                cm.MenuItems.Add("&Delete entries", lvPayments_RightClickDown);

                cm.Show(sender as Control, e.Location);
            }
        }

        private void lvPayments_RightClickDown(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi.Text.StartsWith("&Edit"))
            {
                if (lvPayments.SelectedObjects.Count == 1)
                {
                    classes.Record rec = lvPayments.SelectedObjects[0] as classes.Record;
                    if (rec != null)
                    {
                        classes.Payment p = rec.payment;
                        if (p != null)
                        {
                            frmNewEntry ne = new frmNewEntry(p.client_id);
                            ne.setEditMode(ref p);
                            ne.ShowDialog(this);
                        }
                    }
                }
            }
            else if (mi.Text.StartsWith("&Delete"))
            {
                deleteRecords();
            }
        }

        private void frmRecords_Load(object sender, EventArgs e)
        {

        }
    }
}
