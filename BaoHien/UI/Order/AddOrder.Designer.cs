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
            this.lbCustomerName = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCreateKH = new System.Windows.Forms.Button();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWare = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDiscount = new JRINCCustomControls.currencyTextBox(this.components);
            this.txtVAT = new JRINCCustomControls.currencyTextBox(this.components);
            this.txtNote = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCreatedDate = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.btnPrintS = new System.Windows.Forms.Button();
            this.validator1 = new Itboy.Components.Validator(this.components);
            this.btnCreateNK = new System.Windows.Forms.Button();
            this.btnCreateSX = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwOrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbCustomerName);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.btnCreateKH);
            this.groupBox1.Controls.Add(this.txtReason);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtWare);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.txtVAT);
            this.groupBox1.Controls.Add(this.txtNote);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtCreatedDate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxCustomer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtOrderCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(798, 249);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu bán hàng";
            // 
            // lbCustomerName
            // 
            this.lbCustomerName.AutoSize = true;
            this.lbCustomerName.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCustomerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lbCustomerName.Location = new System.Drawing.Point(526, 51);
            this.lbCustomerName.Name = "lbCustomerName";
            this.lbCustomerName.Size = new System.Drawing.Size(0, 14);
            this.lbCustomerName.TabIndex = 21;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(408, 51);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Tên khách hàng :";
            // 
            // btnCreateKH
            // 
            this.btnCreateKH.Location = new System.Drawing.Point(742, 20);
            this.btnCreateKH.Name = "btnCreateKH";
            this.btnCreateKH.Size = new System.Drawing.Size(24, 22);
            this.btnCreateKH.TabIndex = 2;
            this.btnCreateKH.Text = "+";
            this.btnCreateKH.UseVisualStyleBackColor = true;
            this.btnCreateKH.Click += new System.EventHandler(this.btnCreateKH_Click);
            // 
            // txtReason
            // 
            this.txtReason.Location = new System.Drawing.Point(111, 132);
            this.txtReason.Name = "txtReason";
            this.txtReason.Size = new System.Drawing.Size(655, 20);
            this.txtReason.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Lý do xuất kho:";
            // 
            // txtWare
            // 
            this.txtWare.Location = new System.Drawing.Point(529, 102);
            this.txtWare.Name = "txtWare";
            this.txtWare.Size = new System.Drawing.Size(237, 20);
            this.txtWare.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(444, 108);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Kho hàng:";
            // 
            // txtDiscount
            // 
            this.txtDiscount.DecimalPlaces = 0;
            this.txtDiscount.DecimalsSeparator = ',';
            this.txtDiscount.Location = new System.Drawing.Point(111, 76);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.PreFix = "(VND) ";
            this.txtDiscount.Size = new System.Drawing.Size(197, 20);
            this.txtDiscount.TabIndex = 4;
            this.txtDiscount.ThousandsSeparator = '.';
            this.txtDiscount.WorkingText = null;
            this.txtDiscount.Leave += new System.EventHandler(this.txtDiscount_Leave);
            // 
            // txtVAT
            // 
            this.txtVAT.DecimalPlaces = 0;
            this.txtVAT.DecimalsSeparator = ',';
            this.txtVAT.Location = new System.Drawing.Point(111, 48);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.PreFix = "(VND) ";
            this.txtVAT.Size = new System.Drawing.Size(197, 20);
            this.txtVAT.TabIndex = 3;
            this.txtVAT.ThousandsSeparator = '.';
            this.txtVAT.WorkingText = null;
            this.txtVAT.Leave += new System.EventHandler(this.txtVAT_Leave);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(111, 158);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(655, 77);
            this.txtNote.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(39, 161);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Ghi chú:";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Enabled = false;
            this.txtCreatedDate.Location = new System.Drawing.Point(111, 105);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(197, 20);
            this.txtCreatedDate.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Ngày lập:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Khấu chi:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "VAT:";
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(529, 20);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(208, 21);
            this.cbxCustomer.TabIndex = 1;
            this.validator1.SetType(this.cbxCustomer, Itboy.Components.ValidationType.Required);
            this.cbxCustomer.SelectedIndexChanged += new System.EventHandler(this.cbxCustomer_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(419, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Khách hàng (*):";
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.Enabled = false;
            this.txtOrderCode.Location = new System.Drawing.Point(111, 20);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Size = new System.Drawing.Size(197, 20);
            this.txtOrderCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Phiếu:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgwOrderDetails);
            this.groupBox2.Location = new System.Drawing.Point(13, 267);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(797, 288);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết phiếu bán hàng";
            // 
            // dgwOrderDetails
            // 
            this.dgwOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwOrderDetails.Location = new System.Drawing.Point(7, 19);
            this.dgwOrderDetails.Name = "dgwOrderDetails";
            this.dgwOrderDetails.Size = new System.Drawing.Size(784, 263);
            this.dgwOrderDetails.TabIndex = 9;
            this.dgwOrderDetails.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwOrderDetails_CellEndEdit);
            this.dgwOrderDetails.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgwOrderDetails_CellFormatting);
            this.dgwOrderDetails.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgwOrderDetails_DataError);
            this.dgwOrderDetails.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgwOrderDetails_EditingControlShowing);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label9.Location = new System.Drawing.Point(17, 559);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(380, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "Các trường (*) yêu cầu nhập. Không thể tạo phiếu bán hàng với chi tiết trống";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(447, 559);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(210, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Giá trị phiếu hàng trước thuế và khấu hao:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(447, 581);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 14);
            this.label11.TabIndex = 4;
            this.label11.Text = "Giá trị phiếu hàng sau thuế và khấu hao:";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSubTotal.Location = new System.Drawing.Point(663, 558);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(14, 15);
            this.lblSubTotal.TabIndex = 5;
            this.lblSubTotal.Text = "0";
            // 
            // lblGrantTotal
            // 
            this.lblGrantTotal.AutoSize = true;
            this.lblGrantTotal.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrantTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblGrantTotal.Location = new System.Drawing.Point(663, 580);
            this.lblGrantTotal.Name = "lblGrantTotal";
            this.lblGrantTotal.Size = new System.Drawing.Size(14, 15);
            this.lblGrantTotal.TabIndex = 6;
            this.lblGrantTotal.Text = "0";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(602, 600);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrintS
            // 
            this.btnPrintS.Location = new System.Drawing.Point(683, 600);
            this.btnPrintS.Name = "btnPrintS";
            this.btnPrintS.Size = new System.Drawing.Size(121, 23);
            this.btnPrintS.TabIndex = 10;
            this.btnPrintS.Text = "Lưu && Xuất phiếu";
            this.btnPrintS.UseVisualStyleBackColor = true;
            this.btnPrintS.Click += new System.EventHandler(this.btnPrintS_Click);
            // 
            // validator1
            // 
            this.validator1.Form = this;
            // 
            // btnCreateNK
            // 
            this.btnCreateNK.Location = new System.Drawing.Point(20, 599);
            this.btnCreateNK.Name = "btnCreateNK";
            this.btnCreateNK.Size = new System.Drawing.Size(116, 23);
            this.btnCreateNK.TabIndex = 12;
            this.btnCreateNK.Text = "Tạo phiếu nhập kho";
            this.btnCreateNK.UseVisualStyleBackColor = true;
            this.btnCreateNK.Click += new System.EventHandler(this.btnCreateNK_Click);
            // 
            // btnCreateSX
            // 
            this.btnCreateSX.Location = new System.Drawing.Point(142, 599);
            this.btnCreateSX.Name = "btnCreateSX";
            this.btnCreateSX.Size = new System.Drawing.Size(116, 23);
            this.btnCreateSX.TabIndex = 13;
            this.btnCreateSX.Text = "Tạo phiếu sản xuất";
            this.btnCreateSX.UseVisualStyleBackColor = true;
            this.btnCreateSX.Click += new System.EventHandler(this.btnCreateSX_Click);
            // 
            // AddOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(822, 632);
            this.Controls.Add(this.btnCreateSX);
            this.Controls.Add(this.btnCreateNK);
            this.Controls.Add(this.btnPrintS);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOrderCode;
        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Button btnPrintS;
        private Itboy.Components.Validator validator1;
        private JRINCCustomControls.currencyTextBox txtDiscount;
        private JRINCCustomControls.currencyTextBox txtVAT;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWare;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCreateSX;
        private System.Windows.Forms.Button btnCreateNK;
        private System.Windows.Forms.Button btnCreateKH;
        private System.Windows.Forms.Label lbCustomerName;
        private System.Windows.Forms.Label label12;
    }
}