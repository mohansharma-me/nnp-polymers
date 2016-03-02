using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.report1
{
    public class AutoRow
    {
        private int _month, _year;
        private DateTime nextDate, lastDate;
        private long _clientId = 0;
        private long _currentDebitId = 0, _currentCreditId = 0;
        private bool syncStarted = false, autoOBConsidered = false;
        private SQLiteDataReader dataReader=null;
        private List<Payment> creditCollection=null;
        private int currentCreditIndex = -1, lastCreditIndex4PDebit = -1, lastCreditIndex4STDebit = -1;
        private Payment currentDebit = null;
        private List<Color> colors = null;
        private bool _isReport2 = false;

        public delegate void PriorityAdjusterDelegate();
        public delegate void GeneralAdjusterDelegate();
        public event PriorityAdjusterDelegate PriorityAdjuster;
        public event GeneralAdjusterDelegate GeneralAdjuster;

        public bool Report2
        {
            get { return _isReport2; }
            set { _isReport2 = value; }
        }

        public Client currentClient = null;

        public bool skipCurrentDebit = false, priorityDebitsOnly = false;

        public AutoRow(long clientId, int month, int year, ref List<Color> colors)
        {
            _clientId = clientId;
            _month = month;
            _year = year;

            this.colors = colors;

            currentClient = Job.mainForm.fClients.getCurrentClient();

            lastDate = new DateTime(year, month, 1, 12, 0, 0);
            nextDate = lastDate.AddMonths(1);
        }

        public int startSync(bool priorityDebitsOnly)
        {
            try
            {
                this.priorityDebitsOnly = priorityDebitsOnly;
                syncStarted = false;
                String q = "select * from payment where payment_client_id=@client and lower(payment_mode)='debit' and payment_date<@date order by payment_date, payment_id asc";
                if (priorityDebitsOnly)
                {
                    q = "select * from payment,debit_priorities where (lower(payment_type) LIKE lower(dp_type) and dp_special=0) and payment_client_id=@client and lower(payment_mode)='debit' and payment_date<@date order by payment_date, payment_id asc";
                }
                dataReader = Job.DB.executeReader(q, new SQLiteParameter[] { new SQLiteParameter("@date", nextDate), new SQLiteParameter("@client", _clientId) });
                currentCreditIndex = -1;
                currentDebit = null;
                if (dataReader == null)
                {
                    throw new Exception("Unable to fetch data from payment area.");
                }
                else
                {
                    syncStarted = true;
                    return dataReader.RecordsAffected;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return -1;
        }

        public void stopSync()
        {
            syncStarted = false;
            dataReader.Close();
            dataReader = null;
        }

        public Payment currentCredit()
        {
            if (creditCollection != null && currentCreditIndex < creditCollection.Count && currentCreditIndex > -1)
                return creditCollection[currentCreditIndex];
            return null;
        }

        public Payment currentPriorityCredit()
        {
            if (creditCollection != null && lastCreditIndex4PDebit < creditCollection.Count && lastCreditIndex4PDebit > -1)
                return creditCollection[lastCreditIndex4PDebit];
            return null;
        }

        public Payment currentSpecialCredit()
        {
            if (creditCollection != null && lastCreditIndex4STDebit < creditCollection.Count && lastCreditIndex4STDebit > -1)
                return creditCollection[lastCreditIndex4STDebit];
            return null;
        }

        public int totalCreditsAvailable
        {
            get
            {
                return creditCollection == null ? 0 : creditCollection.Count;
            }
        }

        public Payment nextCredit()
        {
            if (creditCollection == null)
            {
                initCreditCollection();
            }

            if (creditCollection.Count > 0 && currentCreditIndex == -1)
                currentCreditIndex = 0;

            if (currentCreditIndex < creditCollection.Count && currentCreditIndex>-1)
            {
                Payment p = creditCollection[currentCreditIndex];
                bool isItST = Job.PrioritiesAndTypes.find(p.type, true);
                if (!isItST)
                {
                    if (p.remainBalance != 0)
                    {
                        return p;
                    }
                    else
                    {
                        try
                        {
                            currentCreditIndex++;
                            return nextCredit();
                        }
                        catch (Exception) { return null; }
                    }
                }
                else
                {
                    currentCreditIndex++;
                    return nextCredit();
                }
            }
            else
            {
                return null;
            }
        }

        public Payment nextPriorityCredit(DateTime date)
        {
            date = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
            if (creditCollection == null)
            {
                initCreditCollection();
            }

            if (creditCollection.Count > 0 && currentCreditIndex == -1)
                currentCreditIndex = 0;

            if (lastCreditIndex4PDebit == -1 && creditCollection.Count > 0)
                lastCreditIndex4PDebit = currentCreditIndex;

            if (lastCreditIndex4PDebit < creditCollection.Count && lastCreditIndex4PDebit > -1)
            {
                //Payment p = creditCollection[currentCreditIndex];
                int index = creditCollection.FindIndex(lastCreditIndex4PDebit,x =>
                {
                    if (x.isSpecialType) return false;
                    DateTime dt = new DateTime(x.date.Year, x.date.Month, x.date.Day, 12, 0, 0);
                    return dt >= date;
                });
                if (index > -1)
                {
                    lastCreditIndex4PDebit = index;
                    Payment p = creditCollection[lastCreditIndex4PDebit];
                    
                    if (p != null && p.remainBalance != 0)
                    {
                        return p;
                    }
                    else if (p != null)
                    {
                        try
                        {
                            lastCreditIndex4PDebit++;
                            if (lastCreditIndex4PDebit < creditCollection.Count)
                                return nextPriorityCredit(date);
                            else
                                return null;
                        }
                        catch (Exception) { return null; }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public Payment nextSpecialCredit(String type)
        {
            if (creditCollection == null)
            {
                initCreditCollection();
            }

            if (creditCollection.Count > 0 && currentCreditIndex == -1)
                currentCreditIndex = 0;

            if (lastCreditIndex4STDebit == -1 && creditCollection.Count > 0)
                lastCreditIndex4STDebit = 0;

            if (lastCreditIndex4STDebit < creditCollection.Count && lastCreditIndex4STDebit > -1)
            {
                //Payment p = creditCollection[currentCreditIndex];
                int index = creditCollection.FindIndex(lastCreditIndex4STDebit, x =>
                {
                    try
                    {
                        //Console.Write("LCI4ST:" + lastCreditIndex4STDebit);
                        return x.type.ToLower().Trim().Equals(type.ToLower().Trim());
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                });
                Console.WriteLine(", IndexFound:" + index + ", Count:" + creditCollection.Count);
                if (index > -1)
                {
                    lastCreditIndex4STDebit = index;
                    Payment p = creditCollection[lastCreditIndex4STDebit];

                    if (p != null && p.remainBalance != 0)
                    {
                        return p;
                    }
                    else if (p != null)
                    {
                        try
                        {
                            lastCreditIndex4STDebit++;
                            if (lastCreditIndex4STDebit < creditCollection.Count)
                                return nextSpecialCredit(type);
                            else
                                return null;
                        }
                        catch (Exception) { return null; }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        private void initCreditCollection()
        {
            try
            {
                SQLiteDataReader dr = Job.DB.executeReader("select * from payment where payment_client_id=@client and lower(payment_mode)='credit' and payment_date<@date order by payment_date, payment_id asc", new SQLiteParameter[] { new SQLiteParameter("@date", nextDate), new SQLiteParameter("@client", _clientId) });
                creditCollection = new List<Payment>();
                if (dr != null)
                {
                    bool initAdded = false;
                    Payment initPayment=null;
                    Client client = currentClient;
                    if (client != null)
                    {
                        if (client.obalance_type == Client.OpeningBalanceType.Credit && client.obalance > 0)
                        {
                            initPayment = new Payment(0);
                            initPayment.SetInitMode = true;

                            initPayment.client_id = client.id;
                            initPayment.amount = client.obalance;
                            initPayment.grade = Job.Grades.getGrade(0, false);
                            initPayment.invoice = "Opening Balance";
                            initPayment.mode = Payment.PaymentMode.Credit;
                            initPayment.mt = 0;
                            initPayment.particulars = "";
                            initPayment.remainBalance = client.obalance;
                            initPayment.type = "Sale";

                            initPayment.SetInitMode = false;
                        }
                    }

                    int colorIndex = 0;
                    Random rand = new Random(colorIndex);
                    while (dr.Read())
                    {
                        Payment cp = null;
                        Job.Payments.initPaymentObject(ref cp, ref dr);
                        if (cp != null)
                        {
                            colorIndex += 4;
                            if (colorIndex >= colors.Count - 1)
                            {
                                colorIndex = 0;
                            }
                            colorIndex = rand.Next(colorIndex, colors.Count - 1);
                            if (!initAdded && initPayment != null)
                            {
                                initPayment.SetInitMode = true;
                                initPayment.date = new DateTime(cp.date.Year, 4, 1, 12, 0, 0);
                                initPayment.SetInitMode = false;
                                initPayment.color = colors[colorIndex];
                                colorIndex = rand.Next(colors.Count - 1);
                                creditCollection.Add(initPayment);
                                initAdded = true;
                            }
                            cp.color = colors[colorIndex];
                            creditCollection.Add(cp);
                        }
                    }

                    if (creditCollection.Count == 0 && !initAdded && initPayment!=null)
                    {
                        initPayment.SetInitMode = true;
                        initPayment.date = new DateTime(_year, 4, 1, 12, 0, 0);
                        initPayment.SetInitMode = false;
                        creditCollection.Add(initPayment);
                        initAdded = true;
                    }
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Job.Log("InitcreditCllection", ex);
            }
        }

        public bool hasNext()
        {
            if (priorityDebitsOnly)
                return (dataReader != null && dataReader.HasRows);
            return (dataReader != null && (dataReader.HasRows || (currentDebit==null && !autoOBConsidered && currentClient.obalance > 0 && currentClient.obalance_type == Client.OpeningBalanceType.Debit)));
        }

        public List<Row> getRemainingPayments()
        {
            List<Row> rows = new List<Row>();
            rows.Add(null);
            if (creditCollection == null)
            {
                if (currentCreditIndex > -1)
                    return rows;
                else
                    initCreditCollection();
                return getRemainingPayments();
            }
            foreach (Payment credit in creditCollection)
            {
                if (credit.remainBalance != 0)
                {
                    Payment _c = credit;
                    rows.Add(new Row(ref _c));
                }
            }
            return rows;
        }

        public Row getRemainingDebitRow()
        {
            AutoRow ar=this;
            return new Row(ref currentDebit, _month, _year, ref ar, true);
        }

        public Row next(bool skipCurrent=false)
        {
            try
            {
                #region old-code
                /*if (!syncStarted || dataReader==null || (dataReader!=null && !dataReader.Read())) return null;
                AutoRow ar = this;
                if (currentDebit == null)
                    Job.Payments.initPaymentObject(ref currentDebit, ref dataReader);
                if (currentDebit.remainBalance == 0 || skipCurrent) // zero levana baaki or debit fully paid
                {
                    Job.Payments.initPaymentObject(ref currentDebit, ref dataReader);
                    return next();
                }
                else
                {
                    if (currentCreditIndex>-1 && (currentCreditIndex >= creditCollection.Count)) // check wheather credit is avail for this debit
                    {
                        return next(true);
                    }
                    else // same debit again for adjustment
                    {
                        Row r = new Row(ref currentDebit, _month, _year, ref ar);
                        return r;
                    }
                }*/
                #endregion

                // if flag to skip current debit is set than return next one
                if (skipCurrentDebit)
                {
                    skipCurrentDebit = false;
                    return next(true);
                }

                // declare return Row and init
                Row row = null;

                // check wheather database is alive or not
                if (!syncStarted || dataReader == null) return null;

                // init self-reference
                AutoRow ar = this;

                // if its first debit 
                bool isItFirstDebit = false;
                if (currentDebit == null)
                {
                    //Job.Payments.initPaymentObject(ref currentDebit, ref dataReader, false);

                    // init first debit from Client Opening Balance if its for debit mode
                    if (!priorityDebitsOnly && !autoOBConsidered)
                    {
                        Client client = currentClient;
                        if (client != null)
                        {
                            if (client.obalance_type == Client.OpeningBalanceType.Debit && client.obalance > 0)
                            {
                                currentDebit = new Payment(0);
                                currentDebit.SetInitMode = true;

                                currentDebit.client_id = client.id;
                                currentDebit.amount = client.obalance;
                                currentDebit.grade = Job.Grades.getGrade(0, false);
                                currentDebit.invoice = "Opening Balance";
                                currentDebit.mode = Payment.PaymentMode.Debit;
                                currentDebit.mt = 0;
                                currentDebit.particulars = "";
                                currentDebit.remainBalance = client.obalance;
                                currentDebit.type = "Sale";
                                DateTime mdate = Job.Payments.getMinimumDateOf(_clientId, Payment.PaymentMode.Debit);
                                currentDebit.date = new DateTime(mdate.Year, 4, 1, 12, 0, 0);
                                currentDebit.SetInitMode = false;
                            }
                            autoOBConsidered = true;
                        }
                    }
                    if (currentDebit == null)
                        getNextDebit(ref currentDebit, ref dataReader);
                    isItFirstDebit = true;
                }

                // check is it first debit
                bool initRow = false;
                if (isItFirstDebit)
                {
                    if (currentDebit == null) // first debit is not retrived or there is zero debits
                    {
                        // check is ther any debit exists in this client profile
                        // if yes than show error
                        // some how stop processing here
                        Console.WriteLine("E: No Debits");
                        return null;
                    }
                    else // its first debit
                    {
                        initRow = true;
                    }
                }
                else // not its not first debit
                {
                    // check skip debit flag
                    if (skipCurrent)
                    {
                        //Job.Payments.initPaymentObject(ref currentDebit, ref dataReader, false);
                        getNextDebit(ref currentDebit, ref dataReader);
                        if (currentDebit == null) // if there is zero debits
                        {
                            Console.WriteLine("E: No Debits.1");
                        }
                        else // yes new debit is there
                        {
                            initRow = true;
                        }
                    }
                }


                // check wheather currentDebit is not null
                if (currentDebit == null) return null;

                // init row if its flag is set from  [first debit logic]
                if (initRow)
                {
                    row = new Row(ref currentDebit, _month, _year, ref ar);
                    return row;
                }

                // set flag for current debit is fully paid or not
                bool isDebitPaid = currentDebit.remainBalance == 0;

                // if debit is paid then return next debit
                if (isDebitPaid)
                {
                    return next(true);
                } 
                else if(!initRow)
                {
                    // debit isn't paid so find out why not paid

                    // if no credits available then return next debit
                    // get current credit status
                    Payment curCredit = null;

                    if (currentDebit.isPriority)
                    {
                        curCredit = currentPriorityCredit();
                    }
                    else
                    {
                        //isItST = Job.PrioritiesAndTypes.find(_debit.type, true);
                        if (currentDebit.isSpecialType)
                        {
                            curCredit = currentSpecialCredit();
                        }
                        else
                        {
                            curCredit = currentCredit();
                        }
                    }

                creditAnalysis:
                    if (curCredit == null) // all credits shared
                    {
                        if (totalCreditsAvailable == 0) // no credits available
                        {
                            return next(true);
                        }
                        else // credits available but may not inited
                        {
                            int index4 = creditCollection.Count;
                            if (currentDebit.isPriority)
                            {
                                curCredit = nextPriorityCredit(currentDebit.date);
                                index4 = lastCreditIndex4PDebit;
                            }
                            else
                            {
                                if (currentDebit.isSpecialType)
                                {
                                    curCredit = nextSpecialCredit(currentDebit.type);
                                    index4 = lastCreditIndex4STDebit;
                                }
                                else
                                {
                                    curCredit = nextCredit();
                                    index4 = currentCreditIndex;
                                }
                            }
                            
                            if (index4 < creditCollection.Count)
                                goto creditAnalysis;
                            else
                                return next(true);
                        }
                        //return next(true);
                    }
                    else // current credit is there, so return same debit
                    {
                        // and put all code related to payment adjustment in Row Class
                        if (row == null) // only if row isn't already inited
                        {
                            row = new Row(ref currentDebit, _month, _year, ref ar);
                        }
                    }
                }

                return row;

                #region ThisCodeMayBelongToRowClass
                /*
                // else debit is not paid, go for to validate why its not paid

                // get current credit status
                Payment curCredit = currentCredit();

                // verify current credit,check whether credit exists or not
                if (curCredit == null)
                {
                    // check whether is it last credit or credit isn't inited
                    if (currentCreditIndex == -1) // not inited or zero credits
                    {
                        if (creditCollection != null && creditCollection.Count == 0) // zero credits
                        {
                            // set curCredit null for in next section it will RETURN NEXT DEBIT...
                            return next(true);
                        }
                        else if(currentCreditIndex==-1 && creditCollection==null) // not inited
                        {
                            curCredit = nextCredit();
                        }
                    }
                    else // last credit [all credits are used]
                    {
                        //set curcredit null
                        return next(true);
                    }
                }



                *
                 * MAY BE NOT REQUIRED - ITS FOR ROW CLASS CONSTRUCTOR
                // if curCredit is null than there was zero credits or no more credits available
                if (curCredit == null)
                {
                    // for maintaining remaining balance from current debit *
                    if (currentDebit.remainBalance != currentDebit.debit_amount) // if some remaining debit amt is not paid
                    {
                        // prepare row for that remaining debit amount
                        // and return that row
                    }
                    else // if current debit is fully not paid
                    {
                        // return next debit
                        return next(true);
                    }
                } 
                else // if credit exists 
                {
                    if (curCredit.remainBalance > 0) // there is balance in credit
                    {

                    }
                    else
                    {

                    }
                }*
                

                // check whether row is inited or not
                if (row == null) // already inited
                {
                    row = new Row(ref currentDebit, _month, _year, ref ar);
                }

                return row;
                */
                #endregion

            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public void adjustNow()
        {
            PriorityAdjuster();
            GeneralAdjuster();
        }

        private bool getNextDebit(ref Payment currentDebit, ref SQLiteDataReader dataReader, bool dontRead=false)
        {
            Job.Payments.initPaymentObject(ref currentDebit, ref dataReader, dontRead);
            if (currentDebit != null)
            {
                bool isItP = currentDebit.isPriority;//Job.PrioritiesAndTypes.find(currentDebit.type, false);
                if (priorityDebitsOnly)
                {
                    if (!isItP)
                        return getNextDebit(ref currentDebit, ref dataReader, dontRead);
                    else
                        return false;
                }
                else
                {
                    if (isItP)
                        return getNextDebit(ref currentDebit, ref dataReader, dontRead);
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
