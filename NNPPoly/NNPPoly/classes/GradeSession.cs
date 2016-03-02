using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class GradeSession : DataReflector
    {
        private long _company_id;
        private DateTime _from_date;
        private DateTime _to_date;
        
        public GradeSession(long id)
        {
            this._id = id;
            tableName = "grade_session";
            tableColumnPrefix = "gs_";
        }

        public long company_id
        {
            get
            {
                return _company_id;
            }

            set
            {
                _company_id = (long)ReflectData("company_id", value, _company_id, "company id");
            }
        }

        public DateTime from_date
        {
            get
            {
                return _from_date;
            }

            set
            {
                _from_date = (DateTime)ReflectData("from_date", value, _from_date, "session starting date");
            }
        }

        public DateTime to_date
        {
            get
            {
                return _to_date;
            }

            set
            {
                //_to_date = (DateTime)ReflectData("to_date", value, _to_date, "session last date");
                _to_date = value;
            }
        }

        public override string ToString()
        {
            if (_to_date != DateTime.MinValue)
                return _from_date.ToShortDateString() + " >> " + _to_date.ToShortDateString();
            else
                return _from_date.ToShortDateString() + " >> Future";
        }
    }
}
