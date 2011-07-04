using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickBooks.Util;

namespace QuickBooks.BusObj
{
    public class Address
    {
        public Address()
        {
        }


        public Address(Enums.AddressType addressType)
        {
            _addressType = addressType;
        }



        Enums.AddressType _addressType;

        public Enums.AddressType AddressType
        {
            get { return _addressType; }
            set { _addressType = value; }
        }

        public string Street
        {
            get
            {
                string street = string.Empty;
                if (_line1 != "")
                    street = _line1;
                if (_line2 != "")
                    street += _line2.PrependNewLine();
                if (_line3 != "")
                    street += _line3.PrependNewLine();
                if (_line4 != "")
                    street += _line4.PrependNewLine();

                return street;
            }
        }

        string _line1 = string.Empty;

        public string Line1
        {
            get { return _line1; }
            set 
            {
                _line1 = value.RemoveLineBreaks();
            }
        }

        string _line2 = string.Empty;

        public string Line2
        {
            get { return _line2; }
            set 
            {
                _line2 = value.RemoveLineBreaks();
            }
        }

        string _line3 = string.Empty;

        public string Line3
        {
            get { return _line3; }
            set { _line3 = value.RemoveLineBreaks(); }
        }

        string _line4 = string.Empty;

        public string Line4
        {
            get { return _line4; }
            set { _line4 = value.RemoveLineBreaks(); }
        }

        string _city = string.Empty;

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        string _state = string.Empty;

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        string _zip = string.Empty;

        public string Zip
        {
            get { return _zip; }
            set { _zip = value; }
        }
    }
}
