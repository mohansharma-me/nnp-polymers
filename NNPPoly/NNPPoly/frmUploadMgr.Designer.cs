namespace NNPPoly
{
    partial class frmUploadMgr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUploadMgr));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEntruToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.importFromExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshDataFromDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripWriteChanges = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.lvData = new BrightIdeasSoftware.FastObjectListView();
            this.olvColumnStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDebitNotes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnClients = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnPaymentMode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnInvoice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnGrade = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.restoreSavepointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvData)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.refreshDataFromDatabaseToolStripMenuItem,
            this.uploadNowToolStripMenuItem,
            this.toolStripWriteChanges});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEntruToolStripMenuItem,
            this.toolStripMenuItem3,
            this.importFromExcelToolStripMenuItem,
            this.toolStripMenuItem1,
            this.restoreSavepointToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveCloseToolStripMenuItem,
            this.toolStripMenuItem2,
            this.closeToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // newEntruToolStripMenuItem
            // 
            this.newEntruToolStripMenuItem.Name = "newEntruToolStripMenuItem";
            this.newEntruToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newEntruToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.newEntruToolStripMenuItem.Text = "&New Entry";
            this.newEntruToolStripMenuItem.Click += new System.EventHandler(this.newEntruToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(230, 6);
            // 
            // importFromExcelToolStripMenuItem
            // 
            this.importFromExcelToolStripMenuItem.Name = "importFromExcelToolStripMenuItem";
            this.importFromExcelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.importFromExcelToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.importFromExcelToolStripMenuItem.Text = "&Import from Excel";
            this.importFromExcelToolStripMenuItem.Click += new System.EventHandler(this.importFromExcelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(230, 6);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveCloseToolStripMenuItem
            // 
            this.saveCloseToolStripMenuItem.Name = "saveCloseToolStripMenuItem";
            this.saveCloseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveCloseToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.saveCloseToolStripMenuItem.Text = "Save && &Close";
            this.saveCloseToolStripMenuItem.Click += new System.EventHandler(this.saveCloseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(230, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.closeToolStripMenuItem.Text = "&Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // uploadNowToolStripMenuItem
            // 
            this.uploadNowToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.uploadNowToolStripMenuItem.BackColor = System.Drawing.Color.Orange;
            this.uploadNowToolStripMenuItem.Name = "uploadNowToolStripMenuItem";
            this.uploadNowToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
            this.uploadNowToolStripMenuItem.Text = "&Upload Now";
            this.uploadNowToolStripMenuItem.Click += new System.EventHandler(this.uploadNowToolStripMenuItem_Click);
            // 
            // refreshDataFromDatabaseToolStripMenuItem
            // 
            this.refreshDataFromDatabaseToolStripMenuItem.Name = "refreshDataFromDatabaseToolStripMenuItem";
            this.refreshDataFromDatabaseToolStripMenuItem.Size = new System.Drawing.Size(125, 20);
            this.refreshDataFromDatabaseToolStripMenuItem.Text = "&Restore Savepoint";
            this.refreshDataFromDatabaseToolStripMenuItem.Click += new System.EventHandler(this.refreshDataFromDatabaseToolStripMenuItem_Click);
            // 
            // toolStripWriteChanges
            // 
            this.toolStripWriteChanges.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripWriteChanges.BackColor = System.Drawing.Color.Green;
            this.toolStripWriteChanges.ForeColor = System.Drawing.Color.White;
            this.toolStripWriteChanges.Name = "toolStripWriteChanges";
            this.toolStripWriteChanges.Size = new System.Drawing.Size(89, 20);
            this.toolStripWriteChanges.Text = "&Upload Now";
            this.toolStripWriteChanges.Visible = false;
            this.toolStripWriteChanges.Click += new System.EventHandler(this.toolStripWriteChanges_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "yes");
            this.imageList1.Images.SetKeyName(1, "no");
            this.imageList1.Images.SetKeyName(2, "info");
            // 
            // lvData
            // 
            this.lvData.AllColumns.Add(this.olvColumnStatus);
            this.lvData.AllColumns.Add(this.olvColumnDebitNotes);
            this.lvData.AllColumns.Add(this.olvColumnClients);
            this.lvData.AllColumns.Add(this.olvColumnPaymentMode);
            this.lvData.AllColumns.Add(this.olvColumnInvoice);
            this.lvData.AllColumns.Add(this.olvColumnDate);
            this.lvData.AllColumns.Add(this.olvColumnType);
            this.lvData.AllColumns.Add(this.olvColumnGrade);
            this.lvData.AllColumns.Add(this.olvColumnQty);
            this.lvData.AllColumns.Add(this.olvColumnAmount);
            this.lvData.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvData.CellEditTabChangesRows = true;
            this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnStatus,
            this.olvColumnDebitNotes,
            this.olvColumnClients,
            this.olvColumnPaymentMode,
            this.olvColumnInvoice,
            this.olvColumnDate,
            this.olvColumnType,
            this.olvColumnGrade,
            this.olvColumnQty,
            this.olvColumnAmount});
            this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvData.EmptyListMsg = "Press \"CTRL+N\" for new entry...";
            this.lvData.FullRowSelect = true;
            this.lvData.GridLines = true;
            this.lvData.LargeImageList = this.imageList1;
            this.lvData.Location = new System.Drawing.Point(0, 24);
            this.lvData.Name = "lvData";
            this.lvData.OverlayText.BackColor = System.Drawing.Color.Black;
            this.lvData.OverlayText.BorderColor = System.Drawing.Color.Blue;
            this.lvData.OverlayText.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvData.OverlayText.Text = "Press \"CTRL+N\" for new entry...";
            this.lvData.OverlayText.TextColor = System.Drawing.Color.White;
            this.lvData.OverlayText.Transparency = 255;
            this.lvData.OwnerDraw = true;
            this.lvData.ShowGroups = false;
            this.lvData.Size = new System.Drawing.Size(684, 437);
            this.lvData.SmallImageList = this.imageList1;
            this.lvData.TabIndex = 1;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            this.lvData.VirtualMode = true;
            this.lvData.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvData_CellEditFinishing);
            this.lvData.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvData_CellEditStarting);
            this.lvData.SelectedIndexChanged += new System.EventHandler(this.lvData_SelectedIndexChanged);
            this.lvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvData_KeyDown);
            // 
            // olvColumnStatus
            // 
            this.olvColumnStatus.Groupable = false;
            this.olvColumnStatus.IsEditable = false;
            this.olvColumnStatus.Searchable = false;
            this.olvColumnStatus.Sortable = false;
            this.olvColumnStatus.Text = "Status";
            this.olvColumnStatus.UseFiltering = false;
            // 
            // olvColumnDebitNotes
            // 
            this.olvColumnDebitNotes.AspectName = "debit_notes";
            this.olvColumnDebitNotes.FillsFreeSpace = true;
            this.olvColumnDebitNotes.Text = "Debit Notes";
            // 
            // olvColumnClients
            // 
            this.olvColumnClients.AspectName = "client_name";
            this.olvColumnClients.FillsFreeSpace = true;
            this.olvColumnClients.Text = "Client";
            // 
            // olvColumnPaymentMode
            // 
            this.olvColumnPaymentMode.AspectName = "modes";
            this.olvColumnPaymentMode.FillsFreeSpace = true;
            this.olvColumnPaymentMode.Text = "Payment Mode";
            // 
            // olvColumnInvoice
            // 
            this.olvColumnInvoice.AspectName = "invoice";
            this.olvColumnInvoice.FillsFreeSpace = true;
            this.olvColumnInvoice.Text = "Invoice";
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "date";
            this.olvColumnDate.FillsFreeSpace = true;
            this.olvColumnDate.Text = "Date";
            // 
            // olvColumnType
            // 
            this.olvColumnType.AspectName = "type";
            this.olvColumnType.FillsFreeSpace = true;
            this.olvColumnType.Text = "Type";
            // 
            // olvColumnGrade
            // 
            this.olvColumnGrade.AspectName = "grade_code";
            this.olvColumnGrade.FillsFreeSpace = true;
            this.olvColumnGrade.Text = "Material/Grade";
            // 
            // olvColumnQty
            // 
            this.olvColumnQty.AspectName = "mt";
            this.olvColumnQty.FillsFreeSpace = true;
            this.olvColumnQty.Text = "Quantity";
            // 
            // olvColumnAmount
            // 
            this.olvColumnAmount.AspectName = "amount";
            this.olvColumnAmount.FillsFreeSpace = true;
            this.olvColumnAmount.Text = "Amount";
            // 
            // restoreSavepointToolStripMenuItem
            // 
            this.restoreSavepointToolStripMenuItem.Name = "restoreSavepointToolStripMenuItem";
            this.restoreSavepointToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.restoreSavepointToolStripMenuItem.Size = new System.Drawing.Size(233, 22);
            this.restoreSavepointToolStripMenuItem.Text = "&Restore Savepoint";
            this.restoreSavepointToolStripMenuItem.Click += new System.EventHandler(this.restoreSavepointToolStripMenuItem_Click);
            // 
            // frmUploadMgr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(684, 461);
            this.Controls.Add(this.lvData);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "frmUploadMgr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload Manager - NNP Poly - WebcodeZ Infoway";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmUploadMgr_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadNowToolStripMenuItem;
        private BrightIdeasSoftware.FastObjectListView lvData;
        private BrightIdeasSoftware.OLVColumn olvColumnDebitNotes;
        private BrightIdeasSoftware.OLVColumn olvColumnClients;
        private BrightIdeasSoftware.OLVColumn olvColumnPaymentMode;
        private BrightIdeasSoftware.OLVColumn olvColumnInvoice;
        private BrightIdeasSoftware.OLVColumn olvColumnDate;
        private BrightIdeasSoftware.OLVColumn olvColumnGrade;
        private BrightIdeasSoftware.OLVColumn olvColumnQty;
        private BrightIdeasSoftware.OLVColumn olvColumnAmount;
        private System.Windows.Forms.ToolStripMenuItem newEntruToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private BrightIdeasSoftware.OLVColumn olvColumnStatus;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumnType;
        private System.Windows.Forms.ToolStripMenuItem refreshDataFromDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripWriteChanges;
        private System.Windows.Forms.ToolStripMenuItem restoreSavepointToolStripMenuItem;
    }
}

