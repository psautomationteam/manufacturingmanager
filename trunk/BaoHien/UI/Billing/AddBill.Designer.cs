namespace BaoHien.UI
{
    partial class AddBill
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
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbxCustomer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.validator1 = new Itboy.Components.Validator(this.components);
            this.SuspendLayout();
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(92, 84);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(479, 87);
            this.txtNote.TabIndex = 21;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(29, 84);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 14);
            this.label8.TabIndex = 20;
            this.label8.Text = "Ghi chú:";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Enabled = false;
            this.txtCreatedDate.Location = new System.Drawing.Point(357, 44);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(159, 20);
            this.txtCreatedDate.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(298, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 14);
            this.label7.TabIndex = 18;
            this.label7.Text = "Ngày lập:";
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(357, 12);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(214, 22);
            this.cbxCustomer.TabIndex = 17;
            this.validator1.SetType(this.cbxCustomer, Itboy.Components.ValidationType.Required);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(270, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Khách hàng (*):";
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.Enabled = false;
            this.txtOrderCode.Location = new System.Drawing.Point(92, 14);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Size = new System.Drawing.Size(151, 20);
            this.txtOrderCode.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 14;
            this.label1.Text = "Mã Phiếu:";
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(92, 42);
            this.txtAmount.Name = "txtAmount";
            this.validator1.SetRegularExpression(this.txtAmount, "[0-9].[0-9]");
            this.txtAmount.Size = new System.Drawing.Size(151, 20);
            this.txtAmount.TabIndex = 23;
            this.validator1.SetType(this.txtAmount, Itboy.Components.ValidationType.RegularExpression);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 14);
            this.label3.TabIndex = 22;
            this.label3.Text = "Số tiền:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(497, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 25;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(396, 182);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 24;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // validator1
            // 
            this.validator1.Form = this;
            // 
            // AddBill
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 220);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNote);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtCreatedDate);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cbxCustomer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtOrderCode);
            this.Controls.Add(this.label1);
            this.Name = "AddBill";
            this.Text = "Phiếu khách hàng trả tiền";
            this.Load += new System.EventHandler(this.AddBill_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private Itboy.Components.Validator validator1;
    }
}