using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes.collection_list
{
    public class Collection
    {
        public Client client;
        public double CollectingAmount = 0;
        public double overdue = 0;
        public List<classes.report1.Row> Rows = null;
    }
}
