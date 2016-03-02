namespace NNPPoly
{
    partial class EditUserAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditUserAccount));
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtAbout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtBalance = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.radDebit = new System.Windows.Forms.RadioButton();
            this.radCredit = new System.Windows.Forms.RadioButton();
            this.txtIntRate1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFooText = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtIntRate2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCutOffDays = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtLessDays = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEmails = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client name : *";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(142, 17);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(265, 29);
            this.txtName.TabIndex = 1;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtAbout
            // 
            this.txtAbout.Location = new System.Drawing.Point(142, 52);
            this.txtAbout.Name = "txtAbout";
            this.txtAbout.Size = new System.Drawing.Size(265, 29);
            this.txtAbout.TabIndex = 2;
            this.txtAbout.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "About client :";
            // 
            // txtBalance
            // 
            this.txtBalance.Location = new System.Drawing.Point(142, 90);
            this.txtBalance.Name = "txtBalance";
            this.txtBalance.Size = new System.Drawing.Size(265, 29);
            this.txtBalance.TabIndex = 3;
            this.txtBalance.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "O. Balance : *";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(25, 449);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 35);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(321, 449);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 35);
            this.btnCancel.TabIndex = 13;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 129);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "O.B. Type : *";
            // 
            // radDebit
            // 
            this.radDebit.AutoSize = true;
            this.radDebit.Location = new System.Drawing.Point(246, 130);
            this.radDebit.Name = "radDebit";
            this.radDebit.Size = new System.Drawing.Size(68, 25);
            this.radDebit.TabIndex = 4;
            this.radDebit.TabStop = true;
            this.radDebit.Text = "Debit";
            this.radDebit.UseVisualStyleBackColor = true;
            // 
            // radCredit
            // 
            this.radCredit.AutoSize = true;
            this.radCredit.Location = new System.Drawing.Point(334, 130);
            this.radCredit.Name = "radCredit";
            this.radCredit.Size = new System.Drawing.Size(73, 25);
            this.radCredit.TabIndex = 4;
            this.radCredit.TabStop = true;
            this.radCredit.Text = "Credit";
            this.radCredit.UseVisualStyleBackColor = true;
            // 
            // txtIntRate1
            // 
            this.txtIntRate1.Location = new System.Drawing.Point(142, 169);
            this.txtIntRate1.Name = "txtIntRate1";
            this.txtIntRate1.Size = new System.Drawing.Size(265, 29);
            this.txtIntRate1.TabIndex = 5;
            this.txtIntRate1.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(33, 172);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 21);
            this.label5.TabIndex = 11;
            this.label5.Text = "Int. Rate 1 : *";
            // 
            // txtFooText
            // 
            this.txtFooText.Location = new System.Drawing.Point(142, 274);
            this.txtFooText.Name = "txtFooText";
            this.txtFooText.Size = new System.Drawing.Size(265, 29);
            this.txtFooText.TabIndex = 8;
            this.txtFooText.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 277);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 21);
            this.label6.TabIndex = 13;
            this.label6.Text = "Foo Text :";
            // 
            // txtIntRate2
            // 
            this.txtIntRate2.Location = new System.Drawing.Point(142, 204);
            this.txtIntRate2.Name = "txtIntRate2";
            this.txtIntRate2.Size = new System.Drawing.Size(265, 29);
            this.txtIntRate2.TabIndex = 6;
            this.txtIntRate2.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 207);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(103, 21);
            this.label7.TabIndex = 15;
            this.label7.Text = "Int. Rate 2 : *";
            // 
            // txtCutOffDays
            // 
            this.txtCutOffDays.Location = new System.Drawing.Point(142, 239);
            this.txtCutOffDays.Name = "txtCutOffDays";
            this.txtCutOffDays.Size = new System.Drawing.Size(265, 29);
            this.txtCutOffDays.TabIndex = 7;
            this.txtCutOffDays.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(116, 21);
            this.label8.TabIndex = 17;
            this.label8.Text = "CutOff Days : *";
            // 
            // txtLessDays
            // 
            this.txtLessDays.Location = new System.Drawing.Point(142, 309);
            this.txtLessDays.Name = "txtLessDays";
            this.txtLessDays.Size = new System.Drawing.Size(265, 29);
            this.txtLessDays.TabIndex = 9;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 312);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(97, 21);
            this.label9.TabIndex = 19;
            this.label9.Text = "Less days : *";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(142, 344);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(265, 29);
            this.txtMobile.TabIndex = 10;
            this.txtMobile.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(52, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 21);
            this.label10.TabIndex = 21;
            this.label10.Text = "Mobile : *";
            // 
            // txtEmails
            // 
            this.txtEmails.Location = new System.Drawing.Point(142, 379);
            this.txtEmails.Name = "txtEmails";
            this.txtEmails.Size = new System.Drawing.Size(265, 29);
            this.txtEmails.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(59, 382);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(74, 21);
            this.label11.TabIndex = 23;
            this.label11.Text = "Emails : *";
            // 
            // EditUserAccount
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(433, 496);
            this.Controls.Add(this.txtEmails);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtLessDays);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtCutOffDays);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtIntRate2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtFooText);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtIntRate1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.radCredit);
            this.Controls.Add(this.radDebit);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtBalance);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtAbout);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUserAccount";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit client...";
            this.Load += new System.EventHandler(this.EditUserAccount_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtAbout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtBalance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radDebit;
        private System.Windows.Forms.RadioButton radCredit;
        private System.Windows.Forms.TextBox txtIntRate1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFooText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtIntRate2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCutOffDays;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtLessDays;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEmails;
        private System.Windows.Forms.Label label11;
    }
}