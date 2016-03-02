namespace NNPPoly
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menubar = new System.Windows.Forms.MenuStrip();
            this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editCurrentLoggedUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rILDispatchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editSelectedPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteSelectedPaymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.importPaymentsFromExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPaymentsToExcelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exportPaymentsToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewPaymentRecoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreRecoveryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.collectionListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.debitNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.simpleReportStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.priorityTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradesSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.specialTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printFormatSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMSSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterPeriodMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.conflicCheckToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recordsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.collectionListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusbar = new System.Windows.Forms.StatusStrip();
            this.lblLoader = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOpeningBalance = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblLoggedUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel = new System.Windows.Forms.Panel();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.menubar.SuspendLayout();
            this.statusbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // menubar
            // 
            this.menubar.BackColor = System.Drawing.Color.White;
            this.menubar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menubar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.userToolStripMenuItem,
            this.rILDispatchToolStripMenuItem,
            this.txtSearch,
            this.reportToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.filterPeriodMenuItem5,
            this.conflicCheckToolStripMenuItem});
            this.menubar.Location = new System.Drawing.Point(0, 0);
            this.menubar.Name = "menubar";
            this.menubar.Size = new System.Drawing.Size(921, 27);
            this.menubar.TabIndex = 0;
            this.menubar.Text = "menuStrip1";
            // 
            // userToolStripMenuItem
            // 
            this.userToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newUserToolStripMenuItem,
            this.editCurrentLoggedUserToolStripMenuItem,
            this.toolStripMenuItem2,
            this.logoutToolStripMenuItem});
            this.userToolStripMenuItem.Name = "userToolStripMenuItem";
            this.userToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.userToolStripMenuItem.Text = "&Clients";
            // 
            // newUserToolStripMenuItem
            // 
            this.newUserToolStripMenuItem.Name = "newUserToolStripMenuItem";
            this.newUserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.N)));
            this.newUserToolStripMenuItem.Size = new System.Drawing.Size(238, 24);
            this.newUserToolStripMenuItem.Text = "&New clients";
            this.newUserToolStripMenuItem.Click += new System.EventHandler(this.newUserToolStripMenuItem_Click);
            // 
            // editCurrentLoggedUserToolStripMenuItem
            // 
            this.editCurrentLoggedUserToolStripMenuItem.Name = "editCurrentLoggedUserToolStripMenuItem";
            this.editCurrentLoggedUserToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.E)));
            this.editCurrentLoggedUserToolStripMenuItem.Size = new System.Drawing.Size(238, 24);
            this.editCurrentLoggedUserToolStripMenuItem.Text = "&Edit this client";
            this.editCurrentLoggedUserToolStripMenuItem.Click += new System.EventHandler(this.editCurrentLoggedUserToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(235, 6);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(238, 24);
            this.logoutToolStripMenuItem.Text = "&Back to Clients";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
            // 
            // rILDispatchToolStripMenuItem
            // 
            this.rILDispatchToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newEntryToolStripMenuItem,
            this.editSelectedPaymentToolStripMenuItem,
            this.deleteSelectedPaymentsToolStripMenuItem,
            this.toolStripMenuItem3,
            this.importPaymentsFromExcelToolStripMenuItem,
            this.exportPaymentsToExcelToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.exportPaymentsToExcelToolStripMenuItem});
            this.rILDispatchToolStripMenuItem.Name = "rILDispatchToolStripMenuItem";
            this.rILDispatchToolStripMenuItem.Size = new System.Drawing.Size(81, 23);
            this.rILDispatchToolStripMenuItem.Text = "&Payments";
            this.rILDispatchToolStripMenuItem.Click += new System.EventHandler(this.rILDispatchToolStripMenuItem_Click);
            // 
            // newEntryToolStripMenuItem
            // 
            this.newEntryToolStripMenuItem.Name = "newEntryToolStripMenuItem";
            this.newEntryToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newEntryToolStripMenuItem.Size = new System.Drawing.Size(332, 24);
            this.newEntryToolStripMenuItem.Text = "&New entry";
            this.newEntryToolStripMenuItem.Click += new System.EventHandler(this.newEntryToolStripMenuItem_Click);
            // 
            // editSelectedPaymentToolStripMenuItem
            // 
            this.editSelectedPaymentToolStripMenuItem.Name = "editSelectedPaymentToolStripMenuItem";
            this.editSelectedPaymentToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.editSelectedPaymentToolStripMenuItem.Size = new System.Drawing.Size(332, 24);
            this.editSelectedPaymentToolStripMenuItem.Text = "&Edit selected payment";
            this.editSelectedPaymentToolStripMenuItem.Visible = false;
            this.editSelectedPaymentToolStripMenuItem.Click += new System.EventHandler(this.editSelectedPaymentToolStripMenuItem_Click);
            // 
            // deleteSelectedPaymentsToolStripMenuItem
            // 
            this.deleteSelectedPaymentsToolStripMenuItem.Name = "deleteSelectedPaymentsToolStripMenuItem";
            this.deleteSelectedPaymentsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteSelectedPaymentsToolStripMenuItem.Size = new System.Drawing.Size(332, 24);
            this.deleteSelectedPaymentsToolStripMenuItem.Text = "&Delete selected payments";
            this.deleteSelectedPaymentsToolStripMenuItem.Visible = false;
            this.deleteSelectedPaymentsToolStripMenuItem.Click += new System.EventHandler(this.deleteSelectedPaymentsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(329, 6);
            // 
            // importPaymentsFromExcelToolStripMenuItem
            // 
            this.importPaymentsFromExcelToolStripMenuItem.Name = "importPaymentsFromExcelToolStripMenuItem";
            this.importPaymentsFromExcelToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.importPaymentsFromExcelToolStripMenuItem.Size = new System.Drawing.Size(332, 24);
            this.importPaymentsFromExcelToolStripMenuItem.Text = "&Import payments from Excel";
            this.importPaymentsFromExcelToolStripMenuItem.Click += new System.EventHandler(this.importPaymentsFromExcelToolStripMenuItem_Click);
            // 
            // exportPaymentsToExcelToolStripMenuItem1
            // 
            this.exportPaymentsToExcelToolStripMenuItem1.Name = "exportPaymentsToExcelToolStripMenuItem1";
            this.exportPaymentsToExcelToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.E)));
            this.exportPaymentsToExcelToolStripMenuItem1.Size = new System.Drawing.Size(332, 24);
            this.exportPaymentsToExcelToolStripMenuItem1.Text = "&Export payments to Excel";
            this.exportPaymentsToExcelToolStripMenuItem1.Click += new System.EventHandler(this.exportPaymentsToExcelToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(329, 6);
            // 
            // exportPaymentsToExcelToolStripMenuItem
            // 
            this.exportPaymentsToExcelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewPaymentRecoveryToolStripMenuItem,
            this.restoreRecoveryToolStripMenuItem});
            this.exportPaymentsToExcelToolStripMenuItem.Name = "exportPaymentsToExcelToolStripMenuItem";
            this.exportPaymentsToExcelToolStripMenuItem.Size = new System.Drawing.Size(332, 24);
            this.exportPaymentsToExcelToolStripMenuItem.Text = "&Payment Recovery";
            // 
            // createNewPaymentRecoveryToolStripMenuItem
            // 
            this.createNewPaymentRecoveryToolStripMenuItem.Name = "createNewPaymentRecoveryToolStripMenuItem";
            this.createNewPaymentRecoveryToolStripMenuItem.Size = new System.Drawing.Size(203, 24);
            this.createNewPaymentRecoveryToolStripMenuItem.Text = "&Create new recovery";
            this.createNewPaymentRecoveryToolStripMenuItem.Click += new System.EventHandler(this.createNewPaymentRecoveryToolStripMenuItem_Click);
            // 
            // restoreRecoveryToolStripMenuItem
            // 
            this.restoreRecoveryToolStripMenuItem.Name = "restoreRecoveryToolStripMenuItem";
            this.restoreRecoveryToolStripMenuItem.Size = new System.Drawing.Size(203, 24);
            this.restoreRecoveryToolStripMenuItem.Text = "&Restore recovery";
            this.restoreRecoveryToolStripMenuItem.Click += new System.EventHandler(this.restoreRecoveryToolStripMenuItem_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 23);
            this.txtSearch.Text = "Search payment...";
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            this.txtSearch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtSearch_MouseDown);
            this.txtSearch.MouseEnter += new System.EventHandler(this.txtSearch_MouseEnter);
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalReportToolStripMenuItem,
            this.paymentReportToolStripMenuItem,
            this.toolStripMenuItem4,
            this.collectionListToolStripMenuItem1,
            this.debitNotesToolStripMenuItem,
            this.recordsToolStripMenuItem1,
            this.toolStripMenuItem5,
            this.simpleReportStyleToolStripMenuItem});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(62, 23);
            this.reportToolStripMenuItem.Text = "&Report";
            this.reportToolStripMenuItem.Click += new System.EventHandler(this.reportToolStripMenuItem_Click);
            // 
            // generalReportToolStripMenuItem
            // 
            this.generalReportToolStripMenuItem.Name = "generalReportToolStripMenuItem";
            this.generalReportToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.generalReportToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.generalReportToolStripMenuItem.Text = "&Ledger report";
            this.generalReportToolStripMenuItem.Click += new System.EventHandler(this.generalReportToolStripMenuItem_Click);
            // 
            // paymentReportToolStripMenuItem
            // 
            this.paymentReportToolStripMenuItem.Name = "paymentReportToolStripMenuItem";
            this.paymentReportToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.paymentReportToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.paymentReportToolStripMenuItem.Text = "&Monthly report";
            this.paymentReportToolStripMenuItem.Click += new System.EventHandler(this.paymentReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(195, 6);
            // 
            // collectionListToolStripMenuItem1
            // 
            this.collectionListToolStripMenuItem1.Name = "collectionListToolStripMenuItem1";
            this.collectionListToolStripMenuItem1.Size = new System.Drawing.Size(198, 24);
            this.collectionListToolStripMenuItem1.Text = "&Collection List";
            this.collectionListToolStripMenuItem1.Click += new System.EventHandler(this.collectionListToolStripMenuItem1_Click);
            // 
            // debitNotesToolStripMenuItem
            // 
            this.debitNotesToolStripMenuItem.Name = "debitNotesToolStripMenuItem";
            this.debitNotesToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.debitNotesToolStripMenuItem.Text = "&Debit notes/advises";
            this.debitNotesToolStripMenuItem.Click += new System.EventHandler(this.debitNotesToolStripMenuItem_Click);
            // 
            // recordsToolStripMenuItem1
            // 
            this.recordsToolStripMenuItem1.Name = "recordsToolStripMenuItem1";
            this.recordsToolStripMenuItem1.Size = new System.Drawing.Size(198, 24);
            this.recordsToolStripMenuItem1.Text = "&Records";
            this.recordsToolStripMenuItem1.Click += new System.EventHandler(this.recordsToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(195, 6);
            // 
            // simpleReportStyleToolStripMenuItem
            // 
            this.simpleReportStyleToolStripMenuItem.Name = "simpleReportStyleToolStripMenuItem";
            this.simpleReportStyleToolStripMenuItem.Size = new System.Drawing.Size(198, 24);
            this.simpleReportStyleToolStripMenuItem.Text = "&Simple report style";
            this.simpleReportStyleToolStripMenuItem.Click += new System.EventHandler(this.simpleReportStyleToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.priorityTypesToolStripMenuItem,
            this.gradesSettingsToolStripMenuItem,
            this.specialTypesToolStripMenuItem,
            this.printFormatSettingsToolStripMenuItem,
            this.sMSSettingsToolStripMenuItem,
            this.emailSettingsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 23);
            this.settingsToolStripMenuItem.Text = "&Settings";
            // 
            // priorityTypesToolStripMenuItem
            // 
            this.priorityTypesToolStripMenuItem.Name = "priorityTypesToolStripMenuItem";
            this.priorityTypesToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.priorityTypesToolStripMenuItem.Text = "&General settings";
            this.priorityTypesToolStripMenuItem.Click += new System.EventHandler(this.priorityTypesToolStripMenuItem_Click);
            // 
            // gradesSettingsToolStripMenuItem
            // 
            this.gradesSettingsToolStripMenuItem.Name = "gradesSettingsToolStripMenuItem";
            this.gradesSettingsToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.gradesSettingsToolStripMenuItem.Text = "&Grades settings";
            this.gradesSettingsToolStripMenuItem.Click += new System.EventHandler(this.gradesSettingsToolStripMenuItem_Click);
            // 
            // specialTypesToolStripMenuItem
            // 
            this.specialTypesToolStripMenuItem.Name = "specialTypesToolStripMenuItem";
            this.specialTypesToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.specialTypesToolStripMenuItem.Text = "&Special Types";
            this.specialTypesToolStripMenuItem.Click += new System.EventHandler(this.specialTypesToolStripMenuItem_Click);
            // 
            // printFormatSettingsToolStripMenuItem
            // 
            this.printFormatSettingsToolStripMenuItem.Name = "printFormatSettingsToolStripMenuItem";
            this.printFormatSettingsToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.printFormatSettingsToolStripMenuItem.Text = "&Print Format settings";
            this.printFormatSettingsToolStripMenuItem.Click += new System.EventHandler(this.printFormatSettingsToolStripMenuItem_Click);
            // 
            // sMSSettingsToolStripMenuItem
            // 
            this.sMSSettingsToolStripMenuItem.Name = "sMSSettingsToolStripMenuItem";
            this.sMSSettingsToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.sMSSettingsToolStripMenuItem.Text = "&SMS Settings";
            this.sMSSettingsToolStripMenuItem.Click += new System.EventHandler(this.sMSSettingsToolStripMenuItem_Click);
            // 
            // emailSettingsToolStripMenuItem
            // 
            this.emailSettingsToolStripMenuItem.Name = "emailSettingsToolStripMenuItem";
            this.emailSettingsToolStripMenuItem.Size = new System.Drawing.Size(207, 24);
            this.emailSettingsToolStripMenuItem.Text = "&Email Settings";
            this.emailSettingsToolStripMenuItem.Click += new System.EventHandler(this.emailSettingsToolStripMenuItem_Click);
            // 
            // filterPeriodMenuItem5
            // 
            this.filterPeriodMenuItem5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.filterPeriodMenuItem5.Name = "filterPeriodMenuItem5";
            this.filterPeriodMenuItem5.Size = new System.Drawing.Size(94, 23);
            this.filterPeriodMenuItem5.Text = "Filter period";
            this.filterPeriodMenuItem5.Click += new System.EventHandler(this.filterPeriodMenuItem5_Click);
            // 
            // conflicCheckToolStripMenuItem
            // 
            this.conflicCheckToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.conflicCheckToolStripMenuItem.BackColor = System.Drawing.Color.LightGray;
            this.conflicCheckToolStripMenuItem.Name = "conflicCheckToolStripMenuItem";
            this.conflicCheckToolStripMenuItem.Size = new System.Drawing.Size(116, 23);
            this.conflicCheckToolStripMenuItem.Text = "&Conflict Check!!";
            this.conflicCheckToolStripMenuItem.Click += new System.EventHandler(this.conflicCheckToolStripMenuItem_Click);
            // 
            // recordsToolStripMenuItem
            // 
            this.recordsToolStripMenuItem.Name = "recordsToolStripMenuItem";
            this.recordsToolStripMenuItem.Size = new System.Drawing.Size(69, 23);
            this.recordsToolStripMenuItem.Text = "&Records";
            this.recordsToolStripMenuItem.Click += new System.EventHandler(this.recordsToolStripMenuItem_Click);
            // 
            // collectionListToolStripMenuItem
            // 
            this.collectionListToolStripMenuItem.Name = "collectionListToolStripMenuItem";
            this.collectionListToolStripMenuItem.Size = new System.Drawing.Size(106, 23);
            this.collectionListToolStripMenuItem.Text = "&Collection List";
            this.collectionListToolStripMenuItem.Click += new System.EventHandler(this.collectionListToolStripMenuItem_Click);
            // 
            // statusbar
            // 
            this.statusbar.BackColor = System.Drawing.Color.White;
            this.statusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblLoader,
            this.lblOpeningBalance,
            this.lblLoggedUser});
            this.statusbar.Location = new System.Drawing.Point(0, 540);
            this.statusbar.Name = "statusbar";
            this.statusbar.Size = new System.Drawing.Size(921, 22);
            this.statusbar.TabIndex = 1;
            this.statusbar.Text = "statusStrip1";
            // 
            // lblLoader
            // 
            this.lblLoader.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblLoader.Name = "lblLoader";
            this.lblLoader.Size = new System.Drawing.Size(13, 17);
            this.lblLoader.Text = "  ";
            // 
            // lblOpeningBalance
            // 
            this.lblOpeningBalance.Name = "lblOpeningBalance";
            this.lblOpeningBalance.Size = new System.Drawing.Size(103, 17);
            this.lblOpeningBalance.Text = "Opening balance: ";
            // 
            // lblLoggedUser
            // 
            this.lblLoggedUser.Name = "lblLoggedUser";
            this.lblLoggedUser.Size = new System.Drawing.Size(790, 17);
            this.lblLoggedUser.Spring = true;
            this.lblLoggedUser.Text = "..";
            this.lblLoggedUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Control;
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 27);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(921, 513);
            this.panel.TabIndex = 2;
            this.panel.Resize += new System.EventHandler(this.panel_Resize);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(921, 562);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.statusbar);
            this.Controls.Add(this.menubar);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menubar;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NNP Poly";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Main_FormClosed);
            this.Load += new System.EventHandler(this.Main_Load);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.menubar.ResumeLayout(false);
            this.menubar.PerformLayout();
            this.statusbar.ResumeLayout(false);
            this.statusbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menubar;
        private System.Windows.Forms.StatusStrip statusbar;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editCurrentLoggedUserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rILDispatchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newEntryToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblOpeningBalance;
        private System.Windows.Forms.ToolStripStatusLabel lblLoggedUser;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox txtSearch;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.ToolStripMenuItem editSelectedPaymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteSelectedPaymentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem importPaymentsFromExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lblLoader;
        private System.Windows.Forms.ToolStripMenuItem exportPaymentsToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewPaymentRecoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreRecoveryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPaymentsToExcelToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem priorityTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generalReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem simpleReportStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterPeriodMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem gradesSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conflicCheckToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem specialTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recordsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collectionListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem collectionListToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem recordsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem printFormatSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debitNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMSSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailSettingsToolStripMenuItem;

    }
}