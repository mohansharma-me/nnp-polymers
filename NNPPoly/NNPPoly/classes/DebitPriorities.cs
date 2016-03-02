using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class DebitPriorities : DataReflector
    {
        private String _type = "";
        private bool _isSpecial = false;
        private long _compid;

        public DebitPriorities(long id)
        {
            this._id = id;
            tableName = "debit_priorities";
            tableColumnPrefix = "dp_";
        }

        public long id { get { return _id; } }

        public String type
        {
            get { return _type; }
            set
            {
                _type = isInit ? value : (String)ReflectData("type", value, _type, "debit type");
            }
        }

        public long company_id
        {
            get { return _compid; }
            set
            {
                _compid = value;
            }
        }

        public bool is_special
        {
            get { return _isSpecial; }
            set
            {
                _isSpecial = isInit ? value : (bool)ReflectData("special", value, _isSpecial, "special type");
            }
        }
    }
}
