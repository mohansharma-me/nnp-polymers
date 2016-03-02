using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class Grade : DataReflector
    {
        private String _code;
        private long _company_id;
        private long _group_id;
        private GradeGroup _group;

        public long session_id=0;
        private double _grade_amount=0;

        public double getAmount(DateTime date)
        {
            if (id == 0) return Job.Grades.DEFAULT_GRADE_AMOUNT;
            if (Job.Grades.getSessionID(date, ref session_id))
            {
                _grade_amount = Job.Grades.getGradeAmount(id, session_id);
                return _grade_amount;
            }
            return Job.Grades.DEFAULT_GRADE_AMOUNT;
        }

        public double getAmount()
        {
            return getAmount(DateTime.Today);
        }

        public Grade(long id)
        {
            this._id = id;
            tableName = "grade";
            tableColumnPrefix = "grade_";
        }

        public double grade_amount
        {
            get { return _grade_amount; }
            set
            {
                if (isInit)
                {
                    _grade_amount = value;
                }
                else if(reflectData)
                {
                    if (Job.Grades.updateGradeAmount(id, session_id, value))
                    {
                        _grade_amount = value;
                    }
                }
            }
        }

        public GradeGroup group
        {
            get { return _group; }
            set
            {
                if (isInit)
                {
                    _group = value;
                }
                else if (reflectData && value!=null)
                {
                    if (Job.Grades.updateGradeGroup(id, value.id))
                    {
                        _group = value;
                    }
                }
            }
        }

        public long company_id { 
            get 
            {
                return _company_id; 
            }

            set
            {
                _company_id = value;
            }
        }

        public String code
        {
            get { return _code; }
            set
            {
                _code = isInit ? value : (String)ReflectData("code", value, _code, "grade code");
            }
        }

        public override string ToString()
        {
            return _code;
        }

    }
}
