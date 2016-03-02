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
    public partial class frmGradeGroups : Form
    {
        public frmGradeGroups()
        {
            InitializeComponent();

            olvColumnQty.AspectToStringConverter = (r) =>
            {
                return Job.Functions.MTToString(((double)r));
            };

            olvColumnMonthlyMin.AspectToStringConverter = (r) =>
            {
                return ((double)r).ToString("0.00");
            };

            olvColumnQuaterlyMin.AspectToStringConverter = (r) =>
            {
                return ((double)r).ToString("0.00");
            };

            olvColumnYearlyMin.AspectToStringConverter = (r) =>
            {
                return ((double)r).ToString("0.00");
            };

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmGradeGroups_Load(object sender, EventArgs e)
        {
            loadGradeGroups();
        }

        private void loadGradeGroups()
        {
            Thread thread = new Thread(() =>
            {
                List<classes.GradeGroup> groups = Job.GradeGroups.getAll(); ;

                Action act = () =>
                {
                    lvGroups.SetObjects(groups);
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading groups...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void btnNewGroup_Click(object sender, EventArgs e)
        {
            forms.frmNewGradeGroup ngg = new frmNewGradeGroup();
            if (ngg.ShowDialog(this) == DialogResult.OK)
                loadGradeGroups();
        }

        private void lvGroups_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((classes.DataReflector)e.RowObject).SetDataReflector = true;
        }
    }
}
