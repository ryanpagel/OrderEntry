﻿namespace QuickBooks.UI
{
    partial class frmOrder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrder));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCloseOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToRightPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveAsSwatchOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSaveToQuickBooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dtOrderDate = new System.Windows.Forms.DateTimePicker();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCust = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbWireTransfer = new System.Windows.Forms.RadioButton();
            this.rbPaypal = new System.Windows.Forms.RadioButton();
            this.rbMoneyOrder = new System.Windows.Forms.RadioButton();
            this.rbCheck = new System.Windows.Forms.RadioButton();
            this.rbCredit = new System.Windows.Forms.RadioButton();
            this.rbCC = new System.Windows.Forms.RadioButton();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.txtRow3 = new System.Windows.Forms.TextBox();
            this.tcCreditCards = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtRow2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRow1 = new System.Windows.Forms.TextBox();
            this.txtTrim = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.btnCustLookup = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tpOrderDetails = new System.Windows.Forms.TabPage();
            this.txtSubTotal = new System.Windows.Forms.TextBox();
            this.txtTaxes = new System.Windows.Forms.TextBox();
            this.txtGrandTotal = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.flpMain = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.txtInvoiceNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.ucCustomerInfo1 = new QuickBooks.UI.ucCustomerInfo();
            this.ucCC1 = new QuickBooks.UI.ucCreditCard();
            this.ucCC2 = new QuickBooks.UI.ucCreditCard();
            this.ucCC3 = new QuickBooks.UI.ucCreditCard();
            this.ucShippingAddress = new QuickBooks.UI.ucAddress();
            this.ucBillingAddress = new QuickBooks.UI.ucAddress();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCust.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tcCreditCards.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tpOrderDetails.SuspendLayout();
            this.flpMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.orderToolStripMenuItem,
            this.testToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.MdiWindowListItem = this.testToolStripMenuItem;
            this.menuStrip1.Name = "menuStrip1";
            // 
            // orderToolStripMenuItem
            // 
            this.orderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuPrint,
            this.mnuDelete,
            this.mnuCloseOrder});
            this.orderToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
            resources.ApplyResources(this.orderToolStripMenuItem, "orderToolStripMenuItem");
            // 
            // mnuPrint
            // 
            this.mnuPrint.Name = "mnuPrint";
            resources.ApplyResources(this.mnuPrint, "mnuPrint");
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // mnuDelete
            // 
            resources.ApplyResources(this.mnuDelete, "mnuDelete");
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // mnuCloseOrder
            // 
            this.mnuCloseOrder.Name = "mnuCloseOrder";
            resources.ApplyResources(this.mnuCloseOrder, "mnuCloseOrder");
            this.mnuCloseOrder.Click += new System.EventHandler(this.mnuCloseOrder_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.mnuSaveAsSwatchOrder,
            this.saveToRightPanelToolStripMenuItem,
            this.toolStripSeparator1,
            this.mnuSaveToQuickBooksToolStripMenuItem});
            this.testToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.MatchOnly;
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            resources.ApplyResources(this.testToolStripMenuItem, "testToolStripMenuItem");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToLeftPanel_Click);
            // 
            // saveToRightPanelToolStripMenuItem
            // 
            this.saveToRightPanelToolStripMenuItem.Name = "saveToRightPanelToolStripMenuItem";
            resources.ApplyResources(this.saveToRightPanelToolStripMenuItem, "saveToRightPanelToolStripMenuItem");
            this.saveToRightPanelToolStripMenuItem.Click += new System.EventHandler(this.saveToRightPanelToolStripMenuItem_Click);
            // 
            // mnuSaveAsSwatchOrder
            // 
            this.mnuSaveAsSwatchOrder.Name = "mnuSaveAsSwatchOrder";
            resources.ApplyResources(this.mnuSaveAsSwatchOrder, "mnuSaveAsSwatchOrder");
            this.mnuSaveAsSwatchOrder.Click += new System.EventHandler(this.mnuSaveAsSwatchOrder_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // mnuSaveToQuickBooksToolStripMenuItem
            // 
            this.mnuSaveToQuickBooksToolStripMenuItem.Name = "mnuSaveToQuickBooksToolStripMenuItem";
            resources.ApplyResources(this.mnuSaveToQuickBooksToolStripMenuItem, "mnuSaveToQuickBooksToolStripMenuItem");
            this.mnuSaveToQuickBooksToolStripMenuItem.Click += new System.EventHandler(this.saveToQuickBooksToolStripMenuItem_Click);
            // 
            // dtOrderDate
            // 
            resources.ApplyResources(this.dtOrderDate, "dtOrderDate");
            this.dtOrderDate.Name = "dtOrderDate";
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tpCust);
            this.tabControl1.Controls.Add(this.tpOrderDetails);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            // 
            // tpCust
            // 
            this.tpCust.Controls.Add(this.label3);
            this.tpCust.Controls.Add(this.txtNotes);
            this.tpCust.Controls.Add(this.ucCustomerInfo1);
            this.tpCust.Controls.Add(this.groupBox2);
            this.tpCust.Controls.Add(this.txtYear);
            this.tpCust.Controls.Add(this.label2);
            this.tpCust.Controls.Add(this.btnCopy);
            this.tpCust.Controls.Add(this.txtRow3);
            this.tpCust.Controls.Add(this.tcCreditCards);
            this.tpCust.Controls.Add(this.txtRow2);
            this.tpCust.Controls.Add(this.label14);
            this.tpCust.Controls.Add(this.txtRow1);
            this.tpCust.Controls.Add(this.ucShippingAddress);
            this.tpCust.Controls.Add(this.txtTrim);
            this.tpCust.Controls.Add(this.label10);
            this.tpCust.Controls.Add(this.txtModel);
            this.tpCust.Controls.Add(this.ucBillingAddress);
            this.tpCust.Controls.Add(this.label13);
            this.tpCust.Controls.Add(this.txtMake);
            this.tpCust.Controls.Add(this.label11);
            this.tpCust.Controls.Add(this.label15);
            this.tpCust.Controls.Add(this.label21);
            this.tpCust.Controls.Add(this.label12);
            this.tpCust.Controls.Add(this.label16);
            this.tpCust.Controls.Add(this.label20);
            this.tpCust.Controls.Add(this.btnCustLookup);
            this.tpCust.Controls.Add(this.label17);
            this.tpCust.Controls.Add(this.label19);
            this.tpCust.Controls.Add(this.label18);
            resources.ApplyResources(this.tpCust, "tpCust");
            this.tpCust.Name = "tpCust";
            this.tpCust.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // txtNotes
            // 
            resources.ApplyResources(this.txtNotes, "txtNotes");
            this.txtNotes.Name = "txtNotes";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbWireTransfer);
            this.groupBox2.Controls.Add(this.rbPaypal);
            this.groupBox2.Controls.Add(this.rbMoneyOrder);
            this.groupBox2.Controls.Add(this.rbCheck);
            this.groupBox2.Controls.Add(this.rbCredit);
            this.groupBox2.Controls.Add(this.rbCC);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // rbWireTransfer
            // 
            resources.ApplyResources(this.rbWireTransfer, "rbWireTransfer");
            this.rbWireTransfer.Name = "rbWireTransfer";
            this.rbWireTransfer.TabStop = true;
            this.rbWireTransfer.UseVisualStyleBackColor = true;
            // 
            // rbPaypal
            // 
            resources.ApplyResources(this.rbPaypal, "rbPaypal");
            this.rbPaypal.Name = "rbPaypal";
            this.rbPaypal.TabStop = true;
            this.rbPaypal.UseVisualStyleBackColor = true;
            // 
            // rbMoneyOrder
            // 
            resources.ApplyResources(this.rbMoneyOrder, "rbMoneyOrder");
            this.rbMoneyOrder.Name = "rbMoneyOrder";
            this.rbMoneyOrder.TabStop = true;
            this.rbMoneyOrder.UseVisualStyleBackColor = true;
            // 
            // rbCheck
            // 
            resources.ApplyResources(this.rbCheck, "rbCheck");
            this.rbCheck.Name = "rbCheck";
            this.rbCheck.TabStop = true;
            this.rbCheck.UseVisualStyleBackColor = true;
            // 
            // rbCredit
            // 
            resources.ApplyResources(this.rbCredit, "rbCredit");
            this.rbCredit.Name = "rbCredit";
            this.rbCredit.TabStop = true;
            this.rbCredit.UseVisualStyleBackColor = true;
            // 
            // rbCC
            // 
            resources.ApplyResources(this.rbCC, "rbCC");
            this.rbCC.Name = "rbCC";
            this.rbCC.TabStop = true;
            this.rbCC.UseVisualStyleBackColor = true;
            // 
            // txtYear
            // 
            resources.ApplyResources(this.txtYear, "txtYear");
            this.txtYear.Name = "txtYear";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnCopy
            // 
            resources.ApplyResources(this.btnCopy, "btnCopy");
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.TabStop = false;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // txtRow3
            // 
            resources.ApplyResources(this.txtRow3, "txtRow3");
            this.txtRow3.Name = "txtRow3";
            // 
            // tcCreditCards
            // 
            this.tcCreditCards.Controls.Add(this.tabPage1);
            this.tcCreditCards.Controls.Add(this.tabPage2);
            this.tcCreditCards.Controls.Add(this.tabPage3);
            resources.ApplyResources(this.tcCreditCards, "tcCreditCards");
            this.tcCreditCards.Name = "tcCreditCards";
            this.tcCreditCards.SelectedIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ucCC1);
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ucCC2);
            resources.ApplyResources(this.tabPage2, "tabPage2");
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.ucCC3);
            resources.ApplyResources(this.tabPage3, "tabPage3");
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtRow2
            // 
            resources.ApplyResources(this.txtRow2, "txtRow2");
            this.txtRow2.Name = "txtRow2";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // txtRow1
            // 
            resources.ApplyResources(this.txtRow1, "txtRow1");
            this.txtRow1.Name = "txtRow1";
            // 
            // txtTrim
            // 
            resources.ApplyResources(this.txtTrim, "txtTrim");
            this.txtTrim.Name = "txtTrim";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // txtModel
            // 
            resources.ApplyResources(this.txtModel, "txtModel");
            this.txtModel.Name = "txtModel";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // txtMake
            // 
            resources.ApplyResources(this.txtMake, "txtMake");
            this.txtMake.Name = "txtMake";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.Name = "label21";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.Name = "label20";
            // 
            // btnCustLookup
            // 
            resources.ApplyResources(this.btnCustLookup, "btnCustLookup");
            this.btnCustLookup.Name = "btnCustLookup";
            this.btnCustLookup.TabStop = false;
            this.btnCustLookup.UseVisualStyleBackColor = true;
            this.btnCustLookup.Click += new System.EventHandler(this.btnCustLookup_Click);
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.Name = "label19";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // tpOrderDetails
            // 
            this.tpOrderDetails.Controls.Add(this.txtSubTotal);
            this.tpOrderDetails.Controls.Add(this.txtTaxes);
            this.tpOrderDetails.Controls.Add(this.txtGrandTotal);
            this.tpOrderDetails.Controls.Add(this.label27);
            this.tpOrderDetails.Controls.Add(this.label26);
            this.tpOrderDetails.Controls.Add(this.label25);
            this.tpOrderDetails.Controls.Add(this.label24);
            this.tpOrderDetails.Controls.Add(this.label30);
            this.tpOrderDetails.Controls.Add(this.label29);
            this.tpOrderDetails.Controls.Add(this.label28);
            this.tpOrderDetails.Controls.Add(this.label23);
            this.tpOrderDetails.Controls.Add(this.label22);
            this.tpOrderDetails.Controls.Add(this.flpMain);
            resources.ApplyResources(this.tpOrderDetails, "tpOrderDetails");
            this.tpOrderDetails.Name = "tpOrderDetails";
            this.tpOrderDetails.UseVisualStyleBackColor = true;
            // 
            // txtSubTotal
            // 
            resources.ApplyResources(this.txtSubTotal, "txtSubTotal");
            this.txtSubTotal.Name = "txtSubTotal";
            // 
            // txtTaxes
            // 
            resources.ApplyResources(this.txtTaxes, "txtTaxes");
            this.txtTaxes.Name = "txtTaxes";
            // 
            // txtGrandTotal
            // 
            resources.ApplyResources(this.txtGrandTotal, "txtGrandTotal");
            this.txtGrandTotal.Name = "txtGrandTotal";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.Name = "label27";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.Name = "label26";
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.Name = "label25";
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.Name = "label24";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.Name = "label30";
            // 
            // label29
            // 
            resources.ApplyResources(this.label29, "label29");
            this.label29.Name = "label29";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.Name = "label28";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.Name = "label23";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.Name = "label22";
            // 
            // flpMain
            // 
            resources.ApplyResources(this.flpMain, "flpMain");
            this.flpMain.Controls.Add(this.btnAddItem);
            this.flpMain.Name = "flpMain";
            // 
            // btnAddItem
            // 
            resources.ApplyResources(this.btnAddItem, "btnAddItem");
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // txtInvoiceNo
            // 
            resources.ApplyResources(this.txtInvoiceNo, "txtInvoiceNo");
            this.txtInvoiceNo.Name = "txtInvoiceNo";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // ucCustomerInfo1
            // 
            this.ucCustomerInfo1.FullName = "";
            resources.ApplyResources(this.ucCustomerInfo1, "ucCustomerInfo1");
            this.ucCustomerInfo1.Name = "ucCustomerInfo1";
            // 
            // ucCC1
            // 
            resources.ApplyResources(this.ucCC1, "ucCC1");
            this.ucCC1.Name = "ucCC1";
            // 
            // ucCC2
            // 
            resources.ApplyResources(this.ucCC2, "ucCC2");
            this.ucCC2.Name = "ucCC2";
            // 
            // ucCC3
            // 
            resources.ApplyResources(this.ucCC3, "ucCC3");
            this.ucCC3.Name = "ucCC3";
            // 
            // ucShippingAddress
            // 
            this.ucShippingAddress.AddressType = QuickBooks.BusObj.Enums.AddressType.Billing;
            resources.ApplyResources(this.ucShippingAddress, "ucShippingAddress");
            this.ucShippingAddress.Name = "ucShippingAddress";
            // 
            // ucBillingAddress
            // 
            this.ucBillingAddress.AddressType = QuickBooks.BusObj.Enums.AddressType.Billing;
            resources.ApplyResources(this.ucBillingAddress, "ucBillingAddress");
            this.ucBillingAddress.Name = "ucBillingAddress";
            // 
            // frmOrder
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInvoiceNo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.dtOrderDate);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "frmOrder";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmOrder_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmOrder_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpCust.ResumeLayout(false);
            this.tpCust.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tcCreditCards.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tpOrderDetails.ResumeLayout(false);
            this.tpOrderDetails.PerformLayout();
            this.flpMain.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem testToolStripMenuItem;
        private System.Windows.Forms.DateTimePicker dtOrderDate;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpCust;
        private System.Windows.Forms.TabPage tpOrderDetails;
        private System.Windows.Forms.TextBox txtInvoiceNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpMain;
        private ucLineItem ucLineItem1;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPaypal;
        private System.Windows.Forms.RadioButton rbMoneyOrder;
        private System.Windows.Forms.RadioButton rbCheck;
        private System.Windows.Forms.RadioButton rbCC;
        private System.Windows.Forms.TextBox txtYear;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TextBox txtRow3;
        private System.Windows.Forms.TabControl tcCreditCards;
        private System.Windows.Forms.TabPage tabPage1;
        private ucCreditCard ucCC1;
        private System.Windows.Forms.TabPage tabPage2;
        private ucCreditCard ucCC2;
        private System.Windows.Forms.TabPage tabPage3;
        private ucCreditCard ucCC3;
        private System.Windows.Forms.TextBox txtRow2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRow1;
        private ucAddress ucShippingAddress;
        private System.Windows.Forms.TextBox txtTrim;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtModel;
        private ucAddress ucBillingAddress;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btnCustLookup;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox txtGrandTotal;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.TextBox txtTaxes;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox txtSubTotal;
        private System.Windows.Forms.Label label30;
        private ucCustomerInfo ucCustomerInfo1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToRightPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveToQuickBooksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem orderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseOrder;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.RadioButton rbWireTransfer;
        private System.Windows.Forms.RadioButton rbCredit;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveAsSwatchOrder;
    }
}