using System;
using QuickBooks.BusObj;
using System.Collections.Generic;
namespace QuickBooks.DataAccess
{
    public interface IQBRepository
    {
        void AddCreditCardsForCustomer(System.Collections.Generic.List<QuickBooks.BusObj.CreditCard> creditCards, string customerId);
        string AddCustomer(QuickBooks.BusObj.Customer c);
        void AddExtFields();
        System.Collections.Generic.List<QuickBooks.BusObj.OrderItem> GetAllSalesItems(bool forceRefresh);
        QuickBooks.BusObj.Customer GetCustomerByID(string id);
        void ModifyCreditCardsForCustomer(List<CreditCard> creditCards, string customerId);
        void ModifyCustomer(QuickBooks.BusObj.Customer c, bool getLatestEditSequence);
        string SaveInvoice(QuickBooks.BusObj.CustomerOrderObject coo);
        System.Collections.Generic.List<QuickBooks.BusObj.Customer> SearchCustomers(string searchText);
    }
}
