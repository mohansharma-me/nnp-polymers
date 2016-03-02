using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly
{
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
}
