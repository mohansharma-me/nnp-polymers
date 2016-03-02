namespace NNPPoly.forms
{
    partial class frmEmailSettings
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
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFooter = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtCollection = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.txtDespatch = new System.Windows.Forms.RichTextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.txtStock = new System.Windows.Forms.RichTextBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtOrderRequest = new System.Windows.Forms.RichTextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "E-mail address:";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(20, 44);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(296, 26);
            this.txtEmail.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "GMail Username :";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(20, 100);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(296, 26);
            this.txtUsername.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(319, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "GMail Password :";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(322, 100);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(291, 26);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(319, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 18);
            this.label4.TabIndex = 0;
            this.label4.Text = "Footer Message :";
            // 
            // txtFooter
            // 
            this.txtFooter.Location = new System.Drawing.Point(322, 44);
            this.txtFooter.Name = "txtFooter";
            this.txtFooter.Size = new System.Drawing.Size(291, 26);
            this.txtFooter.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(20, 143);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(593, 197);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtCollection);
            this.tabPage3.Location = new System.Drawing.Point(4, 27);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(585, 166);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "Collection Mail";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtCollection
            // 
            this.txtCollection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCollection.Location = new System.Drawing.Point(0, 0);
            this.txtCollection.Name = "txtCollection";
            this.txtCollection.Size = new System.Drawing.Size(585, 166);
            this.txtCollection.TabIndex = 0;
            this.txtCollection.Text = "";
            this.txtCollection.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.txtDespatch);
            this.tabPage4.Location = new System.Drawing.Point(4, 27);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(585, 166);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Despatch Mail";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // txtDespatch
            // 
            this.txtDespatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDespatch.Location = new System.Drawing.Point(0, 0);
            this.txtDespatch.Name = "txtDespatch";
            this.txtDespatch.Size = new System.Drawing.Size(585, 166);
            this.txtDespatch.TabIndex = 1;
            this.txtDespatch.Text = "";
            this.txtDespatch.WordWrap = false;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.txtStock);
            this.tabPage5.Location = new System.Drawing.Point(4, 27);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(585, 166);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Stock Mail";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // txtStock
            // 
            this.txtStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStock.Location = new System.Drawing.Point(0, 0);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(585, 166);
            this.txtStock.TabIndex = 1;
            this.txtStock.Text = "";
            this.txtStock.WordWrap = false;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtOrderRequest);
            this.tabPage6.Location = new System.Drawing.Point(4, 27);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(585, 166);
            this.tabPage6.TabIndex = 3;
            this.tabPage6.Text = "Order Request Mail";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtOrderRequest
            // 
            this.txtOrderRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOrderRequest.Location = new System.Drawing.Point(0, 0);
            this.txtOrderRequest.Name = "txtOrderRequest";
            this.txtOrderRequest.Size = new System.Drawing.Size(585, 166);
            this.txtOrderRequest.TabIndex = 1;
            this.txtOrderRequest.Text = "";
            this.txtOrderRequest.WordWrap = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(20, 346);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(523, 346);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(598, 80);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // frmEmailSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(625, 390);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.txtFooter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEmailSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Email Settings";
            this.Load += new System.EventHandler(this.frmEmailSettings_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFooter;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RichTextBox txtCollection;
        private System.Windows.Forms.RichTextBox txtDespatch;
        private System.Windows.Forms.RichTextBox txtStock;
        private System.Windows.Forms.RichTextBox txtOrderRequest;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

