namespace NNPPoly.forms
{
    partial class frmInterestAdvise
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
            this.lvInterests = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnClientName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnOpeningBalance = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnClosingBalance = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnInterestAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lcProcess = new MyGUI.Preloader.LoadingCircle();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtTotalInterestAmount = new System.Windows.Forms.TextBox();
            this.dtMonth = new System.Windows.Forms.DateTimePicker();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvInterests)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.lvInterests);
            this.panel1.Controls.Add(this.pbProcess);
            this.panel1.Controls.Add(this.lblProcess);
            this.panel1.Controls.Add(this.lcProcess);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 422);
            this.panel1.TabIndex = 0;
            // 
            // lvInterests
            // 
            this.lvInterests.AllColumns.Add(this.olvColumnClientName);
            this.lvInterests.AllColumns.Add(this.olvColumnOpeningBalance);
            this.lvInterests.AllColumns.Add(this.olvColumnClosingBalance);
            this.lvInterests.AllColumns.Add(this.olvColumnInterestAmount);
            this.lvInterests.AllowColumnReorder = true;
            this.lvInterests.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvInterests.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvInterests.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.lvInterests.CellEditTabChangesRows = true;
            this.lvInterests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnClientName,
            this.olvColumnOpeningBalance,
            this.olvColumnClosingBalance,
            this.olvColumnInterestAmount});
            this.lvInterests.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvInterests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInterests.EmptyListMsg = "Please click \"Refresh\" button to generate Interest Advise List";
            this.lvInterests.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvInterests.FullRowSelect = true;
            this.lvInterests.GridLines = true;
            this.lvInterests.HeaderWordWrap = true;
            this.lvInterests.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvInterests.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvInterests.Location = new System.Drawing.Point(0, 0);
            this.lvInterests.MultiSelect = false;
            this.lvInterests.Name = "lvInterests";
            this.lvInterests.OverlayImage.Image = global::NNPPoly.Properties.Resources.adduser;
            this.lvInterests.OverlayText.Text = "Clients";
            this.lvInterests.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvInterests.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvInterests.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvInterests.ShowCommandMenuOnRightClick = true;
            this.lvInterests.ShowGroups = false;
            this.lvInterests.ShowItemToolTips = true;
            this.lvInterests.Size = new System.Drawing.Size(676, 422);
            this.lvInterests.TabIndex = 3;
            this.lvInterests.TintSortColumn = true;
            this.lvInterests.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvInterests.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvInterests.UseAlternatingBackColors = true;
            this.lvInterests.UseCompatibleStateImageBehavior = false;
            this.lvInterests.UseCustomSelectionColors = true;
            this.lvInterests.UseExplorerTheme = true;
            this.lvInterests.UseFilterIndicator = true;
            this.lvInterests.UseFiltering = true;
            this.lvInterests.UseHotItem = true;
            this.lvInterests.UseHyperlinks = true;
            this.lvInterests.UseOverlays = false;
            this.lvInterests.View = System.Windows.Forms.View.Details;
            this.lvInterests.SelectedIndexChanged += new System.EventHandler(this.lvInterests_SelectedIndexChanged);
            this.lvInterests.DoubleClick += new System.EventHandler(this.lvInterests_DoubleClick);
            this.lvInterests.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvInterests_KeyDown);
            // 
            // olvColumnClientName
            // 
            this.olvColumnClientName.AspectName = "client.name";
            this.olvColumnClientName.FillsFreeSpace = true;
            this.olvColumnClientName.IsEditable = false;
            this.olvColumnClientName.Text = "Client";
            this.olvColumnClientName.Width = 70;
            // 
            // olvColumnOpeningBalance
            // 
            this.olvColumnOpeningBalance.AspectName = "opening_balance";
            this.olvColumnOpeningBalance.Text = "Opening Balance";
            this.olvColumnOpeningBalance.Width = 169;
            // 
            // olvColumnClosingBalance
            // 
            this.olvColumnClosingBalance.AspectName = "closing_balance";
            this.olvColumnClosingBalance.Text = "Closing Balance";
            this.olvColumnClosingBalance.Width = 180;
            // 
            // olvColumnInterestAmount
            // 
            this.olvColumnInterestAmount.AspectName = "interest_amount";
            this.olvColumnInterestAmount.Text = "Interest Amount";
            this.olvColumnInterestAmount.Width = 158;
            // 
            // pbProcess
            // 
            this.pbProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbProcess.Location = new System.Drawing.Point(257, 216);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(157, 23);
            this.pbProcess.TabIndex = 4;
            // 
            // lblProcess
            // 
            this.lblProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProcess.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.Location = new System.Drawing.Point(16, 170);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(646, 39);
            this.lblProcess.TabIndex = 1;
            this.lblProcess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lcProcess
            // 
            this.lcProcess.Active = false;
            this.lcProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lcProcess.Color = System.Drawing.Color.Black;
            this.lcProcess.InnerCircleRadius = 5;
            this.lcProcess.Location = new System.Drawing.Point(306, 117);
            this.lcProcess.Name = "lcProcess";
            this.lcProcess.NumberSpoke = 12;
            this.lcProcess.OuterCircleRadius = 11;
            this.lcProcess.RotationSpeed = 100;
            this.lcProcess.Size = new System.Drawing.Size(58, 50);
            this.lcProcess.SpokeThickness = 2;
            this.lcProcess.StylePreset = MyGUI.Preloader.LoadingCircle.StylePresets.MacOSX;
            this.lcProcess.TabIndex = 0;
            this.lcProcess.Text = "loadingCircle1";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.txtTotalInterestAmount);
            this.panel3.Controls.Add(this.dtMonth);
            this.panel3.Controls.Add(this.btnPrint);
            this.panel3.Controls.Add(this.btnExport);
            this.panel3.Controls.Add(this.btnRefresh);
            this.panel3.Location = new System.Drawing.Point(12, 440);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(676, 48);
            this.panel3.TabIndex = 0;
            // 
            // txtTotalInterestAmount
            // 
            this.txtTotalInterestAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotalInterestAmount.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalInterestAmount.Location = new System.Drawing.Point(266, 8);
            this.txtTotalInterestAmount.Name = "txtTotalInterestAmount";
            this.txtTotalInterestAmount.Size = new System.Drawing.Size(215, 30);
            this.txtTotalInterestAmount.TabIndex = 2;
            this.txtTotalInterestAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dtMonth
            // 
            this.dtMonth.CustomFormat = "MM-yyyy";
            this.dtMonth.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtMonth.Location = new System.Drawing.Point(3, 6);
            this.dtMonth.Name = "dtMonth";
            this.dtMonth.Size = new System.Drawing.Size(161, 35);
            this.dtMonth.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(487, 3);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(90, 42);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "&Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(583, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(90, 42);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "&To Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(170, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 42);
            this.btnRefresh.TabIndex = 0;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // frmInterestAdvise
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmInterestAdvise";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interest Advise List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvInterests)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private MyGUI.Preloader.LoadingCircle lcProcess;
        private System.Windows.Forms.Label lblProcess;
        public BrightIdeasSoftware.ObjectListView lvInterests;
        private BrightIdeasSoftware.OLVColumn olvColumnClientName;
        private BrightIdeasSoftware.OLVColumn olvColumnOpeningBalance;
        private BrightIdeasSoftware.OLVColumn olvColumnClosingBalance;
        private BrightIdeasSoftware.OLVColumn olvColumnInterestAmount;
        private System.Windows.Forms.DateTimePicker dtMonth;
        private System.Windows.Forms.ProgressBar pbProcess;
        private System.Windows.Forms.TextBox txtTotalInterestAmount;

    }
}

