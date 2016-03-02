using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    class InterestAdviseRow
    {
        public static DateTime forMonth = DateTime.MinValue;
        public Client client;
        public double interest_amount = 0;

        public double closing_balance = 0;
        public double opening_balance = 0;
    }
}
