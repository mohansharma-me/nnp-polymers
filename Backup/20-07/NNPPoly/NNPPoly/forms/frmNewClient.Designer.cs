namespace NNPPoly.forms
{
    partial class frmNewClient
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewClient));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtAbout = new System.Windows.Forms.RichTextBox();
            this.btnLink = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtReportFooter = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbOBType = new System.Windows.Forms.ComboBox();
            this.txtIntRate2 = new System.Windows.Forms.TextBox();
            this.txtLessDays = new System.Windows.Forms.TextBox();
            this.txtCutoffDays = new System.Windows.Forms.TextBox();
            this.txtIntRate1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOBal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmitNext = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAbout);
            this.groupBox1.Controls.Add(this.btnLink);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.txtReportFooter);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtMobile);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(717, 313);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Client information";
            // 
            // txtAbout
            // 
            this.txtAbout.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAbout.Location = new System.Drawing.Point(201, 147);
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.Size = new System.Drawing.Size(349, 120);
            this.txtAbout.TabIndex = 3;
            this.txtAbout.Text = "";
            this.txtAbout.WordWrap = false;
            this.txtAbout.TextChanged += new System.EventHandler(this.txtAbout_TextChanged);
            this.txtAbout.Enter += new System.EventHandler(this.txtAbout_Enter);
            this.txtAbout.Leave += new System.EventHandler(this.txtAbout_Leave);
            // 
            // btnLink
            // 
            this.btnLink.ImageIndex = 0;
            this.btnLink.ImageList = this.imageList1;
            this.btnLink.Location = new System.Drawing.Point(556, 35);
            this.btnLink.Name = "btnLink";
            this.btnLink.Size = new System.Drawing.Size(158, 175);
            this.btnLink.TabIndex = 4;
            this.btnLink.UseVisualStyleBackColor = true;
            this.btnLink.Visible = false;
            this.btnLink.Click += new System.EventHandler(this.btnLink_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "magnifier_plus_blue.png");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::NNPPoly.Properties.Resources.adduser;
            this.pictureBox1.Location = new System.Drawing.Point(556, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(155, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtReportFooter
            // 
            this.txtReportFooter.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReportFooter.Location = new System.Drawing.Point(201, 273);
            this.txtReportFooter.Name = "txtReportFooter";
            this.txtReportFooter.Size = new System.Drawing.Size(492, 30);
            this.txtReportFooter.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(201, 107);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(349, 30);
            this.txtEmail.TabIndex = 2;
            this.txtEmail.TextChanged += new System.EventHandler(this.TB_ValidateMultipleEmails);
            // 
            // txtMobile
            // 
            this.txtMobile.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMobile.Location = new System.Drawing.Point(201, 71);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(349, 30);
            this.txtMobile.TabIndex = 1;
            this.txtMobile.TextChanged += new System.EventHandler(this.TB_ValidateMultipleLong);
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(201, 35);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(349, 30);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.TB_ValidateString);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Address :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 280);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Report footer text :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "E-mail :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Mobile :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbOBType);
            this.groupBox2.Controls.Add(this.txtIntRate2);
            this.groupBox2.Controls.Add(this.txtLessDays);
            this.groupBox2.Controls.Add(this.txtCutoffDays);
            this.groupBox2.Controls.Add(this.txtIntRate1);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtOBal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(12, 331);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(717, 172);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client parameters";
            // 
            // cmbOBType
            // 
            this.cmbOBType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOBType.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOBType.FormattingEnabled = true;
            this.cmbOBType.Location = new System.Drawing.Point(201, 41);
            this.cmbOBType.Name = "cmbOBType";
            this.cmbOBType.Size = new System.Drawing.Size(121, 30);
            this.cmbOBType.TabIndex = 5;
            // 
            // txtIntRate2
            // 
            this.txtIntRate2.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntRate2.Location = new System.Drawing.Point(446, 82);
            this.txtIntRate2.Name = "txtIntRate2";
            this.txtIntRate2.Size = new System.Drawing.Size(247, 30);
            this.txtIntRate2.TabIndex = 8;
            this.txtIntRate2.Text = "24";
            this.txtIntRate2.TextChanged += new System.EventHandler(this.TB_ValidateDouble);
            // 
            // txtLessDays
            // 
            this.txtLessDays.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLessDays.Location = new System.Drawing.Point(508, 118);
            this.txtLessDays.Name = "txtLessDays";
            this.txtLessDays.Size = new System.Drawing.Size(185, 30);
            this.txtLessDays.TabIndex = 10;
            this.txtLessDays.Text = "10";
            this.txtLessDays.TextChanged += new System.EventHandler(this.TB_ValidateLong);
            // 
            // txtCutoffDays
            // 
            this.txtCutoffDays.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCutoffDays.Location = new System.Drawing.Point(201, 118);
            this.txtCutoffDays.Name = "txtCutoffDays";
            this.txtCutoffDays.Size = new System.Drawing.Size(205, 30);
            this.txtCutoffDays.TabIndex = 9;
            this.txtCutoffDays.Text = "20";
            this.txtCutoffDays.TextChanged += new System.EventHandler(this.TB_ValidateLong);
            // 
            // txtIntRate1
            // 
            this.txtIntRate1.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIntRate1.Location = new System.Drawing.Point(201, 82);
            this.txtIntRate1.Name = "txtIntRate1";
            this.txtIntRate1.Size = new System.Drawing.Size(239, 30);
            this.txtIntRate1.TabIndex = 7;
            this.txtIntRate1.Text = "20";
            this.txtIntRate1.TextChanged += new System.EventHandler(this.TB_ValidateDouble);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(412, 125);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Less Days :";
            // 
            // txtOBal
            // 
            this.txtOBal.Font = new System.Drawing.Font("Arial", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOBal.Location = new System.Drawing.Point(330, 41);
            this.txtOBal.Name = "txtOBal";
            this.txtOBal.Size = new System.Drawing.Size(363, 30);
            this.txtOBal.TabIndex = 6;
            this.txtOBal.Text = "0.00";
            this.txtOBal.TextChanged += new System.EventHandler(this.TB_ValidateDouble);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(93, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Cutoff Days:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "Interest Rate(s) :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "Opening Balance :";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(12, 509);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(98, 37);
            this.btnSubmit.TabIndex = 11;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(631, 509);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(98, 37);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmitNext
            // 
            this.btnSubmitNext.Location = new System.Drawing.Point(116, 509);
            this.btnSubmitNext.Name = "btnSubmitNext";
            this.btnSubmitNext.Size = new System.Drawing.Size(145, 37);
            this.btnSubmitNext.TabIndex = 12;
            this.btnSubmitNext.Text = "&Submit && Next";
            this.btnSubmitNext.UseVisualStyleBackColor = true;
            this.btnSubmitNext.Click += new System.EventHandler(this.btnSubmitNext_Click);
            // 
            // frmNewClient
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(741, 558);
            this.Controls.Add(this.btnSubmitNext);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewClient";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNewClient_FormClosing);
            this.Load += new System.EventHandler(this.frmNewClient_Load);
            this.Shown += new System.EventHandler(this.frmNewClient_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmitNext;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtReportFooter;
        public System.Windows.Forms.TextBox txtEmail;
        public System.Windows.Forms.TextBox txtMobile;
        public System.Windows.Forms.TextBox txtOBal;
        public System.Windows.Forms.ComboBox cmbOBType;
        public System.Windows.Forms.TextBox txtIntRate1;
        public System.Windows.Forms.TextBox txtIntRate2;
        public System.Windows.Forms.TextBox txtCutoffDays;
        public System.Windows.Forms.TextBox txtLessDays;
        private System.Windows.Forms.ImageList imageList1;
        public System.Windows.Forms.Button btnLink;
        private System.Windows.Forms.RichTextBox txtAbout;
    }
}

