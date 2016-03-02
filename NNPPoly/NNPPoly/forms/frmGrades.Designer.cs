namespace NNPPoly.forms
{
    partial class frmGrades
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGrades));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSessions = new System.Windows.Forms.ComboBox();
            this.lvDetails = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnGradeCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnAmount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnNewSession = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAddGrade = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDeleteSession = new System.Windows.Forms.Button();
            this.btnEditSession = new System.Windows.Forms.Button();
            this.btnStockAlert = new System.Windows.Forms.Button();
            this.olvColumnGradeGroup = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.lvDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Session :";
            // 
            // cmbSessions
            // 
            this.cmbSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSessions.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSessions.FormattingEnabled = true;
            this.cmbSessions.Location = new System.Drawing.Point(12, 31);
            this.cmbSessions.Name = "cmbSessions";
            this.cmbSessions.Size = new System.Drawing.Size(345, 31);
            this.cmbSessions.TabIndex = 1;
            this.cmbSessions.SelectedIndexChanged += new System.EventHandler(this.cmbSessions_SelectedIndexChanged);
            // 
            // lvDetails
            // 
            this.lvDetails.AllColumns.Add(this.olvColumnGradeCode);
            this.lvDetails.AllColumns.Add(this.olvColumnAmount);
            this.lvDetails.AllColumns.Add(this.olvColumnGradeGroup);
            this.lvDetails.AllowColumnReorder = true;
            this.lvDetails.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDetails.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvDetails.CellEditTabChangesRows = true;
            this.lvDetails.CheckBoxes = true;
            this.lvDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnGradeCode,
            this.olvColumnAmount,
            this.olvColumnGradeGroup});
            this.lvDetails.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDetails.EmptyListMsg = "--: No Grades :--";
            this.lvDetails.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.GridLines = true;
            this.lvDetails.HeaderWordWrap = true;
            this.lvDetails.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvDetails.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvDetails.Location = new System.Drawing.Point(12, 69);
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.OverlayText.Text = "";
            this.lvDetails.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvDetails.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvDetails.ShowCommandMenuOnRightClick = true;
            this.lvDetails.ShowGroups = false;
            this.lvDetails.ShowItemToolTips = true;
            this.lvDetails.Size = new System.Drawing.Size(494, 433);
            this.lvDetails.TabIndex = 4;
            this.lvDetails.TintSortColumn = true;
            this.lvDetails.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvDetails.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvDetails.UseCompatibleStateImageBehavior = false;
            this.lvDetails.UseCustomSelectionColors = true;
            this.lvDetails.UseExplorerTheme = true;
            this.lvDetails.UseFilterIndicator = true;
            this.lvDetails.UseFiltering = true;
            this.lvDetails.UseHotItem = true;
            this.lvDetails.UseHyperlinks = true;
            this.lvDetails.View = System.Windows.Forms.View.Details;
            this.lvDetails.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvDetails_CellEditFinishing);
            this.lvDetails.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvDetails_CellEditStarting);
            // 
            // olvColumnGradeCode
            // 
            this.olvColumnGradeCode.AspectName = "code";
            this.olvColumnGradeCode.FillsFreeSpace = true;
            this.olvColumnGradeCode.HeaderCheckBox = true;
            this.olvColumnGradeCode.Text = "Grade";
            // 
            // olvColumnAmount
            // 
            this.olvColumnAmount.AspectName = "grade_amount";
            this.olvColumnAmount.FillsFreeSpace = true;
            this.olvColumnAmount.Text = "Amount";
            // 
            // btnNewSession
            // 
            this.btnNewSession.ImageIndex = 1;
            this.btnNewSession.ImageList = this.imageList1;
            this.btnNewSession.Location = new System.Drawing.Point(363, 29);
            this.btnNewSession.Name = "btnNewSession";
            this.btnNewSession.Size = new System.Drawing.Size(44, 33);
            this.btnNewSession.TabIndex = 5;
            this.btnNewSession.UseVisualStyleBackColor = true;
            this.btnNewSession.Click += new System.EventHandler(this.btnNewSession_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "no.png");
            this.imageList1.Images.SetKeyName(1, "plus_green.png");
            this.imageList1.Images.SetKeyName(2, "pencil_green.png");
            // 
            // btnAddGrade
            // 
            this.btnAddGrade.Location = new System.Drawing.Point(12, 508);
            this.btnAddGrade.Name = "btnAddGrade";
            this.btnAddGrade.Size = new System.Drawing.Size(127, 33);
            this.btnAddGrade.TabIndex = 5;
            this.btnAddGrade.Text = "&New Grade";
            this.btnAddGrade.UseVisualStyleBackColor = true;
            this.btnAddGrade.Click += new System.EventHandler(this.btnAddGrade_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(413, 508);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 33);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnDeleteSession
            // 
            this.btnDeleteSession.ImageIndex = 0;
            this.btnDeleteSession.ImageList = this.imageList1;
            this.btnDeleteSession.Location = new System.Drawing.Point(462, 29);
            this.btnDeleteSession.Name = "btnDeleteSession";
            this.btnDeleteSession.Size = new System.Drawing.Size(44, 33);
            this.btnDeleteSession.TabIndex = 5;
            this.btnDeleteSession.UseVisualStyleBackColor = true;
            this.btnDeleteSession.Click += new System.EventHandler(this.btnDeleteSession_Click);
            // 
            // btnEditSession
            // 
            this.btnEditSession.ImageIndex = 2;
            this.btnEditSession.ImageList = this.imageList1;
            this.btnEditSession.Location = new System.Drawing.Point(413, 29);
            this.btnEditSession.Name = "btnEditSession";
            this.btnEditSession.Size = new System.Drawing.Size(44, 33);
            this.btnEditSession.TabIndex = 5;
            this.btnEditSession.UseVisualStyleBackColor = true;
            this.btnEditSession.Click += new System.EventHandler(this.btnEditSession_Click);
            // 
            // btnStockAlert
            // 
            this.btnStockAlert.Location = new System.Drawing.Point(145, 508);
            this.btnStockAlert.Name = "btnStockAlert";
            this.btnStockAlert.Size = new System.Drawing.Size(140, 33);
            this.btnStockAlert.TabIndex = 5;
            this.btnStockAlert.Text = "&Stock Alert";
            this.btnStockAlert.UseVisualStyleBackColor = true;
            this.btnStockAlert.Click += new System.EventHandler(this.btnStockAlert_Click);
            // 
            // olvColumnGradeGroup
            // 
            this.olvColumnGradeGroup.AspectName = "group";
            this.olvColumnGradeGroup.Text = "Group";
            // 
            // frmGrades
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(519, 555);
            this.Controls.Add(this.lvDetails);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStockAlert);
            this.Controls.Add(this.btnAddGrade);
            this.Controls.Add(this.btnEditSession);
            this.Controls.Add(this.btnDeleteSession);
            this.Controls.Add(this.btnNewSession);
            this.Controls.Add(this.cmbSessions);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGrades";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grades";
            this.Load += new System.EventHandler(this.frmGrades_Load);
            this.Shown += new System.EventHandler(this.frmGrades_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbSessions;
        public BrightIdeasSoftware.ObjectListView lvDetails;
        private BrightIdeasSoftware.OLVColumn olvColumnGradeCode;
        private BrightIdeasSoftware.OLVColumn olvColumnAmount;
        private System.Windows.Forms.Button btnNewSession;
        private System.Windows.Forms.Button btnAddGrade;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnDeleteSession;
        private System.Windows.Forms.Button btnEditSession;
        private System.Windows.Forms.Button btnStockAlert;
        private BrightIdeasSoftware.OLVColumn olvColumnGradeGroup;

    }
}

