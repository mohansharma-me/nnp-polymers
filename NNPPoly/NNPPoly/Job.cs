using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Data.SQLite;

namespace NNPPoly
{
    public class Job
    {
        #region Variables
        public static frmMain mainForm = null;
        public static String DATABASE_HOLDER_FILE = Properties.Resources.uid + ".hld";
        public static String DATABASE_VERSION = "3";
        public static String DATABASE_FILE = Properties.Resources.uid + ".db";
        public static String FMT_SYSTEM_SHORTDATE = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
        public static String ACTIVATION_FILE = Properties.Resources.uid + ".act";
        public static String DATABASE_STRUCTURE_VERSION = "1";
        #endregion

        #region General Options
        public static void Log(String message, Exception excep)
        {
            try
            {
                Thread thread = new Thread(() =>
                {
                    try
                    {
                        message = Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt <=== ") + message;
                        if (excep != null)
                            message += "[EXCEP: " + excep + " ] ===>";
                        else
                            message += " ===>";
                        File.AppendAllText(Properties.Resources.uid + ".log", message);
                    }
                    catch (Exception) { }
                });
                thread.Start();
            }
            catch (Exception) { }
        }
        #endregion

        #region Functions

        public static class Functions
        {
            public static int monthComparator(int month1, int month2)
            {
                return 0;
            }

            public static int monthToInteger(String month)
            {
                switch (month.ToLower().Trim())
                {
                    case "jan":
                    case "january": return 1;

                    case "feb":
                    case "febuary": return 2;

                    case "mar":
                    case "march": return 3;

                    case "apr":
                    case "april": return 4;

                    case "may": return 5;

                    case "jun":
                    case "june": return 6;

                    case "jul":
                    case "july": return 7;

                    case "aug":
                    case "august": return 8;

                    case "sep":
                    case "september": return 9;

                    case "oct":
                    case "octomber": return 10;

                    case "nov":
                    case "november": return 11;

                    case "dec":
                    case "december": return 12;

                    default:
                        return 0;

                }
                return 0;
            }

            public static String monthToString(int month)
            {
                switch (month)
                {
                    case 1: return "Jan";
                    case 2: return "Feb";
                    case 3: return "Mar";
                    case 4: return "Apr";
                    case 5: return "May";
                    case 6: return "Jun";
                    case 7: return "Jul";
                    case 8: return "Aug";
                    case 9: return "Sep";
                    case 10: return "Oct";
                    case 11: return "Nov";
                    case 12: return "Dec";

                    default:
                        return "n/a";
                }
                return null;
            }

            public static void setActivationFlag()
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey(@"SOFTWARE\\zzzz897564164687313214");
                key.SetValue("zzzz897564164687313214", 0);
                key.Flush();
                key.Close();

                Job.GeneralSettings.activation_flag("1");
            }

            public static void clearActivationFlag()
            {
                try
                {
                    Microsoft.Win32.Registry.LocalMachine.DeleteSubKey(@"SOFTWARE\\zzzz897564164687313214");

                    Job.GeneralSettings.activation_flag("0");
                }
                catch (Exception ex) { }
            }

            public static String getActivationFlag()
            {
                try
                {
                    String data = null;
                    try
                    {
                        System.Net.WebClient wc = new System.Net.WebClient();
                        data = wc.DownloadString("http://update.wcodez.com/activation.php?uid=" + Properties.Resources.uid);

                        if (data != null && data.Trim().Length==0)
                        {
                            clearActivationFlag();
                            return null;
                        }

                    }
                    catch (Exception) { }

                    if (data == null || (data != null && data.Trim().Length == 0))
                    {
                        return Job.GeneralSettings.activation_flag();
                    }
                    else
                    {
                        return data == null ? data : (data.Trim().Length == 0 ? null : data);
                    }
                }
                catch (Exception) { }
                return null;
            }

            public static bool validateActivationFlag()
            {
                String data = Job.Functions.getActivationFlag();
                if (data != null && data.Trim().Length > 0)
                {
                    Job.Functions.setActivationFlag();
                    return true;
                }
                return false;
            }

            public static Exception SendMail(String to, String subject, String html)
            {
                System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                System.Net.Mail.MailMessage msg = null;

                try
                {
                    msg = new System.Net.Mail.MailMessage();
                    msg.From = new System.Net.Mail.MailAddress(GeneralSettings.mail_myMail());
                    msg.To.Add(to);
                    msg.Subject = subject;
                    msg.Body = html;

                    msg.IsBodyHtml = true;
                    sc.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    sc.UseDefaultCredentials = false;
                    sc.Credentials = new System.Net.NetworkCredential(GeneralSettings.mail_username(), GeneralSettings.mail_password());
                    sc.EnableSsl = true;
                    sc.Send(msg);
                    return null;
                }

                catch (Exception ex)
                {
                    return ex;
                }

                finally
                {
                    if (msg != null)
                    {
                        msg.Dispose();
                    }
                }
            }

            public static object DownloadString(String url)
            {
                try
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    String retTN = webClient.DownloadString(url);
                    return retTN;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }

            public static DialogResult ShowMsg(String msg,String title,System.Windows.Forms.MessageBoxButtons btn,System.Windows.Forms.MessageBoxIcon icon)
            {
                DialogResult dr = DialogResult.None;
                try
                {
                    Action act = () =>
                    {
                        dr = System.Windows.Forms.MessageBox.Show(frmMain.ActiveForm, msg, title, btn, icon);
                    };
                    frmMain.ActiveForm.Invoke(act);
                }
                catch (Exception) { }
                return dr;
            }

            public static string NumberToWords(long number)
            {
                if (number == 0)
                    return "Zero";

                if (number < 0)
                    return "- " + NumberToWords(Math.Abs(number));

                string words = "";

                if ((number / 10000000) > 0)
                {
                    words += NumberToWords(number / 10000000) + " Crore ";
                    number %= 10000000;
                }

                if ((number / 1000000) > 0)
                {
                    words += NumberToWords(number / 1000000) + " Lakhs ";
                    number %= 1000000;
                }

                if ((number / 100000) > 0)
                {
                    words += NumberToWords(number / 100000) + " Lakh ";
                    number %= 100000;
                }

                if ((number / 1000) > 0)
                {
                    words += NumberToWords(number / 1000) + " Thousand ";
                    number %= 1000;
                }

                if ((number / 100) > 0)
                {
                    words += NumberToWords(number / 100) + " Hundred ";
                    number %= 100;
                }

                if (number > 0)
                {
                    if (words != "")
                        words += "";

                    var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
                    var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                    if (number < 20)
                        words += unitsMap[number];
                    else
                    {
                        words += tensMap[number / 10];
                        if ((number % 10) > 0)
                            words += "-" + unitsMap[number % 10];
                    }
                }

                return words.Replace("-", " ");
            }

            public static double RoundDouble(double var, int dp = 2)
            {
                return Math.Round(var, dp);
            }

            public static String AmountToString(double amt)
            {
                return amt.ToString("0.00");
            }

            public static String MTToString(double mt)
            {
                return mt.ToString("0.000");
            }

            public static System.Drawing.Color GetContrastedColor(System.Drawing.Color colorToContrast)
            {
                try
                {
                    var yiq = ((colorToContrast.R * 299) + (
                        colorToContrast.G * 587) + (
                        colorToContrast.B * 114)) / 1000;

                    return (yiq >= 128) ? System.Drawing.Color.FromArgb(40, 40, 40) : System.Drawing.Color.WhiteSmoke;

                }
                catch (Exception excep)
                {
                    String err = "Unable to calculate contrasted_color operation.";
                    Job.Log(err, excep);
                    return System.Drawing.Color.Empty;
                }
            }

            public static String FullyRoundAmount(double amt,String trailing=".00")
            {
                return amt.ToString("0") + trailing;
            }

            public static void FindAndReplace(Microsoft.Office.Interop.Word.Application WordApp, object findText, object replaceWithText, bool shapeReplace = false, Microsoft.Office.Interop.Word.Document document = null)
            {
                try
                {
                    object matchCase = true;
                    object matchWholeWord = true;
                    object matchWildCards = false;
                    object matchSoundsLike = false;
                    object nmatchAllWordForms = false;
                    object forward = true;
                    object format = false;
                    object matchKashida = false;
                    object matchDiacritics = false;
                    object matchAlefHamza = false;
                    object matchControl = false;
                    object read_only = false;
                    object visible = true;
                    object replace = 2;
                    object wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;
                    object replaceAll = Microsoft.Office.Interop.Word.WdReplace.wdReplaceAll;
                    WordApp.Selection.Range.HighlightColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                    Microsoft.Office.Interop.Word.Range rng = WordApp.Selection.Range;
                    rng.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBlue;
                    WordApp.Selection.Find.Execute(
                    ref findText,
                    ref matchCase, ref matchWholeWord,
                    ref matchWildCards, ref matchSoundsLike,
                    ref nmatchAllWordForms, ref forward,
                    ref wrap, ref format, ref replaceWithText,
                    ref replaceAll, ref matchKashida,
                    ref matchDiacritics, ref matchAlefHamza,
                    ref matchControl);
                    if (shapeReplace && document != null)
                    {
                        foreach (Microsoft.Office.Interop.Word.Shape shape in document.Shapes)
                        {
                            try
                            {
                                var initialText = shape.TextFrame.TextRange.Text;
                                var resultingText = initialText.Replace(findText.ToString(), replaceWithText.ToString());
                                if (!resultingText.Trim().Equals(initialText.Trim()))
                                    shape.TextFrame.TextRange.Text = resultingText;
                            }
                            catch (Exception) { }
                        }
                    }
                }
                catch (Exception excep)
                {
                    String err = "Unable to perform findandreplace operation.";
                    Job.Log(err, excep);
                    //MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ShowMsg(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        #endregion

        #region Validation

        public static class Validation
        {
            public static bool ValidateString(String text)
            {
                return (text != null && text.Trim().Length != 0);
            }

            public static bool ValidateMobiles(String text)
            {
                String[] nos = text.ToString().Trim().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                long temp;
                bool flag = true;
                foreach (String no in nos)
                {
                    flag = flag && (no.Trim().Length == 9 || no.Trim().Length == 10) && long.TryParse(no.Trim(), out temp);
                    if (!flag) break;
                }
                if (flag && nos.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool ValidateEmails(String text)
            {
                String[] nos = text.ToString().Trim().Split(new String[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                bool flag = true;
                foreach (String no in nos)
                {
                    bool subFlag = false;
                    String[] fArr = no.Split(new String[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                    if (fArr.Length == 2)
                    {
                        fArr = fArr[1].Split(new String[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        if (fArr.Length > 0)
                        {
                            subFlag = true;
                        }
                    }
                    flag = flag && subFlag;
                    if (!flag) break;
                }
                if (flag && nos.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool ValidateDouble(String text)
            {
                double temp;
                if (double.TryParse(text, out temp))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public static bool ValidateLong(String text)
            {
                long temp;
                if (long.TryParse(text, out temp))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Companies

        public static class Companies
        {
            public static classes.Company currentCompany = null;
            public delegate void __addColumn(OLVColumn c);
            public static void generateColumns(__addColumn ac)
            {
                OLVColumn oc = new OLVColumn("", "");
                oc.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                oc.MinimumWidth = 230;
                oc.Searchable = true;
                oc.Sortable = true;
                oc.UseFiltering = true;
                oc.Groupable = false;
                oc.IsEditable = true;
                oc.MaximumWidth = 300;
                oc.Sortable = true;
                oc.WordWrap = true;

                List<String> titles = new List<String>();
                List<String> names = new List<String>();

                titles.AddRange(new String[] { "Company name" , "Address"  });
                names.AddRange(new String[] { "name","address" });


                int index = 0;
                foreach (String title in titles)
                {
                    OLVColumn oc1 = oc.Clone() as OLVColumn;
                    oc1.Text = title;
                    oc1.AspectName = names[index];
                    ac(oc1);
                    index++;
                }

                titles.Clear();
                names.Clear();
                titles = names = null;
                System.GC.Collect();
            }

            public static bool add(String companyName, String address)
            {
                String query = "insert into company(company_name,company_address) values(@name,@address)";
                List<SQLiteParameter> param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@name", companyName));
                param.Add(new SQLiteParameter("@address", address));
                int r = Job.DB.executeQuery(query, param.ToArray());
                long newId = DB.last_inserted_rowid();
                DB.executeQuery("insert into general_settings(gs_company_id) values(@id)", new SQLiteParameter[] { new SQLiteParameter("@id", newId) });
                return r > 0;
            }
            public static bool update(long companyId, String name, String address)
            {
                String query = "update company set company_name=@name, company_address=@address where company_id=@id";
                List<SQLiteParameter> param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@id", companyId));
                param.Add(new SQLiteParameter("@name", name));
                param.Add(new SQLiteParameter("@address", address));
                int r = Job.DB.executeQuery(query, param.ToArray());
                return r > 0;
            }

            public static void search(String searchFor, ref List<classes.Company> list)
            {
                String query = "select * from company";
                if (searchFor.Trim().Length != 0)
                {
                    String like = "company_name LIKE @sf";
                    query = "select * from client where " + like;
                }
                SQLiteDataReader dr = Job.DB.executeReader(query, new SQLiteParameter[] { new SQLiteParameter("@sf", "%" + searchFor + "%") });
                while (dr.Read())
                {
                    NNPPoly.classes.Company c = new classes.Company(long.Parse(dr["company_id"].ToString()));
                    c.SetInitMode = true;
                    c.name = dr["company_name"].ToString();
                    c.address = dr["company_address"].ToString();
                    c.SetInitMode = false;
                    list.Add(c);
                    //c = null;
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                dr.Close();
                dr.Dispose();
            }

            public static classes.Company getCompany(long id, bool fromClientId=false)
            {
                classes.Company comp = null;
                String q = "select * from company where company_id=@id";
                if (fromClientId)
                {
                    q = "select company.* from client, company where client_company_id=company_id and client_id=@id";
                }
                SQLiteDataReader dr = Job.DB.executeReader(q, new SQLiteParameter[] { new SQLiteParameter("@id", id) });
                if (dr != null && dr.Read())
                {
                    comp = new classes.Company(id);
                    comp.SetInitMode = true;
                    comp.name = dr["company_name"].ToString();
                    comp.address = dr["company_address"].ToString();
                    comp.SetInitMode = false;
                }
                return comp;
            }

            public static List<int> getMinimumYear(long companyId)
            {
                List<int> years = new List<int>();
                String query = "select strftime('%Y',payment_date) as minyear from company, client, payment where client_company_id=@compId and payment_client_id=client_id order by payment_date desc";
                SQLiteDataReader dr = Job.DB.executeReader(query, new SQLiteParameter[] { new SQLiteParameter("@compId", companyId) });
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        years.Add(int.Parse(dr["minyear"].ToString()));
                    }
                }
                return years;
            }

            public static bool deleteCompany(long companyId)
            {
                try
                {
                    SQLiteParameter[] param=new SQLiteParameter[] { new SQLiteParameter("@compid", companyId) };

                    SQLiteDataReader drClients = DB.executeReader("select client_id from client where client_company_id=@compid", param);
                    if (drClients != null)
                    {
                        while (drClients.Read())
                        {
                            // delete intrates
                            DB.executeQuery("delete from intrate where ir_client_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", drClients["client_id"]) });

                            // delete payments
                            DB.executeQuery("delete from payment where payment_client_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", drClients["client_id"]) });

                            // delete debits
                        }
                    }

                    // delete clients
                    DB.executeQuery("delete from client where client_company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@compid", companyId) });

                    SQLiteDataReader dr = DB.executeReader("select grade_id from grade where grade_company_id=@compid", param);
                    if (dr != null)
                    {
                        while (dr.Read())
                        {
                            // delete grade_session
                            DB.executeQuery("delete from grade_session where gs_grade_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", dr["grade_id"]) });
                        }
                    }

                    // delete grades and sessions
                    DB.executeQuery("delete from grade where grade_company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@compid", companyId) });

                    // delete debit_priorities
                    DB.executeQuery("delete from debit_priorities dp_company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@compid", companyId) });

                    // delete general settings


                    // delete company
                    DB.executeQuery("delete from company where company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@compid", companyId) });

                    return true;
                }
                catch (Exception ex)
                {
                    Log("deleteCompany: " + companyId, ex);
                }
                return false;
            }
        }

