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
            this.lvData.Location = new System.Drawing.Point(15, 15);
            this.lvData.Name = "lvData";
            this.lvData.OverlayText.Text = "";
            this.lvData.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvData.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvData.ShowCommandMenuOnRightClick = true;
            this.lvData.ShowGroups = false;
            this.lvData.ShowItemToolTips = true;
            this.lvData.Size = new System.Drawing.Size(705, 465);
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
            this.btnClose.Location = new System.Drawing.Point(630, 495);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnImportData
            // 
            this.btnImportData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnImportData.Location = new System.Drawing.Point(15, 495);
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
            this.btnNewEntry.Location = new System.Drawing.Point(150, 495);
            this.btnNewEntry.Name = "btnNewEntry";
            this.btnNewEntry.Size = new System.Drawing.Size(120, 30);
            this.btnNewEntry.TabIndex = 5;
            this.btnNewEntry.Text = "&New Entry";
            this.btnNewEntry.UseVisualStyleBackColor = true;
            this.btnNewEntry.Click += new System.EventHandler(this.btnNewEntry_Click);
            // 
            // btnDeleteEntries
            // 
            this.btnDeleteEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteEntries.Location = new System.Drawing.Point(285, 495);
            this.btnDeleteEntries.Name = "btnDeleteEntries";
            this.btnDeleteEntries.Size = new System.Drawing.Size(120, 30);
            this.btnDeleteEntries.TabIndex = 5;
            this.btnDeleteEntries.Text = "&Delete Entries";
            this.btnDeleteEntries.UseVisualStyleBackColor = true;
            this.btnDeleteEntries.Click += new System.EventHandler(this.btnDeleteEntries_Click);
            // 
            // frmSchemeData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(736, 541);
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
            this.Shown += new System.EventHandler(this.frmSchemeData_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvData)).EndInit();
            this.ResumeLayout(false);

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
    }
}

