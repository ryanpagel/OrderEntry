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
    public partial class ucCreditCard : UserControl
    {
        public ucCreditCard()
        {
            InitializeComponent();
            LoadComboBoxItems();
        }


        public CreditCard GetCreditCard()
        {
            CreditCard cc = new CreditCard();
            cc.CardholderName = txtCardholderName.Text;
            cc.CardNumber = txtCardNumber.Text;
            cc.ExpirationDate = txtExpiration.Text;
            cc.Note = txtNote.Text;
            try
            {
                cc.CreditCardType = (Enums.CreditCardType)Enum.Parse(typeof(Enums.CreditCardType), cboCardType.Text);
            }
            catch (Exception)
            {
                cc.CreditCardType = Enums.CreditCardType.UnknownOrNotSet;
            }

            return cc;
        }

        public void SetCreditCard(CreditCard cc)
        {
            txtCardholderName.Text = cc.CardholderName;
            txtCardNumber.Text = cc.CardNumber;
            txtExpiration.Text = cc.ExpirationDate;
            txtNote.Text = cc.Note;

            SetCardType(cc.CreditCardType);
        }

        public void SetCardType(Enums.CreditCardType ccType)
        {
            if (ccType == null || ccType == Enums.CreditCardType.UnknownOrNotSet)
                cboCardType.SelectedIndex = 0;
            else cboCardType.SelectedIndex = cboCardType.FindString(ccType.ToString());
        }

        void LoadComboBoxItems()
        {
            var ctypes = Enum.GetNames(typeof(Enums.CreditCardType)).ToList();
            ctypes.Remove(Enums.CreditCardType.UnknownOrNotSet.ToString());
            ctypes.Insert(0, "");

            cboCardType.DataSource = ctypes;
        }
    }
}
