namespace NNPPoly.forms
{
    partial class frmClientSelection
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
            this.lvClients = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).BeginInit();
            this.SuspendLayout();
            // 
            // lvClients
            // 
            this.lvClients.AllColumns.Add(this.olvColumn1);
            this.lvClients.AllColumns.Add(this.olvColumn2);
            this.lvClients.AllColumns.Add(this.olvColumn3);
            this.lvClients.AllColumns.Add(this.olvColumn4);
            this.lvClients.AllowColumnReorder = true;
            this.lvClients.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvClients.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvClients.CellEditTabChangesRows = true;
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.lvClients.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvClients.EmptyListMsg = "--: No credit/debit entries :--";
            this.lvClients.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderWordWrap = true;
            this.lvClients.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvClients.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.Location = new System.Drawing.Point(0, 0);
            this.lvClients.MultiSelect = false;
            this.lvClients.Name = "lvClients";
            this.lvClients.OverlayText.Text = "";
            this.lvClients.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvClients.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvClients.ShowCommandMenuOnRightClick = true;
            this.lvClients.ShowGroups = false;
            this.lvClients.ShowItemToolTips = true;
            this.lvClients.Size = new System.Drawing.Size(672, 615);
            this.lvClients.TabIndex = 3;
            this.lvClients.TintSortColumn = true;
            this.lvClients.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvClients.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.UseCustomSelectionColors = true;
            this.lvClients.UseExplorerTheme = true;
            this.lvClients.UseFilterIndicator = true;
            this.lvClients.UseFiltering = true;
            this.lvClients.UseHotItem = true;
            this.lvClients.UseHyperlinks = true;
            this.lvClients.View = System.Windows.Forms.View.Details;
            this.lvClients.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvClients_KeyDown);
            this.lvClients.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvClients_MouseDoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "name";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Text = "Name";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "about";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Address";
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "mobiles";
            this.olvColumn3.Text = "Mobile";
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "emails";
            this.olvColumn4.Text = "Emails";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(579, 571);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 32);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmClientSelection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(672, 615);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvClients);
            this.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmClientSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Client";
            this.Load += new System.EventHandler(this.frmClientSelectioncs_Load);
            this.Shown += new System.EventHandler(this.frmClientSelection_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvClients;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Button btnClose;
    }
}

