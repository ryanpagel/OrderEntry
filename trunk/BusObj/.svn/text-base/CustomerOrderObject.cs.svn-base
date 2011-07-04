using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickBooks.DataAccess;

namespace QuickBooks.BusObj
{


    public class CustomerOrderObject
    {

        public CustomerOrderObject()
        {
       
            _vehicle = new Vehicle();
            _customer = new Customer();
            _order = new Order();
        }

        string _fileKey;
        public string FileKey
        {
            get { return _fileKey; }
            set { _fileKey = value; }
        }

        PendingOrderSaveLocation _saveLocation;

        public PendingOrderSaveLocation SaveLocation
        {
            get { return _saveLocation; }
            set { _saveLocation = value; }
        }

        Order _order;
        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }

        Vehicle _vehicle;

        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set { _vehicle = value; }
        }

        Customer _customer;

        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }


        
    }
}
