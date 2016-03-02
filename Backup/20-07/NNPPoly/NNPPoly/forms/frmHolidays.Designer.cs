namespace NNPPoly.forms
{
    partial class frmHolidays
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
            this.lvPayments = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnAddSundays = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // lvPayments
            // 
            this.lvPayments.AllColumns.Add(this.olvColumnDate);
            this.lvPayments.AllColumns.Add(this.olvColumn1);
            this.lvPayments.AllowColumnReorder = true;
            this.lvPayments.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvPayments.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvPayments.CellEditTabChangesRows = true;
            this.lvPayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnDate,
            this.olvColumn1});
            this.lvPayments.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvPayments.EmptyListMsg = "--: No Holidays :--";
            this.lvPayments.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPayments.FullRowSelect = true;
            this.lvPayments.GridLines = true;
            this.lvPayments.HeaderWordWrap = true;
            this.lvPayments.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvPayments.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvPayments.Location = new System.Drawing.Point(12, 61);
            this.lvPayments.Name = "lvPayments";
            this.lvPayments.OverlayText.Text = "";
            this.lvPayments.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvPayments.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvPayments.ShowCommandMenuOnRightClick = true;
            this.lvPayments.ShowGroups = false;
            this.lvPayments.ShowItemToolTips = true;
            this.lvPayments.Size = new System.Drawing.Size(481, 432);
            this.lvPayments.TabIndex = 3;
            this.lvPayments.TintSortColumn = true;
            this.lvPayments.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvPayments.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvPayments.UseCompatibleStateImageBehavior = false;
            this.lvPayments.UseCustomSelectionColors = true;
            this.lvPayments.UseExplorerTheme = true;
            this.lvPayments.UseFilterIndicator = true;
            this.lvPayments.UseFiltering = true;
            this.lvPayments.UseHotItem = true;
            this.lvPayments.UseHyperlinks = true;
            this.lvPayments.View = System.Windows.Forms.View.Details;
            this.lvPayments.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvPayments_CellEditStarting);
            this.lvPayments.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvPayments_KeyDown);
            this.lvPayments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvPayments_MouseDown);
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "date";
            this.olvColumnDate.FillsFreeSpace = true;
            this.olvColumnDate.Text = "Date";
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "description";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Text = "Description";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(398, 499);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 33);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 5;
            this.label1.Text = "Date :";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(12, 30);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(134, 26);
            this.dateTimePicker1.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(152, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Description :";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(155, 30);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(257, 26);
            this.textBox1.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(418, 29);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 26);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "&Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnAddSundays
            // 
            this.btnAddSundays.Location = new System.Drawing.Point(12, 499);
            this.btnAddSundays.Name = "btnAddSundays";
            this.btnAddSundays.Size = new System.Drawing.Size(139, 33);
            this.btnAddSundays.TabIndex = 4;
            this.btnAddSundays.Text = "&Add Sundays";
            this.btnAddSundays.UseVisualStyleBackColor = true;
            this.btnAddSundays.Visible = false;
            this.btnAddSundays.Click += new System.EventHandler(this.btnAddSundays_Click);
            // 
            // frmHolidays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(505, 544);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAddSundays);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lvPayments);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmHolidays";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Holidays";
            this.Load += new System.EventHandler(this.frmHolidays_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lvPayments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvPayments;
        private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.OLVColumn olvColumnDate;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnAddSundays;
    }
}

