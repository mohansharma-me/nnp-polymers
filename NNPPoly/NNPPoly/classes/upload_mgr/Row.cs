using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.upload_mgr
{
    [Serializable]
    public class Row
    {
        public Row_DebitNotes debit_notes = Row_DebitNotes.None;

        public long clientid = 0;
        public String client_name = "";
        public classes.Payment.PaymentMode modes = classes.Payment.PaymentMode.Debit;

        public String type = "";
        public String invoice = "";
        public DateTime date = DateTime.Today;
        public double mt = 0;
        public double amount = 0;

        public long gradeid = 0;
        public String grade_code = "Default";
    }

    public enum Row_DebitNotes {
        None, CDDN, ENV, CDDN_ENV
    }

    public enum Row_PaymentMode
    {
        Debit, Credit
    }
}
