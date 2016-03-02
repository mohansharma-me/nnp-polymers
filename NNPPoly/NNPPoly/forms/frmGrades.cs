using NNPPoly.classes;
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
    public partial class frmGrades : Form
    {
        public frmGrades()
        {
            InitializeComponent();
        }

        private void frmGrades_Load(object sender, EventArgs e)
        {

        }

        private void frmGrades_Shown(object sender, EventArgs e)
        {
            loadSessions();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNewSession_Click(object sender, EventArgs e)
        {
            frmNewGradeSession ngs = new frmNewGradeSession();
            ngs.ShowDialog(this);
            loadSessions();
        }

        private void loadSessions()
        {
            Thread thread = new Thread(() =>
            {
                Action act = () => { cmbSessions.Items.Clear(); lvDetails.ClearObjects(); };
                Invoke(act);

                List<GradeSession> sessions = Job.Grades.getAllSessions();
                if (sessions == null)
                {
                    act = () =>
                    {
                        MessageBox.Show(this, "There is something gone wierd, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                    Invoke(act);
                }
                else
                {
                    foreach (GradeSession session in sessions)
                    {
                        act = () =>
                        {
                            cmbSessions.Items.Add(new ComboItem(session.ToString(), session.id));
                        };
                        Invoke(act);
                    }
                }

                act = () => { frmProcess.publicClose(); };
                Invoke(act);

            });
            thread.Name = "Thread: loadSessions";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading sessions...", "", true, (fc) => { }).ShowDialog(this);
        }

        private void btnAddGrade_Click(object sender, EventArgs e)
        {
            frmGrade ng = new frmGrade();
            ng.ShowDialog(this);
            cmbSessions.SelectedIndex = -1;
            loadSessions();
        }

        private void cmbSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvDetails.ClearObjects();
            if (cmbSessions.SelectedIndex >= 0)
            {
                long sessionId = (long)(cmbSessions.SelectedItem as ComboItem).Value;
                Thread thread = new Thread(() =>
                {
                    Action act = () => { };

                    List<classes.Grade> grades = Job.Grades.getAllGrades(sessionId);

                    act = () =>
                    {
                        lvDetails.SetObjects(grades);
                        frmProcess.publicClose();
                    };
                    Invoke(act);

                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                new frmProcess("Loading Grades...", "", true, (fc) => { }).ShowDialog(this);
            }

        }

        private void lvDetails_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            ((classes.Grade)e.RowObject).SetDataReflector = true;
            if (e.Column == olvColumnGradeGroup)
            {
                long groupId = (e.RowObject as classes.Grade).group.id;
                ComboBox cmb = new ComboBox();
                cmb.Items.Add(new ComboItem("Default", 0));
                Job.GradeGroups.getAll((classes.GradeGroup gg) =>
                {
                    int index = cmb.Items.Add(new ComboItem(gg.name, gg.id));
                    if (groupId == gg.id)
                        cmb.SelectedIndex = index;
                }, Job.Companies.currentCompany.id);
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
                if (cmb.SelectedIndex == -1)
                    cmb.SelectedIndex = 0;
                cmb.Bounds = e.CellBounds;
                cmb.SelectedIndexChanged += cmb_SelectedIndexChanged;
                e.Control = cmb;
            }
        }

        void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnEditSession_Click(object sender, EventArgs e)
        {
            if (cmbSessions.SelectedIndex >= 0)
            {
                frmNewGradeSession ngs = new frmNewGradeSession();
                ngs.setEditMode((long)(cmbSessions.SelectedItem as ComboItem).Value);
                ngs.ShowDialog(this);
                loadSessions();
            }
        }

        private void btnDeleteSession_Click(object sender, EventArgs e)
        {
            if (cmbSessions.SelectedIndex >= 0)
            {
                long sessionId = (long)(cmbSessions.SelectedItem as ComboItem).Value;
                if (MessageBox.Show(this, "Are you sure to delete selected session with all its extended amount ?", "Delete Session", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Thread thread = new Thread(() =>
                    {
                        if (Job.Grades.deleteSession(sessionId))
                        {
                            Action act = () =>
                            {
                                MessageBox.Show(this, "Deleted successfully.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            };
                            Invoke(act);
                        }
                        else
                        {
                            Action act = () =>
                            {
                                MessageBox.Show(this, "Can't delete session, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            };
                            Invoke(act);
                        }

                        Action act1 = () =>
                        {
                            frmProcess.publicClose();
                        };
                        Invoke(act1);
                    });
                    thread.Priority = ThreadPriority.Highest;
                    thread.Name = "Thread: deleteSessionThread";
                    thread.Start();
                    new frmProcess("Deleting session...", "", true, (fc) => { }).ShowDialog(this);
                    loadSessions();
                }
            }
        }

        private void btnStockAlert_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                System.Collections.IList list = null;

                Action act = () =>
                {
                    list = lvDetails.CheckedObjects;
                };
                Invoke(act);

                List<MessageHolder> holders = new List<MessageHolder>();

                foreach (classes.Grade g in list)
                {
                    if (g.id > 0)
                    {
                        String _ids = Job.Grades.getGradeClients(g.id);
                        if (_ids != null)
                        {
                            String[] ids = _ids.Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (String id in ids)
                            {
                                MessageHolder holder = Job.Messages.prepareStock(long.Parse(id.Trim()), g.code);
                                if (holder != null) holders.Add(holder);
                            }
                        }
                    }
                }

                act = () =>
                {
                    if (holders.Count == 0)
                    {
                        MessageBox.Show(this, "Sorry, no clients found who placed one of these selected grades.", "No Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        new forms.frmMessageSender(holders).ShowDialog(this);
                    }

                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Loading clients...", "", true, (f) => { }).ShowDialog(this);
        }

        private void lvDetails_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Column == olvColumnGradeGroup)
            {
                if (e.Control is ComboBox && e.RowObject is classes.Grade)
                {
                    long gradeId = (e.RowObject as classes.Grade).id;
                    long groupId = long.Parse(((e.Control as ComboBox).SelectedItem as ComboItem).Value.ToString());

                    Job.Grades.updateGradeGroup(gradeId, groupId, (e.RowObject as classes.Grade).company_id);
                    (e.RowObject as classes.Grade).group = Job.GradeGroups.get(groupId);

                }
            }
        }

    }
}
