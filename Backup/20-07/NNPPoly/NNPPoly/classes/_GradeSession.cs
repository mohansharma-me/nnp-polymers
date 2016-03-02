using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class _GradeSession1 : DataReflector
    {
        private long _grade_id;
        private double _amount;
        private DateTime _session_date;


        public _GradeSession1(long id)
        {
            this._id = id;
            tableName = "grade_session";
            tableColumnPrefix = "gs_";
        }

        public override string ToString()
        {
            return _amount.ToString("0.00");
        }

        public long gradeId
        {
            get { return _grade_id; }
            set
            {
                _grade_id = isInit ? value : (long)ReflectData("grade_id", value, _grade_id, "grade");
            }
        }

        public double amount
        {
            get { return _amount; }
            set
            {
                _amount = isInit ? value : (double)ReflectData("amount", value, _amount, "grade amount");
            }
        }

        public DateTime date
        {
            get { return _session_date; }
            set
            {
                _session_date = isInit ? value : (DateTime)ReflectData("session_date", value, _session_date, "session date");
            }
        }
    }
}
