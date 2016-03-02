namespace NNPPoly
{
    partial class MailSettings
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
            this.txtMyEmail = new System.Windows.Forms.TextBox();
            this.txtGmailUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGmailPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkShowPass = new System.Windows.Forms.CheckBox();
            this.lblFCol = new System.Windows.Forms.Label();
            this.lblFSto = new System.Windows.Forms.Label();
            this.lblFDes = new System.Windows.Forms.Label();
            this.lblFReq = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtFooterMSG = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "My Email Address :";
            // 
            // txtMyEmail
            // 
            this.txtMyEmail.Location = new System.Drawing.Point(16, 33);
            this.txtMyEmail.Name = "txtMyEmail";
            this.txtMyEmail.Size = new System.Drawing.Size(398, 25);
            this.txtMyEmail.TabIndex = 1;
            // 
            // txtGmailUser
            // 
            this.txtGmailUser.Location = new System.Drawing.Point(16, 91);
            this.txtGmailUser.Name = "txtGmailUser";
            this.txtGmailUser.Size = new System.Drawing.Size(398, 25);
            this.txtGmailUser.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(234, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "GMail Username : (Full email address)";
            // 
            // txtGmailPass
            // 
            this.txtGmailPass.Location = new System.Drawing.Point(16, 148);
            this.txtGmailPass.Name = "txtGmailPass";
            this.txtGmailPass.Size = new System.Drawing.Size(398, 25);
            this.txtGmailPass.TabIndex = 5;
            this.txtGmailPass.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "GMail Password: (Case sensitive)";
            // 
            // chkShowPass
            // 
            this.chkShowPass.AutoSize = true;
            this.chkShowPass.Location = new System.Drawing.Point(399, 130);
            this.chkShowPass.Name = "chkShowPass";
            this.chkShowPass.Size = new System.Drawing.Size(15, 14);
            this.chkShowPass.TabIndex = 6;
            this.chkShowPass.UseVisualStyleBackColor = true;
            this.chkShowPass.CheckedChanged += new System.EventHandler(this.chkShowPass_CheckedChanged);
            // 
            // lblFCol
            // 
            this.lblFCol.AutoSize = true;
            this.lblFCol.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFCol.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFCol.ForeColor = System.Drawing.Color.Blue;
            this.lblFCol.Location = new System.Drawing.Point(13, 257);
            this.lblFCol.Name = "lblFCol";
            this.lblFCol.Size = new System.Drawing.Size(140, 17);
            this.lblFCol.TabIndex = 7;
            this.lblFCol.Text = "[ Collection Message ]";
            this.lblFCol.Click += new System.EventHandler(this.lblFCol_Click);
            // 
            // lblFSto
            // 
            this.lblFSto.AutoSize = true;
            this.lblFSto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFSto.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFSto.ForeColor = System.Drawing.Color.Blue;
            this.lblFSto.Location = new System.Drawing.Point(159, 257);
            this.lblFSto.Name = "lblFSto";
            this.lblFSto.Size = new System.Drawing.Size(114, 17);
            this.lblFSto.TabIndex = 8;
            this.lblFSto.Text = "[ Stock Message ]";
            this.lblFSto.Click += new System.EventHandler(this.lblFSto_Click);
            // 
            // lblFDes
            // 
            this.lblFDes.AutoSize = true;
            this.lblFDes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFDes.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFDes.ForeColor = System.Drawing.Color.Blue;
            this.lblFDes.Location = new System.Drawing.Point(279, 257);
            this.lblFDes.Name = "lblFDes";
            this.lblFDes.Size = new System.Drawing.Size(137, 17);
            this.lblFDes.TabIndex = 9;
            this.lblFDes.Text = "[ Despatch Message ]";
            this.lblFDes.Click += new System.EventHandler(this.lblFDes_Click);
            // 
            // lblFReq
            // 
            this.lblFReq.AutoSize = true;
            this.lblFReq.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblFReq.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFReq.ForeColor = System.Drawing.Color.Blue;
            this.lblFReq.Location = new System.Drawing.Point(13, 289);
            this.lblFReq.Name = "lblFReq";
            this.lblFReq.Size = new System.Drawing.Size(169, 17);
            this.lblFReq.TabIndex = 10;
            this.lblFReq.Text = "[ Request Order Message ]";
            this.lblFReq.Click += new System.EventHandler(this.lblFReq_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 325);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 29);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(333, 325);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(81, 29);
            this.btnCancel.TabIndex = 12;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtFooterMSG
            // 
            this.txtFooterMSG.Location = new System.Drawing.Point(16, 204);
            this.txtFooterMSG.Name = "txtFooterMSG";
            this.txtFooterMSG.Size = new System.Drawing.Size(398, 25);
            this.txtFooterMSG.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 184);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Footer message:";
            // 
            // MailSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(426, 366);
            this.Controls.Add(this.txtFooterMSG);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblFReq);
            this.Controls.Add(this.lblFDes);
            this.Controls.Add(this.lblFSto);
            this.Controls.Add(this.lblFCol);
            this.Controls.Add(this.chkShowPass);
            this.Controls.Add(this.txtGmailPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtGmailUser);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMyEmail);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MailSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email Settings";
            this.Load += new System.EventHandler(this.MailSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMyEmail;
        private System.Windows.Forms.TextBox txtGmailUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGmailPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkShowPass;
        private System.Windows.Forms.Label lblFCol;
        private System.Windows.Forms.Label lblFSto;
        private System.Windows.Forms.Label lblFDes;
        private System.Windows.Forms.Label lblFReq;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtFooterMSG;
        private System.Windows.Forms.Label label4;
    }
}