namespace NNPPoly.forms
{
    partial class frmMessageSender
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
            this.lvHolders = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMobiles = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.btnSendBoth = new System.Windows.Forms.Button();
            this.btnSendMailOnly = new System.Windows.Forms.Button();
            this.btnSendSMSOnly = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtSMSContent = new System.Windows.Forms.RichTextBox();
            this.btnSaveMsg = new System.Windows.Forms.Button();
            this.pb = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.lvHolders)).BeginInit();
            this.SuspendLayout();
            // 
            // lvHolders
            // 
            this.lvHolders.AllColumns.Add(this.olvColumn1);
            this.lvHolders.AllColumns.Add(this.olvColumnType);
            this.lvHolders.AllColumns.Add(this.olvColumnMobiles);
            this.lvHolders.AllColumns.Add(this.olvColumn2);
            this.lvHolders.AllColumns.Add(this.olvColumn3);
            this.lvHolders.AllColumns.Add(this.olvColumn4);
            this.lvHolders.AllowColumnReorder = true;
            this.lvHolders.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvHolders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvHolders.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.lvHolders.CellEditTabChangesRows = true;
            this.lvHolders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumnType,
            this.olvColumnMobiles,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.lvHolders.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvHolders.EmptyListMsg = "Zero entries";
            this.lvHolders.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvHolders.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvHolders.FullRowSelect = true;
            this.lvHolders.GridLines = true;
            this.lvHolders.HeaderWordWrap = true;
            this.lvHolders.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvHolders.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvHolders.Location = new System.Drawing.Point(12, 12);
            this.lvHolders.MultiSelect = false;
            this.lvHolders.Name = "lvHolders";
            this.lvHolders.OverlayImage.Image = global::NNPPoly.Properties.Resources.adduser;
            this.lvHolders.OverlayText.Text = "Clients";
            this.lvHolders.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvHolders.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvHolders.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvHolders.ShowCommandMenuOnRightClick = true;
            this.lvHolders.ShowGroups = false;
            this.lvHolders.ShowItemToolTips = true;
            this.lvHolders.Size = new System.Drawing.Size(684, 306);
            this.lvHolders.TabIndex = 1;
            this.lvHolders.TintSortColumn = true;
            this.lvHolders.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvHolders.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvHolders.UseAlternatingBackColors = true;
            this.lvHolders.UseCompatibleStateImageBehavior = false;
            this.lvHolders.UseCustomSelectionColors = true;
            this.lvHolders.UseExplorerTheme = true;
            this.lvHolders.UseFilterIndicator = true;
            this.lvHolders.UseFiltering = true;
            this.lvHolders.UseHotItem = true;
            this.lvHolders.UseHyperlinks = true;
            this.lvHolders.UseOverlays = false;
            this.lvHolders.View = System.Windows.Forms.View.Details;
            this.lvHolders.SelectedIndexChanged += new System.EventHandler(this.lvHolders_SelectedIndexChanged);
            this.lvHolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvHolders_KeyDown);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "client_name";
            this.olvColumn1.Text = "Client";
            this.olvColumn1.Width = 131;
            // 
            // olvColumnType
            // 
            this.olvColumnType.AspectName = "type";
            this.olvColumnType.Text = "Type";
            this.olvColumnType.Width = 101;
            // 
            // olvColumnMobiles
            // 
            this.olvColumnMobiles.AspectName = "mobiles";
            this.olvColumnMobiles.Text = "Mobiles";
            this.olvColumnMobiles.Width = 113;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "emails";
            this.olvColumn2.Text = "Emails";
            this.olvColumn2.Width = 112;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "sms_status";
            this.olvColumn3.Text = "SMS Status";
            this.olvColumn3.Width = 106;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "email_status";
            this.olvColumn4.Text = "Email Status";
            this.olvColumn4.Width = 105;
            // 
            // btnSendBoth
            // 
            this.btnSendBoth.Location = new System.Drawing.Point(12, 324);
            this.btnSendBoth.Name = "btnSendBoth";
            this.btnSendBoth.Size = new System.Drawing.Size(116, 32);
            this.btnSendBoth.TabIndex = 2;
            this.btnSendBoth.Text = "Send &Both";
            this.btnSendBoth.UseVisualStyleBackColor = true;
            this.btnSendBoth.Click += new System.EventHandler(this.btnSendBoth_Click);
            // 
            // btnSendMailOnly
            // 
            this.btnSendMailOnly.Location = new System.Drawing.Point(134, 324);
            this.btnSendMailOnly.Name = "btnSendMailOnly";
            this.btnSendMailOnly.Size = new System.Drawing.Size(140, 32);
            this.btnSendMailOnly.TabIndex = 3;
            this.btnSendMailOnly.Text = "Send &Mail Only";
            this.btnSendMailOnly.UseVisualStyleBackColor = true;
            this.btnSendMailOnly.Click += new System.EventHandler(this.btnSendMailOnly_Click);
            // 
            // btnSendSMSOnly
            // 
            this.btnSendSMSOnly.Location = new System.Drawing.Point(280, 324);
            this.btnSendSMSOnly.Name = "btnSendSMSOnly";
            this.btnSendSMSOnly.Size = new System.Drawing.Size(140, 32);
            this.btnSendSMSOnly.TabIndex = 4;
            this.btnSendSMSOnly.Text = "Send &SMS Only";
            this.btnSendSMSOnly.UseVisualStyleBackColor = true;
            this.btnSendSMSOnly.Click += new System.EventHandler(this.btnSendSMSOnly_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(606, 324);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 32);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Cancel";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSMSContent
            // 
            this.txtSMSContent.Enabled = false;
            this.txtSMSContent.Location = new System.Drawing.Point(702, 12);
            this.txtSMSContent.Name = "txtSMSContent";
            this.txtSMSContent.Size = new System.Drawing.Size(235, 306);
            this.txtSMSContent.TabIndex = 6;
            this.txtSMSContent.Text = "";
            this.txtSMSContent.WordWrap = false;
            // 
            // btnSaveMsg
            // 
            this.btnSaveMsg.Enabled = false;
            this.btnSaveMsg.Location = new System.Drawing.Point(702, 324);
            this.btnSaveMsg.Name = "btnSaveMsg";
            this.btnSaveMsg.Size = new System.Drawing.Size(235, 32);
            this.btnSaveMsg.TabIndex = 5;
            this.btnSaveMsg.Text = "&Save";
            this.btnSaveMsg.UseVisualStyleBackColor = true;
            this.btnSaveMsg.Click += new System.EventHandler(this.btnSaveMsg_Click);
            // 
            // pb
            // 
            this.pb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pb.Location = new System.Drawing.Point(426, 324);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(174, 32);
            this.pb.TabIndex = 7;
            // 
            // frmMessageSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(949, 366);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.txtSMSContent);
            this.Controls.Add(this.btnSaveMsg);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSendSMSOnly);
            this.Controls.Add(this.btnSendMailOnly);
            this.Controls.Add(this.btnSendBoth);
            this.Controls.Add(this.lvHolders);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMessageSender";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Message Sender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMessageSender_FormClosing);
            this.Load += new System.EventHandler(this.frmMessageSender_Load);
            this.Shown += new System.EventHandler(this.frmMessageSender_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvHolders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public BrightIdeasSoftware.ObjectListView lvHolders;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumnType;
        private BrightIdeasSoftware.OLVColumn olvColumnMobiles;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Button btnSendBoth;
        private System.Windows.Forms.Button btnSendMailOnly;
        private System.Windows.Forms.Button btnSendSMSOnly;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RichTextBox txtSMSContent;
        private System.Windows.Forms.Button btnSaveMsg;
        private System.Windows.Forms.ProgressBar pb;
    }
}

