namespace NNPPoly
{
    partial class SMSSettings
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtAPILink = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCollectionSMS = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDespatchSMS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStockSMS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtRequestSMS = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.namDays = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.namDays)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(20, 439);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(63, 31);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(735, 439);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(71, 31);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtAPILink
            // 
            this.txtAPILink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAPILink.Location = new System.Drawing.Point(20, 47);
            this.txtAPILink.Name = "txtAPILink";
            this.txtAPILink.Size = new System.Drawing.Size(786, 27);
            this.txtAPILink.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "API Link :";
            // 
            // txtCollectionSMS
            // 
            this.txtCollectionSMS.AcceptsReturn = true;
            this.txtCollectionSMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCollectionSMS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCollectionSMS.Location = new System.Drawing.Point(20, 118);
            this.txtCollectionSMS.Multiline = true;
            this.txtCollectionSMS.Name = "txtCollectionSMS";
            this.txtCollectionSMS.Size = new System.Drawing.Size(383, 110);
            this.txtCollectionSMS.TabIndex = 9;
            this.txtCollectionSMS.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "Collection SMS :";
            // 
            // txtDespatchSMS
            // 
            this.txtDespatchSMS.AcceptsReturn = true;
            this.txtDespatchSMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDespatchSMS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDespatchSMS.Location = new System.Drawing.Point(409, 118);
            this.txtDespatchSMS.Multiline = true;
            this.txtDespatchSMS.Name = "txtDespatchSMS";
            this.txtDespatchSMS.Size = new System.Drawing.Size(397, 110);
            this.txtDespatchSMS.TabIndex = 11;
            this.txtDespatchSMS.WordWrap = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Despatch SMS :";
            // 
            // txtStockSMS
            // 
            this.txtStockSMS.AcceptsReturn = true;
            this.txtStockSMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStockSMS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockSMS.Location = new System.Drawing.Point(20, 261);
            this.txtStockSMS.Multiline = true;
            this.txtStockSMS.Name = "txtStockSMS";
            this.txtStockSMS.Size = new System.Drawing.Size(383, 110);
            this.txtStockSMS.TabIndex = 13;
            this.txtStockSMS.WordWrap = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "Stock SMS:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtRequestSMS
            // 
            this.txtRequestSMS.AcceptsReturn = true;
            this.txtRequestSMS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequestSMS.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRequestSMS.Location = new System.Drawing.Point(409, 261);
            this.txtRequestSMS.Multiline = true;
            this.txtRequestSMS.Name = "txtRequestSMS";
            this.txtRequestSMS.Size = new System.Drawing.Size(397, 110);
            this.txtRequestSMS.TabIndex = 15;
            this.txtRequestSMS.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(405, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 20);
            this.label5.TabIndex = 14;
            this.label5.Text = "Order Request SMS :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 387);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(222, 20);
            this.label6.TabIndex = 16;
            this.label6.Text = "No of days (for order request) :";
            // 
            // namDays
            // 
            this.namDays.Location = new System.Drawing.Point(244, 385);
            this.namDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.namDays.Name = "namDays";
            this.namDays.Size = new System.Drawing.Size(120, 27);
            this.namDays.TabIndex = 18;
            this.namDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // SMSSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(822, 491);
            this.Controls.Add(this.namDays);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtRequestSMS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtStockSMS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDespatchSMS);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCollectionSMS);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAPILink);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SMSSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SMS Settings";
            this.Load += new System.EventHandler(this.SMSSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.namDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtAPILink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCollectionSMS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDespatchSMS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStockSMS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtRequestSMS;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown namDays;
    }
}