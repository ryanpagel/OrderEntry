using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.BusObj
{
    public class Order
    {
        public Order()
        {
            _orderItems = new List<OrderItem>();
        }
        DateTime _orderDate;

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        double _subTotal;

        public double SubTotal
        {
            get { return _subTotal; }
            set { _subTotal = value; }
        }

        double _taxes;

        public double Taxes
        {
            get { return _taxes; }
            set { _taxes = value; }
        }

        double _grandTotal;

        public double GrandTotal
        {
            get { return _grandTotal; }
            set { _grandTotal = value; }
        }


        Enums.PaymentMethod _paymentMethod;

        public Enums.PaymentMethod PaymentMethod
        {
            get { return _paymentMethod; }
            set { _paymentMethod = value; }
        }

        List<OrderItem> _orderItems;

        public List<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }
    }
}
