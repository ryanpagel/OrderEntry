using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.BusObj
{
    public class Customer : QuickBooks.BusObj.ICustomerPersonalInfo
    {
        public Customer()
        {
            _creditCards = new List<CreditCard>();
            _creditCards.Add(new CreditCard());
            _creditCards.Add(new CreditCard());
            _creditCards.Add(new CreditCard());

            _billingAddress = new Address(Enums.AddressType.Billing);
            _shippingAddress = new Address(Enums.AddressType.Shipping);
        }

        string _customerID = string.Empty;

        public string CustomerID
        {
            get { return _customerID; }
            set { _customerID = value; }
        }

        string _altPhone = string.Empty;
        public string AltPhone
        {
            get { return _altPhone; }
            set { _altPhone = value; }
        }

        string _email = string.Empty;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        string _altContact = string.Empty;

        public string AltContact
        {
            get { return _altContact; }
            set { _altContact = value; }
        }

        string _poBox = string.Empty;

        public string PoBox
        {
            get { return _poBox; }
            set { _poBox = value; }
        }

        string _editSequence = string.Empty;

        public string EditSequence
        {
            get { return _editSequence; }
            set { _editSequence = value; }
        }

        string _phone = string.Empty;

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        Address _billingAddress;

        public Address BillingAddress
        {
            get { return _billingAddress; }
            set { _billingAddress = value; }
        }

        Address _shippingAddress;

        public Address ShippingAddress
        {
            get { return _shippingAddress; }
            set { _shippingAddress = value; }
        }

        bool _creditCardFieldsExistInQB;
        public bool CreditCardFieldsExistInQB
        {
          get { return _creditCardFieldsExistInQB; }
          set { _creditCardFieldsExistInQB = value; }
        }

        List<CreditCard> _creditCards;

        public List<CreditCard> CreditCards
        {
            get { return _creditCards; }
            set { _creditCards = value; }
        }

        public void LoadPersonalInfo(ICustomerPersonalInfo pi)
        {
            this._altContact = pi.AltContact;
            this._altPhone = pi.AltPhone;
            this._customerID = pi.CustomerID;
            this._editSequence = pi.EditSequence;
            this._email = pi.Email;
            this._fullName = pi.FullName;
            this._phone = pi.Phone;
            this._poBox = pi.PoBox;
        }
    }
}
