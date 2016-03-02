namespace NNPPoly.forms
{
    partial class frmModifySchemes
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
            this.lvSchemes = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnClient = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnSchemes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvSchemes)).BeginInit();
            this.SuspendLayout();
            // 
            // lvSchemes
            // 
            this.lvSchemes.AllColumns.Add(this.olvColumnClient);
            this.lvSchemes.AllColumns.Add(this.olvColumnSchemes);
            this.lvSchemes.AllowColumnReorder = true;
            this.lvSchemes.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvSchemes.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSchemes.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvSchemes.CellEditTabChangesRows = true;
            this.lvSchemes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnClient,
            this.olvColumnSchemes});
            this.lvSchemes.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvSchemes.EmptyListMsg = "--: No credit/debit entries :--";
            this.lvSchemes.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSchemes.FullRowSelect = true;
            this.lvSchemes.GridLines = true;
            this.lvSchemes.HeaderWordWrap = true;
            this.lvSchemes.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvSchemes.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvSchemes.Location = new System.Drawing.Point(15, 15);
            this.lvSchemes.Name = "lvSchemes";
            this.lvSchemes.OverlayText.Text = "";
            this.lvSchemes.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvSchemes.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvSchemes.ShowCommandMenuOnRightClick = true;
            this.lvSchemes.ShowGroups = false;
            this.lvSchemes.ShowItemToolTips = true;
            this.lvSchemes.Size = new System.Drawing.Size(510, 330);
            this.lvSchemes.TabIndex = 6;
            this.lvSchemes.TintSortColumn = true;
            this.lvSchemes.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvSchemes.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvSchemes.UseCompatibleStateImageBehavior = false;
            this.lvSchemes.UseCustomSelectionColors = true;
            this.lvSchemes.UseExplorerTheme = true;
            this.lvSchemes.UseFilterIndicator = true;
            this.lvSchemes.UseFiltering = true;
            this.lvSchemes.UseHotItem = true;
            this.lvSchemes.UseHyperlinks = true;
            this.lvSchemes.View = System.Windows.Forms.View.Details;
            this.lvSchemes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvSchemes_KeyDown);
            this.lvSchemes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvSchemes_MouseDoubleClick);
            // 
            // olvColumnClient
            // 
            this.olvColumnClient.AspectName = "client_name";
            this.olvColumnClient.FillsFreeSpace = true;
            this.olvColumnClient.IsEditable = false;
            this.olvColumnClient.Text = "Client";
            // 
            // olvColumnSchemes
            // 
            this.olvColumnSchemes.AspectName = "scheme";
            this.olvColumnSchemes.FillsFreeSpace = true;
            this.olvColumnSchemes.IsEditable = false;
            this.olvColumnSchemes.Text = "Schemes";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(435, 300);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmModifySchemes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(541, 361);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lvSchemes);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmModifySchemes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modify Schemes";
            this.Load += new System.EventHandler(this.frmModifySchemes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvSchemes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvSchemes;
        private BrightIdeasSoftware.OLVColumn olvColumnClient;
        private BrightIdeasSoftware.OLVColumn olvColumnSchemes;
        private System.Windows.Forms.Button btnDelete;
    }
}

