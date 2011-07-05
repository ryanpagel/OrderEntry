namespace QuickBooks.UI
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newOrderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.processLegacyinvFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.otherToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomerSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuPartialOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFullOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.inventoryItemListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRefreshContactsAndPendingOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportSalesItemsToDisk = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblContacts = new System.Windows.Forms.Label();
            this.cboPendingSince = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblPendingOrders = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.cmsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenProgramFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ucGridLeft = new QuickBooks.UI.ucGrid();
            this.ucGridRight = new QuickBooks.UI.ucGrid();
            this.menuStrip1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.cmsGrid.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolsToolStripMenuItem,
            this.otherToolStripMenuItem,
            this.refreshToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MdiWindowListItem = this.toolsToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1184, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newOrderToolStripMenuItem,
            this.processLegacyinvFilesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.toolsToolStripMenuItem.Text = "File";
            // 
            // newOrderToolStripMenuItem
            // 
            this.newOrderToolStripMenuItem.Name = "newOrderToolStripMenuItem";
            this.newOrderToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.N)));
            this.newOrderToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.newOrderToolStripMenuItem.Text = "New Order";
            this.newOrderToolStripMenuItem.Click += new System.EventHandler(this.newOrderToolStripMenuItem_Click);
            // 
            // processLegacyinvFilesToolStripMenuItem
            // 
            this.processLegacyinvFilesToolStripMenuItem.Name = "processLegacyinvFilesToolStripMenuItem";
            this.processLegacyinvFilesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.C)));
            this.processLegacyinvFilesToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.processLegacyinvFilesToolStripMenuItem.Text = "Cascade Windows";
            this.processLegacyinvFilesToolStripMenuItem.Click += new System.EventHandler(this.processLegacyinvFilesToolStripMenuItem_Click_1);
            // 
            // otherToolStripMenuItem
            // 
            this.otherToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCustomerSearch,
            this.showLogToolStripMenuItem,
            this.showSettingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnuPartialOrders,
            this.mnuFullOrders});
            this.otherToolStripMenuItem.Name = "otherToolStripMenuItem";
            this.otherToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.otherToolStripMenuItem.Text = "View";
            // 
            // mnuCustomerSearch
            // 
            this.mnuCustomerSearch.Name = "mnuCustomerSearch";
            this.mnuCustomerSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.mnuCustomerSearch.Size = new System.Drawing.Size(211, 22);
            this.mnuCustomerSearch.Text = "Customer Search...";
            this.mnuCustomerSearch.Click += new System.EventHandler(this.viewCurrentCustomersToolStripMenuItem_Click);
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.L)));
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showLogToolStripMenuItem.Text = "Log...";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
            // 
            // showSettingsToolStripMenuItem
            // 
            this.showSettingsToolStripMenuItem.Name = "showSettingsToolStripMenuItem";
            this.showSettingsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.showSettingsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.showSettingsToolStripMenuItem.Text = "Settings...";
            this.showSettingsToolStripMenuItem.Click += new System.EventHandler(this.showSettingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // mnuPartialOrders
            // 
            this.mnuPartialOrders.Name = "mnuPartialOrders";
            this.mnuPartialOrders.Size = new System.Drawing.Size(211, 22);
            this.mnuPartialOrders.Text = "Contacts Panel";
            this.mnuPartialOrders.Click += new System.EventHandler(this.mnuLeftPanel_Click);
            // 
            // mnuFullOrders
            // 
            this.mnuFullOrders.Name = "mnuFullOrders";
            this.mnuFullOrders.Size = new System.Drawing.Size(211, 22);
            this.mnuFullOrders.Text = "Pending Orders Panel";
            this.mnuFullOrders.Click += new System.EventHandler(this.mnuRightPanel_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inventoryItemListToolStripMenuItem,
            this.mnuRefreshContactsAndPendingOrders,
            this.mnuExportSalesItemsToDisk,
            this.mnuOpenProgramFolderToolStripMenuItem});
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.refreshToolStripMenuItem.Text = "Misc";
            // 
            // inventoryItemListToolStripMenuItem
            // 
            this.inventoryItemListToolStripMenuItem.Name = "inventoryItemListToolStripMenuItem";
            this.inventoryItemListToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.inventoryItemListToolStripMenuItem.Text = "QuickBooks Item List";
            this.inventoryItemListToolStripMenuItem.Visible = false;
            this.inventoryItemListToolStripMenuItem.Click += new System.EventHandler(this.inventoryItemListToolStripMenuItem_Click);
            // 
            // mnuRefreshContactsAndPendingOrders
            // 
            this.mnuRefreshContactsAndPendingOrders.Name = "mnuRefreshContactsAndPendingOrders";
            this.mnuRefreshContactsAndPendingOrders.Size = new System.Drawing.Size(273, 22);
            this.mnuRefreshContactsAndPendingOrders.Text = "Refresh Contacts And Pending Orders";
            this.mnuRefreshContactsAndPendingOrders.Click += new System.EventHandler(this.mnuRefreshContactsAndPendingOrders_Click);
            // 
            // mnuExportSalesItemsToDisk
            // 
            this.mnuExportSalesItemsToDisk.Name = "mnuExportSalesItemsToDisk";
            this.mnuExportSalesItemsToDisk.Size = new System.Drawing.Size(273, 22);
            this.mnuExportSalesItemsToDisk.Text = "Export Sales Items To Disk";
            this.mnuExportSalesItemsToDisk.Click += new System.EventHandler(this.exportSalesItemsToDiskToolStripMenuItem_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(684, 24);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel1.Controls.Add(this.ucGridLeft);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            this.splitContainer2.Panel1.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.ucGridRight);
            this.splitContainer2.Panel2.Controls.Add(this.panel3);
            this.splitContainer2.Size = new System.Drawing.Size(500, 681);
            this.splitContainer2.SplitterDistance = 252;
            this.splitContainer2.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblContacts);
            this.panel2.Controls.Add(this.cboPendingSince);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(2, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 32);
            this.panel2.TabIndex = 5;
            // 
            // lblContacts
            // 
            this.lblContacts.Location = new System.Drawing.Point(3, 14);
            this.lblContacts.Name = "lblContacts";
            this.lblContacts.Size = new System.Drawing.Size(94, 15);
            this.lblContacts.TabIndex = 5;
            this.lblContacts.Text = "Contacts";
            // 
            // cboPendingSince
            // 
            this.cboPendingSince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPendingSince.FormattingEnabled = true;
            this.cboPendingSince.Location = new System.Drawing.Point(103, 8);
            this.cboPendingSince.Name = "cboPendingSince";
            this.cboPendingSince.Size = new System.Drawing.Size(146, 21);
            this.cboPendingSince.TabIndex = 4;
            this.cboPendingSince.SelectedIndexChanged += new System.EventHandler(this.cboPendingSince_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblPendingOrders);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(244, 32);
            this.panel3.TabIndex = 5;
            // 
            // lblPendingOrders
            // 
            this.lblPendingOrders.Location = new System.Drawing.Point(3, 14);
            this.lblPendingOrders.Name = "lblPendingOrders";
            this.lblPendingOrders.Size = new System.Drawing.Size(95, 15);
            this.lblPendingOrders.TabIndex = 5;
            this.lblPendingOrders.Text = "Pending Orders";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 705);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1184, 18);
            this.panel1.TabIndex = 1;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.Filter = "*.inv";
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Created);
            this.fileSystemWatcher1.Deleted += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Deleted);
            // 
            // cmsGrid
            // 
            this.cmsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
            this.cmsGrid.Name = "cmsGrid";
            this.cmsGrid.Size = new System.Drawing.Size(108, 26);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            // 
            // mnuOpenProgramFolderToolStripMenuItem
            // 
            this.mnuOpenProgramFolderToolStripMenuItem.Name = "mnuOpenProgramFolderToolStripMenuItem";
            this.mnuOpenProgramFolderToolStripMenuItem.Size = new System.Drawing.Size(273, 22);
            this.mnuOpenProgramFolderToolStripMenuItem.Text = "Open Program Folder";
            this.mnuOpenProgramFolderToolStripMenuItem.Click += new System.EventHandler(this.mnuOpenProgramFolderToolStripMenuItem_Click);
            // 
            // ucGridLeft
            // 
            this.ucGridLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridLeft.Location = new System.Drawing.Point(2, 32);
            this.ucGridLeft.Name = "ucGridLeft";
            this.ucGridLeft.Size = new System.Drawing.Size(250, 649);
            this.ucGridLeft.TabIndex = 4;
            // 
            // ucGridRight
            // 
            this.ucGridRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucGridRight.Location = new System.Drawing.Point(0, 32);
            this.ucGridRight.Name = "ucGridRight";
            this.ucGridRight.Size = new System.Drawing.Size(244, 649);
            this.ucGridRight.TabIndex = 4;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 723);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CFI Order Entry";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.cmsGrid.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem otherToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPartialOrders;
        private System.Windows.Forms.ToolStripMenuItem mnuFullOrders;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboPendingSince;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.ContextMenuStrip cmsGrid;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private ucGrid ucGridLeft;
        private ucGrid ucGridRight;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblContacts;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblPendingOrders;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem inventoryItemListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRefreshContactsAndPendingOrders;
        private System.Windows.Forms.ToolStripMenuItem processLegacyinvFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomerSearch;
        private System.Windows.Forms.ToolStripMenuItem showSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuExportSalesItemsToDisk;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenProgramFolderToolStripMenuItem;
    }
}

