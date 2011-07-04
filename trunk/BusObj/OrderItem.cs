using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickBooks.BusObj
{
    public class OrderItem
    {

        public OrderItem()
        {
        }

        string _itemID;

        public string ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        string _account = string.Empty;

        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }



        string _itemName = string.Empty;

        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        double _quantity;

        public double Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        double _total;

        public double Total
        {
            get { return _total; }
            set { _total = value; }
        }

        double _price;

        public double Price
        {
            get { return _price; }
            set { _price = value; }
        }

        bool _isTaxed;

        public bool IsTaxed
        {
            get { return _isTaxed; }
            set { _isTaxed = value; }
        }

        public override string ToString()
        {
            return this.ItemName;
        }
    }
}
