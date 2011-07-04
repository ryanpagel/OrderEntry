using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.Util;
using QuickBooks.DataAccess;
using StructureMap;
using QuickBooks.BusObj;

namespace QuickBooks.UI
{
    public partial class frmOrder : Form
    {
        ISettings _settings;
        ILogger _logger;
        IQBRepository _repo;
        IFileSystemRepository _fsRepo;
        PrintDialog _pd;
        Dictionary<string, OrderItem> _orderItems;
        PendingOrderSaveLocation _saveLocation = PendingOrderSaveLocation.NotSet;

        bool _isPreExistingOrder = false;
        string _fileKey = string.Empty;
        string _billingState;


        public frmOrder(ISettings settings, ILogger logger, IQBRepository repo, IFileSystemRepository fsRepo)
        {
            InitializeComponent();

            _settings = settings;
            _logger = logger;
            _repo = repo;
            _fsRepo = fsRepo;

            ucCC1.SetCardType(Enums.CreditCardType.Visa);
            ucCC2.SetCardType(Enums.CreditCardType.Visa);
            ucCC3.SetCardType(Enums.CreditCardType.Visa);
            rbCC.Checked = true;
        }

        public void Initialize(CustomerOrderObject coo)
        {
            _isPreExistingOrder = true;
            mnuDelete.Enabled = true;
            LoadCustomerOrderObj(coo);
        }

        public void SetSalesItems(Dictionary<string, OrderItem> allSalesItems)
        {
            _orderItems = allSalesItems;
        }



        private void LoadCustomerOrderObj(CustomerOrderObject coo)
        {
            _saveLocation = coo.SaveLocation;
            _fileKey = coo.FileKey;
            dtOrderDate.Value = coo.Order.OrderDate;
            ucCustomerInfo1.SetCustomerInfo(coo.Customer);
            ucBillingAddress.SetAddress(coo.Customer.BillingAddress);
            ucShippingAddress.SetAddress(coo.Customer.ShippingAddress);
            ucCC1.SetCreditCard(coo.Customer.CreditCards[0]);
            ucCC2.SetCreditCard(coo.Customer.CreditCards[1]);
            ucCC3.SetCreditCard(coo.Customer.CreditCards[2]);
            SetVehicleInfo(coo.Vehicle);
            SetPaymentMethod(coo.Order.PaymentMethod);
            ClearOrderDetails();
            foreach (var item in coo.Order.OrderItems)
            {
                var ucLi = ObjectFactory.GetInstance<ucLineItem>();
                ucLi.LoadOrderItems(_orderItems);

                    
                flpMain.Controls.Add(ucLi);
                ucLi.SetOrderItem(item);
                ucLi.DeleteItem += new Action<ucLineItem>(li_DeleteItem);
                ucLi.TotalOrTaxableChanged += new Action(li_TotalChanged); 
            }
            flpMain.Controls.Add(btnAddItem);
            RecalculateTotals();
        }

        private void ClearOrderDetails()
        {
            foreach (Control ctrl in flpMain.Controls)
            {
                flpMain.Controls.Remove(ctrl);
            }
        }

        void SetVehicleInfo(Vehicle v)
        {

            txtMake.Text = v.Make;
            txtModel.Text = v.Model;
            txtRow1.Text = v.Row1;
            txtRow2.Text = v.Row2;
            txtRow3.Text = v.Row3;
            txtTrim.Text = v.Trim;
            txtYear.Text = v.Year.DecimalToString();
        }

        void SetOrderDetails(Order o)
        {
            foreach (var lineItem in o.OrderItems)
            {
                var ucLi = ObjectFactory.GetInstance<ucLineItem>();
                ucLi.LoadOrderItems(_orderItems);
                flpMain.Controls.Add(ucLi);
            }
        }

        private void btnCustLookup_Click(object sender, EventArgs e)
        {
            SearchCustomers();
        }

