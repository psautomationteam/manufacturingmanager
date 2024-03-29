﻿namespace BaoHien.UI
{
    partial class ucProductType
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
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvProductTypeList = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalResult = new System.Windows.Forms.Label();
            this.grpSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductTypeList)).BeginInit();
            this.SuspendLayout();
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.btnSearch);
            this.grpSearch.Controls.Add(this.txtName);
            this.grpSearch.Controls.Add(this.txtCode);
            this.grpSearch.Controls.Add(this.label2);
            this.grpSearch.Controls.Add(this.label1);
            this.grpSearch.Location = new System.Drawing.Point(12, 3);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(1052, 67);
            this.grpSearch.TabIndex = 0;
            this.grpSearch.TabStop = false;
            this.grpSearch.Text = "Tìm Kiếm";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(967, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(621, 26);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(242, 20);
            this.txtName.TabIndex = 1;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(139, 26);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(297, 20);
            this.txtCode.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(494, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên loại sản phẩm:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã loại sản phẩm:";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(936, 76);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(118, 22);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Thêm loại sản phẩm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvProductTypeList
            // 
            this.dgvProductTypeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductTypeList.Location = new System.Drawing.Point(12, 104);
            this.dgvProductTypeList.Name = "dgvProductTypeList";
            this.dgvProductTypeList.ReadOnly = true;
            this.dgvProductTypeList.Size = new System.Drawing.Size(1052, 516);
            this.dgvProductTypeList.TabIndex = 4;
            this.dgvProductTypeList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductTypeList_CellClick);
            this.dgvProductTypeList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProductTypeList_CellDoubleClick);
            this.dgvProductTypeList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvProductTypeList_DataBindingComplete);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tổng số kết quả tìm được:";
            // 
            // lblTotalResult
            // 
            this.lblTotalResult.AutoSize = true;
            this.lblTotalResult.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalResult.ForeColor = System.Drawing.Color.Blue;
            this.lblTotalResult.Location = new System.Drawing.Point(145, 80);
            this.lblTotalResult.Name = "lblTotalResult";
            this.lblTotalResult.Size = new System.Drawing.Size(16, 16);
            this.lblTotalResult.TabIndex = 5;
            this.lblTotalResult.Text = "0";
            // 
            // ucProductType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTotalResult);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dgvProductTypeList);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.grpSearch);
            this.Name = "ucProductType";
            this.Size = new System.Drawing.Size(1084, 641);
            this.Load += new System.EventHandler(this.ucProductType_Load);
            this.grpSearch.ResumeLayout(false);
            this.grpSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductTypeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpSearch;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvProductTypeList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalResult;
    }
}
