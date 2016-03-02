namespace NNPPoly.forms
{
    partial class frmDebitNoteEntries
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
            this.lvEntries = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnInvoiceNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnGrade = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvEntries)).BeginInit();
            this.SuspendLayout();
            // 
            // lvEntries
            // 
            this.lvEntries.AllColumns.Add(this.olvColumnInvoiceNo);
            this.lvEntries.AllColumns.Add(this.olvColumnDate);
            this.lvEntries.AllColumns.Add(this.olvColumnGrade);
            this.lvEntries.AllColumns.Add(this.olvColumnQty);
            this.lvEntries.AllColumns.Add(this.olvColumnAmount);
            this.lvEntries.AllowColumnReorder = true;
            this.lvEntries.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvEntries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvEntries.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvEntries.CellEditTabChangesRows = true;
            this.lvEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnInvoiceNo,
            this.olvColumnDate,
            this.olvColumnGrade,
            this.olvColumnQty,
            this.olvColumnAmount});
            this.lvEntries.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvEntries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvEntries.EmptyListMsg = "--: No Entries :--";
            this.lvEntries.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvEntries.FullRowSelect = true;
            this.lvEntries.GridLines = true;
            this.lvEntries.HeaderWordWrap = true;
            this.lvEntries.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvEntries.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvEntries.Location = new System.Drawing.Point(0, 0);
            this.lvEntries.Name = "lvEntries";
            this.lvEntries.OverlayText.Text = "";
            this.lvEntries.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvEntries.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvEntries.ShowCommandMenuOnRightClick = true;
            this.lvEntries.ShowGroups = false;
            this.lvEntries.ShowItemToolTips = true;
            this.lvEntries.Size = new System.Drawing.Size(685, 423);
            this.lvEntries.TabIndex = 3;
            this.lvEntries.TintSortColumn = true;
            this.lvEntries.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvEntries.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvEntries.UseCompatibleStateImageBehavior = false;
            this.lvEntries.UseCustomSelectionColors = true;
            this.lvEntries.UseExplorerTheme = true;
            this.lvEntries.UseFilterIndicator = true;
            this.lvEntries.UseFiltering = true;
            this.lvEntries.UseHotItem = true;
            this.lvEntries.UseHyperlinks = true;
            this.lvEntries.View = System.Windows.Forms.View.Details;
            this.lvEntries.DoubleClick += new System.EventHandler(this.lvEntries_DoubleClick);
            this.lvEntries.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvEntries_MouseDown);
            // 
            // olvColumnInvoiceNo
            // 
            this.olvColumnInvoiceNo.AspectName = "invoice";
            this.olvColumnInvoiceNo.FillsFreeSpace = true;
            this.olvColumnInvoiceNo.IsEditable = false;
            this.olvColumnInvoiceNo.Text = "Invoice";
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "date";
            this.olvColumnDate.FillsFreeSpace = true;
            this.olvColumnDate.IsEditable = false;
            this.olvColumnDate.Text = "Date";
            // 
            // olvColumnGrade
            // 
            this.olvColumnGrade.AspectName = "grade.code";
            this.olvColumnGrade.FillsFreeSpace = true;
            this.olvColumnGrade.IsEditable = false;
            this.olvColumnGrade.Text = "Grade";
            // 
            // olvColumnQty
            // 
            this.olvColumnQty.AspectName = "mt";
            this.olvColumnQty.FillsFreeSpace = true;
            this.olvColumnQty.IsEditable = false;
            this.olvColumnQty.Text = "Qty";
            // 
            // olvColumnAmount
            // 
            this.olvColumnAmount.AspectName = "";
            this.olvColumnAmount.FillsFreeSpace = true;
            this.olvColumnAmount.IsEditable = false;
            this.olvColumnAmount.Text = "Amount";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(579, 380);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(94, 31);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmDebitNoteEntries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(685, 423);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvEntries);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDebitNoteEntries";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entries";
            this.Load += new System.EventHandler(this.frmDebitNoteEntries_Load);
            this.Shown += new System.EventHandler(this.frmDebitNoteEntries_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvEntries)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvEntries;
        private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.OLVColumn olvColumnInvoiceNo;
        private BrightIdeasSoftware.OLVColumn olvColumnDate;
        private BrightIdeasSoftware.OLVColumn olvColumnGrade;
        private BrightIdeasSoftware.OLVColumn olvColumnQty;
        private BrightIdeasSoftware.OLVColumn olvColumnAmount;
    }
}

