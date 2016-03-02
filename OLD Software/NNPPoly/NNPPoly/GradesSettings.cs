using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly
{
    public partial class GradesSettings : Form
    {
        private WaitingDialog waitingDialog = null;

        public GradesSettings()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GradesSettings_Load(object sender, EventArgs e)
        {
            loadGrades();
        }

        private void loadGrades()
        {
            try
            {
                foreach (Grade grade in Datastore.dataFile.Grades)
                {
                    gv.Rows.Add(false, grade.GradeName, grade.Amount);
                }
                gv.Focus();
                gv.CurrentCell = gv.Rows[0].Cells[0];
            }
            catch (Exception excep)
            {
                String err = "Unable to perform loadGrades operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSAve_Click(object sender, EventArgs e)
        {
            try
            {
                Datastore.dataFile.Grades.Clear();
                foreach (DataGridViewRow row in gv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        String gradeName = "";
                        double amount = 0;
                        if (row.Cells[1].Value == null)
                        {
                            MessageBox.Show(this, "Please enter grade name", "Grade name", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gv.CurrentCell = row.Cells[1];
                            return;
                        }
                        else if (row.Cells[2].Value == null)
                        {
                            MessageBox.Show(this, "Please enter grade amount", "Grade amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            gv.CurrentCell = row.Cells[2];
                            return;
                        }
                        else
                        {
                            gradeName = row.Cells[1].Value.ToString().Trim();
                            if (!double.TryParse(row.Cells[2].Value.ToString(), out amount))
                            {
                                MessageBox.Show(this, "Please enter valid grade amount", "Invalid Grade Amount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                gv.CurrentCell = row.Cells[2];
                                return;
                            }
                        }
                        bool add = true;
                        for (int index = row.Index + 1; index < gv.Rows.Count; index++)
                        {
                            if (gv.Rows[index].Cells[1].Value != null)
                            {
                                String destName = gv.Rows[index].Cells[1].Value.ToString().Replace(" ", "").ToLower();
                                String sourName = gradeName.Replace(" ", "").ToLower();
                                if (destName.Equals(sourName))
                                {
                                    add = false;
                                }
                            }
                        }
                        if (add)
                        {
                            Grade grade = new Grade();
                            grade.GradeName = row.Cells[1].Value.ToString().Trim();
                            grade.Amount = amount;
                            Datastore.dataFile.Grades.Add(grade);
                        }
                    }
                }
                Datastore.dataFile.Save();
                Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform saveon_gradesSetting operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Import grades excel file...";
            ofd.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(importGrades));
                thread.Name = "ImportGrades";
                thread.Start(ofd.FileName);
            }
        }

        private void importGrades(object fname)
        {
            try
            {
                String filename = fname.ToString();
                Excel.Application app = new Excel.Application();
                Excel.Workbook wb = app.Workbooks.Open(filename);

                foreach (Excel.Worksheet ws in wb.Worksheets)
                {
                    Excel.Range usedRange = ws.UsedRange;
                    for (int i = 0; i < usedRange.Rows.Count; i++)
                    {
                        if (i == 0) continue;
                        int rowid = i + 1;
                        object _gradeName, _gradeAmount;
                        _gradeName = ws.get_Range("A" + rowid).Value2;
                        _gradeAmount = ws.get_Range("B" + rowid).Value2;

                        if (_gradeAmount != null && _gradeName != null)
                        {
                            String gradeName = _gradeName.ToString();
                            String gradeAmount = _gradeAmount.ToString();

                            if (gradeAmount.Length > 0 && gradeAmount.Length > 0)
                            {
                                double amount = 0;
                                if (double.TryParse(gradeAmount, out amount))
                                {
                                    addRow(gradeName, gradeAmount);
                                }
                            }
                        }
                    }
                }

                wb.Close(false);
                app.Quit();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform importGrades operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addRow(String name,String amt)
        {
            try
            {
                Action a = () =>
                {
                    gv.Rows.Add(false, name, amt);
                };
                if (this.InvokeRequired)
                {
                    Invoke(a);
                }
                else
                {
                    a();
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform addrow_gradesSetting operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnSendUpdates_Click(object sender, EventArgs e)
        {
            List<String> selGrades = new List<String>();
            foreach (DataGridViewRow row in gv.Rows)
            {
                if (row.IsNewRow) continue;
                bool isToSend = false;
                try
                {
                    isToSend = (bool)row.Cells[0].Value;
                }
                catch (Exception) { }
                if (isToSend)
                {
                    if (row.Cells[1].Value != null && row.Cells[1].Value.ToString().Trim().Length > 0)
                    {
                        selGrades.Add(row.Cells[1].Value.ToString().Trim().ToLower());
                    }
                }
            }

            Thread thrToSend = new Thread(new ParameterizedThreadStart(threadSendStockMessage));
            thrToSend.Name = "Thread to send stock message";
            thrToSend.Priority = ThreadPriority.Highest;
            waitingDialog = new WaitingDialog();
            waitingDialog.Text = "Finding clients ...";
            thrToSend.Start(selGrades);
            waitingDialog.ShowDialog(this);
        }

        private void threadSendStockMessage(object obj)
        {
            List<Message> messages = new List<Message>();
            List<String> grades = (List<String>)obj;
            foreach (UserAccount user in Datastore.dataFile.UserAccounts)
            {
                if (user.mobileNumber != null && user.mobileNumber.Trim().Length == 0 && user.emailAddress.Trim().Length == 0) continue;
                foreach (String grade in grades)
                {
                    Payment data = user.Payments.Find(x => (x.Grade.Trim().ToLower().Equals(grade.ToLower().Trim())));
                    if (data != null)
                    {
                        String msg = Datastore.dataFile.msg_Stock;
                        msg = msg.Replace("%grade%", grade.ToUpper());
                        msg = Uri.EscapeDataString(msg);
                        String link = Datastore.dataFile.sms_API.Replace("%numbers%", user.mobileNumber).Replace("%msg%", msg);
                        Message.Mail mail = null;
                        if (user.emailAddress.Trim().Length > 0)
                        {
                            String html = Datastore.dataFile.mail_Stock.Replace("%grade%", grade.ToUpper()) + "<br/>" + Datastore.dataFile.mail_From;
                            mail = new Message.Mail(user.emailAddress, "GRADE AVAILABILITY REPORT", html);
                        }
                        messages.Add(new Message(user.ClientName, user.mobileNumber, MessageType.Stock, link, mail, user.ID));
                    }
                }
            }

            if (messages.Count > 0)
            {
                Action action = () => {
                    new MSGSender(messages).ShowDialog(this);
                    waitingDialog.Close();
                    waitingDialog = null;
                };
                Invoke(action);
            }
            else
            {
                Action action = () => {
                    MessageBox.Show(this, "Sorry, no stock messages to send.", "No stock messages", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    waitingDialog.Close();
                    waitingDialog = null;
                };

                Invoke(action);
            }
        }

    }
}
