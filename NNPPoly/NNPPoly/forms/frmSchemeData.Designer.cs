namespace NNPPoly.forms
{
    partial class frmSchemeData
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
            this.lvData = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnImportData = new System.Windows.Forms.Button();
            this.btnNewEntry = new System.Windows.Forms.Button();
            this.btnDeleteEntries = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.btnLoad = new System.Windows.Forms.Button();
            this.chkDateFilter = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.lvData)).BeginInit();
            this.SuspendLayout();
            // 
            // lvData
            // 
            this.lvData.AllColumns.Add(this.olvColumn1);
            this.lvData.AllColumns.Add(this.olvColumn2);
            this.lvData.AllColumns.Add(this.olvColumn3);
            this.lvData.AllColumns.Add(this.olvColumn4);
            this.lvData.AllowColumnReorder = true;
            this.lvData.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvData.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvData.CellEditTabChangesRows = true;
            this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.lvData.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvData.EmptyListMsg = "Please import data by clicking \"Import Data\" button";
            this.lvData.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvData.FullRowSelect = true;
            this.lvData.GridLines = true;
            this.lvData.HeaderWordWrap = true;
            this.lvData.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvData.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvData.Location = new System.Drawing.Point(10, 80);
            this.lvData.Name = "lvData";
            this.lvData.OverlayText.Text = "";
            this.lvData.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvData.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvData.ShowCommandMenuOnRightClick = true;
            this.lvData.ShowGroups = false;
            this.lvData.ShowItemToolTips = true;
            this.lvData.Size = new System.Drawing.Size(710, 410);
            this.lvData.TabIndex = 4;
            this.lvData.TintSortColumn = true;
            this.lvData.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvData.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.UseCustomSelectionColors = true;
            this.lvData.UseExplorerTheme = true;
            this.lvData.UseFilterIndicator = true;
            this.lvData.UseFiltering = true;
            this.lvData.UseHotItem = true;
            this.lvData.UseHyperlinks = true;
            this.lvData.View = System.Windows.Forms.View.Details;
            this.lvData.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvData_CellEditFinishing);
            this.lvData.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvData_CellEditStarting);
            this.lvData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvData_KeyDown);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "client_name";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Text = "Party Name";
            this.olvColumn1.Width = 123;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "date";
            this.olvColumn2.Text = "Date";
            this.olvColumn2.Width = 114;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "grade_name";
            this.olvColumn3.Text = "Grade";
            this.olvColumn3.Width = 129;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "qty";
            this.olvColumn4.Text = "Qty";
            this.olvColumn4.Width = 125;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(630, 500);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImportData
            // 
            this.btnImportData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportData.Location = new System.Drawing.Point(10, 500);
            this.btnImportData.Name = "btnImportData";
            this.btnImportData.Size = new System.Drawing.Size(120, 30);
            this.btnImportData.TabIndex = 5;
            this.btnImportData.Text = "&Import Data";
            this.btnImportData.UseVisualStyleBackColor = true;
            this.btnImportData.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // btnNewEntry
            // 
            this.btnNewEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNewEntry.Location = new System.Drawing.Point(140, 500);
            this.btnNewEntry.Name = "btnNewEntry";
            this.btnNewEntry.Size = new System.Drawing.Size(120, 30);
            this.btnNewEntry.TabIndex = 6;
            this.btnNewEntry.Text = "&New Entry";
            this.btnNewEntry.UseVisualStyleBackColor = true;
            this.btnNewEntry.Click += new System.EventHandler(this.btnNewEntry_Click);
            // 
            // btnDeleteEntries
            // 
            this.btnDeleteEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteEntries.Location = new System.Drawing.Point(270, 500);
            this.btnDeleteEntries.Name = "btnDeleteEntries";
            this.btnDeleteEntries.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteEntries.TabIndex = 7;
            this.btnDeleteEntries.Text = "&Delete Entries";
            this.btnDeleteEntries.UseVisualStyleBackColor = true;
            this.btnDeleteEntries.Click += new System.EventHandler(this.btnDeleteEntries_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Party :";
            // 
            // cmbClients
            // 
            this.cmbClients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbClients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(10, 40);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(140, 26);
            this.cmbClients.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 8;
            this.label2.Text = "Date :";
            // 
            // dtFrom
            // 
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(160, 40);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(138, 26);
            this.dtFrom.TabIndex = 1;
            this.dtFrom.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // dtTo
            // 
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(300, 40);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(138, 26);
            this.dtTo.TabIndex = 2;
            this.dtTo.ValueChanged += new System.EventHandler(this.dtTo_ValueChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(630, 40);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(90, 26);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "&LOAD";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // chkDateFilter
            // 
            this.chkDateFilter.AutoSize = true;
            this.chkDateFilter.Location = new System.Drawing.Point(220, 13);
            this.chkDateFilter.Name = "chkDateFilter";
            this.chkDateFilter.Size = new System.Drawing.Size(15, 14);
            this.chkDateFilter.TabIndex = 9;
            this.chkDateFilter.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(450, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 18);
            this.label3.TabIndex = 10;
            this.label3.Text = "Group :";
            // 
            // cmbGroups
            // 
            this.cmbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroups.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbGroups.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(450, 40);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(170, 26);
            this.cmbGroups.TabIndex = 3;
            // 
            // frmSchemeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(731, 541);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkDateFilter);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.dtTo);
            this.Controls.Add(this.dtFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbClients);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDeleteEntries);
            this.Controls.Add(this.btnNewEntry);
            this.Controls.Add(this.btnImportData);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvData);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmSchemeData";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scheme Data";
            this.Load += new System.EventHandler(this.frmSchemeData_Load);
            this.Shown += new System.EventHandler(this.frmSchemeData_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvData;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnImportData;
        private System.Windows.Forms.Button btnNewEntry;
        private System.Windows.Forms.Button btnDeleteEntries;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnLoad;
        public System.Windows.Forms.ComboBox cmbClients;
        public System.Windows.Forms.DateTimePicker dtFrom;
        public System.Windows.Forms.DateTimePicker dtTo;
        public System.Windows.Forms.CheckBox chkDateFilter;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox cmbGroups;
    }
}

