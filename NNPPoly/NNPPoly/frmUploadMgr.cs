using NNPPoly.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace NNPPoly
{
    public partial class frmUploadMgr : Form
    {
        private bool isMaster = false;
        private static List<String> _clients = new List<String>();
        private static List<String> _grades = new List<String>();
        private String defName = "default";

        public frmUploadMgr(bool isMaster)
        {
            InitializeComponent();
            this.isMaster = isMaster;

            if (isMaster)
            {
                uploadNowToolStripMenuItem.Visible = false;
                toolStripWriteChanges.Visible = true;
            }
            else
            {
                uploadNowToolStripMenuItem.Visible = true;
                toolStripWriteChanges.Visible = false;
            }

            olvColumnStatus.ImageGetter = (r) =>
            {
                var row = (classes.upload_mgr.Row)r;
                if (row.type.Trim().Length==0 || row.amount==0 || row.client_name.Trim().Length == 0 || row.grade_code.Trim().Length == 0 || row.invoice.Trim().Length == 0)
                    return imageList1.Images["no"];

                return imageList1.Images["yes"];
            };

            olvColumnDate.AspectToStringConverter = (c) =>
            {
                if (c == null) return "";
                DateTime dt = (DateTime)c;
                if (dt == DateTime.MinValue)
                    return "";
                else
                    return dt.ToShortDateString();
            };

            olvColumnAmount.AspectToStringConverter = (c) =>
            {
                return Job.Functions.AmountToString((double)c);
            };

            olvColumnQty.AspectToStringConverter = (c) =>
            {
                return Job.Functions.MTToString((double)c);
            };

        }

        public void setSavePoint(String savepoint)
        {
            this.defName = savepoint;
        }

        private void frmUploadMgr_Load(object sender, EventArgs e)
        {

            /*if (!isMaster)
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                Type type = dialog.GetType();
                FieldInfo info = type.GetField("rootFolder", BindingFlags.NonPublic | BindingFlags.Instance);
                info.SetValue(dialog, 18);
                dialog.ShowDialog(this);
            }*/

            loadClients();
            loadGrades();

            try
            {
                String tmp = defName + ".session";
                if (System.IO.File.Exists(tmp) && lvData.RestoreState(System.IO.File.ReadAllBytes(tmp), true))
                {
                    //MessageBox.Show(this, "Restored successfully.", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //MessageBox.Show(this, "Sorry, \"" + tmp + "\" isn't exists or currupted, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Job.Log("DefaultRestoreStateUploadMngr", ex);
                //MessageBox.Show(this, "Unable to restore entries, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void loadClients()
        {
            Thread thread = new Thread(() =>
            {
                _clients.Clear();
                Job.Clients.search("", 0, 0, (classes.Client c) =>
                {
                    _clients.Add((c.name + " [" + c.id + "]"));
                }, true, true);
                Action act = () => { frmProcess.publicClose(); };
                Invoke(act);
            });
            thread.Name = "Thread: loadclients_upload_mgr";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Clients", "fetching client list...", true, (c) => { }).ShowDialog(this);
        }

        public void loadGrades()
        {
            Thread thread = new Thread(() =>
            {
                _grades.Clear();
                Job.Grades.getAllGrades(0, true, true, (classes.Grade g) =>
                {
                    if (g!=null && !_grades.Contains(g.code))
                        _grades.Add(g.code);
                });
                Action act = () => { frmProcess.publicClose(); };
                Invoke(act);
            });
            thread.Name = "Thread: loadgrades_upload_mgr";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Grades", "fetching grade list...", true, (c) => { }).ShowDialog(this);
        }

        public bool isUpdated
        {
            get;
            set;
        }

        private void newEntruToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lvData.PossibleFinishCellEditing(true);
            lvData.AddObject(new classes.upload_mgr.Row());
            lvData.EditSubItem((BrightIdeasSoftware.OLVListItem)lvData.Items[lvData.Items.Count - 1], 1);
        }

        private void lvData_CellEditStarting(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            if (e.Column == olvColumnClients)
            {
                TextBox tb = e.Control as TextBox;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                tb.AutoCompleteCustomSource.AddRange(_clients.ToArray());
            }
            else if (e.Column == olvColumnGrade)
            {
                TextBox tb = e.Control as TextBox;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                tb.AutoCompleteCustomSource.AddRange(_grades.ToArray());
            }
            else if (e.Column == olvColumnType)
            {
                TextBox tb = e.Control as TextBox;
                tb.AutoCompleteSource = AutoCompleteSource.CustomSource;
                tb.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                tb.AutoCompleteCustomSource.Add("Sale");
                tb.AutoCompleteCustomSource.Add("Frt");
                tb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
        }

        private void lvData_CellEditFinishing(object sender, BrightIdeasSoftware.CellEditEventArgs e)
        {
            
        }

        private void lvData_KeyDown(object sender, KeyEventArgs e)
        {
            //return;
            if (e.KeyCode == Keys.Delete)
            {
                if (lvData.SelectedIndices.Count > 0)
                {
                    lvData.RemoveObjects(lvData.SelectedObjects);
                }
            }
            else if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.Tab || (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z))
            {
                if (lvData.GetItemCount() > 0)
                {
                    if (lvData.SelectedObjects.Count == 0)
                    {
                        lvData.EditSubItem((BrightIdeasSoftware.OLVListItem)lvData.Items[0], 1);
                    }
                    else
                    {
                        lvData.EditSubItem((BrightIdeasSoftware.OLVListItem)lvData.Items[lvData.SelectedIndices[0]], 1);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(false);
        }


        public void save(bool close,bool defSavePoint=false,bool showNotify=true)
        {
            if (!defSavePoint)
            {
                forms.frmAskUser ask = new forms.frmAskUser("Create Savepoint", "Enter savepoint name\n\nNote: It'll overwrite already exists same savepoint.", defName, forms.frmAskUser.ValueType.String);
                if (ask.ShowDialog(this) == DialogResult.OK)
                {
                    defName = ask.getText();
                }
            }

            try
            {
                System.IO.File.WriteAllBytes(defName + ".session", lvData.SaveState(true));
                if (showNotify)
                    MessageBox.Show(this, "Saved successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                if (close)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                Job.Log("WriteStateUploadMngr", ex);
                MessageBox.Show(this, "Unable to save current entries, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void refreshDataFromDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            forms.frmAskUser ask = new forms.frmAskUser("Restore to Savepoint", "Enter savepoint name", defName, forms.frmAskUser.ValueType.String);
            if (ask.ShowDialog(this) == DialogResult.OK)
            {
                defName = ask.getText();
            }

            try
            {
                String tmp = defName + ".session";
                if (System.IO.File.Exists(tmp) && lvData.RestoreState(System.IO.File.ReadAllBytes(tmp),true))
                {
                    MessageBox.Show(this, "Restored successfully.", "Restored", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(this, "Sorry, \""+defName+"\" isn't exists or currupted, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Job.Log("RestoreStateUploadMngr", ex);
                MessageBox.Show(this, "Unable to restore entries, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true);
        }

        private void importFromExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            importFromExcel();
        }

        public void importFromExcel()
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Title = "Import from excel file...";
            od.Filter = "Excel files|*.xls;*.xlsx|Macro-Excel file|*.xlsm";
            od.CheckFileExists = true;
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                String filename = od.FileName;
                Thread thread = new Thread(() =>
                {
                    #region Excel Code

                    Excel.Application app = new Excel.Application();
                    Excel.Workbook wb = app.Workbooks.Open(filename);

                    foreach (Excel.Worksheet ws in wb.Worksheets)
                    {
                        Excel.Range range = ws.UsedRange;
                        bool isPaymentSheet = range.Columns.Count == 4 || range.Columns.Count == 26;
                        bool isDebitSheet = range.Columns.Count == 8;

                        if (isPaymentSheet)
                        {
                            #region PaymentSheet
                            //range.get_Range("B1").EntireColumn.NumberFormat = "Text";
                            for (int i = 1; i <= range.Rows.Count; i++)
                            {
                                String clientname = null, invoice = null;
                                DateTime? date = null;
                                double? amount = null;

                                clientname = range.get_Range("A" + i).get_Value();
                                invoice = range.get_Range("B" + i).get_Value() != null ? range.get_Range("B" + i).get_Value().ToString() : null;
                                date = range.get_Range("C" + i).get_Value();
                                amount = range.get_Range("D" + i).get_Value();

                                classes.upload_mgr.Row row = new classes.upload_mgr.Row();

                                if (clientname != null && invoice != null && date != null && amount != null)
                                {
                                    row.client_name = clientname;
                                    if (invoice != null)
                                        row.invoice = invoice;
                                    if (date != null)
                                        row.date = date.Value;
                                    if (amount != null)
                                        row.amount = amount.Value;

                                    row.modes = Payment.PaymentMode.Credit;
                                    row.type = "BRct";
                                    Action act1 = () =>
                                    {
                                        lvData.AddObject(row);
                                    };
                                    Invoke(act1);

                                }
                            }
                            
                            #endregion
                        }
                        else if (isDebitSheet)
                        {
                            #region DebitSheet
                            for (int i = 2; i <= range.Rows.Count; i++)
                            {
                                String clientname = null, invoice = null;
                                DateTime? date = null;
                                double? amount = null;
                                String grade = null;
                                double? qty = null;

                                String frtInvoice = null;
                                double? frtAmount = null;

                                clientname = range.get_Range("A" + i).get_Value();
                                invoice = range.get_Range("B" + i).get_Value() != null ? range.get_Range("B" + i).get_Value().ToString() : null;
                                date = range.get_Range("C" + i).get_Value();
                                grade = range.get_Range("D" + i).get_Value();
                                qty = range.get_Range("E" + i).get_Value();
                                amount = range.get_Range("F" + i).get_Value();

                                frtInvoice = range.get_Range("G" + i).get_Value() != null ? range.get_Range("G" + i).get_Value().ToString() : null;
                                frtAmount = range.get_Range("H" + i).get_Value();

                                classes.upload_mgr.Row row = new classes.upload_mgr.Row();

                                if (clientname != null)
                                    row.client_name = clientname;
                                if (invoice != null)
                                    row.invoice = invoice;
                                if (date != null)
                                    row.date = date.Value;
                                if (grade != null)
                                    row.grade_code = grade;
                                if (qty != null)
                                    row.mt = qty.Value;
                                if (amount != null)
                                    row.amount = amount.Value;


                                row.modes = Payment.PaymentMode.Debit;
                                row.type = "Sale";
                                Action act1 = () =>
                                {
                                    lvData.AddObject(row);
                                };
                                if (amount != null && invoice != null && grade != null && qty != null && amount != null && amount.Value > 0)
                                    Invoke(act1);

                                if (frtInvoice != null && frtAmount != null)
                                {
                                    classes.upload_mgr.Row nr = new classes.upload_mgr.Row();
                                    nr.amount = frtAmount.Value;
                                    nr.client_name = clientname;
                                    nr.date = date.Value;
                                    nr.grade_code = "Default";
                                    nr.invoice = frtInvoice;
                                    nr.modes = Payment.PaymentMode.Debit;
                                    nr.mt = 0;
                                    nr.type = "Jrnl";
                                    act1 = () =>
                                    {
                                        lvData.AddObject(nr);
                                    };
                                    Invoke(act1);
                                }
                            }
                            #endregion
                        }
                    }

                    //StreamWriter sw = File.CreateText("import_log.txt");
                    //sw.Write(log);
                    //sw.Close();

                    wb.Close(false);
                    app.Quit();
                    app.Quit();
                    app = null;

                    //setLabel(count + " payments imported.");
                    //setLoadDisplay();
                    Action act = () =>
                    {
                        frmProcess.publicClose();
                        MessageBox.Show(this, "Successfully imported selected excel file.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    };
                    Invoke(act);

                    #endregion
                });
                thread.Name = "Thread: importExcelUploadMgr";
                thread.Priority = ThreadPriority.Highest;
                thread.Start();
                new frmProcess("Import from Excel file...", "Importing debit/credit entries...", true, (c) => { }).ShowDialog(this);
            }
        }

        private void uploadNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                lvData.Enabled = false;
                save(false);
                System.IO.File.WriteAllText("request.mgr", defName);
                MessageBox.Show(this, "Request successfully sent to an administrator.\n\nPlease take note of to do not edit that records until administrator confirm/reject it.", "Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lvData.Enabled = true;
            }
            catch (Exception ex)
            {
                Job.Log("uploadEntriesUploadMgr", ex);
            }
        }

        private void toolStripWriteChanges_Click(object sender, EventArgs e)
        {
            lvData.Enabled = false;
            forms.frmCompany comp = new forms.frmCompany(true);
            comp.ShowDialog(this);
            if (comp.selectedCompany != null)
            {
                writeChanges(comp.selectedCompany);
            }
            lvData.Enabled = true;
        }

        private void writeChanges(Company selComp)
        {
            if (!isMaster) return;

            List<long> debitNotesPaymentIDS = new List<long>();
            List<long> debitAdvisesPaymentIDS = new List<long>();
            List<long> envelopePaymentIDS = new List<long>();

            Thread thread = new Thread(() =>
            {
                try
                {
                    System.Collections.IEnumerable rows = null;
                    Action act = () =>
                    {
                        rows = lvData.Objects;
                        List<classes.upload_mgr.Row> delRows = new List<classes.upload_mgr.Row>();
                        foreach (classes.upload_mgr.Row row in rows)
                        {
                            Action updateProcess = () => { frmProcess.getInstance().pbProcess.Value++; };
                            Action act1 = () => {
                                frmProcess.getInstance().pbProcess.Maximum = lvData.GetItemCount();
                            };
                            Invoke(act1);

                            if (row.type.Trim().Length == 0 || row.amount == 0 || row.client_name.Trim().Length == 0 || row.grade_code.Trim().Length == 0 || row.invoice.Trim().Length == 0)
                            {
                                Invoke(updateProcess);
                                continue;
                            }

                            long clientId = getClientIdFromName(row.client_name);
                            Client client = Job.Clients.findClientByID(clientId, selComp.id);
                            if (clientId == -1 || client==null)
                            {
                                fillClientProfile:
                                bool flag = false;
                                forms.frmNewClient nc = new forms.frmNewClient((classes.Client c) =>
                                {
                                    clientId = c.id;
                                    flag = true;
                                    return true;
                                }, selComp.id);
                                nc.txtName.Text = row.client_name;
                                nc.txtName.ReadOnly = true;
                                nc.btnLink.Visible = true;
                                nc.ShowDialog(this);
                                if (!flag)
                                {
                                    if (MessageBox.Show(this, "Are you sure to not add this client profile ?\nBy answering 'Yes' to this answer will skip this entry.", "Skip Client Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                    {
                                        Invoke(updateProcess);
                                        continue;
                                    }
                                    else
                                    {
                                        goto fillClientProfile;
                                    }
                                }
                            }
                            else
                            {
                                
                            }
                            
                            Company comp = selComp;//Job.Companies.getCompany(clientId,true);
                            long companyId = comp == null ? -1 : comp.id;
                            long newId=0;
                            int tried=0;
                        gradeSearch:
                            Grade grade = Job.Grades.getGradeByCode(row.grade_code, false, selComp.id);
                            if (grade == null)
                            {
                                forms.frmGrade fg = new forms.frmGrade(companyId);
                                fg.setGradeCode(row.grade_code);
                                if (fg.ShowDialog(this) == DialogResult.Cancel)
                                {
                                    Job.Grades.add(0, row.grade_code, Job.Grades.DEFAULT_GRADE_AMOUNT, selComp.id);
                                }

                                tried++;
                                if (tried <= 5)
                                    goto gradeSearch;
                            }
                            if (tried > 0)
                            {
                                // edit grade...
                            }
                            Job.Payments.add(ref newId, clientId, row.date, row.invoice, row.type, "ImportedEntries", row.modes, row.amount, row.mt, grade == null ? 0 : grade.id, 0);
                            delRows.Add(row);

                            if (row.modes == Payment.PaymentMode.Debit)
                            {
                                debitAdvisesPaymentIDS.Add(newId);

                                switch (row.debit_notes)
                                {
                                    case classes.upload_mgr.Row_DebitNotes.CDDN:
                                        if (row.modes == Payment.PaymentMode.Debit) debitNotesPaymentIDS.Add(newId);
                                        break;

                                    case classes.upload_mgr.Row_DebitNotes.ENV:

                                        if (!envelopePaymentIDS.Contains(clientId))
                                        {
                                            envelopePaymentIDS.Add(clientId);
                                        }

                                        break;

                                    case classes.upload_mgr.Row_DebitNotes.CDDN_ENV:
                                        if (row.modes == Payment.PaymentMode.Debit) debitNotesPaymentIDS.Add(newId);

                                        if (!envelopePaymentIDS.Contains(clientId))
                                        {
                                            envelopePaymentIDS.Add(clientId);
                                        }

                                        break;
                                }
                            }
                            Invoke(updateProcess);
                        }

                        Action act2 = () =>
                        {
                            lvData.RemoveObjects(delRows);
                            frmProcess.publicClose();
                            save(false, true, false);
                        };
                        Invoke(act2);
                    };
                    Invoke(act);
                }
                catch (Exception ex)
                {
                    Job.Log("UploadMgr_WriteChanges_Thread", ex);
                }
            });
            thread.Name = "Thread: WriteChangesUpdateMgr";
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
            new frmProcess("Writing changes...", "Updating database...", false, (c) => { }).ShowDialog(this);

            List<long> dnoteIds=new List<long>();
            if (debitNotesPaymentIDS.Count > 0)
            {
                forms.frmDebitNotePrinter dp = new forms.frmDebitNotePrinter(debitNotesPaymentIDS);
                dnoteIds=dp.debitNoteIDs;
                if(dnoteIds==null) dnoteIds=new List<long>();
                dp.ShowDialog(this);
            }

            if (debitAdvisesPaymentIDS.Count > 0)
            {
                forms.frmDebitAdvisePrinter da = new forms.frmDebitAdvisePrinter(debitAdvisesPaymentIDS, dnoteIds);
                da.ShowDialog(this);
            }

            if (envelopePaymentIDS.Count > 0)
            {
                forms.frmEnvelopePrinter ep = new forms.frmEnvelopePrinter(envelopePaymentIDS);
                ep.ShowDialog(this);
            }
        }

        private void lvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            String info="Press \"CTRL+N\" for new entry...";
            lvData.OverlayText.Text = info;
            if (lvData.SelectedObjects.Count > 0)
            {
                classes.upload_mgr.Row row = (classes.upload_mgr.Row)lvData.SelectedObjects[0];
                if (row.client_name.Trim().Length > 0)
                {
                    long id = getClientIdFromName(row.client_name);
                    if (id > -1)
                    {
                        Company comp = Job.Companies.getCompany(id, true);
                        if (comp != null)
                        {
                            lvData.OverlayText.Text = "Company: " + comp.name + "\n" + info;
                        }
                    }
                }
            }
            lvData.RefreshOverlays();
        }

        public long getClientIdFromName(String client_name)
        {
            int start = client_name.LastIndexOf("[");
            if (start > -1)
            {
                int end = client_name.LastIndexOf("]");
                if (end > -1)
                {
                    end = end - start - 1;
                    if (end < client_name.Trim().Length - start)
                    {
                        String longid = client_name.Substring(start + 1, end);
                        long id = 0;
                        if (long.TryParse(longid, out id))
                        {
                            return id;
                        }
                    }
                }
            }
            return -1;
        }

        public String getClientNameFromName(String client_name)
        {
            int start = client_name.LastIndexOf("[");
            if (start > -1)
            {
                return client_name.Substring(0, client_name.Length - start);
            }
            return client_name;
        }

        private void restoreSavepointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            refreshDataFromDatabaseToolStripMenuItem_Click(sender, e);
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            save(true,true);
        }
    }
}
