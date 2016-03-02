using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNPPoly.classes
{
    public class MyList:List<object>
    {
        private long _currentId = -1;

        public long SelectedItem
        {
            get { return _currentId; }
            set
            {
                _currentId = value;
            }
        }
    }
}
