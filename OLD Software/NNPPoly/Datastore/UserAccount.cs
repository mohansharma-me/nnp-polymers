using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly
{
    public class UserAccount
    {
        public Decimal ID;
        public Decimal PaymentIDManager = 0;
        public String ClientName,ClientDescription;
        public double OpeningBalance = 0, LessDays = 10;

        public double InterestRate1 = 21, InterestRate2 = 24, CutOffDays = 20;
        public String OBType="";
        public String FooText="";
        public String mobileNumber = "";
        public String emailAddress = "";

        public List<Payment> Payments = new List<Payment>();
    }
}
