using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickBooks.BusObj;
using QuickBooks.Util;
using System.Xml.Linq;
using t = System.Security.Cryptography.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using QuickBooks.Util;

namespace QuickBooks.DataAccess
{
    public class FileSystemRepository : QuickBooks.DataAccess.IFileSystemRepository
    {
        ILogger _logger;
        ISettings _settings;
        IEncryption _encryption;
        IFileSystemRepository _fsRepo;
        Util.Util _util;

        static Dictionary<string, CustomerOrderObject> _cachedOrders = null;

        public FileSystemRepository(ILogger logger, ISettings settings, IEncryption encryption, Util.Util util)
        {
            _logger = logger;
            _settings = settings;
            _encryption = encryption;
            _util = util;
        }

        public CustomerOrderObject GetPendingOrderByKey(bool forceRefresh, string fileKey)
        {
            CustomerOrderObject coo;
            if (forceRefresh)
                coo = this.GetPendingOrderByKey(fileKey);
            else
            {
                if (_cachedOrders.ContainsKey(fileKey))
                    return _cachedOrders[fileKey];
                else return this.GetPendingOrderByKey(fileKey);
            }
            return coo;
        }

        public void DeletePendingOrderByKey(string key)
        {
            try
            {


                string filename = _settings.PendingOrdersPath + "\\" + key + ".inv";
                if (File.Exists(filename))
                    File.Delete(filename);
            }
            catch (Exception ex)
            {
                _logger.LogException("Error deleting file " + key, ex);
                throw new QuickBooksException("Error deleting file.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public CustomerOrderObject GetPendingOrderByKey(string fileKey)
        {
            string file = _settings.PendingOrdersPath + "\\" + fileKey + ".inv";
            return this.GetPendingOrderByFilename(file);
        }

        public CustomerOrderObject GetPendingOrderByFilename(string file)
        {
            try
            {



                CustomerOrderObject coo = new CustomerOrderObject();
                XDocument xDoc = XDocument.Load(file);
                coo.FileKey = xDoc.Root.Attribute("FileKey").Value;
                coo.SaveLocation = _util.ParseEnum<PendingOrderSaveLocation>(xDoc.Root.Attribute("SaveLocation").Value);
                coo.Customer.LoadPersonalInfo(XmlToCustInfo(xDoc.Root.Element("Customer")));
                coo.Customer.BillingAddress = XmlToAddress(xDoc.Root.Element("AddressSet"), Enums.AddressType.Billing);
                coo.Customer.ShippingAddress = XmlToAddress(xDoc.Root.Element("AddressSet"), Enums.AddressType.Shipping);
                coo.Customer.CreditCards = XmlToCreditCardSet(xDoc.Root.Element("CreditCardSet"));
                coo.Order = XmlToOrder(xDoc.Root.Element("Order"));
                coo.Vehicle = XmlToVehicle(xDoc.Root.Element("Vehicle"));

                return coo;

            }
            catch (Exception ex)
            {
                _logger.LogException("Error in GetPendingOrderByFilename.  Filename is " + file, ex);
                throw new QuickBooksException("Error retrieving order from disk.  See log for details.", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public PendingOrderFile GetPendingOrderFileByFilename(string filename)
        {
            var coo = this.GetPendingOrderByFilename(filename);
            var pof = this.CooToPendingOrderFile(coo);
            return pof;
        }

        public FileSet GetPendingOrderFileSet(bool forceRefresh)
        {
            FileSet fs = new FileSet();
            var allPendingOrders = this.GetAllPendingOrders(forceRefresh);
                       
            foreach (var order in allPendingOrders.Values)
            {
                var file = CooToPendingOrderFile(order);
                fs.Add(order.FileKey, file);
            }
            return fs;
        }

        private PendingOrderFile CooToPendingOrderFile(CustomerOrderObject order)
        {
            var file = new PendingOrderFile(order.FileKey, order.Customer.FullName, order.Order.OrderDate, order.SaveLocation);
            return file;
        }

        public Dictionary<string, CustomerOrderObject> GetAllPendingOrders(bool forceRefresh)
        {
            if (!forceRefresh && _cachedOrders != null && _cachedOrders.Count > 0)
                return _cachedOrders;

            List<CustomerOrderObject> coos = new List<CustomerOrderObject>();

            string path = _settings.PendingOrdersPath;

            var files = Directory.GetFiles(path, "*.inv");
            foreach (var file in files)
            {
                var coo = this.GetPendingOrderByFilename(file);
                coos.Add(coo);
            }
            _cachedOrders = coos.ToDictionary<CustomerOrderObject, string>(f => f.FileKey);
            return _cachedOrders;
        }



        public string SavePendingOrder(CustomerOrderObject coo)
        {

            try
            {
                if (string.IsNullOrEmpty(coo.FileKey))
                    coo.FileKey = Guid.NewGuid().ToString("N");

                XElement xCustOrderContainer = new XElement("CustomerOrderContainer",
                                                        new XAttribute("FileKey", coo.FileKey),
                                                        new XAttribute("SaveLocation", coo.SaveLocation.ToString())
                                                        );
                
                XElement xCustomer = CustomerToXml(coo.Customer);
                XElement xVehicle = VehicleToXml(coo.Vehicle);
                XElement xOrder = OrderToXml(coo.Order);
                XElement xCreditCardSet = CreditCardSetToXml(coo.Customer.CreditCards);
                XElement xAddressSet = AddressSetToXml(new List<Address>() { coo.Customer.BillingAddress, coo.Customer.ShippingAddress });

                xCustOrderContainer.Add(xCustomer);
                xCustOrderContainer.Add(xVehicle);
                xCustOrderContainer.Add(xOrder);
                xCustOrderContainer.Add(xCreditCardSet);
                xCustOrderContainer.Add(xAddressSet);

                XDocument xDoc = new XDocument();
                xDoc.Add(xCustOrderContainer);

                xDoc.Save(_settings.PendingOrdersPath + "\\" + coo.FileKey + ".inv");

                return coo.FileKey;
            }
            catch (Exception ex)
            {
                _logger.LogException("Error writing CustomerOrderObject xml file to disk.", ex);
                throw new QuickBooksException(string.Format("Error writing file to disk for customer {0}.  See log for details.", coo.Customer.FullName), "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        #region XML Data Transforation Helper Methods

        ICustomerPersonalInfo XmlToCustInfo(XElement xCustomer)
        {
            ICustomerPersonalInfo cpi = new Customer();
            cpi.CustomerID = xCustomer.Element("CustomerID").Value;
            cpi.EditSequence = xCustomer.Element("EditSequence").Value;
            cpi.FullName = xCustomer.Element("FullName").Value;
            cpi.Phone = xCustomer.Element("Phone").Value;
            cpi.Email = xCustomer.Element("Email").Value;
            cpi.AltPhone = xCustomer.Element("AltPhone").Value;
            cpi.AltContact = xCustomer.Element("AltContact").Value;

            return cpi;
        }

        List<CreditCard> XmlToCreditCardSet(XElement xCC)
        {
            List<CreditCard> creditCards = new List<CreditCard>();

            foreach (var xCreditCard in xCC.Elements("CreditCard"))
            {
                CreditCard cc = new CreditCard();
                cc.CardholderName = xCreditCard.Element("CardholderName").Value;
                cc.CardNumber = _encryption.DecryptData(xCreditCard.Element("CardNumber").Value);
                cc.ExpirationDate = xCreditCard.Element("ExpirationDate").Value;
                cc.Note = xCreditCard.Element("Note").Value;

                try
                {
                    cc.CreditCardType = _util.ParseEnum<QuickBooks.BusObj.Enums.CreditCardType>(xCreditCard.Element("CreditCardType").Value);
                }
                catch (Exception)
                {
                    cc.CreditCardType = Enums.CreditCardType.UnknownOrNotSet;
                }
                creditCards.Add(cc);
            }

            return creditCards;
        }

        Vehicle XmlToVehicle(XElement xVechicle)
        {
            Vehicle v = new Vehicle();
            
            v.Make = xVechicle.Element("Make").Value;
            v.Model = xVechicle.Element("Model").Value;
            v.Row1 = xVechicle.Element("Row1").Value;
            v.Row2 = xVechicle.Element("Row2").Value;
            v.Row3 = xVechicle.Element("Row3").Value;
            v.Trim = xVechicle.Element("Trim").Value;

            int year;
            if (int.TryParse(xVechicle.Element("Year").Value, out year))
                v.Year = year;

            return v;
        }

        Order XmlToOrder(XElement xOrder)
        {
            Order o = new Order();

            double grandTotal;
            double subTotal;
            double taxes;
            DateTime orderDate;

            if (double.TryParse(xOrder.Attribute("GrandTotal").Value, out grandTotal))
                o.GrandTotal = grandTotal;

            if (double.TryParse(xOrder.Attribute("SubTotal").Value, out subTotal))
                o.SubTotal = subTotal;

            if (double.TryParse(xOrder.Attribute("Taxes").Value, out taxes))
                o.Taxes = taxes;

            if (DateTime.TryParse(xOrder.Attribute("OrderDate").Value, out orderDate))
                o.OrderDate = orderDate;

            try
            {
                o.PaymentMethod = _util.ParseEnum<Enums.PaymentMethod>(xOrder.Attribute("PaymentMethod").Value);
            }
            catch (Exception)
            {
                o.PaymentMethod = Enums.PaymentMethod.NotSelected;
            }

            var xOrderItems = xOrder.Elements("OrderItem");

            foreach (var xOrderItem in xOrderItems)
            {

                OrderItem oi = new OrderItem();
                oi.Account = xOrderItem.Element("Account").Value;
                oi.Description = xOrderItem.Element("Description").Value;
                oi.ItemName = xOrderItem.Element("ItemName").Value;
                oi.ItemID = xOrderItem.Element("ItemID").Value;

                double price;
                if (double.TryParse(xOrderItem.Element("Price").Value, out price))
                    oi.Price = price;

                double quantity;
                if (double.TryParse(xOrderItem.Element("Quantity").Value, out quantity))
                    oi.Quantity = quantity;

                double total;
                if (double.TryParse(xOrderItem.Element("Total").Value, out total))
                    oi.Total = total;


                bool taxed = false;
                if (bool.TryParse(xOrderItem.Element("IsTaxed").Value, out taxed))
                    oi.IsTaxed = taxed;

                o.OrderItems.Add(oi);
            }
            return o;

        }

        Address XmlToAddress(XElement xAddressSet, Enums.AddressType addressType)
        {
            var xAddress = (from xA in xAddressSet.Elements("Address")
                            where xA.Element("Type").Value == addressType.ToString()
                            select xA).SingleOrDefault();
            if (xAddress == null)
                return new Address(addressType);

            Address a = new Address(addressType);
            a.Line1 = xAddress.Element("Line1").Value;
            a.Line2 = xAddress.Element("Line2").Value;
            a.Line3 = xAddress.Element("Line3").Value;
            a.Line4 = xAddress.Element("Line4").Value;
            a.State = xAddress.Element("State").Value;
            a.City = xAddress.Element("City").Value;
            a.Zip = xAddress.Element("Zip").Value;

            return a;
        }

        XElement CustomerToXml(Customer c)
        {
            XElement xCust = new XElement("Customer");
            
            xCust.Add(
                        new XElement("CustomerID", c.CustomerID),
                        new XElement("EditSequence", c.EditSequence),
                        new XElement("FullName", c.FullName),
                        new XElement("Phone", c.Phone),
                        new XElement("Email", c.Email),
                        new XElement("AltPhone", c.AltPhone),
                        new XElement("AltContact", c.AltContact)
                        );

            return xCust;
        }

        XElement AddressSetToXml(List<Address> addresses)
        {
            XElement xAddressSet = new XElement("AddressSet");
            foreach (var a in addresses)
            {
                xAddressSet.Add(AddressToXml(a));
            }

            return xAddressSet;
        }

        XElement AddressToXml(Address a)
        {
            XElement xAddress = new XElement("Address",
                new XElement("Type", a.AddressType.ToString()),
                new XElement("Line1", a.Line1),
                new XElement("Line2", a.Line2),
                new XElement("Line3", a.Line3),
                new XElement("Line4", a.Line4),
                new XElement("City", a.City),
                new XElement("State", a.State),
                new XElement("Zip", a.Zip)
                );
            return xAddress;
        }

        XElement CreditCardSetToXml(List<CreditCard> ccs)
        {
            XElement xCCSet = new XElement("CreditCardSet");
            foreach (var cc in ccs)
            {
                xCCSet.Add(CreditCardToXml(cc));
            }
            return xCCSet;
        }

        XElement CreditCardToXml(CreditCard cc)
        {
            string cardNum = cc.CardNumber;
            if (cc.CardNumber != "")
                cardNum = _encryption.EncryptData(cc.CardNumber);

            XElement xCC = new XElement("CreditCard",
                new XElement("CardholderName", cc.CardholderName),
                new XElement("CardNumber", cardNum),
                new XElement("CreditCardType", cc.CreditCardType.ToString()),
                new XElement("ExpirationDate", cc.ExpirationDate),
                new XElement("Note", cc.Note)
                );
            return xCC;
        }

        XElement VehicleToXml(Vehicle v)
        {
            XElement xVechicle = new XElement("Vehicle",
                    new XElement("Make", v.Make),
                    new XElement("Model", v.Model),
                    new XElement("Row1", v.Row1),
                    new XElement("Row2", v.Row2),
                    new XElement("Row3", v.Row3),
                    new XElement("Trim", v.Trim),
                    new XElement("Year", v.Year)
                    );

            return xVechicle;
        }

        XElement OrderToXml(Order o)
        {
            XElement xOrder = new XElement("Order",
                new XAttribute("GrandTotal", o.GrandTotal.ToString()),
                new XAttribute("OrderDate", o.OrderDate.ToShortDateString()),
                new XAttribute("PaymentMethod", o.PaymentMethod.ToString()),
                new XAttribute("SubTotal", o.SubTotal.ToString()),
                new XAttribute("Taxes", o.Taxes)
                );

            foreach (var lineItem in o.OrderItems)
            {
                xOrder.Add(
                    new XElement("OrderItem",
                        new XElement("Account", lineItem.Account),
                        new XElement("Description", lineItem.Description),
                        new XElement("IsTaxed", lineItem.IsTaxed),
                        new XElement("ItemName", lineItem.ItemName),
                        new XElement("Price", lineItem.Price),
                        new XElement("Quantity", lineItem.Quantity),
                        new XElement("ItemID", lineItem.ItemID),
                        new XElement("Total", lineItem.Total)
                    ));
            }
            return xOrder;
        }

        #endregion


    }
}
