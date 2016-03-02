namespace NNPPoly.forms
{
    partial class frmSMSSettings
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
            this.txtAPI = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCollection = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDespatch = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOrderRequest = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.RichTextBox();
            this.txtDays = new System.Windows.Forms.NumericUpDown();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txtMoU = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtDays)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAPI
            // 
            this.txtAPI.Location = new System.Drawing.Point(12, 46);
            this.txtAPI.Name = "txtAPI";
            this.txtAPI.Size = new System.Drawing.Size(645, 26);
            this.txtAPI.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "API Link :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(497, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(256, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Number of Days for Order Request :";
            // 
            // txtCollection
            // 
            this.txtCollection.Location = new System.Drawing.Point(12, 104);
            this.txtCollection.Name = "txtCollection";
            this.txtCollection.Size = new System.Drawing.Size(370, 101);
            this.txtCollection.TabIndex = 2;
            this.txtCollection.Text = "";
            this.txtCollection.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "Collection SMS :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 83);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 18);
            this.label4.TabIndex = 1;
            this.label4.Text = "Despatch SMS :";
            // 
            // txtDespatch
            // 
            this.txtDespatch.Location = new System.Drawing.Point(388, 104);
            this.txtDespatch.Name = "txtDespatch";
            this.txtDespatch.Size = new System.Drawing.Size(365, 101);
            this.txtDespatch.TabIndex = 3;
            this.txtDespatch.Text = "";
            this.txtDespatch.WordWrap = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 212);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 18);
            this.label5.TabIndex = 1;
            this.label5.Text = "Order Request SMS :";
            // 
            // txtOrderRequest
            // 
            this.txtOrderRequest.Location = new System.Drawing.Point(12, 233);
            this.txtOrderRequest.Name = "txtOrderRequest";
            this.txtOrderRequest.Size = new System.Drawing.Size(370, 101);
            this.txtOrderRequest.TabIndex = 4;
            this.txtOrderRequest.Text = "";
            this.txtOrderRequest.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(385, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 18);
            this.label6.TabIndex = 1;
            this.label6.Text = "Stock SMS :";
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(388, 233);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(365, 101);
            this.txtStock.TabIndex = 5;
            this.txtStock.Text = "";
            this.txtStock.WordWrap = false;
            // 
            // txtDays
            // 
            this.txtDays.Location = new System.Drawing.Point(663, 46);
            this.txtDays.Name = "txtDays";
            this.txtDays.Size = new System.Drawing.Size(90, 26);
            this.txtDays.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(15, 465);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(88, 32);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(660, 465);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 32);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 339);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 18);
            this.label7.TabIndex = 1;
            this.label7.Text = "MoU SMS :";
            // 
            // txtMoU
            // 
            this.txtMoU.Location = new System.Drawing.Point(15, 360);
            this.txtMoU.Name = "txtMoU";
            this.txtMoU.Size = new System.Drawing.Size(370, 101);
            this.txtMoU.TabIndex = 4;
            this.txtMoU.Text = "";
            this.txtMoU.WordWrap = false;
            // 
            // frmSMSSettings
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(765, 511);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDays);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtDespatch);
            this.Controls.Add(this.txtMoU);
            this.Controls.Add(this.txtOrderRequest);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCollection);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAPI);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSMSSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SMS Settings";
            this.Load += new System.EventHandler(this.frmSMSSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtDays)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAPI;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox txtCollection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox txtDespatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox txtOrderRequest;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox txtStock;
        private System.Windows.Forms.NumericUpDown txtDays;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox txtMoU;
    }
}

