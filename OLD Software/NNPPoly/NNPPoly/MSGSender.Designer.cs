namespace NNPPoly
{
    partial class MSGSender
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
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Button();
            this.chkSMS = new System.Windows.Forms.CheckBox();
            this.chkEMAIL = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(12, 12);
            this.lv.MultiSelect = false;
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(569, 245);
            this.lv.TabIndex = 1;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            this.lv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Client name";
            this.columnHeader1.Width = 107;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Mobile(s)";
            this.columnHeader2.Width = 107;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Type";
            this.columnHeader3.Width = 62;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "SMS";
            this.columnHeader4.Width = 68;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "EMAIL";
            this.columnHeader5.Width = 88;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Status";
            this.columnHeader6.Width = 106;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(487, 263);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 29);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Close";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Location = new System.Drawing.Point(12, 263);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(199, 29);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "&Send Now";
            this.lblStatus.UseVisualStyleBackColor = true;
            this.lblStatus.Click += new System.EventHandler(this.lblStatus_Click);
            // 
            // chkSMS
            // 
            this.chkSMS.AutoSize = true;
            this.chkSMS.Checked = true;
            this.chkSMS.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSMS.Location = new System.Drawing.Point(252, 268);
            this.chkSMS.Name = "chkSMS";
            this.chkSMS.Size = new System.Drawing.Size(53, 21);
            this.chkSMS.TabIndex = 5;
            this.chkSMS.Text = "SMS";
            this.chkSMS.UseVisualStyleBackColor = true;
            // 
            // chkEMAIL
            // 
            this.chkEMAIL.AutoSize = true;
            this.chkEMAIL.Checked = true;
            this.chkEMAIL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEMAIL.Location = new System.Drawing.Point(328, 268);
            this.chkEMAIL.Name = "chkEMAIL";
            this.chkEMAIL.Size = new System.Drawing.Size(65, 21);
            this.chkEMAIL.TabIndex = 6;
            this.chkEMAIL.Text = "EMAIL";
            this.chkEMAIL.UseVisualStyleBackColor = true;
            // 
            // MSGSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(593, 304);
            this.Controls.Add(this.chkEMAIL);
            this.Controls.Add(this.chkSMS);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lv);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MSGSender";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Message Sender";
            this.Load += new System.EventHandler(this.MSG_Collection_Load);
            this.Shown += new System.EventHandler(this.MSG_Collection_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button lblStatus;
        private System.Windows.Forms.CheckBox chkSMS;
        private System.Windows.Forms.CheckBox chkEMAIL;
    }
}