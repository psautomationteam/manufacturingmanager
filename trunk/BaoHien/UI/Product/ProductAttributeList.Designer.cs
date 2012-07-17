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
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductAttributeList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductAttributeList
            // 
            this.dgvProductAttributeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductAttributeList.Location = new System.Drawing.Point(23, 67);
            this.dgvProductAttributeList.Name = "dgvProductAttributeList";
            this.dgvProductAttributeList.Size = new System.Drawing.Size(787, 469);
            this.dgvProductAttributeList.TabIndex = 17;
            this.dgvProductAttributeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductAttributeList_CellClick);
            this.dgvProductAttributeList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductAttributeList_CellDoubleClick);
            this.dgvProductAttributeList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvProductAttributeList_CellFormatting);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(23, 18);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 43);
            this.btnAdd.TabIndex = 16;
            this.btnAdd.Text = "Thêm thuộc tính mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ProductAttributeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvProductAttributeList);
            this.Controls.Add(this.btnAdd);
            this.Name = "ProductAttributeList";
            this.Size = new System.Drawing.Size(1084, 641);
            this.Load += new System.EventHandler(this.ProductAttributeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductAttributeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductAttributeList;
        private System.Windows.Forms.Button btnAdd;
    }
}
