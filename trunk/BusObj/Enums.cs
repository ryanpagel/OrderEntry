using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.BusObj
{
    public class Enums
    {
        public enum AddressType
        {
            Billing,
            Shipping
        }

        public enum CreditCardType
        {
            Visa,
            Mastercard,
            Discover,
            Amex,
            UnknownOrNotSet
        }

        public enum PaymentMethod
        {
            CreditCard,
            Check,
            MoneyOrder,
            PayPal,
            WireTransfer,
            NotSelected,
            Credit
        }
    }
}
