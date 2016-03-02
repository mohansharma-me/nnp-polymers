using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class Scheme : DataReflector
    {
        private List<Params> _params;
        private long _client_id;
        private int _year;

        public Scheme(long id)
        {
            this._id = id;
            tableName = "scheme";
            tableColumnPrefix = "scheme_";
        }

        public int year
        {
            get { return _year; }
            set
            {
                _year = isInit ? value : (int)ReflectData("year", value, _year, "scheme year");
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

        public String client_name;
        public String scheme;

        public List<Params> parameters
        {
            get { return _params; }
            set
            {
                _params = value;
            }
        }

        public class Params : DataReflector
        {
            private long _group_id;

            private double _qty;
            private DateTime _month;

            public Params(long id)
            {
                this._id = id;
                tableName = "scheme_params";
                tableColumnPrefix = "sp_";
            }

            public Params(long groupId, double __qty, int __month, String __name=null) : this(0)
            {
                this._id = id;
                tableName = "scheme_params";
                tableColumnPrefix = "sp_";

                _qty = __qty;
                month_no = __month;

                _group_id = groupId;
                this.name = __name;
            }

            public String name;

            public long group_id
            {
                get { return _group_id; }
                set
                {
                    _group_id = isInit ? value : (long)ReflectData("group_id", value, _group_id, "group");
                }
            }

            public double qty
            {
                get { return _qty; }
                set
                {
                    _qty = isInit ? value : (double)ReflectData("group_qty", value, _qty, "group quantity");
                }
            }

            public DateTime month
            {
                get
                {
                    return _month;
                }
                set
                {
                    _month = isInit ? value : (DateTime)ReflectData("month", value, _month, "month");
                }
            }

            public int month_no
            {
                get
                {
                    return month.Month;
                }

                set
                {
                    month = new DateTime(month.Year, value, 1, 12, 0, 0);
                }
            }

            public DateTime? toMonth;

            public int toMonth_no
            {
                get
                {
                    return !toMonth.HasValue ? 0 : toMonth.Value.Month;
                }
            }
        }
    }
}
