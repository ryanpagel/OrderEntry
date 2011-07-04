using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.BusObj;

namespace QuickBooks.UI
{
    public partial class ucCustomerInfo : UserControl
    {
        string _customerID = string.Empty;
        string _editSequence = string.Empty;

        public ucCustomerInfo()
        {
            InitializeComponent();
        }

        private void ucCustomerInfo_Load(object sender, EventArgs e)
        {

        }

        public void Clear()
        {
            foreach (var txtbox in this.Controls.OfType<TextBox>())
            {
                txtbox.Clear();
            }
            _customerID = string.Empty;
            _editSequence = string.Empty;
        }

        public void SetCustomerInfo(ICustomerPersonalInfo c)
        {
            txtFullName.Text = c.FullName;
            txtAltContact.Text = c.AltContact;
            txtAltPhone.Text = c.AltPhone;
            txtEmail.Text = c.Email;
            txtPhone.Text = c.Phone;
            txtPO.Text = c.PoBox;

            _customerID = c.CustomerID;
            _editSequence = c.EditSequence;
        }

        public ICustomerPersonalInfo GetCustomer()
        {
            ICustomerPersonalInfo c = new Customer();
            c.FullName = txtFullName.Text;
            c.Phone = txtPhone.Text;
            c.PoBox = txtPO.Text;
            c.AltContact = txtAltContact.Text;
            c.AltPhone = txtAltPhone.Text;
            c.Email = txtEmail.Text;
            c.EditSequence = _editSequence;
            c.CustomerID = _customerID;

            return c;
        }

        public string FullName
        {
            get { return txtFullName.Text; }
            set { txtFullName.Text = value; }
        }

        public event Action SearchCustomers;

        private void txtFullName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode  == Keys.Enter && txtFullName.Text != "")
            {
                if (SearchCustomers != null)
                {
                    SearchCustomers();
                }
            }
                    

        }

        public event Action<string> CustomerNameChanged;

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {
            if (CustomerNameChanged != null)
                CustomerNameChanged(txtFullName.Text);
        }
    }
}
