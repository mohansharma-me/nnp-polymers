namespace NNPPoly.forms
{
    partial class frmCompany
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCompany));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvCompanies = new BrightIdeasSoftware.FastObjectListView();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnNewCompany = new System.Windows.Forms.Button();
            this.dtYear = new System.Windows.Forms.DateTimePicker();
            this.btnNext = new System.Windows.Forms.Button();
            this.lcProcess = new MyGUI.Preloader.LoadingCircle();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvCompanies)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NNPPoly.Properties.Resources.Company;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(396, 146);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(396, 36);
            this.label1.TabIndex = 1;
            this.label1.Text = "Company Selection";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvCompanies
            // 
            this.lvCompanies.AllowColumnReorder = true;
            this.lvCompanies.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvCompanies.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvCompanies.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.F2Only;
            this.lvCompanies.CellEditTabChangesRows = true;
            this.lvCompanies.EmptyListMsg = "-: No Companies : -";
            this.lvCompanies.EmptyListMsgFont = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvCompanies.FullRowSelect = true;
            this.lvCompanies.GridLines = true;
            this.lvCompanies.HeaderWordWrap = true;
            this.lvCompanies.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvCompanies.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvCompanies.Location = new System.Drawing.Point(12, 200);
            this.lvCompanies.Name = "lvCompanies";
            this.lvCompanies.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvCompanies.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu;
            this.lvCompanies.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvCompanies.ShowCommandMenuOnRightClick = true;
            this.lvCompanies.ShowGroups = false;
            this.lvCompanies.ShowItemToolTips = true;
            this.lvCompanies.Size = new System.Drawing.Size(396, 345);
            this.lvCompanies.TabIndex = 2;
            this.lvCompanies.TintSortColumn = true;
            this.lvCompanies.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvCompanies.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvCompanies.UseAlternatingBackColors = true;
            this.lvCompanies.UseCompatibleStateImageBehavior = false;
            this.lvCompanies.UseCustomSelectionColors = true;
            this.lvCompanies.UseExplorerTheme = true;
            this.lvCompanies.UseFilterIndicator = true;
            this.lvCompanies.UseFiltering = true;
            this.lvCompanies.UseHotItem = true;
            this.lvCompanies.UseHyperlinks = true;
            this.lvCompanies.View = System.Windows.Forms.View.Details;
            this.lvCompanies.VirtualMode = true;
            this.lvCompanies.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvCompanies_CellEditFinishing);
            this.lvCompanies.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvCompanies_CellEditStarting);
            this.lvCompanies.SelectedIndexChanged += new System.EventHandler(this.lvCompanies_SelectedIndexChanged);
            this.lvCompanies.DoubleClick += new System.EventHandler(this.lvCompanies_DoubleClick);
            this.lvCompanies.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvCompanies_KeyDown);
            this.lvCompanies.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lvCompanies_KeyUp);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(324, 551);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(84, 35);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "&Close";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnNewCompany
            // 
            this.btnNewCompany.Location = new System.Drawing.Point(234, 551);
            this.btnNewCompany.Name = "btnNewCompany";
            this.btnNewCompany.Size = new System.Drawing.Size(84, 35);
            this.btnNewCompany.TabIndex = 3;
            this.btnNewCompany.Text = "&New [+]";
            this.btnNewCompany.UseVisualStyleBackColor = true;
            this.btnNewCompany.Click += new System.EventHandler(this.btnNewCompany_Click);
            // 
            // dtYear
            // 
            this.dtYear.CustomFormat = "yyyy";
            this.dtYear.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtYear.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtYear.Location = new System.Drawing.Point(304, 164);
            this.dtYear.Name = "dtYear";
            this.dtYear.ShowUpDown = true;
            this.dtYear.Size = new System.Drawing.Size(104, 29);
            this.dtYear.TabIndex = 5;
            // 
            // btnNext
            // 
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(12, 551);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(84, 35);
            this.btnNext.TabIndex = 3;
            this.btnNext.Text = "&Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lcProcess
            // 
            this.lcProcess.Active = false;
            this.lcProcess.Color = System.Drawing.Color.DarkGray;
            this.lcProcess.InnerCircleRadius = 5;
            this.lcProcess.Location = new System.Drawing.Point(343, 203);
            this.lcProcess.Name = "lcProcess";
            this.lcProcess.NumberSpoke = 12;
            this.lcProcess.OuterCircleRadius = 11;
            this.lcProcess.RotationSpeed = 100;
            this.lcProcess.Size = new System.Drawing.Size(62, 47);
            this.lcProcess.SpokeThickness = 2;
            this.lcProcess.StylePreset = MyGUI.Preloader.LoadingCircle.StylePresets.MacOSX;
            this.lcProcess.TabIndex = 4;
            this.lcProcess.Text = "loadingCircle1";
            this.lcProcess.Visible = false;
            // 
            // frmCompany
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(420, 595);
            this.Controls.Add(this.dtYear);
            this.Controls.Add(this.lcProcess);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnNewCompany);
            this.Controls.Add(this.lvCompanies);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCompany";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Companies";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCompany_FormClosed);
            this.Load += new System.EventHandler(this.frmCompany_Load);
            this.Shown += new System.EventHandler(this.frmCompany_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lvCompanies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        public BrightIdeasSoftware.FastObjectListView lvCompanies;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnNewCompany;
        private MyGUI.Preloader.LoadingCircle lcProcess;
        private System.Windows.Forms.DateTimePicker dtYear;
        private System.Windows.Forms.Button btnNext;
    }
}

