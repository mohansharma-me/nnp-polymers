using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class GradeGroup : DataReflector
    {

        private long _company_id;
        private String _name;
        private double _qty;
        private double _monthly_percentage;
        private double _quaterly_percentage;
        private double _yearly_percentage;

        public GradeGroup(long id)
        {
            this._id = id;
            tableName = "grade_group";
            tableColumnPrefix = "gg_";
        }
        public long company_id
        {
            get { return _company_id;}
            set
            {
                _company_id = value;
            }
        }
        public String name
        {
            get { return _name == null ? "Default" : _name; }
            set
            {
                _name = isInit ? value : (String)ReflectData("name", value, _name, "group name");
            }
        }
        public double qty
        {
            get { return _qty; }
            set
            {
                _qty = isInit ? value : (double)ReflectData("qty", value, _qty, "quantity");
            }
        }
        public double monthly_percentage
        {
            get { return _monthly_percentage; }
            set
            {
                _monthly_percentage = isInit ? value : (double)ReflectData("monthly_percentage", value, _monthly_percentage, "minimum monthly %");
            }
        }
        public double quaterly_percentage
        {
            get { return _quaterly_percentage; }
            set
            {
                _quaterly_percentage = isInit ? value : (double)ReflectData("quaterly_percentage", value, _quaterly_percentage, "minimum quaterly %");
            }
        }
        public double yearly_percentage
        {
            get { return _yearly_percentage; }
            set
            {
                _yearly_percentage = isInit ? value : (double)ReflectData("yearly_percentage", value, _yearly_percentage, "minimum yearly %");
            }
        }

        public override string ToString()
        {
            return _name == null ? "Default" : _name;
        }
    }
}
