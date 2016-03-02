using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NNPPoly.forms
{
    public partial class frmDatabaseSelection : Form
    {
        private bool force = false;
        public frmDatabaseSelection(bool force=false)
        {
            InitializeComponent();
            this.force = force;
            if (force)
            {
                Text += " - NNP Polymers";
                TopMost = true;
                ShowInTaskbar = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            Type type = dialog.GetType();
            FieldInfo info = type.GetField("rootFolder", BindingFlags.NonPublic | BindingFlags.Instance);
            info.SetValue(dialog, 18);
            dialog.SelectedPath = txtFolder.Text.Trim();
            showDialogAgain:if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                String folder = dialog.SelectedPath;

                bool flag = false;

                try
                {
                    System.IO.File.WriteAllText(folder + "\\filewrite.test", "test");
                    System.IO.File.Delete(folder + "\\filewrite.test");
                    flag = true;
                }
                catch (Exception) { flag = false; }

                if (flag)
                {
                    txtFolder.Text = dialog.SelectedPath;
                }
                else
                {
                    MessageBox.Show(this, "Sorry, software can't write any data to '" + folder + "', please choose another writable folder.", "Non-Writable/Read-Only Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    goto showDialogAgain;
                }
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String path = txtFolder.Text.Trim();

            if (path.Trim().Length == 0)
            {
                MessageBox.Show(this, "You must select database folder to get working software.", "No Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool flag = false;

            try
            {
                System.IO.File.WriteAllText(path + "\\filewrite.test", "test");
                System.IO.File.Delete(path + "\\filewrite.test");
                flag = true;
            }
            catch (Exception) { flag = false; }

            if (flag)
            {
                if (Job.DB.writeDatabaseHolder(path))
                {
                    DialogResult = DialogResult.OK;
                    force = false;
                    Close();
                }
                else
                {
                    MessageBox.Show(this, "Can't store selected folder into system, please try again after sometime.\nIf same error again occured please re-start your machine and try again.", "Database Folder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }   
            }
            else
            {
                MessageBox.Show(this, "Sorry, software can't write any data to '" + path + "', please choose another writable folder.", "Non-Writable/Read-Only Folder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void frmDatabaseSelection_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (force)
            {
                MessageBox.Show(this, "Sorry, you can't close this dialog until you selects database folder.", "No Folder Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
        }

        private void frmDatabaseSelection_Shown(object sender, EventArgs e)
        {
            String path = Job.DB.getDatabaseHolder(false, true);
            if (path != null)
            {
                txtFolder.Text = path;

                String filePath = path + "\\" + Properties.Resources.uid + "";
                if (!System.IO.File.Exists(filePath))
                {
                    MessageBox.Show(this, "Your selected network folder '" + path + "' didn't have any database, please press 'Save' button to create one or select another folder.", "Database Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }

        }
    }
}
