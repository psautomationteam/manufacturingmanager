namespace UI
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductionRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMeasurementUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystemUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEntity,
            this.menuOperation,
            this.menuConfiguration,
            this.menuReport,
            this.menuSystem,
            this.menuHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(1084, 24);
            this.menuMain.TabIndex = 0;
            this.menuMain.Text = "menuStrip1";
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 20);
            this.menuFile.Text = "Tệp";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(145, 22);
            this.menuExit.Text = "Đóng";
            // 
            // menuEntity
            // 
            this.menuEntity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProductType,
            this.menuProduct});
            this.menuEntity.Name = "menuEntity";
            this.menuEntity.Size = new System.Drawing.Size(74, 20);
            this.menuEntity.Text = "Danh Mục";
            // 
            // menuProductType
            // 
            this.menuProductType.Name = "menuProductType";
            this.menuProductType.Size = new System.Drawing.Size(152, 22);
            this.menuProductType.Text = "Loại Sản Phẩm";
            // 
            // menuProduct
            // 
            this.menuProduct.Name = "menuProduct";
            this.menuProduct.Size = new System.Drawing.Size(152, 22);
            this.menuProduct.Text = "Sản Phẩm";
            // 
            // menuOperation
            // 
            this.menuOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProductionRequest,
            this.menuOrder});
            this.menuOperation.Name = "menuOperation";
            this.menuOperation.Size = new System.Drawing.Size(75, 20);
            this.menuOperation.Text = "Nghiệp Vụ";
            // 
            // menuProductionRequest
            // 
            this.menuProductionRequest.Name = "menuProductionRequest";
            this.menuProductionRequest.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuProductionRequest.Size = new System.Drawing.Size(178, 22);
            this.menuProductionRequest.Text = "Phiếu Xuất Kho";
            // 
            // menuOrder
            // 
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuOrder.Size = new System.Drawing.Size(178, 22);
            this.menuOrder.Text = "Phiếu Bán Hàng";
            // 
            // menuConfiguration
            // 
            this.menuConfiguration.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuAttribute,
            this.menuMeasurementUnit,
            this.menuEmployee});
            this.menuConfiguration.Name = "menuConfiguration";
            this.menuConfiguration.Size = new System.Drawing.Size(69, 20);
            this.menuConfiguration.Text = "Cấu Hình";
            // 
            // menuAttribute
            // 
            this.menuAttribute.Name = "menuAttribute";
            this.menuAttribute.Size = new System.Drawing.Size(152, 22);
            this.menuAttribute.Text = "Thuộc tính SP";
            // 
            // menuMeasurementUnit
            // 
            this.menuMeasurementUnit.Name = "menuMeasurementUnit";
            this.menuMeasurementUnit.Size = new System.Drawing.Size(152, 22);
            this.menuMeasurementUnit.Text = "Đơn Vị Tính";
            // 
            // menuReport
            // 
            this.menuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInventory,
            this.menuInvoice});
            this.menuReport.Name = "menuReport";
            this.menuReport.Size = new System.Drawing.Size(63, 20);
            this.menuReport.Text = "Báo Cáo";
            // 
            // menuInventory
            // 
            this.menuInventory.Name = "menuInventory";
            this.menuInventory.Size = new System.Drawing.Size(225, 22);
            this.menuInventory.Text = "Kho va Thành Phẩm Cuối Kỳ";
            // 
            // menuInvoice
            // 
            this.menuInvoice.Name = "menuInvoice";
            this.menuInvoice.Size = new System.Drawing.Size(225, 22);
            this.menuInvoice.Text = "Tổng Công Nợ";
            // 
            // menuHelp
            // 
            this.menuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuGuide,
            this.menuAbout});
            this.menuHelp.Name = "menuHelp";
            this.menuHelp.Size = new System.Drawing.Size(65, 20);
            this.menuHelp.Text = "Trợ Giúp";
            // 
            // menuGuide
            // 
            this.menuGuide.Name = "menuGuide";
            this.menuGuide.Size = new System.Drawing.Size(152, 22);
            this.menuGuide.Text = "Hướng Dẫn";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(152, 22);
            this.menuAbout.Text = "Phần Mềm";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1084, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 690);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1084, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMessage
            // 
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(35, 17);
            this.lblMessage.Text = "Done";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripProgressBar1.RightToLeftLayout = true;
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystemUser});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(72, 20);
            this.menuSystem.Text = "Hệ Thống";
            // 
            // menuEmployee
            // 
            this.menuEmployee.Name = "menuEmployee";
            this.menuEmployee.Size = new System.Drawing.Size(152, 22);
            this.menuEmployee.Text = "Nhân Viên";
            // 
            // menuSystemUser
            // 
            this.menuSystemUser.Name = "menuSystemUser";
            this.menuSystemUser.Size = new System.Drawing.Size(152, 22);
            this.menuSystemUser.Text = "Người Dùng";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 712);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bao Hien Ltd - He Thong Quan Ly Kinh Doanh";
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem menuEntity;
        private System.Windows.Forms.ToolStripMenuItem menuProductType;
        private System.Windows.Forms.ToolStripMenuItem menuProduct;
        private System.Windows.Forms.ToolStripMenuItem menuOperation;
        private System.Windows.Forms.ToolStripMenuItem menuProductionRequest;
        private System.Windows.Forms.ToolStripMenuItem menuOrder;
        private System.Windows.Forms.ToolStripMenuItem menuConfiguration;
        private System.Windows.Forms.ToolStripMenuItem menuAttribute;
        private System.Windows.Forms.ToolStripMenuItem menuMeasurementUnit;
        private System.Windows.Forms.ToolStripMenuItem menuReport;
        private System.Windows.Forms.ToolStripMenuItem menuInventory;
        private System.Windows.Forms.ToolStripMenuItem menuInvoice;
        private System.Windows.Forms.ToolStripMenuItem menuHelp;
        private System.Windows.Forms.ToolStripMenuItem menuGuide;
        private System.Windows.Forms.ToolStripMenuItem menuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMessage;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripMenuItem menuEmployee;
        private System.Windows.Forms.ToolStripMenuItem menuSystem;
        private System.Windows.Forms.ToolStripMenuItem menuSystemUser;

    }
}