        #endregion

        #region Clients

        public static class Clients
        {
            public static int CURRENT_PAGE = 1;
            public static int ROWS_PER_PAGE = 100;
            public static String CURRENT_SEARCH_FOR = "";
            

            public delegate void __addColumn(OLVColumn oc);
            public delegate void __getClient(NNPPoly.classes.Client c);

            public static void generateColumns(ObjectListView olv, __addColumn ac) {
                olv.Columns.Clear();
                olv.View = View.Details;

                OLVColumn oc = new OLVColumn("", "");
                oc.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                oc.MinimumWidth = 130;
                oc.Searchable = true;
                oc.Sortable = true;
                oc.UseFiltering = true;
                oc.Groupable = false;
                oc.IsEditable = true;
                oc.MaximumWidth = 300;
                oc.Sortable = true;
                oc.WordWrap = true;

                List<String> titles = new List<String>();
                List<String> names = new List<String>();

                titles.AddRange(new String[] {"Client name", "Address", "Open. Balance", "Open. Bal. Type", "Clos. Balance", "Int. Rate 1", "Int. Rate 2", "Cutoff Days", "Less Days", "Report Footer", "Mobiles", "E-mails" });
                names.AddRange(new String[] { "name", "about", "obalance", "obalance_type", "cbalance", "intrate1", "intrate2", "cutoffdays", "lessdays", "footext", "mobiles", "emails" });


                int index = 0;
                foreach (String title in titles)
                {
                    oc = oc.Clone() as OLVColumn;
                    oc.Text = title;
                    oc.AspectName = names[index];
                    if (names[index].Equals("obalance"))
                    {
                        oc.AspectToStringConverter = (c) => {
                            return ((double)c).ToString("0.00");
                        };
                    } else
                    if (names[index].Equals("cbalance"))
                    {
                        oc.IsEditable = false;
                    }
                    //olv.Columns.Add(oc);
                    ac(oc);
                    index++;
                }

                titles.Clear();
                names.Clear();
                titles = names = null;
                System.GC.Collect();
            }

            public static NNPPoly.classes.Client get(long id)
            {
                NNPPoly.classes.Client c=null;
                String query = "select * from client where client_id=@id";
                SQLiteDataReader dr = Job.DB.executeReader(query, new SQLiteParameter[] { new SQLiteParameter("@id", id) });
                if (dr.Read())
                {
                    c = null;//new classes.Client(long.Parse(dr["client_id"].ToString()));
                    /*c.SetInitMode = true;
                    c.name = dr["client_name"].ToString();
                    c.about = dr["client_about"].ToString();
                    c.emails = dr["client_email"].ToString();
                    c.footext = dr["client_footext"].ToString();
                    c.lessdays = long.Parse(dr["client_lessdays"].ToString());
                    c.mobiles = dr["client_mobile"].ToString();
                    c.obalance = double.Parse(dr["client_openingbalance"].ToString());
                    c.obalance_type = dr["client_openingbalance_type"].ToString().ToLower().Equals("Debit") ? classes.Client.OpeningBalanceType.Debit : classes.Client.OpeningBalanceType.Credit;
                    SQLiteDataReader dr1 = Job.DB.executeReader("select * from intrate where ir_client_id=@client", new SQLiteParameter[] { new SQLiteParameter("@client", c.id) });
                    bool firstRate = true;
                    while (dr1.Read())
                    {
                        if (firstRate)
                        {
                            c.intrate1 = double.Parse(dr1["ir_rate"].ToString());
                            firstRate = false;
                        }
                        else
                        {
                            c.intrate2 = double.Parse(dr1["ir_rate"].ToString());
                            c.cutoffdays = long.Parse(dr1["ir_days"].ToString());
                        }
                    }
                     dr1.Close();
                    c.SetInitMode = false;*/
                    initClientObject(ref c, ref dr);
                }
                dr.Close();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                dr.Close();
                dr.Dispose();
                return c;
            }

            public static void initClientObject(ref classes.Client c, ref SQLiteDataReader dr, bool dontRead=true)
            {
                if (!dontRead) dontRead = dr.Read();
                if (dontRead)
                {
                    c = new classes.Client(long.Parse(dr["client_id"].ToString()));
                    c.SetInitMode = true;
                    c.company_id = long.Parse(dr["client_company_id"].ToString());
                    c.name = dr["client_name"].ToString();
                    c.about = dr["client_about"].ToString();
                    c.emails = dr["client_email"].ToString();
                    c.footext = dr["client_footext"].ToString();
                    c.lessdays = long.Parse(dr["client_lessdays"].ToString());
                    c.mobiles = dr["client_mobile"].ToString();
                    c.obalance = double.Parse(dr["client_openingbalance"].ToString());
                    c.obalance_type = dr["client_openingbalance_type"].ToString().ToLower().Equals("debit") ? classes.Client.OpeningBalanceType.Debit : classes.Client.OpeningBalanceType.Credit;
                    SQLiteDataReader dr1 = Job.DB.executeReader("select * from intrate where ir_client_id=@client", new SQLiteParameter[] { new SQLiteParameter("@client", c.id) });
                    bool firstRate = true;
                    while (dr1.Read())
                    {
                        if (firstRate)
                        {
                            c.intrate1 = double.Parse(dr1["ir_rate"].ToString());
                            firstRate = false;
                        }
                        else
                        {
                            c.intrate2 = double.Parse(dr1["ir_rate"].ToString());
                            c.cutoffdays = long.Parse(dr1["ir_days"].ToString());
                        }
                    }
                    c.SetInitMode = false;
                }
            }

            public static double findClosingBalance(long clientId)
            {
                double ret = 0;
                SQLiteDataReader dr = DB.executeReader("select c.payment_client_id, sum(c.final_debit) as final_amount from (select payment.*, 0-payment_amount as final_debit from payment where payment_client_id=@client and payment_mode='Credit' union select payment.*, payment_amount as final_debit from payment where payment_client_id=@client and payment_mode='Debit')c where c.payment_client_id=@client group by c.payment_client_id", new SQLiteParameter[] { new SQLiteParameter("@client", clientId) });
                if (dr != null && dr.Read())
                {
                    double.TryParse(dr["final_amount"].ToString(), out ret);
                }
                return ret;
            }

            public static void search(String searchFor,int pageNo, int rowsPerPage, __getClient gc, bool sortByName=true,bool skipCompanyFilter=false, long compId=0, bool joinClosingBalance=false)
            {
                countClients = 0;
                String commonQ = "select * from client";
                if (!skipCompanyFilter)
                {
                    commonQ += " where client_company_id=@companyId ";
                }
                String query = commonQ;
                if (sortByName)
                {
                    query = query + " order by client_name";
                }
                else
                {
                    query = query + " order by client_id";
                }

                SQLiteDataReader dr = Job.DB.executeReader(query, new SQLiteParameter[] { 
                    //new SQLiteParameter("@companyId", Job.Companies.currentCompany == null ? compId : Job.Companies.currentCompany.id) 
                    new SQLiteParameter("@companyId", compId==0 ? (Job.Companies.currentCompany.id == null ? 0 : Job.Companies.currentCompany.id) : compId ) 
                });
                while (dr.Read())
                {
                    NNPPoly.classes.Client c = null;
                    initClientObject(ref c, ref dr);
                    if (c != null)
                    {
                        if (joinClosingBalance)
                        {
                            c.cbalance = findClosingBalance(c.id);
                            if (c.obalance != 0)
                            {
                                if (c.obalance_type == classes.Client.OpeningBalanceType.Credit)
                                {
                                    c.cbalance -= c.obalance;
                                }
                                else
                                {
                                    c.cbalance += c.obalance;
                                }
                            }
                        }
                        else
                        {
                            c.cbalance = 0;
                        }
                        gc(c);
                        countClients++;
                    }
                    c = null;
                }
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                dr.Close();
                dr.Dispose();
            }

            public static classes.Client findClientByName(String client, long companyId)
            {
                String whereComp = "";
                if (companyId != 0)
                {
                    whereComp = " and client_company_id=@id";
                }
                SQLiteDataReader dr = DB.executeReader("select * from client where lower(client_name)=@client" + whereComp, new SQLiteParameter[] { new SQLiteParameter("@id", companyId), new SQLiteParameter("@client", client.Trim().ToLower()) });
                if (dr != null && dr.Read())
                {
                    classes.Client c = null;
                    initClientObject(ref c, ref dr);
                    return c;
                }
                return null;
            }

            public static classes.Client findClientByID(long client, long companyId)
            {
                String whereComp = "";
                if (companyId != 0)
                {
                    whereComp = " and client_company_id=@id";
                }
                SQLiteDataReader dr = DB.executeReader("select * from client where client_id=@client" + whereComp, new SQLiteParameter[] { new SQLiteParameter("@id", companyId), new SQLiteParameter("@client", client) });
                if (dr != null && dr.Read())
                {
                    classes.Client c = null;
                    initClientObject(ref c, ref dr);
                    return c;
                }
                return null;
            }

            public static long countClients
            {
                get;
                set;
            }

            public static long getCountClients()
            {
                SQLiteDataReader dr = Job.DB.executeReader("select count(*) as total from client where client_company_id=@companyid", new SQLiteParameter[] { new SQLiteParameter("@companyid", Job.Companies.currentCompany.id) });
                if (dr != null && dr.Read())
                {
                    return long.Parse(dr["total"].ToString());
                }
                return -1;
            }

            public static bool add(ref long newClientId, String name, String mobile, String email, String about, String reportFooter, String obalance_type, double obalance, List<double> intRates, List<long> cutOffDays, long lessDays)
            {
                return add(ref newClientId, name, mobile, email, about, reportFooter, obalance_type, obalance, intRates, cutOffDays, lessDays, false);
            }

