using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.mou_report
{
    public class Gap
    {

        public Gap(int schemeYear, int activeMonth,classes.Scheme.Params param, classes.GradeGroup gradeGroup, DateTime fromMonth, DateTime? toMonth)
        {
            this.schemeYear = schemeYear;
            this.activeMonth = activeMonth;
            this.param = param;
            this.gradeGroup = gradeGroup;
            this.fromMonth = fromMonth;
            this.toMonth = toMonth;
        }

        public int schemeYear;
        public int activeMonth;
        public classes.Scheme.Params param;
        public classes.GradeGroup gradeGroup;
        public DateTime fromMonth;


        private DateTime? __toMonth = null;
        public DateTime? toMonth
        {
            get
            {
                if (__toMonth == null)
                    toMonth = null;
                return __toMonth;
            }

            set
            {
                if (value == null)
                {
                    __toMonth = new DateTime(schemeYear + 1, 3, 1, 12, 0, 0);
                }
                else
                {
                    __toMonth = value;
                }

            }
        }

        public DateTime nowDate
        {
            get
            {
                int mineYear = schemeYear;
                if (activeMonth >= 1 && activeMonth <= 3)
                    mineYear++;
                return new DateTime(mineYear, activeMonth, 1, 12, 0, 0);
            }
        }

        public double qty
        {
            get { return param.qty; }
            set
            {
                param.SetInitMode = true;
                param.qty = value;
                param.SetInitMode = false;
            }
        }

        public double apr, may, jun, jul, aug, sep, oct, nov, dec, jan, feb, mar;

        public bool getMonthCounts(long clientId)
        {
            DateTime tmpMonth;
            DateTime selDate = new DateTime(activeMonth > 3 ? schemeYear : schemeYear + 1, activeMonth, 1, 12, 0, 0);
            apr = may = jun = jul = aug = sep = oct = nov = dec = jan = feb = mar = 0.000;


            tmpMonth = new DateTime(schemeYear, 4, 1, 12, 0, 0);
            if (tmpMonth<=selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                apr = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 5, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                may = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 6, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                jun = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 7, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                jul = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 8, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                aug = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 9, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                sep = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 10, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                oct = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 11, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                nov = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear, 12, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                dec = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear+1, 1, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                jan = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear+1, 2, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value > tmpMonth)))
                feb = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            tmpMonth = new DateTime(schemeYear+1, 3, 1, 12, 0, 0);
            if (tmpMonth <= selDate && fromMonth <= tmpMonth && (!toMonth.HasValue || (toMonth.HasValue && toMonth.Value >= tmpMonth)))
                mar = Job.Schemes.getMonthReport(tmpMonth.Year, tmpMonth.Month, clientId, gradeGroup.id);

            return true;
        }

    }

}