        private void SearchCustomers()
        {

            var fSearchCust = ObjectFactory.GetInstance<frmCustomerSearch>();

            if (ucCustomerInfo1.FullName != "")
                fSearchCust.SetSearchString(ucCustomerInfo1.FullName);

            fSearchCust.Text = "Select a QuickBooks Customer";
            fSearchCust.ShowDialog();

            if (fSearchCust.SelectedCustomer == null)
                return;

            Customer customer = fSearchCust.SelectedCustomer;

            ucCustomerInfo1.SetCustomerInfo(customer);

            ucBillingAddress.SetAddress(customer.BillingAddress);
            ucShippingAddress.SetAddress(customer.ShippingAddress);

            ucCC1.SetCreditCard(customer.CreditCards[0]);
            ucCC2.SetCreditCard(customer.CreditCards[1]);
            ucCC3.SetCreditCard(customer.CreditCards[2]);
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            ucShippingAddress.Clear();

            var billing = ucBillingAddress.GetAddress();
            ucShippingAddress.SetAddress(billing);
        }

        private void frmOrder_Load(object sender, EventArgs e)
        {
            ucBillingAddress.AddressType = Enums.AddressType.Billing;
            ucShippingAddress.AddressType = Enums.AddressType.Shipping;
            ucCustomerInfo1.CustomerNameChanged += new Action<string>(ucCustomerInfo1_CustomerNameChanged);
            ucCustomerInfo1.SearchCustomers += new Action(ucCustomerInfo1_SearchCustomers);

            ucBillingAddress.StateChanged += new Action<string>(ucBillingAddress_StateChanged);

            if(!_isPreExistingOrder)
                AddLineItem();
        }

        void ucCustomerInfo1_CustomerNameChanged(string obj)
        {
            string saveLoc = string.Empty;
            if (_saveLocation == PendingOrderSaveLocation.LeftPanel)
                saveLoc = "Contact";
            else if (_saveLocation == PendingOrderSaveLocation.RightPanel)
                saveLoc = "Pending Order";

            if (saveLoc != "")
                this.Text = string.Format("{0} ({1})", obj, saveLoc);
            else this.Text = obj;
        }

        void ucBillingAddress_StateChanged(string stateVal)
        {
            _billingState = stateVal;
            RecalculateTotals();
        }



        void ucCustomerInfo1_SearchCustomers()
        {
            SearchCustomers();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddLineItem();
        }

        private void AddLineItem()
        {

            ucLineItem li = ObjectFactory.GetInstance<ucLineItem>();
            li.LoadOrderItems(_orderItems);
            

            li.DeleteItem += new Action<ucLineItem>(li_DeleteItem);
            li.TotalOrTaxableChanged += new Action(li_TotalChanged);

            flpMain.Controls.Remove(btnAddItem);
            flpMain.Controls.Add(li);
            flpMain.Controls.Add(btnAddItem);
        }

        void li_TotalChanged()
        {
            RecalculateTotals();
        }

        private void RecalculateTotals()
        {
            double subTotal = 0;
            double taxes = 0;

            for (int i = 0; i < flpMain.Controls.Count - 1; i++)
            {
                var ucLineItem = (ucLineItem)flpMain.Controls[i];
                var orderItem = ucLineItem.GetOrderItem();
                subTotal += orderItem.Total;

                if (orderItem.IsTaxed && (_billingState == _settings.TaxableState))
                    taxes += orderItem.Total * _settings.TaxableRate;
            }

            double grandTotal = taxes + subTotal;

            txtSubTotal.Text = string.Format("{0:n2}", subTotal);
            txtTaxes.Text = string.Format("{0:n2}", taxes);
            txtGrandTotal.Text = string.Format("{0:n2}", grandTotal);
        }

        void li_DeleteItem(ucLineItem obj)
        {
            flpMain.Controls.Remove(obj);
            RecalculateTotals();
        }

        List<OrderItem> GetAllLineItems()
        {
            var items = new List<OrderItem>();

            for (int i = 0; i < flpMain.Controls.Count - 1; i++)
            {
                var ucLineItem = (ucLineItem)flpMain.Controls[i];
                var orderItem = ucLineItem.GetOrderItem();
                items.Add(orderItem);
            }
            return items;

        }

