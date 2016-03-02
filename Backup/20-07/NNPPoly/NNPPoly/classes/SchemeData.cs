using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class SchemeData : DataReflector 
    {

        private long _client_id;
        private long _grade_id;
        private DateTime _date;
        private double _qty;

        public SchemeData(long id)
        {
            this._id = id;
            tableName = "scheme_data";
            tableColumnPrefix = "sd_";
        }

        public String client_name;
        public long client_id
        {
            get
            {
                return _client_id;
            }

            set
            {
                _client_id = isInit ? value : (long)ReflectData("client_id", value, _client_id, "client profile");
            }
        }

        public String grade_name;
        public long grade_id
        {
            get
            {
                return _grade_id;
            }

            set
            {
                _grade_id = isInit ? value : (long)ReflectData("grade_id", value, _grade_id, "grade");
            }
        }

        public DateTime date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = isInit ? value : (DateTime)ReflectData("date", value, _date, "entry date");
            }
        }

        public double qty
        {
            get
            {
                return Job.Functions.RoundDouble(_qty, 3);
            }

            set
            {
                _qty = isInit ? value : (double)ReflectData("qty", value, _qty, "quantity");
            }
        }

    }
}
