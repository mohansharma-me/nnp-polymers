using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NNPPoly
{
    public partial class frmSplash : Form
    {
        private bool initDB = true;
        public frmSplash(bool initDB=true)
        {
            InitializeComponent();
            this.initDB = initDB;
        }

        private void frmSplash_Shown(object sender, EventArgs e)
        {
            lblVersion.Text = Properties.Resources.version;
            Thread thread = new Thread(() => {
                try
                {
                    Action act = () => { };
                    if (initDB)
                    {
                        try
                        {
                            bool forceSelect = false;
                            bool flagCreateTables = false;
                        validateAgain:
                            if (!Job.DB.isDatabaseExists() || forceSelect)
                            {
                                act = () =>
                                {
                                    forms.frmDatabaseSelection selDB = new forms.frmDatabaseSelection(true);
                                    selDB.ShowDialog(this);
                                };
                                Invoke(act);

                                String tmpPath1 = Job.DB.getDatabaseHolder();
                                if (tmpPath1 != null)
                                {
                                    System.Data.SQLite.SQLiteConnection.CreateFile(tmpPath1);
                                    flagCreateTables = true;
                                }
                                else
                                {
                                    goto validateAgain;
                                }
                            }

                            String tmpPath2 = Job.DB.getDatabaseHolder();
                            if (tmpPath2 != null && !System.IO.File.Exists(tmpPath2))
                            {
                                act = () =>
                                {
                                    MessageBox.Show(this, "Sorry, database file is missing, please select another location for database holder.", "Database Holder", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    forceSelect = true;
                                };
                                Invoke(act);
                                goto validateAgain;
                            }


                            Job.DB.initiateDatabaseConnection(flagCreateTables);
                            bool validatedAF = Job.Functions.validateActivationFlag();

                            Action act1 = () =>
                            {

                                try
                                {
                                    if (validatedAF)
                                    {
                                        Application.Exit();
                                    }

                                }
                                catch (Exception ex)
                                {

                                }

                            };
                            Invoke(act1);

                        }
                        catch (Exception ex)
                        {
                            Job.Log("DatabaseLoader#1", ex);
                        }
                    }

                    Thread.Sleep(1500);

                    act = () => { Close(); };
                    Invoke(act);
                }
                catch (Exception ex) {
                    Job.Log("DatabaseLoader", ex);
                }
            });
            thread.Start();
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
           
        }
    }
}
