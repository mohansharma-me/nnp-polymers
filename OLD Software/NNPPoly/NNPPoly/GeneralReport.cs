using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Threading;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace NNPPoly
{
    public partial class GeneralReport : Form
    {
        private static DateTime dtMon;
        private static String LessDays="";
        private WaitingDialog waitingDialog;
        private static ListView lv = new ListView();
        private double _amtDue = 0, _intDue = 0;

        private double interestCounted = -1;

        public bool isWaitingEnabled = true;

        public delegate void ProcessDoneHandler(Decimal clientID, ListView lv);
        public delegate void AmountHandler(Decimal clientID, double amtDue, double intDue);
        public event ProcessDoneHandler processDone;
        public event AmountHandler amountDone;

        public GeneralReport(int month,int year)
        {
            try
            {
                InitializeComponent();
                try
                {
                    dtMon = new DateTime(year, month, 1, 12, 0, 0);
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Incorrect selected month and year, please select month by clicking on appropriate month button and also select year if necessary.", "Date invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                }
                //LessDays = Datastore.dataFile.DefualtLessDays.ToString();
                LessDays = Datastore.current.LessDays.ToString();

            }
            catch (Exception excep)
            {
                String err = "Unable to initilize monthly_report operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GeneralReport_Load(object sender, EventArgs e)
        {
            try
            {
                colAutoSize();
                btnProcess_Click(btnProcess, new EventArgs());
                if (processDone != null)
                {
                    ListView tmplv = new ListView();
                    foreach (ListViewItem li in lvUser.Items)
                        tmplv.Items.Add(li.Clone() as ListViewItem);
                    processDone(Datastore.current.ID, tmplv);
                }
                if (amountDone != null)
                {
                    amountDone(Datastore.current.ID, _amtDue, _intDue);
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform GeneralReport_Load operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void colAutoSize()
        {
            for (int i = 0; i < lvUser.Columns.Count; i++)
            {
                lvUser.Columns[i].TextAlign = HorizontalAlignment.Center;
                if (i == 3)
                {
                    lvUser.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
                else
                {
                    lvUser.Columns[i].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                }
                if (lvUser.Columns[i].Width < 90)
                    lvUser.Columns[i].Width = 90;
            }
        }

        private void lv_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void lv_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            /*bool empty = true;
            foreach (ListViewItem.ListViewSubItem sitem in e.Item.SubItems)
                if (sitem.Text.Trim().Length > 0) empty = false;
            if (!empty)*/
            e.Item.UseItemStyleForSubItems = true;
            e.DrawDefault = true;
        }

        private void lv_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
            int ec = e.ColumnIndex;
            if (ec >= 0 && ec <= 1)
            {
                
            }
            else if (ec >= 2 && ec <= 5)
            {
                if (e.SubItem.Text.Length > 0)
                    e.SubItem.BackColor = Color.SkyBlue;
            }
            else if (ec >= 6 && ec <= 9)
            {

            }
            else if (ec >= 10 && ec <= 13)
            {

            }
            else if (ec >= 14 && ec <= 15)
            {

            }

            e.SubItem.ForeColor = GetContrastedColor(e.SubItem.BackColor);
            
        }

        private Color GetContrastedColor(Color colorToContrast)
        {
            try
            {
                var yiq = ((colorToContrast.R * 299) + (
                    colorToContrast.G * 587) + (
                    colorToContrast.B * 114)) / 1000;

                return (yiq >= 128) ? Color.FromArgb(40, 40, 40) : Color.WhiteSmoke;

            }
            catch (Exception excep)
            {
                String err = "Unable to calculate contrasted_color operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Color.Empty;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            lvUser.Visible = false;
            panHtml.Visible = false;

            btnProcess.Enabled = false;
            btnProcess.Text = "Wait..";
            lvUser.Items.Clear();
                

            Thread thread = new Thread(new ThreadStart(adjustmentCode));
            thread.Name = "AdjustmentThread";
            thread.Priority = ThreadPriority.Highest;
            
            waitingDialog = new WaitingDialog(isWaitingEnabled);
            waitingDialog.Text = "Generating report ...";
            

            thread.Start();
            
            if (isWaitingEnabled)
                waitingDialog.ShowDialog(this);
            while (thread.IsAlive)
                System.Windows.Forms.Application.DoEvents();
        }

        private void adjustmentCode()
        {
            Action action = null;
            List<Payment> icome = null, ocome = null;

            #region payment_divider
            try
            {
                if (Datastore.current.Payments.Count == 0) return;
                lv = new ListView();
                action = () =>
                {
                    foreach (ColumnHeader ch in lvUser.Columns)
                        lv.Columns.Add(ch.Text);
                };
                Invoke(action);
                GC.Collect();
                icome = new List<Payment>(); //credit
                ocome = new List<Payment>(); //debit

                List<Payment> payments = Datastore.current.Payments.ToList<Payment>();
                payments.Sort(new Comparison<Payment>(Display.paymentSorter));
                if (Datastore.current.OpeningBalance > 0)
                {
                    Payment obp = new Payment();
                    obp.Date = new DateTime(payments[0].Date.Year, 4, 1, 12, 0, 0);
                    if (Datastore.current.OBType.ToLower().Contains("debit"))
                        obp.Debit = Datastore.current.OpeningBalance;
                    else
                        obp.Credit = Datastore.current.OpeningBalance;
                    obp.DocChqNo = "Opening balance";
                    obp.MT = "-";
                    obp.Particulars = "O.B.";
                    if (Datastore.current.OBType.ToLower().Contains("debit"))
                        obp.Remain = 0;
                    else
                        obp.Remain = obp.Credit;
                    obp.Type = "sale";
                    payments.Insert(0, obp);
                }

                foreach (Payment pay in payments)
                {
                    int dtmon_month = dtMon.Month;
                    int dtmon_year = dtMon.Year;
                    int pay_month = pay.Date.Month;
                    int pay_year = pay.Date.Year;

                    if (pay_year > dtmon_year)
                    {
                        continue;
                    }
                    else if (pay_year == dtmon_year && pay_month > dtmon_month)
                    {
                        continue;
                    }

                    Payment p = new Payment();
                    p.Credit = pay.Credit;
                    p.Date = new DateTime(pay.Date.Year, pay.Date.Month, pay.Date.Day, 12, 0, 0);
                    p.Debit = pay.Debit;
                    p.DocChqNo = pay.DocChqNo;
                    p.ID = pay.ID;
                    p.MT = pay.MT;
                    if (p.MT != null && p.MT.Trim().Length > 0)
                    {
                        double mtValue = 0;
                        double.TryParse(p.MT, out mtValue);
                        p.MT = mtValue.ToString("0.000");
                    }
                    p.Particulars = pay.Particulars;
                    p.Remain = pay.Remain;
                    p.Tag = pay.Tag;
                    p.Type = pay.Type;
                    p.Grade = pay.Grade;
                    p.color = pay.color;
                    p.HighlightThis = pay.HighlightThis;

                    if (pay.Credit > 0)
                    {
                        icome.Add(p);
                    }
                    else if (pay.Debit > 0)
                    {
                        ocome.Add(p);
                    }
                }

            }
            catch (Exception excep)
            {
                String err = "Unable to divise payments into categories.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region initilize report with debits
            try
            {
                icome.Sort(new Comparison<Payment>(Display.paymentSorter));
                ocome.Sort(new Comparison<Payment>(Display.paymentSorter));

                Color lvUserBG = Color.Empty, lvUserFG = Color.Empty;
                action = () =>
                {
                    lvUserBG = lvUser.BackColor;
                    lvUserFG = lvUser.ForeColor;
                };
                Invoke(action);

                foreach (Payment op in ocome) //default debit entries
                {
                    bool isSpecialType = false;
                    if (Datastore.dataFile.SpecialTypes.Contains(op.Type.Trim().ToLower()))
                        isSpecialType = true;
                    ListViewItem li = new ListViewItem(new String[] {
                        "","",
                        op.Date.ToString(Program.SystemSDFormat),op.DocChqNo,op.MT,op.Debit.ToString(".00"),
                        "","","","","","","","","",""
                    });
                    if (isSpecialType)
                    {
                        li.SubItems[3].Tag = "ST";
                    }
                    li.Tag = op;
                    li.UseItemStyleForSubItems = true;
                    li.BackColor = lvUserBG;
                    li.ForeColor = lvUserFG;
                    lv.Items.Add(li);
                    ListViewItem emptyLi = new ListViewItem(new String[] { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" });
                    emptyLi.UseItemStyleForSubItems = true;
                    emptyLi.BackColor = Color.GhostWhite;
                    emptyLi.ForeColor = Color.GhostWhite;
                    emptyLi.Tag = null;
                    lv.Items.Add(emptyLi);
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize listview_monthlyreport operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region generate color enumaration
            List<Color> _colors = null;
            try
            {
                Decimal colorCounter = 0;
                _colors = new List<Color>();
                foreach (PropertyInfo property in typeof(System.Drawing.Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
                    if (property.PropertyType == typeof(System.Drawing.Color))
                    {
                        _colors.Add(Color.FromName(property.Name));
                        colorCounter++;
                    }
            }
            catch (Exception excep)
            {
                String err = "Unable to collect colors_enumeration operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            System.Collections.IEnumerator colors = _colors.GetEnumerator();
            #endregion

            #region process report
            try
            {
                if (icome.Count + ocome.Count == 0)
                {
                    
                }
                alloter(icome, ocome, true, colors);
                alloter(icome, ocome, false, colors);

                foreach (String typeTotype in Datastore.dataFile.SpecialTypes)
                    alloter(icome, ocome, false, colors, true, typeTotype);

                //showRemainingBalance(icome);

                interestAlloter(); // days calculator

                lessDaysCalc(); // 10 alloter

                formatResolver(); // format resolver
            }
            catch (Exception excep)
            {
                String err = "Unable to perform monthlyreport_generation operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region generate html report
            String htmlCode = Properties.Resources.html;
            try
            {
                bool payment = false, flagAdd = false;
                List<ListViewItem> items = new List<ListViewItem>();

                foreach (ListViewItem li in lv.Items)
                {
                    Payment debit = li.Tag as Payment;
                    Payment credit = li.SubItems[6].Tag as Payment;
                    if (li.SubItems[2].Text.Length > 0 && li.SubItems[3].Text.Length > 0 && li.SubItems[4].Text.Length > 0 && li.SubItems[5].Text.Length > 0)
                    {
                        payment = true;
                        items = new List<ListViewItem>();
                    }

                    if (payment)
                    {
                        String ddate = debit != null ? debit.Date.ToString("dd-MM-yyyy") : li.SubItems[2].Text;
                        String cdate = credit != null ? credit.Date.ToString("dd-MM-yyyy") : li.SubItems[9].Text;

                        bool emptyLi = true;
                        foreach (ListViewItem.ListViewSubItem sub in li.SubItems)
                        {
                            if (sub.Text.Length > 0)
                            {
                                emptyLi = false;
                                break;
                            }
                        }

                        //if (ddate.Contains(dtMon.ToString("MM-yyyy")) || cdate.Contains(dtMon.ToString("MM-yyyy")))
                        bool fDDate = ddate.Contains(dtMon.ToString("MM-yyyy"));
                        bool fCDate = cdate.Contains(dtMon.ToString("MM-yyyy"));
                        if (fDDate || fCDate)
                        {
                            if (!fDDate && ddate.Length > 0)
                            {
                                /*//DateTime tmpDt = DateTime.Parse(ddate);
                                double days = dtMon.Subtract(debit.Date).TotalDays;

                                if (days >= 0)
                                    flagAdd = true;*/
                                int dyear = debit.Date.Year, dmonth = debit.Date.Month;
                                int myear = dtMon.Year, mmonth = dtMon.Month;

                                if (dyear < myear || (dyear==myear && dmonth<=mmonth))
                                {
                                    flagAdd = true;
                                }

                                if (dyear <= myear && dmonth <= mmonth)
                                {
                                    
                                }
                            }
                            else if(fDDate)
                            {
                                flagAdd = true;
                            }
                        }
                        else if (cdate.Length == 0 && ddate.Length > 0)
                        {
                            //DateTime tmpDt = DateTime.Parse(ddate);

                            double days = dtMon.Subtract(debit.Date).TotalDays;
                            if (days >= 0)
                                flagAdd = true;
                        }
                        else if (debit != null && debit.Debit != debit.Remain)
                        {
                            flagAdd = true;
                        }

                        items.Add(li.Clone() as ListViewItem);

                        if (emptyLi)
                        {
                            payment = false;
                        }
                    }

                    if (!payment)
                    {
                        if (flagAdd)
                        {
                            action = () =>
                            {
                                lvUser.Items.AddRange(items.ToArray());
                            };
                            Invoke(action);
                            addTR(ref htmlCode, items.ToArray());
                        }
                        flagAdd = false;
                    }
                }

                showRemainingBalance(icome);

                items = new List<ListViewItem>();
                foreach (ListViewItem li in lv.Items)
                {
                    if (li.SubItems[7].Tag != null && li.SubItems[7].Tag.ToString().Equals("remli"))
                    {
                        items.Add(li.Clone() as ListViewItem);
                    }
                }
                if (items.Count > 0)
                {
                    //lvUser.Items.AddRange(items.ToArray());
                    action = () =>
                    {
                        lvUser.Items.AddRange(items.ToArray());
                    };
                    Invoke(action);
                    addTR(ref htmlCode, items.ToArray());
                }

                action = () =>
                {
                    //lvUser.Visible = true;
                    btnProcess.Enabled = true;
                    btnProcess.Text = "Process";
                };
                Invoke(action);
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize monthlyreport_from_listview operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region generate html footer
            try
            {
                double dtotal = 0, ctotal = 0, itotal = 0, cdtotal = 0, amtdue = 0, intdue = 0, mttotal = 0;
                action = () =>
                {
                    foreach (ListViewItem li in lvUser.Items)
                    {
                        double temp = 0;
                        if (double.TryParse(li.SubItems[4].Text, out temp))
                        {
                            mttotal += temp;
                        }
                        if (double.TryParse(li.SubItems[5].Text, out temp))
                        {
                            dtotal += temp;
                        }
                        if (double.TryParse(li.SubItems[7].Text, out temp))
                        {
                            ctotal += temp;
                        }
                        if (double.TryParse(li.SubItems[13].Text, out temp))
                        {
                            itotal += temp;
                        }
                        if (double.TryParse(li.SubItems[14].Text, out temp))
                        {
                            cdtotal += temp;
                        }
                    }
                };
                Invoke(action);

                amtdue = dtotal - ctotal;
                intdue = itotal + cdtotal;

                _amtDue = amtdue;
                _intDue = intdue;

                htmlCode = htmlCode.Replace("%mttotal%", (mttotal >= 0 && mttotal < 1) ? "0.00" : mttotal.ToString("0.00"));
                htmlCode = htmlCode.Replace("%dtotal%", (dtotal >= 0 && dtotal < 1) ? "0.00" : dtotal.ToString("0.00"));
                htmlCode = htmlCode.Replace("%ctotal%", (ctotal >= 0 && ctotal < 1) ? "0.00" : ctotal.ToString("0.00"));
                htmlCode = htmlCode.Replace("%itotal%", (itotal >= 0 && itotal < 1) ? "0.00" : itotal.ToString("0.00"));
                htmlCode = htmlCode.Replace("%cdtotal%", (cdtotal >= 0 && cdtotal < 1) ? "0.00" : cdtotal.ToString("0.00"));
                htmlCode = htmlCode.Replace("%amtdue%", (amtdue >= 0 && amtdue < 1) ? "0.00" : amtdue.ToString("0.00"));
                htmlCode = htmlCode.Replace("%intdue%", (intdue >= 0 && intdue < 1) ? "0.00" : intdue.ToString("0.00"));
                interestCounted = intdue;
                String dtValue="";
                action=()=>{
                    dtValue=dtMon.ToString("MMMM yyyy");
                };
                Invoke(action);
                htmlCode = htmlCode.Replace("%newtr%", "").Replace("%title%", Datastore.current.ClientName + " - " + dtValue).Replace("%footext%", Datastore.current.FooText).Replace("%intrate%", Datastore.current.InterestRate1.ToString(".00"));

                action = () =>
                {
                    panHtml.Text = htmlCode;
                    panHtml.PerformLayout();
                    //panHtml.Visible = true;
                };
                Invoke(action);
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize htmlComponent_totalAmounts operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region write html-report to temp.html
            try
            {
                FileStream fs = new FileStream("temp.html", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(panHtml.Text);
                sw.Close();
                fs.Close();
            }
            catch (Exception excep)
            {
                String err = "Unable to create htmlpage.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region ui-update
            action = () =>
            {
                if (Main.simpleReport)
                {
                    lvUser.Visible = true;
                    panHtml.Visible = false;
                }
                else
                {
                    lvUser.Visible = false;
                    panHtml.Visible = true;
                }
            };
            Invoke(action);
            #endregion

            closeWaitingDialog();
        }

        private void showRemainingBalance(List<Payment> icome)
        {
            List<String[]> finalp = new List<String[]>();
            List<Decimal> countedids = new List<Decimal>();

            foreach (Payment ip in icome)
            {
                if (ip.Remain != 0)
                {
                    double days = ip.Date.Subtract(dtMon).TotalDays;
                    if (days < 0)
                    {
                        String[] tmpArray = new String[4];
                        tmpArray[0] = ip.Credit.ToString("0") + ".00";
                        tmpArray[1] = ip.Remain.ToString("0") + ".00";
                        tmpArray[2] = ip.DocChqNo;
                        tmpArray[3] = ip.Date.ToString(Program.SystemSDFormat);
                        finalp.Add(tmpArray);
                    }
                }
            }
            Action action = () =>
            {
                foreach (ListViewItem li in lvUser.Items)
                {
                    Payment ip = li.SubItems[6].Tag as Payment;
                    if (ip != null && !ip.Verified && (ip.Date.Year == dtMon.Year && ip.Date.Month == dtMon.Month))
                    {
                        double totalAllotedMoney = 0;
                        for (int i = li.Index; i < lvUser.Items.Count; i++)
                        {
                            ListViewItem nli = lvUser.Items[i];
                            Payment nip = nli.SubItems[6].Tag as Payment;
                            if (nip != null && nip.ID == ip.ID)
                            {
                                double allotedMoney = 0;
                                if (double.TryParse(nli.SubItems[7].Text.Trim(), out allotedMoney))
                                {
                                    totalAllotedMoney += allotedMoney;
                                }
                            }
                        }
                        if ((ip.Credit - totalAllotedMoney) != 0)
                        {
                            String[] tmpArray = new String[4];
                            tmpArray[0] = ip.Credit.ToString("0") + ".00";
                            tmpArray[1] = (ip.Credit - totalAllotedMoney).ToString("0") + ".00";
                            tmpArray[2] = ip.DocChqNo;
                            tmpArray[3] = ip.Date.ToString(Program.SystemSDFormat);
                            finalp.Add(tmpArray);
                        }
                        countedids.Add(ip.ID);
                        ip.Verified = true;
                    }
                }
            };
            Invoke(action);

            foreach (Payment ip in icome)
            {
                if (ip.Date.Month == dtMon.Month && ip.Date.Year == dtMon.Year && !countedids.Contains(ip.ID))
                {
                    String[] tmpArray = new String[4];
                    tmpArray[0] = ip.Credit.ToString("0") + ".00";
                    tmpArray[1] = ip.Remain.ToString("0") + ".00";
                    tmpArray[2] = ip.DocChqNo;
                    tmpArray[3] = ip.Date.ToString(Program.SystemSDFormat);
                    finalp.Add(tmpArray);
                }
            }

            foreach (String[] array in finalp)
            {
                ListViewItem li = new ListViewItem(new String[] { "", "", "", "", "","",
                    array[0], array[1], array[2], array[3],
                    "", "", "", "", ""});
                action = () =>
                {
                    li.BackColor = lvUser.BackColor;
                    li.ForeColor = lvUser.ForeColor;
                };
                Invoke(action);
                li.Tag = null;
                li.SubItems[7].Tag = "remli";
                lv.Items.Add(li);
            }
        }

        private void formatResolver()
        {
            try
            {
                foreach (ListViewItem li in lv.Items)
                {
                    Payment debit = li.Tag as Payment;
                    Payment credit = li.SubItems[6].Tag as Payment;

                    if (debit != null && Datastore.dataFile.PriorityTypes.Find(x => (x.ToLower().Trim().Equals(debit.Type.Trim().ToLower()))) != null)
                    {
                        li.SubItems[4].Text = "-";
                    }
                    else if (debit != null && debit.MT.Trim().Length > 0 && (debit.MT.Trim().Equals("0.000") || debit.MT.Trim().Equals("0")))
                    {
                        li.SubItems[4].Text = "-";
                    }

                    #region changes-on-1/11,04/12

                    /*
                     * old code
                     * if(13 is clear) then
                     * clear 14 as well.
                     */


                    if (li.SubItems[13].Text.Trim().Length == 0)
                    {
                        double dueday = 0;
                        double.TryParse(li.SubItems[12].Text, out dueday);
                        bool val=dueday < 0;
                        if (!val)
                        {
                            li.SubItems[14].Text = "";
                        }
                    }

                    #endregion
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize zeromt_to_desh.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lessDaysCalc()
        {
            try
            {
                foreach (ListViewItem li in lv.Items)
                {
                    Payment de = li.Tag as Payment;
                    Payment cr = li.SubItems[6].Tag as Payment;
                    if (de == null || (de != null && !de.Type.ToLower().Equals("sale")))
                    {
                        if (de != null)
                            li.SubItems[11].Text = "0";
                        continue;
                    }

                    DateTime dd = new DateTime(), cd = new DateTime();

                    if (de != null)
                        dd = de.Date;
                    if (cr != null)
                        cd = cr.Date;

                    if (de != null && cr != null)
                    {
                        String lDate = li.SubItems[1].Text;
                        String nDate = li.SubItems[0].Text;
                        if (lDate.Length > 0)
                        {
                            li.SubItems[11].Text = "0";
                        }
                        else
                        {
                            if (de.Type.ToLower().Equals("sale") && li.SubItems[1].Text.Trim().Length == 0)
                                li.SubItems[11].Text = LessDays;
                            else
                                li.SubItems[11].Text = "0";
                        }

                        double tdays = 0;
                        if (double.TryParse(li.SubItems[10].Text, out tdays))
                        {
                            if (tdays < 0)
                                li.SubItems[11].Text = "0";
                        }
                    }

                    if (de != null && cr == null)
                    {
                        double tdays = double.Parse(li.SubItems[10].Text);
                        if (tdays >= 10)
                        {
                            if (de.Type.ToLower().Equals("sale") && li.SubItems[1].Text.Trim().Length == 0)
                                li.SubItems[11].Text = LessDays;
                            else
                                li.SubItems[11].Text = "0";
                        }
                        else
                        {
                            
                            if (de.Type.ToLower().Equals("sale") && li.SubItems[1].Text.Trim().Length == 0)
                            {
                                li.SubItems[0].Text = "";
                                li.SubItems[10].Text = "";
                                li.SubItems[11].Text = LessDays;
                            }
                            else
                            {
                                li.SubItems[10].Text = "";
                                li.SubItems[11].Text = "0";
                            }
                        }
                    }

                    if (li.SubItems[0].Text.Length > 0 && li.SubItems[1].Text.Length > 0)
                        li.SubItems[11].Text = "0";
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform lessDaysCalc_phase1 operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                foreach (ListViewItem li in lv.Items)
                {
                    Payment paym = li.Tag as Payment;

                    if (li.SubItems[0].Text.Length > 0 && li.SubItems[11].Text.Length == 0)
                    {
                        String tmpLD = LessDays;
                        if (li.SubItems[0].Tag != null && li.SubItems[0].Tag is Payment)
                        {
                            Payment tempPay = li.SubItems[0].Tag as Payment;
                            if (tempPay.Type.ToLower().Equals("sale") && li.SubItems[1].Text.Trim().Length == 0)
                                tmpLD = LessDays;
                            else
                            {
                                double days = 0;
                                if (li.SubItems[1].Text.Trim().Length > 0)
                                {
                                    DateTime lDate = DateTime.Parse(li.SubItems[1].Text.Trim());
                                    lDate = new DateTime(lDate.Year, lDate.Month, lDate.Day, 12, 0, 0);
                                    days = lDate.Subtract(tempPay.Date).TotalDays / 30;
                                }
                                else
                                {
                                    days = 1;
                                }
                                if (days >= 1)
                                {
                                    tmpLD = "0";
                                }
                                tmpLD = "0";
                            }
                        }
                        li.SubItems[11].Text = tmpLD;
                        /*if (paym != null && paym.Type.ToLower().Equals("sale"))
                            li.SubItems[11].Text = LessDays;
                        else
                            li.SubItems[11].Text = li.SubItems[11].Text;*/
                    }
                    else if (li.SubItems[0].Text.Length > 0 && li.SubItems[11].Text.Equals("0"))
                    {
                        String backupTotalDay = li.SubItems[10].Text;
                        if (paym != null && paym.Type.ToLower().Equals("sale"))
                            li.SubItems[10].Text = "";
                        else
                            li.SubItems[10].Text = li.SubItems[10].Text;

                        if (li.SubItems[0].Text.Length > 0 && li.SubItems[1].Text.Length > 0)
                            li.SubItems[10].Text = backupTotalDay;
                    }


                    if (li.Tag != null && li.Tag.ToString().Equals("stop"))
                    {
                        bool flag1 = false;
                        if (li.Index != 0)
                        {
                            if (lv.Items[li.Index - 1].SubItems[1].Text.Length == 0 && !Datastore.dataFile.SpecialTypes.Contains((lv.Items[li.Index - 1].Tag as Payment).Type.Trim().ToLower()))
                                flag1 = true;
                            else if (Datastore.dataFile.SpecialTypes.Contains((lv.Items[li.Index - 1].Tag as Payment).Type.Trim().ToLower()))
                                flag1 = false;
                        }
                        if (!flag1)
                            li.SubItems[11].Text = "0";
                    }

                    Payment pp = li.Tag as Payment;
                    if (pp != null && pp.DocChqNo.Contains("Opening balance"))
                        li.SubItems[11].Text = "0";

                    if (pp != null && pp.Type.Trim().ToLower().Equals("frt"))
                    {
                        Payment ip=li.SubItems[6].Tag as Payment;
                        if (ip != null)
                        {
                            DateTime opDate = new DateTime(pp.Date.Year, pp.Date.Month, pp.Date.Day, 12, 0, 0);
                            DateTime ipDate = new DateTime(ip.Date.Year, ip.Date.Month, ip.Date.Day, 12, 0, 0);
                            if (opDate.Month == ipDate.Month && opDate.Year == ipDate.Year && opDate.AddDays(1).Day == ipDate.Day)
                            {
                                li.SubItems[10].Text = "0";
                            }
                        }
                    }

                    bool flag = false;
                    double tot = 0, les = 0, due = 0;
                    if (double.TryParse(li.SubItems[10].Text, out tot) && double.TryParse(li.SubItems[11].Text, out les))
                    {
                        due = tot - les;
                        li.SubItems[12].Text = due.ToString();
                        flag = true;
                    }

                    if (flag)
                    {
                        //21*due*apamt/36500
                        double apamt = 0, iAmt = 0, tmpone = 0;
                        if (tot < 0)
                            tmpone = tot;
                        else
                            tmpone = due;

                        double intrate = Datastore.current.InterestRate1;

                        Payment pde = li.Tag as Payment;
                        Payment pcr = li.SubItems[6].Tag as Payment;
                        if (pde != null && pcr != null)
                        {
                            double fullDays = pcr.Date.Subtract(pde.Date).TotalDays;
                            if (fullDays > Datastore.current.CutOffDays)
                            {
                                intrate = Datastore.current.InterestRate2;
                                li.SubItems[13].Tag = "LATE";
                            }
                        }

                        if (pde != null && pcr == null)
                        {
                            DateTime cdate = DateTime.Parse(li.SubItems[0].Text.Trim());
                            cdate = new DateTime(cdate.Year, cdate.Month, cdate.Day, 12, 0, 0);

                            double fullDays = cdate.Subtract(pde.Date).TotalDays;
                            if (fullDays > Datastore.current.CutOffDays)
                            {
                                intrate = Datastore.current.InterestRate2;
                                li.SubItems[13].Tag = "LATE";
                            }
                        }


                        if (li.Tag != null && li.Tag.ToString().Equals("stop"))
                        {
                            DateTime debitDate = new DateTime();
                            bool dtFlag = false;
                            for (int i = li.Index; i >= 0; i--)
                            {
                                if (lv.Items[i].SubItems[2].Text.Length > 0 && lv.Items[i].Tag is Payment)
                                {
                                    dtFlag = true;
                                    debitDate = (lv.Items[i].Tag as Payment).Date;
                                    debitDate = new DateTime(debitDate.Year, debitDate.Month, debitDate.Day, 12, 0, 0);
                                    break;
                                }
                            }
                            if (dtFlag)
                            {
                                DateTime ndate = DateTime.Parse(li.SubItems[0].Text.Trim());
                                ndate = new DateTime(ndate.Year, ndate.Month, ndate.Day, 12, 0, 0);
                                double days = ndate.Subtract(debitDate).TotalDays;
                                if (days > Datastore.current.CutOffDays)
                                {
                                    intrate = Datastore.current.InterestRate2;
                                    li.SubItems[13].Tag = "LATE";
                                }
                            }
                        }


                        if (double.TryParse(li.SubItems[7].Text, out apamt))
                        {
                            iAmt = intrate * tmpone * apamt / 36500;
                        }
                        else if (double.TryParse(li.SubItems[5].Text, out apamt))
                        {
                            iAmt = intrate * tmpone * apamt / 36500;
                        }
                        else if (double.TryParse(li.SubItems[3].Text, out apamt))
                        {
                            iAmt = intrate * tmpone * apamt / 36500;
                        }

                        if ((iAmt > 0 && iAmt < 1) || iAmt == 0)
                            li.SubItems[13].Text = "0.00";
                        else
                            li.SubItems[13].Text = iAmt.ToString("0") + ".00";

                        if (tot > 0 && due < 0)
                            li.SubItems[13].Text = "";

                        //String myear = dtMon.ToString("MM-yyyy");
                        if (li.SubItems[9].Text.Length > 0 && li.SubItems[6].Tag is Payment)// && !li.SubItems[9].Text.Contains(myear))
                        {
                            Payment tmpPay = li.SubItems[6].Tag as Payment;
                            if (!(dtMon.Month == tmpPay.Date.Month && dtMon.Year == tmpPay.Date.Year))
                                li.SubItems[13].Text = "RECD";
                        }

                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform interest_calculation operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                //tknamt/(damt/mt)*100*totday -- cd for neg due-days
                //tknamt/(damt/mt)*100*lesday -- cd for pos due-days
                foreach (ListViewItem li in lv.Items)
                {
                    double tknamt = 0, damt = 0, mt = 0, totday = 0, lesday = 0, dued = 0;
                    double gradeAmt = 100;
                    if (double.TryParse(li.SubItems[12].Text, out dued))
                    {
                        Payment debitPayment = li.Tag as Payment;
                        if (debitPayment!=null && debitPayment.Date.Day == 6 && debitPayment.Date.Month == 11)
                        {
                            int tempsomething = 1;
                            Console.WriteLine("Hello:" + tempsomething);
                        }
                        for (int i = li.Index; i >= 0; i--)
                        {
                            if (double.TryParse(lv.Items[i].SubItems[5].Text, out damt))
                            {
                                if (debitPayment == null)
                                    debitPayment = lv.Items[i].Tag as Payment;
                                double.TryParse(lv.Items[i].SubItems[4].Text, out mt);
                                break;
                            }
                        }

                        if (debitPayment != null)
                        {
                            Grade grade = Datastore.dataFile.Grades.Find(x => (x.GradeName.Replace(" ", "").ToLower().Equals(debitPayment.Grade.Replace(" ", "").ToLower())));
                            if (grade != null)
                            {
                                gradeAmt = grade.Amount;
                            }
                        }

                        #region old-code on 04/12
                        /*
                         if (double.TryParse(li.SubItems[7].Text, out tknamt))
                        {
                            double cdamt = 0;
                            if (dued < 0 && double.TryParse(li.SubItems[10].Text, out totday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * totday;
                            }
                            else if (double.TryParse(li.SubItems[11].Text, out lesday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * lesday;
                            }
                            if (cdamt == 0)
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                            else
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                        }
                        else if (li.SubItems[2].Text.Length > 0 && li.SubItems[9].Text.Length == 0)
                        {
                            double cdamt = 0;
                            if (dued < 0 && double.TryParse(li.SubItems[10].Text, out totday))
                            {
                                cdamt = damt / (damt / mt) * gradeAmt * totday;
                            }
                            else if (double.TryParse(li.SubItems[11].Text, out lesday))
                            {
                                cdamt = damt / (damt / mt) * gradeAmt * lesday;
                            }
                            if (cdamt == 0)
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                            else
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                        }
                        else if (double.TryParse(li.SubItems[3].Text, out tknamt))
                        {
                            double cdamt = 0;
                            if (dued < 0 && double.TryParse(li.SubItems[10].Text, out totday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * totday;
                            }
                            else if (double.TryParse(li.SubItems[11].Text, out lesday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * lesday;
                            }

                            if (cdamt == 0)
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                            else
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                        }
                         */
                        #endregion

                        if (double.TryParse(li.SubItems[7].Text, out tknamt))
                        {
                            double cdamt = 0;
                            if (dued < 0 && double.TryParse(li.SubItems[10].Text, out totday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * totday;
                            }
                            else if (double.TryParse(li.SubItems[11].Text, out lesday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * lesday;
                            }
                            if (cdamt == 0)
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                            else
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                        }
                        else if (li.SubItems[2].Text.Length > 0 && li.SubItems[9].Text.Length == 0)
                        {
                            double cdamt = 0;
                            if (dued < 0 && double.TryParse(li.SubItems[10].Text, out totday))
                            {
                                cdamt = damt / (damt / mt) * gradeAmt * totday;
                            }
                            else if (double.TryParse(li.SubItems[11].Text, out lesday))
                            {
                                cdamt = damt / (damt / mt) * gradeAmt * lesday;
                            }
                            if (cdamt == 0)
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                            else
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                        }
                        else if (double.TryParse(li.SubItems[3].Text, out tknamt))
                        {
                            double cdamt = 0;
                            if (dued < 0 && double.TryParse(li.SubItems[10].Text, out totday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * totday;
                            }
                            else if (double.TryParse(li.SubItems[11].Text, out lesday))
                            {
                                cdamt = tknamt / (damt / mt) * gradeAmt * lesday;
                            }

                            if (cdamt == 0)
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                            else
                                li.SubItems[14].Text = cdamt.ToString("0") + ".00";
                        }


                        //changed on 04/12
                        if (totday < 0)
                            li.SubItems[14].Text = "";
                    }

                    if (li.SubItems[13].Text.Contains("RECD"))
                        li.SubItems[14].Text = "RECD";

                    if (li.SubItems[1].Text.Length == 0 && (li.SubItems[10].Text.Equals("1") || li.SubItems[10].Text.Equals("0") || li.SubItems[10].Text.Equals("-1")))
                    {
                        Payment p = li.Tag as Payment;
                        if (p != null && p.Type.Trim().ToLower().Equals("sale"))
                            li.SubItems[13].Text = li.SubItems[14].Text = "";
                    }


                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform cash_discount operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void addTR(ref String html,ListViewItem[] items)
        {
            try
            {
                foreach (ListViewItem li in items)
                {
                    bool emptyLi = true;
                    foreach (ListViewItem.ListViewSubItem sub in li.SubItems)
                    {
                        if (sub.Text.Length > 0)
                            emptyLi = false;
                    }

                    if (emptyLi)
                    {
                        html = html.Replace("%newtr%", "<tr><td bgcolor='white' colspan='15'></td></tr>" + Environment.NewLine + "%newtr%");
                    }
                    else
                    {
                        html = html.Replace("%newtr%", Properties.Resources.trRep);
                        html = html.Replace("%NEW DATE%", li.SubItems[0].Text);
                        html = html.Replace("%LAST DATE%", li.SubItems[1].Text);
                        html = html.Replace("%DATE1%", li.SubItems[2].Text);
                        if (li.Tag != null && li.Tag.ToString().Equals("stop"))
                        {
                            html = html.Replace("%INVOICE NO%", "<center><font size='20'>"+li.SubItems[3].Text+"</font></center>");
                        }
                        else
                        {
                            html = html.Replace("%INVOICE NO%", li.SubItems[3].Text);
                        }
                        html = html.Replace("%MT%", li.SubItems[4].Text);
                        html = html.Replace("%AMT1%", li.SubItems[5].Text);
                        String u1 = "";
                        String u2 = "";
                        if (li.SubItems[7].Tag != null && li.SubItems[7].Tag.ToString().Equals("remli"))
                        {
                            u1 = "<u><i>";
                            u2 = "</i></u>";
                        }
                        html = html.Replace("%AMT4%", u1 + li.SubItems[6].Text + u2);
                        html = html.Replace("%AMT2%", u1 + li.SubItems[7].Text + u2);
                        html = html.Replace("%CH NO%", u1 + li.SubItems[8].Text + u2);
                        html = html.Replace("%DATE2%", u1 + li.SubItems[9].Text + u2);
                        html = html.Replace("%TOTAL DAYS%", li.SubItems[10].Text);
                        html = html.Replace("%LESS DAYS%", li.SubItems[11].Text);
                        html = html.Replace("%DUE DAYS%", li.SubItems[12].Text);
                        if (li.SubItems[13].Tag != null && li.SubItems[13].Tag.ToString().Equals("LATE"))
                            html = html.Replace("%AMT3%", "<u>" + li.SubItems[13].Text + "</u>");
                        else
                            html = html.Replace("%AMT3%", li.SubItems[13].Text);
                        html = html.Replace("%CD%", li.SubItems[14].Text);
                        Color bg = li.SubItems[7].BackColor;
                        Color fg = GetContrastedColor(bg);
                        
                        Payment p1 = null, p2 = null;
                        p1 = li.Tag as Payment;
                        p2 = li.SubItems[6].Tag as Payment;
                        if ((p1 != null && p1.HighlightThis) || (p2 != null && p2.HighlightThis))
                        {
                            if (p1 != null && p1.HighlightThis)
                                html = html.Replace("%hclass%", "highlight1");
                            if (p2 != null && p2.HighlightThis)
                            {
                                html = html.Replace("%hclass%", "");
                                html = html.Replace("%bg%", "rgb(255,0,0)");
                                html = html.Replace("%fg%", "rgb(255,255,255)");
                            }

                        }
                        else
                        {
                            html = html.Replace("%hclass%", "");
                            html = html.Replace("%bg%", "rgb(" + bg.R + "," + bg.G + "," + bg.B + ")");
                            html = html.Replace("%fg%", "rgb(" + fg.R + "," + fg.G + "," + fg.B + ")");
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform addtr_html operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void interestAlloter()
        {

            try
            {
                Color lvUserBG = Color.Empty, lvUserFG = Color.Empty;
                Action action = () =>
                {
                    lvUserBG = lvUser.BackColor;
                    lvUserFG = lvUser.ForeColor;
                };
                Invoke(action);
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    ListViewItem li = lv.Items[i];
                    Payment op = li.Tag as Payment;
                    Payment ip = li.SubItems[6].Tag as Payment;

                    DateTime opDate = DateTime.Today, ipDate = DateTime.Today;
                    if (op != null)
                        opDate = op.Date;
                    if (ip != null)
                        ipDate = ip.Date;

                    if (op != null && ip != null)
                    {
                        //opDate = DateTime.ParseExact(op.Date, "dd-MM-yyyy", Program.provider);
                        //ipDate = DateTime.ParseExact(ip.Date, "dd-MM-yyyy", Program.provider);

                        bool pFlag = false;
                        String str = Datastore.dataFile.PriorityTypes.Find(x => (x.ToLower().Equals(op.Type.ToLower())));
                        if (str != null) pFlag = true;

                        if (opDate.Month == ipDate.Month)// || pFlag)
                        {
                            double days = ipDate.Subtract(opDate).TotalDays;
                            li.SubItems[10].Text = days.ToString();
                        }
                        else
                        {
                            /*String mon = ""+ipDate.Month;
                            if (mon.Length == 1) mon = "0" + mon;
                            String lastDate = "01-" + mon + "-" + ipDate.Year;*/
                            DateTime lDate = new DateTime(ipDate.Year, ipDate.Month, 1, 12, 0, 0);//DateTime.ParseExact(lastDate, "dd-MM-yyyy", Program.provider);

                            double test = lDate.Subtract(opDate).TotalDays;
                            bool isSaleDebit = op.Type.Trim().ToLower().Equals("sale");

                            if (test >= double.Parse(LessDays) || pFlag || !isSaleDebit)
                            {
                                li.SubItems[1].Text = lDate.ToString(Program.SystemSDFormat);
                                double days = ipDate.Subtract(lDate).TotalDays;
                                li.SubItems[10].Text = days.ToString();
                            }
                            else
                            {
                                li.SubItems[1].Text = "";
                                double days = ipDate.Subtract(opDate).TotalDays;
                                li.SubItems[10].Text = days.ToString();
                            }
                        }

                        if (op.Remain != op.Debit)
                        {
                            DateTime dt = dtMon.AddMonths(1); //opDate.AddMonths(1);
                            DateTime nDate = new DateTime(dt.Year, dt.Month, 1, 12, 0, 0);//DateTime.ParseExact(newDate, "dd-MM-yyyy", Program.provider);
                            String lastDate = "";
                            for (int ind = li.Index; ind >= 0; ind--)
                            {
                                if (li.Tag.ToString().Equals("stop") || (li.Tag as Payment) == null)
                                    break;
                                if (li.SubItems[1].Text.Length > 0)
                                    lastDate = li.SubItems[1].Text;
                                else 
                                { 
                                    
                                }
                            }

                            DateTime destDate = opDate;
                            if (lastDate.Length > 0)
                            {
                                destDate = DateTime.Parse(lastDate);
                                destDate = new DateTime(destDate.Year, destDate.Month, destDate.Day, 12, 0, 0);
                            }

                            double days = Math.Abs(destDate.Subtract(nDate).TotalDays);
                            op.NewDate = nDate.ToString(Program.SystemSDFormat);

                            int newMonth = nDate.Month;
                            int opMonth = opDate.Month;
                            int ans = newMonth - opMonth;
                            String stopLastDate = "";

                            double tmpAns = 0, compare = 2;
                            if (nDate.Year != opDate.Year)
                            {
                                tmpAns = nDate.Subtract(opDate).TotalDays / 30;
                                compare = 2;
                            }
                            else
                            {
                                tmpAns = ans;
                                compare = 2;
                            }
                            String totalDays = "";
                            if (tmpAns >= compare)
                            {
                                //DateTime tmpDT = op.Date.AddMonths(1);
                                DateTime tmpDT = new DateTime(dtMon.Year, dtMon.Month, 1, 12, 0, 0);
                                
                                double tmpDays = tmpDT.Subtract(op.Date).TotalDays;
                                if (tmpDays < double.Parse(LessDays))
                                {
                                    if (op.Type.Trim().ToLower().Equals("sale"))
                                    {
                                        #region change-on-5/11 [problem: last-date problem after solution of 25-10 to 31-10 info]

                                        //change-on 04/12 of lastDate.Trim().Length>0
                                        if (op.Date.Month < tmpDT.Month && op.Date.Year <= tmpDT.Year && lastDate.Trim().Length>0)
                                        {
                                            stopLastDate = tmpDT.ToString(Program.SystemSDFormat);
                                            days = nDate.Subtract(tmpDT).TotalDays;
                                        }
                                        else
                                        {
                                            stopLastDate = "";
                                            days = nDate.Subtract(op.Date).TotalDays;
                                        }
                                        #endregion
                                        /*stopLastDate = "";
                                        days = nDate.Subtract(op.Date).TotalDays;*/
                                    }
                                    else
                                    {
                                        stopLastDate = tmpDT.ToString(Program.SystemSDFormat);
                                        days = nDate.Subtract(tmpDT).TotalDays;
                                    }
                                }
                                else
                                {
                                    DateTime dtStopLD = nDate.AddMonths(-1);
                                    stopLastDate = dtStopLD.ToString(Program.SystemSDFormat);
                                    days = nDate.Subtract(dtStopLD).TotalDays;
                                }
                                totalDays = days.ToString();
                            }
                            else // change on 17-12 if partial payment of debit in last 10 days then no cd/iamt if due days is less then 0
                            {
                                DateTime tmpDT = new DateTime(dtMon.Year, dtMon.Month, 1, 12, 0, 0);
                                tmpDT = tmpDT.AddMonths(1);
                                double tmpDays = tmpDT.Subtract(op.Date).TotalDays;
                                if (tmpDays < double.Parse(LessDays))
                                {
                                    totalDays = "";
                                } 
                                else 
                                {
                                    totalDays = days.ToString();
                                }
                            }

                            ListViewItem _li = new ListViewItem(new String[] { nDate.ToString(Program.SystemSDFormat), stopLastDate, "", (op.Debit - op.Remain).ToString(), "", "", "", "", "", "", totalDays, "", "", "", "", "", "" });
                            _li.UseItemStyleForSubItems = true;
                            _li.BackColor = lvUserBG;
                            _li.ForeColor = lvUserFG;
                            _li.Tag = "stop";
                            int placeIndex = li.Index + 1;
                            for (int tt = placeIndex; tt < lv.Items.Count; tt++)
                            {
                                if (lv.Items[tt].Tag == null)
                                {
                                    placeIndex = tt;
                                    lv.Items[tt].SubItems[0].Tag = "stop";
                                    break;
                                }
                                else if (lv.Items[tt].Tag.ToString().Equals("stop"))
                                {
                                    placeIndex = -1;
                                    break;
                                }
                            }
                            _li.SubItems[0].Tag = op;
                            if (placeIndex != -1)
                                lv.Items.Insert(placeIndex, _li);
                        }
                    }

                    if (ip == null && op != null)
                    {
                        //newdate lastdate calc
                        DateTime dt = dtMon.AddMonths(1); //opDate.AddMonths(1);
                        DateTime nDate = new DateTime(dt.Year, dt.Month, 1, 12, 0, 0);//DateTime.ParseExact(newDate, "dd-MM-yyyy", Program.provider);
                        double days = Math.Abs(opDate.Subtract(nDate).TotalDays);

                        li.SubItems[0].Text = nDate.ToString(Program.SystemSDFormat); // new date
                        li.SubItems[10].Text = days.ToString();

                        double test = double.Parse(LessDays);

                        if (!op.Type.Trim().ToLower().Equals("sale") && op.Date.Month!=dtMon.Month)
                        {
                            DateTime tmpLDate=nDate.AddMonths(-1);
                            li.SubItems[1].Text = tmpLDate.ToString(Program.SystemSDFormat);
                            days = Math.Abs(nDate.Subtract(tmpLDate).TotalDays);
                            li.SubItems[10].Text = days.ToString();
                        }
                    }

                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform interestAlloter_phase1 operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                foreach (ListViewItem li in lv.Items)
                {
                    Payment debit = li.Tag as Payment;
                    Payment credit = li.SubItems[6].Tag as Payment;
                    if (credit == null && debit != null)
                    {
                        String newDate = li.SubItems[0].Text;
                        String lastDate = li.SubItems[1].Text;
                        String debitDate = debit.Date.ToString(Program.SystemSDFormat);

                        if (lastDate.Length > 0 || newDate.Length == 0) continue;

                        DateTime dtNew = DateTime.Parse(newDate);
                        dtNew = new DateTime(dtNew.Year, dtNew.Month, dtNew.Day, 12, 0, 0);
                        DateTime dtLast = dtNew.AddMonths(-1);
                        //DateTime dtDebit = debit.Date;

                        double days = dtLast.Subtract(debit.Date).TotalDays;
                        if (days >= double.Parse(LessDays))
                        {
                            li.SubItems[1].Text = dtLast.ToString(Program.SystemSDFormat);
                            li.SubItems[10].Text = dtNew.Subtract(dtLast).TotalDays.ToString();
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform interestAlloter_phase2 operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void alloter(List<Payment> icome, List<Payment> ocome, bool filterPriorities, System.Collections.IEnumerator colors,bool isTypeToType=false,String typeTotype="")
        {
            try
            {
                if (isTypeToType && typeTotype.Trim().Length == 0) return;
                #region ALLOTER
                for (int i = 0; i < lv.Items.Count; i++) //debit
                {
                    ListViewItem li = lv.Items[i];
                    int index = li.Index;
                    Payment op = li.Tag as Payment;
                    if (op != null)
                    {
                        bool totFlag = Datastore.dataFile.SpecialTypes.Contains(op.Type.Trim().ToLower());
                        if (!isTypeToType && totFlag) continue;
                        if (isTypeToType && !totFlag) continue;
                        if (typeTotype.Trim().Length > 0 && !op.Type.Trim().ToLower().Equals(typeTotype.ToLower().Trim()))
                        {
                            continue;
                        }

                        String tempString = Datastore.dataFile.PriorityTypes.Find(x => (x.ToLower().Trim().Equals(op.Type.ToLower().Trim())));
                        if (tempString == null) // not priority
                        {
                            if (filterPriorities)
                                continue;
                        }
                        else // priority
                        {
                            if (!filterPriorities)
                                continue;
                        }
                    }

                    if (op != null)
                    {
                        Color lvUserBG = Color.Empty, lvUserFG = Color.Empty;
                        Action action = () =>
                        {
                            lvUserBG = lvUser.BackColor;
                            lvUserFG = lvUser.ForeColor;
                        };
                        Invoke(action);
                        foreach (Payment ip in icome) //credit
                        {
                            bool totFlag = Datastore.dataFile.SpecialTypes.Contains(ip.Type.Trim().ToLower());
                            if (!isTypeToType && totFlag) continue;
                            if (isTypeToType && !totFlag) continue;
                            if (typeTotype.Trim().Length > 0 && !ip.Type.Trim().ToLower().Equals(typeTotype.ToLower().Trim()))
                            {
                                continue;
                            }

                            //DateTime dt1 = ip.Date;
                            DateTime dt2 = dtMon.AddMonths(1);
                            dt2 = new DateTime(dt2.Year, dt2.Month, 1, 12, 0, 0);
                            //dt2 = dt2.AddMonths(1);
                            //dt2 = DateTime.ParseExact(dt2.ToString("01-MM-yyyy"), "dd-MM-yyyy", Program.provider);
                            double tmpDays = dt2.Subtract(ip.Date).TotalDays;
                            if (tmpDays <= 0)
                            {
                                continue;
                            }
                            if (op.Remain != op.Debit) // debit is not returned
                            {
                                if (ip.Remain != 0) // if credit has balance
                                {
                                    //set color code

                                    DateTime dop = op.Date;//DateTime.ParseExact(op.Date.Trim(), "dd-MM-yyyy", Program.provider);
                                    DateTime dip = ip.Date;//DateTime.ParseExact(ip.Date.Trim(), "dd-MM-yyyy", Program.provider);

                                    double cmp = dip.Subtract(dop).TotalDays;//dip.CompareTo(dop);
                                    if (filterPriorities && (cmp < 0))
                                    {
                                        continue;
                                    }

                                    if (ip.color == Color.Empty)
                                    {
                                        #region old_color_calib
                                        /*colors.MoveNext(); colors.MoveNext(); colors.MoveNext(); colors.MoveNext();
                                        ip.color = (Color)colors.Current;
                                        while (ip.color.Name.ToLower().Contains("black") || ip.color.Name.ToLower().Contains("white"))
                                        {
                                            colors.MoveNext(); colors.MoveNext(); colors.MoveNext(); colors.MoveNext();
                                            ip.color = (Color)colors.Current;
                                        }*/
                                        #endregion

                                        try
                                        {

                                            bool hasNext = colors.MoveNext();// colors.MoveNext(); colors.MoveNext(); colors.MoveNext();
                                            if (hasNext)
                                            {
                                                ip.color = (Color)colors.Current;
                                                while (ip.color.Name.ToLower().Contains("black") || ip.color.Name.ToLower().Contains("white"))
                                                {
                                                    hasNext = colors.MoveNext(); //colors.MoveNext(); colors.MoveNext(); colors.MoveNext();
                                                    if (hasNext)
                                                        ip.color = (Color)colors.Current;
                                                    else
                                                        colors.Reset();
                                                }
                                            }
                                            else
                                            {
                                                colors.Reset();
                                                hasNext = colors.MoveNext();
                                                if (hasNext)
                                                    ip.color = (Color)colors.Current;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            Console.WriteLine("");
                                        }
                                    }

                                    double reqD = op.Debit - op.Remain;//op.Remain == 0 ? op.Debit - op.Remain : op.Remain;//
                                    double availC = ip.Remain; //
                                    double disA = 0;

                                    if (reqD == availC)
                                    {
                                        disA = reqD;
                                        op.Remain = op.Debit;
                                        ip.Remain = 0;
                                    }
                                    else if (reqD > availC)
                                    {
                                        disA = availC;
                                        op.Remain += disA;// (reqD - availC);
                                        ip.Remain = 0;
                                    }
                                    else if (reqD < availC)
                                    {
                                        disA = reqD;
                                        op.Remain = op.Debit;
                                        ip.Remain -= disA;// (availC - reqD);
                                    }

                                    ListViewItem _li = new ListViewItem(new String[] {
                                        "","","","","","",
                                        ip.Credit.ToString("0.00"),disA.ToString("0.00"),ip.DocChqNo,ip.Date.ToString(Program.SystemSDFormat),
                                        "","","","","",""
                                    });
                                    _li.BackColor = lvUserBG;
                                    _li.ForeColor = lvUserFG;
                                    //ip.AdjustedDate = op.Date;

                                    if (li.Index == index)
                                    {
                                        li.SubItems[6].Text = ip.Credit.ToString("0.00");
                                        li.SubItems[7].Text = disA.ToString("0.00");
                                        li.SubItems[8].Text = ip.DocChqNo;
                                        li.SubItems[9].Text = ip.Date.ToString(Program.SystemSDFormat);
                                        li.SubItems[6].Tag = ip;

                                        for (int a = 6; a <= 9; a++)
                                        {
                                            li.SubItems[a].BackColor = ip.color;
                                            li.SubItems[a].ForeColor = Color.FromArgb(128, ip.color);
                                        }
                                        index++;
                                    }
                                    else
                                    {
                                        _li.SubItems[6].Tag = ip;
                                        _li.Tag = op;
                                        for (int a = 6; a <= 9; a++)
                                        {
                                            _li.SubItems[a].BackColor = ip.color;
                                            _li.SubItems[a].ForeColor = Color.FromArgb(128, ip.color);
                                        }
                                        lv.Items.Insert(index, _li);
                                        index++;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion ALLOTER
            }
            catch (Exception excep)
            {
                String err = "Unable to perform payment_adjustment operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error"+excep, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            panHtml.Visible = !checkBox1.Checked;
            lvUser.Visible = checkBox1.Checked;
        }

        public enum Months
        {
            January=1,
            February,
            March,
            April,
            May,June,July,August,September,October
            ,November,December
        }

        private void panHtml_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                cm.MenuItems.Add(new MenuItem("&Print directly (via Default Printer)", panHtml_RightClickAction));
                cm.MenuItems.Add(new MenuItem("&Print preview", panHtml_RightClickAction));
                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Export to Excel", panHtml_RightClickAction));
                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Switch to simple view", panHtml_RightClickAction));
                cm.MenuItems.Add(new MenuItem("-"));
                cm.MenuItems.Add(new MenuItem("&Next month", panHtml_RightClickAction));
                cm.MenuItems.Add(new MenuItem("&Previous month", panHtml_RightClickAction));
                cm.MenuItems.Add(new MenuItem("-"));

                MenuItem menu = new MenuItem("&Debit note");
                menu.MenuItems.Add(new MenuItem("&Print directly (via Default Printer)", DebitNote_RightClickAction));
                menu.MenuItems.Add(new MenuItem("-"));
                menu.MenuItems.Add(new MenuItem("&Save as", DebitNote_RightClickAction));
                cm.MenuItems.Add(menu);

                cm.Show(sender as Control, e.Location);
            }
        }

        private void DebitNote_RightClickAction(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                if (mi.Text.StartsWith("&Print directly"))
                {
                    printDirectly = true;
                    //exportDD("");
                    System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(exportDD));
                    th.Start("");
                    showWaitingDialog("Printing ...");
                }
                else if (mi.Text.StartsWith("&Save as"))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "Word files|*.doc;*.docx|All files|*.*";
                    sfd.FileName = Datastore.current.ClientName + "_DebitNote_" + dtMon.ToString("MMMM yyyy");
                    if (sfd.ShowDialog(this) == DialogResult.OK)
                    {
                        System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(exportDD));
                        th.Start(sfd.FileName);
                        showWaitingDialog("Saving debit note ...");
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform debitnote_rightclickaction operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportDD(object fname)
        {
            String outputFilename = fname.ToString();
            String tempFile = System.Windows.Forms.Application.StartupPath+"\\a4_debitnote_template.docx";
            try
            {
                if (!File.Exists(tempFile))
                {
                    try
                    {
                        System.IO.File.WriteAllBytes(tempFile, Properties.Resources.DebitNoteFormat);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Error in writing document file to saving location." + Environment.NewLine + "Error message:" + Environment.NewLine + ex.Message, "Document write error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to initilize template_debitnote operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (System.IO.File.Exists(tempFile))
            {
                try {
                    Word.Application appWord = new Word.Application();

                    Object filename = outputFilename;
                    Object boolFalse = false;
                    Word.Document doc = (Word.Document)appWord.Documents.Open(tempFile);

                    FindAndReplace(appWord, "%monthno%", dtMon.ToString("MM").ToUpper());
                    FindAndReplace(appWord, "%month%", dtMon.ToString("MMMM").ToUpper());
                    FindAndReplace(appWord, "%year%", dtMon.ToString("yyyy"));
                    FindAndReplace(appWord, "%name%", Datastore.current.ClientName);
                    FindAndReplace(appWord, "%address%", Datastore.current.ClientDescription);
                    FindAndReplace(appWord, "%amt%", interestCounted.ToString("0") + ".00");
                    FindAndReplace(appWord, "%words%", Main.NumberToWords((int.Parse(interestCounted.ToString("0")))));

                    if (outputFilename.Length==0)
                    {
                        doc.PageSetup.PaperSize = Word.WdPaperSize.wdPaperA4;
                        doc.PageSetup.Orientation = Word.WdOrientation.wdOrientPortrait;
                        /*/doc.PageSetup.Zoom = 80;
                        doc.PageSetup.BottomMargin = 0.25f;
                        doc.PageSetup.LeftMargin = 0.25f;
                        doc.PageSetup.RightMargin = 0.25f;
                        doc.PageSetup.TopMargin = 0.25f;
                        //doc.PageSetup.FitToPagesWide = 1;
                        //doc.PageSetup.CenterHorizontally = true;
                        //doc.PageSetup.CenterVertically = true;*/

                        if (printDirectly)
                        {
                            Decimal vfu=0;
                            Action act = () => {
                                NoOfPrints nop = new NoOfPrints();
                                nop.ValueFromUser = 0;
                                nop.ShowDialog(this);
                                vfu = nop.ValueFromUser;
                            };
                            Invoke(act);

                            if (vfu > 0)
                            {
                                //doc.PrintOut();
                                object background = false;
                                object missing = Type.Missing;
                                doc.PrintOut(background, missing, missing, missing, missing, missing, missing, (int)vfu);
                            }

                            for (int i = 0; i < 10; i++)
                            {
                                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                                System.Windows.Forms.Application.DoEvents();
                            }
                        }
                    }

                    if (outputFilename.Length > 0)
                    {
                        doc.SaveAs(outputFilename);
                        doc.Close(true);
                        appWord.Quit(true);
                    }
                    else
                    {
                        doc.Close(false);
                        appWord.Quit(false);
                    }
                }
                catch (Exception excep)
                {
                    String err = "Unable to generate debitnote_exportDD operation.";
                    Log.output(err, excep);
                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            printDirectly = printPreviewWindow = false;
            closeWaitingDialog();
        }

        private void showWaitingDialog(String title)
        {
            Action action = () => {
                waitingDialog = new WaitingDialog();
                waitingDialog.Text = title;
                waitingDialog.ShowDialog(this);
            };
            if (this.InvokeRequired)
                Invoke(action);
            else
                action();
        }

        private void closeWaitingDialog()
        {
            Action action = () => {
                if (waitingDialog != null)
                {
                    waitingDialog.Close();
                    waitingDialog = null;
                }
            };
            if (this.InvokeRequired)
                Invoke(action);
            else
                action();
        }

        public static void FindAndReplace(Microsoft.Office.Interop.Word.Application WordApp, object findText, object replaceWithText,bool shapeReplace=false,Word.Document document=null)
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
                if (shapeReplace && document!=null)
                {
                    foreach (Shape shape in document.Shapes)
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
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static bool printPreviewWindow = false,printDirectly=false;
        private void panHtml_RightClickAction(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                if (mi.Text.StartsWith("&Print directly"))
                {
                    printPreviewWindow = true;
                    printDirectly = true;
                    System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadExporter));
                    th.Name = "ReportPrinter";
                    th.Start("print");
                    showWaitingDialog("Printing ...");
                }
                else if (mi.Text.StartsWith("&Print preview"))
                {
                    printPreviewWindow = true;
                    printDirectly = false;
                    System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadExporter));
                    th.Name = "ReportPrinter";
                    th.Start("print");
                    showWaitingDialog("Opening preview ...");
                }
                else if (mi.Text.StartsWith("&Export"))
                {
                    exportXLS();
                }
                else if (mi.Text.StartsWith("&Switch"))
                {
                    Main.simpleReport = !Main.simpleReport;
                    btnProcess_Click(sender, e);
                }
                else if (mi.Text.StartsWith("&Next"))
                {
                    dtMon = dtMon.AddMonths(1);
                    btnProcess_Click(sender, e);
                }
                else if (mi.Text.StartsWith("&Previous"))
                {
                    dtMon = dtMon.AddMonths(-1);
                    btnProcess_Click(sender, e);
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform panHTML_RightClickAction operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exportXLS() {
            bool impFlag = true;
            try
            {
                Excel.Application app = new Excel.Application();
                app.Quit(); app.Quit();
                impFlag = true;
            }
            catch (Exception) { impFlag = false; }
            if (!impFlag)
            {
                MessageBox.Show(this, "Microsoft Office Interop library is not instaled, please install microsoft office 2010 or leter.", "MS Office not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog od = new SaveFileDialog();
            od.FileName = Datastore.current.ClientName + "_" + dtMon.ToString("MMMMyyyy");
            od.Filter = "Excel files|*.xls;*.xlsx|All files|*.*";
            if (od.ShowDialog(this) == DialogResult.OK)
            {
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(threadExporter));
                th.Name = "ReportExporter";
                th.Start(od.FileName);
                showWaitingDialog("Exporting ...");
            }
        }

        private void threadExporter(object fname)
        {
            try
            {
                String filename = fname.ToString();
                Excel.Application app = new Excel.Application();
                /*FileStream fs = new FileStream("temp.html", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(panHtml.Text);
                sw.Close();
                Excel.Workbook wb = app.Workbooks.Open(Application.StartupPath + "\\temp.html");
                wb.SaveAs(filename, Excel.XlFileFormat.xlExcel12);*/

                Excel.Workbook wb = app.Workbooks.Add();
                Excel.Worksheet ws = wb.Worksheets.Add();
                ws.Name = dtMon.ToString("MMMM yyyy");

                int count = 1;

                //header
                String range = "A1:O2";
                var mv = Type.Missing;
                ws.get_Range(range, mv).Merge();
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, Datastore.current.ClientName + " - " + dtMon.ToString("MMMM yyyy"));
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "A3:B3";
                ws.get_Range(range, mv).Merge();
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "INTEREST UPTO");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "C3:F3";
                ws.get_Range(range, mv).Merge();
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "RIL DESPATCH");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "G3:J3";
                ws.get_Range(range, mv).Merge();
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "ACTUAL PAYMENTS");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "K3";
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "TOTAL");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "L3";
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "LESS");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "M3:N3";
                ws.get_Range(range, mv).Merge();
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "INTEREST");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                range = "O3";
                ws.get_Range(range, mv).Merge();
                ws.get_Range(range, mv).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range, mv).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                ws.get_Range(range, mv).Font.Bold = true;
                ws.get_Range(range, mv).BorderAround(Excel.XlLineStyle.xlDouble, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlRgbColor.rgbBlack);
                ws.get_Range(range, mv).set_Value(mv, "CD");
                ws.get_Range(range, mv).EntireColumn.AutoFit();

                //ws.get_Range("A1:O2").Font.Size = 18;
                //ws.get_Range("A3:O3").Font.Size = 15;

                ws.get_Range("A4").set_Value(Type.Missing, "NEW DATE");
                ws.get_Range("B4").set_Value(Type.Missing, "LAST DATE");
                ws.get_Range("C4").set_Value(Type.Missing, "DATE");
                ws.get_Range("D4").set_Value(Type.Missing, "INVOICE NO");
                ws.get_Range("E4").set_Value(Type.Missing, "MT");
                ws.get_Range("F4").set_Value(Type.Missing, "AMT");
                ws.get_Range("G4").set_Value(Type.Missing, "CRDT AMT");
                ws.get_Range("h4").set_Value(Type.Missing, "ADJ. AMT");
                ws.get_Range("i4").set_Value(Type.Missing, "CH NO");
                ws.get_Range("j4").set_Value(Type.Missing, "DATE");
                ws.get_Range("k4").set_Value(Type.Missing, "DAYS");
                ws.get_Range("l4").set_Value(Type.Missing, "DAYS");
                ws.get_Range("m4").set_Value(Type.Missing, "DAYS");
                ws.get_Range("n4").set_Value(Type.Missing, "AMT");
                ws.get_Range("o4").set_Value(Type.Missing, "REVERSAL");

                ws.get_Range("A1:O4").Font.Bold = true;
                //ws.get_Range("A4:O4").Font.Size = 15;
                ws.get_Range("A3:O4").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("A3:O4").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                ws.get_Range("a3:o4").Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                ws.get_Range("a3:o4").Borders.Weight = Excel.XlBorderWeight.xlThin;
                ws.get_Range("a3:o4").Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                ws.get_Range("a3:o4").Borders.Color = Excel.XlRgbColor.rgbBlack;
                ws.get_Range("A3:O4").BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlMedium,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);
                ws.get_Range("A4:O4").BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlMedium,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);

                int rowid = 5; // done
                foreach (ListViewItem li in getListView().Items)
                {
                    try
                    {
                        ws.get_Range("A" + rowid).set_Value(Type.Missing, DateTime.Parse(li.SubItems[0].Text).ToOADate());
                        ws.get_Range("A" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }
                    catch (Exception) { }
                    try
                    {
                        ws.get_Range("B" + rowid).set_Value(Type.Missing, DateTime.Parse(li.SubItems[1].Text).ToOADate());
                        ws.get_Range("b" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }
                    catch (Exception) { }
                    try
                    {
                        ws.get_Range("C" + rowid).set_Value(Type.Missing, DateTime.Parse(li.SubItems[2].Text).ToOADate());
                        ws.get_Range("c" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }
                    catch (Exception) { }
                    if (li.Tag != null && li.Tag.ToString().Equals("stop")) 
                    {
                        ws.get_Range("D" + rowid).set_Value(Type.Missing, li.SubItems[3].Text);
                        ws.get_Range("D" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                        ws.get_Range("D" + rowid).Font.Bold=true;
                    }
                    else
                    {
                        ws.get_Range("D" + rowid).set_Value(Type.Missing, li.SubItems[3].Text);
                        ws.get_Range("D" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    }
                    ws.get_Range("E" + rowid).set_Value(Type.Missing, li.SubItems[4].Text);
                    ws.get_Range("e" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("F" + rowid).set_Value(Type.Missing, li.SubItems[5].Text);
                    ws.get_Range("f" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("G" + rowid).set_Value(Type.Missing, li.SubItems[6].Text);
                    ws.get_Range("g" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("h" + rowid).set_Value(Type.Missing, li.SubItems[7].Text);
                    ws.get_Range("h" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("i" + rowid).set_Value(Type.Missing, li.SubItems[8].Text);
                    ws.get_Range("i" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
                    try
                    {
                        ws.get_Range("j" + rowid).set_Value(Type.Missing, DateTime.Parse(li.SubItems[9].Text).ToOADate());
                        ws.get_Range("j" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    }
                    catch (Exception) { }
                    ws.get_Range("k" + rowid).set_Value(Type.Missing, li.SubItems[10].Text);
                    ws.get_Range("k" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("l" + rowid).set_Value(Type.Missing, li.SubItems[11].Text);
                    ws.get_Range("l" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("m" + rowid).set_Value(Type.Missing, li.SubItems[12].Text);
                    ws.get_Range("m" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    ws.get_Range("n" + rowid).set_Value(Type.Missing, li.SubItems[13].Text);
                    ws.get_Range("n" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
                    if (li.SubItems[13].Tag != null && li.SubItems[13].Tag.ToString().Equals("LATE"))
                        ws.get_Range("n" + rowid).Font.Underline = true;
                    ws.get_Range("o" + rowid).set_Value(Type.Missing, li.SubItems[14].Text);
                    ws.get_Range("o" + rowid).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

                    for (int ii = (int)'A'; ii <= (int)'O'; ii++)
                        ws.get_Range(("" + (char)ii).ToString() + rowid).EntireColumn.AutoFit();

                    if (!printPreviewWindow)
                    {
                        range = "G" + rowid + ":J" + rowid;

                        ws.get_Range(range).Font.Color = System.Drawing.ColorTranslator.ToOle(li.SubItems[6].ForeColor);
                        ws.get_Range(range).Font.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                        if (li.SubItems[6].Text.Length > 0)
                            ws.get_Range(range).Interior.Color = System.Drawing.ColorTranslator.ToOle(li.SubItems[6].BackColor);
                        ws.get_Range(range).Interior.Pattern = Excel.XlPattern.xlPatternSolid;
                    }
                    if (li.SubItems[7].Tag != null && li.SubItems[7].Tag.ToString().Equals("remli"))
                    {
                        range = "G" + rowid + ":J" + rowid;
                        ws.get_Range(range).Font.Italic = true;
                        ws.get_Range(range).Font.Underline = true;
                        ws.get_Range(range).Font.Color = System.Drawing.ColorTranslator.ToOle(Color.Black);
                        ws.get_Range(range).Interior.Color = System.Drawing.ColorTranslator.ToOle(Color.White);
                        //ws.get_Range(range).Interior.Pattern = Excel.XlPattern.xlPatternSolid;
                    }

                    rowid++;
                }
                ws.get_Range("A5").EntireColumn.NumberFormat = Program.SystemSDFormat;
                ws.get_Range("B5").EntireColumn.NumberFormat = Program.SystemSDFormat;
                ws.get_Range("C5").EntireColumn.NumberFormat = Program.SystemSDFormat;
                ws.get_Range("J5").EntireColumn.NumberFormat = Program.SystemSDFormat;

                ws.get_Range("a5:o" + (rowid+1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                ws.get_Range("a5:o" + (rowid + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;
                ws.get_Range("a5:o" + (rowid + 1)).Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                ws.get_Range("a5:o" + (rowid + 1)).Borders.Color = Excel.XlRgbColor.rgbBlack;

                int backupRowId = rowid;
                range = "A5:O" + rowid;
                //ws.get_Range(range).Font.Size = 15;
                range = "A" + rowid + ":D" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "TOTAL DR.");
                ws.get_Range("E" + rowid).set_Value(Type.Missing, "=SUM(E5:E" + (rowid - 1) + ")");
                ws.get_Range("E" + rowid).NumberFormat = "0.00";
                ws.get_Range("E" + rowid).Font.Bold = true;
                ws.get_Range("F" + rowid).set_Value(Type.Missing, "=SUM(F1:F" + (rowid - 1) + ")");
                ws.get_Range("F" + rowid).NumberFormat = "0.00";
                ws.get_Range("F" + rowid).Font.Bold = true;
                ws.get_Range("A" + rowid + ":F" + rowid).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlThin,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);

                range = "H" + rowid;//"G" + rowid + ":H" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "=SUM(H1:H" + (rowid - 1) + ")");
                ws.get_Range(range).NumberFormat = "0.00";
                ws.get_Range("G" + rowid + ":O" + rowid).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlThin,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);


                range = "I" + rowid + ":M" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "TOTAL CR.");
                ws.get_Range("N" + rowid).set_Value(Type.Missing, "=SUM(N1:N" + (rowid - 1) + ")");
                ws.get_Range("N" + rowid).NumberFormat = "0.00";
                ws.get_Range("O" + rowid).set_Value(Type.Missing, "=SUM(O1:O" + (rowid - 1) + ")");
                ws.get_Range("O" + rowid).NumberFormat = "0.00";

                //ws.get_Range("A" + rowid + ":O" + rowid).Font.Size = 12;
                ws.get_Range("A" + rowid + ":O" + rowid).Font.Bold = true;

                rowid++;
                ws.get_Range("A" + rowid + ":O" + rowid).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlThin,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);

                range = "A" + rowid + ":E" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "AMT DUE RS.");
                range = "F" + rowid + ":G" + rowid;// +":H" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "=F" + (rowid - 1) + "-H" + (rowid - 1));
                ws.get_Range(range).NumberFormat = "0.00";

                range = "H" + rowid + ":M" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "INTEREST AMT RS.");
                range = "N" + rowid + ":O" + rowid;
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, "=N" + (rowid - 1) + "+O" + (rowid - 1));
                ws.get_Range(range).NumberFormat = "0.00";

                //ws.get_Range("A" + rowid + ":O" + rowid).Font.Size = 12;
                ws.get_Range("A" + rowid + ":O" + rowid).Font.Bold = true;

                rowid++;
                range = "A" + rowid + ":O" + (rowid + 1);
                ws.get_Range(range).Merge();
                ws.get_Range(range).set_Value(Type.Missing, Datastore.current.FooText);
                //ws.get_Range(range).Font.Size = 12;
                ws.get_Range(range).Font.Bold = true;

                range = "A" + backupRowId + ":O" + rowid;
                //ws.get_Range(range).Font.Size = 18;
                ws.get_Range(range).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range(range).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                //ws.get_Range("e1").EntireColumn.AutoFit();
                ws.get_Range("D1").EntireColumn.ColumnWidth = 15;
                //ws.get_Range("F1:G1:N1:O1").EntireColumn.AutoFit();
                ws.get_Range("A1:O"+(rowid+1)).EntireColumn.AutoFit();
                ws.get_Range("A1:O" + (rowid + 1)).EntireRow.AutoFit();
                //ws.get_Range("A1:O" + rowid).Font.Name = "Arial";                
                //ws.get_Range("A1:O" + rowid).Font.Size = 9;
                ws.get_Range("a3:o3").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ws.get_Range("a3:o3").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                //ws.get_Range("D3").EntireColumn.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

                ws.get_Range("a1:o" + (rowid + 1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlThin,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);
                //ws.get_Range("a1:o" + (rowid + 1)).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                //ws.get_Range("a1:o" + (rowid + 1)).Borders.Weight = Excel.XlBorderWeight.xlThin;
                //ws.get_Range("a1:o" + (rowid + 1)).Borders.ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic;
                //ws.get_Range("a1:o" + (rowid + 1)).Borders.Color = Excel.XlRgbColor.rgbBlack;

                ws.get_Range("a1:b" + (rowid-1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlMedium,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);
                ws.get_Range("c1:f" + (rowid-1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlMedium,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);
                ws.get_Range("g1:j" + (rowid-1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlMedium,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);
                ws.get_Range("k1:o" + (rowid-1)).BorderAround(Excel.XlLineStyle.xlDouble,
                                                        Excel.XlBorderWeight.xlMedium,
                                                        Excel.XlColorIndex.xlColorIndexAutomatic,
                                                        Excel.XlRgbColor.rgbBlack);

                ws.get_Range("F1").EntireColumn.NumberFormat = "0.00";
                ws.get_Range("G1").EntireColumn.NumberFormat = "0.00";
                ws.get_Range("H1").EntireColumn.NumberFormat = "0.00";
                ws.get_Range("N1").EntireColumn.NumberFormat = "0.00";
                ws.get_Range("O1").EntireColumn.NumberFormat = "0.00";

                /*
                ws.get_Range("A1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("b1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("c1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("e1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("f1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("g1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("h1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("j1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("k1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("l1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("m1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("n1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("o1").EntireColumn.HorizontalAlignment =
                    Excel.XlHAlign.xlHAlignRight;

                ws.get_Range("d1").EntireColumn.HorizontalAlignment =
                    ws.get_Range("i1").EntireColumn.HorizontalAlignment =
                    Excel.XlHAlign.xlHAlignLeft;*/

                ws.PageSetup.PaperSize = Excel.XlPaperSize.xlPaperA4;
                ws.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;
                ws.PageSetup.Zoom = 100;
                ws.PageSetup.BottomMargin = 0.25;
                ws.PageSetup.LeftMargin = 0.25;
                ws.PageSetup.RightMargin = 0.25;
                ws.PageSetup.TopMargin = 0.25;
                ws.PageSetup.CenterHorizontally = true;
                ws.PageSetup.CenterVertically = false;
                ws.PageSetup.Zoom = false;
                ws.PageSetup.FitToPagesWide = 1;
                ws.PageSetup.FitToPagesTall = 10;

                if (printPreviewWindow)
                {
                    if (printDirectly)
                    {
                        ws.PrintOutEx();
                    }
                    else
                    {
                        app.Visible = true;
                        bool userDidntCancel = app.Dialogs[Excel.XlBuiltInDialog.xlDialogPrintPreview].Show(
                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    }
                    wb.Close(false);
                }
                else
                {
                    wb.Close(true, filename);
                }


                GC.Collect();
                GC.WaitForPendingFinalizers();

                app.Quit();

                app.Quit();
                app = null;
                if (printDirectly == false && printPreviewWindow == false)
                    sucExportExcel();
                printPreviewWindow = printDirectly = false;
            }
            catch (Exception excep)
            {
                String err = "Unable to perform threadEXP_monthlyreport operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            closeWaitingDialog();
        }

        private void sucExportExcel()
        {
            try
            {
                Action a = () =>
                {
                    MessageBox.Show(this, "Successfully exported.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                String err = "Unable to perform sucExportExcel operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private ListView getListView()
        {
            ListView lvi = new ListView();
            try
            {
                Action a = () =>
                {
                    foreach (ListViewItem li in lvUser.Items)
                    {
                        lvi.Items.Add(li.Clone() as ListViewItem);
                    }
                };
                if (this.InvokeRequired)
                    Invoke(a);
                else
                    a();
            }
            catch (Exception excep)
            {
                String err = "Unable to perform getListView_monthlyreport operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return lvi;
        }

        private void lvUser_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lvUser.SelectedItems.Count > 0)
                {
                    int mx = Cursor.Position.X;
                    int my = Cursor.Position.Y;
                    int x = lvUser.Items[0].SubItems[6].Bounds.Location.X;
                    if (mx < x)
                    {
                        Payment p = lvUser.SelectedItems[0].Tag as Payment;
                        if (p != null)
                        {
                            NewPayment newp = new NewPayment();
                            newp.setEditMode(p.ID.ToString());
                            newp.ShowDialog(this);
                            btnProcess_Click(sender, e);
                        }
                    }
                    else if (mx > x)
                    {
                        Payment p = lvUser.SelectedItems[0].SubItems[6].Tag as Payment;
                        if (p != null)
                        {
                            NewPayment newp = new NewPayment();
                            newp.setEditMode(p.ID.ToString());
                            newp.ShowDialog(this);
                            btnProcess_Click(sender, e);
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                String err = "Unable to perform OnTheGoEdit_monthlyreport operation.";
                Log.output(err, excep);
                MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvUser_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu cm = new ContextMenu();
                if (lvUser.SelectedItems.Count == 1)
                {
                    if (lvUser.SelectedItems.Count > 0)
                    {
                        int mx = Cursor.Position.X;
                        int my = Cursor.Position.Y;
                        int x = lvUser.Items[0].SubItems[6].Bounds.Location.X;
                        if (mx < x)
                        {
                            //debit
                            //Payment p = lvUser.SelectedItems[0].Tag as Payment;
                            cm.MenuItems.Add(new MenuItem("&Delete selected 'Debit' entry", lvUser_RightClickAction));
                        }
                        else if (mx > x)
                        {
                            //credit
                            //Payment p = lvUser.SelectedItems[0].SubItems[6].Tag as Payment;
                            cm.MenuItems.Add(new MenuItem("&Delete selected 'Credit' entry", lvUser_RightClickAction));
                        }
                    }
                }
                cm.MenuItems.Add(new MenuItem("&Switch to full view", lvUser_RightClickAction));
                cm.Show(sender as Control, e.Location);
            }
        }

        private void lvUser_RightClickAction(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            if (mi.Text.StartsWith("&Switch"))
            {
                Main.simpleReport = !Main.simpleReport;
                btnProcess_Click(sender, e);
            }
            else if (mi.Text.StartsWith("&Delete selected"))
            {
                if (lvUser.SelectedItems.Count > 0)
                {
                    int mx = Cursor.Position.X;
                    int my = Cursor.Position.Y;
                    int x = lvUser.Items[0].SubItems[6].Bounds.Location.X;
                    if (mx < x)
                    {
                        //debit
                        if (MessageBox.Show(this, "Are you sure to delete selected 'Debit' entry ?", "Delete Debit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Payment p = lvUser.SelectedItems[0].Tag as Payment;
                            if (Datastore.current.Payments.RemoveAll(xx=>(xx.ID==p.ID))>0)
                            {
                                MessageBox.Show(this, "Successfully deleted.", "Debit Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnProcess_Click(sender, e);
                            }
                            else
                            {
                                MessageBox.Show(this, "Can't delete debit entry, please try again.", "Debit Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else if (mx > x)
                    {
                        //credit
                        if (MessageBox.Show(this, "Are you sure to delete selected 'Credit' entry ?", "Delete Debit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Payment p = lvUser.SelectedItems[0].SubItems[6].Tag as Payment;
                            if (Datastore.current.Payments.RemoveAll(xx => (xx.ID == p.ID))>0)
                            {
                                MessageBox.Show(this, "Successfully deleted.", "Credit Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                btnProcess_Click(sender, e);
                            }
                            else
                            {
                                MessageBox.Show(this, "Can't delete credit entry, please try again.", "Credit Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void GeneralReport_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
            }
            catch (Exception) { }
        }

        private void lvUser_MouseClick(object sender, MouseEventArgs e)
        {
            
        }
    }
}
