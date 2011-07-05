using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.BusObj;
using QuickBooks.Util;

namespace QuickBooks.UI
{
    public partial class ucLineItem : UserControl
    {

        bool _formShown = false;
        Dictionary<string, OrderItem> _orderItems;
        ILogger _logger;

        public ucLineItem()
        {
            InitializeComponent();

        }
        public ucLineItem(ILogger logger)
        {
            InitializeComponent();
            _logger = logger;
            cbTaxable.Checked = true;
        }

        public void LoadOrderItems(Dictionary<string,OrderItem> orderItems)
        {
            _orderItems = orderItems;
            cboItems.ValueMember = "ItemID";
            cboItems.DisplayMember = "ItemName";
            cboItems.DataSource = orderItems.Values.ToList();
        }

        public void SetOrderItem(OrderItem oi)
        {
            cboItems.SelectedValue = oi.ItemID;
            txtDescription.Text = oi.Description;
            txtPrice.Text = oi.Price.ToString();
            txtQuantity.Text = oi.Quantity.ToString();
            txtTotal.Text = oi.Total.ToString();
            cbTaxable.Checked = oi.IsTaxed;
            
        }

        public OrderItem GetOrderItem()
        {
            try
            {

                OrderItem oi = new OrderItem();
                oi.Description = txtDescription.Text;
                oi.IsTaxed = cbTaxable.Checked;
                oi.ItemName = cboItems.Text;
                oi.ItemID = cboItems.SelectedValue.ToString();
                if(txtPrice.Text != "")
                    oi.Price = Double.Parse(txtPrice.Text);

                if(txtQuantity.Text != "")
                    oi.Quantity = double.Parse(txtQuantity.Text);

                if(txtTotal.Text != "")
                    oi.Total = double.Parse(txtTotal.Text);

                return oi;
            }
            catch (Exception ex)
            {
                throw new Exception("Error with order item, description is '" + txtDescription.Text + "'.");
            }
        }

        public event Action TotalOrTaxableChanged;

        public event Action<ucLineItem> DeleteItem;

        public event Action<ucLineItem> InsertItem;

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteItem(this);
        }

        private void CalculateTotalPrice()
        {
            if (txtQuantity.Text == "" || txtPrice.Text == "")
            {
                txtTotal.Text = "";
                return;
            }

            try
            {
                double total = 0;
                double quantity = 0;
                double price = 0;
                quantity = double.Parse(txtQuantity.Text);
                price = double.Parse(txtPrice.Text);
                total = quantity * price;
                txtTotal.Text = string.Format("{0:n2}", total);
            }
            catch (Exception ex)
            {
                _logger.LogException("Error in CalculateTotalPrice() of ucLineItem.", ex);
                txtTotal.Text = "";
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalPrice();
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            if(TotalOrTaxableChanged != null)
                TotalOrTaxableChanged();
        }

        private void ucLineItem_Load(object sender, EventArgs e)
        {

            _formShown = true;
        }

        private void cbTaxable_CheckedChanged(object sender, EventArgs e)
        {
            if (_formShown)
                TotalOrTaxableChanged();

        }

        private void cboItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_orderItems.ContainsKey(cboItems.SelectedValue.ToString()))
            {
                OrderItem oi = _orderItems[cboItems.SelectedValue.ToString()];
                txtDescription.Text = oi.Description;
                txtPrice.Text = oi.Price.ToString();
            }
            else _logger.Log(string.Format("User selected Item ID '{0}' which was not found in the order form item dictionary.", cboItems.SelectedValue.ToString()));
        }

        private void btnInsertItem_Click(object sender, EventArgs e)
        {
            if (this.InsertItem != null)
                InsertItem(this);
        }

    }
}
