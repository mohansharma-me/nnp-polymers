namespace NNPPoly.forms
{
    partial class frmNewScheme
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
            this.cmbClient = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtYear = new System.Windows.Forms.DateTimePicker();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lvGroups = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnGroup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnQty = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMonth = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.label3 = new System.Windows.Forms.Label();
            this.cmbGroups = new System.Windows.Forms.ComboBox();
            this.cmbMonths = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMonthQty = new System.Windows.Forms.TextBox();
            this.btnAddParam = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client :";
            // 
            // cmbClient
            // 
            this.cmbClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClient.FormattingEnabled = true;
            this.cmbClient.Location = new System.Drawing.Point(15, 45);
            this.cmbClient.Name = "cmbClient";
            this.cmbClient.Size = new System.Drawing.Size(240, 26);
            this.cmbClient.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Year :";
            // 
            // dtYear
            // 
            this.dtYear.CustomFormat = "yyyy";
            this.dtYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtYear.Location = new System.Drawing.Point(270, 45);
            this.dtYear.Name = "dtYear";
            this.dtYear.Size = new System.Drawing.Size(240, 26);
            this.dtYear.TabIndex = 1;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(15, 360);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(90, 30);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(420, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lvGroups
            // 
            this.lvGroups.AllColumns.Add(this.olvColumnGroup);
            this.lvGroups.AllColumns.Add(this.olvColumnQty);
            this.lvGroups.AllColumns.Add(this.olvColumnMonth);
            this.lvGroups.AllowColumnReorder = true;
            this.lvGroups.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvGroups.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvGroups.CellEditTabChangesRows = true;
            this.lvGroups.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnGroup,
            this.olvColumnQty,
            this.olvColumnMonth});
            this.lvGroups.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvGroups.EmptyListMsg = "---";
            this.lvGroups.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvGroups.FullRowSelect = true;
            this.lvGroups.GridLines = true;
            this.lvGroups.HeaderWordWrap = true;
            this.lvGroups.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvGroups.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvGroups.Location = new System.Drawing.Point(15, 90);
            this.lvGroups.Name = "lvGroups";
            this.lvGroups.OverlayText.Text = "";
            this.lvGroups.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvGroups.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvGroups.ShowCommandMenuOnRightClick = true;
            this.lvGroups.ShowGroups = false;
            this.lvGroups.ShowItemToolTips = true;
            this.lvGroups.Size = new System.Drawing.Size(315, 255);
            this.lvGroups.TabIndex = 6;
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
            this.lvGroups.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvGroups_CellEditFinishing);
            this.lvGroups.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvGroups_CellEditStarting);
            this.lvGroups.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvGroups_KeyDown);
            // 
            // olvColumnGroup
            // 
            this.olvColumnGroup.AspectName = "name";
            this.olvColumnGroup.IsEditable = false;
            this.olvColumnGroup.Text = "Group";
            this.olvColumnGroup.Width = 96;
            // 
            // olvColumnQty
            // 
            this.olvColumnQty.AspectName = "qty";
            this.olvColumnQty.Text = "Qty";
            this.olvColumnQty.Width = 89;
            // 
            // olvColumnMonth
            // 
            this.olvColumnMonth.AspectName = "month_no";
            this.olvColumnMonth.Text = "Month";
            this.olvColumnMonth.Width = 122;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(345, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Group:";
            // 
            // cmbGroups
            // 
            this.cmbGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroups.FormattingEnabled = true;
            this.cmbGroups.Location = new System.Drawing.Point(345, 120);
            this.cmbGroups.Name = "cmbGroups";
            this.cmbGroups.Size = new System.Drawing.Size(165, 26);
            this.cmbGroups.TabIndex = 2;
            // 
            // cmbMonths
            // 
            this.cmbMonths.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMonths.FormattingEnabled = true;
            this.cmbMonths.Location = new System.Drawing.Point(345, 195);
            this.cmbMonths.Name = "cmbMonths";
            this.cmbMonths.Size = new System.Drawing.Size(165, 26);
            this.cmbMonths.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Month :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(345, 240);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Monthly Qty :";
            // 
            // txtMonthQty
            // 
            this.txtMonthQty.Location = new System.Drawing.Point(345, 270);
            this.txtMonthQty.Name = "txtMonthQty";
            this.txtMonthQty.Size = new System.Drawing.Size(165, 26);
            this.txtMonthQty.TabIndex = 4;
            this.txtMonthQty.TextChanged += new System.EventHandler(this.txtMonthQty_TextChanged);
            // 
            // btnAddParam
            // 
            this.btnAddParam.Location = new System.Drawing.Point(345, 308);
            this.btnAddParam.Name = "btnAddParam";
            this.btnAddParam.Size = new System.Drawing.Size(165, 38);
            this.btnAddParam.TabIndex = 5;
            this.btnAddParam.Text = "< Add";
            this.btnAddParam.UseVisualStyleBackColor = true;
            this.btnAddParam.Click += new System.EventHandler(this.btnAddParam_Click);
            // 
            // frmNewScheme
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(526, 406);
            this.Controls.Add(this.btnAddParam);
            this.Controls.Add(this.txtMonthQty);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lvGroups);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dtYear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMonths);
            this.Controls.Add(this.cmbGroups);
            this.Controls.Add(this.cmbClient);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewScheme";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Scheme";
            this.Load += new System.EventHandler(this.frmNewScheme_Load);
            this.Shown += new System.EventHandler(this.frmNewScheme_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClient;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtYear;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        public BrightIdeasSoftware.ObjectListView lvGroups;
        private BrightIdeasSoftware.OLVColumn olvColumnGroup;
        private BrightIdeasSoftware.OLVColumn olvColumnQty;
        private BrightIdeasSoftware.OLVColumn olvColumnMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbGroups;
        private System.Windows.Forms.ComboBox cmbMonths;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMonthQty;
        private System.Windows.Forms.Button btnAddParam;
    }
}

