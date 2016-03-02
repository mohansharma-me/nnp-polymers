namespace NNPPoly
{
    partial class Records
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Records));
            this.label1 = new System.Windows.Forms.Label();
            this.dtRecordDate = new System.Windows.Forms.DateTimePicker();
            this.btnShow = new System.Windows.Forms.Button();
            this.lv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.cmbEntries = new System.Windows.Forms.ComboBox();
            this.chkALL = new System.Windows.Forms.CheckBox();
            this.chkShowDeleted = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select date :";
            // 
            // dtRecordDate
            // 
            this.dtRecordDate.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtRecordDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtRecordDate.Location = new System.Drawing.Point(117, 14);
            this.dtRecordDate.Name = "dtRecordDate";
            this.dtRecordDate.Size = new System.Drawing.Size(99, 25);
            this.dtRecordDate.TabIndex = 1;
            // 
            // btnShow
            // 
            this.btnShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShow.Location = new System.Drawing.Point(718, 10);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(75, 31);
            this.btnShow.TabIndex = 4;
            this.btnShow.Text = "&Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // lv
            // 
            this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader5});
            this.lv.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(12, 49);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(781, 341);
            this.lv.TabIndex = 5;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            this.lv.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Client name";
            this.columnHeader1.Width = 204;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Doc/Chq/Invoice No";
            this.columnHeader3.Width = 155;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date";
            this.columnHeader4.Width = 83;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "MT";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Grade";
            this.columnHeader7.Width = 77;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Amount";
            this.columnHeader8.Width = 134;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "ID";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(12, 398);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(117, 21);
            this.lblTotalAmount.TabIndex = 6;
            this.lblTotalAmount.Text = "Total amount : ";
            // 
            // cmbClients
            // 
            this.cmbClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbClients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbClients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClients.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(284, 14);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(265, 25);
            this.cmbClients.TabIndex = 2;
            // 
            // cmbEntries
            // 
            this.cmbEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEntries.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbEntries.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbEntries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEntries.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEntries.FormattingEnabled = true;
            this.cmbEntries.Items.AddRange(new object[] {
            "Debit entries",
            "Credit entries"});
            this.cmbEntries.Location = new System.Drawing.Point(555, 13);
            this.cmbEntries.Name = "cmbEntries";
            this.cmbEntries.Size = new System.Drawing.Size(157, 25);
            this.cmbEntries.TabIndex = 3;
            // 
            // chkALL
            // 
            this.chkALL.AutoSize = true;
            this.chkALL.Location = new System.Drawing.Point(222, 14);
            this.chkALL.Name = "chkALL";
            this.chkALL.Size = new System.Drawing.Size(56, 25);
            this.chkALL.TabIndex = 7;
            this.chkALL.Text = "&ALL";
            this.chkALL.UseVisualStyleBackColor = true;
            // 
            // chkShowDeleted
            // 
            this.chkShowDeleted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowDeleted.AutoSize = true;
            this.chkShowDeleted.Location = new System.Drawing.Point(663, 397);
            this.chkShowDeleted.Name = "chkShowDeleted";
            this.chkShowDeleted.Size = new System.Drawing.Size(130, 25);
            this.chkShowDeleted.TabIndex = 8;
            this.chkShowDeleted.Text = "&Show deleted";
            this.chkShowDeleted.UseVisualStyleBackColor = true;
            this.chkShowDeleted.CheckedChanged += new System.EventHandler(this.chkShowDeleted_CheckedChanged);
            // 
            // Records
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(805, 428);
            this.Controls.Add(this.chkShowDeleted);
            this.Controls.Add(this.chkALL);
            this.Controls.Add(this.cmbEntries);
            this.Controls.Add(this.cmbClients);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.dtRecordDate);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Records";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Records";
            this.Load += new System.EventHandler(this.Records_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtRecordDate;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.ComboBox cmbEntries;
        private System.Windows.Forms.CheckBox chkALL;
        private System.Windows.Forms.CheckBox chkShowDeleted;
        private System.Windows.Forms.ColumnHeader columnHeader5;
    }
}