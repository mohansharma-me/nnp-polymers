using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class IntRate:DataReflector
    {
        private long _client_id;
        private double _rate;
        private long _days;

        public IntRate(int id)
        {
            this._id = id;
            tableName = "intrate";
            tableColumnPrefix = "ir_";
        }

        public long id { get { return _id; } }

        public long client_id
        {
            get { return _client_id; }
            set
            {
                _client_id = isInit ? value : (long)ReflectData("client_id", value, _client_id, "client");
            }
        }

        public double rate
        {
            get { return _rate; }
            set
            {
                _rate = isInit ? value : (double)ReflectData("rate", value, _rate, "rate");
            }
        }

        public long days
        {
            get { return _days; }
            set
            {
                _days = isInit ? value : (long)ReflectData("days", value, _days, "cutoff days");
            }
        }
    }
}
