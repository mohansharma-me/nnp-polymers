using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmModifySchemes : Form
    {
        public frmModifySchemes()
        {
            InitializeComponent();
        }

        private void frmModifySchemes_Load(object sender, EventArgs e)
        {
            loadSchemes();
        }

        private void loadSchemes()
        {
            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                Action act = () =>
                {
                    lvSchemes.ClearObjects();
                };
                Invoke(act);

                Job.Schemes.allSchemes(true, 0, (classes.Scheme s) =>
                {
                    act = () =>
                    {
                        lvSchemes.AddObject(s);
                    };
                    Invoke(act);
                }, true);

                act = () =>
                {
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = System.Threading.ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading schemes...", "", true, (c) => { }).ShowDialog(this);
        }

        private void lvSchemes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvSchemes.SelectedObjects.Count == 1)
            {
                classes.Scheme scheme = lvSchemes.SelectedObjects[0] as classes.Scheme;
                if (scheme != null)
                {
                    forms.frmNewScheme ns = new frmNewScheme();
                    ns.setEditMode(scheme.id);
                    ns.ShowDialog(this);
                    loadSchemes();
                }
            }
        }

        private void lvSchemes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && lvSchemes.SelectedObjects.Count > 0)
            {
                if (MessageBox.Show(this, "Are you sure to delete all selecteds schemes ?", "Delete ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                foreach (classes.Scheme s in lvSchemes.SelectedObjects)
                {
                    s.Delete();
                }

                loadSchemes();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            KeyEventArgs keyEvent=new KeyEventArgs(Keys.Delete);
            lvSchemes_KeyDown(lvSchemes, keyEvent);
        }
    }
}
