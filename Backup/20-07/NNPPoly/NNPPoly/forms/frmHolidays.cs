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
    public partial class frmHolidays : Form
    {
        public frmHolidays()
        {
            InitializeComponent();

            olvColumnDate.AspectToStringConverter = (r) =>
            {
                return ((DateTime)r).ToShortDateString();
            };
        }

        private void frmHolidays_Load(object sender, EventArgs e)
        {
            loadHolidays();
        }

        private void loadHolidays()
        {
            Thread thread = new Thread(() =>
            {

                List<classes.Holiday> days = Job.Holidays.getAllHolidays();

                Action act = () =>
                {
                    textBox1.Text = "";
                    lvPayments.ClearObjects();
                    lvPayments.SetObjects(days);
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading holidays...", "", true, (f) => { }).ShowDialog(this);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime date = dateTimePicker1.Value;
            String desc = textBox1.Text.Trim();

            if (desc.Length == 0)
            {
                MessageBox.Show(this, "Please enter valid date description.", "No Description", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Focus();
                return;
            }

            if (!Job.Holidays.add(date, desc))
            {
                MessageBox.Show(this, "Sorry, unable to add new holiday, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                loadHolidays();
            }

        }

        private void lvPayments_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((classes.Holiday)e.RowObject).SetDataReflector = true;
        }

        private void btnAddSundays_Click(object sender, EventArgs e)
        {

        }

        private void lvPayments_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void lvPayments_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (lvPayments.SelectedObjects.Count > 0)
                {
                    if (MessageBox.Show(this, "Are you sure to delete all selected holidays ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

                    foreach (classes.Holiday h in lvPayments.SelectedObjects)
                    {
                        h.Delete();
                    }

                    loadHolidays();
                }
            }
        }
    }
}
