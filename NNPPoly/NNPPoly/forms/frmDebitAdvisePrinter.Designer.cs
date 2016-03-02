namespace NNPPoly.forms
{
    partial class frmDebitAdvisePrinter
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
            this.pb = new System.Windows.Forms.ProgressBar();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(28, 90);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(490, 36);
            this.pb.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb.TabIndex = 4;
            // 
            // lblProcess
            // 
            this.lblProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProcess.AutoSize = true;
            this.lblProcess.Location = new System.Drawing.Point(493, 21);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(25, 18);
            this.lblProcess.TabIndex = 2;
            this.lblProcess.Text = "[0]";
            this.lblProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(25, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(154, 18);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Printing debit notes...";
            // 
            // frmDebitAdvisePrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(543, 147);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.lblProcess);
            this.Controls.Add(this.lblStatus);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDebitAdvisePrinter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Debit Advises";
            this.Load += new System.EventHandler(this.frmDebitAdvisePrinter_Load);
            this.Shown += new System.EventHandler(this.frmDebitAdvisePrinter_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Label lblStatus;
    }
}

