using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.mou_report
{
    public class Row
    {
        private Row lastRow=null;
        private Gap _gap;
        public DateTime firstDate, lastDate;

        public Row(ref Row lastRow, String clientName, Gap gap)
        {
            this.lastRow = lastRow;
            this._gap = gap;
            this.client_name = clientName;

            this.firstDate = new DateTime(_gap.schemeYear, 4, 1, 12, 0, 0);
            this.lastDate = new DateTime(_gap.schemeYear+1, 3, 1, 12, 0, 0);
        }

        public GradeGroup gradeGroup
        {
            get
            {
                return _gap.gradeGroup;
            }
        }

        public String client_name;

        public long client_id;

        public DateTime toDate
        {
            get
            {
                return _gap.toMonth.Value;
            }
        }

        public DateTime fromDate
        {
            get
            {
                return _gap.fromMonth;
            }
        }

        public DateTime nowDate
        {
            get
            {
                return _gap.nowDate;
            }
        }

        public double mou_qty
        {
            get
            {
                return month_avg * 12;
            }
        }

        public double month_avg
        {
            get
            {
                return _gap.qty;
            }
        }

        public double month_min
        {
            get
            {
                return mou_qty * (_gap.gradeGroup.monthly_percentage / 100);
            }
        }

        public double qtr_avg
        {
            get
            {
                return month_avg * (12 / 4) * (activeMonthsInQuarter() / 3);
            }
        }

        public double activeMonthsInQuarter(bool forQtrMin=false)
        {
            if (fromDate == lastDate) return 1;

            if (fromDate == firstDate && toDate == lastDate)
            {
                return 3;
            }
            else if ((fromDate == firstDate && toDate < lastDate))
            {
                bool flag2 = quarterOfMonth(nowDate.Month) == quarterOfMonth(toDate.AddMonths(-1).Month);
                if (!flag2 && forQtrMin)
                {
                    return 3;
                }
                else if (flag2)
                {
                    return quarterUsability(toDate.AddMonths(-1).Month);
                }
                else
                {
                    return 0;
                }

            }
            else if (fromDate > firstDate && toDate == lastDate)
            {

                if (quarterOfMonth(nowDate.Month) == quarterOfMonth(fromDate.Month))
                {
                    return 4 - quarterUsability(fromDate.Month);
                }
                else
                {
                    return 3;
                }

            }
            else if (fromDate > firstDate && toDate < lastDate)
            {
                bool flag2 = quarterOfMonth(nowDate.Month) == quarterOfMonth(toDate.AddMonths(-1).Month);
                if (!flag2 && forQtrMin)
                {
                    return 3;
                }
                else if (flag2)
                {
                    return quarterUsability(toDate.AddMonths(-1).Month);
                }
                else
                {
                    return 0;
                }

            }

            #region commented-code 16-06
            /*if (nowDate < toDate) // in the range...
            {
                if (false && isParted && (partedQuarter == current_quarter)) 
                {

                }
                else
                {
                    if (fromDate.Month > 4)
                    {
                        if (current_quarter == partedQuarter)
                        {
                            return 4-quarterUsability(fromDate.Month);
                        }
                        else
                        {
                            return 3;
                        }
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            else if (nowDate > toDate) // if need to show older data
            {
                if (current_quarter == partedQuarter)
                {
                    double val = 3 - quarterUsability(toDate.Month);
                    return val == 0 ? 3 : val;
                }
                else
                {
                    return 0;
                    DateTime tempDate = new DateTime(toDate.Year, toDate.Month, 1, 12, 0, 0);
                    tempDate = tempDate.AddMonths(-1);
                    if (fromDate == tempDate)
                    {
                        return 1;
                    }
                    else
                    {
                        return 3;
                    }                    
                }
            }
            else // if looking on same month
            {
                if (fromDate.Month>4 && current_quarter == partedQuarter)
                {
                    return 4 - quarterUsability(fromDate.Month);
                }
                else
                {
                    double val = 3 - quarterUsability(current_month);
                    return val == 0 ? 3 : val;
                }
            }
            */
            #endregion

            throw new Exception("Unable to guess active months in current environment, please try again.");
            
            #region OldCode Commented - Working till 2nd level
            /*
            if (_gap.toMonth.HasValue)
            {
                if (quarterOfMonth(_gap.toMonth.Value.Month) == quarterOfMonth(current_month))
                {
                    return _gap.toMonth.Value.Month - firstMonthOfQuarter(quarterOfMonth(_gap.toMonth.Value.Month));
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                if (quarterOfMonth(current_month) != quarterOfMonth(_gap.fromMonth.Month))
                {
                    if (_gap.toMonth.HasValue && quarterOfMonth(_gap.toMonth.Value.Month) == quarterOfMonth(current_month))
                    {
                        return _gap.toMonth.Value.Month - firstMonthOfQuarter(quarterOfMonth(_gap.toMonth.Value.Month));
                    }
                    else
                    {
                        return 3;
                    }
                    //return (_gap.fromMonth.Month - firstMonthOfQuarter(quarterOfMonth(_gap.fromMonth.Month)));
                }
                else
                {
                    return 3 - (_gap.fromMonth.Month - firstMonthOfQuarter(quarterOfMonth(_gap.fromMonth.Month)));
                }
            }*/
            #endregion
        }

        public double partedQuarter
        {
            get 
            {
                if (fromDate.Month == 4)
                {
                    if (toDate.Month == 3)
                    {
                        return 1;
                    }
                    else
                    {
                        return quarterOfMonth(toDate.Month);
                    }
                }
                else
                {
                    return quarterOfMonth(fromDate.Month);
                }
            }
        }

        public bool isParted
        {
            get
            {
                return toDate.Month != 3;
            }
        }

        public double quarterUsability(int month)
        {

            double val = (double)month / 3.00f;
            val = val - Math.Floor(val);
            val = Job.Functions.RoundDouble(val);

            if (val == 0.33 || val==0.34) return 1;
            if (val == 0.66 || val==0.67) return 2;
            if (val == 0) return 3;

            throw new Exception("Unexpected error occured while observing usables months in current environment, please try again");

        }

        public double firstMonthOfQuarter(int quarter)
        {
            switch (quarter)
            {
                case 1: return 4;
                case 2: return 7;
                case 3: return 10;
                case 4: return 1;
            }

            throw new Exception("Unexpected error occured, quarter[#" + quarter + "] is not valid, please contact software developer team with error code #9u982478");
        }

        public double qtr_min
        {
            get
            {
                return (month_avg * 12) * (_gap.gradeGroup.quaterly_percentage / 100) * (activeMonthsInQuarter(true) / 3);
                #region commented code
                /*if (_schemeOfMonth == 4 && _partedMonth>0) // first line
                {
                    if (quarterOfMonth(_partedMonth) == quarterOfMonth(_curMonth))
                    {
                        return (mou_qty * (_group.quaterly_percentage / 100)) * (firstLineQuarterly() / 3);
                    }
                    else
                    {
                        return (mou_qty * (_group.quaterly_percentage / 100));
                    }
                }
                else if (_schemeOfMonth > 4 && myNumber==2) // line 2 calculation
                {
                    if (_partedMonth > 0) // if there is third line ahead
                    {

                        return (mou_qty * (_group.quaterly_percentage / 100)) * (firstLineQuarterly() / 3);

                    }
                    else // if this second last of this same client
                    {
                        if (quarterOfMonth(_schemeOfMonth) != quarterOfMonth(_curMonth))
                        {
                            return (mou_qty * (_group.quaterly_percentage / 100));
                        }
                        else
                        {
                            return (mou_qty * (_group.quaterly_percentage / 100)) * (secondLineQuarterly() / 3);
                        }
                    }
                }
                else if (myNumber == 3)
                {
                    if (quarterOfMonth(_schemeOfMonth) == quarterOfMonth(_curMonth))
                    {
                        return (mou_qty * (_group.quaterly_percentage / 100)) * (thirdLineQuarterly() / 3);
                    }
                    else
                    {
                        return (mou_qty * (_group.quaterly_percentage / 100));
                    }
                }
                else
                {
                    if (_schemeOfMonth != 4)
                    {
                        return (mou_qty * (_group.yearly_percentage / 100)) * countMonthsBefore(_curMonth) / 12; // it must be changed when third lap occures
                    }
                    else
                    {
                        return mou_qty * (_group.quaterly_percentage / 100);
                    }
                }

                return mou_qty * (_group.quaterly_percentage / 100);
            */
                #endregion
            }
        }

        public double year_min
        {
            get
            {
                return (month_avg * 12) * (_gap.gradeGroup.yearly_percentage / 100) * (activeMonthsInYears() / 12);
            }
        }

        public double yearUsability(int month)
        {

            switch (month)
            {
                case 4: return 1;
                case 5: return 2;
                case 6: return 3;
                case 7: return 4;
                case 8: return 5;
                case 9: return 6;
                case 10: return 7;
                case 11: return 8;
                case 12: return 9;
                case 1: return 10;
                case 2: return 11;
                case 3: return 12;
            }

            throw new Exception("Unable to count year usability of month("+month+"), month isn't valid.");
        }

        public double activeMonthsInYears(bool forQtrMin = false)
        {
            //return yearUsability(toDate.Month);
            //return Math.Ceiling(toDate.Subtract(fromDate).TotalDays / 30);
            if (fromDate == firstDate && toDate == lastDate)
            {
                return 12;
            }
            else if ((fromDate == firstDate && toDate < lastDate))
            {
                return yearUsability(toDate.Month - 1);
            }
            else if (fromDate > firstDate && toDate == lastDate)
            {
                return 13 - yearUsability(fromDate.Month);
            }
            else if (fromDate > firstDate && toDate < lastDate)
            {
                return yearUsability(toDate.Month) - yearUsability(fromDate.Month);
            }

            #region commented-code 16-06
            /*if (nowDate < toDate) // in the range...
            {
                if (false && isParted && (partedQuarter == current_quarter)) 
                {

                }
                else
                {
                    if (fromDate.Month > 4)
                    {
                        if (current_quarter == partedQuarter)
                        {
                            return 4-quarterUsability(fromDate.Month);
                        }
                        else
                        {
                            return 3;
                        }
                    }
                    else
                    {
                        return 3;
                    }
                }
            }
            else if (nowDate > toDate) // if need to show older data
            {
                if (current_quarter == partedQuarter)
                {
                    double val = 3 - quarterUsability(toDate.Month);
                    return val == 0 ? 3 : val;
                }
                else
                {
                    return 0;
                    DateTime tempDate = new DateTime(toDate.Year, toDate.Month, 1, 12, 0, 0);
                    tempDate = tempDate.AddMonths(-1);
                    if (fromDate == tempDate)
                    {
                        return 1;
                    }
                    else
                    {
                        return 3;
                    }                    
                }
            }
            else // if looking on same month
            {
                if (fromDate.Month>4 && current_quarter == partedQuarter)
                {
                    return 4 - quarterUsability(fromDate.Month);
                }
                else
                {
                    double val = 3 - quarterUsability(current_month);
                    return val == 0 ? 3 : val;
                }
            }
            */
            #endregion

            throw new Exception("Unable to guess active months in current environment, please try again.");

            #region OldCode Commented - Working till 2nd level
            /*
            if (_gap.toMonth.HasValue)
            {
                if (quarterOfMonth(_gap.toMonth.Value.Month) == quarterOfMonth(current_month))
                {
                    return _gap.toMonth.Value.Month - firstMonthOfQuarter(quarterOfMonth(_gap.toMonth.Value.Month));
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                if (quarterOfMonth(current_month) != quarterOfMonth(_gap.fromMonth.Month))
                {
                    if (_gap.toMonth.HasValue && quarterOfMonth(_gap.toMonth.Value.Month) == quarterOfMonth(current_month))
                    {
                        return _gap.toMonth.Value.Month - firstMonthOfQuarter(quarterOfMonth(_gap.toMonth.Value.Month));
                    }
                    else
                    {
                        return 3;
                    }
                    //return (_gap.fromMonth.Month - firstMonthOfQuarter(quarterOfMonth(_gap.fromMonth.Month)));
                }
                else
                {
                    return 3 - (_gap.fromMonth.Month - firstMonthOfQuarter(quarterOfMonth(_gap.fromMonth.Month)));
                }
            }*/
            #endregion
        }

        public double month_pending
        {
            get
            {
                if (toDate!=lastDate) return 0;
                double monthAmount = 0;
                switch (current_month)
                {
                    case 4: monthAmount = apr; break;
                    case 5: monthAmount = may; break;
                    case 6: monthAmount = jun; break;
                    case 7: monthAmount = jul; break;
                    case 8: monthAmount = aug; break;
                    case 9: monthAmount = sep; break;
                    case 10: monthAmount = oct; break;
                    case 11: monthAmount = nov; break;
                    case 12: monthAmount = dec; break;
                    case 1: monthAmount = jan; break;
                    case 2: monthAmount = feb; break;
                    case 3: monthAmount = mar; break;

                    default: monthAmount = 9999999999999999; break;
                }
                return month_avg - monthAmount;
            }
        }

        public double quarter_pending
        {
            get
            {
                if (toDate != lastDate) return 0;

                double quarterAmount = 0;
                switch (current_quarter)
                {
                    case 1: quarterAmount = q1_total; break;
                    case 2: quarterAmount = q2_total; break;
                    case 3: quarterAmount = q3_total; break;
                    case 4: quarterAmount = q4_total; break;

                    default: quarterAmount = 9999999999999999; break;
                }

                double totalQtrAvg = qtr_avg;
                Row curRow = lastRow;
                while (curRow != null)
                {
                    switch (current_quarter)
                    {
                        case 1: quarterAmount += curRow.q1_total; break;
                        case 2: quarterAmount += curRow.q2_total; break;
                        case 3: quarterAmount += curRow.q3_total; break;
                        case 4: quarterAmount += curRow.q4_total; break;
                    }
                    totalQtrAvg += curRow.qtr_avg;
                    curRow = curRow.lastRow;
                }

                return totalQtrAvg - quarterAmount;
            }
        }

        public int current_month
        {
            get
            {
                return _gap.activeMonth;
            }
        }

        public int current_quarter
        {
            get
            {
                return quarterOfMonth(current_month);
            }
        }

        public double year_avg
        {
            get
            {
                return (month_avg * 12) * (activeMonthsInYears() / 12);
            }
        }

        public double annual_pending {
            get
            {
                if (toDate != lastDate) return 0;
                double totalYearAvgAmount = year_avg;
                double totalYearAmount = yearly_total;
                Row curRow = lastRow;
                while (curRow != null)
                {
                    totalYearAmount += curRow.yearly_total;
                    totalYearAvgAmount += curRow.year_avg;
                    curRow = curRow.lastRow;
                }

                return totalYearAvgAmount - totalYearAmount;
            }
        }
        
        public double apr
        {
            get
            {
                return _gap.apr;
            }
        }

        public double may
        {
            get
            {
                return _gap.may;
            }
        }
        public double jun
        {
            get
            {
                return _gap.jun;
            }

        }
        public double q1_total
        {
            get
            {
                return apr + may + jun;
            }
        }
        public double jul
        {
            get
            {
                return _gap.jul;
            }

        }
        public double aug
        {
            get
            {
                return _gap.aug;
            }

        }
        public double sep
        {
            get
            {
                return _gap.sep;
            }

        }
        public double q2_total
        {
            get
            {
                return jul + aug + sep;
            }
        }
        public double oct
        {
            get
            {
                return _gap.oct;
            }

        }
        public double nov
        {
            get
            {
                return _gap.nov;
            }
        }
        public double dec
        {
            get
            {
                return _gap.dec;
            }

        }
        public double q3_total
        {
            get
            {
                return oct + nov + dec;
            }
        }
        public double jan
        {
            get
            {
                return _gap.jan;
            }

        }
        public double feb
        {
            get
            {
                return _gap.feb;
            }

        }
        public double mar
        {
            get
            {
                return _gap.mar;
            }

        }
        public double q4_total 
        {
            get { return jan + feb + mar; }
        }

        public double yearly_total 
        {
            get
            {
                return q1_total + q2_total + q3_total + q4_total;
            }
        }

        private double calcMonthValue(int mineMonth, double curValue)
        {
            return 0;
            /*if (_partedMonth > 0)
            {
                if (_schemeOfMonth <= mineMonth && mineMonth < _partedMonth)
                    return curValue;
                else
                    return 0;
            }
            else
            {
                return _schemeOfMonth > mineMonth ? 0 : curValue;
            }*/
        }

        public int quarterOfMonth(int month)
        {
            switch (month)
            {
                case 1:
                case 2:
                case 3: return 4;

                case 4:
                case 5:
                case 6: return 1;

                case 7:
                case 8:
                case 9: return 2;

                case 10:
                case 11:
                case 12: return 3;
            }

            throw new Exception("Unexpected month given while analysising quarter of month procedure, please try again or if error persist than please contact software developer team with error code #298342934");
        }

        #region 1st, 2nd, 3rd line code
        /*
        private double firstLineQuarterly()
        {
            switch (_partedMonth)
            {
                case 5:
                case 8:
                case 11:
                case 2: return 1;

                case 6:
                case 9:
                case 12:
                case 3: return 2;

                case 4:
                case 7:
                case 10:
                case 1: return 3;

                default: return 0;
            }
        }

        private double secondLineQuarterly()
        {
            switch (_schemeOfMonth)
            {
                case 5:
                case 8:
                case 11:
                case 2: return 2;

                case 6:
                case 9:
                case 12:
                case 3: return 3;

                case 4:
                case 7:
                case 10:
                case 1: return 1;

                default: return 0;
            }
        }

        private double thirdLineQuarterly()
        {
            switch (_schemeOfMonth)
            {
                case 5:
                case 8:
                case 11:
                case 2: return 3;

                case 6:
                case 9:
                case 12:
                case 3: return 1;

                case 4:
                case 7:
                case 10:
                case 1: return 2;

                default: return 0;
            }
        }

        */

        #endregion

    }
}
