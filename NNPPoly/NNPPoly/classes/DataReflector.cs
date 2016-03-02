using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public abstract class DataReflector
    {
        protected long _id = 0;
        protected String tableName = "";
        protected String tableColumnPrefix = "";
        protected bool reflectData = false;
        protected bool isInit = true;

        public long id { get { return _id; } }

        public override string ToString()
        {
            return base.ToString();
        }

        public bool SetDataReflector
        {
            get { return reflectData; }
            set
            {
                reflectData = value;
            }
        }

        public bool SetInitMode { get { return isInit; } set { isInit = value; } }

        public static int encodeBool(bool boolean)
        {
            if (boolean) return 1;
            return 0;
        }

        public static bool decodeBool(int code)
        {
            return code == 1 ? true : false;
        }

        public static bool decodeBool(String code)
        {
            try
            {
                return decodeBool(int.Parse(code));
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReflectData(String column, object value)
        {
            try
            {
                if (!reflectData) return false;
                if (value is bool)
                {
                    value = (bool)value ? 1 : 0;
                }
                return Job.DB.executeQuery("update " + tableName + " set " + tableColumnPrefix + column + "=@val where " + tableColumnPrefix + "id=" + _id, new System.Data.SQLite.SQLiteParameter[] { new System.Data.SQLite.SQLiteParameter("@val", value) }) == 1;
            }
            catch (Exception ex)
            {
                Job.Log("Error[ReflectData(String,object)]", ex);
                return false;
            }
            finally
            {
                reflectData = false;
            }
        }

        public object ReflectData(String c, object newval, object oldval, String name)
        {
            return ReflectData(c, newval, oldval, name, false);
        }

        public object ReflectData(String c, object newval, object oldval, String name, bool toString)
        {
            try
            {
                if (isInit) return newval;
                if (!reflectData) return oldval;
                object val = newval;
                if (toString)
                {
                    val = newval.ToString();
                }

                if (!ReflectData(c, val))
                {
                    reflectData = false;
                    Job.Functions.ShowMsg("Unable reflect new " + name + " ('" + newval + "') to database, please try again.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return oldval;
                }
                else
                {
                    reflectData = false;
                    return newval;
                }
            }
            catch (Exception ex)
            {
                Job.Log("Error[ReflectData(String,object,object,String)]", ex);
                return oldval;
            }
            finally
            {
                reflectData = false;
            }
        }

        public bool Delete()
        {
            return Job.DB.executeQuery("delete from " + tableName + " where " + tableColumnPrefix + "id=@id", new System.Data.SQLite.SQLiteParameter[] { new System.Data.SQLite.SQLiteParameter("@id", _id) }) == 1;
        }

        public object tag = null;
    }
}
