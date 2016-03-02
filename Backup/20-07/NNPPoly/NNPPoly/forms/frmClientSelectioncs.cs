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
    public partial class frmClientSelection : Form
    {
        public classes.Client selectedClientProfile = null;
        private long companyId = 0;

        public frmClientSelection(long companyId)
        {
            InitializeComponent();
            this.companyId = companyId;
        }

        private void frmClientSelectioncs_Load(object sender, EventArgs e)
        {

        }

        private void loadClients()
        {
            lvClients.ClearObjects();

            Thread thread = new Thread(() =>
            {

                List<classes.Client> clients = new List<classes.Client>();

                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    clients.Add(c);
                }, true, false, companyId);

                Action act = () =>
                {
                    lvClients.SetObjects(clients);
                    frmProcess.publicClose();
                };
                Invoke(act);

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();

            new frmProcess("Loading clients...", "", true, (fc) => { }).ShowDialog(this);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvClients_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvClients.SelectedObjects.Count == 1)
            {
                selectClient(lvClients.SelectedObjects[0] as classes.Client);
            }
        }

        private void selectClient(classes.Client c)
        {
            if (c == null) return;
            selectedClientProfile = c;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void lvClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (lvClients.SelectedObjects.Count == 1)
                {
                    selectClient(lvClients.SelectedObjects[0] as classes.Client);
                }
            }
        }

        private void frmClientSelection_Shown(object sender, EventArgs e)
        {
            loadClients();
        }
    }
}
