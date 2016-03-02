using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class Payment:DataReflector
    {
        private long _client_id;
        private DateTime _date;
        private String _invoice;
        private String _type;
        private String _particulars;
        private PaymentMode _mode;
        private double _amount, _mt, _creditAmount, _debitAmount, _closingBalance;
        private Grade _grade;
        private long _debit_note_id = 0;
        private bool _highlighted;
        private bool _isP = false, _isST = false;
        private double _remainBalance = 0;

        public bool isConsidered = false;
        
        public enum PaymentMode { Credit, Debit }

        public System.Drawing.Color color = System.Drawing.Color.Empty;

        public Payment(long id)
        {
            this._id = id;
            tableName = "payment";
            tableColumnPrefix = "payment_";
        }

        public double remainBalance
        {
            get
            {
                return _remainBalance;
            }
            set
            {
                _remainBalance = Job.Functions.RoundDouble(value);
            }
        }

        public bool isSale
        {
            get
            {
                return type.Trim().ToLower().Equals("sale");
            }
        }

        public bool isPriority
        {
            get
            {
                if (isInit)
                {
                    _isP = Job.PrioritiesAndTypes.find(type, false);
                }
                return _isP;
            }
        }

        public bool isSpecialType
        {
            get
            {
                if (isInit)
                {
                    _isST = Job.PrioritiesAndTypes.find(type, true);
                }
                return _isST;
            }
        }

        public long id { get { return _id; } }

        public double closing_balance
        {
            get { return _closingBalance; }
            set
            {
                _closingBalance = value;
            }
        }

        public bool highlighted
        {
            get { return _highlighted; }
            set
            {
                _highlighted = isInit ? value : (bool)ReflectData("highlighted", value, _highlighted, "set highlights");
            }
        }

        public Grade grade
        {
            get { return _grade; }
            set
            {
                if (!isInit && reflectData && value != null)
                {
                    long gradeId = (long)ReflectData("grade_id", value.id, _grade == null ? 0 : _grade.id, "grade change");
                    _grade = value;//Job.Grades.getGrade(gradeId, false);
                }
                else
                {
                    _grade = value;
                }
            }
        }

        public bool isDebitNote
        {
            get { return _debit_note_id > 0; }
        }

        public long debit_note_id
        {
            get
            {
                return _debit_note_id;
            }
            set
            {
                //_debit_note_id = isInit ? value : (long)ReflectData("isdnote_payment", value, _debit_note_id, "is it debit note id");
                _debit_note_id = isInit ? value : _debit_note_id;
            }
        }

        public double mt
        {
            get { return _mt; }
            set
            {
                if (mode == PaymentMode.Debit)
                    _mt = isInit ? value : (double)ReflectData("mt", value, _mt, "M.T.");
                else
                    _mt = 0.000;
            }
        }

        public double amount
        {
            get 
            {
                if (_mode == PaymentMode.Credit) 
                {
                    _creditAmount = _amount;
                }
                else if (_mode == PaymentMode.Debit)
                {
                    _debitAmount = _amount;
                }
                return _amount;
            }
            set
            {
                _amount = Job.Functions.RoundDouble(isInit ? value : (double)ReflectData("amount", value, _amount, "payment amount"));
                if (_mode == PaymentMode.Credit)
                {
                    _creditAmount = _amount;
                    remainBalance = _amount;
                }
                else if (_mode == PaymentMode.Debit)
                {
                    _debitAmount = _amount;
                    remainBalance = _amount;
                }
            }
        }

        public double credit_amount
        {
            get
            {
                double tmp = amount;
                return _mode == PaymentMode.Credit ? _creditAmount : 0;
            }
            set
            {
                if (_mode == PaymentMode.Credit)
                {
                    amount = value;
                }
            }
        }

        public double debit_amount
        {
            get
            {
                double tmp = amount;
                return _mode == PaymentMode.Credit ? 0 : _debitAmount;
            }
            set
            {
                if (_mode == PaymentMode.Debit)
                {
                    amount = value;
                }
            }
        }

        public PaymentMode mode
        {
            get { return _mode; }
            set
            {
                _mode = isInit ? value : (PaymentMode)ReflectData("mode", value, _mode, "payment t/n type", true);
            }
        }

        public String particulars
        {
            get { return _particulars; }
            set
            {
                _particulars = isInit ? value : (String)ReflectData("particulars", value, _type, "payment particulars");
            }
        }

        public String type
        {
            get { return _type; }
            set
            {
                _type = isInit ? value : (String)ReflectData("type", value, _type, "payment type");
            }
        }

        public bool skipDebitNoteFormation = false;

        public String invoice { 
            get 
            {
                if (isDebitNote && !skipDebitNoteFormation)
                {
                    return "DN No." + _invoice;
                }
                skipDebitNoteFormation = false;
                return _invoice; 
            }
            set 
            {
                if (!isInit && isDebitNote) return;
                _invoice = isInit ? value : (String)ReflectData("invoice", value, _invoice, "payment invoice number");
            }
        }

        public DateTime date
        {
            get { return _date; }
            set
            {
                _date = isInit ? value : (DateTime)ReflectData("date", value, _date, "payment date");
            }
        }

        public long client_id
        {
            get { return _client_id; }
            set
            {
                _client_id = isInit ? value : (long)ReflectData("client_id", value, _client_id, "client");
            }
        }


    }
}
