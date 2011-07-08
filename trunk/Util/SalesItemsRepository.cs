using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickBooks.BusObj;
using System.Xml.Linq;

namespace QuickBooks.Util
{
    public class SalesItemsRepository
    {
        ISettings _settings;
        XDocument _xDocSettings;
        List<OrderItem> _salesItems;

        public SalesItemsRepository(ISettings settings)
        {
            _settings = settings;

            _xDocSettings = XDocument.Load(_settings.SalesItemsPath);

        }


        public List<OrderItem> GetItemsFromDisk()
        {
            List<OrderItem> items = new List<OrderItem>();

            var xItems = _xDocSettings.Root.Element("SalesItems");
            if (xItems == null)
                return items;

            foreach (var item in xItems.Elements("Item"))
            {
                var orderItem = new OrderItem();

                orderItem.Account = item.Attribute("Account").Value;
                orderItem.Description = item.Attribute("Description").Value;
                orderItem.ItemName = item.Attribute("Name").Value;
                orderItem.ItemID = item.Attribute("Id").Value;

                items.Add(orderItem);
            }

            return items;
        }

        public void SaveItemsToDisk(List<OrderItem> itemsToSave)
        {
            var xItems = new XElement("SalesItems");

            foreach (var item in itemsToSave)
            {
                xItems.Add(new XElement("Item",
                                new XAttribute("Name", item.ItemName),
                                new XAttribute("Id", item.ItemID),
                                new XAttribute("Account", item.Account),
                                new XAttribute("Description", item.Description)
                                ));

                                
            }

            //remove any existing SalesItems node
            if (_xDocSettings.Root.Element("SalesItems") != null)
                _xDocSettings.Root.Element("SalesItems").Remove();

            _xDocSettings.Root.Add(xItems);

            _xDocSettings.Save(_settings.SalesItemsPath);
        }
    }
}
