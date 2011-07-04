using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.Util
{
    public class ListItem
    {
        object _value;

        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }


        string _display;

        public string Display
        {
            get { return _display; }
            set { _display = value; }
        }

        public override string ToString()
        {
            return _display;
        }

        public ListItem(string display, object value)
        {
            _value = value;
            _display = display;
        }

    }
}
