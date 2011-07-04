using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.Util
{
    class IncludeAttribute : Attribute
    {
        bool _include;

        public bool Include
        {
            get { return _include; }
            set { _include = value; }
        }

        public IncludeAttribute(bool include)
        {
            _include = include;
        }
    }
}
