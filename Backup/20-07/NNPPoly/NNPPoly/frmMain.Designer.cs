namespace NNPPoly
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.toolBar = new System.Windows.Forms.ToolStrip();
            this.btnCloseLedger = new System.Windows.Forms.ToolStripButton();
            this.btnNewClient = new System.Windows.Forms.ToolStripButton();
            this.btnImports = new System.Windows.Forms.ToolStripButton();
            this.btnNewPaymentEntry = new System.Windows.Forms.ToolStripButton();
            this.btnLedgerReport = new System.Windows.Forms.ToolStripButton();
            this.btnInterestAdviseList = new System.Windows.Forms.ToolStripButton();
            this.btnCollectionList = new System.Windows.Forms.ToolStripButton();
            this.btnDNotesAdvises = new System.Windows.Forms.ToolStripButton();
            this.btnRecords = new System.Windows.Forms.ToolStripButton();
            this.btnSchemes = new System.Windows.Forms.ToolStripButton();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnSettings = new System.Windows.Forms.ToolStripDropDownButton();
            this.changeDatabaseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.companyProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.requestOrdersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.holidaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradeSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gradegToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.printFormatSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sMSSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.emailSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debitPrioritiesTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.restoreDataFromPreviousVersionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFromRecoveryOldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToRecoveryOldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.importGradesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportPaymentTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutNNPPolymersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnChangeCompany = new System.Windows.Forms.ToolStripButton();
            this.lvClients = new BrightIdeasSoftware.ObjectListView();
            this.statusBar = new System.Windows.Forms.ToolStrip();
            this.btnPrintClientlist = new System.Windows.Forms.ToolStripButton();
            this.btnRefreshClients = new System.Windows.Forms.ToolStripButton();
            this.panClients = new System.Windows.Forms.Panel();
            this.panLedger = new System.Windows.Forms.Panel();
            this.panCollectionList = new System.Windows.Forms.Panel();
            this.panInterestAdvise = new System.Windows.Forms.Panel();
            this.panRecords = new System.Windows.Forms.Panel();
            this.panDebitNotes = new System.Windows.Forms.Panel();
            this.panSchemes = new System.Windows.Forms.Panel();
            this.toolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).BeginInit();
            this.statusBar.SuspendLayout();
            this.panClients.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBar
            // 
            this.toolBar.Font = new System.Drawing.Font("Arial", 10F);
            this.toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolBar.ImageScalingSize = new System.Drawing.Size(52, 52);
            this.toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseLedger,
            this.btnNewClient,
            this.btnImports,
            this.btnNewPaymentEntry,
            this.btnLedgerReport,
            this.btnInterestAdviseList,
            this.btnCollectionList,
            this.btnDNotesAdvises,
            this.btnRecords,
            this.btnSchemes,
            this.btnExit,
            this.btnSettings,
            this.btnChangeCompany});
            this.toolBar.Location = new System.Drawing.Point(0, 0);
            this.toolBar.Name = "toolBar";
            this.toolBar.Size = new System.Drawing.Size(1000, 75);
            this.toolBar.TabIndex = 0;
            this.toolBar.Text = "Toolbar";
            // 
            // btnCloseLedger
            // 
            this.btnCloseLedger.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCloseLedger.Image = global::NNPPoly.Properties.Resources.arrow_plain_green_W;
            this.btnCloseLedger.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseLedger.Name = "btnCloseLedger";
            this.btnCloseLedger.Size = new System.Drawing.Size(56, 72);
            this.btnCloseLedger.Text = "&Clients";
            this.btnCloseLedger.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCloseLedger.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCloseLedger.ToolTipText = "Go back to Client list...";
            this.btnCloseLedger.Visible = false;
            this.btnCloseLedger.Click += new System.EventHandler(this.btnCloseLedger_Click);
            // 
            // btnNewClient
            // 
            this.btnNewClient.Image = global::NNPPoly.Properties.Resources.adduser;
            this.btnNewClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewClient.Name = "btnNewClient";
            this.btnNewClient.Size = new System.Drawing.Size(78, 72);
            this.btnNewClient.Text = "&New Client";
            this.btnNewClient.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNewClient.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewClient.ToolTipText = "Create new client profile...";
            this.btnNewClient.Click += new System.EventHandler(this.btnNewClient_Click);
            // 
            // btnImports
            // 
            this.btnImports.Image = global::NNPPoly.Properties.Resources.table_pencil_green;
            this.btnImports.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImports.Name = "btnImports";
            this.btnImports.Size = new System.Drawing.Size(84, 72);
            this.btnImports.Text = "&Upload Mgr";
            this.btnImports.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnImports.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImports.ToolTipText = "Upload debits/credits entries";
            this.btnImports.Click += new System.EventHandler(this.btnImports_Click);
            // 
            // btnNewPaymentEntry
            // 
            this.btnNewPaymentEntry.Image = global::NNPPoly.Properties.Resources.plus_blue;
            this.btnNewPaymentEntry.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPaymentEntry.Name = "btnNewPaymentEntry";
            this.btnNewPaymentEntry.Size = new System.Drawing.Size(75, 72);
            this.btnNewPaymentEntry.Text = "&New Entry";
            this.btnNewPaymentEntry.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnNewPaymentEntry.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNewPaymentEntry.ToolTipText = "Add new debit/credit entry";
            this.btnNewPaymentEntry.Click += new System.EventHandler(this.btnNewPaymentEntry_Click);
            // 
            // btnLedgerReport
            // 
            this.btnLedgerReport.Image = global::NNPPoly.Properties.Resources.ClientPanel;
            this.btnLedgerReport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLedgerReport.Name = "btnLedgerReport";
            this.btnLedgerReport.Size = new System.Drawing.Size(88, 72);
            this.btnLedgerReport.Text = "&Client Panel";
            this.btnLedgerReport.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLedgerReport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLedgerReport.Visible = false;
            this.btnLedgerReport.Click += new System.EventHandler(this.btnLedgerReport_Click);
            // 
            // btnInterestAdviseList
            // 
            this.btnInterestAdviseList.Image = global::NNPPoly.Properties.Resources.interestadviselist;
            this.btnInterestAdviseList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInterestAdviseList.Name = "btnInterestAdviseList";
            this.btnInterestAdviseList.Size = new System.Drawing.Size(104, 72);
            this.btnInterestAdviseList.Text = "&Interest Advise";
            this.btnInterestAdviseList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInterestAdviseList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnInterestAdviseList.Click += new System.EventHandler(this.btnInterestAdviseList_Click);
            // 
            // btnCollectionList
            // 
            this.btnCollectionList.Image = ((System.Drawing.Image)(resources.GetObject("btnCollectionList.Image")));
            this.btnCollectionList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCollectionList.Name = "btnCollectionList";
            this.btnCollectionList.Size = new System.Drawing.Size(100, 72);
            this.btnCollectionList.Text = "&Collection List";
            this.btnCollectionList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCollectionList.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCollectionList.Click += new System.EventHandler(this.btnCollectionList_Click);
            // 
            // btnDNotesAdvises
            // 
            this.btnDNotesAdvises.Image = global::NNPPoly.Properties.Resources.paste;
            this.btnDNotesAdvises.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDNotesAdvises.Name = "btnDNotesAdvises";
            this.btnDNotesAdvises.Size = new System.Drawing.Size(119, 72);
            this.btnDNotesAdvises.Text = "&D. Notes/Advises";
            this.btnDNotesAdvises.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDNotesAdvises.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnDNotesAdvises.Click += new System.EventHandler(this.btnDNotesAdvises_Click);
            // 
            // btnRecords
            // 
            this.btnRecords.Image = ((System.Drawing.Image)(resources.GetObject("btnRecords.Image")));
            this.btnRecords.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRecords.Name = "btnRecords";
            this.btnRecords.Size = new System.Drawing.Size(65, 72);
            this.btnRecords.Text = "&Records";
            this.btnRecords.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRecords.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRecords.Click += new System.EventHandler(this.btnRecords_Click);
            // 
            // btnSchemes
            // 
            this.btnSchemes.Image = global::NNPPoly.Properties.Resources.Spreadsheet;
            this.btnSchemes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSchemes.Name = "btnSchemes";
            this.btnSchemes.Size = new System.Drawing.Size(70, 72);
            this.btnSchemes.Text = "&Schemes";
            this.btnSchemes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSchemes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSchemes.Click += new System.EventHandler(this.btnSchemes_Click);
            // 
            // btnExit
            // 
            this.btnExit.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnExit.Image = global::NNPPoly.Properties.Resources.exit;
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(56, 72);
            this.btnExit.Text = "&Exit";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeDatabaseFolderToolStripMenuItem,
            this.companyProfileToolStripMenuItem,
            this.toolStripSeparator1,
            this.requestOrdersToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.holidaysToolStripMenuItem,
            this.gradeSettingsToolStripMenuItem,
            this.gradegToolStripMenuItem,
            this.printFormatSettingsToolStripMenuItem,
            this.sMSSettingsToolStripMenuItem,
            this.emailSettingsToolStripMenuItem,
            this.debitPrioritiesTypesToolStripMenuItem,
            this.toolStripMenuItem3,
            this.restoreDataFromPreviousVersionToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.excelTemplatesToolStripMenuItem,
            this.toolStripMenuItem5,
            this.aboutNNPPolymersToolStripMenuItem});
            this.btnSettings.Image = global::NNPPoly.Properties.Resources.settings;
            this.btnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(72, 72);
            this.btnSettings.Text = "&Settings";
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // changeDatabaseFolderToolStripMenuItem
            // 
            this.changeDatabaseFolderToolStripMenuItem.Name = "changeDatabaseFolderToolStripMenuItem";
            this.changeDatabaseFolderToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.changeDatabaseFolderToolStripMenuItem.Text = "&Change Database Folder";
            this.changeDatabaseFolderToolStripMenuItem.Click += new System.EventHandler(this.changeDatabaseFolderToolStripMenuItem_Click);
            // 
            // companyProfileToolStripMenuItem
            // 
            this.companyProfileToolStripMenuItem.Name = "companyProfileToolStripMenuItem";
            this.companyProfileToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.companyProfileToolStripMenuItem.Text = "&Company Profile";
            this.companyProfileToolStripMenuItem.Click += new System.EventHandler(this.companyProfileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(233, 6);
            // 
            // requestOrdersToolStripMenuItem
            // 
            this.requestOrdersToolStripMenuItem.Name = "requestOrdersToolStripMenuItem";
            this.requestOrdersToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.requestOrdersToolStripMenuItem.Text = "&Request Orders";
            this.requestOrdersToolStripMenuItem.Click += new System.EventHandler(this.requestOrdersToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(236, 22);
            this.toolStripMenuItem1.Text = "Send custom &message";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(233, 6);
            // 
            // holidaysToolStripMenuItem
            // 
            this.holidaysToolStripMenuItem.Name = "holidaysToolStripMenuItem";
            this.holidaysToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.holidaysToolStripMenuItem.Text = "&Holidays";
            this.holidaysToolStripMenuItem.Click += new System.EventHandler(this.holidaysToolStripMenuItem_Click);
            // 
            // gradeSettingsToolStripMenuItem
            // 
            this.gradeSettingsToolStripMenuItem.Name = "gradeSettingsToolStripMenuItem";
            this.gradeSettingsToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.gradeSettingsToolStripMenuItem.Text = "&Grade settings";
            this.gradeSettingsToolStripMenuItem.Click += new System.EventHandler(this.gradeSettingsToolStripMenuItem_Click);
            // 
            // gradegToolStripMenuItem
            // 
            this.gradegToolStripMenuItem.Name = "gradegToolStripMenuItem";
            this.gradegToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.gradegToolStripMenuItem.Text = "&Grade Groups";
            this.gradegToolStripMenuItem.Click += new System.EventHandler(this.gradegToolStripMenuItem_Click);
            // 
            // printFormatSettingsToolStripMenuItem
            // 
            this.printFormatSettingsToolStripMenuItem.Name = "printFormatSettingsToolStripMenuItem";
            this.printFormatSettingsToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.printFormatSettingsToolStripMenuItem.Text = "&Print format settings";
            this.printFormatSettingsToolStripMenuItem.Click += new System.EventHandler(this.printFormatSettingsToolStripMenuItem_Click);
            // 
            // sMSSettingsToolStripMenuItem
            // 
            this.sMSSettingsToolStripMenuItem.Name = "sMSSettingsToolStripMenuItem";
            this.sMSSettingsToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.sMSSettingsToolStripMenuItem.Text = "&SMS settings";
            this.sMSSettingsToolStripMenuItem.Click += new System.EventHandler(this.sMSSettingsToolStripMenuItem_Click);
            // 
            // emailSettingsToolStripMenuItem
            // 
            this.emailSettingsToolStripMenuItem.Name = "emailSettingsToolStripMenuItem";
            this.emailSettingsToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.emailSettingsToolStripMenuItem.Text = "&E-mail settings";
            this.emailSettingsToolStripMenuItem.Click += new System.EventHandler(this.emailSettingsToolStripMenuItem_Click);
            // 
            // debitPrioritiesTypesToolStripMenuItem
            // 
            this.debitPrioritiesTypesToolStripMenuItem.Name = "debitPrioritiesTypesToolStripMenuItem";
            this.debitPrioritiesTypesToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.debitPrioritiesTypesToolStripMenuItem.Text = "&Debit Priorities && Types";
            this.debitPrioritiesTypesToolStripMenuItem.Click += new System.EventHandler(this.debitPrioritiesTypesToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(233, 6);
            // 
            // restoreDataFromPreviousVersionToolStripMenuItem
            // 
            this.restoreDataFromPreviousVersionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromRecoveryOldToolStripMenuItem,
            this.exportToRecoveryOldToolStripMenuItem,
            this.toolStripMenuItem4,
            this.importGradesToolStripMenuItem});
            this.restoreDataFromPreviousVersionToolStripMenuItem.Name = "restoreDataFromPreviousVersionToolStripMenuItem";
            this.restoreDataFromPreviousVersionToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.restoreDataFromPreviousVersionToolStripMenuItem.Text = "&Recovery";
            // 
            // importFromRecoveryOldToolStripMenuItem
            // 
            this.importFromRecoveryOldToolStripMenuItem.Name = "importFromRecoveryOldToolStripMenuItem";
            this.importFromRecoveryOldToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.importFromRecoveryOldToolStripMenuItem.Text = "Import from Recovery (Old)";
            this.importFromRecoveryOldToolStripMenuItem.Click += new System.EventHandler(this.importFromRecoveryOldToolStripMenuItem_Click);
            // 
            // exportToRecoveryOldToolStripMenuItem
            // 
            this.exportToRecoveryOldToolStripMenuItem.Name = "exportToRecoveryOldToolStripMenuItem";
            this.exportToRecoveryOldToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.exportToRecoveryOldToolStripMenuItem.Text = "Export to Recovery (Old)";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(244, 6);
            // 
            // importGradesToolStripMenuItem
            // 
            this.importGradesToolStripMenuItem.Name = "importGradesToolStripMenuItem";
            this.importGradesToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.importGradesToolStripMenuItem.Text = "Import &Grades";
            this.importGradesToolStripMenuItem.Click += new System.EventHandler(this.importGradesToolStripMenuItem_Click);
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "&Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // excelTemplatesToolStripMenuItem
            // 
            this.excelTemplatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportPaymentTableToolStripMenuItem});
            this.excelTemplatesToolStripMenuItem.Name = "excelTemplatesToolStripMenuItem";
            this.excelTemplatesToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.excelTemplatesToolStripMenuItem.Text = "Excel &Templates";
            // 
            // exportPaymentTableToolStripMenuItem
            // 
            this.exportPaymentTableToolStripMenuItem.Name = "exportPaymentTableToolStripMenuItem";
            this.exportPaymentTableToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.exportPaymentTableToolStripMenuItem.Text = "Export \"Payment Table\"";
            this.exportPaymentTableToolStripMenuItem.Click += new System.EventHandler(this.exportPaymentTableToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(233, 6);
            // 
            // aboutNNPPolymersToolStripMenuItem
            // 
            this.aboutNNPPolymersToolStripMenuItem.Name = "aboutNNPPolymersToolStripMenuItem";
            this.aboutNNPPolymersToolStripMenuItem.Size = new System.Drawing.Size(236, 22);
            this.aboutNNPPolymersToolStripMenuItem.Text = "&About \"NNP Polymers\"";
            this.aboutNNPPolymersToolStripMenuItem.Click += new System.EventHandler(this.aboutNNPPolymersToolStripMenuItem_Click);
            // 
            // btnChangeCompany
            // 
            this.btnChangeCompany.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnChangeCompany.Image = global::NNPPoly.Properties.Resources.Company;
            this.btnChangeCompany.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnChangeCompany.Name = "btnChangeCompany";
            this.btnChangeCompany.Size = new System.Drawing.Size(83, 72);
            this.btnChangeCompany.Text = "&Companies";
            this.btnChangeCompany.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnChangeCompany.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnChangeCompany.Click += new System.EventHandler(this.btnChangeCompany_Click);
            // 
            // lvClients
            // 
            this.lvClients.AllowColumnReorder = true;
            this.lvClients.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvClients.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.lvClients.CellEditTabChangesRows = true;
            this.lvClients.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvClients.EmptyListMsg = "--: No clients :--";
            this.lvClients.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderWordWrap = true;
            this.lvClients.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvClients.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.Location = new System.Drawing.Point(0, 0);
            this.lvClients.Name = "lvClients";
            this.lvClients.OverlayImage.Image = global::NNPPoly.Properties.Resources.adduser;
            this.lvClients.OverlayText.Text = "Clients";
            this.lvClients.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvClients.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvClients.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvClients.ShowCommandMenuOnRightClick = true;
            this.lvClients.ShowGroups = false;
            this.lvClients.ShowItemToolTips = true;
            this.lvClients.Size = new System.Drawing.Size(322, 185);
            this.lvClients.TabIndex = 1;
            this.lvClients.TintSortColumn = true;
            this.lvClients.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvClients.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.UseAlternatingBackColors = true;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.UseCustomSelectionColors = true;
            this.lvClients.UseExplorerTheme = true;
            this.lvClients.UseFilterIndicator = true;
            this.lvClients.UseFiltering = true;
            this.lvClients.UseHotItem = true;
            this.lvClients.UseHyperlinks = true;
            this.lvClients.View = System.Windows.Forms.View.Details;
            this.lvClients.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvClients_CellEditFinishing);
            this.lvClients.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvClients_CellEditStarting);
            this.lvClients.SelectedIndexChanged += new System.EventHandler(this.lvClients_SelectedIndexChanged);
            this.lvClients.DoubleClick += new System.EventHandler(this.lvClients_DoubleClick);
            this.lvClients.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvClients_KeyDown);
            this.lvClients.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvClients_MouseDown);
            this.lvClients.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvClients_MouseDown_1);
            // 
            // statusBar
            // 
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.statusBar.ImageScalingSize = new System.Drawing.Size(36, 36);
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPrintClientlist,
            this.btnRefreshClients});
            this.statusBar.Location = new System.Drawing.Point(0, 185);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(322, 25);
            this.statusBar.TabIndex = 2;
            this.statusBar.Text = "toolStrip1";
            // 
            // btnPrintClientlist
            // 
            this.btnPrintClientlist.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnPrintClientlist.Image = ((System.Drawing.Image)(resources.GetObject("btnPrintClientlist.Image")));
            this.btnPrintClientlist.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrintClientlist.Name = "btnPrintClientlist";
            this.btnPrintClientlist.Size = new System.Drawing.Size(117, 22);
            this.btnPrintClientlist.Text = "&Print Client List";
            this.btnPrintClientlist.Click += new System.EventHandler(this.btnPrintClientlist_Click);
            // 
            // btnRefreshClients
            // 
            this.btnRefreshClients.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnRefreshClients.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshClients.Image")));
            this.btnRefreshClients.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefreshClients.Name = "btnRefreshClients";
            this.btnRefreshClients.Size = new System.Drawing.Size(66, 22);
            this.btnRefreshClients.Text = "&Refresh";
            this.btnRefreshClients.Click += new System.EventHandler(this.btnRefreshClients_Click);
            // 
            // panClients
            // 
            this.panClients.Controls.Add(this.lvClients);
            this.panClients.Controls.Add(this.statusBar);
            this.panClients.Location = new System.Drawing.Point(34, 144);
            this.panClients.Name = "panClients";
            this.panClients.Size = new System.Drawing.Size(322, 210);
            this.panClients.TabIndex = 3;
            // 
            // panLedger
            // 
            this.panLedger.Location = new System.Drawing.Point(362, 144);
            this.panLedger.Name = "panLedger";
            this.panLedger.Size = new System.Drawing.Size(100, 91);
            this.panLedger.TabIndex = 4;
            // 
            // panCollectionList
            // 
            this.panCollectionList.Location = new System.Drawing.Point(468, 144);
            this.panCollectionList.Name = "panCollectionList";
            this.panCollectionList.Size = new System.Drawing.Size(98, 91);
            this.panCollectionList.TabIndex = 5;
            // 
            // panInterestAdvise
            // 
            this.panInterestAdvise.Location = new System.Drawing.Point(362, 241);
            this.panInterestAdvise.Name = "panInterestAdvise";
            this.panInterestAdvise.Size = new System.Drawing.Size(98, 91);
            this.panInterestAdvise.TabIndex = 5;
            // 
            // panRecords
            // 
            this.panRecords.Location = new System.Drawing.Point(468, 241);
            this.panRecords.Name = "panRecords";
            this.panRecords.Size = new System.Drawing.Size(98, 91);
            this.panRecords.TabIndex = 5;
            // 
            // panDebitNotes
            // 
            this.panDebitNotes.Location = new System.Drawing.Point(572, 144);
            this.panDebitNotes.Name = "panDebitNotes";
            this.panDebitNotes.Size = new System.Drawing.Size(98, 91);
            this.panDebitNotes.TabIndex = 6;
            // 
            // panSchemes
            // 
            this.panSchemes.Location = new System.Drawing.Point(570, 240);
            this.panSchemes.Name = "panSchemes";
            this.panSchemes.Size = new System.Drawing.Size(98, 91);
            this.panSchemes.TabIndex = 5;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panDebitNotes);
            this.Controls.Add(this.panSchemes);
            this.Controls.Add(this.panRecords);
            this.Controls.Add(this.panInterestAdvise);
            this.Controls.Add(this.panCollectionList);
            this.Controls.Add(this.panLedger);
            this.Controls.Add(this.panClients);
            this.Controls.Add(this.toolBar);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1000, 700);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
            this.toolBar.ResumeLayout(false);
            this.toolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.panClients.ResumeLayout(false);
            this.panClients.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolBar;
        private System.Windows.Forms.ToolStripButton btnNewClient;
        private System.Windows.Forms.ToolStripButton btnInterestAdviseList;
        private System.Windows.Forms.ToolStripButton btnCollectionList;
        private System.Windows.Forms.ToolStripButton btnDNotesAdvises;
        private System.Windows.Forms.ToolStripButton btnRecords;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripDropDownButton btnSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem gradeSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printFormatSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sMSSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem emailSettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStrip statusBar;
        private System.Windows.Forms.ToolStripButton btnLedgerReport;
        private System.Windows.Forms.Panel panClients;
        private System.Windows.Forms.Panel panLedger;
        private System.Windows.Forms.ToolStripButton btnCloseLedger;
        private System.Windows.Forms.ToolStripButton btnNewPaymentEntry;
        public BrightIdeasSoftware.ObjectListView lvClients;
        private System.Windows.Forms.ToolStripButton btnChangeCompany;
        private System.Windows.Forms.ToolStripMenuItem debitPrioritiesTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem restoreDataFromPreviousVersionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importFromRecoveryOldToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToRecoveryOldToolStripMenuItem;
        private System.Windows.Forms.Panel panCollectionList;
        private System.Windows.Forms.Panel panInterestAdvise;
        private System.Windows.Forms.Panel panRecords;
        private System.Windows.Forms.ToolStripButton btnImports;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem importGradesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem companyProfileToolStripMenuItem;
        private System.Windows.Forms.Panel panDebitNotes;
        private System.Windows.Forms.ToolStripMenuItem excelTemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportPaymentTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem aboutNNPPolymersToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem requestOrdersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem holidaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnPrintClientlist;
        private System.Windows.Forms.ToolStripMenuItem changeDatabaseFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnRefreshClients;
        private System.Windows.Forms.ToolStripMenuItem gradegToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnSchemes;
        private System.Windows.Forms.Panel panSchemes;

    }
}

