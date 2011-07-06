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
        private readonly string sFormTitleText =  "CFI Order Entry";

        ILogger _logger;
        QBRepository _qbRepo;
        ISettings _settings;
        frmCustomerSearch _fCustomerSearch;
        IFileSystemRepository _fsRepo;
        SalesItemsRepository _salesItemsRepository;

        Dictionary<string, OrderItem> _orderItems;

        FileSet _fileSet;

        ucGrid _ucSwatchOrders;
        ucGrid _ucPendingOrders;
        ucGrid _ucContacts;

        public frmMain(ILogger logger, QBRepository qbRepo, ISettings settings, frmCustomerSearch fCustSearch, IFileSystemRepository fsRepo, SalesItemsRepository salesItemsRepo)
        {
            InitializeComponent();

            try
            {
                _ucContacts = ucGridContacts;
                _ucSwatchOrders = ucSwatches;
                _ucPendingOrders = ucGridPendingOrders;

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

                LoadCboPendingSince();
                SetConnectionStatus();

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
                msg = "Could not establish a connection to Quickbooks" + 
                    Environment.NewLine +
                    "The Order Entry Application will continue in disconnected mode";
                icon = MessageBoxIcon.Warning;

                this.Text = this.sFormTitleText + " [DISCONNECTED]";
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
            ucGridContacts.DeleteFileKey += new Action<string,string>(DeleteFileByKey);
            ucGridPendingOrders.DeleteFileKey += new Action<string, string>(DeleteFileByKey);
            ucSwatches.DeleteFileKey += new Action<string,string>(DeleteFileByKey);

            ucGridContacts.OpenOrderByKey += new Action<string>(DisplayPendingOrderByKey);
            ucGridPendingOrders.OpenOrderByKey += new Action<string>(DisplayPendingOrderByKey);
            ucSwatches.OpenOrderByKey += new Action<string>(DisplayPendingOrderByKey);
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
                mnuContactsPanel.Checked = true;
                mnuPendingOrdersPanel.Checked = true;
                mnuSwatchesPanel.Checked = true;
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
            var contactsPanel = SearchContactsPanelFiles();

            var pendingOrdersPanel = from o in _fileSet.Values
                             where o.SaveLocation == PendingOrderSaveLocation.RightPanel
                             select o;

            var swatchPanel = from o in _fileSet.Values
                              where o.SaveLocation == PendingOrderSaveLocation.Swatch
                              select o;

            ucGridContacts.SetDatasource(contactsPanel.ToList());
            ucGridPendingOrders.SetDatasource(pendingOrdersPanel.ToList());
            ucSwatches.SetDatasource(swatchPanel.ToList());
            SetLeftGridviewRowCountLabel();
            SetRightGridviewRowCountLabel();
        }

        private void SetLeftGridviewRowCountLabel()
        {
            //lblContacts.Text = string.Format("Contacts ({0})", ucGridContacts.Rows);
            
        }
        void SetRightGridviewRowCountLabel()
        {
            //lblPendingOrders.Text = string.Format("Pending Orders ({0})", ucGridPendingOrders.Rows);
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

        private void InsertItemIntoFlowLayoutPanel(Control obj, int idx)
        {
         

            List<Control> addBackIn = new List<Control>();
            int itemsCount = this.flowLayoutPanel1.Controls.Count;
            for (int i = itemsCount -1 ; i >=idx; i--)
            {
                addBackIn.Add(this.flowLayoutPanel1.Controls[i]);
                this.flowLayoutPanel1.Controls.RemoveAt(i);
            }

            //add the item we want to insert
            this.flowLayoutPanel1.Controls.Add(obj);

            //add back in the controls that come after inserted control
            addBackIn.Reverse();
            foreach (var item in addBackIn)
            {
                this.flowLayoutPanel1.Controls.Add(item);
            }
        }

        private void mnuContactsPanel_Click(object sender, EventArgs e)
        {
            if (mnuContactsPanel.Checked)
            {
                flowLayoutPanel1.Controls.Remove(_ucContacts);
                flowLayoutPanel1.Refresh();
                mnuContactsPanel.Checked = false;
            }
            else
            {
                //show the panel
                InsertItemIntoFlowLayoutPanel(_ucContacts, 2);
                mnuContactsPanel.Checked = true;
            }
        }

        private void mnuRightPanel_Click(object sender, EventArgs e)
        {
            if (mnuPendingOrdersPanel.Checked)
            {
                flowLayoutPanel1.Controls.Remove(_ucPendingOrders);
                flowLayoutPanel1.Refresh();
                mnuPendingOrdersPanel.Checked = false;
            }
            else
            {
                InsertItemIntoFlowLayoutPanel(_ucPendingOrders, 1);
                mnuPendingOrdersPanel.Checked = true;
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
            ShowConnectionStatus();
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
            var files = SearchContactsPanelFiles();
            ucGridContacts.SetDatasource(files);
            SetLeftGridviewRowCountLabel();
        }

        private List<PendingOrderFile> SearchContactsPanelFiles()
        {
            var dt = (DateTime)(((ListItem)cboPendingSince.SelectedItem).Value);
            var files = SearchFiles(dt, PendingOrderSaveLocation.LeftPanel);
            return files;
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

            string allItems = "";
            foreach (var item in items)
            {
                allItems += item.ItemName + Environment.NewLine;
            }


            MessageBox.Show("The following Sales Items were exported: " + Environment.NewLine +
                allItems, "Sales Items Exported", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mnuOpenProgramFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string myPath = _settings.AppRootPath;
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = myPath;
            prc.Start();
        }

   

        private void mnuSwatchesPanel_Click(object sender, EventArgs e)
        {
            if (this.mnuSwatchesPanel.Checked)
            {
                flowLayoutPanel1.Controls.Remove(_ucSwatchOrders);
                this.mnuSwatchesPanel.Checked = false;
            }
            else
            {
                InsertItemIntoFlowLayoutPanel(_ucSwatchOrders, 0);
                this.mnuSwatchesPanel.Checked = true;
            }
        }




    }
}
