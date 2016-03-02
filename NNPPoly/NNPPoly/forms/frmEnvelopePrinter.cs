using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace NNPPoly.forms
{
    public partial class frmEnvelopePrinter : Form
    {
        private bool printFromWord = false;
        private Decimal copies=1;
        private List<long> clientIds = new List<long>();

        public frmEnvelopePrinter(List<long> clientIds)
        {
            InitializeComponent();
            this.clientIds = clientIds;
        }

        private void frmEnvelopePrinter_Load(object sender, EventArgs e)
        {

        }

        private void frmEnvelopePrinter_Shown(object sender, EventArgs e)
        {
            forms.frmAskUser ask = new frmAskUser("Number of copies of Envelopes ?", "0 means skip", copies + "", frmAskUser.ValueType.Long);
            ask.ShowDialog(this);
            Decimal.TryParse(ask.getText(), out copies);

            if (copies > 0)
                printFromWord = MessageBox.Show(this, "Print all envelopes in blank envelopes ?", "Blank Envelopes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No;
            startProcess();
        }

        private void startProcess()
        {
            Thread thread = new Thread(() =>
            {

                try
                {
                    Action act = () =>
                    {
                        pb.Style = ProgressBarStyle.Blocks;
                        pb.Maximum = clientIds.Count;
                        pb.Value = 0;
                    };
                    Invoke(act);

                    if (copies > 0)
                    {

                        Action updateProcess = () =>
                        {
                            pb.Value++;
                            lblProcess.Text = pb.Value + "/" + pb.Maximum;
                        };


                        String templateName = printFromWord ? "withDesign" : "withoutDesign";
                        String tempFile = System.Windows.Forms.Application.StartupPath + "\\envelope_" + templateName + "_template.docx";
                        try
                        {
                            if (!File.Exists(tempFile))
                            {
                                try
                                {
                                    if (printFromWord)
                                        System.IO.File.WriteAllBytes(tempFile, Properties.Resources.envelope_withDesign);
                                    else
                                        System.IO.File.WriteAllBytes(tempFile, Properties.Resources.envelope_withoutDesign);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(this, "Error in writing document file to saving location." + Environment.NewLine + "Error message:" + Environment.NewLine + ex.Message, "Document write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception excep)
                        {
                            String err = "Unable to initilize template_envelope operation.";
                            Job.Log(err, excep);
                            MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        if (System.IO.File.Exists(tempFile))
                        {
                            try
                            {
                                Word.Application appWord = new Word.Application();

                                Object boolFalse = false;
                                Word.Document doc = (Word.Document)appWord.Documents.Open(tempFile);

                                foreach (long clientId in clientIds)
                                {
                                    classes.Client c = Job.Clients.get(clientId);
                                    if (c != null)
                                    {
                                        classes.Company comp = Job.Companies.getCompany(c.id, true);

                                        if (comp != null)
                                        {

                                            String compTitle = comp.name;
                                            String compAddress = comp.address.Replace("\n", "");
                                            String client_address = c.about;//.Replace(",", "," + '\n');
                                            String client_name = c.name;
                                            String mobiles = c.mobiles.Replace(",", Environment.NewLine);

                                            Job.Functions.FindAndReplace(appWord, "%client_name%", client_name, true, doc);
                                            Job.Functions.FindAndReplace(appWord, "%mobiles%", mobiles, true, doc);
                                            Job.Functions.FindAndReplace(appWord, "%client_address%", client_address, true, doc);
                                            Job.Functions.FindAndReplace(appWord, "%company_address%", compAddress, true, doc);
                                            Job.Functions.FindAndReplace(appWord, "%company_title%", compTitle, true, doc);

                                            doc.PrintOut(boolFalse, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, (int)copies);

                                            Job.Functions.FindAndReplace(appWord, client_address, "%client_address%", true, doc);
                                            Job.Functions.FindAndReplace(appWord, compAddress, "%company_address%", true, doc);
                                            Job.Functions.FindAndReplace(appWord, compTitle, "%company_title%", true, doc);

                                        }
                                    }

                                    Invoke(updateProcess);
                                }

                                //Job.Functions.FindAndReplace(appWord, "%client_address%", dt.ToString("dd").ToUpper());

                                doc.Close(false);
                                appWord.Quit(false);
                            }
                            catch (Exception excep)
                            {
                                String err = "Unable to generate envelope operation.";
                                Job.Log(err, excep);
                                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {

                            }
                        }
                    }

                    act = () =>
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    };
                    Invoke(act);

                }
                catch (Exception ex) {
                    Job.Log("PrintingEvelopesThread", ex);
                }

            });
            thread.Priority = ThreadPriority.Highest;
            thread.Start();
        }
    }
}
