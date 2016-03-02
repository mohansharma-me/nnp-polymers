namespace NNPPoly.forms
{
    partial class frmCollectionList
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvDebits = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnDebitInvoice = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDays = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panProcess = new System.Windows.Forms.Panel();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lcProcess = new MyGUI.Preloader.LoadingCircle();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExportToExcel = new System.Windows.Forms.Button();
            this.btnSendCollectionSMS = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lvClients = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnClientName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnCollectingAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnRegenerate = new System.Windows.Forms.Button();
            this.txtTotalCollectingAmount = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvDebits)).BeginInit();
            this.panProcess.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lvDebits);
            this.panel1.Controls.Add(this.panProcess);
            this.panel1.Location = new System.Drawing.Point(427, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(351, 468);
            this.panel1.TabIndex = 0;
            // 
            // lvDebits
            // 
            this.lvDebits.AllColumns.Add(this.olvColumnDebitInvoice);
            this.lvDebits.AllColumns.Add(this.olvColumnDate);
            this.lvDebits.AllColumns.Add(this.olvColumnAmount);
            this.lvDebits.AllColumns.Add(this.olvColumnDays);
            this.lvDebits.AllowColumnReorder = true;
            this.lvDebits.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvDebits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDebits.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.lvDebits.CellEditTabChangesRows = true;
            this.lvDebits.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnDebitInvoice,
            this.olvColumnDate,
            this.olvColumnAmount,
            this.olvColumnDays});
            this.lvDebits.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDebits.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDebits.EmptyListMsg = "Zero entries";
            this.lvDebits.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDebits.FullRowSelect = true;
            this.lvDebits.GridLines = true;
            this.lvDebits.HeaderWordWrap = true;
            this.lvDebits.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvDebits.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvDebits.Location = new System.Drawing.Point(0, 0);
            this.lvDebits.MultiSelect = false;
            this.lvDebits.Name = "lvDebits";
            this.lvDebits.OverlayImage.Image = global::NNPPoly.Properties.Resources.adduser;
            this.lvDebits.OverlayText.Text = "Clients";
            this.lvDebits.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvDebits.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvDebits.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvDebits.ShowCommandMenuOnRightClick = true;
            this.lvDebits.ShowGroups = false;
            this.lvDebits.ShowItemToolTips = true;
            this.lvDebits.Size = new System.Drawing.Size(351, 468);
            this.lvDebits.TabIndex = 2;
            this.lvDebits.TintSortColumn = true;
            this.lvDebits.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvDebits.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvDebits.UseAlternatingBackColors = true;
            this.lvDebits.UseCompatibleStateImageBehavior = false;
            this.lvDebits.UseCustomSelectionColors = true;
            this.lvDebits.UseExplorerTheme = true;
            this.lvDebits.UseFilterIndicator = true;
            this.lvDebits.UseFiltering = true;
            this.lvDebits.UseHotItem = true;
            this.lvDebits.UseHyperlinks = true;
            this.lvDebits.UseOverlays = false;
            this.lvDebits.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnDebitInvoice
            // 
            this.olvColumnDebitInvoice.AspectName = "invoice";
            this.olvColumnDebitInvoice.FillsFreeSpace = true;
            this.olvColumnDebitInvoice.HeaderCheckBox = true;
            this.olvColumnDebitInvoice.IsEditable = false;
            this.olvColumnDebitInvoice.Text = "Invoice";
            this.olvColumnDebitInvoice.Width = 114;
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "date";
            this.olvColumnDate.FillsFreeSpace = true;
            this.olvColumnDate.IsEditable = false;
            this.olvColumnDate.Text = "Date";
            this.olvColumnDate.Width = 108;
            // 
            // olvColumnAmount
            // 
            this.olvColumnAmount.AspectName = "amount";
            this.olvColumnAmount.FillsFreeSpace = true;
            this.olvColumnAmount.IsEditable = false;
            this.olvColumnAmount.Text = "Amount";
            // 
            // olvColumnDays
            // 
            this.olvColumnDays.AspectName = "days";
            this.olvColumnDays.FillsFreeSpace = true;
            this.olvColumnDays.IsEditable = false;
            this.olvColumnDays.Text = "Days";
            // 
            // panProcess
            // 
            this.panProcess.Controls.Add(this.pbProcess);
            this.panProcess.Controls.Add(this.lblProcess);
            this.panProcess.Controls.Add(this.lcProcess);
            this.panProcess.Location = new System.Drawing.Point(656, 3);
            this.panProcess.Name = "panProcess";
            this.panProcess.Size = new System.Drawing.Size(17, 18);
            this.panProcess.TabIndex = 0;
            // 
            // pbProcess
            // 
            this.pbProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbProcess.Location = new System.Drawing.Point(-107, 22);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(229, 23);
            this.pbProcess.TabIndex = 2;
            // 
            // lblProcess
            // 
            this.lblProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProcess.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.Location = new System.Drawing.Point(-317, -21);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(651, 38);
            this.lblProcess.TabIndex = 1;
            this.lblProcess.Text = "Processing";
            this.lblProcess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lcProcess
            // 
            this.lcProcess.Active = false;
            this.lcProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lcProcess.Color = System.Drawing.Color.Black;
            this.lcProcess.InnerCircleRadius = 5;
            this.lcProcess.Location = new System.Drawing.Point(-27, -76);
            this.lcProcess.Name = "lcProcess";
            this.lcProcess.NumberSpoke = 12;
            this.lcProcess.OuterCircleRadius = 11;
            this.lcProcess.RotationSpeed = 100;
            this.lcProcess.Size = new System.Drawing.Size(66, 48);
            this.lcProcess.SpokeThickness = 2;
            this.lcProcess.StylePreset = MyGUI.Preloader.LoadingCircle.StylePresets.MacOSX;
            this.lcProcess.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnExportToExcel);
            this.panel2.Controls.Add(this.btnSendCollectionSMS);
            this.panel2.Controls.Add(this.btnPrint);
            this.panel2.Location = new System.Drawing.Point(427, 486);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(351, 42);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // btnExportToExcel
            // 
            this.btnExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportToExcel.Location = new System.Drawing.Point(248, 7);
            this.btnExportToExcel.Name = "btnExportToExcel";
            this.btnExportToExcel.Size = new System.Drawing.Size(100, 33);
            this.btnExportToExcel.TabIndex = 0;
            this.btnExportToExcel.Text = "&To Excel";
            this.btnExportToExcel.UseVisualStyleBackColor = true;
            this.btnExportToExcel.Click += new System.EventHandler(this.btnExportToExcel_Click);
            // 
            // btnSendCollectionSMS
            // 
            this.btnSendCollectionSMS.Location = new System.Drawing.Point(3, 7);
            this.btnSendCollectionSMS.Name = "btnSendCollectionSMS";
            this.btnSendCollectionSMS.Size = new System.Drawing.Size(74, 33);
            this.btnSendCollectionSMS.TabIndex = 0;
            this.btnSendCollectionSMS.Text = "&Send";
            this.btnSendCollectionSMS.UseVisualStyleBackColor = true;
            this.btnSendCollectionSMS.Click += new System.EventHandler(this.btnSendCollectionSMS_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(154, 7);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(88, 33);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lvClients
            // 
            this.lvClients.AllColumns.Add(this.olvColumnClientName);
            this.lvClients.AllColumns.Add(this.olvColumnCollectingAmount);
            this.lvClients.AllowColumnReorder = true;
            this.lvClients.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvClients.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.lvClients.CellEditTabChangesRows = true;
            this.lvClients.CheckBoxes = true;
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnClientName,
            this.olvColumnCollectingAmount});
            this.lvClients.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvClients.EmptyListMsg = "Click \"Refresh\" to generate collection list";
            this.lvClients.EmptyListMsgFont = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderWordWrap = true;
            this.lvClients.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvClients.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.Location = new System.Drawing.Point(12, 51);
            this.lvClients.MultiSelect = false;
            this.lvClients.Name = "lvClients";
            this.lvClients.OverlayImage.Image = global::NNPPoly.Properties.Resources.adduser;
            this.lvClients.OverlayText.Text = "Clients";
            this.lvClients.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvClients.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvClients.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvClients.ShowCommandMenuOnRightClick = true;
            this.lvClients.ShowGroups = false;
            this.lvClients.ShowItemToolTips = true;
            this.lvClients.Size = new System.Drawing.Size(409, 442);
            this.lvClients.TabIndex = 2;
            this.lvClients.TintSortColumn = true;
            this.lvClients.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvClients.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.UseAlternatingBackColors = true;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.UseCustomSelectionColors = true;
            this.lvClients.UseExplorerTheme = true;
            this.lvClients.UseFilterIndicator = true;
            this.lvClients.UseFiltering = true;
            this.lvClients.UseHotItem = true;
            this.lvClients.UseHyperlinks = true;
            this.lvClients.UseOverlays = false;
            this.lvClients.View = System.Windows.Forms.View.Details;
            this.lvClients.SelectedIndexChanged += new System.EventHandler(this.lvClients_SelectedIndexChanged);
            // 
            // olvColumnClientName
            // 
            this.olvColumnClientName.AspectName = "client.name";
            this.olvColumnClientName.FillsFreeSpace = true;
            this.olvColumnClientName.HeaderCheckBox = true;
            this.olvColumnClientName.IsEditable = false;
            this.olvColumnClientName.Text = "Client";
            this.olvColumnClientName.Width = 114;
            // 
            // olvColumnCollectingAmount
            // 
            this.olvColumnCollectingAmount.AspectName = "CollectingAmount";
            this.olvColumnCollectingAmount.FillsFreeSpace = true;
            this.olvColumnCollectingAmount.IsEditable = false;
            this.olvColumnCollectingAmount.Text = "Total Due";
            this.olvColumnCollectingAmount.Width = 108;
            // 
            // btnRegenerate
            // 
            this.btnRegenerate.Location = new System.Drawing.Point(12, 12);
            this.btnRegenerate.Name = "btnRegenerate";
            this.btnRegenerate.Size = new System.Drawing.Size(409, 33);
            this.btnRegenerate.TabIndex = 3;
            this.btnRegenerate.Text = "&Refresh";
            this.btnRegenerate.UseVisualStyleBackColor = true;
            this.btnRegenerate.Click += new System.EventHandler(this.btnRegenerate_Click);
            // 
            // txtTotalCollectingAmount
            // 
            this.txtTotalCollectingAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTotalCollectingAmount.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCollectingAmount.Location = new System.Drawing.Point(12, 499);
            this.txtTotalCollectingAmount.Name = "txtTotalCollectingAmount";
            this.txtTotalCollectingAmount.ReadOnly = true;
            this.txtTotalCollectingAmount.Size = new System.Drawing.Size(409, 29);
            this.txtTotalCollectingAmount.TabIndex = 4;
            // 
            // frmCollectionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(790, 540);
            this.Controls.Add(this.txtTotalCollectingAmount);
            this.Controls.Add(this.btnRegenerate);
            this.Controls.Add(this.lvClients);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmCollectionList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Collection List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.frmCollectionList_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvDebits)).EndInit();
            this.panProcess.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panProcess;
        private MyGUI.Preloader.LoadingCircle lcProcess;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.ProgressBar pbProcess;
        public BrightIdeasSoftware.ObjectListView lvClients;
        private BrightIdeasSoftware.OLVColumn olvColumnClientName;
        private BrightIdeasSoftware.OLVColumn olvColumnCollectingAmount;
        private System.Windows.Forms.Button btnRegenerate;
        public BrightIdeasSoftware.ObjectListView lvDebits;
        private BrightIdeasSoftware.OLVColumn olvColumnDebitInvoice;
        private BrightIdeasSoftware.OLVColumn olvColumnDate;
        private BrightIdeasSoftware.OLVColumn olvColumnAmount;
        private BrightIdeasSoftware.OLVColumn olvColumnDays;
        private System.Windows.Forms.TextBox txtTotalCollectingAmount;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportToExcel;
        private System.Windows.Forms.Button btnSendCollectionSMS;

    }
}