        private CustomerOrderObject GetCustomerOrderObj()
        {
            CustomerOrderObject coo = new CustomerOrderObject();

            Customer c = coo.Customer;
            Vehicle v = coo.Vehicle;
            Order o = coo.Order;

            c.LoadPersonalInfo(ucCustomerInfo1.GetCustomer());

            c.BillingAddress = ucBillingAddress.GetAddress();
            c.ShippingAddress = ucShippingAddress.GetAddress();

            c.CreditCards[0] = ucCC1.GetCreditCard();
            c.CreditCards[1] = ucCC2.GetCreditCard();
            c.CreditCards[2] = ucCC3.GetCreditCard();

            v.Make = txtMake.Text;
            v.Model = txtModel.Text;
            v.Row1 = txtRow1.Text;
            v.Row2 = txtRow2.Text;
            v.Row3 = txtRow3.Text;
            v.Trim = txtTrim.Text;

            try
            {
                v.Year = int.Parse(txtYear.Text);
            }
            catch { }

            try
            {
                o.GrandTotal = double.Parse(txtGrandTotal.Text);
            }
            catch { }

            try
            {
                o.SubTotal = double.Parse(txtSubTotal.Text);
            }
            catch { }

            try
            {
                o.Taxes = double.Parse(txtTaxes.Text);
            }
            catch { }
            
            o.PaymentMethod = GetPaymentMethod();
            o.OrderDate = dtOrderDate.Value;

            for(int i = 0; i<flpMain.Controls.Count-1; i++)
            {
                ucLineItem ucLineItem = (ucLineItem)flpMain.Controls[i];
                OrderItem oi = ucLineItem.GetOrderItem();
                o.OrderItems.Add(oi);
            }

            return coo;
        }

        void SetPaymentMethod(Enums.PaymentMethod pm)
        {
            switch (pm)
            {
                case Enums.PaymentMethod.CreditCard:
                    rbCC.Checked = true;
                    break;
                case Enums.PaymentMethod.Check:
                    rbCheck.Checked = true;
                    break;
                case Enums.PaymentMethod.MoneyOrder:
                    rbMoneyOrder.Checked = true;
                    break;
                case Enums.PaymentMethod.PayPal:
                    rbPaypal.Checked = true;
                    break;
                case Enums.PaymentMethod.WireTransfer:
                    rbWireTransfer.Checked = true;
                    break;
                case Enums.PaymentMethod.NotSelected:
                    break;
                default:
                    break;
            }
        }


        Enums.PaymentMethod GetPaymentMethod()
        {
            if (rbCC.Checked)
                return Enums.PaymentMethod.CreditCard;
            else if (rbCheck.Checked)
                return Enums.PaymentMethod.Check;
            else if (rbMoneyOrder.Checked)
                return Enums.PaymentMethod.MoneyOrder;
            else if (rbPaypal.Checked)
                return Enums.PaymentMethod.PayPal;
            else if (rbWireTransfer.Checked)
                return Enums.PaymentMethod.WireTransfer;
            else if (rbCredit.Checked)
                return Enums.PaymentMethod.Credit;
            else return Enums.PaymentMethod.NotSelected;
        }

        private void saveToLeftPanel_Click(object sender, EventArgs e)
        {
            

            GetCustomerOrderAndSaveFile(PendingOrderSaveLocation.LeftPanel);
        }

