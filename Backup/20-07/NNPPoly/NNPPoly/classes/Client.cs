using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class Client : DataReflector
    {
        public enum OpeningBalanceType
        {
            Credit, Debit
        }

        private long _company_id = 0;
        private String _name = "";
        private String _about = "";
        private double _obalance = 0.00;
        private double _cbalance = 0.00;
        private OpeningBalanceType _obalance_type = OpeningBalanceType.Debit;
        private double _ir1 = 0.00, _ir2 = 0.00;
        private long _cutoffdays = 0, _lessday = 0;
        private String _foo = "";
        private String _mobiles = "";
        private String _emails = "";


        public Client(long id)
        {
            _id = id;
            tableName = "client";
            tableColumnPrefix = "client_";
        }

        public long id { get { return _id; } }

        public long company_id
        {
            get { return _company_id; }
            set
            {
                _company_id = isInit ? value : (long)ReflectData("company_id", value, _name, "company");
            }
        }

        public String name { 
            get { return _name; } 
            set 
            {
                if (!Job.Validation.ValidateString(value))
                {
                    Job.Functions.ShowMsg("Please enter valid client name, please try again.", "Invalid Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                _name = isInit ? value : (String)ReflectData("name", value, _name, "client name");
            }
        }

        
        public String about
        {
            get { return _about; }
            set
            {
                _about = isInit ? value : (String)ReflectData("about", value, _about, "address");
            }
        }

        public String[] about1
        {
            get
            {
                List<String> s = new List<String>();
                s.Add("1"); s.Add("2");
                return s.ToArray();
            }
            set
            {

            }
        }

        
        public double obalance
        {
            get { return _obalance; }
            set
            {
                _obalance = isInit ? value : (double)ReflectData("openingbalance", value, _obalance, "opening balance");
            }
        }

        
        public double cbalance
        {
            get { return _cbalance; }
            set
            {
                _cbalance = value;
            }
        }

        
        public OpeningBalanceType obalance_type { get { return _obalance_type; }
            set 
            {
                _obalance_type = isInit ? value : (OpeningBalanceType)ReflectData("openingbalance_type", value, _obalance_type, "opening balance type", true);
            }
        }

        public bool threadUpdateForIntRates = true;

        public double intrate1 { get {return _ir1;}
            set 
            {
                if (reflectData)
                {
                    if (threadUpdateForIntRates)
                    {
                        System.Threading.Thread th = new System.Threading.Thread(() =>
                        {
                            System.Data.SQLite.SQLiteDataReader dr = Job.DB.executeReader("select ir_id from intrate where ir_client_id=" + _id);
                            if (dr.Read())
                            {
                                long irid = (long)dr["ir_id"];
                                if (Job.DB.executeQuery("update intrate set ir_rate=" + value + " where ir_id=" + irid + " and ir_client_id=" + _id) == 1)
                                {
                                    _ir1 = value;
                                }
                                else
                                {
                                    Job.Functions.ShowMsg("Unable to reflect new interest rate ('" + value + "') to database, please try again.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }
                            }
                            reflectData = false;
                        });
                        th.Priority = System.Threading.ThreadPriority.Highest;
                        th.Start();
                    }
                    else
                    {
                        System.Data.SQLite.SQLiteDataReader dr = Job.DB.executeReader("select ir_id from intrate where ir_client_id=" + _id);
                        if (dr.Read())
                        {
                            long irid = (long)dr["ir_id"];
                            if (Job.DB.executeQuery("update intrate set ir_rate=" + value + " where ir_id=" + irid + " and ir_client_id=" + _id) == 1)
                            {
                                _ir1 = value;
                            }
                            else
                            {
                                Job.Functions.ShowMsg("Unable to reflect new interest rate ('" + value + "') to database, please try again.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                        reflectData = false;
                    }
                }
                else
                {
                    _ir1 = value;
                }
            }
        }
        public double intrate2
        {
            get { return _ir2; }
            set
            {
                if (reflectData)
                {
                    if (threadUpdateForIntRates)
                    {
                        System.Threading.Thread th = new System.Threading.Thread(() =>
                        {
                            System.Data.SQLite.SQLiteDataReader dr = Job.DB.executeReader("select ir_id from intrate where ir_client_id=" + _id);
                            if (dr.Read() && dr.Read())
                            {
                                long irid = (long)dr["ir_id"];
                                if (Job.DB.executeQuery("update intrate set ir_rate=" + value + " where ir_id=" + irid + " and ir_client_id=" + _id) == 1)
                                {
                                    _ir2 = value;
                                }
                                else
                                {
                                    Job.Functions.ShowMsg("Unable to reflect new interest rate ('" + value + "') to database, please try again.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                                }
                            }
                            reflectData = false;
                        });
                        th.Priority = System.Threading.ThreadPriority.Highest;
                        th.Start();
                    }
                    else
                    {
                        System.Data.SQLite.SQLiteDataReader dr = Job.DB.executeReader("select ir_id from intrate where ir_client_id=" + _id);
                        if (dr.Read() && dr.Read())
                        {
                            long irid = (long)dr["ir_id"];
                            if (Job.DB.executeQuery("update intrate set ir_rate=" + value + " where ir_id=" + irid + " and ir_client_id=" + _id) == 1)
                            {
                                _ir2 = value;
                            }
                            else
                            {
                                Job.Functions.ShowMsg("Unable to reflect new interest rate ('" + value + "') to database, please try again.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                        reflectData = false;
                    }
                }
                else
                {
                    _ir2 = value;
                }
            }
        }

        
        public long cutoffdays { get {return _cutoffdays;} 
            set
            {
                if (reflectData)
                {
                    System.Threading.Thread th = new System.Threading.Thread(() =>
                    {
                        System.Data.SQLite.SQLiteDataReader dr = Job.DB.executeReader("select ir_id from intrate where ir_client_id=" + _id);
                        if (dr.Read() && dr.Read())
                        {
                            long irid = (long)dr["ir_id"];
                            if (Job.DB.executeQuery("update intrate set ir_days=" + value + " where ir_id=" + irid + " and ir_client_id=" + _id) == 1)
                            {
                                _cutoffdays = value;
                            }
                            else
                            {
                                Job.Functions.ShowMsg("Unable to reflect new cut off days ('" + value + "') to database, please try again.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            }
                        }
                        reflectData = false;
                    });
                    th.Priority = System.Threading.ThreadPriority.Highest;
                    th.Start();
                }
                else
                {
                    _cutoffdays = value;
                }
            } 
        }

        public long lessdays { get { return _lessday; }
            set
            {
                _lessday = isInit ? value : (long)ReflectData("lessdays", value, _lessday, "less days");
            }
        }

        
        public String footext { get { return _foo; } 
            set 
            {
                _foo = isInit ? value : (String)ReflectData("footext", value, _foo, "report footer text");
            } 
        }

        
        public String mobiles { get { return _mobiles; } 
            set 
            {
                if (!Job.Validation.ValidateMobiles(value))
                {
                    Job.Functions.ShowMsg("Please enter valid mobile numbers separated by ',' (single comma), please try again.", "Invalid Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
                _mobiles = isInit ? value : (String)ReflectData("mobile", value, _mobiles, "mobile numbers");
            } 
        }

        
        public String emails { get { return _emails; }
            set 
            {
                if (!Job.Validation.ValidateEmails(value))
                {
                    Job.Functions.ShowMsg("Please enter valid email address separated by ',' (single comma), please try again.", "Invalid Data", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }

                _emails = isInit ? value : (String)ReflectData("email", value, _emails, "email address");
            }
        }
    }
}
