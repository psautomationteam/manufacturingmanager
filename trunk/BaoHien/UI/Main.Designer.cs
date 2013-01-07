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
            this.menuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuChangePass = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEntity = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductType = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOperation = new System.Windows.Forms.ToolStripMenuItem();
            this.menuProductionRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOrder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStockIn = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.menuConfiguration = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAttribute = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMeasurementUnit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEmployee = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInventory = new System.Windows.Forms.ToolStripMenuItem();
            this.menuArrear = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCommission = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSystemUser = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDBConfig = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
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
            this.menuChangePass,
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(39, 20);
            this.menuFile.Text = "Tệp";
            // 
            // menuLogout
            // 
            this.menuLogout.Name = "menuLogout";
            this.menuLogout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.menuLogout.Size = new System.Drawing.Size(151, 22);
            this.menuLogout.Text = "Thoát";
            this.menuLogout.Click += new System.EventHandler(this.menuLogout_Click);
            // 
            // menuChangePass
            // 
            this.menuChangePass.Name = "menuChangePass";
            this.menuChangePass.Size = new System.Drawing.Size(151, 22);
            this.menuChangePass.Text = "Đổi mật mã";
            this.menuChangePass.Click += new System.EventHandler(this.menuChangePass_Click);
            // 
            // menuExit
            // 
            this.menuExit.Name = "menuExit";
            this.menuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuExit.Size = new System.Drawing.Size(151, 22);
            this.menuExit.Text = "Đóng";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // menuEntity
            // 
            this.menuEntity.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProductType,
            this.menuProduct,
            this.menuCustomer});
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
            this.menuProduct.Click += new System.EventHandler(this.menuProduct_Click);
            // 
            // menuCustomer
            // 
            this.menuCustomer.Name = "menuCustomer";
            this.menuCustomer.Size = new System.Drawing.Size(152, 22);
            this.menuCustomer.Text = "Khách Hàng";
            this.menuCustomer.Click += new System.EventHandler(this.menuCustomer_Click);
            // 
            // menuOperation
            // 
            this.menuOperation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProductionRequest,
            this.menuOrder,
            this.menuStockIn,
            this.menuBilling});
            this.menuOperation.Name = "menuOperation";
            this.menuOperation.Size = new System.Drawing.Size(75, 20);
            this.menuOperation.Text = "Nghiệp Vụ";
            // 
            // menuProductionRequest
            // 
            this.menuProductionRequest.Name = "menuProductionRequest";
            this.menuProductionRequest.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.menuProductionRequest.Size = new System.Drawing.Size(190, 22);
            this.menuProductionRequest.Text = "Phiếu Sản Xuất";
            this.menuProductionRequest.Click += new System.EventHandler(this.menuProductionRequest_Click);
            // 
            // menuOrder
            // 
            this.menuOrder.Name = "menuOrder";
            this.menuOrder.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.menuOrder.Size = new System.Drawing.Size(190, 22);
            this.menuOrder.Text = "Phiếu Bán Hàng";
            this.menuOrder.Click += new System.EventHandler(this.menuOrder_Click);
            // 
            // menuStockIn
            // 
            this.menuStockIn.Name = "menuStockIn";
            this.menuStockIn.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.menuStockIn.Size = new System.Drawing.Size(190, 22);
            this.menuStockIn.Text = "Phiếu Nhập Kho";
            this.menuStockIn.Click += new System.EventHandler(this.menuStockIn_Click);
            // 
            // menuBilling
            // 
            this.menuBilling.Name = "menuBilling";
            this.menuBilling.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menuBilling.Size = new System.Drawing.Size(190, 22);
            this.menuBilling.Text = "Phiếu Thanh Toán";
            this.menuBilling.Click += new System.EventHandler(this.menuBilling_Click);
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
            this.menuAttribute.Size = new System.Drawing.Size(140, 22);
            this.menuAttribute.Text = "Quy cách SP";
            this.menuAttribute.Click += new System.EventHandler(this.menuAttribute_Click);
            // 
            // menuMeasurementUnit
            // 
            this.menuMeasurementUnit.Name = "menuMeasurementUnit";
            this.menuMeasurementUnit.Size = new System.Drawing.Size(140, 22);
            this.menuMeasurementUnit.Text = "Đơn Vị Tính";
            this.menuMeasurementUnit.Click += new System.EventHandler(this.menuMeasurementUnit_Click);
            // 
            // menuEmployee
            // 
            this.menuEmployee.Name = "menuEmployee";
            this.menuEmployee.Size = new System.Drawing.Size(140, 22);
            this.menuEmployee.Text = "Nhân Viên";
            this.menuEmployee.Click += new System.EventHandler(this.menuEmployee_Click);
            // 
            // menuReport
            // 
            this.menuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInventory,
            this.menuArrear,
            this.menuCommission});
            this.menuReport.Name = "menuReport";
            this.menuReport.Size = new System.Drawing.Size(63, 20);
            this.menuReport.Text = "Báo Cáo";
            // 
            // menuInventory
            // 
            this.menuInventory.Name = "menuInventory";
            this.menuInventory.Size = new System.Drawing.Size(181, 22);
            this.menuInventory.Text = "Kho và Thành Phẩm";
            this.menuInventory.Click += new System.EventHandler(this.menuInventory_Click);
            // 
            // menuArrear
            // 
            this.menuArrear.Name = "menuArrear";
            this.menuArrear.Size = new System.Drawing.Size(181, 22);
            this.menuArrear.Text = "Công Nợ";
            this.menuArrear.Click += new System.EventHandler(this.menuArrear_Click);
            // 
            // menuCommission
            // 
            this.menuCommission.Name = "menuCommission";
            this.menuCommission.Size = new System.Drawing.Size(181, 22);
            this.menuCommission.Text = "Hoa Hồng NV";
            this.menuCommission.Click += new System.EventHandler(this.menuCommission_Click);
            // 
            // menuSystem
            // 
            this.menuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuSystemUser,
            this.menuDBConfig});
            this.menuSystem.Name = "menuSystem";
            this.menuSystem.Size = new System.Drawing.Size(72, 20);
            this.menuSystem.Text = "Hệ Thống";
            // 
            // menuSystemUser
            // 
            this.menuSystemUser.Name = "menuSystemUser";
            this.menuSystemUser.Size = new System.Drawing.Size(167, 22);
            this.menuSystemUser.Text = "Người Dùng";
            this.menuSystemUser.Click += new System.EventHandler(this.menuSystemUser_Click);
            // 
            // menuDBConfig
            // 
            this.menuDBConfig.Name = "menuDBConfig";
            this.menuDBConfig.Size = new System.Drawing.Size(167, 22);
            this.menuDBConfig.Text = "Cấu Hình Dữ Liệu";
            this.menuDBConfig.Click += new System.EventHandler(this.menuDBConfig_Click);
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
            this.menuGuide.Size = new System.Drawing.Size(149, 22);
            this.menuGuide.Text = "Hướng Dẫn";
            // 
            // menuAbout
            // 
            this.menuAbout.Name = "menuAbout";
            this.menuAbout.Size = new System.Drawing.Size(149, 22);
            this.menuAbout.Text = "TT Phần Mềm";
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
            this.toolStripLabel4,
            this.toolStripButton1,
            this.toolStripLabel5,
            this.toolStripButton2,
            this.toolStripLabel6});
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
            this.tsbAddOrder.ToolTipText = "Lập phiếu bán hàng";
            this.tsbAddOrder.Click += new System.EventHandler(this.tsbAddOrder_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel1.Text = "Phiếu bán hàng";
            // 
            // tsbAddProductionRequest
            // 
            this.tsbAddProductionRequest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbAddProductionRequest.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddProductionRequest.Image")));
            this.tsbAddProductionRequest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddProductionRequest.Name = "tsbAddProductionRequest";
            this.tsbAddProductionRequest.Size = new System.Drawing.Size(23, 22);
            this.tsbAddProductionRequest.Text = "toolStripButton2";
            this.tsbAddProductionRequest.ToolTipText = "Lập phiếu sản xuất";
            this.tsbAddProductionRequest.Click += new System.EventHandler(this.tsbAddProductionRequest_Click);
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
            this.tsbAddProduct.ToolTipText = "Tạo sản phẩm";
            this.tsbAddProduct.Click += new System.EventHandler(this.tsbAddProduct_Click);
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
            this.tsbAddCustomer.ToolTipText = "Thêm khách hàng";
            this.tsbAddCustomer.Click += new System.EventHandler(this.tsbAddCustomer_Click);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(103, 22);
            this.toolStripLabel4.Text = "Thêm khách hàng";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.ToolTipText = "tsbAddBill";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel5.Text = "KH Thanh Toán";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "tbsAddStockEntrance";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(90, 22);
            this.toolStripLabel6.Text = "Phiếu nhập kho";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMessage,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 671);
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
            this.pnlMain.Size = new System.Drawing.Size(1084, 622);
            this.pnlMain.TabIndex = 3;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 693);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bao Hien Ltd - He Thong Quan Ly Kinh Doanh";
            this.Load += new System.EventHandler(this.Main_Load);
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
        private System.Windows.Forms.ToolStripMenuItem menuArrear;
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
        private System.Windows.Forms.ToolStripMenuItem menuCustomer;
        private System.Windows.Forms.ToolStripMenuItem menuChangePass;
        private System.Windows.Forms.ToolStripMenuItem menuStockIn;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripMenuItem menuCommission;
        private System.Windows.Forms.ToolStripMenuItem menuBilling;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripMenuItem menuDBConfig;

    }
}

