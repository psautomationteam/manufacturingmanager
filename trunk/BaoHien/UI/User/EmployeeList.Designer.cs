namespace BaoHien.UI
{
    partial class EmployeeList
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
            this.dgvEmployeeList = new System.Windows.Forms.DataGridView();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvEmployeeList
            // 
            this.dgvEmployeeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmployeeList.Location = new System.Drawing.Point(33, 76);
            this.dgvEmployeeList.Name = "dgvEmployeeList";
            this.dgvEmployeeList.Size = new System.Drawing.Size(787, 505);
            this.dgvEmployeeList.TabIndex = 20;
            this.dgvEmployeeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployeeList_CellClick);
            this.dgvEmployeeList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvEmployeeList_CellDoubleClick);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(155, 24);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(107, 46);
            this.btnAdd.TabIndex = 19;
            this.btnAdd.Text = "Thêm nhân viên mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(35, 24);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(104, 46);
            this.btnDelete.TabIndex = 21;
            this.btnDelete.Text = "Tạm dừng NV được chọn";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // EmployeeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvEmployeeList);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnDelete);
            this.Name = "EmployeeList";
            this.Size = new System.Drawing.Size(1084, 690);
            this.Load += new System.EventHandler(this.EmployeeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmployeeList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvEmployeeList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
    }
}
