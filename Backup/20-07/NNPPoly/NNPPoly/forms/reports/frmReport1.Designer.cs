namespace NNPPoly.forms.reports
{
    partial class frmReport1
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
            this.panProcess = new System.Windows.Forms.Panel();
            this.lblProcess = new System.Windows.Forms.Label();
            this.lcProcess = new MyGUI.Preloader.LoadingCircle();
            this.panSimpleView = new System.Windows.Forms.Panel();
            this.lvSimpleView = new BrightIdeasSoftware.ObjectListView();
            this.panHTMLView = new System.Windows.Forms.Panel();
            this.htmlViewer = new HtmlRenderer.HtmlPanel();
            this.panProcess.SuspendLayout();
            this.panSimpleView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvSimpleView)).BeginInit();
            this.panHTMLView.SuspendLayout();
            this.SuspendLayout();
            // 
            // panProcess
            // 
            this.panProcess.BackColor = System.Drawing.Color.White;
            this.panProcess.Controls.Add(this.lblProcess);
            this.panProcess.Controls.Add(this.lcProcess);
            this.panProcess.Location = new System.Drawing.Point(460, 12);
            this.panProcess.Name = "panProcess";
            this.panProcess.Size = new System.Drawing.Size(150, 101);
            this.panProcess.TabIndex = 0;
            this.panProcess.Visible = false;
            // 
            // lblProcess
            // 
            this.lblProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProcess.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcess.Location = new System.Drawing.Point(-224, 49);
            this.lblProcess.Name = "lblProcess";
            this.lblProcess.Size = new System.Drawing.Size(598, 62);
            this.lblProcess.TabIndex = 1;
            this.lblProcess.Text = "...";
            this.lblProcess.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lcProcess
            // 
            this.lcProcess.Active = false;
            this.lcProcess.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lcProcess.Color = System.Drawing.Color.Black;
            this.lcProcess.InnerCircleRadius = 5;
            this.lcProcess.Location = new System.Drawing.Point(39, -23);
            this.lcProcess.Name = "lcProcess";
            this.lcProcess.NumberSpoke = 12;
            this.lcProcess.OuterCircleRadius = 11;
            this.lcProcess.RotationSpeed = 100;
            this.lcProcess.Size = new System.Drawing.Size(68, 69);
            this.lcProcess.SpokeThickness = 2;
            this.lcProcess.StylePreset = MyGUI.Preloader.LoadingCircle.StylePresets.MacOSX;
            this.lcProcess.TabIndex = 0;
            this.lcProcess.Text = "loadingCircle1";
            // 
            // panSimpleView
            // 
            this.panSimpleView.Controls.Add(this.lvSimpleView);
            this.panSimpleView.Location = new System.Drawing.Point(308, 12);
            this.panSimpleView.Name = "panSimpleView";
            this.panSimpleView.Size = new System.Drawing.Size(146, 101);
            this.panSimpleView.TabIndex = 1;
            this.panSimpleView.Visible = false;
            // 
            // lvSimpleView
            // 
            this.lvSimpleView.AllowColumnReorder = true;
            this.lvSimpleView.AlternateRowBackColor = System.Drawing.Color.Gainsboro;
            this.lvSimpleView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvSimpleView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.lvSimpleView.CellEditTabChangesRows = true;
            this.lvSimpleView.Cursor = System.Windows.Forms.Cursors.Default;
            this.lvSimpleView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSimpleView.EmptyListMsg = "--: No credit/debit entries :--";
            this.lvSimpleView.EmptyListMsgFont = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvSimpleView.FullRowSelect = true;
            this.lvSimpleView.GridLines = true;
            this.lvSimpleView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvSimpleView.HeaderWordWrap = true;
            this.lvSimpleView.HighlightBackgroundColor = System.Drawing.Color.OrangeRed;
            this.lvSimpleView.HighlightForegroundColor = System.Drawing.Color.White;
            this.lvSimpleView.IncludeColumnHeadersInCopy = true;
            this.lvSimpleView.IsSearchOnSortColumn = false;
            this.lvSimpleView.Location = new System.Drawing.Point(0, 0);
            this.lvSimpleView.Name = "lvSimpleView";
            this.lvSimpleView.OverlayText.Text = "";
            this.lvSimpleView.RenderNonEditableCheckboxesAsDisabled = true;
            this.lvSimpleView.SelectedColumnTint = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))));
            this.lvSimpleView.ShowCommandMenuOnRightClick = true;
            this.lvSimpleView.ShowGroups = false;
            this.lvSimpleView.ShowItemToolTips = true;
            this.lvSimpleView.Size = new System.Drawing.Size(146, 101);
            this.lvSimpleView.TabIndex = 3;
            this.lvSimpleView.TintSortColumn = true;
            this.lvSimpleView.UnfocusedHighlightBackgroundColor = System.Drawing.Color.SteelBlue;
            this.lvSimpleView.UnfocusedHighlightForegroundColor = System.Drawing.Color.White;
            this.lvSimpleView.UseCompatibleStateImageBehavior = false;
            this.lvSimpleView.UseCustomSelectionColors = true;
            this.lvSimpleView.UseExplorerTheme = true;
            this.lvSimpleView.UseFilterIndicator = true;
            this.lvSimpleView.UseFiltering = true;
            this.lvSimpleView.UseHotItem = true;
            this.lvSimpleView.UseHyperlinks = true;
            this.lvSimpleView.View = System.Windows.Forms.View.Details;
            this.lvSimpleView.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.lvSimpleView_CellEditFinishing);
            this.lvSimpleView.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.lvSimpleView_CellEditStarting);
            this.lvSimpleView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lvSimpleView_MouseDown);
            // 
            // panHTMLView
            // 
            this.panHTMLView.BackColor = System.Drawing.Color.White;
            this.panHTMLView.Controls.Add(this.htmlViewer);
            this.panHTMLView.Location = new System.Drawing.Point(140, 12);
            this.panHTMLView.Name = "panHTMLView";
            this.panHTMLView.Size = new System.Drawing.Size(162, 101);
            this.panHTMLView.TabIndex = 2;
            this.panHTMLView.Visible = false;
            // 
            // htmlViewer
            // 
            this.htmlViewer.AutoScroll = true;
            this.htmlViewer.BackColor = System.Drawing.Color.Transparent;
            this.htmlViewer.BaseStylesheet = null;
            this.htmlViewer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlViewer.IsContextMenuEnabled = false;
            this.htmlViewer.Location = new System.Drawing.Point(0, 0);
            this.htmlViewer.Name = "htmlViewer";
            this.htmlViewer.Size = new System.Drawing.Size(162, 101);
            this.htmlViewer.TabIndex = 7;
            this.htmlViewer.Text = null;
            // 
            // frmReport1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(622, 488);
            this.Controls.Add(this.panHTMLView);
            this.Controls.Add(this.panSimpleView);
            this.Controls.Add(this.panProcess);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmReport1";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monthly Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmReport1_Load);
            this.Shown += new System.EventHandler(this.frmReport1_Shown);
            this.panProcess.ResumeLayout(false);
            this.panSimpleView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lvSimpleView)).EndInit();
            this.panHTMLView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panProcess;
        private MyGUI.Preloader.LoadingCircle lcProcess;
        private System.Windows.Forms.Label lblProcess;
        private System.Windows.Forms.Panel panSimpleView;
        public BrightIdeasSoftware.ObjectListView lvSimpleView;
        private System.Windows.Forms.Panel panHTMLView;
        public HtmlRenderer.HtmlPanel htmlViewer;


    }
}

