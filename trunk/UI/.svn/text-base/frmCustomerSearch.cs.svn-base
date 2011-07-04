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
using QuickBooks.BusObj;

namespace QuickBooks.UI
{
    public partial class frmCustomerSearch : Form
    {

        SortedList<string, Customer> _cachedCustomers = new SortedList<string, Customer>();

        bool _formShown = false;

        ILogger _logger;
        ISettings _settings;
        IQBRepository _repo;

        public frmCustomerSearch(ILogger logger, ISettings settings, IQBRepository repo)
        {
            InitializeComponent();

            _logger = logger;
            _settings = settings;
            _repo = repo;
        }

        private void frmCustomerSearch_Load(object sender, EventArgs e)
        {
            lblRowCount.Text = "";
            gvMain.AutoGenerateColumns = false;
        }

        public void SetSearchString(string name)
        {
            txtSearch.Text = name;
        }

        

        void SearchCustomers()
        {
            if (txtSearch.Text == "")
            {
                MessageBox.Show("Enter a name", "QuickBooks", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }

            this.Cursor = Cursors.WaitCursor;
            _cachedCustomers.Clear();

            var customers = _repo.SearchCustomers(txtSearch.Text);
            foreach (var item in customers)
            {
                _cachedCustomers.Add(item.CustomerID, item);
            }

            gvMain.DataSource = new SortableBindingList<Customer>(customers);

            lblRowCount.Text = string.Format("({0}) Results", gvMain.Rows.Count);
            this.Cursor = Cursors.Default;
        }


        Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
        }
       

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchCustomers();
            //gvMain.Select();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            SelectCustomerFromRow();
        }

        private void SelectCustomerFromRow()
        {
            if (gvMain.SelectedRows.Count == 1)
            {
                string selectedCustId = gvMain.SelectedRows[0].Cells["CustomerID"].Value.ToString();
                if (_cachedCustomers.ContainsKey(selectedCustId))
                    _selectedCustomer = _cachedCustomers[selectedCustId];
                this.Close();
            }
            else
            {
                MessageBox.Show("Select a customer", "QuickBooks", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gvMain_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            SelectCustomerFromRow();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();

            gvMain.DataSource = null;
        }

        private void frmCustomerSearch_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            
            if (txtSearch.Text != "")
                SearchCustomers();
            
            _formShown = true;

            ShowSelectedCustomer();
            
        }

        private void txtLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchCustomers();
            }
        }



        private void gvMain_SelectionChanged(object sender, EventArgs e)
        {
            ShowSelectedCustomer();
        }

        private void ShowSelectedCustomer()
        {
            if (!_formShown)
                return;

            if (gvMain.SelectedRows.Count > 1 || gvMain.SelectedRows.Count == 0)
                return;
            string id = gvMain.SelectedRows[0].Cells["CustomerID"].Value.ToString();

            ucBilling.Clear();
            ucShipping.Clear();
            ucCustomerInfo.Clear();


            Customer c;
            if (!_cachedCustomers.ContainsKey(id))
                return;

            c = _cachedCustomers[id];

            ucBilling.SetAddress(c.BillingAddress);
            ucShipping.SetAddress(c.ShippingAddress);
            ucCustomerInfo.SetCustomerInfo(c);
        }

        private void refreshCustomerListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchCustomers();
        }




    }
}
