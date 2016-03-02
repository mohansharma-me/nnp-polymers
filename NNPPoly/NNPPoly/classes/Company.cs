using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class Company: DataReflector
    {
        private String _name = "";
        private String _address = "";

        public Company(long id)
        {
            _id = id;
            tableName = "company";
            tableColumnPrefix = "company_";
        }

        public String name
        {
            get { return _name; }
            set
            {
                _name = isInit ? value : (String)ReflectData("name", value, _name, "company name");
            }
        }

        public String address
        {
            get { return _address; }
            set
            {
                _address = isInit ? value : (String)ReflectData("address", value, _name, "company address");
            }
        }

        public int selectedYear { get; set; }
    }
}
