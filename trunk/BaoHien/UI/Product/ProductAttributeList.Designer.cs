namespace BaoHien.UI
{
    partial class ProductAttributeList
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
            this.dgvProductAttributeList = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblTotalResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductAttributeList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductAttributeList
            // 
            this.dgvProductAttributeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductAttributeList.Location = new System.Drawing.Point(23, 45);
            this.dgvProductAttributeList.Name = "dgvProductAttributeList";
            this.dgvProductAttributeList.ReadOnly = true;
            this.dgvProductAttributeList.Size = new System.Drawing.Size(1030, 567);
            this.dgvProductAttributeList.TabIndex = 1;
            this.dgvProductAttributeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductAttributeList_CellClick);
            this.dgvProductAttributeList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductAttributeList_CellDoubleClick);
            this.dgvProductAttributeList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductAttributeList_DataBindingComplete);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(927, 14);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(126, 25);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm Quy cách mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblTotalResult
            // 
            this.lblTotalResult.AutoSize = true;
            this.lblTotalResult.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalResult.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalResult.Location = new System.Drawing.Point(156, 19);
            this.lblTotalResult.Name = "lblTotalResult";
            this.lblTotalResult.Size = new System.Drawing.Size(16, 16);
            this.lblTotalResult.TabIndex = 7;
            this.lblTotalResult.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tổng số kết quả tìm được:";
            // 
            // ProductAttributeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvProductAttributeList);
            this.Controls.Add(this.btnAdd);
            this.Name = "ProductAttributeList";
            this.Size = new System.Drawing.Size(1084, 641);
            this.Load += new System.EventHandler(this.ProductAttributeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductAttributeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductAttributeList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblTotalResult;
        private System.Windows.Forms.Label label3;
    }
}
