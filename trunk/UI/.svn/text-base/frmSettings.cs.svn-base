using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.Util;
using System.IO;

namespace QuickBooks.UI
{
    public partial class frmSettings : Form
    {
        ILogger _l;
        ISettings _s;
        bool _restartRequired = false;
        public frmSettings(ILogger logger, ISettings settings)
        {
            InitializeComponent();

            _l = logger;
            _s = settings;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            txtAppRootPath.Text = _s.AppRootPath;
            txtPrivateFieldsID.Text = _s.CustomerPrivateFieldsID;
            txtQBAppName.Text = _s.QbAppName;
            txtMaxLogSize.Text = _s.MaxLogSizeBytes.ToString();
            txtQBFilePath.Text = _s.QuickBooksFilePath;

            txtPendingOrdersDir.Text = _s.PendingOrdersPath;
            txtTaxableState.Text = _s.TaxableState;
            txtInStateTaxCode.Text = _s.InStateTaxCodeName;
            txtOutOfStateTaxCode.Text = _s.OutOfStateTaxCodeName;
            txtTaxableRate.Text = string.Format("{0:0.###}",(_s.TaxableRate * 100));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _s.IsInitialStartup = false;
            _s.Save(_s.ConfigFilePath);
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _s.AppRootPath = txtAppRootPath.Text;
            _s.CustomerPrivateFieldsID = txtPrivateFieldsID.Text;
            _s.MaxLogSizeBytes = int.Parse(txtMaxLogSize.Text);
            _s.QbAppName = txtQBAppName.Text;
            _s.QuickBooksFilePath = txtQBFilePath.Text;
            _s.PendingOrdersPath = txtPendingOrdersDir.Text;
            
            _s.TaxableState = txtTaxableState.Text;
            _s.TaxableRate = double.Parse(txtTaxableRate.Text)/100;
            _s.OutOfStateTaxCodeName = txtOutOfStateTaxCode.Text;
            _s.InStateTaxCodeName = txtInStateTaxCode.Text;
            _s.IsInitialStartup = false;
            _s.Save(_s.ConfigFilePath);
            _s.NotifyIfChangesMades();
            
            //if(_restartRequired)
            //    MessageBox.Show("A restart of the application is required.", "Restart Required", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
        }

        private void btnBrowseAppRoot_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (txtAppRootPath.Text != "")
                fbd.SelectedPath = txtAppRootPath.Text;
            var result = fbd.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            txtAppRootPath.Text = fbd.SelectedPath;
        }

        private void btnBrowseQBFile_Click(object sender, EventArgs e)
        {
            ofdQBFile.ValidateNames = false;
            var result = ofdQBFile.ShowDialog();
            if (result != DialogResult.Cancel)
            {
                if (File.Exists(ofdQBFile.FileName))
                {
                    if (_s.QuickBooksFilePath != ofdQBFile.FileName)
                    {
                        txtQBFilePath.Text = ofdQBFile.FileName;
                        _s.QuickBooksFilePath = ofdQBFile.FileName;
                        _restartRequired = true;
                    }
                }
            }
        }

        private void btnPendingOrdersDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (this.txtPendingOrdersDir.Text != "")
                fbd.SelectedPath = txtPendingOrdersDir.Text;
            var result = fbd.ShowDialog();
            if (result == DialogResult.Cancel)
                return;

            txtPendingOrdersDir.Text = fbd.SelectedPath;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }


    }
}

