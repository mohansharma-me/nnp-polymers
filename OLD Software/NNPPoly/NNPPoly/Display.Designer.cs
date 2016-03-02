﻿namespace NNPPoly
{
    partial class Display
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
            this.panel = new System.Windows.Forms.Panel();
            this.pb = new System.Windows.Forms.ProgressBar();
            this.lv = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.SystemColors.Control;
            this.panel.Controls.Add(this.pb);
            this.panel.Controls.Add(this.lv);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(737, 513);
            this.panel.TabIndex = 0;
            // 
            // pb
            // 
            this.pb.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pb.Location = new System.Drawing.Point(241, 235);
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(281, 23);
            this.pb.Step = 1;
            this.pb.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb.TabIndex = 1;
            this.pb.Visible = false;
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
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader9,
            this.columnHeader6,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12});
            this.lv.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(12, 12);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(713, 489);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.@__SortOnClick_ListView_onColumnClick);
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            this.lv.DoubleClick += new System.EventHandler(this.lv_DoubleClick);
            this.lv.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lv_KeyDown);
            this.lv.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_MouseClick);
            this.lv.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lv_MouseDown);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 33;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Date";
            this.columnHeader2.Width = 45;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Invoice/Doc/Chq No.";
            this.columnHeader3.Width = 170;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Type";
            this.columnHeader4.Width = 45;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Particulers";
            this.columnHeader5.Width = 79;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "M.T.";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Grade";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Credit (C)";
            this.columnHeader10.Width = 100;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Debit (D)";
            this.columnHeader11.Width = 84;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Closing balance";
            // 
            // Display
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(737, 513);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Display";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Display";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ProgressBar pb;
        private System.Windows.Forms.ColumnHeader columnHeader6;
    }
}