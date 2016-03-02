using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class DebitNote : DataReflector
    {
        private long _client_id;
        private bool _isDNote;
        private long _no;
        private DateTime _date;
        private List<PaymentEntry> _entries;

        public DebitNote(long id)
        {
            _id = id;
            tableName = "debitnote";
            tableColumnPrefix = "dn_";
        }

        public long client_id
        {
            get { return _client_id; }
            set
            {
                _client_id = value;
            }
        }

        public bool isDNote
        {
            get { return _isDNote; }
            set { _isDNote = (bool)ReflectData("isnote", value, _isDNote, "is it debit note"); }
        }

        public DateTime date
        {
            get { return _date; }
            set { _date = (DateTime)ReflectData("date", value, _date, "debit note date"); }
        }

        public List<PaymentEntry> entries
        {
            get { return _entries; }
            set
            {
                if (isInit)
                {
                    _entries = value;
                }
            }
        }

        public String client_name
        {
            get;
            set;
        }

        public double total_amount
        {
            get;
            set;
        }

        public class PaymentEntry
        {
            public long paymentId;
            public long myId;
        }
    }
}
