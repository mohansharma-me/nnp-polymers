using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class Holiday : DataReflector
    {
        private DateTime _date;
        private String _desc;

        public Holiday(long id)
        {
            this._id = id;
            this.tableName = "holidays";
            this.tableColumnPrefix = "holiday_";
        }

        public DateTime date
        {
            get { return _date; }
            set
            {
                _date = isInit ? value : (DateTime)ReflectData("date", value, _date, "holiday date");
            }
        }

        public String description
        {
            get { return _desc; }
            set
            {
                _desc = isInit ? value : (String)ReflectData("desc", value, _desc, "holiday description");
            }
        }

    }
}
