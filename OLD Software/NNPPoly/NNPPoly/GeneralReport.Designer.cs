namespace NNPPoly
{
    partial class GeneralReport
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
            this.lvUser = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnProcess = new System.Windows.Forms.Button();
            this.panHtml = new HtmlRenderer.HtmlPanel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lvUser
            // 
            this.lvUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUser.BackColor = System.Drawing.SystemColors.Control;
            this.lvUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader16,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.lvUser.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvUser.ForeColor = System.Drawing.Color.Black;
            this.lvUser.FullRowSelect = true;
            this.lvUser.GridLines = true;
            this.lvUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUser.Location = new System.Drawing.Point(2, 9);
            this.lvUser.MultiSelect = false;
            this.lvUser.Name = "lvUser";
            this.lvUser.OwnerDraw = true;
            this.lvUser.Size = new System.Drawing.Size(626, 416);
            this.lvUser.TabIndex = 0;
            this.lvUser.UseCompatibleStateImageBehavior = false;
            this.lvUser.View = System.Windows.Forms.View.Details;
            this.lvUser.Visible = false;
            this.lvUser.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.lv_DrawColumnHeader);
            this.lvUser.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.lv_DrawItem);
            this.lvUser.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.lv_DrawSubItem);
            this.lvUser.DoubleClick += new System.EventHandler(this.lvUser_DoubleClick);
            this.lvUser.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvUser_MouseClick);
            this.lvUser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvUser_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "New Date";
            this.columnHeader1.Width = 93;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Last Date";
            this.columnHeader2.Width = 95;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Debit Date";
            this.columnHeader3.Width = 103;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Invoice no";
            this.columnHeader4.Width = 132;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "MT";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Debit AMT";
            this.columnHeader6.Width = 116;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Credit AMT";
            this.columnHeader7.Width = 127;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Credit taken";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "CH No";
            this.columnHeader8.Width = 84;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Credit Date";
            this.columnHeader9.Width = 100;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Total days";
            this.columnHeader10.Width = 89;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Less days";
            this.columnHeader11.Width = 84;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Due days";
            this.columnHeader12.Width = 91;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "INT AMT @21%";
            this.columnHeader13.Width = 133;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "CD";
            this.columnHeader14.Width = 66;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "ESP";
            this.columnHeader15.Width = 69;
            // 
            // btnProcess
            // 
            this.btnProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnProcess.Location = new System.Drawing.Point(542, 9);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(96, 29);
            this.btnProcess.TabIndex = 5;
            this.btnProcess.Text = "Process";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Visible = false;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // panHtml
            // 
            this.panHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panHtml.AutoScroll = true;
            this.panHtml.BackColor = System.Drawing.Color.Transparent;
            this.panHtml.BaseStylesheet = null;
            this.panHtml.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.panHtml.IsContextMenuEnabled = false;
            this.panHtml.Location = new System.Drawing.Point(2, 12);
            this.panHtml.Name = "panHtml";
            this.panHtml.Size = new System.Drawing.Size(626, 415);
            this.panHtml.TabIndex = 6;
            this.panHtml.Text = null;
            this.panHtml.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panHtml_MouseClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(623, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // GeneralReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(650, 437);
            this.Controls.Add(this.lvUser);
            this.Controls.Add(this.panHtml);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnProcess);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GeneralReport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "General Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeneralReport_FormClosed);
            this.Load += new System.EventHandler(this.GeneralReport_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvUser;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private HtmlRenderer.HtmlPanel panHtml;
        private System.Windows.Forms.CheckBox checkBox1;





    }
}