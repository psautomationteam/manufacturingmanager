namespace BaoHien.UI
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
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductionRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMeasurementUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystemUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddOrder = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAddProductionRequest = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAddProduct = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.tsbAddCustomer = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMessage = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlMain = new System.Windows.Forms.Panel();
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
            this.menuLogout,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 20);
            this.menuFile.Text = "Tệp";
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(152, 22);
            this.menuExit.Text = "Đóng";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.menuLogout.Size = new System.Drawing.Size(152, 22);
            this.menuLogout.Text = "Thoát";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
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
            this.menuProductType.Click += new System.EventHandler(this.menuProductType_Click);
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
            this.menuAttribute.Size = new System.Drawing.Size(148, 22);
            this.menuAttribute.Text = "Thuộc tính SP";
            // 
            // menuMeasurementUnit
            // 
            this.menuMeasurementUnit.Name = "menuMeasurementUnit";
            this.menuMeasurementUnit.Size = new System.Drawing.Size(148, 22);
            this.menuMeasurementUnit.Text = "Đơn Vị Tính";
            // 
            // menuEmployee
            // 
            this.menuEmployee.Name = "menuEmployee";
            this.menuEmployee.Size = new System.Drawing.Size(148, 22);
            this.menuEmployee.Text = "Nhân Viên";
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
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystemUser});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(72, 20);
            this.menuSystem.Text = "Hệ Thống";
            // 
            // menuSystemUser
            // 
            this.menuSystemUser.Name = "menuSystemUser";
            this.menuSystemUser.Size = new System.Drawing.Size(139, 22);
            this.menuSystemUser.Text = "Người Dùng";
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
            this.menuGuide.Size = new System.Drawing.Size(135, 22);
            this.menuGuide.Text = "Hướng Dẫn";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(135, 22);
            this.menuAbout.Text = "Phần Mềm";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Highlight;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddOrder,
            this.toolStripLabel1,
            this.tsbAddProductionRequest,
            this.toolStripLabel2,
            this.tsbAddProduct,
            this.toolStripLabel3,
            this.tsbAddCustomer,
            this.toolStripLabel4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1084, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddOrder
            // 
            this.tsbAddOrder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddOrder.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddOrder.Image")));
            this.tsbAddOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddOrder.Name = "tsbAddOrder";
            this.tsbAddOrder.Size = new System.Drawing.Size(23, 22);
            this.tsbAddOrder.Text = "toolStripButton1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(62, 22);
            this.toolStripLabel1.Text = "Phiếu xuất";
            // 
            // tsbAddProductionRequest
            // 
            this.tsbAddProductionRequest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddProductionRequest.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddProductionRequest.Image")));
            this.tsbAddProductionRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddProductionRequest.Name = "tsbAddProductionRequest";
            this.tsbAddProductionRequest.Size = new System.Drawing.Size(23, 22);
            this.tsbAddProductionRequest.Text = "toolStripButton2";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(83, 22);
            this.toolStripLabel2.Text = "Phiếu sản xuất";
            // 
            // tsbAddProduct
            // 
            this.tsbAddProduct.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddProduct.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddProduct.Image")));
            this.tsbAddProduct.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddProduct.Name = "tsbAddProduct";
            this.tsbAddProduct.Size = new System.Drawing.Size(23, 22);
            this.tsbAddProduct.Text = "toolStripButton2";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel3.Text = "Tạo sản phẩm";
            // 
            // tsbAddCustomer
            // 
            this.tsbAddCustomer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddCustomer.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddCustomer.Image")));
            this.tsbAddCustomer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddCustomer.Name = "tsbAddCustomer";
            this.tsbAddCustomer.Size = new System.Drawing.Size(23, 22);
            this.tsbAddCustomer.Text = "toolStripButton3";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(103, 22);
            this.toolStripLabel4.Text = "Thêm khách hàng";
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
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 49);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1084, 641);
            this.pnlMain.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 712);
            this.Controls.Add(this.pnlMain);
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
        private System.Windows.Forms.ToolStripButton tsbAddOrder;
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbAddProductionRequest;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripButton tsbAddProduct;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripButton tsbAddCustomer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.ToolStripMenuItem menuLogout;

    }
}

