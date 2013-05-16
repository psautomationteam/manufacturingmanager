namespace BaoHien.UI.Cleaner
{
    partial class Cleaner
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
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.chkBH = new System.Windows.Forms.CheckBox();
            this.chkTT = new System.Windows.Forms.CheckBox();
            this.chkNK = new System.Windows.Forms.CheckBox();
            this.chkSX = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Xóa dữ liệu trước ngày :";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "dd/MM/yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(167, 23);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(126, 20);
            this.dtpDate.TabIndex = 1;
            // 
            // chkBH
            // 
            this.chkBH.AutoSize = true;
            this.chkBH.Checked = true;
            this.chkBH.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBH.Location = new System.Drawing.Point(24, 65);
            this.chkBH.Name = "chkBH";
            this.chkBH.Size = new System.Drawing.Size(101, 17);
            this.chkBH.TabIndex = 2;
            this.chkBH.Text = "Phiếu bán hàng";
            this.chkBH.UseVisualStyleBackColor = true;
            // 
            // chkTT
            // 
            this.chkTT.AutoSize = true;
            this.chkTT.Checked = true;
            this.chkTT.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTT.Location = new System.Drawing.Point(167, 65);
            this.chkTT.Name = "chkTT";
            this.chkTT.Size = new System.Drawing.Size(107, 17);
            this.chkTT.TabIndex = 3;
            this.chkTT.Text = "Phiếu thanh toán";
            this.chkTT.UseVisualStyleBackColor = true;
            // 
            // chkNK
            // 
            this.chkNK.AutoSize = true;
            this.chkNK.Checked = true;
            this.chkNK.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkNK.Location = new System.Drawing.Point(24, 105);
            this.chkNK.Name = "chkNK";
            this.chkNK.Size = new System.Drawing.Size(101, 17);
            this.chkNK.TabIndex = 4;
            this.chkNK.Text = "Phiếu nhập kho";
            this.chkNK.UseVisualStyleBackColor = true;
            // 
            // chkSX
            // 
            this.chkSX.AutoSize = true;
            this.chkSX.Checked = true;
            this.chkSX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSX.Location = new System.Drawing.Point(167, 105);
            this.chkSX.Name = "chkSX";
            this.chkSX.Size = new System.Drawing.Size(96, 17);
            this.chkSX.TabIndex = 5;
            this.chkSX.Text = "Phiếu sản xuất";
            this.chkSX.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(50, 144);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(167, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // Cleaner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(315, 196);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.chkSX);
            this.Controls.Add(this.chkNK);
            this.Controls.Add(this.chkTT);
            this.Controls.Add(this.chkBH);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Name = "Cleaner";
            this.Text = "Cleaner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.CheckBox chkBH;
        private System.Windows.Forms.CheckBox chkTT;
        private System.Windows.Forms.CheckBox chkNK;
        private System.Windows.Forms.CheckBox chkSX;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}