namespace BaoHien.UI
{
    partial class BaseUnitList
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
            this.dgvBaseUnitList = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaseUnitList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBaseUnitList
            // 
            this.dgvBaseUnitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaseUnitList.Location = new System.Drawing.Point(44, 78);
            this.dgvBaseUnitList.Name = "dgvBaseUnitList";
            this.dgvBaseUnitList.Size = new System.Drawing.Size(787, 505);
            this.dgvBaseUnitList.TabIndex = 14;
            this.dgvBaseUnitList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaseUnitList_CellClick);
            this.dgvBaseUnitList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaseUnitList_CellDoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(142, 25);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 46);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Thêm đơn vị mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(46, 25);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 46);
            this.btnDelete.TabIndex = 15;
            this.btnDelete.Text = "Xóa đơn vị được chọn";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // BaseUnitList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvBaseUnitList);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Name = "BaseUnitList";
            this.Size = new System.Drawing.Size(1084, 690);
            this.Load += new System.EventHandler(this.BaseUnitList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaseUnitList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBaseUnitList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;

    }
}
