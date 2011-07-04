using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickBooks.DataAccess;

namespace QuickBooks.BusObj
{
    public class PendingOrderFile
    {
        string _fileKey;

        public string FileKey
        {
            get { return _fileKey; }
            set { _fileKey = value; }
        }

        string _fullName;

        public string FullName
        {
            get { return _fullName; }
            set { _fullName = value; }
        }

        DateTime _orderDate;

        public DateTime OrderDate
        {
            get { return _orderDate; }
            set { _orderDate = value; }
        }

        PendingOrderSaveLocation _saveLocation;

        public PendingOrderSaveLocation SaveLocation
        {
            get { return _saveLocation; }
            set { _saveLocation = value; }
        }

        public PendingOrderFile(string key, string custFullName, DateTime orderDate, PendingOrderSaveLocation saveLocation)
        {
            _fileKey = key;
            _fullName = custFullName;
            _orderDate = orderDate;
            _saveLocation = saveLocation;

        }
    }
}
