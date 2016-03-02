namespace NNPPoly.forms
{
    partial class frmGradeGroups
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
            this.lvGroups = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMonthlyMin = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQuaterlyMin = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnYearlyMin = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnNewGroup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // lvGroups
            // 
            this.lvGroups.AllColumns.Add(this.olvColumnName);
            this.lvGroups.AllColumns.Add(this.olvColumnQty);
            this.lvGroups.AllColumns.Add(this.olvColumnMonthlyMin);
            this.lvGroups.AllColumns.Add(this.olvColumnQuaterlyMin);
            this.lvGroups.AllColumns.Add(this.olvColumnYearlyMin);
            this.lvGroups.AllowColumnReorder = true;
            this.lvGroups.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvGroups.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvGroups.CellEditTabChangesRows = true;
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnName,
            this.olvColumnQty,
            this.olvColumnMonthlyMin,
            this.olvColumnQuaterlyMin,
            this.olvColumnYearlyMin});
            this.lvGroups.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvGroups.EmptyListMsg = "--: No credit/debit entries :--";
            this.lvGroups.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.GridLines = true;
            this.lvGroups.HeaderWordWrap = true;
            this.lvGroups.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvGroups.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvGroups.Location = new System.Drawing.Point(15, 15);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.OverlayText.Text = "";
            this.lvGroups.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvGroups.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvGroups.ShowCommandMenuOnRightClick = true;
            this.lvGroups.ShowGroups = false;
            this.lvGroups.ShowItemToolTips = true;
            this.lvGroups.Size = new System.Drawing.Size(630, 360);
            this.lvGroups.TabIndex = 3;
            this.lvGroups.TintSortColumn = true;
            this.lvGroups.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvGroups.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvGroups.UseCompatibleStateImageBehavior = false;
            this.lvGroups.UseCustomSelectionColors = true;
            this.lvGroups.UseExplorerTheme = true;
            this.lvGroups.UseFilterIndicator = true;
            this.lvGroups.UseFiltering = true;
            this.lvGroups.UseHotItem = true;
            this.lvGroups.UseHyperlinks = true;
            this.lvGroups.View = System.Windows.Forms.View.Details;
            this.lvGroups.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvGroups_CellEditStarting);
            // 
            // olvColumnName
            // 
            this.olvColumnName.AspectName = "name";
            this.olvColumnName.FillsFreeSpace = true;
            this.olvColumnName.Text = "Group";
            // 
            // olvColumnQty
            // 
            this.olvColumnQty.AspectName = "qty";
            this.olvColumnQty.FillsFreeSpace = true;
            this.olvColumnQty.Text = "APP/MoU Qty.";
            // 
            // olvColumnMonthlyMin
            // 
            this.olvColumnMonthlyMin.AspectName = "monthly_percentage";
            this.olvColumnMonthlyMin.FillsFreeSpace = true;
            this.olvColumnMonthlyMin.Text = "Monthly Min %";
            // 
            // olvColumnQuaterlyMin
            // 
            this.olvColumnQuaterlyMin.AspectName = "quaterly_percentage";
            this.olvColumnQuaterlyMin.FillsFreeSpace = true;
            this.olvColumnQuaterlyMin.Text = "Quater Min %";
            // 
            // olvColumnYearlyMin
            // 
            this.olvColumnYearlyMin.AspectName = "yearly_percentage";
            this.olvColumnYearlyMin.FillsFreeSpace = true;
            this.olvColumnYearlyMin.Text = "Yearly Min %";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(555, 390);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnNewGroup
            // 
            this.btnNewGroup.Location = new System.Drawing.Point(15, 390);
            this.btnNewGroup.Name = "btnNewGroup";
            this.btnNewGroup.Size = new System.Drawing.Size(120, 30);
            this.btnNewGroup.TabIndex = 4;
            this.btnNewGroup.Text = "&New Group";
            this.btnNewGroup.UseVisualStyleBackColor = true;
            this.btnNewGroup.Click += new System.EventHandler(this.btnNewGroup_Click);
            // 
            // frmGradeGroups
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(659, 436);
            this.Controls.Add(this.btnNewGroup);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvGroups);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGradeGroups";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grade Groups";
            this.Load += new System.EventHandler(this.frmGradeGroups_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvGroups)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvGroups;
        private BrightIdeasSoftware.OLVColumn olvColumnName;
        private BrightIdeasSoftware.OLVColumn olvColumnQty;
        private BrightIdeasSoftware.OLVColumn olvColumnMonthlyMin;
        private BrightIdeasSoftware.OLVColumn olvColumnQuaterlyMin;
        private BrightIdeasSoftware.OLVColumn olvColumnYearlyMin;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnNewGroup;
    }
}

