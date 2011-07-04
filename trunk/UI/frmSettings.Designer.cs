namespace QuickBooks.UI
{
    partial class frmSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaxLogSize = new System.Windows.Forms.TextBox();
            this.txtAppRootPath = new System.Windows.Forms.TextBox();
            this.txtQBAppName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtQBFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtPrivateFieldsID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowseQBFile = new System.Windows.Forms.Button();
            this.btnBrowseAppRoot = new System.Windows.Forms.Button();
            this.ofdQBFile = new System.Windows.Forms.OpenFileDialog();
            this.txtPendingOrdersDir = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnPendingOrdersDir = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtInStateTaxCode = new System.Windows.Forms.TextBox();
            this.txtTaxableRate = new System.Windows.Forms.TextBox();
            this.txtTaxableState = new System.Windows.Forms.TextBox();
            this.txtOutOfStateTaxCode = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(22, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Max Log Size (bytes)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtMaxLogSize
            // 
            this.txtMaxLogSize.Location = new System.Drawing.Point(148, 97);
            this.txtMaxLogSize.Name = "txtMaxLogSize";
            this.txtMaxLogSize.Size = new System.Drawing.Size(253, 20);
            this.txtMaxLogSize.TabIndex = 3;
            // 
            // txtAppRootPath
            // 
            this.txtAppRootPath.Location = new System.Drawing.Point(148, 71);
            this.txtAppRootPath.Name = "txtAppRootPath";
            this.txtAppRootPath.Size = new System.Drawing.Size(253, 20);
            this.txtAppRootPath.TabIndex = 2;
            // 
            // txtQBAppName
            // 
            this.txtQBAppName.Enabled = false;
            this.txtQBAppName.Location = new System.Drawing.Point(148, 123);
            this.txtQBAppName.Name = "txtQBAppName";
            this.txtQBAppName.Size = new System.Drawing.Size(253, 20);
            this.txtQBAppName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(42, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "App Root Path";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(22, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "QuickBooks App Name";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtQBFilePath
            // 
            this.txtQBFilePath.Location = new System.Drawing.Point(148, 19);
            this.txtQBFilePath.Name = "txtQBFilePath";
            this.txtQBFilePath.Size = new System.Drawing.Size(253, 20);
            this.txtQBFilePath.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(22, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "QuickBooks File Path";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(392, 284);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 284);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.TabStop = false;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtPrivateFieldsID
            // 
            this.txtPrivateFieldsID.Enabled = false;
            this.txtPrivateFieldsID.Location = new System.Drawing.Point(148, 149);
            this.txtPrivateFieldsID.Name = "txtPrivateFieldsID";
            this.txtPrivateFieldsID.Size = new System.Drawing.Size(253, 20);
            this.txtPrivateFieldsID.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(22, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Private Fields ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnBrowseQBFile
            // 
            this.btnBrowseQBFile.Location = new System.Drawing.Point(413, 18);
            this.btnBrowseQBFile.Name = "btnBrowseQBFile";
            this.btnBrowseQBFile.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseQBFile.TabIndex = 7;
            this.btnBrowseQBFile.TabStop = false;
            this.btnBrowseQBFile.Text = "...";
            this.btnBrowseQBFile.UseVisualStyleBackColor = true;
            this.btnBrowseQBFile.Click += new System.EventHandler(this.btnBrowseQBFile_Click);
            // 
            // btnBrowseAppRoot
            // 
            this.btnBrowseAppRoot.Location = new System.Drawing.Point(413, 68);
            this.btnBrowseAppRoot.Name = "btnBrowseAppRoot";
            this.btnBrowseAppRoot.Size = new System.Drawing.Size(24, 23);
            this.btnBrowseAppRoot.TabIndex = 6;
            this.btnBrowseAppRoot.TabStop = false;
            this.btnBrowseAppRoot.Text = "...";
            this.btnBrowseAppRoot.UseVisualStyleBackColor = true;
            this.btnBrowseAppRoot.Click += new System.EventHandler(this.btnBrowseAppRoot_Click);
            // 
            // ofdQBFile
            // 
            this.ofdQBFile.Filter = "QuickBooks File (*.qbw)|*.qbw";
            // 
            // txtPendingOrdersDir
            // 
            this.txtPendingOrdersDir.Location = new System.Drawing.Point(148, 45);
            this.txtPendingOrdersDir.Name = "txtPendingOrdersDir";
            this.txtPendingOrdersDir.Size = new System.Drawing.Size(253, 20);
            this.txtPendingOrdersDir.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(12, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "Pending Orders Directory";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnPendingOrdersDir
            // 
            this.btnPendingOrdersDir.Location = new System.Drawing.Point(413, 45);
            this.btnPendingOrdersDir.Name = "btnPendingOrdersDir";
            this.btnPendingOrdersDir.Size = new System.Drawing.Size(24, 23);
            this.btnPendingOrdersDir.TabIndex = 8;
            this.btnPendingOrdersDir.TabStop = false;
            this.btnPendingOrdersDir.Text = "...";
            this.btnPendingOrdersDir.UseVisualStyleBackColor = true;
            this.btnPendingOrdersDir.Click += new System.EventHandler(this.btnPendingOrdersDir_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txtInStateTaxCode);
            this.groupBox1.Controls.Add(this.txtTaxableRate);
            this.groupBox1.Controls.Add(this.txtTaxableState);
            this.groupBox1.Controls.Add(this.txtOutOfStateTaxCode);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Location = new System.Drawing.Point(12, 199);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 79);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Taxes";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(203, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(10, 15);
            this.label11.TabIndex = 1;
            this.label11.Text = "%";
            // 
            // txtInStateTaxCode
            // 
            this.txtInStateTaxCode.Location = new System.Drawing.Point(331, 45);
            this.txtInStateTaxCode.Name = "txtInStateTaxCode";
            this.txtInStateTaxCode.Size = new System.Drawing.Size(82, 20);
            this.txtInStateTaxCode.TabIndex = 3;
            // 
            // txtTaxableRate
            // 
            this.txtTaxableRate.Location = new System.Drawing.Point(133, 45);
            this.txtTaxableRate.Name = "txtTaxableRate";
            this.txtTaxableRate.Size = new System.Drawing.Size(70, 20);
            this.txtTaxableRate.TabIndex = 2;
            // 
            // txtTaxableState
            // 
            this.txtTaxableState.Location = new System.Drawing.Point(331, 19);
            this.txtTaxableState.Name = "txtTaxableState";
            this.txtTaxableState.Size = new System.Drawing.Size(82, 20);
            this.txtTaxableState.TabIndex = 1;
            // 
            // txtOutOfStateTaxCode
            // 
            this.txtOutOfStateTaxCode.Location = new System.Drawing.Point(133, 19);
            this.txtOutOfStateTaxCode.Name = "txtOutOfStateTaxCode";
            this.txtOutOfStateTaxCode.Size = new System.Drawing.Size(70, 20);
            this.txtOutOfStateTaxCode.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(11, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(116, 15);
            this.label10.TabIndex = 0;
            this.label10.Text = "Taxable Rate";
            this.label10.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(11, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(116, 15);
            this.label9.TabIndex = 0;
            this.label9.Text = "Out of State Tax Code";
            this.label9.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(228, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "In State Tax Code";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(228, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Taxable State";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnPendingOrdersDir);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnBrowseAppRoot);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnBrowseQBFile);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtMaxLogSize);
            this.groupBox2.Controls.Add(this.txtPrivateFieldsID);
            this.groupBox2.Controls.Add(this.txtAppRootPath);
            this.groupBox2.Controls.Add(this.txtPendingOrdersDir);
            this.groupBox2.Controls.Add(this.txtQBAppName);
            this.groupBox2.Controls.Add(this.txtQBFilePath);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 181);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Basic Settings";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 318);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Application Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaxLogSize;
        private System.Windows.Forms.TextBox txtAppRootPath;
        private System.Windows.Forms.TextBox txtQBAppName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtQBFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox txtPrivateFieldsID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowseQBFile;
        private System.Windows.Forms.Button btnBrowseAppRoot;
        private System.Windows.Forms.OpenFileDialog ofdQBFile;
        private System.Windows.Forms.TextBox txtPendingOrdersDir;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnPendingOrdersDir;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtOutOfStateTaxCode;
        private System.Windows.Forms.TextBox txtInStateTaxCode;
        private System.Windows.Forms.TextBox txtTaxableRate;
        private System.Windows.Forms.TextBox txtTaxableState;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
    }
}