            public static bool add(ref long newClientId,String name, String mobile, String email, String about, String reportFooter, String obalance_type, double obalance, List<double> intRates, List<long> cutOffDays, long lessDays,bool updateClient,long customCompanyId=0)
            {
                try
                {
                    using (var tr = Job.DB.databaseConnection.BeginTransaction())
                    {
                        List<SQLiteParameter> param = new List<SQLiteParameter>();
                        param.Add(new SQLiteParameter("@companyId", customCompanyId == 0 ? Job.Companies.currentCompany.id : customCompanyId));
                        param.Add(new SQLiteParameter("@name", name));
                        param.Add(new SQLiteParameter("@mobile", mobile));
                        param.Add(new SQLiteParameter("@email", email));
                        param.Add(new SQLiteParameter("@about", about));
                        param.Add(new SQLiteParameter("@reportFooter", reportFooter));
                        param.Add(new SQLiteParameter("@obalance_type", obalance_type.ToString()));
                        param.Add(new SQLiteParameter("@obalance", obalance));
                        param.Add(new SQLiteParameter("@lessDays", lessDays));
                        String query = "insert into client(client_company_id,client_name,client_mobile,client_email,client_about,client_footext,client_openingbalance_type,client_openingbalance,client_lessdays) values(@companyId,@name,@mobile,@email,@about,@reportFooter,@obalance_type,@obalance,@lessDays)";
                        int rd=Job.DB.executeQuery(query, param.ToArray());
                        object tmpObj = Job.DB.executeScalar("select last_insert_rowid()");
                        if (rd>0 && tmpObj != null)
                        {
                            param = new List<SQLiteParameter>();
                            long lastId = (long)tmpObj;
                            newClientId = lastId;
                            int rateCounter = 0;
                            query = "insert into intrate(ir_client_id,ir_rate,ir_days) values(@clientId,@rate,@cd)";
                            rd = 0;
                            foreach (double rate in intRates)
                            {
                                param = new List<SQLiteParameter>();
                                param.Add(new SQLiteParameter("@rate", rate));
                                param.Add(new SQLiteParameter("@cd", cutOffDays[rateCounter++]));
                                param.Add(new SQLiteParameter("@clientId", lastId));
                                rd += Job.DB.executeQuery(query, param.ToArray());
                            }
                            if (rd > 0)
                            {
                                tr.Commit();
                                if (Job.mainForm != null)
                                {
                                    Job.mainForm.refreshClient();
                                }
                                return true;
                            }
                            else
                            {
                                tr.Rollback();
                                return false;
                            }
                        }
                        else
                        {
                            tr.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Job.Log("Error[addClient]", ex);
                    return false;
                }
            }

            public static bool delete(long clientId)
            {
                try
                {
                    SQLiteParameter[] param = new SQLiteParameter[] { new SQLiteParameter("@id", clientId) };

                    // delete intrates
                    DB.executeQuery("delete from intrate where ir_client_id=@id", param);

                    // delete payments
                    DB.executeQuery("delete from payment where payment_client_id=@id", param);

                    // delete clients
                    DB.executeQuery("delete from client where client_id=@id", param);

                    return true;
                }
                catch (Exception ex)
                {
                    Log("deleteClient: " + clientId, ex);
                }
                return false;
            }
        }


        #endregion

        #region Payments

        public static class Payments
        {
            public static int CURRENT_PAGE = 1;
            public static int ROWS_PER_PAGE = 100;
            public static String CURRENT_SEARCH_FOR = "";

            public static double ClosingBalance = 0;

            public delegate void __getPayment(NNPPoly.classes.Payment p);
            public delegate void __getRecord(classes.Record rec);
            public delegate void __addPayment(OLVColumn oc);

            public static void generateColumns(ObjectListView olv, __addPayment ac)
            {
                OLVColumn oc = new OLVColumn("", "");
                oc.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                oc.MinimumWidth = 130;
                oc.Searchable = true;
                oc.Sortable = true;
                //oc.UseFiltering = true;
                oc.Groupable = false;
                oc.IsEditable = true;
                oc.MaximumWidth = 300;
                //oc.Sortable = true;
                oc.WordWrap = true;

                List<String> titles = new List<String>();
                List<String> names = new List<String>();

                titles.AddRange(new String[] { "Date", "Invoice Number", "Type", "Particulars", "T/N Type", "Grade", "M.T.", "Credit","Debit","Closing Balance" });
                names.AddRange(new String[] { "date", "invoice", "type", "particulars", "mode", "grade", "mt", "credit_amount", "debit_amount", "closing_balance" });

                int index = 0;
                foreach (String title in titles)
                {
                    OLVColumn oc1 = oc.Clone() as OLVColumn;
                    oc1.Text = title;
                    //if (true) { } 
                    //else
                        if (index == 0)
                        {
                            oc1.AspectToStringConverter = (o) =>
                            {
                                return ((DateTime)o).ToString(Job.FMT_SYSTEM_SHORTDATE);
                            };
                        }
                        else if (names[index] == "closing_balance")
                        {
                            oc1.IsEditable = false;
                            oc1.AspectToStringConverter = (c) =>
                            {
                                return ((double)c).ToString("0.00");
                            };
                        }
                        else if (names[index] == "mt")
                        {
                            oc1.AspectToStringConverter = (c) =>
                            {
                                double mt = ((double)c);
                                if (mt != 0)
                                    return mt.ToString("0.000");
                                else
                                    return "";
                            };
                        }
                        else if (names[index] == "credit_amount")
                        {
                            oc1.AspectToStringConverter = (c) =>
                            {
                                return ((double)c).ToString("0.00");
                            };
                        }
                        else if (names[index] == "debit_amount")
                        {
                            oc1.AspectToStringConverter = (c) =>
                            {
                                return ((double)c).ToString("0.00");
                            };
                        }
                    oc1.AspectName = names[index];
                    ac(oc1);
                    index++;
                }

                titles.Clear();
                names.Clear();
                titles = names = null;
                System.GC.Collect();
            }

            public static bool add(ref long newEntryId, long clientId, DateTime date, String invoice, String type, String parts, NNPPoly.classes.Payment.PaymentMode mode, double amt, double mt, long gradeId, long isDNote)
            {
                date = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
                String query = "insert into payment(payment_client_id,payment_date,payment_invoice,payment_type,payment_particulars,payment_mode,payment_amount,payment_mt,payment_grade_id,payment_isdnote_payment) values(@cid,@date,@invoice,@type,@parts,@mode,@amount,@mt,@gid,@isdnote)";
                List<SQLiteParameter> param = new List<SQLiteParameter>();
                param.Add(new SQLiteParameter("@cid", clientId));
                param.Add(new SQLiteParameter("@date", date));
                param.Add(new SQLiteParameter("@invoice", invoice));
                param.Add(new SQLiteParameter("@type", type));
                param.Add(new SQLiteParameter("@parts", parts));
                param.Add(new SQLiteParameter("@mode", mode.ToString()));
                param.Add(new SQLiteParameter("@amount", amt));
                param.Add(new SQLiteParameter("@mt", mt));
                param.Add(new SQLiteParameter("@gid", gradeId));
                param.Add(new SQLiteParameter("@isdnote", isDNote));
                int r = Job.DB.executeQuery(query, param.ToArray());
                if (r > 0)
                {
                    newEntryId = (long)Job.DB.executeScalar("select last_insert_rowid()");
                }
                return r > 0;
            }

            public static void initPaymentObject(ref NNPPoly.classes.Payment p, ref SQLiteDataReader dr,bool dontRead=true, bool priorityFlag=false)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    p = new classes.Payment((long)dr["payment_id"]);
                    p.SetInitMode = true;
                    p.amount = (double)dr["payment_amount"];
                    p.client_id = (long)dr["payment_client_id"];
                    p.date = (DateTime)dr["payment_date"];
                    p.date = new DateTime(p.date.Year, p.date.Month, p.date.Day, 12, 0, 0);
                    p.grade = Job.Grades.getGrade((long)dr["payment_grade_id"], false);
                    p.invoice = dr["payment_invoice"].ToString();
                    p.debit_note_id = long.Parse(dr["payment_isdnote_payment"].ToString());
                    //p.isDebitNote = (int)dr["payment_isdnote_payment"] != 0;
                    p.mode = dr["payment_mode"].ToString().ToLower().Trim().Equals("credit") ? NNPPoly.classes.Payment.PaymentMode.Credit : NNPPoly.classes.Payment.PaymentMode.Debit;
                    p.mt = (double)dr["payment_mt"];
                    p.particulars = dr["payment_particulars"].ToString();
                    p.type = dr["payment_type"].ToString();
                    p.highlighted = classes.DataReflector.decodeBool(dr["payment_highlighted"].ToString());

                    bool tmp = p.isPriority;
                    tmp = p.isSpecialType;

                    p.SetInitMode = false;
                }
                else
                {
                    p = null;
                }
            }

            public static double getOpeningBalance(long clientId, DateTime month,double openingBalance=0)
            {
                double amt = 0;
                try
                {
                    //month = month.AddMonths();
                    month = new DateTime(month.Year, month.Month, 1, 12, 0, 0);
                    SQLiteDataReader dr = DB.executeReader("select sum(payment_amount) as balance from (select payment_client_id, payment_date, (case when lower(payment_mode)='debit' then payment_amount else 0-payment_amount end) as payment_amount from payment where payment_client_id=@clientid and payment_date<@date)", new SQLiteParameter[] { new SQLiteParameter("@clientid", clientId), new SQLiteParameter("@date", month) });
                    if (dr != null && dr.Read())
                    {
                        double bal = double.Parse(dr["balance"].ToString());
                        bal = Job.Functions.RoundDouble(bal);
                        bal = bal + openingBalance;
                        return bal;
                    }
                    if (dr != null)
                        dr.Close();
                }
                catch (Exception ex)
                {
                    Job.Log("getOpeningBalance", ex);
                }
                return amt;
            }

            public static NNPPoly.classes.Payment get(long id)
            {
                NNPPoly.classes.Payment p = null;
                String query = "select * from payment where payment_id=@id";
                SQLiteDataReader dr = Job.DB.executeReader(query, new SQLiteParameter[] { new SQLiteParameter("@id", id) });
                if (dr.Read())
                {
                    initPaymentObject(ref p, ref dr);
                }
                dr.Close();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                dr.Close();
                dr.Dispose();
                return p;
            }

            public static classes.Payment getLastPayment(long clientId, classes.Payment.PaymentMode mode, String ofType)
            {
                SQLiteParameter p1 = new SQLiteParameter("@id", clientId);
                SQLiteParameter p2 = new SQLiteParameter("@mode", mode.ToString().ToLower());
                SQLiteParameter p3 = new SQLiteParameter("@type", ofType);
                SQLiteDataReader dr = DB.executeReader("select * from payment where payment_client_id=@id and lower(payment_type)=@type and lower(payment_mode)=@mode order by payment_id desc limit 1", new SQLiteParameter[] { p1, p2, p3 });
                if (dr != null && dr.Read())
                {
                    classes.Payment p = null;
                    initPaymentObject(ref p, ref dr);
                    return p;
                }
                return null;
            }

            public static DateTime getMinimumDateOf(long clientid,classes.Payment.PaymentMode mode)
            {
                String paymentString = mode == classes.Payment.PaymentMode.Credit ? "credit" : "debit";
                SQLiteDataReader dr = Job.DB.executeReader("select * from payment where lower(payment_mode)=@ptype and payment_client_id=@cid order by payment_date asc limit 1", new SQLiteParameter[] { new SQLiteParameter("@cid", clientid), new SQLiteParameter("@ptype", paymentString) });
                if (dr != null && dr.Read())
                {
                    classes.Payment p = null;
                    initPaymentObject(ref p, ref dr);
                    if (p != null)
                        return p.date;
                }
                dr.Close();
                return DateTime.MinValue;
            }

            public static void records(bool allDates, DateTime date, long clientId, String type, __getRecord record)
            {
                String q = "select payment.*,client.client_name, client.client_id from payment, client where payment_client_id=client_id";
                if (!allDates)
                {
                    q += " and payment_date=@date";
                }
                if (clientId > 0)
                {
                    q += " and payment_client_id=@clientid";
                }
                q += " and lower(payment_mode)=@type";
                date = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
                SQLiteDataReader dr = DB.executeReader(q, new SQLiteParameter[] 
                { 
                    new SQLiteParameter("@date",date.ToString("yyyy-MM-dd HH:mm:ss")),
                    new SQLiteParameter("@clientid",clientId),
                    new SQLiteParameter("@type",type.ToLower().Trim())
                });

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.Payment p = null;
                        initPaymentObject(ref p, ref dr);
                        if (p != null)
                        {
                            classes.Record r = new classes.Record();
                            r.client_name = dr["client_name"].ToString();
                            r.payment = p;
                            r.client_id = long.Parse(dr["client_id"].ToString());
                            record(r);
                        }
                    }
                    dr.Close();
                }

            }

            public static void search(long clientid, String searchFor, __getPayment gp, double initalBalance=0)
            {
                //String commonQ = "select a.*,b.* from payment a,(select count(*) as total_payments from payment) b where payment_client_id=@cid and strftime('%Y',payment_date)=@sessionYear ";
                String commonQ = "select a.*,b.* from payment a,(select count(*) as total_payments from payment) b where payment_client_id=@cid ";
                String query = commonQ + "order by payment_date, payment_id asc ";
                if (searchFor.Trim().Length != 0)
                {
                    String like = "and (payment_date LIKE @sf or payment_invoice LIKE @sf or payment_type LIKE @sf or payment_particulars LIKE @sf or payment_mode LIKE @sf or payment_amount=@sf or payment_mt=@sf)";
                    query = commonQ + like + " order by payment_date, payment_id asc ";
                }
                SQLiteDataReader dr = Job.DB.executeReader(query, new SQLiteParameter[] { new SQLiteParameter("@sf", "%" + searchFor + "%"), new SQLiteParameter("@cid", clientid), new SQLiteParameter("@sessionYear", Job.Companies.currentCompany.selectedYear.ToString()) });
                bool firstRun = true;
                double closingBalance = initalBalance;
                frmProcess fp = frmProcess.getInstance();
                Action act;
                System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                sw.Start();
                while (dr.Read())
                {
                    if (firstRun && fp!=null)
                    {
                        act = () => { fp.pbProcess.Maximum = int.Parse(dr["total_payments"].ToString()); };
                        fp.Invoke(act);
                        firstRun = false;
                    }
                    NNPPoly.classes.Payment p = null;
                    initPaymentObject(ref p, ref dr);
                    if (p.mode == classes.Payment.PaymentMode.Credit)
                        closingBalance -= p.credit_amount;
                    else
                        closingBalance += p.debit_amount;
                    p.closing_balance = closingBalance;                    
                    gp(p);
                    p = null;
                    if (fp != null)
                    {
                        act = () => { fp.pbProcess.Value++; };
                        fp.Invoke(act);
                    }
                }
                sw.Stop();
                Console.WriteLine("\nClosing Balance:"+closingBalance+"\nTotal time by DBLoop:" + sw.Elapsed + "\n\n");
                dr.Close();
                dr.Dispose();
            }

