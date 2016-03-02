namespace NNPPoly.forms
{
    partial class frmDebiPrioritiesAndTypes
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvDetails = new BrightIdeasSoftware.ObjectListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnAddDP = new System.Windows.Forms.Button();
            this.chkSpecial = new System.Windows.Forms.CheckBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvDetails)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvDetails);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 293);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Priorities && Types";
            // 
            // lvDetails
            // 
            this.lvDetails.AllowColumnReorder = true;
            this.lvDetails.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvDetails.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvDetails.CellEditTabChangesRows = true;
            this.lvDetails.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDetails.EmptyListMsg = "--: No types :--";
            this.lvDetails.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvDetails.FullRowSelect = true;
            this.lvDetails.GridLines = true;
            this.lvDetails.HeaderWordWrap = true;
            this.lvDetails.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvDetails.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvDetails.Location = new System.Drawing.Point(3, 22);
            this.lvDetails.Name = "lvDetails";
            this.lvDetails.OverlayText.Text = "";
            this.lvDetails.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvDetails.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvDetails.ShowCommandMenuOnRightClick = true;
            this.lvDetails.ShowGroups = false;
            this.lvDetails.ShowItemToolTips = true;
            this.lvDetails.Size = new System.Drawing.Size(396, 268);
            this.lvDetails.TabIndex = 3;
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
            this.lvDetails.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvDetails_KeyDown);
            this.lvDetails.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvDetails_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btnAddDP);
            this.groupBox2.Controls.Add(this.chkSpecial);
            this.groupBox2.Controls.Add(this.txtType);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(420, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(268, 255);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "New type";
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(214, 71);
            this.label2.TabIndex = 4;
            this.label2.Text = "Note: If any type is selected to be Special Type than it\'s not considered as Prio" +
    "rity type.";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // btnAddDP
            // 
            this.btnAddDP.Location = new System.Drawing.Point(30, 205);
            this.btnAddDP.Name = "btnAddDP";
            this.btnAddDP.Size = new System.Drawing.Size(75, 31);
            this.btnAddDP.TabIndex = 3;
            this.btnAddDP.Text = "&Add";
            this.btnAddDP.UseVisualStyleBackColor = true;
            this.btnAddDP.Click += new System.EventHandler(this.btnAddDP_Click);
            // 
            // chkSpecial
            // 
            this.chkSpecial.AutoSize = true;
            this.chkSpecial.Location = new System.Drawing.Point(30, 96);
            this.chkSpecial.Name = "chkSpecial";
            this.chkSpecial.Size = new System.Drawing.Size(150, 22);
            this.chkSpecial.TabIndex = 2;
            this.chkSpecial.Text = "Is it special type ?";
            this.chkSpecial.UseVisualStyleBackColor = true;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(30, 63);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(214, 26);
            this.txtType.TabIndex = 1;
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(588, 273);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // frmDebiPrioritiesAndTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(700, 323);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDebiPrioritiesAndTypes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debit Priorities & Types";
            this.Shown += new System.EventHandler(this.frmDebiPrioritiesAndTypes_Shown);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvDetails)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public BrightIdeasSoftware.ObjectListView lvDetails;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.CheckBox chkSpecial;
        private System.Windows.Forms.Button btnAddDP;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label2;
    }
}

