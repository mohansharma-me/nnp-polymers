namespace NNPPoly.forms
{
    partial class frmSendCustomMessages
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
            this.lvClients = new BrightIdeasSoftware.ObjectListView();
            this.olvColumnClientName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumnMobiles = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSMSMessage = new System.Windows.Forms.RichTextBox();
            this.btnSendBoth = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtMailMessage = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMailSubject = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Client :";
            // 
            // lvClients
            // 
            this.lvClients.AllColumns.Add(this.olvColumnClientName);
            this.lvClients.AllColumns.Add(this.olvColumnMobiles);
            this.lvClients.AllColumns.Add(this.olvColumn1);
            this.lvClients.AllowColumnReorder = true;
            this.lvClients.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvClients.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lvClients.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvClients.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvClients.CellEditTabChangesRows = true;
            this.lvClients.CheckBoxes = true;
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumnClientName,
            this.olvColumnMobiles,
            this.olvColumn1});
            this.lvClients.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvClients.EmptyListMsg = "-: No Clients :-";
            this.lvClients.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.HeaderWordWrap = true;
            this.lvClients.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvClients.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.Location = new System.Drawing.Point(12, 40);
            this.lvClients.Name = "lvClients";
            this.lvClients.OverlayText.Text = "";
            this.lvClients.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvClients.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvClients.ShowCommandMenuOnRightClick = true;
            this.lvClients.ShowGroups = false;
            this.lvClients.ShowItemToolTips = true;
            this.lvClients.Size = new System.Drawing.Size(327, 470);
            this.lvClients.TabIndex = 3;
            this.lvClients.TintSortColumn = true;
            this.lvClients.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvClients.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.UseCustomSelectionColors = true;
            this.lvClients.UseExplorerTheme = true;
            this.lvClients.UseFilterIndicator = true;
            this.lvClients.UseFiltering = true;
            this.lvClients.UseHotItem = true;
            this.lvClients.UseHyperlinks = true;
            this.lvClients.View = System.Windows.Forms.View.Details;
            // 
            // olvColumnClientName
            // 
            this.olvColumnClientName.AspectName = "name";
            this.olvColumnClientName.FillsFreeSpace = true;
            this.olvColumnClientName.HeaderCheckBox = true;
            this.olvColumnClientName.Text = "Client";
            // 
            // olvColumnMobiles
            // 
            this.olvColumnMobiles.AspectName = "mobiles";
            this.olvColumnMobiles.Text = "Mobiles";
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "emails";
            this.olvColumn1.Text = "Emails";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(345, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(484, 450);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtSMSMessage);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(476, 419);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SMS";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtMailSubject);
            this.tabPage2.Controls.Add(this.txtMailMessage);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 27);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(476, 419);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "EMAIL";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(27, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Message :";
            // 
            // txtSMSMessage
            // 
            this.txtSMSMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSMSMessage.Location = new System.Drawing.Point(30, 47);
            this.txtSMSMessage.Name = "txtSMSMessage";
            this.txtSMSMessage.Size = new System.Drawing.Size(421, 338);
            this.txtSMSMessage.TabIndex = 1;
            this.txtSMSMessage.Text = "";
            this.txtSMSMessage.WordWrap = false;
            // 
            // btnSendBoth
            // 
            this.btnSendBoth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendBoth.Location = new System.Drawing.Point(599, 475);
            this.btnSendBoth.Name = "btnSendBoth";
            this.btnSendBoth.Size = new System.Drawing.Size(112, 35);
            this.btnSendBoth.TabIndex = 5;
            this.btnSendBoth.Text = "&Send";
            this.btnSendBoth.UseVisualStyleBackColor = true;
            this.btnSendBoth.Click += new System.EventHandler(this.btnSendBoth_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(717, 475);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 35);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtMailMessage
            // 
            this.txtMailMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailMessage.Location = new System.Drawing.Point(29, 105);
            this.txtMailMessage.Name = "txtMailMessage";
            this.txtMailMessage.Size = new System.Drawing.Size(421, 284);
            this.txtMailMessage.TabIndex = 5;
            this.txtMailMessage.Text = "";
            this.txtMailMessage.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Message :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(27, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 18);
            this.label4.TabIndex = 2;
            this.label4.Text = "Subject : ";
            // 
            // txtMailSubject
            // 
            this.txtMailSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMailSubject.Location = new System.Drawing.Point(29, 41);
            this.txtMailSubject.Name = "txtMailSubject";
            this.txtMailSubject.Size = new System.Drawing.Size(421, 26);
            this.txtMailSubject.TabIndex = 4;
            // 
            // frmSendCustomMessages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(841, 534);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSendBoth);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lvClients);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimizeBox = false;
            this.Name = "frmSendCustomMessages";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Send Messages";
            this.Shown += new System.EventHandler(this.frmSendCustomMessages_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.lvClients)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public BrightIdeasSoftware.ObjectListView lvClients;
        private BrightIdeasSoftware.OLVColumn olvColumnClientName;
        private BrightIdeasSoftware.OLVColumn olvColumnMobiles;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtSMSMessage;
        private System.Windows.Forms.Button btnSendBoth;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RichTextBox txtMailMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMailSubject;
        private System.Windows.Forms.Label label4;
    }
}

