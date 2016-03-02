namespace NNPPoly
{
    partial class frmProcess
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.pbProcess = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lcProcess = new MyGUI.Preloader.LoadingCircle();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(8, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(61, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "label1";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.Location = new System.Drawing.Point(12, 53);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(508, 103);
            this.lblMsg.TabIndex = 0;
            this.lblMsg.Text = "label1";
            // 
            // pbProcess
            // 
            this.pbProcess.Location = new System.Drawing.Point(12, 159);
            this.pbProcess.Name = "pbProcess";
            this.pbProcess.Size = new System.Drawing.Size(508, 23);
            this.pbProcess.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(534, 27);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 14);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lcProcess
            // 
            this.lcProcess.Active = false;
            this.lcProcess.Color = System.Drawing.Color.White;
            this.lcProcess.InnerCircleRadius = 5;
            this.lcProcess.Location = new System.Drawing.Point(233, 159);
            this.lcProcess.Name = "lcProcess";
            this.lcProcess.NumberSpoke = 12;
            this.lcProcess.OuterCircleRadius = 11;
            this.lcProcess.RotationSpeed = 100;
            this.lcProcess.Size = new System.Drawing.Size(53, 23);
            this.lcProcess.SpokeThickness = 2;
            this.lcProcess.StylePreset = MyGUI.Preloader.LoadingCircle.StylePresets.MacOSX;
            this.lcProcess.TabIndex = 1;
            this.lcProcess.Text = "loadingCircle1";
            // 
            // frmProcess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(532, 194);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lcProcess);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pbProcess);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProcess";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processing...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmProcess_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmProcess_FormClosed);
            this.Load += new System.EventHandler(this.frmProcess_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblTitle;
        public System.Windows.Forms.Label lblMsg;
        public MyGUI.Preloader.LoadingCircle lcProcess;
        public System.Windows.Forms.ProgressBar pbProcess;
        private System.Windows.Forms.Button btnCancel;

    }
}

