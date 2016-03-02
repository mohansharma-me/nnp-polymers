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
    public partial class frmDebiPrioritiesAndTypes : Form
    {
        public frmDebiPrioritiesAndTypes()
        {
            InitializeComponent();
        }

        private void frmDebiPrioritiesAndTypes_Shown(object sender, EventArgs e)
        {
            load();
        }

        private void load()
        {
            Thread th = new Thread(() =>
            {
                Action act = () =>
                {
                    lvDetails.Visible = false;
                };
                Invoke(act);

                if (lvDetails.Columns.Count == 0)
                    Job.PrioritiesAndTypes.generateColumns((c) =>
                    {
                        act = () =>
                        {
                            lvDetails.Columns.Add(c);
                        };
                        Invoke(act);
                    });

                List<classes.DebitPriorities> dps = new List<classes.DebitPriorities>();
                Job.PrioritiesAndTypes.getAll((classes.DebitPriorities dp) =>
                {
                    dps.Add(dp);
                });

                act = () =>
                {
                    lvDetails.SetObjects(dps);
                    lvDetails.Visible = true;
                    frmProcess.publicClose();
                };
                Invoke(act);
            });
            th.Name = "Thread: DebitPriorities&Types";
            th.Priority = ThreadPriority.Highest;
            th.Start();
            new frmProcess("Debit priorities & types", "Fetching", true, (c) => { }).ShowDialog(this);
        }

        private void btnAddDP_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                submit();
            }
            else
            {
                MessageBox.Show(this, "Please enter valid debit type name.", "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            txtType.BackColor = Job.Validation.ValidateString(txtType.Text) ? Color.White : Color.Red;
        }

        private bool validate()
        {
            txtType_TextChanged(txtType, new EventArgs());

            return txtType.BackColor == Color.White;
        }

        private void submit()
        {
            if (Job.PrioritiesAndTypes.add(txtType.Text.Trim(), chkSpecial.Checked))
            {
                load();
            }
            else
            {
                MessageBox.Show(this, "Ohh, unexpected error occured, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvDetails_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.RowObject != null)
            {
                ((classes.DebitPriorities)e.RowObject).SetDataReflector = true;
            }
        }

        private void lvDetails_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {

        }

        private void lvDetails_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void lvDetails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                DialogResult dr = MessageBox.Show(this, "Are you sure to delete all selecteds debit types ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (lvDetails.SelectedObjects.Count > 0 && dr==DialogResult.Yes)
                {
                    foreach (classes.DebitPriorities dp in lvDetails.SelectedObjects)
                    {
                        dp.Delete();
                    }
                    load();
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }
    }
}
