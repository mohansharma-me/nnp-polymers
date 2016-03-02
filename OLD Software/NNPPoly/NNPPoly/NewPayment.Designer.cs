namespace NNPPoly
{
    partial class NewPayment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewPayment));
            this.label1 = new System.Windows.Forms.Label();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDocChqNo = new System.Windows.Forms.TextBox();
            this.txtType = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtParticulars = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.radDebit = new System.Windows.Forms.RadioButton();
            this.radCredit = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmt = new System.Windows.Forms.Label();
            this.txtMT = new System.Windows.Forms.TextBox();
            this.lblMT = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblGrade = new System.Windows.Forms.Label();
            this.cmbGrade = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Date: ";
            // 
            // dtPicker
            // 
            this.dtPicker.CustomFormat = "dd-MM-yyyy";
            this.dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtPicker.Location = new System.Drawing.Point(12, 44);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(152, 29);
            this.dtPicker.TabIndex = 1;
            this.dtPicker.Value = new System.DateTime(2014, 6, 25, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Invoice/Doc/Chq No :";
            // 
            // txtDocChqNo
            // 
            this.txtDocChqNo.Location = new System.Drawing.Point(170, 44);
            this.txtDocChqNo.Name = "txtDocChqNo";
            this.txtDocChqNo.Size = new System.Drawing.Size(219, 29);
            this.txtDocChqNo.TabIndex = 3;
            this.txtDocChqNo.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // txtType
            // 
            this.txtType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtType.Location = new System.Drawing.Point(395, 44);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(152, 29);
            this.txtType.TabIndex = 5;
            this.txtType.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(391, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Type : *";
            // 
            // txtParticulars
            // 
            this.txtParticulars.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtParticulars.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtParticulars.Location = new System.Drawing.Point(12, 113);
            this.txtParticulars.Name = "txtParticulars";
            this.txtParticulars.Size = new System.Drawing.Size(535, 29);
            this.txtParticulars.TabIndex = 7;
            this.txtParticulars.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Particulars : *";
            // 
            // radDebit
            // 
            this.radDebit.AutoSize = true;
            this.radDebit.Location = new System.Drawing.Point(272, 153);
            this.radDebit.Name = "radDebit";
            this.radDebit.Size = new System.Drawing.Size(93, 25);
            this.radDebit.TabIndex = 9;
            this.radDebit.TabStop = true;
            this.radDebit.Text = "Debit (D)";
            this.radDebit.UseVisualStyleBackColor = true;
            this.radDebit.CheckedChanged += new System.EventHandler(this.radDebit_CheckedChanged);
            // 
            // radCredit
            // 
            this.radCredit.AutoSize = true;
            this.radCredit.Location = new System.Drawing.Point(149, 155);
            this.radCredit.Name = "radCredit";
            this.radCredit.Size = new System.Drawing.Size(97, 25);
            this.radCredit.TabIndex = 8;
            this.radCredit.TabStop = true;
            this.radCredit.Text = "Credit (C)";
            this.radCredit.UseVisualStyleBackColor = true;
            this.radCredit.CheckedChanged += new System.EventHandler(this.radCredit_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 21);
            this.label5.TabIndex = 10;
            this.label5.Text = "Amount type : *";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(12, 216);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(197, 29);
            this.txtAmount.TabIndex = 12;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // lblAmt
            // 
            this.lblAmt.AutoSize = true;
            this.lblAmt.Location = new System.Drawing.Point(8, 192);
            this.lblAmt.Name = "lblAmt";
            this.lblAmt.Size = new System.Drawing.Size(88, 21);
            this.lblAmt.TabIndex = 11;
            this.lblAmt.Text = "Amount : *";
            // 
            // txtMT
            // 
            this.txtMT.Location = new System.Drawing.Point(215, 216);
            this.txtMT.Name = "txtMT";
            this.txtMT.Size = new System.Drawing.Size(150, 29);
            this.txtMT.TabIndex = 14;
            this.txtMT.TextChanged += new System.EventHandler(this.txtType_TextChanged);
            // 
            // lblMT
            // 
            this.lblMT.AutoSize = true;
            this.lblMT.Location = new System.Drawing.Point(211, 192);
            this.lblMT.Name = "lblMT";
            this.lblMT.Size = new System.Drawing.Size(42, 21);
            this.lblMT.TabIndex = 13;
            this.lblMT.Text = "MT :";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 268);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 32);
            this.btnOK.TabIndex = 16;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(467, 268);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.TabIndex = 17;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(367, 192);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(62, 21);
            this.lblGrade.TabIndex = 17;
            this.lblGrade.Text = "Grade :";
            this.lblGrade.Visible = false;
            // 
            // cmbGrade
            // 
            this.cmbGrade.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrade.FormattingEnabled = true;
            this.cmbGrade.Location = new System.Drawing.Point(371, 216);
            this.cmbGrade.Name = "cmbGrade";
            this.cmbGrade.Size = new System.Drawing.Size(176, 29);
            this.cmbGrade.TabIndex = 15;
            this.cmbGrade.Visible = false;
            // 
            // NewPayment
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(559, 312);
            this.Controls.Add(this.cmbGrade);
            this.Controls.Add(this.lblGrade);
            this.Controls.Add(this.dtPicker);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtMT);
            this.Controls.Add(this.lblMT);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.lblAmt);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.radCredit);
            this.Controls.Add(this.radDebit);
            this.Controls.Add(this.txtParticulars);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDocChqNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewPayment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Make new payment";
            this.Load += new System.EventHandler(this.NewPayment_Load);
            this.Shown += new System.EventHandler(this.NewPayment_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDocChqNo;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtParticulars;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radDebit;
        private System.Windows.Forms.RadioButton radCredit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmt;
        private System.Windows.Forms.TextBox txtMT;
        private System.Windows.Forms.Label lblMT;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.ComboBox cmbGrade;
    }
}