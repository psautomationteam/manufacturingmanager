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
            this.lblTotalResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaseUnitList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBaseUnitList
            // 
            this.dgvBaseUnitList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBaseUnitList.Location = new System.Drawing.Point(44, 46);
            this.dgvBaseUnitList.Name = "dgvBaseUnitList";
            this.dgvBaseUnitList.ReadOnly = true;
            this.dgvBaseUnitList.Size = new System.Drawing.Size(1007, 567);
            this.dgvBaseUnitList.TabIndex = 1;
            this.dgvBaseUnitList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaseUnitList_CellClick);
            this.dgvBaseUnitList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBaseUnitList_CellDoubleClick);
            this.dgvBaseUnitList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvBaseUnitList_DataBindingComplete);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(924, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(127, 28);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Thêm đơn vị mới";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblTotalResult
            // 
            this.lblTotalResult.AutoSize = true;
            this.lblTotalResult.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalResult.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalResult.Location = new System.Drawing.Point(177, 19);
            this.lblTotalResult.Name = "lblTotalResult";
            this.lblTotalResult.Size = new System.Drawing.Size(16, 16);
            this.lblTotalResult.TabIndex = 7;
            this.lblTotalResult.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Tổng số kết quả tìm được:";
            // 
            // BaseUnitList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvBaseUnitList);
            this.Controls.Add(this.btnAdd);
            this.Name = "BaseUnitList";
            this.Size = new System.Drawing.Size(1084, 641);
            this.Load += new System.EventHandler(this.BaseUnitList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBaseUnitList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBaseUnitList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblTotalResult;
        private System.Windows.Forms.Label label3;

    }
}
