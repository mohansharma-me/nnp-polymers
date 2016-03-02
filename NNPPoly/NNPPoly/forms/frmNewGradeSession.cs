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
    public partial class frmNewGradeSession : Form
    {
        private bool editMode = false;
        private long sessionId = 0;
        public frmNewGradeSession()
        {
            InitializeComponent();
        }

        private void chkMaxDate_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        public void setEditMode(long sessionId)
        {
            this.sessionId = sessionId;
            editMode = true;
            Text = "Edit Session";
        }

        private void frmNewGradeSession_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Today;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DateTime selFromDate = dtFrom.Value;
            
            Thread thread = new Thread(() =>
            {
                Action act = () => { };

                // check if same starting date is exists or not...
                if (Job.Grades.isMatchingStartingDateExists(selFromDate,sessionId))
                {
                    act = () =>
                    {
                        MessageBox.Show(this, "Sorry, same starting date is already exists, please choose another one.", "Same Starting Date", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    };
                    Invoke(act);
                }
                else
                {
                    bool added = editMode ? Job.Grades.updateSession(sessionId,selFromDate) : Job.Grades.addSession(selFromDate);
                    if (added)
                    {
                        act = () =>
                        {
                            String msg = editMode?"Updated":"Added";
                            MessageBox.Show(this, msg+" successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        };
                    }
                    else
                    {
                        act = () =>
                        {
                            String msg = editMode ? "update" : "add";
                            MessageBox.Show(this, "Unable to "+msg+" new session, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        };
                    }
                    Invoke(act);
                }

                act = () => { frmProcess.publicClose(); };
                Invoke(act);
            });
            thread.Name = "Thread: NewSession_ButtonOK";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("New Session", "Processing...", true, (fc) => { }).ShowDialog(this);
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void frmNewGradeSession_Shown(object sender, EventArgs e)
        {
            if (editMode)
            {
                Thread thread = new Thread(() =>
                {
                    classes.GradeSession gs = Job.Grades.getGradeSession(sessionId);
                    if (gs != null)
                    {
                        Action act = () =>
                        {
                            dtFrom.Value = gs.from_date;
                            frmProcess.publicClose();
                        };
                        Invoke(act);
                    }
                    else
                    {
                        Action act = () =>
                        {
                            MessageBox.Show(this, "Sorry, selected session isn't valid, please try again.", "Sorry", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            frmProcess.publicClose();
                            Close();
                        };
                        Invoke(act);
                    }
                });
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                new frmProcess("Loading date...", "", true, (fc) => { }).ShowDialog(this);
            }
        }
    }
}
