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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrintOrder = new System.Windows.Forms.Button();
            this.validator1 = new Itboy.Components.Validator(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwOrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
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
            this.groupBox1.Size = new System.Drawing.Size(716, 197);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin phiếu bán hàng";
            // 
            // txtDiscount
            // 
            this.txtDiscount.DecimalPlaces = 0;
            this.txtDiscount.DecimalsSeparator = ',';
            this.txtDiscount.Location = new System.Drawing.Point(481, 48);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.PreFix = "(VND) ";
            this.txtDiscount.Size = new System.Drawing.Size(221, 20);
            this.txtDiscount.TabIndex = 14;
            this.txtDiscount.ThousandsSeparator = '.';
            this.txtDiscount.Leave += new System.EventHandler(this.txtDiscount_Leave);
            // 
            // txtVAT
            // 
            this.txtVAT.DecimalPlaces = 0;
            this.txtVAT.DecimalsSeparator = ',';
            this.txtVAT.Location = new System.Drawing.Point(94, 48);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.PreFix = "(VND) ";
            this.txtVAT.Size = new System.Drawing.Size(186, 20);
            this.txtVAT.TabIndex = 13;
            this.txtVAT.ThousandsSeparator = '.';
            this.txtVAT.Leave += new System.EventHandler(this.txtVAT_Leave_1);
            // 
            // txtNote
            // 
            this.txtNote.Location = new System.Drawing.Point(94, 110);
            this.txtNote.Multiline = true;
            this.txtNote.Name = "txtNote";
            this.txtNote.Size = new System.Drawing.Size(608, 81);
            this.txtNote.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(28, 110);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Ghi chú:";
            // 
            // txtCreatedDate
            // 
            this.txtCreatedDate.Enabled = false;
            this.txtCreatedDate.Location = new System.Drawing.Point(94, 79);
            this.txtCreatedDate.Name = "txtCreatedDate";
            this.txtCreatedDate.Size = new System.Drawing.Size(186, 20);
            this.txtCreatedDate.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(23, 82);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 10;
            this.label7.Text = "Ngày lập:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(403, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Khấu chi:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(47, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "VAT:";
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(481, 20);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(221, 21);
            this.cbxCustomer.TabIndex = 1;
            this.validator1.SetType(this.cbxCustomer, Itboy.Components.ValidationType.Required);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(374, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Khách hàng (*):";
            // 
            // txtOrderCode
            // 
            this.txtOrderCode.Enabled = false;
            this.txtOrderCode.Location = new System.Drawing.Point(94, 20);
            this.txtOrderCode.Name = "txtOrderCode";
            this.txtOrderCode.Size = new System.Drawing.Size(186, 20);
            this.txtOrderCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Phiếu:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgwOrderDetails);
            this.groupBox2.Location = new System.Drawing.Point(13, 216);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(715, 276);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết phiếu bán hàng";
            // 
            // dgwOrderDetails
            // 
            this.dgwOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwOrderDetails.Location = new System.Drawing.Point(7, 20);
            this.dgwOrderDetails.Name = "dgwOrderDetails";
            this.dgwOrderDetails.Size = new System.Drawing.Size(693, 250);
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
            this.label9.Location = new System.Drawing.Point(0, 497);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(380, 14);
            this.label9.TabIndex = 2;
            this.label9.Text = "Các trường (*) yêu cầu nhập. Không thể tạo phiếu bán hàng với chi tiết trống";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(400, 497);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(210, 14);
            this.label10.TabIndex = 3;
            this.label10.Text = "Giá trị phiếu hàng trước thuế và khấu hao:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(400, 519);
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
            this.lblSubTotal.Location = new System.Drawing.Point(654, 495);
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
            this.lblGrantTotal.Location = new System.Drawing.Point(654, 517);
            this.lblGrantTotal.Name = "lblGrantTotal";
            this.lblGrantTotal.Size = new System.Drawing.Size(49, 16);
            this.lblGrantTotal.TabIndex = 6;
            this.lblGrantTotal.Text = "0 VND";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(476, 538);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(657, 538);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnPrintOrder
            // 
            this.btnPrintOrder.Location = new System.Drawing.Point(557, 538);
            this.btnPrintOrder.Name = "btnPrintOrder";
            this.btnPrintOrder.Size = new System.Drawing.Size(94, 23);
            this.btnPrintOrder.TabIndex = 4;
            this.btnPrintOrder.Text = "Xuất phiếu";
            this.btnPrintOrder.UseVisualStyleBackColor = true;
            this.btnPrintOrder.Click += new System.EventHandler(this.btnPrintOrder_Click);
            // 
            // validator1
            // 
            this.validator1.Form = this;
            // 
            // AddOrder
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(740, 573);
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
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrintOrder;
        private Itboy.Components.Validator validator1;
        private JRINCCustomControls.currencyTextBox txtDiscount;
        private JRINCCustomControls.currencyTextBox txtVAT;
    }
}