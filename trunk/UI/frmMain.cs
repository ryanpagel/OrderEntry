﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuickBooks.Util;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using QuickBooks.BusObj;
using QuickBooks.DataAccess;
using StructureMap;
using System.Threading;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using StructureMap.Pipeline;

namespace QuickBooks.UI
{
    public partial class frmMain : Form
    {
        ILogger _logger;
        QBRepository _qbRepo;
        ISettings _settings;
        frmCustomerSearch _fCustomerSearch;
        IFileSystemRepository _fsRepo;
        SalesItemsRepository _salesItemsRepository;

        Dictionary<string, OrderItem> _orderItems;

        FileSet _fileSet;
        public frmCustomerSearch FCustomerSearch
        {
            get { return _fCustomerSearch; }
            set { _fCustomerSearch = value; }
        }

        bool _overBorder;
        bool _mouseDown;

        bool Resizing
        {
            get
            {
                return _overBorder && _mouseDown;
            }
        }
        public frmMain(ILogger logger, QBRepository qbRepo, ISettings settings, frmCustomerSearch fCustSearch, IFileSystemRepository fsRepo, SalesItemsRepository salesItemsRepo)
        {
            InitializeComponent();

            try
            {
                
                _logger = logger;
                _qbRepo = qbRepo;
                _settings = settings;
                _fCustomerSearch = fCustSearch;
                _fsRepo = fsRepo;
                _salesItemsRepository = salesItemsRepo;

                _fCustomerSearch.Text = "QuickBooks Customers";

                if (_settings.IsInitialStartup)
                {
                    ShowSettingsForm();
                }

                //InitializeFileSystemWatcher();

                LoadCboPendingSince();
                SetConnectionStatus();
                ShowConnectionStatus();

                SetConnectionBasedUiElements();

                CacheSalesItemsFromAppConfigFile();

                
            }
            catch (Exception ex)
            {
                _logger.LogException("There was an error in frmMain load.", ex);
                MessageBox.Show("There was an error on application startup (see the log for details).  The error may be related to incorrect settings.  The settings are located under View > Settings.", "Error",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void CacheSalesItemsFromAppConfigFile()
        {
            _orderItems = this._salesItemsRepository.GetItemsFromDisk().ToDictionary<OrderItem, string>(p => p.ItemID);
        }

        private void SetConnectionBasedUiElements()
        {
            var connected = _settings.IsConnected;

            mnuCustomerSearch.Enabled = connected;
            mnuExportSalesItemsToDisk.Enabled = connected;
            

        }

        private void SetConnectionStatus()
        {
            _settings.IsConnected = _qbRepo.HasValidConnection();

            
        }

        private void ShowConnectionStatus()
        {
            string msg = "";
            MessageBoxIcon icon;
            if (_settings.IsConnected)
            {
                msg = "Connected to Quickbooks";
                icon = MessageBoxIcon.Information;
            }
            else
            {
                msg = "Could not establish a connection to Quickbooks";
                icon = MessageBoxIcon.Error;
            }

            MessageBox.Show(msg, "Connection Status", MessageBoxButtons.OK, icon);
        }

        private void RefreshSalesItemsCacheFromQuickBooks()
        {
            _orderItems = this._qbRepo.GetAllSalesItems(false).ToDictionary<OrderItem, string>(p => p.ItemID);
        }

        void LoadCboPendingSince()
        {
            List<ListItem> items = new List<ListItem>()
            {
                new ListItem("All", DateTime.Now.AddYears(-100)),
                new ListItem("24 Hours", DateTime.Now.AddHours(-24)),
                new ListItem("1 Week", DateTime.Now.AddDays(-7)),
                new ListItem("1 Month", DateTime.Now.AddMonths(-1)),
                new ListItem("6 Months", DateTime.Now.AddMonths(-6)),
                new ListItem("1 Year", DateTime.Now.AddYears(-1))
            };
            
            cboPendingSince.DataSource = items;
        }

        void WireupGridViewEvents()
        {
            ucGridLeft.DeleteFileKey += new Action<string,string>(DeleteFileByKey);
            ucGridRight.DeleteFileKey += new Action<string, string>(DeleteFileByKey);

            ucGridLeft.OpenOrderByKey += new Action<string>(DisplayPendingOrderByKey);
            ucGridRight.OpenOrderByKey += new Action<string>(DisplayPendingOrderByKey);
        }


        void DeleteFileByKey(string fileKey, string fullName)
        {
            var resp = MessageBox.Show(string.Format("Are you sure you want to delete the order for {0}", fullName), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (resp == DialogResult.No)
                return;

            _fsRepo.DeletePendingOrderByKey(fileKey);
        }

        private void InitializeFileSystemWatcher()
        {
            fileSystemWatcher1.Path = _settings.PendingOrdersPath;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            try
            {
                this.WindowState = FormWindowState.Maximized;

                _logger.Log("Application starting up...");
                //frmSplash fSplash = new frmSplash();
                //Application.DoEvents();
                //fSplash.Show();
                //Thread.Sleep(1000);
                //fSplash.Close();
                mnuPartialOrders.Checked = true;
                mnuFullOrders.Checked = true;
                this.IsMdiContainer = true;
                _fileSet = _fsRepo.GetPendingOrderFileSet(true);
                LoadPanels();
                WireupGridViewEvents();

                //frmOrder fOrder = ObjectFactory.GetInstance<frmOrder>();
                //fOrder.MdiParent = this;
                //fOrder.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in main form load.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _logger.LogException(ex);
                
            }
        }

        void LoadPanels()
        {
            var leftPanel = SearchLeftPanelFiles();

            var rightPanel = from o in _fileSet.Values
                             where o.SaveLocation == PendingOrderSaveLocation.RightPanel
                             select o;
            ucGridLeft.SetDatasource(leftPanel.ToList());
            ucGridRight.SetDatasource(rightPanel.ToList());

            SetLeftGridviewRowCountLabel();
            SetRightGridviewRowCountLabel();
        }

        private void SetLeftGridviewRowCountLabel()
        {
            lblContacts.Text = string.Format("Contacts ({0})", ucGridLeft.Rows);
            
        }
        void SetRightGridviewRowCountLabel()
        {
            lblPendingOrders.Text = string.Format("Pending Orders ({0})", ucGridRight.Rows);
        }

        

        //private void processLegacyinvFilesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    PendingOrdersUpdater pou = ObjectFactory.GetInstance<PendingOrdersUpdater>();

        //    List<CustomerOrderObject> orders = new List<CustomerOrderObject>();
            
        //    var result = Directory.GetFileSystemEntries(Environment.CurrentDirectory + "\\LegacyData", "*.inv");

        //    foreach (var file in result)
        //    {
        //        Stream s = File.OpenRead(file);
        //        BinaryFormatter b = new BinaryFormatter();
        //        OrderForm of = (OrderForm)b.Deserialize(s);
        //        s.Close();

        //        var order = pou.GetOrderFromForm(of);
        //        orders.Add(order);
        //    }
     
        //}


        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlSerializer s = new XmlSerializer(typeof(CustomerOrderObject));
            CustomerOrderObject coo;
            TextReader tr = new StreamReader(_settings.AppDataDirectory + "\\test.xml");
            coo = (CustomerOrderObject)s.Deserialize(tr);

            _logger.Log("This is a test");
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fLog = ObjectFactory.GetInstance<frmLog>();
            fLog.ShowDialog();
        }

        private void showSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSettingsForm();
        }

        private void ShowSettingsForm()
        {
            var fSettings = ObjectFactory.GetInstance<frmSettings>();
            fSettings.ShowDialog();
        }

        private void newOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrder fOrder = ObjectFactory.GetInstance<frmOrder>();
            fOrder.SetSalesItems(_orderItems);
            fOrder.MdiParent = this;
            fOrder.Show();
        }

        private void mnuLeftPanel_Click(object sender, EventArgs e)
        {
            if (mnuPartialOrders.Checked)
            {
                //hiding the panel
                splitContainer2.Panel1Collapsed = true;
                mnuPartialOrders.Checked = false;
                SubtractPanel();
            }
            else
            {
                //show the panel
                splitContainer2.Panel1Collapsed = false;
                mnuPartialOrders.Checked = true;
                AddLeftPanel();
            }
        }

        private void mnuRightPanel_Click(object sender, EventArgs e)
        {
            if (mnuFullOrders.Checked)
            {
                //hiding the panel
                splitContainer2.Panel2Collapsed = true;
                mnuFullOrders.Checked = false;
                SubtractPanel();
            }
            else
            {
                //show the panel
                splitContainer2.Panel2Collapsed = false;
                mnuFullOrders.Checked = true;
                AddRightPanel();
            }
        }

        void AddLeftPanel()
        {
            if (mnuFullOrders.Checked && mnuPartialOrders.Checked)
                splitContainer2.Width = splitContainer2.Width * 2;
            else
            {
                splitContainer2.Visible = true;
                splitContainer2.Width = 250;
                splitContainer2.Panel2Collapsed = true;
            }
        }

        void AddRightPanel()
        {
            if (mnuFullOrders.Checked && mnuPartialOrders.Checked)
                splitContainer2.Width = splitContainer2.Width * 2;
            else
            {
                splitContainer2.Visible = true;
                splitContainer2.Width = 250;
                splitContainer2.Panel1Collapsed = true;
            }
        }

        void SubtractPanel()
        {
            if (!mnuPartialOrders.Checked && !mnuFullOrders.Checked)
                splitContainer2.Visible = false;
            else
            {
                splitContainer2.Width = splitContainer2.Width / 2;
            }
                
            
        }

        private void viewCurrentCustomersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( _fCustomerSearch == null || _fCustomerSearch.IsDisposed)
            {
                _fCustomerSearch = ObjectFactory.GetInstance<frmCustomerSearch>();
                _fCustomerSearch.Text = "QuickBooks Customers";
                _fCustomerSearch.Show();
            }
            else
            {
                if (_fCustomerSearch.Visible)
                    _fCustomerSearch.BringToFront();
                else _fCustomerSearch.Show();
            }

        }

