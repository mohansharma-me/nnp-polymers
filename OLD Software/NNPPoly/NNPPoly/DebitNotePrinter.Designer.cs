namespace NNPPoly
{
    partial class DebitNotePrinter
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
            this.lblPrintingStatus = new System.Windows.Forms.Label();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDebitNotes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPrintingStatus
            // 
            this.lblPrintingStatus.AutoSize = true;
            this.lblPrintingStatus.Location = new System.Drawing.Point(12, 16);
            this.lblPrintingStatus.Name = "lblPrintingStatus";
            this.lblPrintingStatus.Size = new System.Drawing.Size(160, 20);
            this.lblPrintingStatus.TabIndex = 0;
            this.lblPrintingStatus.Text = "Printing debit notes ...";
            // 
            // pb
            // 
            this.pb.Location = new System.Drawing.Point(16, 52);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(365, 23);
            this.pb.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pb.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(387, 50);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 27);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDebitNotes
            // 
            this.lblDebitNotes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDebitNotes.Location = new System.Drawing.Point(299, 16);
            this.lblDebitNotes.Name = "lblDebitNotes";
            this.lblDebitNotes.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDebitNotes.Size = new System.Drawing.Size(160, 20);
            this.lblDebitNotes.TabIndex = 3;
            this.lblDebitNotes.Text = "-/-";
            this.lblDebitNotes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // DebitNotePrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(472, 90);
            this.ControlBox = false;
            this.Controls.Add(this.lblDebitNotes);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pb);
            this.Controls.Add(this.lblPrintingStatus);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DebitNotePrinter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Debit notes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebitNotePrinter_FormClosing);
            this.Load += new System.EventHandler(this.DebitNotePrinter_Load);
            this.Shown += new System.EventHandler(this.DebitNotePrinter_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPrintingStatus;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDebitNotes;
    }
}