namespace NNPPoly
{
    partial class GradesSettings
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.importFromExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSAve = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gv = new System.Windows.Forms.DataGridView();
            this.btnSendUpdates = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.clName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFromExcelToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(540, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // importFromExcelToolStripMenuItem
            // 
            this.importFromExcelToolStripMenuItem.Name = "importFromExcelToolStripMenuItem";
            this.importFromExcelToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.importFromExcelToolStripMenuItem.Text = "&Import from Excel";
            this.importFromExcelToolStripMenuItem.Click += new System.EventHandler(this.importFromExcelToolStripMenuItem_Click);
            // 
            // btnSAve
            // 
            this.btnSAve.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSAve.Location = new System.Drawing.Point(12, 377);
            this.btnSAve.Name = "btnSAve";
            this.btnSAve.Size = new System.Drawing.Size(86, 32);
            this.btnSAve.TabIndex = 3;
            this.btnSAve.Text = "&Save";
            this.btnSAve.UseVisualStyleBackColor = true;
            this.btnSAve.Click += new System.EventHandler(this.btnSAve_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(442, 377);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 32);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // gv
            // 
            this.gv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gv.BackgroundColor = System.Drawing.SystemColors.Control;
            this.gv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.clName,
            this.clDesc});
            this.gv.Location = new System.Drawing.Point(12, 36);
            this.gv.Name = "gv";
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.gv.Size = new System.Drawing.Size(516, 335);
            this.gv.TabIndex = 5;
            this.gv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gv_CellContentClick);
            // 
            // btnSendUpdates
            // 
            this.btnSendUpdates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSendUpdates.Location = new System.Drawing.Point(104, 377);
            this.btnSendUpdates.Name = "btnSendUpdates";
            this.btnSendUpdates.Size = new System.Drawing.Size(183, 32);
            this.btnSendUpdates.TabIndex = 6;
            this.btnSendUpdates.Text = "&Send Stock Message";
            this.btnSendUpdates.UseVisualStyleBackColor = true;
            this.btnSendUpdates.Click += new System.EventHandler(this.btnSendUpdates_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Send Stock Message";
            this.Column1.Name = "Column1";
            // 
            // clName
            // 
            this.clName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clName.HeaderText = "Grade name";
            this.clName.Name = "clName";
            // 
            // clDesc
            // 
            this.clDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clDesc.HeaderText = "Amount";
            this.clDesc.Name = "clDesc";
            // 
            // GradesSettings
            // 
            this.AcceptButton = this.btnSAve;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(540, 421);
            this.Controls.Add(this.btnSendUpdates);
            this.Controls.Add(this.gv);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSAve);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GradesSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Grades Settings";
            this.Load += new System.EventHandler(this.GradesSettings_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem importFromExcelToolStripMenuItem;
        private System.Windows.Forms.Button btnSAve;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView gv;
        private System.Windows.Forms.Button btnSendUpdates;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDesc;
    }
}