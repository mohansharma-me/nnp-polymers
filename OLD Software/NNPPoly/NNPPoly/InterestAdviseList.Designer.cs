namespace NNPPoly
{
    partial class InterestAdviseList
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
            this.panel = new System.Windows.Forms.Panel();
            this.chkZeroInt = new System.Windows.Forms.CheckBox();
            this.chkZeroAmt = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(13, 13);
            this.lv.MultiSelect = false;
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(814, 473);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Client Name";
            this.columnHeader1.Width = 311;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Opening Balance";
            this.columnHeader2.Width = 158;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Closing Balance";
            this.columnHeader3.Width = 131;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Int Amt";
            this.columnHeader4.Width = 205;
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.DarkRed;
            this.panel.Location = new System.Drawing.Point(617, 37);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(200, 100);
            this.panel.TabIndex = 1;
            this.panel.Visible = false;
            // 
            // chkZeroInt
            // 
            this.chkZeroInt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkZeroInt.AutoSize = true;
            this.chkZeroInt.Location = new System.Drawing.Point(13, 497);
            this.chkZeroInt.Name = "chkZeroInt";
            this.chkZeroInt.Size = new System.Drawing.Size(163, 24);
            this.chkZeroInt.TabIndex = 2;
            this.chkZeroInt.Text = "Ignore Zero Int Amt";
            this.chkZeroInt.UseVisualStyleBackColor = true;
            this.chkZeroInt.CheckedChanged += new System.EventHandler(this.chkZeroInt_CheckedChanged);
            // 
            // chkZeroAmt
            // 
            this.chkZeroAmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkZeroAmt.AutoSize = true;
            this.chkZeroAmt.Location = new System.Drawing.Point(199, 497);
            this.chkZeroAmt.Name = "chkZeroAmt";
            this.chkZeroAmt.Size = new System.Drawing.Size(174, 24);
            this.chkZeroAmt.TabIndex = 2;
            this.chkZeroAmt.Text = "Ignore Zero Clos. Bal.";
            this.chkZeroAmt.UseVisualStyleBackColor = true;
            this.chkZeroAmt.CheckedChanged += new System.EventHandler(this.chkZeroAmt_CheckedChanged);
            // 
            // InterestAdviseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(839, 540);
            this.Controls.Add(this.chkZeroAmt);
            this.Controls.Add(this.chkZeroInt);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InterestAdviseList";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Interest Advise List";
            this.Load += new System.EventHandler(this.InterestAdviseList_Load);
            this.Shown += new System.EventHandler(this.InterestAdviseList_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.CheckBox chkZeroInt;
        private System.Windows.Forms.CheckBox chkZeroAmt;
    }
}