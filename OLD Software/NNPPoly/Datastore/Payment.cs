using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly
{
    public class Payment
    {
        public Decimal ID;
        public DateTime Date;
        public String DocChqNo, Type, Particulars, MT;
        public double Credit, Debit, Remain;

        public String NewDate = "", LastDate = "";
        public String Grade = "";
        public System.Drawing.Color color=System.Drawing.Color.Empty;
        public bool Verified = false,HighlightThis=false;
        public object Tag;
        public double CollectingAmount = 0;

        public bool Compare(Payment p,bool ignoreID=true)
        {
            return (ignoreID?true:p.ID == this.ID) &&
                p.Date.CompareTo(this.Date) == 0 &&
                p.DocChqNo.Trim().ToLower().Equals(this.DocChqNo.Trim().ToLower()) &&
                p.Type.Trim().ToLower().Equals(this.Type.Trim().ToLower()) &&
                p.Particulars.Trim().ToLower().Equals(this.Particulars.Trim().ToLower()) &&
                p.MT==this.MT &&
                p.Credit == Credit && p.Debit == Debit && p.Remain == Remain &&
                p.Grade.Trim().ToLower().Equals(Grade.Trim().ToLower());
        }
    }
}