        private void GetCustomerOrderAndSaveFile(PendingOrderSaveLocation saveLocation)
        {
            if (string.IsNullOrEmpty(ucCustomerInfo1.FullName))
            {
                MessageBox.Show("Enter a name before saving.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            var coo = GetCustomerOrderObj();
            coo.SaveLocation = saveLocation;
            if (_fileKey != "")
                coo.FileKey = _fileKey;

            _fsRepo.SavePendingOrder(coo);
            MessageBox.Show(
                string.Format("{0} - order successfully saved to disk.", coo.Customer.FullName),
                "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void frmOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.Shift)
                {
                    if (e.KeyCode == Keys.C)
                    {
                        frmMain fMain = (frmMain)this.MdiParent;
                        fMain.CascadeWindows();
                    }
                }

            }
        }

        private void saveToRightPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCustomerOrderAndSaveFile(PendingOrderSaveLocation.RightPanel);
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {

             printDialog1.Document = printDocument1;
            if (printDialog1.ShowDialog() == DialogResult.OK)
                printDocument1.Print();           
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font printFont = new Font("Courier New", 9f);
            e.Graphics.DrawString(GetPrintString(), printFont, Brushes.Black, (float)0f, (float)0f);
        }

        private void mnuCloseOrder_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string GetPrintString()
        {
            try
            {
                string ps = "";

                var coo = this.GetCustomerOrderObj();
                ps = ps = string.Format("Date of Order : {0,-20:g}", coo.Order.OrderDate) + Environment.NewLine;

                if (txtInvoiceNo.Text == "")
                    ps += string.Format("Invoice Number: {0}", "Pending");
                else
                    ps += string.Format("Invoice Number: {0}", txtInvoiceNo.Text);

                ps = (((((((((((((((((ps + "\r\nCustomer's PO: " + coo.Customer.PoBox) + "\r\n\r\n") +
                    string.Format("       {0,-30}{1,-30}{2,-30}", "CUSTOMER INFORMATION", "BILLING ADDRESS", "SHIPPING ADDRESS") + "\r\n") +
                    string.Format(" Name: {0,-30}{1,-30}{2,-30}", coo.Customer.FullName, coo.Customer.BillingAddress.Line1, coo.Customer.ShippingAddress.Line1) + "\r\n") +
                    string.Format("  Ph1: {0,-30}{1,-30}{2,-30}", coo.Customer.Phone, coo.Customer.BillingAddress.Line2, coo.Customer.ShippingAddress.Line2) + "\r\n") +
                    string.Format("  Ph2: {0,-30}{1,-30}{2,-30}", coo.Customer.AltPhone, coo.Customer.BillingAddress.Line3, coo.Customer.ShippingAddress.Line3) + "\r\n") +
                    string.Format("Email: {0,-30}{1,-30}{2,-30}", coo.Customer.Email, coo.Customer.BillingAddress.Line4, coo.Customer.ShippingAddress.Line4) + "\r\n") +
                    string.Format("AltCt: {0,-30}{1,-30}{2,-30}", (object[])new string[] { coo.Customer.AltContact, (coo.Customer.BillingAddress.City + "," + coo.Customer.BillingAddress.State), (coo.Customer.ShippingAddress.City + "," + coo.Customer.ShippingAddress.State) }) + "\r\n") +
                    string.Format("{0,-37}{1,-30}{2,-30}", "", coo.Customer.BillingAddress.Zip, coo.Customer.ShippingAddress.Zip) + "\r\n\r\n") +
                    "\r\n" + "Notes: " + txtNotes.Text + "\r\n\r\n" +
                    "       VEHICLE\r\n") + " Year: " + coo.Vehicle.Year + "\r\n") + " Make: " + coo.Vehicle.Make+ "\r\n") + "Model: " + coo.Vehicle.Model + "\r\n") + " Trim: " + coo.Vehicle.Trim + "\r\n") + " Row1: " + coo.Vehicle.Row1 + "\r\n") + " Row2: " + coo.Vehicle.Row2 + "\r\n") + " Row3: " + coo.Vehicle.Row3 + "\r\n\r\n") + "       PAYMENT: ";
                if (rbCC.Checked)
                    ps += "Credit Card";
                else if (rbCheck.Checked)
                    ps += "Check";
                else if (rbMoneyOrder.Checked)
                    ps += "Money Order";
                else if (rbPaypal.Checked)
                    ps += "PayPal";
                else if (rbCredit.Checked)
                    ps += "Credit";

                ps = (((((((((ps + "\r\n") + string.Format("       {0,-30}{1,-30}{2,-30}", "Card 1", "Card 2", "Card 3") + "\r\n") + string.Format(" Type: {0,-30}{1,-30}{2,-30}", coo.Customer.CreditCards[0].CreditCardType, coo.Customer.CreditCards[1].CreditCardType, coo.Customer.CreditCards[2].CreditCardType) + "\r\n") + string.Format(" Name: {0,-30}{1,-30}{2,-30}", coo.Customer.CreditCards[0].CardholderName, coo.Customer.CreditCards[1].CardholderName, coo.Customer.CreditCards[2].CardholderName) + "\r\n") + string.Format("  Num: {0,-30}{1,-30}{2,-30}", coo.Customer.CreditCards[0].CardNumber, coo.Customer.CreditCards[1].CardNumber, coo.Customer.CreditCards[2].CardNumber) + "\r\n") + string.Format("  Exp: {0,-30}{1,-30}{2,-30}", coo.Customer.CreditCards[0].ExpirationDate, coo.Customer.CreditCards[1].ExpirationDate, coo.Customer.CreditCards[2].ExpirationDate) + "\r\n") + string.Format(" Note: {0,-30}{1,-30}{2,-30}", coo.Customer.CreditCards[0].Note, coo.Customer.CreditCards[1].Note, coo.Customer.CreditCards[2].Note) + "\r\n\r\n") + "       ORDER\r\n\r\n") + string.Format("{0,-30}{1,-45}{2,-3} {3,-7} {4,-7} {5,-2}", (object[])new string[] { "Name", "Description", "Qty", "Price", "Total", "Tx" }) + "\r\n") + string.Format("{0,-30}{1,-45}{2,-3} {3,-7} {4,-7} {5,-2}", (object[])new string[] { "----", "-----------", "---", "-------", "-------", "--" }) + "\r\n";
                foreach (var item in coo.Order.OrderItems)
                {
                    string txable = "N";
                    if (item.IsTaxed)
                        txable = "Y";
                    ps = ps + string.Format("{0,-30}{1,-45}{2,-3} {3,-7} {4,-7} {5,-2}", (object[])new string[] { item.ItemName, item.Description, item.Quantity.ToString().PadLeft(3), string.Format("{0:n2}", item.Price).PadLeft(7), string.Format("{0:n2}", item.Total).PadLeft(7), txable }) + "\r\n";


                }
                ps = (((ps + "\r\n") + "   Subtotal: " + string.Format("{0:n2}", coo.Order.SubTotal).PadLeft(8) + "\r\n") + "      Taxes: " + string.Format("{0:n2}", coo.Order.Taxes).PadLeft(8) + "\r\n") + "Grand Total: " + string.Format("{0:n2}", coo.Order.GrandTotal).PadLeft(8) + "\r\n";
                return ps;


            }
            catch (Exception ex)
            {
                _logger.LogException("Error in GetPrintString()", ex);
                throw new QuickBooksException("Error generating print document.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void saveToQuickBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                var coo = this.GetCustomerOrderObj();
                string invoiceNo = _repo.SaveInvoice(coo);
                txtInvoiceNo.Text = invoiceNo;
                this.Cursor = Cursors.Default;
                MessageBox.Show(string.Format("Invoice successfully saved.{0}Customer:   {1}{0}Invoice No: {2}", Environment.NewLine, coo.Customer.FullName, txtInvoiceNo.Text),
                    "Invoice",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                if (this._isPreExistingOrder)
                    _fsRepo.DeletePendingOrderByKey(_fileKey);
                this.Close();

            }
            catch (Exception)
            {
                this.Cursor = Cursors.Default;
                throw;
            }

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msg = "";
            if (ucCustomerInfo1.FullName != "")
                msg = string.Format("Are you sure you want to delete the order for {0}?", ucCustomerInfo1.FullName);
            else
                msg = "Are you sure you want to delete the current order?";

            var resp = MessageBox.Show(msg, "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resp == DialogResult.No)
                return;

            _fsRepo.DeletePendingOrderByKey(_fileKey);
            this.Close();
        }
        

    }
}