        private void loadCOOToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void DisplayPendingOrderByKey(string fileKey)
        {
            this.Cursor = Cursors.WaitCursor;
            var coo = _fsRepo.GetPendingOrderByKey(fileKey);
            frmOrder fOrder = ObjectFactory.GetInstance<frmOrder>();
            fOrder.SetSalesItems(_orderItems);
            fOrder.MdiParent = this;
            fOrder.Show();
            fOrder.Initialize(coo);
            
            this.Cursor = Cursors.Default;
        }

        private void processLegacyinvFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CascadeWindows();
        }

        public void CascadeWindows()
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void frmMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                if (e.Shift)
                {
                    if (e.KeyCode == Keys.C)
                    {
                        this.CascadeWindows();
                    }
                }

            }
        }

        private void frmMain_Shown(object sender, EventArgs e)
        {

        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            var pof = _fsRepo.GetPendingOrderFileByFilename(e.FullPath);
            if (_fileSet.ContainsKey(pof.FileKey))
                _fileSet[pof.FileKey] = pof;
            LoadPanels();
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            var pof = _fsRepo.GetPendingOrderFileByFilename(e.FullPath);
            _fileSet.Add(pof.FileKey, pof);
            LoadPanels();
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            var parts = e.Name.Split(".".ToCharArray());
            string fileKey = parts[0];
            if (_fileSet.ContainsKey(fileKey))
                _fileSet.Remove(fileKey);
            LoadPanels();
        }

        List<PendingOrderFile> SearchFiles(DateTime pendingSince, PendingOrderSaveLocation loc)
        {
            return (from f in _fileSet.Values
                    where f.SaveLocation == loc
                    && f.OrderDate >= pendingSince
                    select f).ToList();
        }

        private void cboPendingSince_SelectedIndexChanged(object sender, EventArgs e)
        {
            var files = SearchLeftPanelFiles();
            ucGridLeft.SetDatasource(files);
            SetLeftGridviewRowCountLabel();
        }

        private List<PendingOrderFile> SearchLeftPanelFiles()
        {
            var dt = (DateTime)(((ListItem)cboPendingSince.SelectedItem).Value);
            var files = SearchFiles(dt, PendingOrderSaveLocation.LeftPanel);
            return files;
        }

        private void getInventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mnuRefreshContactsAndPendingOrders_Click(object sender, EventArgs e)
        {
            _fileSet =  _fsRepo.GetPendingOrderFileSet(true);
            LoadPanels();
            MessageBox.Show("Contacts and pending orders successfully refreshed.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void inventoryItemListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshSalesItemsCacheFromQuickBooks();
            MessageBox.Show("The sales items cache was successfully refreshed.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void processLegacyinvFilesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.CascadeWindows();
        }

        private void exportSalesItemsToDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshSalesItemsCacheFromQuickBooks();
            var items = _orderItems.Values.ToList();
            _salesItemsRepository.SaveItemsToDisk(items);
        }




    }
}
