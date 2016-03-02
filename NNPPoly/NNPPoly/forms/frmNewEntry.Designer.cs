namespace NNPPoly.forms
{
    partial class frmNewEntry
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbClients = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDebit = new System.Windows.Forms.CheckBox();
            this.gbDebit = new System.Windows.Forms.GroupBox();
            this.cmbGrades = new System.Windows.Forms.ComboBox();
            this.txtMT = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtParticulars = new System.Windows.Forms.TextBox();
            this.txtInvoice = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtPicker = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.gbDebit.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Client :";
            // 
            // cmbClients
            // 
            this.cmbClients.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbClients.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbClients.FormattingEnabled = true;
            this.cmbClients.Location = new System.Drawing.Point(76, 18);
            this.cmbClients.Name = "cmbClients";
            this.cmbClients.Size = new System.Drawing.Size(511, 26);
            this.cmbClients.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDebit);
            this.groupBox1.Controls.Add(this.gbDebit);
            this.groupBox1.Controls.Add(this.txtType);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.txtParticulars);
            this.groupBox1.Controls.Add(this.txtInvoice);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtPicker);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(15, 50);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(572, 253);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // chkDebit
            // 
            this.chkDebit.AutoSize = true;
            this.chkDebit.Location = new System.Drawing.Point(34, 133);
            this.chkDebit.Name = "chkDebit";
            this.chkDebit.Size = new System.Drawing.Size(65, 22);
            this.chkDebit.TabIndex = 6;
            this.chkDebit.Text = "Debit";
            this.chkDebit.UseVisualStyleBackColor = true;
            this.chkDebit.CheckedChanged += new System.EventHandler(this.chkDebit_CheckedChanged);
            // 
            // gbDebit
            // 
            this.gbDebit.Controls.Add(this.cmbGrades);
            this.gbDebit.Controls.Add(this.txtMT);
            this.gbDebit.Controls.Add(this.label8);
            this.gbDebit.Controls.Add(this.label7);
            this.gbDebit.Enabled = false;
            this.gbDebit.Location = new System.Drawing.Point(18, 134);
            this.gbDebit.Name = "gbDebit";
            this.gbDebit.Size = new System.Drawing.Size(537, 100);
            this.gbDebit.TabIndex = 6;
            this.gbDebit.TabStop = false;
            // 
            // cmbGrades
            // 
            this.cmbGrades.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbGrades.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbGrades.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrades.FormattingEnabled = true;
            this.cmbGrades.Location = new System.Drawing.Point(267, 56);
            this.cmbGrades.Name = "cmbGrades";
            this.cmbGrades.Size = new System.Drawing.Size(254, 26);
            this.cmbGrades.TabIndex = 8;
            // 
            // txtMT
            // 
            this.txtMT.Location = new System.Drawing.Point(16, 56);
            this.txtMT.Name = "txtMT";
            this.txtMT.Size = new System.Drawing.Size(245, 26);
            this.txtMT.TabIndex = 7;
            this.txtMT.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(264, 35);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 18);
            this.label8.TabIndex = 0;
            this.label8.Text = "Grade :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 18);
            this.label7.TabIndex = 0;
            this.label7.Text = "MT :";
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(446, 45);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(109, 26);
            this.txtType.TabIndex = 3;
            this.txtType.TextChanged += new System.EventHandler(this.txtInvoice_TextChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(304, 102);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(251, 26);
            this.txtAmount.TabIndex = 5;
            this.txtAmount.TextChanged += new System.EventHandler(this.txtAmount_TextChanged);
            // 
            // txtParticulars
            // 
            this.txtParticulars.Location = new System.Drawing.Point(18, 102);
            this.txtParticulars.Name = "txtParticulars";
            this.txtParticulars.Size = new System.Drawing.Size(280, 26);
            this.txtParticulars.TabIndex = 4;
            this.txtParticulars.TextChanged += new System.EventHandler(this.txtInvoice_TextChanged);
            // 
            // txtInvoice
            // 
            this.txtInvoice.Location = new System.Drawing.Point(160, 45);
            this.txtInvoice.Name = "txtInvoice";
            this.txtInvoice.Size = new System.Drawing.Size(280, 26);
            this.txtInvoice.TabIndex = 2;
            this.txtInvoice.TextChanged += new System.EventHandler(this.txtInvoice_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 18);
            this.label6.TabIndex = 0;
            this.label6.Text = "Amount :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(443, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Type :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Particulars :";
            // 
            // dtPicker
            // 
            this.dtPicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtPicker.Location = new System.Drawing.Point(18, 45);
            this.dtPicker.Name = "dtPicker";
            this.dtPicker.Size = new System.Drawing.Size(136, 26);
            this.dtPicker.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Invoice :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Date :";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(12, 311);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(93, 36);
            this.btnSubmit.TabIndex = 9;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(494, 311);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(93, 36);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // frmNewEntry
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(599, 359);
            this.Controls.Add(this.cmbClients);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewEntry";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Entry";
            this.Load += new System.EventHandler(this.frmNewEntry_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbDebit.ResumeLayout(false);
            this.gbDebit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbClients;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtPicker;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInvoice;
        private System.Windows.Forms.TextBox txtType;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtParticulars;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox gbDebit;
        private System.Windows.Forms.CheckBox chkDebit;
        private System.Windows.Forms.TextBox txtMT;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbGrades;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
    }
}

