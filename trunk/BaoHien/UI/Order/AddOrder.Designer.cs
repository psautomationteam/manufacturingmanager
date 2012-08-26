using BaoHien.UI.Base;
namespace BaoHien.UI
{
    partial class AddOrder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVAT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxCustomer = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOrderCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgwOrderDetails = new BaoHien.UI.Base.KeyPressAwareDataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.lblGrantTotal = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.btnPrintXK = new System.Windows.Forms.Button();
            this.validator1 = new Itboy.Components.Validator(this.components);
            this.printDoc = new System.Drawing.Printing.PrintDocument();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwOrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCreatedDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtVAT);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxCustomer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtOrderCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(716, 212);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu bán hàng";
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(94, 118);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(608, 87);
            this.txtNote.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 118);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 14);
            this.label8.TabIndex = 12;
            this.label8.Text = "Ghi chú:";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Enabled = false;
            this.txtCreatedDate.Location = new System.Drawing.Point(94, 88);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(159, 20);
            this.txtCreatedDate.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 88);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "Ngày lập:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(671, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 14);
            this.label6.TabIndex = 9;
            this.label6.Text = "VND";
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(481, 54);
            this.txtDiscount.Name = "txtDiscount";
            this.validator1.SetRegularExpression(this.txtDiscount, "[0-9].[0-9]");
            this.txtDiscount.Size = new System.Drawing.Size(183, 20);
            this.txtDiscount.TabIndex = 8;
            this.validator1.SetType(this.txtDiscount, Itboy.Components.ValidationType.RegularExpression);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(259, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "VND";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "Khấu chi:";
            // 
            // txtVAT
            // 
            this.txtVAT.Location = new System.Drawing.Point(94, 55);
            this.txtVAT.Name = "txtVAT";
            this.validator1.SetRegularExpression(this.txtVAT, "[0-9].[0-9]");
            this.txtVAT.Size = new System.Drawing.Size(159, 20);
            this.txtVAT.TabIndex = 5;
            this.validator1.SetType(this.txtVAT, Itboy.Components.ValidationType.RegularExpression);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "VAT:";
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(481, 22);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(221, 22);
            this.cbxCustomer.TabIndex = 3;
            this.validator1.SetType(this.cbxCustomer, Itboy.Components.ValidationType.Required);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "Khách hàng (*):";
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.Enabled = false;
            this.txtOrderCode.Location = new System.Drawing.Point(94, 22);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Size = new System.Drawing.Size(186, 20);
            this.txtOrderCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Phiếu:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgwOrderDetails);
            this.groupBox2.Location = new System.Drawing.Point(13, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 297);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết phiếu bán hàng";
            // 
            // dgwOrderDetails
            // 
            this.dgwOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwOrderDetails.Location = new System.Drawing.Point(7, 22);
            this.dgwOrderDetails.Name = "dgwOrderDetails";
            this.dgwOrderDetails.Size = new System.Drawing.Size(693, 269);
            this.dgwOrderDetails.TabIndex = 0;
            this.dgwOrderDetails.keyPressHook += new System.Windows.Forms.KeyEventHandler(this.dgwOrderDetails_KeyUp);
            this.dgwOrderDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwOrderDetails_CellEndEdit);
            this.dgwOrderDetails.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwOrderDetails_CellEnter);
            this.dgwOrderDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwOrderDetails_DataError);
            this.dgwOrderDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwOrderDetails_EditingControlShowing);
            this.dgwOrderDetails.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgwOrderDetails_KeyUp);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(0, 535);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(380, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "Các trường (*) yêu cầu nhập. Không thể tạo phiếu bán hàng với chi tiết trống";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(373, 533);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(210, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Giá trị phiếu hàng trước thuế và khấu hao:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(380, 559);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 14);
            this.label11.TabIndex = 4;
            this.label11.Text = "Giá trị phiếu hàng sau thuế và khấu hao:";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSubTotal.Location = new System.Drawing.Point(596, 531);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(49, 16);
            this.lblSubTotal.TabIndex = 5;
            this.lblSubTotal.Text = "0 VND";
            // 
            // lblGrantTotal
            // 
            this.lblGrantTotal.AutoSize = true;
            this.lblGrantTotal.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrantTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblGrantTotal.Location = new System.Drawing.Point(596, 554);
            this.lblGrantTotal.Name = "lblGrantTotal";
            this.lblGrantTotal.Size = new System.Drawing.Size(49, 16);
            this.lblGrantTotal.TabIndex = 6;
            this.lblGrantTotal.Text = "0 VND";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(361, 583);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 25);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(657, 579);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.Location = new System.Drawing.Point(455, 582);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(94, 25);
            this.btnPrintOrder.TabIndex = 9;
            this.btnPrintOrder.Text = "In phiếu BH";
            this.btnPrintOrder.UseVisualStyleBackColor = true;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
            // 
            // btnPrintXK
            // 
            this.btnPrintXK.Location = new System.Drawing.Point(565, 582);
            this.btnPrintXK.Name = "btnPrintXK";
            this.btnPrintXK.Size = new System.Drawing.Size(75, 25);
            this.btnPrintXK.TabIndex = 10;
            this.btnPrintXK.Text = "In phiễu XK";
            this.btnPrintXK.UseVisualStyleBackColor = true;
            this.btnPrintXK.Click += new System.EventHandler(this.btnPrintXK_Click);
            // 
            // validator1
            // 
            this.validator1.Form = this;
            // 
            // printDoc
            // 
            this.printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDoc_PrintPage);
            // 
            // AddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 617);
            this.Controls.Add(this.btnPrintXK);
            this.Controls.Add(this.btnPrintOrder);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblGrantTotal);
            this.Controls.Add(this.lblSubTotal);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "AddOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập phiếu bán hàng";
            this.Load += new System.EventHandler(this.AddOrder_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwOrderDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVAT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNote;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCreatedDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private KeyPressAwareDataGridView dgwOrderDetails;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblGrantTotal;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrintOrder;
        private System.Windows.Forms.Button btnPrintXK;
        private Itboy.Components.Validator validator1;
        private System.Drawing.Printing.PrintDocument printDoc;
    }
}