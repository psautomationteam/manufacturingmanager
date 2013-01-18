namespace BaoHien.UI
{
    partial class ProductAndMaterialReport
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbmUnits = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbmAttrs = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbmProductTypes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbmProducts = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgwStockEntranceList = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStockEntranceList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbmUnits);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbmAttrs);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbmProductTypes);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.cbmProducts);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(19, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1053, 118);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin tạo báo cáo";
            // 
            // cbmUnits
            // 
            this.cbmUnits.FormattingEnabled = true;
            this.cbmUnits.Location = new System.Drawing.Point(734, 65);
            this.cbmUnits.Name = "cbmUnits";
            this.cbmUnits.Size = new System.Drawing.Size(188, 21);
            this.cbmUnits.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(664, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Đơn vị :";
            // 
            // cbmAttrs
            // 
            this.cbmAttrs.FormattingEnabled = true;
            this.cbmAttrs.Location = new System.Drawing.Point(425, 65);
            this.cbmAttrs.Name = "cbmAttrs";
            this.cbmAttrs.Size = new System.Drawing.Size(188, 21);
            this.cbmAttrs.TabIndex = 18;
            this.cbmAttrs.SelectedIndexChanged += new System.EventHandler(this.cbmAttrs_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(343, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Quy cách :";
            // 
            // cbmProductTypes
            // 
            this.cbmProductTypes.FormattingEnabled = true;
            this.cbmProductTypes.Location = new System.Drawing.Point(734, 26);
            this.cbmProductTypes.Name = "cbmProductTypes";
            this.cbmProductTypes.Size = new System.Drawing.Size(188, 21);
            this.cbmProductTypes.TabIndex = 16;
            this.cbmProductTypes.SelectedIndexChanged += new System.EventHandler(this.cbmProductTypes_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(664, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Loại :";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(425, 27);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(188, 20);
            this.dtpTo.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Đến ngày :";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(104, 27);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(188, 20);
            this.dtpFrom.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Từ ngày :";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(962, 26);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(71, 60);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Báo Cáo";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbmProducts
            // 
            this.cbmProducts.FormattingEnabled = true;
            this.cbmProducts.Location = new System.Drawing.Point(104, 65);
            this.cbmProducts.Name = "cbmProducts";
            this.cbmProducts.Size = new System.Drawing.Size(188, 21);
            this.cbmProducts.TabIndex = 9;
            this.cbmProducts.SelectedIndexChanged += new System.EventHandler(this.cbmProducts_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Sản phẩm :";
            // 
            // dgwStockEntranceList
            // 
            this.dgwStockEntranceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwStockEntranceList.Location = new System.Drawing.Point(19, 165);
            this.dgwStockEntranceList.Name = "dgwStockEntranceList";
            this.dgwStockEntranceList.Size = new System.Drawing.Size(1053, 456);
            this.dgwStockEntranceList.TabIndex = 21;
            this.dgwStockEntranceList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgwStockEntranceList_CellDoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(324, 14);
            this.label8.TabIndex = 20;
            this.label8.Text = "Chú ý: mặc định sẽ là tình trạng hiện tại trong kho và thành phẩm";
            // 
            // ProductAndMaterialReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgwStockEntranceList);
            this.Controls.Add(this.label8);
            this.Name = "ProductAndMaterialReport";
            this.Size = new System.Drawing.Size(1084, 641);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwStockEntranceList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbmProducts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgwStockEntranceList;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbmProductTypes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbmAttrs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbmUnits;
        private System.Windows.Forms.Label label6;
    }
}
