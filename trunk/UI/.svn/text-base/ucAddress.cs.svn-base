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
    public partial class ucAddress : UserControl
    {
        public ucAddress()
        {
            InitializeComponent();
        }

        private void ucAddress_Load(object sender, EventArgs e)
        {

        }


        Enums.AddressType _addressType;

        public Enums.AddressType AddressType
        {
            get { return _addressType; }
            set { _addressType = value; }
        }

        public void SetAddress(Address address)
        {
            txtState.Text = address.State;
            txtCity.Text = address.City;
            txtZip.Text = address.Zip;
            txtAddress.Text = address.Line1 + Environment.NewLine + address.Line2 + Environment.NewLine + address.Line3 + Environment.NewLine + address.Line4;
            //txtLine1.Text = address.Line1;
            //txtLine2.Text = address.Line2;
            //txtLine3.Text = address.Line3;
            //txtLine4.Text = address.Line4;
        }

        public void Clear()
        {
            txtState.Clear();
            txtCity.Clear();
            txtZip.Clear();
            txtAddress.Clear();
        }

        public Address GetAddress()
        {
            var pieces = txtAddress.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            Address a = new Address(_addressType);

            if (pieces.Count() > 0)
                a.Line1 = pieces[0];
            if (pieces.Count() > 1)
                a.Line2 = pieces[1];
            if (pieces.Count() > 2)
                a.Line3 = pieces[2];
            if (pieces.Count() > 3)
                a.Line4 = pieces[3];

            a.City = txtCity.Text;
            a.State = txtState.Text;
            a.Zip = txtZip.Text;

            return a;
        }

        private void txtLine1_TextChanged(object sender, EventArgs e)
        {

        }

        public event Action<string> StateChanged;

        private void txtState_TextChanged(object sender, EventArgs e)
        {
            if (StateChanged != null)
                StateChanged(txtState.Text);

        }

    }
}
