using System;
namespace QuickBooks.DataAccess
{
    public interface IFileSystemRepository
    {
        void DeletePendingOrderByKey(string key);
        System.Collections.Generic.Dictionary<string, QuickBooks.BusObj.CustomerOrderObject> GetAllPendingOrders(bool forceRefresh);
        QuickBooks.BusObj.CustomerOrderObject GetPendingOrderByFilename(string file);
        QuickBooks.BusObj.CustomerOrderObject GetPendingOrderByKey(bool forceRefresh, string fileKey);
        QuickBooks.BusObj.CustomerOrderObject GetPendingOrderByKey(string fileKey);
        QuickBooks.BusObj.PendingOrderFile GetPendingOrderFileByFilename(string filename);
        QuickBooks.BusObj.FileSet GetPendingOrderFileSet(bool forceRefresh);
        string SavePendingOrder(QuickBooks.BusObj.CustomerOrderObject coo);
    }
}
