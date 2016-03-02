using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.old_version
{
    class AllOldClasses
    {
    }

    public class DebitNotePrint
    {
        public Decimal clientID;
        public String toClientName;
        public Decimal debitNoteNo;
        public DateTime debitDate;
        public List<DebitNoteRow> debitNoteRows;
        public double totalAmount;
        public object ID;
    }
    public class DebitNoteRow
    {
        /*public String InvoiceNo;
        public DateTime Date;
        public String Grade;
        public String Qty;*/
        public double Amount;
        public object ID;
    }

    public class Grade
    {
        public String GradeName;
        public double Amount = 0;
    }

    public class Payment
    {
        public Decimal ID;
        public DateTime Date;
        public String DocChqNo, Type, Particulars, MT;
        public double Credit, Debit, Remain;

        public String NewDate = "", LastDate = "";
        public String Grade = "";
        public System.Drawing.Color color = System.Drawing.Color.Empty;
        public bool Verified = false, HighlightThis = false;
        public object Tag;
        public double CollectingAmount = 0;

        public bool Compare(Payment p, bool ignoreID = true)
        {
            return (ignoreID ? true : p.ID == this.ID) &&
                p.Date.CompareTo(this.Date) == 0 &&
                p.DocChqNo.Trim().ToLower().Equals(this.DocChqNo.Trim().ToLower()) &&
                p.Type.Trim().ToLower().Equals(this.Type.Trim().ToLower()) &&
                p.Particulars.Trim().ToLower().Equals(this.Particulars.Trim().ToLower()) &&
                p.MT == this.MT &&
                p.Credit == Credit && p.Debit == Debit && p.Remain == Remain &&
                p.Grade.Trim().ToLower().Equals(Grade.Trim().ToLower());
        }
    }

    public class Record
    {
        public Decimal ID;
        public DateTime Date;
        public Payment payment;
        public Decimal ClientID;
        public String ClientName;
    }

    public class UserAccount
    {
        public Decimal ID;
        public Decimal PaymentIDManager = 0;
        public String ClientName, ClientDescription;
        public double OpeningBalance = 0, LessDays = 10;

        public double InterestRate1 = 21, InterestRate2 = 24, CutOffDays = 20;
        public String OBType = "";
        public String FooText = "";
        public String mobileNumber = "";
        public String emailAddress = "";

        public List<Payment> Payments = new List<Payment>();
    }
}