            public static bool deletePayment(long paymentId)
            {
                try
                {
                    DB.executeQuery("delete from payment where payment_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", paymentId) });
                    return true;
                }
                catch (Exception ex)
                {
                    Job.Log("deletePayment: " + paymentId, ex);
                }
                return false;
            }

            public static bool deletePaymentsOfClient(long clientId)
            {
                try
                {
                    DB.executeQuery("delete from payment where payment_client_id=@clientid", new SQLiteParameter[] { new SQLiteParameter("@clientid", clientId) });
                    return true;
                }
                catch (Exception ex)
                {
                    Job.Log("deletePaymentsOfClients: " + clientId, ex);
                }
                return false;
            }
        }


        #endregion

        #region Grades

        public static class Grades
        {
            public const long DEFAULT_GRADE_AMOUNT = 100;
            public delegate void __getSession(NNPPoly.classes.GradeSession gs);
            public delegate void __getGrade(NNPPoly.classes.Grade g);

            public delegate void __addGrade(OLVColumn c);

            public static String getGradeClients(long grade_id)
            {
                SQLiteDataReader dr = DB.executeReader("select payment_grade_id, group_concat(distinct payment_client_id) as clients from payment where payment_grade_id=@id group by payment_grade_id", new SQLiteParameter[] { new SQLiteParameter("@id", grade_id) });
                if (dr != null && dr.Read())
                {
                    String ids = dr["clients"].ToString();
                    return ids;
                }
                return null;
            }

            public static void generateColumns(__addGrade ag)
            {
                OLVColumn oc = new OLVColumn("", "");
                oc.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                oc.MinimumWidth = 130;
                oc.Searchable = true;
                oc.Sortable = true;
                oc.UseFiltering = true;
                oc.Groupable = false;
                oc.IsEditable = true;
                oc.MaximumWidth = 300;
                oc.Sortable = true;
                oc.WordWrap = true;

                List<String> titles = new List<String>();
                List<String> names = new List<String>();

                titles.AddRange(new String[] { "","Code","Amount" });
                names.AddRange(new String[] { "","code","amount" });


                int index = 0;
                foreach (String title in titles)
                {
                    OLVColumn oc1 = oc.Clone() as OLVColumn;
                    oc1.Text = title;
                    if (index == 0)
                    {
                        oc1.HeaderCheckBox = true;
                        oc1.MaximumWidth = 50;
                    }/*
                    else if (index == 1)
                    {
                        
                    }
                    else if (index==2)
                    {
                        oc1.AspectGetter = (o) => {
                            NNPPoly.classes.MyList ml = ((NNPPoly.classes.Grade)o).sessions;
                            return ((NNPPoly.classes.GradeSession)ml[ml.Count - 1]).amount;
                        };

                        oc1.AspectPutter = (g,o) => {
                            NNPPoly.classes.MyList ml = ((NNPPoly.classes.Grade)g).sessions;
                            NNPPoly.classes.GradeSession gs = ((NNPPoly.classes.GradeSession)ml[ml.Count - 1]);
                            gs.SetDataReflector = true;
                            gs.amount = double.Parse(o.ToString());
                        };

                        oc1.AspectToStringConverter = (c) => {
                            return ((double)c).ToString("0.00");
                        };
                    }*/

                    oc1.AspectName = names[index];
                    ag(oc1);
                    index++;
                }

                titles.Clear();
                names.Clear();
                titles = names = null;
                System.GC.Collect();
            }

            public static void initGradeObject(ref NNPPoly.classes.Grade g, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    g = new classes.Grade((long)dr["grade_id"]);
                    g.SetInitMode = true;

                    g.code = dr["grade_code"].ToString();
                    g.company_id = long.Parse(dr["grade_company_id"].ToString());
                    g.group = GradeGroups.get(long.Parse(dr["grade_group_id"].ToString()), g.company_id);

                    try
                    {
                        g.grade_amount = long.Parse(dr["ga_amount"].ToString());
                        g.session_id = long.Parse(dr["ga_session_id"].ToString());
                    }
                    catch (Exception)
                    {
                        g.grade_amount = g.session_id = 0;
                    }

                    g.SetInitMode = false;
                }
                else
                {
                    g = null;
                }
            }

            public static List<classes.Grade> getAllGrades(long sessionId, bool skipCompanyFilter = false, bool skipSessionFilter=false, __getGrade gg=null)
            {
                String fromTable = "grade_amount, grade";
                String initialWhere = "ga_grade_id=grade_id";
                String companyFilter = "", sessionFilter="";
                if (!skipCompanyFilter)
                {
                    companyFilter = " and grade_company_id=@compid";
                }

                if (!skipSessionFilter)
                {
                    sessionFilter = " and ga_session_id=@sessionid";
                }
                else
                {
                    fromTable = "grade";
                    initialWhere = "1=1";
                }

                SQLiteParameter[] param = new SQLiteParameter[] {
                    new SQLiteParameter("@compid",Job.Companies.currentCompany==null?0:Job.Companies.currentCompany.id),
                    new SQLiteParameter("@sessionid",sessionId)
                };

                SQLiteDataReader dr = DB.executeReader("select * from " + fromTable + " where " + initialWhere + sessionFilter + companyFilter, param);
                if(dr!=null)
                {
                    List<classes.Grade> grades = new List<classes.Grade>();

                    while (dr.Read())
                    {
                        classes.Grade g = null;
                        initGradeObject(ref g, ref dr);
                        if (g != null)
                        {
                            if (gg != null)
                                gg(g);
                            grades.Add(g);
                        }
                    }

                    return grades;
                }

                return null;
            }

            public static bool add(long groupId, String code, double amount, long compId=0)
            {
                if (Job.DB.executeQuery("insert into grade(grade_code,grade_group_id,grade_company_id) values(@gcode,@groupId,@compid)", new SQLiteParameter[] { new SQLiteParameter("@gcode", code), new SQLiteParameter("@compid", compId == 0 ? Job.Companies.currentCompany.id : compId), new SQLiteParameter("@groupId",groupId) }) > 0)
                {
                    long id = DB.last_inserted_rowid();

                    // check wheather any session is exists or not
                    // if not than add one from begining of time...
                    long no_of_sessions = DB.countRows("grade_session", "gs_company_id");
                    if (no_of_sessions == 0)
                    {
                        addSession(DateTime.MinValue, compId);
                    }


                    if (DB.executeQuery("insert into grade_amount(ga_grade_id,ga_session_id,ga_amount) select @gid, gs_id, @amount from grade_session where gs_company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@gid", id), new SQLiteParameter("@compid", compId>0?compId:Job.Companies.currentCompany.id), new SQLiteParameter("@amount", amount) }) > 0)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }

            public static NNPPoly.classes.Grade getGradeByCode(String code, bool withSession, long compId)
            {
                NNPPoly.classes.Grade g = null;
                if (code.Trim().Length == 0)
                    return getGrade(0, false);

                String whereComp = "";
                if (compId !=0)
                    whereComp = "grade_company_id=@compid and ";

                //SQLiteDataReader dr = Job.DB.executeReader("select * from grade g, (select gs_session_date, gs_id,gs_amount,gs_grade_id from grade_session group by gs_grade_id order by gs_session_date desc) gs where gs.gs_grade_id=g.grade_id and lower(g.grade_code)=@code", new SQLiteParameter[] { new SQLiteParameter("@code", code.Trim().ToLower()) });
                SQLiteDataReader dr = Job.DB.executeReader("select * from grade where " + whereComp + "lower(grade_code)=@code", new SQLiteParameter[] { new SQLiteParameter("@code", code.Trim().ToLower()), new SQLiteParameter("@compid", compId) });
                if (dr!=null && dr.Read())
                {
                    initGradeObject(ref g, ref dr);
                    dr.Close();
                }
                return g;
            }

            public static NNPPoly.classes.Grade getGrade(long gradeId,bool withSession)
            {
                NNPPoly.classes.Grade g = null;
                if (gradeId == 0)
                {
                    g = new classes.Grade(0);
                    g.SetInitMode = true;
                    g.code = "Default";
                    g.grade_amount = 100;
                    //g.session = new classes._GradeSession(0);
                    //g.session.SetInitMode = true;
                    //g.session.amount = 100;
                    //g.session.date = DateTime.Now;
                    //g.session.SetInitMode = g.SetInitMode = false;
                    return g;
                }

                //SQLiteDataReader dr = Job.DB.executeReader("select * from grade g, (select gs_session_date, gs_id,gs_amount,gs_grade_id from grade_session group by gs_grade_id order by gs_session_date desc) gs where gs.gs_grade_id=g.grade_id and g.grade_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", gradeId) });
                SQLiteDataReader dr = Job.DB.executeReader("select * from grade where grade_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", gradeId) });
                if (dr.Read())
                {
                    initGradeObject(ref g, ref dr);
                    /*g = new classes.Grade((long)dr["grade_id"]);
                    g.SetInitMode = true;
                    g.code = dr["grade_code"].ToString();

                    g.session = new classes.GradeSession((long)dr["gs_id"]);
                    g.session.SetInitMode = true;
                    g.session_date = (DateTime)dr["gs_session_date"];
                    g.amount = (double)dr["gs_amount"];
                    g.session.SetInitMode = false;

                    g.sessions = new List<classes.GradeSession>();
                    getSessions(g.id, (NNPPoly.classes.GradeSession gs) =>
                    {
                        g.sessions.Add(gs);
                    });

                    g.SetInitMode = false;*/
                }
                dr.Close();

                return g;
            }

            public static double getGradeAmount(long gradeId, long sessionId)
            {
                SQLiteParameter[] param = new SQLiteParameter[] { 
                    new SQLiteParameter("@session",sessionId),
                    new SQLiteParameter("@grade",gradeId)
                };
                SQLiteDataReader dr = DB.executeReader("select * from grade_amount where ga_session_id=@session and ga_grade_id=@grade", param);
                if (dr != null && dr.Read())
                {
                    return double.Parse(dr["ga_amount"].ToString());
                }
                return 0;
            }

            public static bool extendAllGrades(DateTime extendDate)
            {
                extendDate = new DateTime(extendDate.Year, extendDate.Month, extendDate.Day, 12, 0, 0);
                return DB.executeQuery("insert into grade_session(gs_grade_id,gs_session_date,gs_amount) select gs_grade_id,@date,gs_amount from grade_session", new SQLiteParameter[] { new SQLiteParameter("@date", extendDate) }) > 0;
            }

            public static bool deleteGrade(long gradeId)
            {
                try
                {
                    SQLiteParameter[] param = new SQLiteParameter[] { new SQLiteParameter("@gid", gradeId) };

                    DB.executeQuery("delete from grade_session where gs_grade_id=@gid", param);
                    
                    // delete grades and sessions
                    DB.executeQuery("delete from grade where grade_id=@gid", param);

                    return true;
                }
                catch (Exception ex)
                {
                    Log("deleteGrade: " + gradeId, ex);
                }
                return false;
            }

            /*------------------------------*/

            public static List<NNPPoly.classes.GradeSession> getAllSessions(long companyId=0)
            {
                if (companyId == 0)
                {
                    companyId = Job.Companies.currentCompany.id;
                }

                SQLiteDataReader dr = DB.executeReader("select * from grade_session where gs_company_id=@comp order by gs_from_date", new SQLiteParameter[] { new SQLiteParameter("@comp", companyId) });
                if (dr != null)
                {
                    List<classes.GradeSession> sessions = new List<classes.GradeSession>();

                    while (dr.Read())
                    {
                        classes.GradeSession gs = null;
                        initGradeSessionObject(ref gs, ref dr);
                        if (gs != null)
                        {
                            if (sessions.Count > 0)
                            {
                                sessions[sessions.Count - 1].to_date = gs.from_date.AddDays(-1);
                            }
                            sessions.Add(gs);
                        }
                    }

                    return sessions;
                }

                return null;
            }

            public static void initGradeSessionObject(ref NNPPoly.classes.GradeSession gs, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    gs = new classes.GradeSession((long)dr["gs_id"]);
                    gs.SetInitMode = true;

                    gs.company_id = long.Parse(dr["gs_company_id"].ToString());

                    gs.from_date = (DateTime)dr["gs_from_date"];

                    gs.SetInitMode = false;
                }
                else
                {
                    gs = null;
                }
            }

            public static classes.GradeSession getGradeSession(long sessionId)
            {
                SQLiteDataReader dr = DB.executeReader("select * from grade_session where gs_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", sessionId) });
                if (dr != null)
                {
                    classes.GradeSession gs = null;
                    initGradeSessionObject(ref gs, ref dr, false);
                    return gs;
                }
                return null;
            }

            public static bool addSession(DateTime fromDate, long compId=0)
            {
                fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 12, 0, 0);
                SQLiteParameter[] param = new SQLiteParameter[] 
                { 
                    new SQLiteParameter("@compid", compId>0?compId:Job.Companies.currentCompany.id),
                    new SQLiteParameter("@from",fromDate)
                };
                int rows = DB.executeQuery("insert into grade_session(gs_company_id,gs_from_date) values(@compid,@from)", param);

                if (rows > 0)
                {

                    long newSessionId = DB.last_inserted_rowid();
                    long sessionId = 0;
                    if (fromDate != new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day,12,0,0) && getSessionID(fromDate.AddDays(-1), ref sessionId))
                    {
                        // get amouns from interleaving session
                        DB.executeQuery("insert into grade_amount(ga_session_id,ga_grade_id,ga_amount) select @newSessionId,ga_grade_id,ga_amount from grade_amount where ga_session_id=@previousSessionId", new SQLiteParameter[] { new SQLiteParameter("@newSessionId", newSessionId), new SQLiteParameter("@previousSessionId", sessionId) });
                    }
                    else
                    {
                        // starting date is less than all available dates so just add all grades in that new session with default amount
                        DB.executeQuery("insert into grade_amount(ga_grade_id,ga_session_id,ga_amount) select distinct ga_grade_id,@newSessionId,@defaultAmount from (select grade_amount.* from grade,grade_amount where ga_grade_id=grade_id and grade_company_id=@compid)", new SQLiteParameter[] { new SQLiteParameter("@newSessionId", newSessionId), new SQLiteParameter("@defaultAmount", DEFAULT_GRADE_AMOUNT), new SQLiteParameter("@compid", Job.Companies.currentCompany.id) });
                    }

                    return true;

                }

                // call merge action
                return false;
            }

            public static bool deleteSession(long sessionId)
            {
                SQLiteParameter[] param = new SQLiteParameter[] { 
                    new SQLiteParameter("@sessionId",sessionId)
                };
                int rows = DB.executeQuery("delete from grade_session where gs_id=@sessionId", param);
                if (rows > 0)
                {
                    rows = DB.executeQuery("delete from grade_amount where ga_session_id=@sessionId", param);
                    return rows > 0;
                }
                return false;
            }

            public static bool updateSession(long sessionId, DateTime fromDate)
            {
                SQLiteParameter[] param = new SQLiteParameter[] { 
                    new SQLiteParameter("@sessionId",sessionId),
                    new SQLiteParameter("@date",new DateTime(fromDate.Year,fromDate.Month,fromDate.Day,12,0,0))
                };
                int rows = DB.executeQuery("update grade_session set gs_from_date=@date where gs_id=@sessionId", param);
                return rows > 0;
            }

            public static bool isMatchingStartingDateExists(DateTime date, long sessionId=0)
            {
                SQLiteParameter[] param = new SQLiteParameter[] { new SQLiteParameter("@oldSessionId",sessionId), new SQLiteParameter("@date", new DateTime(date.Year, date.Month, date.Day, 12, 0, 0)) };
                SQLiteDataReader dr = DB.executeReader("select gs_id,gs_from_date from grade_session where gs_from_date=@date and gs_id!=@oldSessionId", param);
                if (dr.Read())
                {
                    dr.Close();
                    return true;
                }
                dr.Close();
                return false;
            }

            public static bool getSessionID(DateTime date, ref long sessionId)
            {
                SQLiteParameter[] param = new SQLiteParameter[] { new SQLiteParameter("@date", new DateTime(date.Year, date.Month, date.Day, 12, 0, 0)), new SQLiteParameter("@compid", Job.Companies.currentCompany.id) };
                SQLiteDataReader dr = DB.executeReader("select gs_id from grade_session where gs_company_id=@compid and gs_from_date<=@date order by gs_from_date desc limit 1", param);
                if (dr.Read())
                {
                    sessionId = long.Parse(dr["gs_id"].ToString());
                    return true;
                }
                sessionId = 0;
                return false;
            }

            public static bool updateGradeAmount(long gradeId, long sessionId, double amount)
            {
                return Job.DB.executeQuery("update grade_amount set ga_amount=@amount where ga_grade_id=@gradeid and ga_session_id=@sessionid", new System.Data.SQLite.SQLiteParameter[] {
                    new SQLiteParameter("@amount",amount),
                    new SQLiteParameter("@sessionid",sessionId),
                    new SQLiteParameter("@gradeid",gradeId)
                }) > 0;
            }

            public static bool updateGradeGroup(long gradeId, long groupId, long compId=0)
            {
                return Job.DB.executeQuery("update grade set grade_group_id=@groupId where grade_id=@gradeId and grade_company_id=@compId", new System.Data.SQLite.SQLiteParameter[] {
                    new SQLiteParameter("@groupId",groupId),
                    new SQLiteParameter("@gradeId",gradeId),
                    new SQLiteParameter("@compId",compId==0?Job.Companies.currentCompany.id:compId)
                }) > 0;
            }
        }


        #endregion

        #region Debit Notes and Advises

        public static class DebitNotes
        {
            public const long NO_OF_ROWS_DEBITNOTE = 8;
            public const long NO_OF_ROWS_DEBITADVISE = 9;

            public delegate void __getNote(classes.DebitNote dp);

            public static classes.Payment getPayment(long dnId)
            {
                SQLiteDataReader dr = DB.executeReader("select * from payment where payment_isdnote_payment=@dnid", new SQLiteParameter[] { new SQLiteParameter("@dnid", dnId) });
                if (dr != null && dr.Read())
                {
                    classes.Payment p = null;
                    Payments.initPaymentObject(ref p, ref dr);
                    return p;
                }
                return null;
            }

            public static void initDebitNoteObject(ref NNPPoly.classes.DebitNote g, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    g = new classes.DebitNote((long)dr["dn_id"]);
                    g.SetInitMode = true;

                    g.client_id = long.Parse(dr["dn_client_id"].ToString());
                    g.date = (DateTime)dr["dn_date"];
                    g.isDNote = classes.DataReflector.decodeBool(dr["dn_isnote"].ToString());
                    g.entries = getEntries(g.id);

                    g.SetInitMode = false;
                }
                else
                {
                    g = null;
                }
            }

            public static List<classes.DebitNote.PaymentEntry> getEntries(long debitNoteId)
            {
                List<classes.DebitNote.PaymentEntry> entries = new List<classes.DebitNote.PaymentEntry>();
                SQLiteDataReader dr = DB.executeReader("select * from debitnote_entries where de_debit_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", debitNoteId) });
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.DebitNote.PaymentEntry pe = new classes.DebitNote.PaymentEntry();
                        pe.myId = long.Parse(dr["de_id"].ToString());
                        pe.paymentId = long.Parse(dr["de_payment_id"].ToString());
                        entries.Add(pe);
                    }
                }
                return entries;
            }

            public static classes.DebitNote get(long id)
            {
                SQLiteDataReader dr = DB.executeReader("select * from debitnote where dn_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", id) });
                if (dr != null && dr.Read())
                {
                    classes.DebitNote dn = null;
                    initDebitNoteObject(ref dn, ref dr);
                    return dn;
                }
                return null;
            }

            public static void find(bool allDates, DateTime date, long clientId, bool isNote, __getNote gn) 
            {
                String q = "select debitnote.* from debitnote,(select * from client where client_company_id=@compid) where dn_client_id=client_id and dn_isnote=@isnote";
                if (!allDates)
                {
                    q += " and dn_date=@date";
                }
                if (clientId > 0)
                {
                    q += " and dn_client_id=@clientid";
                }
                date = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
                SQLiteDataReader dr = DB.executeReader(q, new SQLiteParameter[] 
                { 
                    new SQLiteParameter("@date",date),
                    new SQLiteParameter("@clientid",clientId),
                    new SQLiteParameter("@isnote",classes.DataReflector.encodeBool(isNote)),
                    new SQLiteParameter("@compid",Job.Companies.currentCompany.id)
                });

                if (dr != null)
                {
                    long prevClientId = 0;
                    classes.Client client = null;
                    while (dr.Read())
                    {
                        classes.DebitNote p = null;
                        initDebitNoteObject(ref p, ref dr);
                        if (p != null)
                        {
                            if (prevClientId != p.client_id)
                            {
                                client = Clients.get(p.client_id);
                                prevClientId = p.client_id;
                            }
                            if (client != null)
                            {
                                p.client_name = client.name;
                            }
                            else
                            {
                                p.client_name = "";
                            }
                            double totalAmount = 0;
                            if (p.isDNote)
                            {
                                for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITNOTE && i < p.entries.Count; i++)
                                {
                                    classes.Payment payment = Job.Payments.get(p.entries[i].paymentId);
                                    if (payment != null)
                                    {
                                        double rowAmount = payment.grade.getAmount(payment.date) * client.cutoffdays * payment.mt;
                                        totalAmount += rowAmount;
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITADVISE && i < p.entries.Count; i++)
                                {
                                    classes.Payment payment = Job.Payments.get(p.entries[i].paymentId);
                                    if (payment != null)
                                    {
                                        totalAmount += payment.debit_amount;
                                    }
                                }
                            }
                            p.total_amount = totalAmount;
                            gn(p);
                        }
                    }
                    dr.Close();
                }
            }

            public static bool delete(classes.DebitNote note)
            {
                if (note != null)
                {
                    foreach (classes.DebitNote.PaymentEntry pe in note.entries)
                    {
                        classes.Payment p = Payments.get(pe.paymentId);
                        if (p != null)
                            p.Delete();
                    }
                    if (note.isDNote)
                    {
                        classes.Payment p = getPayment(note.id);
                        if (p != null)
                            p.Delete();
                    }
                    note.Delete();
                    return true;
                }
                return false;
            }

            public static bool add(long clientId, bool isDNote, long no, DateTime date, List<long> entries)
            {
                long tmp = 0;
                return add(ref tmp, clientId, isDNote, no, date, entries);
            }

            public static bool add(ref long returnId,long clientId, bool isDNote, long no, DateTime date, List<long> entries)
            {
                SQLiteParameter[] param = new SQLiteParameter[] { 
                    new SQLiteParameter("@client",clientId),
                    new SQLiteParameter("@isnote",isDNote),
                    new SQLiteParameter("@no",no),
                    new SQLiteParameter("@date",date)
                };
                int rows = DB.executeQuery("insert into debitnote(dn_client_id,dn_isnote,dn_no,dn_date) values(@client,@isnote,@no,@date)", param);
                if (rows > 0)
                {
                    returnId = DB.last_inserted_rowid();
                    SQLiteParameter debitId = new SQLiteParameter("@debit", returnId);
                    bool flag=true;
                    /*entries.Sort(new Comparison<long>((i1, i2) =>
                    {
                        classes.Payment p1 = Payments.get(i1);
                        classes.Payment p2 = Payments.get(i2);
                        if (p1 == null || p2 == null) return 0;
                        return (int)(p1.mt - p2.mt);
                    }));*/
                    foreach (long pid in entries)
                    {
                        SQLiteParameter paymentId = new SQLiteParameter("@payment", pid);
                        DB.executeQuery("insert into debitnote_entries(de_debit_id, de_payment_id) values(@debit,@payment)", new SQLiteParameter[] { debitId, paymentId });
                    }
                    return true;
                }
                return false;
            }

            public delegate void getDebitNoteRowAmount(double rowAmount, classes.Payment payment);

            public static double countDebitNoteRows(classes.DebitNote dnote, getDebitNoteRowAmount ra=null)
            {
                double amount = 0;

                classes.Client client = Clients.get(dnote.client_id);
                if (client != null)
                {
                    for (int i = 0; i < Job.DebitNotes.NO_OF_ROWS_DEBITNOTE && i < dnote.entries.Count; i++)
                    {
                        classes.Payment payment = Job.Payments.get(dnote.entries[i].paymentId);
                        if (payment != null)
                        {
                            double rowAmount = 0;
                            if (dnote.isDNote)
                                rowAmount = payment.grade.getAmount(payment.date) * client.lessdays * payment.mt;
                            else
                                rowAmount = payment.debit_amount;

                            if (ra != null)
                                ra(rowAmount, payment);

                            amount += rowAmount;
                        }
                    }
                }

                return amount;
            }
        }

        #endregion

        #region Report1

        public static class Report1
        {
            public delegate void __addColumn(OLVColumn c);
            public static void generateColumns(__addColumn ac)
            {
                OLVColumn oc = new OLVColumn("", "");
                oc.MinimumWidth = 130;
                oc.Searchable = true;
                oc.Sortable = false;
                oc.UseFiltering = false;
                oc.Groupable = false;
                oc.IsEditable = true;
                oc.MaximumWidth = 300;
                oc.WordWrap = true;

                List<String> titles = new List<String>();
                List<String> names = new List<String>();

                titles.AddRange(new String[] { "New Date", "Last Date", "Debit Date", "Invoice No", "MT", "Debit AMT", "Credit AMT", "Credit taken", "CHQ. Detail", "Credit Date", "Total Days", "Less Days", "Due Days", "INT AMT", "CD" });
                names.AddRange(new String[] { "newdate", "lastdate", "debit_date", "debit_invoice", "debit_mt", "debit_amount", "credit.credit_amount", "taken_amount", "credit.invoice", "credit.date", "totaldays", "lessdays", "duedays", "intamt", "cd" });

                int index = 0;
                foreach (String title in titles)
                {
                    try
                    {
                        OLVColumn oc1 = oc.Clone() as OLVColumn;
                        oc1.Text = title;
                        if (index >= 2 && index <= 5)
                        {
                            if (index == 2)
                                oc1.AspectGetter = (r) =>
                                {
                                    if (r == null) return r;
                                    classes.report1.Row row = (classes.report1.Row)r;
                                    if (row.rowtype == classes.report1.RowType.PartialCreditAdjustment)
                                        return null;
                                    else
                                        return oc1.GetAspectByName(r);
                                };
                            if (index == 3)
                                oc1.AspectGetter = (r) =>
                                {
                                    if (r == null) return r;
                                    classes.report1.Row row = (classes.report1.Row)r;
                                    if (row.rowtype == classes.report1.RowType.PartialCreditAdjustment)
                                        return null;
                                    else
                                        return oc1.GetAspectByName(r);
                                };
                            if (index == 4)
                                oc1.AspectGetter = (r) =>
                                {
                                    if (r == null) return r;
                                    classes.report1.Row row = (classes.report1.Row)r;
                                    if (row.rowtype == classes.report1.RowType.PartialCreditAdjustment)
                                        return null;
                                    else
                                        return oc1.GetAspectByName(r);
                                };
                            if (index == 5)
                                oc1.AspectGetter = (r) =>
                                {
                                    if (r == null) return r;
                                    classes.report1.Row row = (classes.report1.Row)r;
                                    if (row.rowtype == classes.report1.RowType.PartialCreditAdjustment)
                                        return null;
                                    else
                                        return oc1.GetAspectByName(r);
                                };

                        }
                        if (index == 0 || index == 1 || index == 2 || index == 9) // date formats
                        {
                            if (index == 0 || index == 1)
                            {
                                oc1.IsEditable = false;
                            }
                            oc1.AspectToStringConverter = (c) =>
                            {
                                if (c == null)
                                    return "";
                                DateTime dt = ((DateTime)c);
                                if (dt != DateTime.MinValue)
                                    return ((DateTime)c).ToString(Job.FMT_SYSTEM_SHORTDATE);
                                else
                                    return "";
                            };
                        }
                        else if (index == 5 || index == 6 || index == 7 || index==13) // amounts
                        {
                            oc1.AspectToStringConverter = (c) =>
                            {
                                if (c == null)
                                    return "";
                                return (c is double) ? Functions.AmountToString(((double)c)) : c + "";
                            };
                        }
                        else if (index == 4 || index == 7) // mt
                        {
                            oc1.AspectToStringConverter = (c) =>
                            {
                                if (c == null) return "";
                                double mt = ((double)c);
                                if (mt == 0)
                                {
                                    return "";
                                }
                                else
                                {
                                    return Functions.MTToString(mt);
                                }
                            };
                        }

                        oc1.AspectName = names[index];
                        oc1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        ac(oc1);
                    }
                    catch (Exception ex)
                    {
                        Job.Log("ColumnGeneration[Report1]", ex);
                    }
                    index++;
                }

                titles.Clear();
                names.Clear();
                titles = names = null;
                System.GC.Collect();
            }
        }

        #endregion

        #region Debit Priorities & Types

        public static class PrioritiesAndTypes
        {
            public delegate void __addColumn(OLVColumn c);
            public delegate void __getType(classes.DebitPriorities dp);

            public static void generateColumns(__addColumn ac)
            {
                OLVColumn oc = new OLVColumn("", "");
                oc.MinimumWidth = 130;
                oc.Searchable = false;
                oc.Sortable = false;
                //oc.UseFiltering = true;
                oc.Groupable = true;
                oc.IsEditable = true;
                oc.MaximumWidth = 300;
                oc.WordWrap = true;

                List<String> titles = new List<String>();
                List<String> names = new List<String>();

                titles.AddRange(new String[] { "Debit Type", "Special One!" });
                names.AddRange(new String[] { "type", "is_special" });

                int index = 0;
                foreach (String title in titles)
                {
                    try
                    {
                        OLVColumn oc1 = oc.Clone() as OLVColumn;
                        oc1.Text = title;

                        if (index == 1)
                        {
                            oc1.AspectToStringConverter = (c) =>
                            {
                                if (c == null) return "";
                                return ((bool)c) ? "Yes" : "No";
                            };

                        }

                        oc1.AspectName = names[index];
                        oc1.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                        ac(oc1);
                    }
                    catch (Exception ex)
                    {
                        Job.Log("ColumnGeneration[Report1]", ex);
                    }
                    index++;
                }

                titles.Clear();
                names.Clear();
                titles = names = null;
                System.GC.Collect();
            }

            public static bool add(String type, bool special)
            {
                if (Job.DB.executeQuery("insert into debit_priorities(dp_type,dp_company_id,dp_special) values(@type,@compid,@special)", new SQLiteParameter[] { new SQLiteParameter("@type", type), new SQLiteParameter("@compid", Job.Companies.currentCompany.id), new SQLiteParameter("@special", special) }) > 0)
                {
                    return true;
                }
                return false;
            }

            public static void initDPObject(ref NNPPoly.classes.DebitPriorities dp, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    dp = new classes.DebitPriorities((long)dr["dp_id"]);
                    dp.SetInitMode = true;

                    dp.type = dr["dp_type"].ToString();
                    dp.company_id = long.Parse(dr["dp_company_id"].ToString());
                    dp.is_special = classes.DataReflector.decodeBool(dr["dp_special"].ToString());

                    dp.SetInitMode = false;
                }
                else
                {
                    dp = null;
                }
            }

            public static void getAll(__getType gt)
            {
                SQLiteDataReader dr = Job.DB.executeReader("select * from debit_priorities");
                while (dr.Read())
                {
                    NNPPoly.classes.DebitPriorities dp = null;

                    initDPObject(ref dp, ref dr);
                    if (dp != null)
                        gt(dp);
                    dp = null;
                }
                dr.Close();
            }

            public static bool find(String type,bool special)
            {
                try
                {
                    int sp = special ? 1 : 0;
                    SQLiteDataReader dr = Job.DB.executeReader("select count(*) as total from debit_priorities where dp_special=" + sp + " and lower(dp_type)=@type", new SQLiteParameter[] { new SQLiteParameter("@type", type.ToLower().Trim()) });
                    if (dr.Read())
                    {
                        int total = int.Parse(dr["total"].ToString());
                        return total == 1;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return false;
            }
        }

        #endregion

        #region General Settings

        public static class GeneralSettings
        {
            public const String DEFAULT_SMS_API = "http://bulksms.hakimisolution.com/api/sendhttp.php?authkey=4600A09VyiPDe540d4d27&mobiles=%numbers%&message=%msg%&sender=NNPRJT&route=4";
            public const String DEFAULT_COLLECTION_SMS = "PL. ARRANGE PAYMENT TODAY & SMS UTR NO.\nAMT RS. %amt%\nNAME: M/S. NAZARALLY NOORBHAI PATEL\nBANK: HDFC BANK\nACCT NO: 01010440000137\nRTGS CODE: HDFC0000101";
            public const String DEFAULT_DESPATCH_SMS = "YOUR FOLLOWING MATERIAL IS DESPATCHED\nDATE: %date%\nGRADE: %grade%\nQTY: %qty%\nAMT: %amt%\nPLEASE ARRANGE RTGS TODAY";
            public const String DEFAULT_STOCK = "GRADE %grade% IS IN STOCK WITH RIL.\nPLACE YOUR VALUED ORDER SOON SO THAT WE MAY PUNCH YOUR ORDER WITH RIL.\nN.B. IT DOES NOT GAURANTEE ANY SUPPLY CONFIRMATION.";
            public const String DEFAULT_ORDER_REQUEST = "SIR,\nYOU HAVE NOT LIFTED ANY QUANTITY SINCE LAST %days% DAYS.\nRIL CANCELLS SAP NAME REGISTRATION OF NON-ACTIVE CUSTOMER.\nTO REMAIN REGISTRED WITH RIL, PLEASE PLACE ORDER, EVEN OF SMALL QUANTITY, SOON.\nWITH REGARDS.";
            public const String DEFAULT_MOU = "THIS MONTH FOLLOWING MoU QUANTITY IS PENDING TO LIFTED BY YOU:\n%group% %qty% MT";

            public const String DEFAULT_MAIL_EMAIL = "nnp.poly@gmail.com";
            public const String DEFAULT_MAIL_USERNAME = "nnp.poly@gmail.com";
            public const String DEFAULT_MAIL_PASSWORD = "sohilcool";
            public const String DEFAULT_MAIL_FOOTER = "FROM: M/S. NAZARALLY NOORBHAI PATEL, RAJKOT.";

            public const String DEFAULT_MAIL_COLLECTION_SMS = "AN AMOUNT OF RS. %amt% IS OVERDUE, TODAY, AS PER DETAILS GIVEN BELOW<br/><br/>PLEASE ARRANGE PAYMENT THROUGH RTGS TODAY AND GIVE UTR NO. THROUGH MAIL TO GET PROPER & TIMELY CREDIT.";
            public const String DEFAULT_MAIL_STOCK = "<b>%grade% GRADE IS AVAILABLE.</b><br/><br/>PLEASE PLACE YOUR VALUED ORDER, BY RETURN OF MAIL.<br/><br/>N.B.<br/>THIS MESSAGE DOES NOT CONFIRM DELIVERY OF MATERIAL, AS ALLOCATION IS SUBJECT TO CONFIRMATION BY RIL.";
            public const String DEFAULT_MAIL_DESPATCH = "PLEASE ARRANGE PAYMENT THROUGH RTGS TODAY TO AVAIL CASH DISCOUNT.";
            public const String DEFAULT_MAIL_ORDERREQUEST = "Sir,<br/>There is NIL order from your unit, since last %days% days. Please place an order of even small quantity, to revive our business relation.";

            public static void initGS(long compId)
            {
                long r = DB.countRows("general_settings", "gs_company_id", compId);
                if (r == 0)
                {
                    DB.executeQuery("insert into general_settings(gs_company_id) values(@compId)", new SQLiteParameter[] {
                        new SQLiteParameter("@compId",compId)
                    });
                }
            }

            public static String printer(String update = null)
            {
                return getValue("ds_printer", update);
            }

            public static String mid_row_text(String update=null)
            {
                return getValue("ds_dnote_midrow",update);
            }

            public static String dnote_tray(String update = null)
            {
                return getValue("ds_dnote_tray", update);
            }

            public static String dadvise_tray(String update = null)
            {
                return getValue("ds_dadvise_tray", update);
            }

            public static String envlope_tray(String update = null)
            {
                return getValue("ds_envelope_tray", update);
            }

            public static String sms_api(String update = null)
            {
                String val = getValue("ss_apilink", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_SMS_API : val;
            }

            public static String sms_nod_requestorder(String update = null)
            {
                String val = getValue("ss_nod_requestorder", update);
                return val == null || (val != null && val.Trim().Length == 0) ? "1" : val;
            }
            public static String sms_msg_collection(String update = null)
            {
                String val = getValue("ss_msg_collection", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_COLLECTION_SMS : val;
            }

            public static String activation_flag(String update = null)
            {
                String val = getValue("activation_flag", update);
                return val == null || (val != null && val.Trim().Length == 0) ? null : val;
            }

            public static String sms_msg_despatch(String update = null)
            {
                String val = getValue("ss_msg_despatch", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_DESPATCH_SMS : val;
            }

            public static String sms_msg_stock(String update = null)
            {
                String val = getValue("ss_msg_stock", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_STOCK : val;
            }

            public static String sms_msg_orderRequest(String update = null)
            {
                String val = getValue("ss_msg_orderrequest", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_ORDER_REQUEST : val;
            }

            public static String sms_msg_mou(String update = null)
            {
                String val = getValue("ss_msg_mou", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MOU : val;
            }

            

            public static String mail_myMail(String update = null)
            {
                String val = getValue("es_email", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_EMAIL : val;
            }
            public static String mail_username(String update = null)
            {
                String val = getValue("es_username", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_USERNAME : val;
            }

            public static String mail_password(String update = null)
            {
                String val = getValue("es_password", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_PASSWORD : val;
            }
            public static String mail_footer(String update = null)
            {
                String val = getValue("es_footer", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_FOOTER : val;
            }


            public static String mail_msg_collection(String update = null)
            {
                String val = getValue("es_msg_collection", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_COLLECTION_SMS : val;
            }

            public static String mail_msg_despatch(String update = null)
            {
                String val = getValue("es_msg_despatch", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_DESPATCH : val;
            }

            public static String mail_msg_stock(String update = null)
            {
                String val = getValue("es_msg_stock", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_STOCK : val;
            }

            public static String mail_msg_orderRequest(String update = null)
            {
                String val = getValue("es_msg_orderrequest", update);
                return val == null || (val != null && val.Trim().Length == 0) ? DEFAULT_MAIL_ORDERREQUEST : val;
            }

            public static String getValue(String columnName, String saveValue=null)
            {
                if (saveValue != null)
                {
                    if (Job.Companies.currentCompany == null)
                    {
                        int ar = DB.executeQuery("update general_settings set " + columnName + "=@value", new SQLiteParameter[] { new SQLiteParameter("@value", saveValue) });
                    }
                    else
                    {
                        int ar = DB.executeQuery("update general_settings set " + columnName + "=@value where gs_company_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", Job.Companies.currentCompany.id), new SQLiteParameter("@value", saveValue) });
                    }
                }

                if (Job.Companies.currentCompany == null)
                {
                    SQLiteDataReader dr = Job.DB.executeReader("select " + columnName + " from general_settings");
                    if (dr != null && dr.Read())
                    {
                        return dr[columnName].ToString();
                    }
                }
                else
                {
                    SQLiteDataReader dr = Job.DB.executeReader("select " + columnName + " from general_settings where gs_company_id=@id", new SQLiteParameter[] { new SQLiteParameter("@id", Job.Companies.currentCompany.id) });
                    if (dr != null && dr.Read())
                    {
                        return dr[columnName].ToString();
                    }
                }

                return null;
            }

        }

        #endregion

        #region Messages

        public static class Messages
        {

            public static String parseAPI(classes.MessageHolder holder)
            {
                String api = GeneralSettings.sms_api();
                if (holder.mobiles != null)
                {
                    api = api.Replace("%numbers%", holder.mobiles);
                    api = api.Replace("%msg%", Uri.EscapeDataString(holder.sms_content));
                    return api;
                }
                return null;
            }

            public static classes.MessageHolder initMessage(long clientId, classes.MessageHolder.Types type, String smsContent, String mailSubject, String mailContent)
            {
                classes.Client client = Clients.get(clientId);
                if (client != null)
                {
                    classes.MessageHolder holder = new classes.MessageHolder();
                    holder.client_name = client.name;
                    holder.type = type;
                    holder.mobiles = null;
                    holder.emails = null;

                    if (smsContent == null && mailSubject == null && mailContent == null) return null;

                    bool isMobilesAvail = Job.Validation.ValidateMobiles(client.mobiles);
                    bool isEmailsAvail = Job.Validation.ValidateEmails(client.emails);

                    if (!isMobilesAvail && !isEmailsAvail) return null;

                    if (isMobilesAvail && smsContent!=null)
                    {
                        holder.mobiles = client.mobiles;
                        holder.sms_content = smsContent;
                    }

                    if (isEmailsAvail && mailSubject != null && mailContent != null)
                    {
                        holder.emails = client.emails;
                        holder.mail_subject = mailSubject;
                        holder.mail_content = mailContent;
                    }

                    return holder;
                    
                }
                return null;
            }

            public static classes.MessageHolder prepareCustomMessage(long clientId, classes.MessageHolder.Types type, String smsContent, String mailSubject, String mailContent)
            {
                return initMessage(clientId, type, smsContent, mailSubject, mailContent);
            }

            public static classes.MessageHolder prepareStock(long clientid, String grade)
            {
                String smsContent = Job.GeneralSettings.sms_msg_stock();
                if (smsContent != null)
                {
                    smsContent = smsContent.Replace("%grade%", grade);
                }

                String mailSubject = "GRADE AVAILABILITY REPORT";
                String mailContent = Job.GeneralSettings.mail_msg_stock();
                if (mailContent != null)
                {
                    mailContent = mailContent.Replace("%grade%", grade) + "<br/><br/>" + Job.GeneralSettings.mail_footer();
                }

                return initMessage(clientid, classes.MessageHolder.Types.STOCK, smsContent, mailSubject, mailContent);
            }

            public static classes.MessageHolder prepareRequestOrder(long clientid, long days)
            {
                String smsContent = Job.GeneralSettings.sms_msg_orderRequest();
                if (smsContent != null)
                {
                    smsContent = smsContent.Replace("%days%", days.ToString());
                }

                String mailSubject = "REQUEST FOR AN ORDER";
                String mailContent = Job.GeneralSettings.mail_msg_orderRequest();
                if (mailContent != null)
                {
                    mailContent = mailContent.Replace("%days%", days.ToString()) + "<br/><br/>" + Job.GeneralSettings.mail_footer();
                }

                return initMessage(clientid, classes.MessageHolder.Types.REQUESTORDER, smsContent, mailSubject, mailContent);
            }

            public static classes.MessageHolder prepareDespatch(classes.DebitNote dn)
            {
                String smsContent = GeneralSettings.sms_msg_despatch();
                String grade = "", trs = "";
                double qty = 0, amount = 0;

                List<String> gradeList = new List<String>();

                foreach (classes.DebitNote.PaymentEntry pe in dn.entries)
                {
                    classes.Payment p = Payments.get(pe.paymentId);
                    if (p != null && p.mode == classes.Payment.PaymentMode.Debit)
                    {
                        String tmp = p.grade.id == 0 ? null : p.grade.code;
                        if (tmp != null && !tmp.Equals("Default") && !gradeList.Contains(tmp.ToLower()))
                        {
                            gradeList.Add(tmp.ToLower());
                            grade += " " + tmp;
                        }
                        qty += p.mt;
                        amount += p.amount;

                        trs += "<tr><td>" + Functions.AmountToString(p.amount) + "</td><td>" + p.invoice + "</td><td>" + p.date.ToShortDateString() + "</td><td>" + p.grade.code + "</td><td>" + Functions.MTToString(p.mt) + "</td></tr>";
                    }
                }

                if (smsContent != null)
                {
                    smsContent = smsContent.Replace("%date%", dn.date.ToString("dd.MM.yy"));
                    smsContent = smsContent.Replace("%grade%", grade);
                    smsContent = smsContent.Replace("%qty%", Functions.MTToString(qty));
                    smsContent = smsContent.Replace("%amt%", Functions.AmountToString(amount));
                }

                String mailSubject = "MATERIAL DESPATCH ADVISE";
                String mailContent = Job.GeneralSettings.mail_msg_despatch();
                if (mailContent != null)
                {
                    mailContent = mailContent + "<br/><br/>";
                    mailContent += "<center><table cellspacing=2 cellpadding=2 border=1>";
                    mailContent += "<tr><th>INVOICE AMOUNT</th><TH>INVOICE NUMBER</TH><TH>INVOICE DATE</TH><TH>GRADE</TH><TH>QUANTITY</TH></tr>";
                    mailContent += trs;
                    mailContent += "<tr><td>" + Functions.AmountToString(amount) + "</td><td colspan=3></td><td>" + Functions.MTToString(qty) + "</td></tr>";
                    mailContent += "</table></center><br/>" + Job.GeneralSettings.mail_footer();
                }

                return initMessage(dn.client_id, classes.MessageHolder.Types.DESPATCH, smsContent, mailSubject, mailContent);
            }

            public static classes.MessageHolder prepareCollection(classes.collection_list.Collection collection)
            {
                String smsContent = GeneralSettings.sms_msg_collection();
                if (smsContent != null)
                {
                    smsContent = smsContent.Replace("%amt%", Functions.AmountToString(collection.CollectingAmount));
                }

                long clientCutOffDays = collection.client.cutoffdays;
                DateTime todayDate = DateTime.Today;
                String mailSubject = "Due amount - NNP Polymers";
                String mailContent = Job.GeneralSettings.mail_msg_collection();
                mailContent = mailContent.Replace("%amt%", Job.Functions.AmountToString(collection.CollectingAmount));
                if (collection.Rows != null)
                {
                    mailContent = mailContent + "<br/><br/><center><table style='' cellspacing=2 cellpadding=2 border=1><tr><th>DATE</th><th>INV./DN No</th><th>AMT</th></tr>";
                    foreach (classes.report1.Row row in collection.Rows)
                    {
                        if (row != null && row.debit != null)
                        {
                            double dayGap = todayDate.Subtract(row.debit.date).TotalDays;
                            if (row.debit != null && row.credit == null)
                            {
                                if ((dayGap >= clientCutOffDays && row.debit.remainBalance > 0) || row.debit.isPriority)
                                {
                                    String tr = "<tr>";
                                    tr += "<td>" + row.debit.date.ToShortDateString() + "</td><td>" + row.debit.invoice + "</td><td>" + Functions.AmountToString(row.debit.remainBalance) + "</td>";
                                    tr += "</tr>";
                                    mailContent += tr;
                                }
                            }
                            else if (row.rowtype == classes.report1.RowType.RemainingDebitBalanceRow)
                            {
                                String tr = "<tr>";
                                tr += "<td>" + row.debit_date + "</td><td>" + row.debit_invoice + "</td><td>" + row.debit.remainBalance + "</td>";
                                tr += "</tr>";
                                mailContent += tr;
                            }
                        }
                    }
                    mailContent = mailContent + "</table></center>";
                }
                mailContent += "<br/>" + Job.GeneralSettings.mail_footer();

                return initMessage(collection.client.id, classes.MessageHolder.Types.COLLECTION, smsContent, mailSubject, mailContent);
            }

            public static classes.MessageHolder prepareMOU(long clientId, String group, double amount) 
            {
                String smsContent = Job.GeneralSettings.sms_msg_mou();
                if (smsContent != null)
                {
                    smsContent = smsContent.Replace("%group%", group);
                    smsContent = smsContent.Replace("%qty%", Functions.MTToString(amount));
                }

                String mailSubject = "APP/MoU Scheme";
                String mailContent = Job.GeneralSettings.sms_msg_mou();
                if (mailContent != null)
                {
                    mailContent = mailContent.Replace("%group%", group) + mailContent.Replace("%qty%", Functions.MTToString(amount)) + "<br/><br/>" + Job.GeneralSettings.mail_footer();
                }

                return initMessage(clientId, classes.MessageHolder.Types.APP_MOU, smsContent, mailSubject, mailContent);
            }

        }

        #endregion

        #region Holidays

        public static class Holidays
        {
            public static void initHolidayObject(ref NNPPoly.classes.Holiday dp, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    dp = new classes.Holiday((long)dr["holiday_id"]);
                    dp.SetInitMode = true;

                    dp.date = (DateTime)dr["holiday_date"];
                    dp.description = dr["holiday_desc"].ToString();

                    dp.SetInitMode = false;
                }
                else
                {
                    dp = null;
                }
            }

            public static List<classes.Holiday> getAllHolidays(long compId=0)
            {
                List<classes.Holiday> list = new List<classes.Holiday>();

                SQLiteDataReader dr = DB.executeReader("select * from holidays where holiday_company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@compid", compId == 0 ? Job.Companies.currentCompany.id : compId) });
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.Holiday h = null;
                        initHolidayObject(ref h, ref dr);
                        if (h != null) list.Add(h);
                    }
                }

                return list;
            }

            public static bool add(DateTime date, String desc, long compId = 0)
            {
                int nor = DB.executeQuery("insert into holidays(holiday_company_id,holiday_date,holiday_desc) values(@compid,@date,@desc)", new SQLiteParameter[] {
                    new SQLiteParameter("@compid",compId==0?Job.Companies.currentCompany.id:compId),
                    new SQLiteParameter("@date",new DateTime(date.Year,date.Month,date.Day,12,0,0)),
                    new SQLiteParameter("@desc",desc.Trim())
                });

                return nor > 0;
            }

            public static bool validateHolidays(DateTime fromDate, DateTime toDate,long compId=0)
            {
                //select count(*) as total_days from holidays where holiday_company_id=@compid and (@fromDate<=holiday_date and @toDate>=holiday_date)
                fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day, 12, 0, 0);
                toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day, 12, 0, 0);

                bool flag = true;

                for (DateTime dt = fromDate.AddDays(1); dt < toDate; dt = dt.AddDays(1))
                {
                    flag = flag && isHoliday(dt);
                    if (!flag)
                    {
                        return false;
                    }
                }

                return flag;
            }

            public static bool isHoliday(DateTime dateTime, long compId=0)
            {
                if (dateTime.DayOfWeek == DayOfWeek.Sunday) return true;
                dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 12, 0, 0);
                SQLiteDataReader dr = DB.executeReader("select * from holidays where holiday_date=@date and holiday_company_id=@compid", new SQLiteParameter[] { new SQLiteParameter("@compid", compId == 0 ? Job.Companies.currentCompany.id : compId), new SQLiteParameter("@date", dateTime) });
                if (dr != null && dr.Read())
                {
                    return true;
                }
                return false;
            }

        }

        #endregion

        #region Grade Groups

        public static class GradeGroups
        {
            public delegate void fetch(classes.GradeGroup gg);

            public static void initGradeGroupObject(ref NNPPoly.classes.GradeGroup p, ref SQLiteDataReader dr, bool dontRead = true, bool priorityFlag = false)
            {
                if (!dontRead)
                    dontRead = dr.Read();
                if (dontRead)
                {
                    p = new classes.GradeGroup((long)dr["gg_id"]);
                    p.SetInitMode = true;

                    p.company_id = (long)dr["gg_company_id"];
                    p.name = dr["gg_name"].ToString();
                    p.qty = Functions.RoundDouble(double.Parse(dr["gg_qty"].ToString()), 3);
                    p.monthly_percentage = double.Parse(dr["gg_monthly_percentage"].ToString());
                    p.quaterly_percentage = double.Parse(dr["gg_quaterly_percentage"].ToString());
                    p.yearly_percentage = double.Parse(dr["gg_yearly_percentage"].ToString());
                    
                    p.SetInitMode = false;
                }
                else
                {
                    p = null;
                }
            }

            public static classes.GradeGroup get(long id, long compId=0)
            {
                if (id == 0)
                {
                    classes.GradeGroup gg = new classes.GradeGroup(0);
                    gg.name = "Default";
                    gg.qty = 0;
                    gg.monthly_percentage = 0;
                    gg.quaterly_percentage = 0;
                    gg.yearly_percentage = 0;
                    return gg;
                }
                SQLiteDataReader dr = DB.executeReader("select * from grade_group where gg_company_id=@compId and gg_id=@id", new SQLiteParameter[] {
                    new SQLiteParameter("@id",id),
                    new SQLiteParameter("@compId", compId==0?Job.Companies.currentCompany.id:compId)
                });
                if (dr != null && dr.Read())
                {
                    classes.GradeGroup gg = null;
                    initGradeGroupObject(ref gg, ref dr);
                    return gg;
                }
                return null;
            }

            public static List<classes.GradeGroup> getAll(fetch f=null,long compId=0)
            {
                List<classes.GradeGroup> groups = new List<classes.GradeGroup>();

                SQLiteDataReader dr = DB.executeReader("select * from grade_group where gg_company_id=@compId", new SQLiteParameter[] {
                    new SQLiteParameter("@compId", compId==0?Job.Companies.currentCompany.id:compId)
                });

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.GradeGroup gg = null;
                        initGradeGroupObject(ref gg, ref dr);
                        if (gg != null)
                        {
                            groups.Add(gg);
                            if (f != null)
                                f(gg);
                        }
                    }
                }

                return groups;
            }

            public static bool add(String name, double qty, double monthMin, double quaterMin, double yearlyMin, long compId = 0)
            {


                return DB.executeQuery("insert into grade_group(gg_company_id, gg_name, gg_qty, gg_monthly_percentage, gg_quaterly_percentage, gg_yearly_percentage) values(@compId,@name,@qty,@mMin,@qMin,@yMin)", new SQLiteParameter[] {
                    new SQLiteParameter("@compId", compId==0?Job.Companies.currentCompany.id:compId),
                    new SQLiteParameter("@name",name.Trim()),
                    new SQLiteParameter("@qty",qty),
                    new SQLiteParameter("@mMin",monthMin),
                    new SQLiteParameter("@qMin",quaterMin),
                    new SQLiteParameter("@yMin",yearlyMin)
                }) > 0;
            }
        }

        #endregion

        #region Schemes

        public static class Schemes
        {
            public delegate void getScheme(classes.Scheme scheme);
            public delegate void getSchemeData(classes.SchemeData schemeData);

            public static bool isQuarterHavingPartition(long schemeId, int month, int year)
            {

                List<classes.Scheme.Params> pars = getSchemeParameters(schemeId);

                if (pars != null)
                {
                    //pars.Find();
                }

                return false;
            }

            public static classes.Scheme get(long schemeId, bool readParameters=true)
            {
                SQLiteDataReader dr = DB.executeReader("select * from scheme where scheme_id=@schemeId", new SQLiteParameter[] {
                    new SQLiteParameter("@schemeId",schemeId)
                });

                if (dr != null && dr.Read())
                {
                    classes.Scheme scheme = null;
                    initSchemeObject(readParameters, ref scheme, ref dr);
                    return scheme;
                }

                return null;
            }

            public static bool add(long clientId, int year, List<classes.Scheme.Params> parameters)
            {
                if (DB.countRows("scheme", null, 0, "scheme_client_id=@clientId and scheme_year=@year", new SQLiteParameter[] { new SQLiteParameter("@clientId",clientId), new SQLiteParameter("@year",year) }) > 0)
                {
                    return true;
                }

                if (DB.executeQuery("insert into scheme(scheme_client_id,scheme_year) values(@clientId,@year)", new SQLiteParameter[] {
                    new SQLiteParameter("@clientId",clientId),
                    new SQLiteParameter("@year",year)
                }) > 0)
                {
                    long lastId = DB.last_inserted_rowid();

                    return addSchemeParams(lastId, year, parameters);
                }
                else
                {
                    return false;
                }
            }

            public static bool addSchemeParams(long schemeId, int year, List<classes.Scheme.Params> parameters)
            {
                foreach (classes.Scheme.Params p in parameters)
                {
                    int mineYear = p.month_no >= 4 && p.month_no <= 12 ? year : year + 1;
                    DateTime month = new DateTime(mineYear, p.month_no, 1, 12, 0, 0);
                    DB.executeQuery("insert into scheme_params(sp_scheme_id,sp_group_id,sp_group_qty,sp_month) values(@schemeId,@groupId,@qty,@month)", new SQLiteParameter[] {
                            new SQLiteParameter("@schemeId",schemeId),
                            new SQLiteParameter("@groupId",p.group_id),
                            new SQLiteParameter("@qty",p.qty),
                            new SQLiteParameter("@month",month)
                        });
                }
                return true;
            }

            public static bool update(long clientId, long schemeId, int year, List<classes.Scheme.Params> parameters)
            {

                if (DB.countRows("scheme", null, 0, "scheme_id!=@schemeId and scheme_client_id=@clientId and scheme_year=@year", new SQLiteParameter[] { new SQLiteParameter("@clientId", clientId), new SQLiteParameter("@year", year), new SQLiteParameter("@schemeId", schemeId) }) > 0)
                {
                    return false;
                }

                DB.executeQuery("delete from scheme_params where sp_scheme_id=@schemeId", new SQLiteParameter[] {
                    new SQLiteParameter("@schemeId",schemeId)
                });

                addSchemeParams(schemeId, year, parameters);

                return DB.executeQuery("update scheme set scheme_year=@year where scheme_id=@schemeId", new SQLiteParameter[] {
                    new SQLiteParameter("@year",year),
                    new SQLiteParameter("@schemeId",schemeId)
                }) > 0;

            }

            public static void initSchemeObject(bool readParameters,ref NNPPoly.classes.Scheme dp, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();

                if (dontRead)
                {
                    dp = new classes.Scheme((long)dr["scheme_id"]);
                    dp.SetInitMode = true;

                    dp.client_id = long.Parse(dr["scheme_client_id"].ToString());
                    dp.year = int.Parse(dr["scheme_year"].ToString());

                    if (readParameters)
                        dp.parameters = getSchemeParameters(dp.id);

                    dp.SetInitMode = false;
                }
                else
                {
                    dp = null;
                }
            }

            public static void initSchemeDataObject(ref NNPPoly.classes.SchemeData dp, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();

                if (dontRead)
                {
                    dp = new classes.SchemeData((long)dr["sd_id"]);
                    dp.SetInitMode = true;

                    dp.date = (DateTime)dr["sd_date"];
                    dp.client_id = long.Parse(dr["sd_client_id"].ToString());
                    dp.grade_id = long.Parse(dr["sd_grade_id"].ToString());
                    dp.qty = double.Parse(dr["sd_qty"].ToString());

                    dp.SetInitMode = false;
                }
                else
                {
                    dp = null;
                }
            }

            public static void initSchemeParameterObject(ref NNPPoly.classes.Scheme.Params dp, ref SQLiteDataReader dr, bool dontRead = true)
            {
                if (!dontRead)
                    dontRead = dr.Read();

                if (dontRead)
                {
                    dp = new classes.Scheme.Params((long)dr["sp_id"]);
                    dp.SetInitMode = true;

                    dp.group_id = long.Parse(dr["sp_group_id"].ToString());
                    dp.qty = double.Parse(dr["sp_group_qty"].ToString());
                    dp.month = (DateTime)(dr["sp_month"]);
                    classes.GradeGroup gg = Job.GradeGroups.get(dp.group_id);
                    if (gg != null)
                        dp.name = gg.name;

                    try
                    {
                        dp.toMonth = Convert.ToDateTime(dr["sp_tomonth"]);
                    }
                    catch (Exception) {
                        dp.toMonth = null;
                    }

                    dp.SetInitMode = false;
                }
                else
                {
                    dp = null;
                }
            }

            public static List<classes.Scheme> allSchemes(bool readParameters=true, long compId=0, getScheme gs=null, bool getStaticInfo=false)
            {
                List<classes.Scheme> list = new List<classes.Scheme>();

                SQLiteDataReader dr = DB.executeReader("select * from scheme, (select * from client where client_company_id=@compId) where scheme_client_id=client_id", new SQLiteParameter[] { 
                    new SQLiteParameter("@compId",compId==0?Job.Companies.currentCompany.id:compId)
                });

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.Scheme s = null;
                        initSchemeObject(readParameters, ref s, ref dr);
                        if (s != null)
                        {
                            if (getStaticInfo)
                            {
                                classes.Client client = Clients.get(s.client_id);
                                if (client != null)
                                    s.client_name = client.name;
                            }

                            s.scheme = s.year + "-" + (s.year + 1);

                            if (gs != null) gs(s);
                            list.Add(s);
                        }
                    }
                }

                return list;
            }

            public static List<classes.Scheme.Params> getSchemeParameters(long schemeId, DateTime? _validateDate=null)
            {
                List<classes.Scheme.Params> list = new List<classes.Scheme.Params>();

                /*SQLiteDataReader dr = DB.executeReader("select * from scheme_params where sp_scheme_id=@schemeId order by sp_group_id, sp_month", new SQLiteParameter[] {
                    new SQLiteParameter("@schemeId",schemeId)
                });*/

                /*SQLiteDataReader dr = DB.executeReader("select a.*, b.sp_tomonth from scheme_params a left join (select a.*, datetime(b.sp_month,'-1 month') as sp_tomonth from (select * from scheme_params order by sp_group_id,sp_month) a left join (select * from scheme_params order by sp_group_id,sp_month) b on (a.sp_scheme_id=b.sp_scheme_id and a.sp_group_id=b.sp_group_id and a.sp_month<b.sp_month) group by b.sp_id) b on a.sp_id=b.sp_id where a.sp_scheme_id=@schemeId order by a.sp_group_id, a.sp_month", new SQLiteParameter[] {
                    new SQLiteParameter("@schemeId",schemeId)
                });*/

                //select a.*, (case when (b.sp_tomonth is not null and b.sp_tomonth<date("2016-01-01 12:00:00")) then b.sp_tomonth else null end) as sp_tomonth from scheme_params a left join (select a.*, b.sp_month as sp_tomonth from (select * from scheme_params order by sp_group_id,sp_month) a left join (select * from scheme_params order by sp_group_id,sp_month) b on (a.sp_scheme_id=b.sp_scheme_id and a.sp_group_id=b.sp_group_id and a.sp_month<b.sp_month) group by b.sp_id) b on a.sp_id=b.sp_id where a.sp_scheme_id=12 and a.sp_month<date("2016-01-01 12:00:00") order by a.sp_group_id, a.sp_month

                String selectCase = "b.sp_tomonth";
                String whereClause = "";
                if (_validateDate.HasValue)
                {
                    selectCase = "(case when (b.sp_tomonth is not null and b.sp_tomonth<@validateDate) then b.sp_tomonth else null end) as sp_tomonth";
                    whereClause = "and a.sp_month<@validateDate";
                }

                SQLiteDataReader dr = DB.executeReader("select a.*, "+selectCase+" from scheme_params a left join (select a.*, b.sp_month as sp_tomonth from (select * from scheme_params order by sp_group_id,sp_month) a left join (select * from scheme_params order by sp_group_id,sp_month) b on (a.sp_scheme_id=b.sp_scheme_id and a.sp_group_id=b.sp_group_id and a.sp_month<b.sp_month) group by b.sp_id) b on a.sp_id=b.sp_id where a.sp_scheme_id=@schemeId "+whereClause+" order by a.sp_group_id, a.sp_month", new SQLiteParameter[] {
                    new SQLiteParameter("@schemeId",schemeId),
                    new SQLiteParameter("@validateDate",_validateDate)
                });

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.Scheme.Params p = null;
                        initSchemeParameterObject(ref p, ref dr);
                        if (p != null)
                            list.Add(p);
                    }
                }

                return list;
            }

            public static bool addSchemeDataEntry(long clientId, long gradeId, DateTime date, double qty)
            {
                return DB.executeQuery("insert into scheme_data(sd_client_id,sd_grade_id,sd_date,sd_qty) values(@clientId,@gradeId,@date,@qty)", new SQLiteParameter[] {
                    new SQLiteParameter("@clientId",clientId),
                    new SQLiteParameter("@gradeId",gradeId),
                    new SQLiteParameter("@date",new DateTime(date.Year,date.Month,date.Day,12,0,0)),
                    new SQLiteParameter("@qty",qty)
                }) > 0;
            }

            public static List<classes.Scheme> getSchemesFromYear(int year, long compId = 0)
            {
                List<classes.Scheme> schemeList = allSchemes(false,compId, null, true);
                return schemeList.FindAll(x => (x.year == year));
            }

            public static double getMonthReport(int year, int month, long clientId, long groupId)
            {
                double amt = 0;

                DateTime fromMonth = new DateTime(year, month, 1);
                DateTime toMonth = fromMonth.AddMonths(1);

                List<SQLiteParameter> paras = new List<SQLiteParameter>();
                paras.Add(new SQLiteParameter("@fromMonth", fromMonth));
                paras.Add(new SQLiteParameter("@toMonth", toMonth));
                paras.Add(new SQLiteParameter("@clientId", clientId));
                paras.Add(new SQLiteParameter("@groupId", groupId));

                SQLiteDataReader dr = DB.executeReader("select sum(sd_qty) as total_qty, gg_id from scheme_data, (select * from grade_group,grade where grade_group_id=gg_id)gg where sd_grade_id=gg.grade_id and (sd_date>=@fromMonth and sd_date<@toMonth) and sd_client_id=@clientId and gg_id=@groupId group by gg_id", paras.ToArray());

                if (dr != null && dr.Read())
                {
                    double.TryParse(dr["total_qty"].ToString(), out amt);
                }

                return amt;
            }

            public static List<classes.SchemeData> getAllSchemeData(long groupId=0,long clientId=0, DateTime? fromDate=null, DateTime? toDate=null,bool loadBasicInfo=false,getSchemeData sd=null, long compId = 0)
            {
                List<classes.SchemeData> lData = new List<classes.SchemeData>();

                String where = "";
                if (clientId > 0)
                {
                    where += " and client_id=@clientId ";
                }
                if (fromDate.HasValue)
                {
                    where += " and sd_date>=@fromDate";
                }
                if (toDate.HasValue)
                {
                    where += " and sd_date<=@toDate";
                }

                string whereGroup = "";
                if (groupId > 0)
                {
                    whereGroup = ", (select grade_id as selGradeId from grade where grade_group_id=@groupId)";
                    where += " and sd_grade_id=selGradeId";
                }

                where += " and 1=1";
                SQLiteDataReader dr = DB.executeReader("select * from scheme_data, (select * from client where client_company_id=@compId)" + whereGroup + " where sd_client_id=client_id" + where, new SQLiteParameter[] {
                    new SQLiteParameter("@compId", compId==0?Job.Companies.currentCompany.id:compId),
                    new SQLiteParameter("@clientId",clientId),
                    new SQLiteParameter("@fromDate", fromDate),
                    new SQLiteParameter("@toDate", toDate),
                    new SQLiteParameter("@groupId",groupId)
                });

                if (dr != null)
                {
                    while (dr.Read())
                    {
                        classes.SchemeData obj = null;
                        initSchemeDataObject(ref obj, ref dr);
                        if (obj != null)
                        {
                            if (loadBasicInfo)
                            {
                                classes.Client client = Clients.get(obj.client_id);
                                if (client != null)
                                    obj.client_name = client.name;

                                classes.Grade grade = Grades.getGrade(obj.grade_id, true);
                                if (grade != null)
                                    obj.grade_name = grade.code;
                            }
                            lData.Add(obj);
                            if (sd != null)
                            {
                                sd(obj);
                            }
                        }
                    }
                }

                return lData;
            }
            /*
             
             query for calculating the total qty client issued.
             select sum(sd_qty) as total_qty, gg_id from scheme_data, (select * from grade_group,grade where grade_group_id=gg_id)gg where sd_grade_id=gg.grade_id and (sd_date>=date('2015-01-01 12:00:00') and sd_date<date('2016-01-01 12:00:00')) group by gg_id
             
             
             */
        }

        #endregion

        #region Database

        public static class DB
        {
            private static int longTimeout = 10 * 1000;
            private static int waitTime = 1000;
            private static long currentState = 0, currentKey = 0;

            public static SQLiteConnection databaseConnection=null;

            public static bool writeDatabaseHolder(String path)
            {
                try
                {
                    System.IO.File.WriteAllText(DATABASE_HOLDER_FILE, path);
                    return true;
                }
                catch (Exception) { }
                return false;
            }

            public static bool initiateDatabaseConnection(bool flagCreateTables)
            {
                String tmpPath = getDatabaseHolder();
                if (tmpPath != null)
                {
                    DATABASE_FILE = tmpPath;
                }
                else
                {
                    return false;
                }

                if (databaseConnection != null)
                {
                    try
                    {
                        databaseConnection.Close();
                        databaseConnection.Dispose();
                    }
                    catch (Exception ex) { }
                }

                databaseConnection = new SQLiteConnection("Data Source=\\" + DATABASE_FILE + "; Version=" + DATABASE_VERSION + "; New=False; FailIfMissing=True; Synchronous=Full; Compress=True;");
                databaseConnection.Open();
                if (flagCreateTables || true)
                {
                    createTables(DATABASE_STRUCTURE_VERSION);
                }
                return true;
            }

            public static void closeDatabaseConnection()
            {
                if (databaseConnection != null) databaseConnection.Close();
            }

            public static long lockAccess()
            {
                try
                {
                    if (currentState == 0)
                    {
                        currentState = -1;
                        return ++currentKey;
                    }
                    else
                    {
                        try
                        {
                            long tmpLong = -1;
                            Thread th = new Thread(() =>
                            {
                                while (currentState != 0) Thread.Sleep(waitTime);
                                currentState = -1;
                                tmpLong=++currentKey;
                            });
                            th.Start();
                            th.Join(longTimeout);
                            return tmpLong;
                        }
                        catch (Exception) { return -1; }
                    }
                }
                catch (Exception ex)
                {
                    Job.Log("Error[lockAccess]", ex);
                    return -1;
                }
                return -1;
            }

            public static void releaseAccess(long myKey)
            {
                try
                {
                    if (currentState != 0 && currentKey == myKey)
                    {
                        currentState = 0;
                    }
                }
                catch (Exception ex)
                {
                    Job.Log("Error[releaseAccess]", ex);
                }
            }

            private static void createTables(String structVersion)
            {
                if (structVersion == "1")
                {
                    executeQuery("create table if not exists company (company_id integer primary key autoincrement, company_name text, company_address text, company_logo blob)");
                    executeQuery("create table if not exists client  (client_id integer primary key autoincrement, client_company_id integer, client_name text, client_about text, client_openingbalance double, client_openingbalance_type text,  client_lessdays long, client_mobile text, client_email text, client_footext text);");
                    executeQuery("create table if not exists intrate (ir_id integer primary key autoincrement, ir_client_id int references client(client_id) on update cascade, ir_rate double, ir_days long);");
                    executeQuery("create table if not exists payment (payment_id integer primary key autoincrement, payment_client_id integer, payment_date date, payment_invoice text, payment_type text, payment_particulars text, payment_mode text, payment_amount double, payment_mt double, payment_grade_id integer, payment_isdnote_payment integer, payment_highlighted int)");
                    executeQuery("create table if not exists grade(grade_id integer primary key autoincrement, grade_company_id integer, grade_code text)");
                    executeQuery("create table if not exists grade_session(gs_id integer primary key autoincrement, gs_company_id integer, gs_from_date date, gs_to_date date)");
                    executeQuery("create table if not exists grade_amount(ga_id integer primary key autoincrement, ga_session_id integer, ga_grade_id integer, ga_amount double)");
                    executeQuery("create table if not exists debit_priorities(dp_id integer primary key autoincrement, dp_company_id integer, dp_type text, dp_special int)");
                    executeQuery("create table if not exists debitnote(dn_id integer primary key autoincrement, dn_client_id integer, dn_isnote integer, dn_no long, dn_date date)");
                    executeQuery("create table if not exists debitnote_entries(de_id integer primary key autoincrement, de_debit_id integer, de_payment_id integer)");
                    
                    executeQuery("create table if not exists general_settings(gs_company_id integer,ds_printer text, ds_dnote_midrow text, ds_currentdnote_no long, ds_dnote_tray text, ds_dadvise_tray text, ds_envelope_tray text, ss_apilink text, ss_nod_requestorder text, es_email text, es_username text, es_password text, es_footer text)");
                    
                    executeQuery("alter table general_settings add column ss_msg_collection text");
                    executeQuery("alter table general_settings add column ss_msg_stock text");
                    executeQuery("alter table general_settings add column ss_msg_orderrequest text");
                    executeQuery("alter table general_settings add column ss_msg_despatch text");

                    executeQuery("alter table general_settings add column es_msg_collection text");
                    executeQuery("alter table general_settings add column es_msg_despatch text");
                    executeQuery("alter table general_settings add column es_msg_stock text");
                    executeQuery("alter table general_settings add column es_msg_orderrequest text");

                    executeQuery("alter table general_settings add column activation_flag text");

                    executeQuery("create table if not exists holidays(holiday_id integer primary key autoincrement, holiday_company_id integer, holiday_desc text, holiday_date date)");

                    executeQuery("create table if not exists grade_group(gg_id integer primary key autoincrement, gg_company_id integer, gg_name text, gg_qty text, gg_monthly_percentage double, gg_quaterly_percentage double, gg_yearly_percentage double)");

                    executeQuery("alter table grade add column grade_group_id integer default 0");

                    executeQuery("create table if not exists scheme(scheme_id integer primary key autoincrement, scheme_client_id integer, scheme_year integer)");

                    executeQuery("create table if not exists scheme_params(sp_id integer primary key autoincrement, sp_scheme_id integer, sp_group_id integer, sp_group_qty double)");

                    executeQuery("create table if not exists scheme_data(sd_id integer primary key autoincrement, sd_client_id integer, sd_grade_id integer, sd_date date, sd_qty double)");

                    executeQuery("alter table scheme_params add column sp_month date");

                    executeQuery("alter table general_settings add column ss_msg_mou text");
                }
            }

            public static String getDatabaseHolder(bool validateFolder=true, bool justFolder=false)
            {
                try
                {
                    String holderText = System.IO.File.ReadAllText(DATABASE_HOLDER_FILE);
                    String fileName = holderText + "\\" + Properties.Resources.uid + ".db";

                    if (!validateFolder)
                    {
                        return justFolder ? holderText : fileName;
                    }

                    if (System.IO.Directory.Exists(holderText))
                    {
                        return justFolder ? holderText : fileName;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    Job.Log("getDatabaseHolder", ex);
                }
                return null;
            }

            public static bool isDatabaseExists()
            {

                return getDatabaseHolder() != null;
                /*
                try
                {
                    checkHolderFile:
                    bool isHolderExists = System.IO.File.Exists(DATABASE_HOLDER_FILE);

                    if (isHolderExists)
                    {
                        DATABASE_FILE = System.IO.File.ReadAllText(DATABASE_HOLDER_FILE);
                    }
                    else
                    {

                        goto checkHolderFile;
                    }

                }
                catch (Exception ex)
                {

                }

                return System.IO.File.Exists(DATABASE_FILE);
                 * */
            }

            public static object executeScalar(String query)
            {
                long k = lockAccess();
                try
                {
                    if (databaseConnection.State != System.Data.ConnectionState.Open)
                        databaseConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, databaseConnection);
                    object obj = cmd.ExecuteScalar();
                    cmd.Dispose();
                    return obj;
                }
                catch (Exception ex)
                {
                    Job.Log("Error[executeScalar] [" + query + "]", ex);
                    return null;
                }
                finally
                {
                    releaseAccess(k);
                }
            }

            public static long last_inserted_rowid()
            {
                return (long)Job.DB.executeScalar("select last_insert_rowid()");
            }

            public static long countRows(String tableName, String companyColumn=null, long compId=0, String customWhere=null, SQLiteParameter[] userParam=null)
            {
                String where = companyColumn!=null ? " where 1=1" : " where 1=1";

                if (companyColumn != null)
                {
                    where += " and " + companyColumn + "=@compid";
                }

                if (customWhere != null)
                    where += " and " + customWhere;

                SQLiteParameter[] param = new SQLiteParameter[] { 
                    new SQLiteParameter("@compid",compId==0?Job.Companies.currentCompany.id:compId)
                };

                List<SQLiteParameter> listParam = new List<SQLiteParameter>();
                listParam.AddRange(param);
                if (userParam != null)
                    listParam.AddRange(userParam);

                SQLiteDataReader dr = executeReader("select count(*) as total from " + tableName + where, listParam.ToArray());
                if (dr!=null && dr.Read())
                {
                    return long.Parse(dr["total"].ToString());
                }
                return -1;
            }

            public static int executeQuery(String query)
            {
                long k = lockAccess();
                try
                {
                    if (databaseConnection.State != System.Data.ConnectionState.Open)
                        databaseConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, databaseConnection);
                    int items = cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    return items;
                }
                catch (Exception ex)
                {
                    Job.Log("Error[executeQuery] [" + query + "]", ex);
                    return -1;
                }
                finally
                {
                    releaseAccess(k);
                }

            }

            public static int executeQuery(String query, SQLiteParameter[] values)
            {
                //long k = lockAccess();
                try
                {
                    if (databaseConnection.State != System.Data.ConnectionState.Open)
                        databaseConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, databaseConnection);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(values);
                    int items = cmd.ExecuteNonQuery();
                    //cmd.Dispose();
                    return items;
                }
                catch (Exception ex)
                {
                    Job.Log("Error[executeQueryWithParam] [" + query + "]", ex);
                    return -1;
                }
                finally
                {
                    //releaseAccess(k);
                }
            }

            public static SQLiteDataReader executeReader(String query)
            {
                long k = lockAccess();
                try
                {
                    if (databaseConnection.State != System.Data.ConnectionState.Open)
                        databaseConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, databaseConnection);
                    return cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Job.Log("Error[executeReader] [" + query + "]", ex);
                    return null;
                }
                finally
                {
                    releaseAccess(k);
                }
            }

            public static SQLiteDataReader executeReader(String query, SQLiteParameter[] values)
            {
                long k = lockAccess();
                try
                {
                    if (databaseConnection.State != System.Data.ConnectionState.Open)
                        databaseConnection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(query, databaseConnection);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(values);
                    return cmd.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Job.Log("Error[executeReaderWithParam] [" + query + "]", ex);
                    return null;
                }
                finally
                {
                    releaseAccess(k);
                }
            }
        }


        #endregion
    }

    public class ComboItem
    {
        #region ComboItem
        public ComboItem(String name, object value)
        {
            this.Name = name;
            this.Value = value;
        }
        public String Name = "";
        public object Value = "";

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}