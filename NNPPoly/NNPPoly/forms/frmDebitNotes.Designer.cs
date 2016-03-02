namespace NNPPoly.forms
{
    partial class frmDebitNotes
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
            this.btnShow = new System.Windows.Forms.Button();
            this.cmbPaymentType = new System.Windows.Forms.ComboBox();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.chkAllDates = new System.Windows.Forms.CheckBox();
            this.dtDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lvPayments = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnClientName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDebitNo = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnEntries = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnShow);
            this.panel1.Controls.Add(this.cmbPaymentType);
            this.panel1.Controls.Add(this.cmbClients);
            this.panel1.Controls.Add(this.chkAllDates);
            this.panel1.Controls.Add(this.dtDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(676, 41);
            this.panel1.TabIndex = 2;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.Location = new System.Drawing.Point(598, 3);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 35);
            this.btnShow.TabIndex = 5;
            this.btnShow.Text = "&Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // cmbPaymentType
            // 
            this.cmbPaymentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbPaymentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPaymentType.FormattingEnabled = true;
            this.cmbPaymentType.Items.AddRange(new object[] {
            "Debit Notes",
            "Debit Advises"});
            this.cmbPaymentType.Location = new System.Drawing.Point(479, 8);
            this.cmbPaymentType.Name = "cmbPaymentType";
            this.cmbPaymentType.Size = new System.Drawing.Size(113, 26);
            this.cmbPaymentType.TabIndex = 4;
            // 
            // cmbClients
            // 
            this.cmbClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbClients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbClients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(293, 8);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(180, 26);
            this.cmbClients.TabIndex = 3;
            // 
            // chkAllDates
            // 
            this.chkAllDates.AutoSize = true;
            this.chkAllDates.Location = new System.Drawing.Point(197, 10);
            this.chkAllDates.Name = "chkAllDates";
            this.chkAllDates.Size = new System.Drawing.Size(90, 22);
            this.chkAllDates.TabIndex = 2;
            this.chkAllDates.Text = "All Dates";
            this.chkAllDates.UseVisualStyleBackColor = true;
            // 
            // dtDate
            // 
            this.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtDate.Location = new System.Drawing.Point(59, 7);
            this.dtDate.Name = "dtDate";
            this.dtDate.Size = new System.Drawing.Size(132, 26);
            this.dtDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date :";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.lvPayments);
            this.panel2.Location = new System.Drawing.Point(12, 59);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(676, 429);
            this.panel2.TabIndex = 3;
            // 
            // lvPayments
            // 
            this.lvPayments.AllColumns.Add(this.olvColumnClientName);
            this.lvPayments.AllColumns.Add(this.olvColumnDebitNo);
            this.lvPayments.AllColumns.Add(this.olvColumnDate);
            this.lvPayments.AllColumns.Add(this.olvColumnEntries);
            this.lvPayments.AllColumns.Add(this.olvColumnAmount);
            this.lvPayments.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvPayments.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvPayments.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvPayments.CellEditTabChangesRows = true;
            this.lvPayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnClientName,
            this.olvColumnDebitNo,
            this.olvColumnDate,
            this.olvColumnEntries,
            this.olvColumnAmount});
            this.lvPayments.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPayments.EmptyListMsg = "--: No Records :--";
            this.lvPayments.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPayments.FullRowSelect = true;
            this.lvPayments.GridLines = true;
            this.lvPayments.HeaderWordWrap = true;
            this.lvPayments.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvPayments.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvPayments.Location = new System.Drawing.Point(0, 0);
            this.lvPayments.Name = "lvPayments";
            this.lvPayments.OverlayText.BackColor = System.Drawing.Color.Black;
            this.lvPayments.OverlayText.BorderColor = System.Drawing.Color.Blue;
            this.lvPayments.OverlayText.Font = new System.Drawing.Font("Arial Narrow", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvPayments.OverlayText.Text = "Clients";
            this.lvPayments.OverlayText.TextColor = System.Drawing.Color.AliceBlue;
            this.lvPayments.OverlayText.Transparency = 255;
            this.lvPayments.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvPayments.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvPayments.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvPayments.ShowCommandMenuOnRightClick = true;
            this.lvPayments.ShowGroups = false;
            this.lvPayments.ShowItemToolTips = true;
            this.lvPayments.Size = new System.Drawing.Size(676, 429);
            this.lvPayments.TabIndex = 2;
            this.lvPayments.TintSortColumn = true;
            this.lvPayments.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvPayments.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvPayments.UseAlternatingBackColors = true;
            this.lvPayments.UseCompatibleStateImageBehavior = false;
            this.lvPayments.UseCustomSelectionColors = true;
            this.lvPayments.UseExplorerTheme = true;
            this.lvPayments.UseFilterIndicator = true;
            this.lvPayments.UseFiltering = true;
            this.lvPayments.UseHotItem = true;
            this.lvPayments.UseHyperlinks = true;
            this.lvPayments.UseOverlays = false;
            this.lvPayments.View = System.Windows.Forms.View.Details;
            this.lvPayments.SelectedIndexChanged += new System.EventHandler(this.lvPayments_SelectedIndexChanged);
            this.lvPayments.DoubleClick += new System.EventHandler(this.lvPayments_DoubleClick);
            this.lvPayments.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvPayments_MouseClick);
            this.lvPayments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvPayments_MouseDoubleClick);
            this.lvPayments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvPayments_MouseDown);
            // 
            // olvColumnClientName
            // 
            this.olvColumnClientName.AspectName = "client_name";
            this.olvColumnClientName.FillsFreeSpace = true;
            this.olvColumnClientName.IsEditable = false;
            this.olvColumnClientName.Text = "Client Name";
            // 
            // olvColumnDebitNo
            // 
            this.olvColumnDebitNo.AspectName = "id";
            this.olvColumnDebitNo.Text = "Debit No";
            // 
            // olvColumnDate
            // 
            this.olvColumnDate.AspectName = "date";
            this.olvColumnDate.FillsFreeSpace = true;
            this.olvColumnDate.IsEditable = false;
            this.olvColumnDate.Text = "Date";
            this.olvColumnDate.Width = 62;
            // 
            // olvColumnEntries
            // 
            this.olvColumnEntries.AspectName = "entries.Count";
            this.olvColumnEntries.FillsFreeSpace = true;
            this.olvColumnEntries.IsEditable = false;
            this.olvColumnEntries.Text = "Entries";
            // 
            // olvColumnAmount
            // 
            this.olvColumnAmount.AspectName = "total_amount";
            this.olvColumnAmount.FillsFreeSpace = true;
            this.olvColumnAmount.IsEditable = false;
            this.olvColumnAmount.Text = "Total Amount";
            // 
            // frmDebitNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "frmDebitNotes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debit Notes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ComboBox cmbPaymentType;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.CheckBox chkAllDates;
        private System.Windows.Forms.DateTimePicker dtDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        public BrightIdeasSoftware.ObjectListView lvPayments;
        private BrightIdeasSoftware.OLVColumn olvColumnClientName;
        private BrightIdeasSoftware.OLVColumn olvColumnDebitNo;
        private BrightIdeasSoftware.OLVColumn olvColumnDate;
        private BrightIdeasSoftware.OLVColumn olvColumnEntries;
        private BrightIdeasSoftware.OLVColumn olvColumnAmount;
    }
}

