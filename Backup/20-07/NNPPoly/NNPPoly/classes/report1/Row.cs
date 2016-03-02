using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NNPPoly.classes.report1
{
    public class Row
    {
        private int _month, _year;
        private Payment _debit, _credit;
        private double creditAdjusted = 0;
        private AutoRow autoRow;

        private bool showRemDebit = false, showRemCredit = false;
        private bool isItP = false, isItST = false;

        public Row(ref Payment debit, int month, int year, ref AutoRow ar, bool showRemDebit=false)
        {
            _debit = debit;
            _month = month;
            _year = year;
            this.showRemDebit = showRemDebit;
            autoRow = ar;
            isItP = _debit.isPriority;
            if (!isItP)
                isItST = _debit.isSpecialType;
            perform();
        }

        public Row(ref Payment credit)
        {
            rowtype = RowType.RemainingPayments;
            _credit = credit;
        }

        public void perform()
        {
            if (showRemDebit)
            {
                rowtype = RowType.RemainingDebitBalanceRow;
            }
            else
            {
                processDebit();

                if (_credit == null)
                {
                    rowtype = _debit.isConsidered ? RowType.RemainingDebitBalanceRow : RowType.NotAdjusted;
                    if (!_debit.isConsidered)
                        _debit.isConsidered = true;
                }
                else
                {
                    rowtype = _debit.isConsidered ? RowType.PartialCreditAdjustment : RowType.Adjusted;
                    if (!_debit.isConsidered)
                        _debit.isConsidered = true;
                }
            }
        }
        
        private void processDebit()
        {
            //isItP = Job.PrioritiesAndTypes.find(_debit.type, false);
            if (isItP)
            {
                _credit = autoRow.nextPriorityCredit(_debit.date);
            }
            else
            {
                //isItST = Job.PrioritiesAndTypes.find(_debit.type, true);
                if (isItST)
                {
                    _credit = autoRow.nextSpecialCredit(_debit.type);
                }
                else
                {
                    _credit = autoRow.nextCredit();
                }
            }
            if (_credit == null)
            {
                if (isItP || isItST)
                {
                    //autoRow.next(true);
                    autoRow.skipCurrentDebit = true;
                }
                else
                {

                }
            }
            else
            {
                if (_credit.remainBalance > 0)
                {
                    creditAdjusted = adjust();
                }
            }
        }

        private double adjust(Payment d=null, Payment c=null)
        {
            if (d == null) d = _debit;
            if (c == null) c = _credit;

            double remb = c.remainBalance;
            double reqb = d.remainBalance;

            double takenAmt = 0;
            if (remb == reqb)
            {
                d.remainBalance = 0;
                c.remainBalance = 0;
                takenAmt = remb;
            }
            else if (reqb > remb)
            {
                d.remainBalance -= remb;
                c.remainBalance = 0;
                takenAmt = remb;
            }
            else if (reqb < remb)
            {
                d.remainBalance = 0;
                c.remainBalance = remb - reqb;
                takenAmt = reqb;
            }
            else
            {
                Console.WriteLine("\n\n\nHello!!!\n\n\n");
            }
            return Job.Functions.RoundDouble(takenAmt);
        }

        public RowType rowtype { get; set; }

        public DateTime? newdate
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                DateTime newDate = new DateTime(_year, _month, 1, 12, 0, 0);
                newDate = newDate.AddMonths(1);

                bool setDate = false;
                bool validateSaleType = false;

                // if no credit is adjusted means that its fully remain
                if (_credit == null && _debit.debit_amount == _debit.remainBalance)
                {
                    setDate = true;
                    validateSaleType = true;
                }
                else if (rowtype==RowType.RemainingDebitBalanceRow)
                {
                    setDate = true;
                }

                if (validateSaleType && setDate)
                {
                    if (isSale())
                    {
                        double nDateMdDate = newDate.Subtract(_debit.date).TotalDays;
                        // if its sale type debit
                        if (nDateMdDate < Job.mainForm.fClients.getCurrentClient().lessdays)
                        {
                            // if debit's lessdays is already counted than set new date.
                            setDate = false;
                        }
                        else
                        {
                            // less days of debit is not counted yet so dont set new date.
                            setDate = true;
                        }
                    }
                    else
                    {
                        // if it's not sale type debit
                    }
                }

                if (setDate)
                    return newDate;
                else
                    return null;
            }
            
        }

        private bool isSale()
        {
            return _debit.type.Trim().ToLower().Equals("sale");
        }

        public DateTime? lastdate 
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                DateTime lastDate = new DateTime(_year, _month, 1, 12, 0, 0);
                bool setDate = false, validateSale=false;

                if (_credit == null && _debit.date < lastDate)
                {
                    setDate = true;
                    if (_debit.date.Month == _month && _debit.date.Year == _year)
                        validateSale = false;
                    else
                        validateSale = true;
                }
                else if (_credit != null && _debit.date < _credit.date && (_debit.date.Month != _credit.date.Month || _debit.date.Year != _credit.date.Year))
                {
                    lastDate = new DateTime(_credit.date.Year, _credit.date.Month, 1, 12, 0, 0);
                    if ( _debit.date < lastDate)
                    {
                        validateSale = setDate = true;
                    }
                }

                if (validateSale && setDate)
                {
                    if (isSale())
                    {
                        double nDateMdDate = lastDate.Subtract(_debit.date).TotalDays;

                        // if its sale type debit
                        if (nDateMdDate >= Job.mainForm.fClients.getCurrentClient().lessdays)
                        {
                            // if debit's lessdays is already counted than set new date.
                            setDate = true;
                        }
                        else
                        {
                            // less days of debit is not counted yet so dont set new date.
                            setDate = false;
                        }
                    }
                    else
                    {
                        // if it's not sale type debit
                    }
                }

                if (setDate)
                    return lastDate;
                else
                    return null;
            } 
        }

        public Payment debit { get { return _debit; } set { _debit = value; } }
        public Payment credit { get { return _credit; } set { _credit = value; } }

        public String debit_invoice
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return "";
                if (rowtype == RowType.RemainingDebitBalanceRow)
                {
                    return ((double)(debit.remainBalance)).ToString("0.00");
                }
                else
                {
                    return debit.invoice;
                }
            }
            set
            {
                if (rowtype != RowType.RemainingDebitBalanceRow)
                    debit.invoice = value;
            }
        }

        public object debit_date
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                if (rowtype == RowType.RemainingDebitBalanceRow)
                    return null;
                return debit.date;
            }
            set
            {
                if (rowtype != RowType.RemainingDebitBalanceRow)
                    debit.date = (DateTime)value;
            }
        }

        public object debit_amount
        {
            get 
            {
                if (rowtype == RowType.RemainingPayments) return null;
                if (rowtype == RowType.RemainingDebitBalanceRow)
                    return null;
                return debit.debit_amount;
            }
            set {
                if (rowtype != RowType.RemainingDebitBalanceRow)
                    debit.debit_amount = (double)value;
            }
        }

        public object debit_mt
        {
            get 
            {
                if (rowtype == RowType.RemainingPayments) return null;
                if (rowtype == RowType.RemainingDebitBalanceRow)
                    return null; 
                return debit.mt;
            }
            set
            {
                if (rowtype != RowType.RemainingDebitBalanceRow)
                debit.mt = (double)value; 
            }
        }

        public String credit_amt
        {
            get
            {
                if (_credit == null) return "";
                return _credit.credit_amount.ToString();
            }
        }

        public String credt_adjusted_amt
        {
            get
            {
                if (_credit == null) return "";
                if (rowtype == RowType.RemainingPayments)
                {
                    return Job.Functions.RoundDouble(_credit.remainBalance).ToString();
                }
                return taken_amount == null ? "" : taken_amount.Value.ToString();
            }
        }

        public String credit_invoice
        {
            get
            {
                if (_credit == null) return "";
                return _credit.invoice;
            }
        }

        public String credit_date
        {
            get
            {
                if (_credit == null) return "";
                return _credit.date.ToShortDateString();
            }
        }

        public double? taken_amount
        {
            get 
            {
                if (rowtype == RowType.RemainingPayments)
                {
                    if (_credit != null)
                    {
                        return Job.Functions.RoundDouble(_credit.remainBalance);
                    }
                    return null;
                }
                if (_credit == null || rowtype == RowType.RemainingDebitBalanceRow)
                    return null;
                return creditAdjusted; 
            }
        }

        public double? totaldays 
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                double? ret = null;
                DateTime? newDate = newdate;
                DateTime? lastDate = lastdate;

                bool validateHolidays = false;
                DateTime fromDate = DateTime.Today, toDate = DateTime.Today;

                if (newDate != null && lastDate != null)
                {
                    ret = newDate.Value.Subtract(lastDate.Value).TotalDays;
                    fromDate = lastDate.Value;
                    toDate = newDate.Value;
                    validateHolidays = true;
                }
                else if (newDate == null && lastDate != null)
                {
                    if (_credit != null)
                    {
                        ret = _credit.date.Subtract(lastDate.Value).TotalDays;

                        fromDate = lastDate.Value;
                        toDate = _credit.date;
                        validateHolidays = true;
                    }
                }
                else if (newDate != null && lastDate == null)
                {
                    ret = newDate.Value.Subtract(_debit.date).TotalDays;
                    fromDate = _debit.date;
                    toDate = newDate.Value;
                    validateHolidays = true;
                }
                else if (newDate == null && lastDate == null)
                {
                    if (_credit != null)
                    {
                        ret = _credit.date.Subtract(_debit.date).TotalDays;

                        fromDate = _debit.date;
                        toDate = _credit.date;
                        validateHolidays = true;
                    }
                }

                if (autoRow.Report2)
                {
                    return ret;
                }

                if (isSale() || (!isItP && !isSale()))
                {
                    if (rowtype == RowType.RemainingDebitBalanceRow)
                    {
                        if (ret < Job.mainForm.fClients.getCurrentClient().lessdays)
                        {
                            return null;
                        }
                    }
                }
                else if (isItP)
                {
                    if (_credit != null && ret != null && ret.Value >= -1 && ret.Value <= 1)
                    {
                        try
                        {
                            double _days = _credit.date.Subtract(_debit.date).TotalDays;

                            fromDate = _debit.date;
                            toDate = _credit.date;
                            validateHolidays = true;

                            if (_days > 1)
                            {
                                return ret;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            Job.Log("IsPriorityAndTotalDaysProblem: ",ex);
                        }
                    }
                    else if (rowtype == RowType.RemainingDebitBalanceRow)
                    {
                        if (!(newdate != null && lastdate != null))

                            return null;
                    }
                }

                /*if (validateHolidays && _credit != null && _debit != null)
                {
                    if (Job.Holidays.validateHolidays(fromDate, toDate))
                    {
                        return 0;
                    }
                }*/

                return ret;
            }
        }
        public double? lessdays 
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                if (autoRow.Report2) return null;
                try
                {
                    if (isSale())
                    {
                        if (_debit.id == 0) return 0;
                        if (lastdate != null) return 0;
                        if (totaldays != null && totaldays.Value < 0) return 0;
                        DateTime lastDate = new DateTime(_year, _month, 1, 12, 0, 0);
                        double tmp = lastDate.Subtract(_debit.date).TotalDays;
                        if (tmp <= Job.mainForm.fClients.getCurrentClient().lessdays)
                        {
                            return Job.mainForm.fClients.getCurrentClient().lessdays;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0.00;
                    }
                }
                catch (Exception ex)
                {
                    Job.Log("LessDays", ex);
                }
                return null;
            } 
        }
        public double? duedays 
        { 
            get 
            {
                if (rowtype == RowType.RemainingPayments) return null;
                if (autoRow.Report2) return null;
                if (totaldays == null || lessdays == null) return null;
                return totaldays.Value - lessdays.Value; 
            } 
        }

        public bool isRegularIntRateUsed = true;

        public object intamt 
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                if (autoRow.Report2) return null;
                if (isSale() && lastdate == null && totaldays >= -1 && totaldays <= 1) return null;

                if (_credit != null && _debit != null)
                {
                    if (Job.Holidays.validateHolidays(_debit.date, _credit.date))
                    {
                        return null;
                    }
                }

                double? days = totaldays < 0 ? totaldays : duedays;
                double intrate = Job.Functions.RoundDouble(Job.mainForm.fClients.getCurrentClient().intrate1);

                double? ret = null;
                
                double val=0;

                if (_credit != null)
                {
                    if (isSale() && _credit.date.Subtract(_debit.date).TotalDays == 1)
                    {
                        return null;
                    }
                    double tmp = _credit.date.Subtract(_debit.date).TotalDays;
                    if (tmp > Job.mainForm.fClients.getCurrentClient().cutoffdays)
                    {
                        intrate = Job.Functions.RoundDouble(Job.mainForm.fClients.getCurrentClient().intrate2);
                        isRegularIntRateUsed = false;
                    }
                }
                else if (newdate != null && (_credit == null || rowtype == RowType.RemainingDebitBalanceRow))
                {
                    double tmp = newdate.Value.Subtract(_debit.date).TotalDays;
                    if (tmp > Job.mainForm.fClients.getCurrentClient().cutoffdays)
                    {
                        intrate = Job.Functions.RoundDouble(Job.mainForm.fClients.getCurrentClient().intrate2);
                        isRegularIntRateUsed = false;
                    }
                }

                if (_credit != null)
                {
                    ret = intrate * days * taken_amount.Value / 36500;
                }
                else if (rowtype != RowType.RemainingDebitBalanceRow)
                {
                    ret = intrate * days * _debit.debit_amount / 36500;
                }
                else if (rowtype == RowType.RemainingDebitBalanceRow && double.TryParse(debit_invoice, out val))
                {
                    ret = intrate * days * val / 36500;
                }
                else
                {
                    ret = null;
                }

                if (ret!=null)
                {
                    bool isDebitOld = (_debit.date < new DateTime(_year, _month, 1,12,0,0));
                    bool isCreditOld = (_credit == null || (_credit != null && _credit.date < new DateTime(_year, _month, 1,12,0,0)));
                    if ((isDebitOld && isCreditOld))
                    {
                        if (totaldays != null && totaldays > 0 && duedays < 0)
                        {
                            return null;
                        }
                        else if (totaldays != null && totaldays < 0 && duedays < 0)
                        {
                            return "RECD";
                        }
                        else if (_credit != null)
                        {
                            return "RECD";
                        }
                    }
                    else if ((duedays != null && duedays < 0))
                    {
                        if (totaldays != null && totaldays > 0)
                        {
                            return null;
                        }
                        else if ((isDebitOld || isCreditOld) && totaldays != null && totaldays < 0 && duedays < 0)
                        {
                            return "RECD";
                        }
                    }
                }

                

                return ret == null ? null : (double?)Job.Functions.RoundDouble(ret.Value, 0);
            } 
        }

        public String cd
        {
            get
            {
                if (rowtype == RowType.RemainingPayments) return null;
                String ret = null;

                if (_credit != null && _debit != null)
                {
                    if (Job.Holidays.validateHolidays(_debit.date, _credit.date))
                    {
                        return null;
                    }
                }

                double gradeAmt = Job.Grades.DEFAULT_GRADE_AMOUNT;
                double days = 0;
                double tkamt=0;

                if (isSale() && lastdate == null && totaldays >= -1 && totaldays <= 1) return ret;

                if (totaldays < 0) return ret;

                if (duedays == null && !autoRow.Report2) return ret;

                if (_credit != null)
                {
                    if (isSale() && _credit.date.Subtract(_debit.date).TotalDays == 1)
                    {
                        return null;
                    }
                }

                if (_debit.grade != null && _debit.grade.id != 0)
                {
                    gradeAmt = _debit.grade.getAmount(_debit.date);
                }

                if (_credit != null)
                {
                    tkamt = taken_amount.Value;
                    if (duedays < 0 && totaldays != null)
                    {
                        days = totaldays.Value;
                    }
                    else if (lessdays != null)
                    {
                        days = lessdays.Value;
                    }
                }
                else if (rowtype != RowType.RemainingDebitBalanceRow)
                {
                    tkamt = _debit.debit_amount;
                    if (duedays < 0 && totaldays != null)
                    {
                        days = totaldays.Value;
                    }
                    else if (lessdays != null)
                    {
                        days = lessdays.Value;
                    }
                }
                else if (rowtype == RowType.RemainingDebitBalanceRow && double.TryParse(debit_invoice, out tkamt))
                {
                    if (duedays < 0 && totaldays != null)
                    {
                        days = totaldays.Value;
                    }
                    else if (lessdays != null)
                    {
                        days = lessdays.Value;
                    }
                }

                if (autoRow.Report2 && totaldays.HasValue)
                {
                    days = totaldays.Value;
                }
                else if(autoRow.Report2)
                {
                    days = 0;
                }

                double cdAmt=(tkamt / (_debit.debit_amount / _debit.mt) * gradeAmt * days);
                ret = cdAmt.ToString("0") + ".00";

                if (ret != null)
                {
                    double _dueDays = duedays == null ? -1 : duedays.Value;
                    bool isDebitOld = (_debit.date < new DateTime(_year, _month, 1, 12, 0, 0));
                    bool isCreditOld = (_credit == null || (_credit != null && _credit.date < new DateTime(_year, _month, 1, 12, 0, 0)));
                    if ((isDebitOld && isCreditOld))
                    {
                        if (totaldays != null && totaldays > 0 && duedays < 0)
                        {
                            return null;
                        }
                        else if (totaldays != null && totaldays < 0 && duedays < 0)
                        {
                            return "RECD";
                        }
                        else if (_credit != null)
                        {
                            return "RECD";
                        }
                    }
                    else if ((duedays != null && duedays < 0))
                    {
                        if (totaldays != null && totaldays < 0)
                        {
                            return null;
                        }
                        else if ((isDebitOld || isCreditOld) && totaldays != null && totaldays < 0 && duedays < 0)
                        {
                            return "RECD";
                        }
                    }
                }

                /*if (cdAmt > 0)
                {
                    if ((_debit.date < new DateTime(_year, _month, 1)) || (_credit != null && _credit.date < new DateTime(_year, _month, 1)))
                    {
                        if (_credit != null)
                            return "RECD";
                    }
                }*/

                return ret;
            }
        } 

    }
}
