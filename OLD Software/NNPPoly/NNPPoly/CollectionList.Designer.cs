namespace NNPPoly
{
    partial class CollectionList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CollectionList));
            this.lv = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lvUser = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblLastDateWas = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblCollectingAmount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblClientName = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDebitDetails = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvClients = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSClient = new System.Windows.Forms.TextBox();
            this.txtSPays = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnPrintCollecionList = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lv
            // 
            this.lv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader22});
            this.lv.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lv.FullRowSelect = true;
            this.lv.GridLines = true;
            this.lv.Location = new System.Drawing.Point(240, 12);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(356, 423);
            this.lv.TabIndex = 0;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
            this.lv.SelectedIndexChanged += new System.EventHandler(this.lv_SelectedIndexChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Debit Details";
            this.columnHeader2.Width = 96;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Debit Date";
            this.columnHeader3.Width = 98;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Amount";
            this.columnHeader4.Width = 96;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Days";
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(357, 113);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(191, 143);
            this.panel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvUser);
            this.groupBox1.Controls.Add(this.lblLastDateWas);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblCollectingAmount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lblClientName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtDebitDetails);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(602, 1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 465);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // lvUser
            // 
            this.lvUser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUser.BackColor = System.Drawing.SystemColors.Control;
            this.lvUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lvUser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader16,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20});
            this.lvUser.Font = new System.Drawing.Font("Segoe UI Semibold", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvUser.ForeColor = System.Drawing.Color.Black;
            this.lvUser.FullRowSelect = true;
            this.lvUser.GridLines = true;
            this.lvUser.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUser.Location = new System.Drawing.Point(19, 360);
            this.lvUser.MultiSelect = false;
            this.lvUser.Name = "lvUser";
            this.lvUser.Size = new System.Drawing.Size(289, 99);
            this.lvUser.TabIndex = 8;
            this.lvUser.UseCompatibleStateImageBehavior = false;
            this.lvUser.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "New Date";
            this.columnHeader5.Width = 93;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Last Date";
            this.columnHeader6.Width = 95;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Debit Date";
            this.columnHeader7.Width = 103;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Invoice no";
            this.columnHeader8.Width = 132;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "MT";
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Debit AMT";
            this.columnHeader10.Width = 116;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Credit AMT";
            this.columnHeader11.Width = 127;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Credit taken";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "CH No";
            this.columnHeader12.Width = 84;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Credit Date";
            this.columnHeader13.Width = 100;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Total days";
            this.columnHeader14.Width = 89;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Less days";
            this.columnHeader15.Width = 84;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Due days";
            this.columnHeader17.Width = 91;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "INT AMT @21%";
            this.columnHeader18.Width = 133;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "CD";
            this.columnHeader19.Width = 66;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "ESP";
            // 
            // lblLastDateWas
            // 
            this.lblLastDateWas.AutoSize = true;
            this.lblLastDateWas.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastDateWas.Location = new System.Drawing.Point(34, 328);
            this.lblLastDateWas.Name = "lblLastDateWas";
            this.lblLastDateWas.Size = new System.Drawing.Size(21, 19);
            this.lblLastDateWas.TabIndex = 7;
            this.lblLastDateWas.Text = "--";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 307);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 21);
            this.label5.TabIndex = 6;
            this.label5.Text = "Last date was :";
            // 
            // lblCollectingAmount
            // 
            this.lblCollectingAmount.AutoSize = true;
            this.lblCollectingAmount.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCollectingAmount.Location = new System.Drawing.Point(34, 274);
            this.lblCollectingAmount.Name = "lblCollectingAmount";
            this.lblCollectingAmount.Size = new System.Drawing.Size(21, 19);
            this.lblCollectingAmount.TabIndex = 5;
            this.lblCollectingAmount.Text = "--";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 253);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 21);
            this.label4.TabIndex = 4;
            this.label4.Text = "Collecting amount :";
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClientName.Location = new System.Drawing.Point(34, 221);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(21, 19);
            this.lblClientName.TabIndex = 3;
            this.lblClientName.Text = "--";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 200);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Client name :";
            // 
            // txtDebitDetails
            // 
            this.txtDebitDetails.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebitDetails.Location = new System.Drawing.Point(19, 49);
            this.txtDebitDetails.Multiline = true;
            this.txtDebitDetails.Name = "txtDebitDetails";
            this.txtDebitDetails.Size = new System.Drawing.Size(289, 134);
            this.txtDebitDetails.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Debit details ";
            // 
            // lvClients
            // 
            this.lvClients.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvClients.CheckBoxes = true;
            this.lvClients.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader21});
            this.lvClients.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvClients.FullRowSelect = true;
            this.lvClients.GridLines = true;
            this.lvClients.Location = new System.Drawing.Point(12, 12);
            this.lvClients.MultiSelect = false;
            this.lvClients.Name = "lvClients";
            this.lvClients.Size = new System.Drawing.Size(222, 375);
            this.lvClients.TabIndex = 3;
            this.lvClients.UseCompatibleStateImageBehavior = false;
            this.lvClients.View = System.Windows.Forms.View.Details;
            this.lvClients.SelectedIndexChanged += new System.EventHandler(this.lvClients_SelectedIndexChanged);
            this.lvClients.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvClients_KeyDown);
            this.lvClients.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvClients_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Client name";
            this.columnHeader1.Width = 112;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Total Amount";
            this.columnHeader21.Width = 100;
            // 
            // txtSClient
            // 
            this.txtSClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSClient.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSClient.Location = new System.Drawing.Point(12, 441);
            this.txtSClient.Name = "txtSClient";
            this.txtSClient.Size = new System.Drawing.Size(222, 25);
            this.txtSClient.TabIndex = 4;
            this.txtSClient.Text = "Filter clients";
            this.txtSClient.TextChanged += new System.EventHandler(this.txtSClient_TextChanged);
            // 
            // txtSPays
            // 
            this.txtSPays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSPays.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSPays.Location = new System.Drawing.Point(240, 441);
            this.txtSPays.Name = "txtSPays";
            this.txtSPays.Size = new System.Drawing.Size(356, 25);
            this.txtSPays.TabIndex = 5;
            this.txtSPays.Text = "Filter entries";
            this.txtSPays.TextChanged += new System.EventHandler(this.txtSPays_TextChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Location = new System.Drawing.Point(654, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 46);
            this.label3.TabIndex = 6;
            this.label3.Text = "Please select client and then select debit entry.";
            // 
            // btnPrintCollecionList
            // 
            this.btnPrintCollecionList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrintCollecionList.Location = new System.Drawing.Point(12, 393);
            this.btnPrintCollecionList.Name = "btnPrintCollecionList";
            this.btnPrintCollecionList.Size = new System.Drawing.Size(222, 42);
            this.btnPrintCollecionList.TabIndex = 7;
            this.btnPrintCollecionList.Text = "&Print Coll. List";
            this.btnPrintCollecionList.UseVisualStyleBackColor = true;
            this.btnPrintCollecionList.Click += new System.EventHandler(this.btnPrintCollecionList_Click);
            // 
            // CollectionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(943, 478);
            this.Controls.Add(this.btnPrintCollecionList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtSPays);
            this.Controls.Add(this.txtSClient);
            this.Controls.Add(this.lvClients);
            this.Controls.Add(this.lv);
            this.Controls.Add(this.panel);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CollectionList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Collection List";
            this.Load += new System.EventHandler(this.CollectionList_Load);
            this.Shown += new System.EventHandler(this.CollectionList_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDebitDetails;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label lblCollectingAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblLastDateWas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView lvUser;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ListView lvClients;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.TextBox txtSClient;
        private System.Windows.Forms.TextBox txtSPays;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.Button btnPrintCollecionList;
    }
}