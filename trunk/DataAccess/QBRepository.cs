using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QBFC8Lib;
using QuickBooks.BusObj;
using QuickBooks.Util;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace QuickBooks.DataAccess
{
    public class QBRepository : QuickBooks.DataAccess.IQBRepository
    {
        IQBSessionManager _qbSession = new QBSessionManagerClass();

        ILogger _logger;
        ISettings _settings;

        public QBRepository(ILogger logger, ISettings settings)
        {
            _logger = logger;
            _settings = settings;
        }

        public bool HasValidConnection()
        {
            bool isValid = true;
            try
            {
                this.OpenSession();
                Thread.Sleep(2000);
                this.CloseSession();
            }
            catch (Exception)
            {
                isValid = false;
                
            }

            return isValid;

        }

        private void OpenSession()
        {
            try
            {
                _qbSession = new QBSessionManagerClass();
                _qbSession.OpenConnection("", _settings.QbAppName);
                _qbSession.BeginSession(_settings.QuickBooksFilePath, ENOpenMode.omDontCare);
            }
            catch (Exception ex)
            {
                _logger.LogException("Error opening QB Session", ex);
                throw new QuickBooksException("Error opening QuickBooks session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        List<OrderItem> _allItems = new List<OrderItem>();

        public List<OrderItem> GetAllSalesItems(bool forceRefresh)
        {
            if (forceRefresh && _allItems != null && _allItems.Count > 0)
                return _allItems;

            OpenSession();

            List<OrderItem> nonInv;
            List<OrderItem> inv;
            List<OrderItem> service;

            try
            {
                nonInv = GetNonInventoryItems();
                inv = GetInventoryItems();
                service = GetServiceItems();

                

                List<OrderItem> result = new List<OrderItem>();
                result.AddRange(nonInv);
                result.AddRange(inv);
                result.AddRange(service);

                _allItems = result.OrderBy(r => r.ItemName).ToList();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                CloseSession();
            }



            return _allItems;
        }

        List<OrderItem> GetNonInventoryItems()
        {

            List<OrderItem> result = new List<OrderItem>();

            try
            {

                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);

                msgSetRequest.ClearRequests();
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;

                IItemNonInventoryQuery itemNonInventoryQuery = msgSetRequest.AppendItemNonInventoryQueryRq();

                itemNonInventoryQuery.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asAll);
                itemNonInventoryQuery.ORListQuery.ListFilter.MaxReturned.SetValue(100);
                
                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);
                if (((msgSetResponse == null) || (msgSetResponse.ResponseList == null)) || (msgSetResponse.ResponseList.Count <= 0))
                {
                    throw new Exception();
                }
                IResponseList responseList = msgSetResponse.ResponseList;
                if (responseList.Count != 1)
                {
                    throw new Exception();
                }
                IResponse response = responseList.GetAt(0);
                if (response == null)
                {
                    throw new Exception();
                }
                if (response.StatusCode != 0)
                {
                    _logger.Log("GetNonInventoryItems status code was " + response.StatusCode.ToString());
                }
                if (response.Type == null)
                {
                    throw new Exception();
                }
                if (response.Detail == null)
                {
                    throw new Exception();
                }
                if (response.Detail.Type == null)
                {
                    throw new Exception();
                }
                if (!(response.Detail is IItemNonInventoryRetList))
                {
                    throw new Exception();
                }

                IItemNonInventoryRetList itemNonInventoryRetList = (IItemNonInventoryRetList)response.Detail;
                
                for (int i = 0; i < itemNonInventoryRetList.Count; i++)
                {
                    OrderItem or = new OrderItem();

                    IItemNonInventoryRet itemNonInventoryRet = itemNonInventoryRetList.GetAt(i);
                    
                    or.ItemName = itemNonInventoryRet.FullName.GetValue();
                    try
                    {
                        or.Description = itemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();
                    }
                    catch (Exception)
                    {
                        or.Description = "";
                    }
                    or.ItemID = itemNonInventoryRet.ListID.GetValue();
                    or.Account = itemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.GetValue();
                    or.Price = (double)itemNonInventoryRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue();
                    result.Add(or);
                }

                
            }
            catch (Exception ex)
            {

                _logger.LogException("Error in GetNonInventoryItems.", ex);
                
            }
            return result;
        }

        List<OrderItem> GetInventoryItems()
        {
            List<OrderItem> result = new List<OrderItem>();

            try
            {


                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);

                msgSetRequest.ClearRequests();
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;

                IItemInventoryQuery itemInventoryQuery = msgSetRequest.AppendItemInventoryQueryRq();

                itemInventoryQuery.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asAll);
                itemInventoryQuery.ORListQuery.ListFilter.MaxReturned.SetValue(100);

                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);
                if (((msgSetResponse == null) || (msgSetResponse.ResponseList == null)) || (msgSetResponse.ResponseList.Count <= 0))
                {
                    throw new Exception();
                }
                IResponseList responseList = msgSetResponse.ResponseList;
                if (responseList.Count != 1)
                {
                    throw new Exception();
                }
                IResponse response = responseList.GetAt(0);
                if (response == null)
                {
                    throw new Exception();
                }
                if (response.StatusCode != 0)
                {
                    _logger.Log("GetInventoryItems status code was " + response.StatusCode.ToString());
                                    }
                if (response.Type == null)
                {
                    throw new Exception();
                }
                if (response.Detail == null)
                {
                    throw new Exception();
                }
                if (response.Detail.Type == null)
                {
                    throw new Exception();
                }

                if (!(response.Detail is IItemInventoryRetList))
                {
                    throw new Exception();
                }

                IItemInventoryRetList itemInventoryRetList = (IItemInventoryRetList)response.Detail;

                for (int i = 0; i < itemInventoryRetList.Count; i++)
                {
                    OrderItem or = new OrderItem();

                    IItemInventoryRet itemInventoryRet = itemInventoryRetList.GetAt(i);

                    or.ItemName = itemInventoryRet.FullName.GetValue();
                    try
                    {
                        or.Description = itemInventoryRet.SalesDesc.GetValue();
                    }
                    catch (Exception)
                    {
                        or.Description = "";
                    }
                    or.ItemID = itemInventoryRet.ListID.GetValue();
                    or.Account = itemInventoryRet.IncomeAccountRef.FullName.GetValue();
                    or.Price = (double)itemInventoryRet.SalesPrice.GetValue();
                    result.Add(or);
                }


                
            }
            catch (Exception ex)
            {

                _logger.LogException("Error in GetInventoryItems.", ex);
            }
            return result;
        }

        List<OrderItem> GetServiceItems()
        {
            List<OrderItem> result = new List<OrderItem>();

            try
            {
                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);

                msgSetRequest.ClearRequests();
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;

                IItemServiceQuery itemServiceQuery = msgSetRequest.AppendItemServiceQueryRq();

                itemServiceQuery.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asAll);
                itemServiceQuery.ORListQuery.ListFilter.MaxReturned.SetValue(100);

                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);
                if (((msgSetResponse == null) || (msgSetResponse.ResponseList == null)) || (msgSetResponse.ResponseList.Count <= 0))
                {
                    
                    throw new Exception();
                }
                IResponseList responseList = msgSetResponse.ResponseList;
                if (responseList.Count != 1)
                {
                    throw new Exception();
                }
                IResponse response = responseList.GetAt(0);
                if (response == null)
                {
                    throw new Exception();
                }
                if (response.StatusCode != 0)
                {
                    _logger.Log("GetServiceItems status code was " + response.StatusCode.ToString());
                }
                if (response.Type == null)
                {
                    throw new Exception();
                }
                if (response.Detail == null)
                {
                    throw new Exception();
                }
                if (response.Detail.Type == null)
                {
                    throw new Exception();
                }

                if (!(response.Detail is IItemServiceRetList))
                {
                    throw new Exception();
                }

                var itemServiceRetList = (IItemServiceRetList)response.Detail;

                for (int i = 0; i < itemServiceRetList.Count; i++)
                {
                    OrderItem or = new OrderItem();

                    var itemServiceRet = itemServiceRetList.GetAt(i);

                    or.ItemName = itemServiceRet.FullName.GetValue();
                    try
                    {
                        or.Description = itemServiceRet.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();
                    }
                    catch (Exception)
                    {
                        or.Description = "";
                    }
                    or.ItemID = itemServiceRet.ListID.GetValue();
                    or.Account = itemServiceRet.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.GetValue();
                    or.Price = (double)itemServiceRet.ORSalesPurchase.SalesOrPurchase.ORPrice.Price.GetValue();
                    result.Add(or);
                }
 
               
            }
            catch (Exception ex)
            {

                _logger.LogException("Error in GetServiceItems.", ex);
            }
            return result;
        }

        public Customer GetCustomerByID(string id)
        {
            Customer customer = new Customer();

            try
            {
                OpenSession();
                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);
                msgSetRequest.ClearRequests();

                ICustomerQuery customerQuery = msgSetRequest.AppendCustomerQueryRq();
                customerQuery.ORCustomerListQuery.ListIDList.Add(id);
                customerQuery.OwnerIDList.Add(_settings.CustomerPrivateFieldsID);
                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);
                if (((msgSetResponse == null) || (msgSetResponse.ResponseList == null)) || (msgSetResponse.ResponseList.Count <= 0))
                {
                    throw new Exception();
                }
                IResponseList responseList = msgSetResponse.ResponseList;
                if (responseList.Count != 1)
                {
                    throw new Exception();
                }
                IResponse response = responseList.GetAt(0);
                if (response == null)
                {
                    throw new Exception();
                }
                if (response.StatusCode != 0)
                {
                    _logger.Log("Error in GetAllCustomersByID." + Environment.NewLine +
                        "Status Code: " + response.StatusCode);
                }
                if (response.Type == null)
                {
                    throw new Exception();
                }
                if (response.Detail == null)
                {
                    throw new Exception();
                }
                if (response.Detail.Type == null)
                {
                    throw new Exception();
                }
                ICustomerRetList customerRetList = (ICustomerRetList)response.Detail;
                if (customerRetList.Count > 1)
                {
                    _logger.Log("More than one customer found with id " + id + " in method GetCustomerById");
                    throw new Exception();
                }
                if (customerRetList.Count == 0)
                {
                    _logger.Log("Customer with ID " + id + " not found in GetAllCustomersByID method");
                    throw new Exception();
                }

                var cust = customerRetList.GetAt(0);

                if (cust.FullName != null && !cust.FullName.IsEmpty())
                    customer.FullName = cust.FullName.GetValue();

                if (cust.AltContact != null && !cust.AltContact.IsEmpty())
                    customer.AltContact = cust.AltContact.GetValue();

                if (cust.AltPhone != null && !cust.AltPhone.IsEmpty())
                    customer.AltPhone = cust.AltPhone.GetValue();

                if (cust.Email != null && !cust.Email.IsEmpty())
                    customer.Email = cust.Email.GetValue();

                if (cust.ListID != null && !cust.ListID.IsEmpty())
                    customer.CustomerID = cust.ListID.GetValue().ToString();

                if (cust.EditSequence != null && !cust.EditSequence.IsEmpty())
                    customer.EditSequence = cust.EditSequence.GetValue();

                if (cust.Phone != null && !cust.Phone.IsEmpty())
                    customer.Phone = cust.Phone.GetValue();
                if (cust.BillAddress != null)
                {
                    var ba = cust.BillAddress;
                    if (ba.Addr1 != null && !ba.Addr1.IsEmpty())
                        customer.BillingAddress.Line1 = cust.BillAddress.Addr1.GetValue();
                    if (ba.Addr2 != null && !ba.Addr2.IsEmpty())
                        customer.BillingAddress.Line2 = Environment.NewLine + ba.Addr2.GetValue();
                    if (ba.Addr3 != null && !ba.Addr3.IsEmpty())
                        customer.BillingAddress.Line3 = Environment.NewLine + ba.Addr3.GetValue();
                    if (ba.Addr4 != null && !ba.Addr4.IsEmpty())
                        customer.BillingAddress.Line4 = Environment.NewLine + ba.Addr4.GetValue();

                    if (ba.City != null && !ba.City.IsEmpty())
                        customer.BillingAddress.City = ba.City.GetValue();
                    if (ba.State != null && !ba.State.IsEmpty())
                        customer.BillingAddress.State = ba.State.GetValue();
                    if (ba.PostalCode != null && !ba.PostalCode.IsEmpty())
                        customer.BillingAddress.Zip = ba.PostalCode.GetValue();
                }


                if (cust.ShipAddress != null)
                {
                    var sa = cust.ShipAddress;
                    if (sa.Addr1 != null && !sa.Addr1.IsEmpty())
                        customer.ShippingAddress.Line1 = sa.Addr1.GetValue();
                    if (sa.Addr2 != null && !sa.Addr2.IsEmpty())
                        customer.ShippingAddress.Line2 = sa.Addr2.GetValue();
                    if (sa.Addr3 != null && !sa.Addr3.IsEmpty())
                        customer.ShippingAddress.Line3 = sa.Addr3.GetValue();
                    if (sa.Addr4 != null && !sa.Addr4.IsEmpty())
                        customer.ShippingAddress.Line4 = sa.Addr4.GetValue();

                    if (sa.City != null && !sa.City.IsEmpty())
                        customer.ShippingAddress.City = sa.City.GetValue();
                    if (sa.State != null && !sa.State.IsEmpty())
                        customer.ShippingAddress.State = sa.State.GetValue();
                    if (sa.PostalCode != null && !sa.PostalCode.IsEmpty())
                        customer.ShippingAddress.Zip = sa.PostalCode.GetValue();
                }

                bool fieldsExist = false;
                if (cust.DataExtRetList != null)
                {
                    for (int i = 0; i < cust.DataExtRetList.Count; i++)
                    {
                        if (cust.DataExtRetList.GetAt(i).DataExtName.GetValue().ToString().Equals("CardNameType1"))
                        {
                            fieldsExist = true;
                            break;
                        }
                    }
                }

                if (!fieldsExist)
                {
                    customer.CreditCardFieldsExistInQB = fieldsExist;
                }
                else
                {
                    customer.CreditCardFieldsExistInQB = fieldsExist;
                    for (int j = 0; j < cust.DataExtRetList.Count; j++)
                    {
                        CreditCard cc1 = customer.CreditCards[0];
                        CreditCard cc2 = customer.CreditCards[1];
                        CreditCard cc3 = customer.CreditCards[2];

                        IDataExtRet field = cust.DataExtRetList.GetAt(j);
                        string name = field.DataExtName.GetValue();
                        string val = field.DataExtValue.GetValue();
                        if (name.StartsWith("CardNameType"))
                        {
                            string[] pieces = val.Split(":".ToCharArray());
                            if (pieces.Length >= 2)
                            {
                                Enums.CreditCardType ccType;

                                string type = string.Empty;
                                if (pieces[0].StartsWith("V"))
                                    ccType = Enums.CreditCardType.Visa;
                                else if (pieces[0].StartsWith("M"))
                                    ccType = Enums.CreditCardType.Mastercard;
                                else if (pieces[0].StartsWith("D"))
                                    ccType = Enums.CreditCardType.Discover;
                                else if (pieces[0].StartsWith("A"))
                                    ccType = Enums.CreditCardType.Amex;
                                else ccType = Enums.CreditCardType.UnknownOrNotSet;

                                if (name.EndsWith("1"))
                                {
                                    cc1.CreditCardType = ccType;
                                    cc1.CardholderName = pieces[1];
                                }
                                if (name.EndsWith("2"))
                                {
                                    cc2.CreditCardType = ccType;
                                    cc2.CardholderName = pieces[1];
                                }
                                if (name.EndsWith("3"))
                                {
                                    cc3.CreditCardType = ccType;
                                    cc3.CardholderName = pieces[1];
                                }
                            }
                        }
                        else if (name.StartsWith("CardNumExp"))
                        {
                            string[] pieces = val.Split(":".ToCharArray());
                            if (pieces.Length >= 2)
                            {
                                if (name.EndsWith("1"))
                                {
                                    cc1.CardNumber = pieces[0];

                                    try
                                    {
                                        cc1.ExpirationDate = pieces[1];
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogException(ex);
                                    }
                                }
                                else if (name.EndsWith("2"))
                                {
                                    cc2.CardNumber = pieces[0];
                                    try
                                    {
                                        cc2.ExpirationDate = pieces[1];
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogException(ex);
                                    }
                                }
                                else if (name.EndsWith("3"))
                                {
                                    cc2.CardNumber = pieces[0];
                                    try
                                    {
                                        cc3.ExpirationDate = pieces[1];
                                    }
                                    catch (Exception ex)
                                    {
                                        _logger.LogException(ex);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogException("Error in GetCustomerById with cust id of " + id, ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }

            return customer;
        }

        private List<Customer> GetAllCustomersPersonalInfo()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                OpenSession();
                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);
                msgSetRequest.ClearRequests();

                ICustomerQuery customerQuery = msgSetRequest.AppendCustomerQueryRq();
                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);
                if (((msgSetResponse == null) || (msgSetResponse.ResponseList == null)) || (msgSetResponse.ResponseList.Count <= 0))
                {
                    throw new Exception();
                }
                IResponseList responseList = msgSetResponse.ResponseList;
                if (responseList.Count != 1)
                {
                    throw new Exception();
                }
                IResponse response = responseList.GetAt(0);
                if (response == null)
                {
                    throw new Exception();
                }
                if (response.StatusCode != 0)
                {
                    _logger.Log("Error in GetAllCustomers." + Environment.NewLine +
                        "Status Code: " + response.StatusCode);
                    throw new Exception();
                }
                if (response.Type == null)
                {
                    throw new Exception();
                }
                if (response.Detail == null)
                {
                    throw new Exception();
                }
                if (response.Detail.Type == null)
                {
                    throw new Exception();
                }
                ICustomerRetList customerRetList = (ICustomerRetList)response.Detail;
                for (int i = 0; i < customerRetList.Count; i++)
                {
                    var cust = customerRetList.GetAt(i);

                    Customer customer = new Customer();
                    if (customer == null)
                    {
                        _logger.Log("Error GetAllCustomers.");
                        CloseSession();
                        throw new Exception("Error finding customer by ID");
                    }

                    if (cust.FullName != null && !cust.FullName.IsEmpty())
                        customer.FullName = cust.FullName.GetValue();

                    if (cust.AltContact != null && !cust.AltContact.IsEmpty())
                        customer.AltContact = cust.AltContact.GetValue();

                    if (cust.AltPhone != null && !cust.AltPhone.IsEmpty())
                        customer.AltPhone = cust.AltPhone.GetValue();

                    if (cust.Email != null && !cust.Email.IsEmpty())
                        customer.Email = cust.Email.GetValue();

                    if (cust.ListID != null && !cust.ListID.IsEmpty())
                        customer.CustomerID = cust.ListID.GetValue().ToString();

                    if (cust.EditSequence != null && !cust.EditSequence.IsEmpty())
                        customer.EditSequence = cust.EditSequence.GetValue();

                    if (cust.Phone != null && !cust.Phone.IsEmpty())
                        customer.Phone = cust.Phone.GetValue();

                    customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                _logger.LogException("Error in GetallCustomersPersonalInfo()", ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }
            
            return customers;
        }

        public  List<Customer> SearchCustomers(string searchText)
        {
            bool useFirstAndLast = false;
            
            string first = string.Empty;
            string last = string.Empty;
            
            var searchPieces = searchText.Split(" ".ToCharArray()).ToList();

            if (searchPieces.Count == 2)
            {
                useFirstAndLast = true;

                first = searchPieces[0];
                last = searchPieces[1];
            }


            List<Customer> customers = new List<Customer>();

            try
            {
                OpenSession();
                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);
                msgSetRequest.ClearRequests();

                ICustomerQuery customerQuery = msgSetRequest.AppendCustomerQueryRq();
                customerQuery.ORCustomerListQuery.CustomerListFilter.MaxReturned.SetValue(150);
                customerQuery.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asAll);
                customerQuery.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.Name.SetValue(searchText);

                customerQuery.ORCustomerListQuery.CustomerListFilter.ORNameFilter.NameFilter.MatchCriterion.SetValue(ENMatchCriterion.mcContains);
                customerQuery.OwnerIDList.Add(_settings.CustomerPrivateFieldsID);

                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);
                if (((msgSetResponse == null) || (msgSetResponse.ResponseList == null)) || (msgSetResponse.ResponseList.Count <= 0))
                {
                    throw new Exception();
                }
                IResponseList responseList = msgSetResponse.ResponseList;
                if (responseList.Count != 1)
                {
                    throw new Exception();
                }
                IResponse response = responseList.GetAt(0);
                if (response == null)
                {
                    throw new Exception();
                }
                if (response.StatusCode != 0)
                {
                    _logger.Log("Error in GetAllCustomers." + Environment.NewLine +
                        "Status Code: " + response.StatusCode);
                    throw new Exception();
                }
                if (response.Type == null)
                {
                    throw new Exception();
                }
                if (response.Detail == null)
                {
                    throw new Exception();
                }
                if (response.Detail.Type == null)
                {
                    throw new Exception();
                }
                ICustomerRetList customerRetList = (ICustomerRetList)response.Detail;
                for (int i = 0; i < customerRetList.Count; i++)
                {
                    var cust = customerRetList.GetAt(i);

                    Customer customer = new Customer();
                    if (customer == null)
                    {
                        _logger.Log("Error GetAllCustomers.");
                        CloseSession();
                        throw new Exception("Error finding customer by ID");
                    }

                    if (cust.FullName != null && !cust.FullName.IsEmpty())
                        customer.FullName = cust.FullName.GetValue();

                    if (cust.AltContact != null && !cust.AltContact.IsEmpty())
                        customer.AltContact = cust.AltContact.GetValue();

                    if (cust.AltPhone != null && !cust.AltPhone.IsEmpty())
                        customer.AltPhone = cust.AltPhone.GetValue();

                    if (cust.Email != null && !cust.Email.IsEmpty())
                        customer.Email = cust.Email.GetValue();

                    if (cust.ListID != null && !cust.ListID.IsEmpty())
                        customer.CustomerID = cust.ListID.GetValue().ToString();

                    if (cust.EditSequence != null && !cust.EditSequence.IsEmpty())
                        customer.EditSequence = cust.EditSequence.GetValue();

                    if (cust.Phone != null && !cust.Phone.IsEmpty())
                        customer.Phone = cust.Phone.GetValue();
                    if (cust.BillAddress != null)
                    {
                        var ba = cust.BillAddress;
                        if (ba.Addr1 != null && !ba.Addr1.IsEmpty())
                            customer.BillingAddress.Line1 = cust.BillAddress.Addr1.GetValue();
                        if (ba.Addr2 != null && !ba.Addr2.IsEmpty())
                            customer.BillingAddress.Line2 = Environment.NewLine + ba.Addr2.GetValue();
                        if (ba.Addr3 != null && !ba.Addr3.IsEmpty())
                            customer.BillingAddress.Line3 = Environment.NewLine + ba.Addr3.GetValue();
                        if (ba.Addr4 != null && !ba.Addr4.IsEmpty())
                            customer.BillingAddress.Line4 = Environment.NewLine + ba.Addr4.GetValue();

                        if (ba.City != null && !ba.City.IsEmpty())
                            customer.BillingAddress.City = ba.City.GetValue();
                        if (ba.State != null && !ba.State.IsEmpty())
                            customer.BillingAddress.State = ba.State.GetValue();
                        if (ba.PostalCode != null && !ba.PostalCode.IsEmpty())
                            customer.BillingAddress.Zip = ba.PostalCode.GetValue();
                    }


                    if (cust.ShipAddress != null)
                    {
                        var sa = cust.ShipAddress;
                        if (sa.Addr1 != null && !sa.Addr1.IsEmpty())
                            customer.ShippingAddress.Line1 = sa.Addr1.GetValue();
                        if (sa.Addr2 != null && !sa.Addr2.IsEmpty())
                            customer.ShippingAddress.Line2 = sa.Addr2.GetValue();
                        if (sa.Addr3 != null && !sa.Addr3.IsEmpty())
                            customer.ShippingAddress.Line3 = sa.Addr3.GetValue();
                        if (sa.Addr4 != null && !sa.Addr4.IsEmpty())
                            customer.ShippingAddress.Line4 = sa.Addr4.GetValue();

                        if (sa.City != null && !sa.City.IsEmpty())
                            customer.ShippingAddress.City = sa.City.GetValue();
                        if (sa.State != null && !sa.State.IsEmpty())
                            customer.ShippingAddress.State = sa.State.GetValue();
                        if (sa.PostalCode != null && !sa.PostalCode.IsEmpty())
                            customer.ShippingAddress.Zip = sa.PostalCode.GetValue();
                    }

                    if (cust.DataExtRetList != null)
                    {
                        for (int j = 0; j < cust.DataExtRetList.Count; j++)
                        {
                            CreditCard cc1 = customer.CreditCards[0];
                            CreditCard cc2 = customer.CreditCards[1];
                            CreditCard cc3 = customer.CreditCards[2];

                            IDataExtRet field = cust.DataExtRetList.GetAt(j);
                            string name = field.DataExtName.GetValue();
                            string val = field.DataExtValue.GetValue();
                            if (name.StartsWith("CardNameType"))
                            {
                                string[] pieces = val.Split(":".ToCharArray());
                                string cardholderName = string.Empty;
                                Enums.CreditCardType ccType;

                                if (pieces.Length >= 2)
                                {
                                    cardholderName = pieces[1];
                                    string type = string.Empty;
                                    if (pieces[0].StartsWith("V"))
                                        ccType = Enums.CreditCardType.Visa;
                                    else if (pieces[0].StartsWith("M"))
                                        ccType = Enums.CreditCardType.Mastercard;
                                    else if (pieces[0].StartsWith("D"))
                                        ccType = Enums.CreditCardType.Discover;
                                    else if (pieces[0].StartsWith("A"))
                                        ccType = Enums.CreditCardType.Amex;
                                    else ccType = Enums.CreditCardType.UnknownOrNotSet;
                                }
                                else
                                {
                                    ccType = Enums.CreditCardType.UnknownOrNotSet;
                                    cardholderName = string.Empty;
                                }
                                if (name.EndsWith("1"))
                                {
                                    cc1.CreditCardType = ccType;
                                    cc1.CardholderName = cardholderName;
                                }
                                if (name.EndsWith("2"))
                                {
                                    cc2.CreditCardType = ccType;
                                    cc2.CardholderName = cardholderName;
                                }
                                if (name.EndsWith("3"))
                                {
                                    cc3.CreditCardType = ccType;
                                    cc3.CardholderName = cardholderName;
                                }
                            }
                            else if (name.StartsWith("CardNumExp"))
                            {
                                string[] pieces = val.Split(":".ToCharArray());
                                if (pieces.Length >= 2)
                                {
                                    if (name.EndsWith("1"))
                                    {
                                        cc1.CardNumber = pieces[0];

                                        cc1.ExpirationDate = pieces[1];

                                    }
                                    else if (name.EndsWith("2"))
                                    {
                                        cc2.CardNumber = pieces[0];

                                        cc2.ExpirationDate = pieces[1];

                                    }
                                    else if (name.EndsWith("3"))
                                    {
                                        cc3.CardNumber = pieces[0];
                                        cc3.ExpirationDate = pieces[1];
                                    }
                                }
                            }

                        }
                    }
                    if (customer != null)
                        customers.Add(customer);
                }

            }
            catch (QuickBooksException qbe)
            {
                throw qbe;
            }
            catch (Exception ex)
            {
                _logger.LogException("Error in GetAllCustomers.", ex);
            }
            finally
            {
                CloseSession();
            }

            return customers;
        }

        public string SaveInvoice(CustomerOrderObject coo)
        {
            Customer customer = null;
            string s = string.Empty;
            bool custExists = false;

            if (!string.IsNullOrEmpty(coo.Customer.CustomerID))
            {
                try
                {
                    customer = this.GetCustomerByID(coo.Customer.CustomerID);
                }
                catch (Exception)
                {
                }
            }

            if (customer != null)
                custExists = true;

            if (!custExists)
            {
                string id = this.AddCustomer(coo.Customer);
                customer = this.GetCustomerByID(id);
                coo.Customer.CustomerID = id;
            }
            else //customer does exist
            {
                string listID = string.Empty;
                listID = customer.CustomerID;

                if (!string.IsNullOrEmpty(listID))
                {
                    this.ModifyCustomer(coo.Customer, false);
                    if (customer.CreditCardFieldsExistInQB)
                        this.ModifyCreditCardsForCustomer(coo.Customer.CreditCards, customer.CustomerID);
                    else
                        AddCreditCardsForCustomer(coo.Customer.CreditCards, listID);
                }
            }

            QBSessionManager session = new QBSessionManagerClass();
            IMsgSetRequest requestMsgSet = session.CreateMsgSetRequest("US", 8, 0);
            try
            {
                requestMsgSet.ClearRequests();
                requestMsgSet.Attributes.OnError = ENRqOnError.roeStop;
                session.OpenConnection("", _settings.QbAppName);
                session.BeginSession(_settings.QuickBooksFilePath, ENOpenMode.omDontCare);
                IInvoiceAdd invoice = requestMsgSet.AppendInvoiceAddRq();

                invoice.CustomerRef.ListID.SetValue(customer.CustomerID);
                invoice.ShipAddress.Addr1.SetValue(coo.Customer.ShippingAddress.Line1);
                invoice.ShipAddress.Addr2.SetValue(coo.Customer.ShippingAddress.Line2);
                invoice.ShipAddress.Addr3.SetValue(coo.Customer.ShippingAddress.Line3);
                invoice.ShipAddress.Addr4.SetValue(coo.Customer.ShippingAddress.Line4);

                invoice.ShipAddress.City.SetValue(coo.Customer.ShippingAddress.City);
                invoice.ShipAddress.State.SetValue(coo.Customer.ShippingAddress.State);
                invoice.ShipAddress.PostalCode.SetValue(coo.Customer.ShippingAddress.Zip);

                Address billing = coo.Customer.BillingAddress;
                invoice.BillAddress.Addr1.SetValue(billing.Line1);
                invoice.BillAddress.Addr2.SetValue(billing.Line2);
                invoice.BillAddress.Addr3.SetValue(billing.Line3);
                invoice.BillAddress.Addr4.SetValue(billing.Line4);

                invoice.BillAddress.City.SetValue(billing.City);
                invoice.BillAddress.State.SetValue(billing.State);
                invoice.BillAddress.PostalCode.SetValue(billing.Zip);
                invoice.TxnDate.SetValue(coo.Order.OrderDate);  //TODO confirm how this date gets set
                invoice.PONumber.SetValue(coo.Customer.PoBox);
                invoice.IsToBePrinted.SetValue(true);
                invoice.Memo.SetValue(coo.Vehicle.Year.ToString() + " " + coo.Vehicle.Make + " " + coo.Vehicle.Model);
                if (coo.Customer.BillingAddress.State == _settings.TaxableState)
                    invoice.ItemSalesTaxRef.FullName.SetValue(_settings.InStateTaxCodeName);
                else
                    invoice.ItemSalesTaxRef.FullName.SetValue(_settings.OutOfStateTaxCodeName);

                foreach (var lineItem in coo.Order.OrderItems)
                {
                    IORInvoiceLineAdd invoiceLine = invoice.ORInvoiceLineAddList.Append();
                    invoiceLine.InvoiceLineAdd.Quantity.SetValue(lineItem.Quantity);
                    invoiceLine.InvoiceLineAdd.ORRatePriceLevel.Rate.SetValue(lineItem.Price);
                    invoiceLine.InvoiceLineAdd.Desc.SetValue(lineItem.Description);
                    invoiceLine.InvoiceLineAdd.ItemRef.ListID.SetValue(lineItem.ItemID);

                    if (lineItem.IsTaxed)
                        invoiceLine.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue("Tax");
                    else
                        invoiceLine.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue("Non");
                }

                IResponse response = session.DoRequests(requestMsgSet).ResponseList.GetAt(0);
                IInvoiceRet invoiceRet = (IInvoiceRet)response.Detail;
                if(invoiceRet == null)
                {
                    string msg = string.Format("Error adding invoice.{0}Status = {1}{0}Response={2}", Environment.NewLine,response.StatusCode, response.StatusMessage);
                    _logger.Log(msg);
                    throw new QuickBooksException(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                s = invoiceRet.RefNumber.GetValue();
                return s;
            }
            catch(QuickBooksException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogException("Error in Save Invoice method.", ex);
                throw new QuickBooksException("There was an error saving the invoice.  See log for details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                session.EndSession();
                session.CloseConnection();
            }
            
        }


        public void ModifyCreditCardsForCustomer(List<CreditCard> creditCards, string customerId)
        {
            if (creditCards.Count == 0)
                return;
            if (creditCards.Count > 3)
                throw new Exception("Error, attempting to modify more than 3 credit cards.");

            OpenSession();
            IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);
            try
            {
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;

                for (int i = 0; i < creditCards.Count; i++)
                {
                    CreditCard cc = creditCards[i];

                    string sCardNameType = string.Format("CardNameType{0}", i + 1);
                    //string sCardNumber = string.Format("CardNumber{0}", i + 1);
                    string sCardNumExpiration = string.Format("CardNumExp{0}", i + 1);

                    IDataExtMod a1 = msgSetRequest.AppendDataExtModRq();
                    a1.OwnerID.SetValue(_settings.CustomerPrivateFieldsID);
                    a1.ORListTxn.ListDataExt.ListDataExtType.SetValue(ENListDataExtType.ldetCustomer);
                    a1.ORListTxn.ListDataExt.ListObjRef.ListID.SetValue(customerId);
                    a1.DataExtName.SetValue(sCardNameType);

                    string sType = string.Empty;
                    if (cc.CreditCardType != Enums.CreditCardType.UnknownOrNotSet)
                        sType = cc.CreditCardType.ToString().Substring(0, 1);

                    a1.DataExtValue.SetValue(sType + ":" + cc.CardholderName);

                    IDataExtMod a2 = msgSetRequest.AppendDataExtModRq();
                    a2.OwnerID.SetValue(_settings.CustomerPrivateFieldsID);
                    a2.ORListTxn.ListDataExt.ListDataExtType.SetValue(ENListDataExtType.ldetCustomer);
                    a2.ORListTxn.ListDataExt.ListObjRef.ListID.SetValue(customerId);
                    a2.DataExtName.SetValue(sCardNumExpiration);
                    a2.DataExtValue.SetValue(cc.CardNumber + ":" + cc.ExpirationDate.ToString());
                }
                IMsgSetResponse resp = _qbSession.DoRequests(msgSetRequest);
                IResponseList list = resp.ResponseList;
                IResponse r = list.GetAt(0);
                int code = r.StatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogException("Error modifying credit card information for customer ID " + customerId, ex);
                throw new QuickBooksException(string.Format("There was an error modifying credit card information for customer ID {0}.", customerId), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseSession();
            }
        }

        public void AddCreditCardsForCustomer(List<CreditCard> creditCards, string customerId)
        {
            if (creditCards.Count == 0)
                return;
            if (creditCards.Count > 3)
                throw new Exception("Error, attempting to add more than 3 credit cards.  There is a maxium of three cards allowed.");

            OpenSession();
            IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);
            try
            {
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;


                for (int i = 0; i < creditCards.Count; i++)
                {
                    CreditCard cc = creditCards[i];

                    string sCardNameType = string.Format("CardNameType{0}", i + 1);
                    string sCardNumExpiration = string.Format("CardNumExp{0}", i + 1);

                    IDataExtAdd a1 = msgSetRequest.AppendDataExtAddRq();
                    a1.OwnerID.SetValue(_settings.CustomerPrivateFieldsID);
                    a1.ORListTxnWithMacro.ListDataExt.ListDataExtType.SetValue(ENListDataExtType.ldetCustomer);
                    a1.ORListTxnWithMacro.ListDataExt.ListObjRef.ListID.SetValue(customerId);
                    a1.DataExtName.SetValue(sCardNameType);

                    string sType = string.Empty;
                    if (cc.CreditCardType != Enums.CreditCardType.UnknownOrNotSet)
                        sType = cc.CreditCardType.ToString().Substring(0, 1);

                    a1.DataExtValue.SetValue(sType + ":" + cc.CardholderName);

                    IDataExtAdd a2 = msgSetRequest.AppendDataExtAddRq();
                    a2.OwnerID.SetValue(_settings.CustomerPrivateFieldsID);
                    a2.ORListTxnWithMacro.ListDataExt.ListDataExtType.SetValue(ENListDataExtType.ldetCustomer);
                    a2.ORListTxnWithMacro.ListDataExt.ListObjRef.ListID.SetValue(customerId);
                    a2.DataExtName.SetValue(sCardNumExpiration);
                    a2.DataExtValue.SetValue(cc.CardNumber + ":" + cc.ExpirationDate.ToString());
                }

                IMsgSetResponse msgSetResp = _qbSession.DoRequests(msgSetRequest);
                IResponseList list = msgSetResp.ResponseList;

                for (int i = 0; i < list.Count; i++)
                {
                    IResponse resp = list.GetAt(0);
                    IDataExtRet data = (IDataExtRet)resp.Detail;

                }
                
                
//                int code = resp.StatusCode;
            }
            catch (Exception ex)
            {
                _logger.LogException("There was an error adding credit card information for customer ID " + customerId, ex);
                throw new QuickBooksException(string.Format("There was an error adding the credit cards for customer ID {0}", customerId), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseSession();
            }
        }

        public void AddExtFields()
        {
            OpenSession();
            IMsgSetRequest msgSetRq = _qbSession.CreateMsgSetRequest("US", 8, 0);
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue;
            try
            {
                for (int i = 0; i <= 2; i++)
                {
                    string sCardNameType = string.Format("CardNameType{0}", i + 1);
                    string sCardNumber = string.Format("CardNumber{0}", i + 1);
                    string sCardNumExpiration = string.Format("CardNumExp1{0}", i + 1);

                    IDataExtDefAdd a = msgSetRq.AppendDataExtDefAddRq();
                    a.OwnerID.SetValue(_settings.CustomerPrivateFieldsID);
                    a.DataExtName.SetValue(sCardNameType);
                    a.DataExtType.SetValue(ENDataExtType.detSTR255TYPE);
                    a.AssignToObjectList.Add(ENAssignToObject.atoCustomer);

                    IDataExtDefAdd b = msgSetRq.AppendDataExtDefAddRq();
                    b.OwnerID.SetValue(_settings.CustomerPrivateFieldsID);
                    b.DataExtName.SetValue(sCardNumExpiration);
                    b.DataExtType.SetValue(ENDataExtType.detSTR255TYPE);
                    b.AssignToObjectList.Add(ENAssignToObject.atoCustomer);
                }
                IMsgSetResponse resp = _qbSession.DoRequests(msgSetRq);
            }
            catch (Exception ex)
            {
                _logger.LogException("Error adding ext fields.", ex);
                throw;
            }
            finally
            {
                CloseSession();
            }
            
        }

        public void ModifyCustomer(Customer c, bool getLatestEditSequence)
        {
            string editSequence = c.EditSequence;
            
            if (getLatestEditSequence)
            {
                var latestCust = GetCustomerByID(c.CustomerID);
                editSequence = latestCust.EditSequence;
            }

            Address billingAddress = c.BillingAddress;
            Address shippingAddress = c.ShippingAddress;

            try
            {
                OpenSession();
                IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;
                ICustomerMod modCust = msgSetRequest.AppendCustomerModRq();

                modCust.ListID.SetValue(c.CustomerID);
                modCust.EditSequence.SetValue(editSequence);

                modCust.Name.SetValue(c.FullName);
                modCust.Phone.SetValue(c.Phone);
                modCust.AltPhone.SetValue(c.AltPhone);
                modCust.AltContact.SetValue(c.AltContact);
                modCust.Email.SetValue(c.Email);

                modCust.ShipAddress.Addr1.SetValue(shippingAddress.Line1);
                modCust.ShipAddress.Addr2.SetValue(shippingAddress.Line2);
                modCust.ShipAddress.Addr3.SetValue(shippingAddress.Line3);
                modCust.ShipAddress.Addr4.SetValue(shippingAddress.Line4);
                modCust.ShipAddress.City.SetValue(shippingAddress.City);
                modCust.ShipAddress.State.SetValue(shippingAddress.State);
                modCust.ShipAddress.PostalCode.SetValue(shippingAddress.Zip);

                modCust.BillAddress.Addr1.SetValue(billingAddress.Line1);
                modCust.BillAddress.Addr2.SetValue(billingAddress.Line2);
                modCust.BillAddress.Addr3.SetValue(billingAddress.Line3);
                modCust.BillAddress.Addr4.SetValue(billingAddress.Line4);
                modCust.BillAddress.City.SetValue(billingAddress.City);
                modCust.BillAddress.State.SetValue(billingAddress.State);
                modCust.BillAddress.PostalCode.SetValue(billingAddress.Zip);

                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);

                IResponseList responseList = msgSetResponse.ResponseList;
                IResponse rsp = responseList.GetAt(0);
                if (rsp.StatusCode == 3200) //3200 means edit sequence code is out of date
                {
                    if (getLatestEditSequence) //do not allow more than one re-try
                    {
                        _logger.Log(string.Format("First attempt to modify customer {0} failed.  Attempted to get the latest edit sequence id for customer and retry the edit but it also failed.", c.FullName));
                        throw new QuickBooksException("Unable to successfully modify customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else //re-try the modification now
                    {
                        CloseSession();
                        _logger.Log(string.Format("Attempting to get the latest edit sequence for customer {0} and retry the ModifyCustomer method.", c.FullName));
                        ModifyCustomer(c, true);
                    }
                }
                else if (rsp.StatusCode != 0)
                {
                    _logger.Log(string.Format("Status code of {0} returned for customer modification for customer {1}", rsp.StatusCode, c.FullName));
                    //throw new QuickBooksException(StatusCode.Failure, string.Format("Error modifying customer {1}", c.FullName), true);  TODO: look at this code again.
                }
                else if (getLatestEditSequence) //success
                {
                    _logger.Log(string.Format("Successfully retrieved latest edit sequence for customer {0}", c.FullName));
                    throw new QuickBooksException("Customer was modified successfully; however, the customer data was stale " +
                        "and had to be merged with newer data." + Environment.NewLine + "It is highly recommended that you perform a customer " +
                        "refresh periodically to prevent this from becoming a regular occurence.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
            }
            catch (QuickBooksException qbe)
            {
                throw qbe;
            }
            catch (Exception ex)
            {
                _logger.LogException(string.Format("Error modifying customer with ID {0} and name {1}", c.CustomerID, c.FullName), ex);
                throw ex;
            }
            finally
            {
                CloseSession();
            }

        }

        public string AddCustomer(Customer c)
        {
            var billingAddress = c.BillingAddress;
            var shippingAddress = c.ShippingAddress;

            OpenSession();
            IMsgSetRequest msgSetRequest = _qbSession.CreateMsgSetRequest("US", 8, 0);

            try
            {
                msgSetRequest.Attributes.OnError = ENRqOnError.roeContinue;
                ICustomerAdd newCust = msgSetRequest.AppendCustomerAddRq();
                newCust.Name.SetValue(c.FullName);
                newCust.Phone.SetValue(c.Phone);
                newCust.AltPhone.SetValue(c.AltPhone);
                newCust.AltContact.SetValue(c.AltContact);
                newCust.Email.SetValue(c.Email);

                newCust.ShipAddress.Addr1.SetValue(shippingAddress.Line1);
                newCust.ShipAddress.Addr2.SetValue(shippingAddress.Line2);
                newCust.ShipAddress.Addr3.SetValue(shippingAddress.Line3);
                newCust.ShipAddress.Addr4.SetValue(shippingAddress.Line4);
                newCust.ShipAddress.City.SetValue(shippingAddress.City);
                newCust.ShipAddress.State.SetValue(shippingAddress.State);
                newCust.ShipAddress.PostalCode.SetValue(shippingAddress.Zip);

                newCust.BillAddress.Addr1.SetValue(billingAddress.Line1);
                newCust.BillAddress.Addr2.SetValue(billingAddress.Line2);
                newCust.BillAddress.Addr3.SetValue(billingAddress.Line3);
                newCust.BillAddress.Addr4.SetValue(billingAddress.Line4);
                newCust.BillAddress.City.SetValue(billingAddress.City);
                newCust.BillAddress.State.SetValue(billingAddress.State);
                newCust.BillAddress.PostalCode.SetValue(billingAddress.Zip);

                IMsgSetResponse msgSetResponse = _qbSession.DoRequests(msgSetRequest);

                IResponseList list = msgSetResponse.ResponseList;
                IResponse resp = list.GetAt(0);
                if (resp.StatusCode != 0)
                {
                    _logger.Log("Error adding customer with name " + c.FullName + ". Status code is " + resp.StatusCode);
                    throw new Exception();
                }
                ICustomerRet cust = (ICustomerRet)resp.Detail;
                string id = cust.ListID.GetValue();

                CloseSession();

                this.AddCreditCardsForCustomer(c.CreditCards, id);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogException(string.Format("Error adding customer with ID {0} and name {1}", c.CustomerID, c.FullName), ex);
                throw new QuickBooksException("There was an error adding the customer to QuickBooks.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseSession();
            }
        }


        private void CloseSession()
        {
            try
            {
                if (_qbSession != null)
                {
                    _qbSession.CloseConnection();
                    _qbSession.EndSession();
                    _qbSession = null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogException("Error closing Quickbooks connection or ending session", ex);
                throw new QuickBooksException("Error closing QB connection or ending QB session", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




    }
}
