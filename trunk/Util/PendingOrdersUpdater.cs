using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Forms;
using QuickBooks.BusObj;


namespace QuickBooks.Util
{
    public class PendingOrdersUpdater
    {
        Logger _logger;
        public PendingOrdersUpdater(Logger logger)
        {
            _logger = logger;
        }

        List<T> GetControls<T>(Control ctrl)
        {
            List<T> items = new List<T>();
            return GetControls(ctrl, items);
        }

        List<T> GetControls<T>(Control ctrl, List<T> masterList)
        {
            IEnumerable<T> controls = ctrl.Controls.OfType<T>();
            masterList.AddRange(controls);
            foreach (Control childCtrl in ctrl.Controls)
            {
                GetControls(childCtrl, masterList);
            }
            return masterList;
        }

        //public CustomerOrderObject GetOrderFromForm(OrderForm legacyOrderForm)
        //{
        //    CustomerOrderObject coo = new CustomerOrderObject();
            
        //    try
        //    {
        //        legacyOrderForm.ShowDialog();
                
        //    }
        //    catch (Exception)
        //    {
        //        legacyOrderForm.Dispose();
        //    }

        //    var textboxes = GetControls<TextBox>(legacyOrderForm);

        //    foreach (var tb in textboxes)
        //    {
        //        if (tb.Name == "alContact")
        //            coo.Customer.AltContact = tb.Text;
        //        if (tb.Name == " AltTelephoneTextBox")
        //            coo.Customer.AltPhone = tb.Text;
        //        if (tb.Name == "BillingAddressCity")
        //            coo.BillingAddress.City = tb.Text;
        //        if (tb.Name == "BillingAddressState")
        //            coo.BillingAddress.State = tb.Text;
        //        if (tb.Name == "BillingAddressTextBox")
        //            coo.BillingAddress.Line1 = tb.Text;
        //        if (tb.Name == "BillingAddressZip")
        //            coo.BillingAddress.Zip = tb.Text;
                
        //        if(tb.Name == "CardExpiration1" && tb.Text != "")
        //        {
        //            try
        //            {
        //                coo.CreditCards[0].ExpirationDate = DateTime.Parse(tb.Text);
        //            }
        //            catch (Exception)
        //            {
        //                MessageBox.Show("Error parsing credit card exp. date for customer: " + coo.Customer.FirstName + " " + coo.Customer.LastName);
                        
        //            }
        //        }

        //        if (tb.Name == "CardNote1")
        //            coo.CreditCards[0].Note = tb.Text;
        //        if (tb.Name == "CardName1")
        //            coo.CreditCards[0].CardholderName = tb.Text;
        //        if (tb.Name == "CardNumber1")
        //            coo.CreditCards[0].CardNumber = tb.Text;
        //        if (tb.Name == "EmailTextBox")
        //            coo.Customer.Email = tb.Name;
        //        if (tb.Name == "grandTotal")
        //        {
        //            double g;
        //            if (double.TryParse(tb.Text, out g))
        //                coo.Order.GrandTotal = g;
        //        }

        //        if (tb.Name == "MakeTextBox")
        //            coo.Vehicle.Make = tb.Text;
        //        if (tb.Name == "ModelTextBox")
        //            coo.Vehicle.Model = tb.Text;

        //        if (tb.Name == "NameTextBox")
        //            coo.Customer.FirstName = tb.Text;
        //    }

        //    return coo;

        //}
    }
}
