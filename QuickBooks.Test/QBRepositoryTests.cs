using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QuickBooks.DataAccess;
using QuickBooks.BusObj;
using QuickBooks.Util;

namespace QuickBooks.Test
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class QBRepositoryTest
    {
        public QBRepositoryTest()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void SaveInvoice_NewCustomer()
        {
            var cust = GetPopulatedCustomer();
            string custName = cust.FullName;
            CustomerOrderObject coo = new CustomerOrderObject()
            {
                Customer =  cust,
                Order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem(){
                             ItemID  = "8000005C-1213652981",
                              IsTaxed = false,
                               Description = "test desc",
                                Quantity = 5,
                                 Price  = 10
                                  
                        }
                    }
                }
            };

            ISettings s = GetPopulatedSettingsObj();

            var mockLogger = new Mock<ILogger>();

            var repo = new QBRepository(mockLogger.Object, s);
            repo.SaveInvoice(coo);

            var custs = repo.SearchCustomers(custName);
            Assert.IsTrue(custs[0].CreditCards[0].IsUsed);
            
        }

        private ISettings GetPopulatedSettingsObj()
        {
            ISettings s = new Settings();
            s.MaxLogSizeBytes = 1000000;
            s.InStateTaxCodeName = "OKC Sales Tax";
            s.TaxableRate = .08375;
            s.TaxableState = "OK";
            s.OutOfStateTaxCodeName = "Out of State";
            s.PendingOrdersPath = @"C:\Repositories\QuickBooks\trunk\bin\Debug\PendingOrders";
            s.IsInitialStartup = false;
            s.QbAppName = "CFIApplication";
            s.QuickBooksFilePath = @"C:\Shared\LeatherSeats.com\Customized Factory Interiors3.qbw";
            s.CustomerPrivateFieldsID = "{ABE77B20-5280-4f3b-BB4A-B66E5502D33A}";
            s.AppRootPath = @"C:\Repositories\QuickBooks\trunk\bin\Debug";
            return s;
        }

        Customer GetPopulatedCustomer()
        {
            Customer c = new Customer()
            {
                FullName = RandomString(10),
                CreditCards = new List<CreditCard>(){
                           new CreditCard(){
                                CardholderName = RandomString(10),
                                 CreditCardType = Enums.CreditCardType.Discover,
                                  ExpirationDate = "exp",
                                   CardNumber =  GetRandomNumber(100, 999999).ToString()
                           },
                           new CreditCard(),
                           new CreditCard()
                      }

            };
            return c;
        }

        //[TestMethod]
        //public void SaveInvoice_ExistingCusomter()
        //{
        //    var fullname = RandomString(10);
        //    var mockLogger = new Mock<ILogger>();
        //    var settings = GetPopulatedSettingsObj();
        //    var repo = new QBRepository(mockLogger.Object, settings);
        //    var cust = GetPopulatedCustomer();
        //    cust.FullName = fullname;
        //    repo.SaveInvoice(

        //}

        [TestMethod]
        public void AddCreditCardsToCustomer()
        {
            var mockLogger = new Mock<ILogger>();
            var settings = GetPopulatedSettingsObj();
            var repo = new QBRepository(mockLogger.Object, settings);
            var cust = GetPopulatedCustomer();
            cust.CreditCards[0].CardholderName = RandomString(10);
            cust.CreditCards[0].CardNumber = GetRandomNumber(100, 200).ToString();
            repo.AddCreditCardsForCustomer(cust.CreditCards, "80002E4E-1279074492");
            var custAfter = repo.GetCustomerByID("80002E4E-1279074492");
            Assert.IsTrue(cust.CreditCards[0].CardholderName == custAfter.CreditCards[0].CardholderName);            
        }

        [TestMethod]
        public void AddCreditCardsWithNewCustomer()
        {
            var custOrig = this.GetPopulatedCustomer();
            var mockLogger = new Mock<ILogger>();
            var settings = GetPopulatedSettingsObj();
            var repo = new QBRepository(mockLogger.Object, settings);
                        var id = repo.AddCustomer(custOrig);
            var cust = repo.GetCustomerByID(id);
            Assert.IsTrue(cust.CreditCards[0].CardholderName == custOrig.CreditCards[0].CardholderName);
        }


        [TestMethod]
        public void AddCustomer()
        {
            var mockLogger = new Mock<ILogger>();
            var settings = GetPopulatedSettingsObj();
            var repo = new QBRepository(mockLogger.Object, settings);
            var id = repo.AddCustomer(this.GetPopulatedCustomer());
            var cust = repo.GetCustomerByID(id);
            Assert.IsTrue(cust != null);
        }

        [TestMethod]
        public void ModifyCreditCardInfo()
        {
            string cholderName = RandomString(10);
            string cardnum = GetRandomNumber(1000, 999999).ToString();
            string expiration = GetRandomDay().ToShortDateString();
            string note = RandomString(10);

            var mockLogger = new Mock<ILogger>();
            var settings = GetPopulatedSettingsObj();
            var repo = new QBRepository(mockLogger.Object, settings);
            var existingCustomer = repo.GetCustomerByID("80002E4C-1279071429");
            var newCardType = ChangeCreditCardType(existingCustomer.CreditCards[1].CreditCardType);
            existingCustomer.CreditCards[1].CreditCardType = newCardType;
            existingCustomer.CreditCards[1].CardholderName = cholderName;
            existingCustomer.CreditCards[1].CardNumber = cardnum;
            existingCustomer.CreditCards[1].ExpirationDate = expiration;
            existingCustomer.CreditCards[1].Note = note;

            repo.ModifyCreditCardsForCustomer(existingCustomer.CreditCards, existingCustomer.CustomerID);

            var modifiedCust = repo.GetCustomerByID("80002E4C-1279071429");

            Assert.IsTrue(cholderName == modifiedCust.CreditCards[1].CardholderName);
            Assert.IsTrue(cardnum == modifiedCust.CreditCards[1].CardNumber);
            Assert.IsTrue(expiration == modifiedCust.CreditCards[1].ExpirationDate);
            Assert.IsTrue(newCardType == modifiedCust.CreditCards[1].CreditCardType);
        }

        Enums.CreditCardType ChangeCreditCardType(Enums.CreditCardType type)
        {
            if (type == Enums.CreditCardType.Discover)
            {
                return Enums.CreditCardType.Mastercard;
            }
            else if (type == Enums.CreditCardType.Mastercard)
            {
                return Enums.CreditCardType.Visa;
            }
            else if (type == Enums.CreditCardType.Visa)
            {
                return Enums.CreditCardType.UnknownOrNotSet;
            }
            else if (type == Enums.CreditCardType.UnknownOrNotSet)
                return Enums.CreditCardType.Visa;
            else return Enums.CreditCardType.UnknownOrNotSet;
        }

        [TestMethod]
        public void GetAllSalesItems()
        {
            var mockLogger = new Mock<ILogger>();
            var settings= GetPopulatedSettingsObj();
            var repo = new QBRepository(mockLogger.Object, settings);
            var allItems = repo.GetAllSalesItems(true);
            Assert.IsTrue(allItems.Count == 36);
        }

        private int GetRandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        DateTime GetRandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();

            int range = ((TimeSpan)(DateTime.Today - start)).Days;
            return start.AddDays(gen.Next(range));
        } 


        private static Random random = new Random((int)DateTime.Now.Ticks);//thanks to McAden 
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        } 

    }
}
