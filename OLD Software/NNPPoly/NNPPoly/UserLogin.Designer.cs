namespace NNPPoly
{
    partial class UserLogin
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
            this.lv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.clientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteClientsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.printClientListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSupplyExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importPaymentExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.collectionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debitNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendGeneralMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.gradeSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printFormatSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMSSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewRecoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreRecoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolstripRequestForAnOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.interestAdviseListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menubar.SuspendLayout();
            this.SuspendLayout();
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
            this.columnHeader6});
            this.lv.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(12, 37);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(791, 455);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            this.lv.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            this.lv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_MouseClick);
            this.lv.MouseEnter += new System.EventHandler(this.lv_MouseEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 49;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Client name";
            this.columnHeader2.Width = 189;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Client description";
            this.columnHeader3.Width = 172;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Opening balance";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 169;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Closing Balance";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader6.Width = 125;
            // 
            // menubar
            // 
            this.menubar.BackColor = System.Drawing.Color.White;
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientsToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.recoveryToolStripMenuItem,
            this.txtSearch,
            this.toolstripRequestForAnOrder});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(815, 27);
            this.menubar.TabIndex = 1;
            this.menubar.Text = "menuStrip1";
            // 
            // clientsToolStripMenuItem
            // 
            this.clientsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newClientsToolStripMenuItem,
            this.editClientToolStripMenuItem,
            this.deleteClientsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.printClientListToolStripMenuItem});
            this.clientsToolStripMenuItem.Name = "clientsToolStripMenuItem";
            this.clientsToolStripMenuItem.Size = new System.Drawing.Size(55, 23);
            this.clientsToolStripMenuItem.Text = "&Clients";
            this.clientsToolStripMenuItem.Click += new System.EventHandler(this.clientsToolStripMenuItem_Click);
            // 
            // newClientsToolStripMenuItem
            // 
            this.newClientsToolStripMenuItem.Name = "newClientsToolStripMenuItem";
            this.newClientsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.newClientsToolStripMenuItem.Text = "&New clients";
            this.newClientsToolStripMenuItem.Click += new System.EventHandler(this.newClientsToolStripMenuItem_Click);
            // 
            // editClientToolStripMenuItem
            // 
            this.editClientToolStripMenuItem.Name = "editClientToolStripMenuItem";
            this.editClientToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.editClientToolStripMenuItem.Text = "&Edit selected client";
            this.editClientToolStripMenuItem.Click += new System.EventHandler(this.editClientToolStripMenuItem_Click);
            // 
            // deleteClientsToolStripMenuItem
            // 
            this.deleteClientsToolStripMenuItem.Name = "deleteClientsToolStripMenuItem";
            this.deleteClientsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.deleteClientsToolStripMenuItem.Text = "&Delete selected clients";
            this.deleteClientsToolStripMenuItem.Click += new System.EventHandler(this.deleteClientsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(191, 6);
            // 
            // printClientListToolStripMenuItem
            // 
            this.printClientListToolStripMenuItem.Name = "printClientListToolStripMenuItem";
            this.printClientListToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.printClientListToolStripMenuItem.Text = "&Print && Save Client List";
            this.printClientListToolStripMenuItem.Click += new System.EventHandler(this.printClientListToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importSupplyExcelToolStripMenuItem,
            this.importPaymentExcelToolStripMenuItem,
            this.toolStripMenuItem1,
            this.interestAdviseListToolStripMenuItem,
            this.collectionListToolStripMenuItem,
            this.debitNotesToolStripMenuItem,
            this.recordsToolStripMenuItem,
            this.sendGeneralMessageToolStripMenuItem,
            this.toolStripMenuItem2,
            this.gradeSettingsToolStripMenuItem,
            this.printFormatSettingsToolStripMenuItem,
            this.sMSSettingsToolStripMenuItem,
            this.emailSettingsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 23);
            this.optionsToolStripMenuItem.Text = "&Options";
            // 
            // importSupplyExcelToolStripMenuItem
            // 
            this.importSupplyExcelToolStripMenuItem.Name = "importSupplyExcelToolStripMenuItem";
            this.importSupplyExcelToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.importSupplyExcelToolStripMenuItem.Text = "&Import Supply Excel";
            this.importSupplyExcelToolStripMenuItem.Click += new System.EventHandler(this.importSupplyExcelToolStripMenuItem_Click);
            // 
            // importPaymentExcelToolStripMenuItem
            // 
            this.importPaymentExcelToolStripMenuItem.Name = "importPaymentExcelToolStripMenuItem";
            this.importPaymentExcelToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.importPaymentExcelToolStripMenuItem.Text = "&Import Payment Excel";
            this.importPaymentExcelToolStripMenuItem.Click += new System.EventHandler(this.importPaymentExcelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(191, 6);
            // 
            // collectionListToolStripMenuItem
            // 
            this.collectionListToolStripMenuItem.Name = "collectionListToolStripMenuItem";
            this.collectionListToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.collectionListToolStripMenuItem.Text = "&Collection List";
            this.collectionListToolStripMenuItem.Click += new System.EventHandler(this.collectionListToolStripMenuItem_Click);
            // 
            // debitNotesToolStripMenuItem
            // 
            this.debitNotesToolStripMenuItem.Name = "debitNotesToolStripMenuItem";
            this.debitNotesToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.debitNotesToolStripMenuItem.Text = "&Debit notes/advises";
            this.debitNotesToolStripMenuItem.Click += new System.EventHandler(this.debitNotesToolStripMenuItem_Click);
            // 
            // recordsToolStripMenuItem
            // 
            this.recordsToolStripMenuItem.Name = "recordsToolStripMenuItem";
            this.recordsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.recordsToolStripMenuItem.Text = "&Records";
            this.recordsToolStripMenuItem.Click += new System.EventHandler(this.recordsToolStripMenuItem_Click);
            // 
            // sendGeneralMessageToolStripMenuItem
            // 
            this.sendGeneralMessageToolStripMenuItem.Name = "sendGeneralMessageToolStripMenuItem";
            this.sendGeneralMessageToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.sendGeneralMessageToolStripMenuItem.Text = "&Send Custom Message";
            this.sendGeneralMessageToolStripMenuItem.Click += new System.EventHandler(this.sendGeneralMessageToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(191, 6);
            // 
            // gradeSettingsToolStripMenuItem
            // 
            this.gradeSettingsToolStripMenuItem.Name = "gradeSettingsToolStripMenuItem";
            this.gradeSettingsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.gradeSettingsToolStripMenuItem.Text = "&Grade settings";
            this.gradeSettingsToolStripMenuItem.Click += new System.EventHandler(this.gradeSettingsToolStripMenuItem_Click);
            // 
            // printFormatSettingsToolStripMenuItem
            // 
            this.printFormatSettingsToolStripMenuItem.Name = "printFormatSettingsToolStripMenuItem";
            this.printFormatSettingsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.printFormatSettingsToolStripMenuItem.Text = "&Print Format settings";
            this.printFormatSettingsToolStripMenuItem.Click += new System.EventHandler(this.printFormatSettingsToolStripMenuItem_Click);
            // 
            // sMSSettingsToolStripMenuItem
            // 
            this.sMSSettingsToolStripMenuItem.Name = "sMSSettingsToolStripMenuItem";
            this.sMSSettingsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.sMSSettingsToolStripMenuItem.Text = "&SMS Settings";
            this.sMSSettingsToolStripMenuItem.Click += new System.EventHandler(this.sMSSettingsToolStripMenuItem_Click);
            // 
            // emailSettingsToolStripMenuItem
            // 
            this.emailSettingsToolStripMenuItem.Name = "emailSettingsToolStripMenuItem";
            this.emailSettingsToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.emailSettingsToolStripMenuItem.Text = "&Email Settings";
            this.emailSettingsToolStripMenuItem.Click += new System.EventHandler(this.emailSettingsToolStripMenuItem_Click);
            // 
            // recoveryToolStripMenuItem
            // 
            this.recoveryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewRecoveryToolStripMenuItem,
            this.restoreRecoveryToolStripMenuItem});
            this.recoveryToolStripMenuItem.Name = "recoveryToolStripMenuItem";
            this.recoveryToolStripMenuItem.Size = new System.Drawing.Size(67, 23);
            this.recoveryToolStripMenuItem.Text = "&Recovery";
            // 
            // createNewRecoveryToolStripMenuItem
            // 
            this.createNewRecoveryToolStripMenuItem.Name = "createNewRecoveryToolStripMenuItem";
            this.createNewRecoveryToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.createNewRecoveryToolStripMenuItem.Text = "&Create new recovery";
            this.createNewRecoveryToolStripMenuItem.Click += new System.EventHandler(this.createNewRecoveryToolStripMenuItem_Click);
            // 
            // restoreRecoveryToolStripMenuItem
            // 
            this.restoreRecoveryToolStripMenuItem.Name = "restoreRecoveryToolStripMenuItem";
            this.restoreRecoveryToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.restoreRecoveryToolStripMenuItem.Text = "&Restore recovery";
            this.restoreRecoveryToolStripMenuItem.Click += new System.EventHandler(this.restoreRecoveryToolStripMenuItem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.Text = "Search client...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // toolstripRequestForAnOrder
            // 
            this.toolstripRequestForAnOrder.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolstripRequestForAnOrder.Name = "toolstripRequestForAnOrder";
            this.toolstripRequestForAnOrder.Size = new System.Drawing.Size(128, 23);
            this.toolstripRequestForAnOrder.Text = "&Request for an Order";
            this.toolstripRequestForAnOrder.Click += new System.EventHandler(this.toolstripRequestForAnOrder_Click);
            // 
            // interestAdviseListToolStripMenuItem
            // 
            this.interestAdviseListToolStripMenuItem.Name = "interestAdviseListToolStripMenuItem";
            this.interestAdviseListToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.interestAdviseListToolStripMenuItem.Text = "&Interest Advise List";
            this.interestAdviseListToolStripMenuItem.Click += new System.EventHandler(this.interestAdviseListToolStripMenuItem_Click);
            // 
            // UserLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(815, 504);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.menubar);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menubar;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UserLogin";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "User Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.UserLogin_Load);
            this.Shown += new System.EventHandler(this.UserLogin_Shown);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.ToolStripMenuItem clientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteClientsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolStripMenuItem createNewRecoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreRecoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editClientToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importSupplyExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importPaymentExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem gradeSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collectionListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem printFormatSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debitNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMSSettingsToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ToolStripMenuItem toolstripRequestForAnOrder;
        private System.Windows.Forms.ToolStripMenuItem emailSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem printClientListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendGeneralMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem interestAdviseListToolStripMenuItem;



    }
}