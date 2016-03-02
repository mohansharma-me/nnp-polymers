namespace NNPPoly
{
    partial class SendCustomMessage
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
            this.btnSendSMS = new System.Windows.Forms.Button();
            this.txtSMS = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMobiles = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSendMail = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblSelNumbers = new System.Windows.Forms.Label();
            this.lblSelEmails = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSelNumbers);
            this.groupBox1.Controls.Add(this.btnSendSMS);
            this.groupBox1.Controls.Add(this.txtSMS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMobiles);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 326);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "  SMS  ";
            // 
            // btnSendSMS
            // 
            this.btnSendSMS.Location = new System.Drawing.Point(25, 276);
            this.btnSendSMS.Name = "btnSendSMS";
            this.btnSendSMS.Size = new System.Drawing.Size(92, 33);
            this.btnSendSMS.TabIndex = 3;
            this.btnSendSMS.Text = "&Send";
            this.btnSendSMS.UseVisualStyleBackColor = true;
            this.btnSendSMS.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // txtSMS
            // 
            this.txtSMS.Location = new System.Drawing.Point(25, 128);
            this.txtSMS.Multiline = true;
            this.txtSMS.Name = "txtSMS";
            this.txtSMS.Size = new System.Drawing.Size(323, 142);
            this.txtSMS.TabIndex = 2;
            this.txtSMS.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Message:";
            // 
            // txtMobiles
            // 
            this.txtMobiles.Location = new System.Drawing.Point(25, 60);
            this.txtMobiles.Name = "txtMobiles";
            this.txtMobiles.Size = new System.Drawing.Size(323, 27);
            this.txtMobiles.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mobile Numbers :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblSelEmails);
            this.groupBox2.Controls.Add(this.txtSubject);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnSendMail);
            this.groupBox2.Controls.Add(this.txtMessage);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(398, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(371, 326);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "  EMAIL  ";
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(25, 128);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(323, 27);
            this.txtSubject.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Subject:";
            // 
            // btnSendMail
            // 
            this.btnSendMail.Location = new System.Drawing.Point(25, 276);
            this.btnSendMail.Name = "btnSendMail";
            this.btnSendMail.Size = new System.Drawing.Size(92, 33);
            this.btnSendMail.TabIndex = 4;
            this.btnSendMail.Text = "&Send";
            this.btnSendMail.UseVisualStyleBackColor = true;
            this.btnSendMail.Click += new System.EventHandler(this.btnSendMail_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(25, 185);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(323, 85);
            this.txtMessage.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Message:";
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(25, 60);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(323, 27);
            this.txtTo.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "To :";
            // 
            // lblSelNumbers
            // 
            this.lblSelNumbers.AutoSize = true;
            this.lblSelNumbers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSelNumbers.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelNumbers.ForeColor = System.Drawing.Color.Blue;
            this.lblSelNumbers.Location = new System.Drawing.Point(319, 36);
            this.lblSelNumbers.Name = "lblSelNumbers";
            this.lblSelNumbers.Size = new System.Drawing.Size(29, 20);
            this.lblSelNumbers.TabIndex = 4;
            this.lblSelNumbers.Text = "[+]";
            this.lblSelNumbers.Click += new System.EventHandler(this.lblSelNumbers_Click);
            // 
            // lblSelEmails
            // 
            this.lblSelEmails.AutoSize = true;
            this.lblSelEmails.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSelEmails.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelEmails.ForeColor = System.Drawing.Color.Blue;
            this.lblSelEmails.Location = new System.Drawing.Point(319, 36);
            this.lblSelEmails.Name = "lblSelEmails";
            this.lblSelEmails.Size = new System.Drawing.Size(29, 20);
            this.lblSelEmails.TabIndex = 6;
            this.lblSelEmails.Text = "[+]";
            this.lblSelEmails.Click += new System.EventHandler(this.lblSelEmails_Click);
            // 
            // SendCustomMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(784, 351);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendCustomMessage";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Send Custom Message";
            this.Load += new System.EventHandler(this.SendCustomMessage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSMS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMobiles;
        private System.Windows.Forms.Button btnSendSMS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnSendMail;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblSelNumbers;
        private System.Windows.Forms.Label lblSelEmails;

    }
}