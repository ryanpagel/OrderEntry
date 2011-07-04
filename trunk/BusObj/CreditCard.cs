using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.BusObj
{
    public class CreditCard
    {
        public CreditCard()
        {
        }

        Enums.CreditCardType _creditCardType = Enums.CreditCardType.UnknownOrNotSet;

        public Enums.CreditCardType CreditCardType
        {
            get { return _creditCardType; }
            set { _creditCardType = value; }
        }

        string _cardholderName = string.Empty;

        public string CardholderName
        {
            get { return _cardholderName; }
            set { _cardholderName = value; }
        }

        string _cardNumber = string.Empty;

        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        string _expirationDate = string.Empty;

        public string ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }


        string note = string.Empty;

        public string Note
        {
            get { return note; }
            set { note = value; }
        }

        public bool IsUsed
        {
            get
            {
                return _creditCardType != Enums.CreditCardType.UnknownOrNotSet
                    && _cardholderName != string.Empty
                    && _cardNumber != string.Empty;
            }
        }

    }
}
