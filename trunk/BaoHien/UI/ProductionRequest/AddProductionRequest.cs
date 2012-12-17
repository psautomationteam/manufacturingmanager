using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.MeasurementUnits;
using BaoHien.Services.Products;
using BaoHien.Services.ProductAttributes;
using BaoHien.Common;
using BaoHien.Services.ProductionRequests;
using DAL.Helper;
using BaoHien.Services.ProductionRequestDetails;
using BaoHien.UI.Base;
using BaoHien.Services.BaseAttributes;
using BaoHien.Model;
using BaoHien.Services.MaterialInStocks;
using BaoHien.Services.ProductInStocks;
namespace BaoHien.UI
{
    public partial class AddProductionRequest : BaseForm
    {
        bool isUpdating = false;
        ProductionRequest productionRequest;
        
        BindingList<ProductionRequestDetail> productionRequestDetailInProductions;
        BindingList<ProductionRequestDetail> productionRequestDetailInMaterials;
        BindingList<Product> productForProducts;
        BindingList<Product> productForMaterials;
        BindingList<BaseAttribute> baseAttributesAtRowForMaterial;
        BindingList<BaseAttribute> baseAttributesAtRowForProduct;
        BindingList<ProductionRequestDetailModel> originalMaterials;
        BindingList<ProductionRequestDetailModel> originalProductions;
       
        public AddProductionRequest()
        {
            InitializeComponent();
        }
        private void SetupColumnsForMaterial()
        {

            dgvMaterial.AutoGenerateColumns = false;




            if (productionRequestDetailInMaterials == null)
            {
                productionRequestDetailInMaterials = new BindingList<ProductionRequestDetail>();


            }

            var query = from productionRequestDetail in productionRequestDetailInMaterials

                        select new ProductionRequestDetailModel
                        {
                            ProductId = productionRequestDetail.ProductId,
                            AttributeId = productionRequestDetail.AttributeId,
                            NumberUnit = productionRequestDetail.NumberUnit,

                            Note = productionRequestDetail.Note,
                        };
            originalMaterials = new BindingList<ProductionRequestDetailModel>(query.ToList());
            dgvMaterial.DataSource = new BindingList<ProductionRequestDetailModel>(query.ToList());

            dgvMaterial.ReadOnly = false;

            DataGridViewComboBoxColumn productColumn = new DataGridViewComboBoxColumn();
            productColumn.Width = 150;
            productColumn.AutoComplete = false;

            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = productForProducts;
            productColumn.DisplayMember = "ProductName";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";

            dgvMaterial.Columns.Add(productColumn);

            DataGridViewComboBoxColumn productAttributeColumn = new DataGridViewComboBoxColumn();
            productAttributeColumn.Width = 150;
            productAttributeColumn.HeaderText = "Quy cách sản phẩm";

            productAttributeColumn.DataSource = baseAttributesAtRowForProduct;

            productAttributeColumn.DisplayMember = "AttributeName";
            //productColumn.Frozen = true;
            productAttributeColumn.ValueMember = "Id";

            dgvMaterial.Columns.Add(productAttributeColumn);



            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();

            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            //numberUnitColumn.ValueType = typeof(int);
            dgvMaterial.Columns.Add(numberUnitColumn);


            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();

            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 100;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgvMaterial.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.DataPropertyName = "DeleteButton";
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
        }
        private void SetupColumnsForProductRequest()
        {
           
            dgvProduct.AutoGenerateColumns = false;

            if (productionRequestDetailInProductions == null)
            {
                productionRequestDetailInProductions = new BindingList<ProductionRequestDetail>();
                

            }
            
            var query = from productionRequestDetail in productionRequestDetailInProductions

                        select new ProductionRequestDetailModel
                        {
                            ProductId = productionRequestDetail.ProductId,
                            AttributeId = productionRequestDetail.AttributeId,
                            NumberUnit = productionRequestDetail.NumberUnit,

                            Note = productionRequestDetail.Note,
                        };
            originalProductions = new BindingList<ProductionRequestDetailModel>(query.ToList());
            dgvProduct.DataSource = new BindingList<ProductionRequestDetailModel>(query.ToList());
            
            dgvProduct.ReadOnly = false;
            
            DataGridViewComboBoxColumn productColumn = new DataGridViewComboBoxColumn();
            productColumn.Width = 150;
            productColumn.AutoComplete = false;

            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = productForProducts;
            productColumn.DisplayMember = "ProductName";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";

            dgvProduct.Columns.Add(productColumn);

            DataGridViewComboBoxColumn productAttributeColumn = new DataGridViewComboBoxColumn();
            productAttributeColumn.Width = 150;
            productAttributeColumn.HeaderText = "Quy cách sản phẩm";

            productAttributeColumn.DataSource = baseAttributesAtRowForProduct;

            productAttributeColumn.DisplayMember = "AttributeName";
            //productColumn.Frozen = true;
            productAttributeColumn.ValueMember = "Id";

            dgvProduct.Columns.Add(productAttributeColumn);



            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();

            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            //numberUnitColumn.ValueType = typeof(int);
            dgvProduct.Columns.Add(numberUnitColumn);



            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
           
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 100;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgvProduct.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.DataPropertyName = "DeleteButton";
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;


        }
        private void AddProductionRequest_Load(object sender, EventArgs e)
        {
            loadSomeData();
            SetupColumnsForMaterial();
            SetupColumnsForProductRequest();
            updateProductionRequestDetailCells();
        }
        private void loadSomeData()
        {
            if (productionRequest != null)
            {
                txtDate.Text = productionRequest.RequestedDate.ToShortDateString();
                txtDate.Enabled = false;
                txtUser.Text = productionRequest.SystemUser.FullName;
                txtUser.Enabled = false;
                txtNote.Text = productionRequest.Note;
                txtCode.Text = productionRequest.ReqCode;
            }
            else
            {
                txtDate.Text = DateTime.Now.ToShortDateString();
                txtDate.Enabled = false;
                txtUser.Text = (Global.CurrentUser != null) ? Global.CurrentUser.FullName : "";
                txtUser.Enabled = false;
                txtCode.Text = RandomGeneration.GeneratingCode(BHConstant.PREFIX_FOR_PRODUCTION);
            }
            
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            baseAttributesAtRowForProduct = new BindingList<BaseAttribute>(baseAttributeService.GetBaseAttributes());
            baseAttributesAtRowForMaterial = new BindingList<BaseAttribute>(baseAttributeService.GetBaseAttributes()) ;
            ProductService productService = new ProductService();
            productForProducts = new BindingList<Product>(productService.GetProducts());
            productForMaterials = new BindingList<Product>(productService.GetProducts());
        }
        private void dgvMaterial_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvMaterial.CurrentCell != null)
            {
                if (dgvMaterial.CurrentCell.ColumnIndex == 0)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<Product, String>(productForProducts.ToArray(), delegate(Product row) { return (String)row.ProductName; });
                    source.AddRange(stringArray);

                    ComboBox prodCode = e.Control as ComboBox;
                    if (prodCode != null)
                    {
                        prodCode.DropDownStyle = ComboBoxStyle.DropDown;
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        prodCode.MaxDropDownItems = 5;

                    }
                    //DataGridViewComboBoxEditingControl avcbe = e.Control as DataGridViewComboBoxEditingControl;
                    //this.validator1.SetType(avcbe, Itboy.Components.ValidationType.Required);
                }
                else if (dgvMaterial.CurrentCell.ColumnIndex == 1)
                {
                    if (baseAttributesAtRowForMaterial != null)
                    {
                        var source = new AutoCompleteStringCollection();
                        String[] stringArray = Array.ConvertAll<BaseAttribute, String>(baseAttributesAtRowForMaterial.ToArray(), delegate(BaseAttribute row) { return (String)row.AttributeName; });
                        source.AddRange(stringArray);

                        ComboBox prodCode = e.Control as ComboBox;
                        if (prodCode != null)
                        {
                            prodCode.DropDownStyle = ComboBoxStyle.DropDown;
                            prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            prodCode.AutoCompleteCustomSource = source;
                            prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            prodCode.MaxDropDownItems = 5;

                        }
                        //DataGridViewComboBoxEditingControl avcbe = e.Control as DataGridViewComboBoxEditingControl;
                        //this.validator1.SetType(avcbe, Itboy.Components.ValidationType.Required);
                    }

                }
                else if (dgvMaterial.CurrentCell.ColumnIndex == 2)
                {
                    TextBox numberOfUnit = e.Control as TextBox;
                    this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                    this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                }
                else
                {
                    if (e.Control is TextBox)
                    {
                        TextBox other = e.Control as TextBox;
                        this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                    }
                }
            }
            
        }

        private void dgvMaterial_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
             
        }

        private void dgvMaterial_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (productionRequestDetailInMaterials == null)
            {
                productionRequestDetailInMaterials = new BindingList<ProductionRequestDetail>();
            }
            if (productionRequestDetailInMaterials.Count < dgvMaterial.RowCount)
            {
                ProductionRequestDetail productionRequestDetail = new ProductionRequestDetail();
                productionRequestDetail.Direction = false;
                productionRequestDetailInMaterials.Add(productionRequestDetail);
            }


            if (dgv.CurrentCell.Value != null)
            {
                if (e.ColumnIndex == 0)
                {
                    productionRequestDetailInMaterials[e.RowIndex].ProductId = (int)dgv.CurrentCell.Value;
                    ProductAttributeService productAttributeService = new ProductAttributeService();
                    List<ProductAttribute> productAttributes = productAttributeService.SelectProductAttributeByWhere(ba => ba.Id == (int)dgv.CurrentCell.Value);
                    baseAttributesAtRowForMaterial = new BindingList<BaseAttribute>();
                    foreach (ProductAttribute pa in productAttributes)
                    {
                        baseAttributesAtRowForMaterial.Add(pa.BaseAttribute);
                    }
                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dgvMaterial.Rows[e.RowIndex].Cells[1];
                    currentCell.DataSource = baseAttributesAtRowForMaterial;
                    if (productionRequestDetailInMaterials.Count > e.RowIndex && baseAttributesAtRowForMaterial.Count > 0)
                    {
                        productionRequestDetailInMaterials[e.RowIndex].AttributeId = baseAttributesAtRowForMaterial[0].Id;
                        currentCell.Value = baseAttributesAtRowForMaterial[0].Id;
                    }
                    if (baseAttributesAtRowForMaterial.Count > 0)
                        dgv.Rows[e.RowIndex].Cells[2].ReadOnly = false;


                }
                else if (e.ColumnIndex == 1)
                {
                    productionRequestDetailInMaterials[e.RowIndex].AttributeId = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 2)
                {
                    MaterialInStockService mis = new MaterialInStockService();
                    List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == productionRequestDetailInMaterials[e.RowIndex].ProductId && pt.AttributeId == productionRequestDetailInMaterials[e.RowIndex].AttributeId);
                    if (lstMaterial.Count == 0 || lstMaterial.First<MaterialInStock>().NumberOfItem == 0)
                    {
                        MessageBox.Show("Số lượng vật liệu trong kho đã hết");

                    }
                    else if (lstMaterial.First<MaterialInStock>().NumberOfItem < (int)dgv.Rows[e.RowIndex].Cells[2].Value)
                    {
                        MessageBox.Show("Số lượng vật liệu trong kho còn lại là: " + lstMaterial.First<MaterialInStock>().NumberOfItem.ToString());
                        
                    }
                    else
                    {
                        productionRequestDetailInMaterials[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    productionRequestDetailInMaterials[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                }
            }

            calculateTotalForMaterialGrid();
            
        }


        private void calculateTotalForMaterialGrid()
        {
            

           
        }
        private void calculateTotalForProductGrid()
        {


            
        }
        private void dgvMaterial_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProduct.CurrentCell != null)
            {
                if (dgvProduct.CurrentCell.ColumnIndex == 0)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<Product, String>(productForProducts.ToArray(), delegate(Product row) { return (String)row.ProductName; });
                    source.AddRange(stringArray);

                    ComboBox prodCode = e.Control as ComboBox;
                    if (prodCode != null)
                    {
                        prodCode.DropDownStyle = ComboBoxStyle.DropDown;
                        prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        prodCode.AutoCompleteCustomSource = source;
                        prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        prodCode.MaxDropDownItems = 5;

                    }
                    //DataGridViewComboBoxEditingControl avcbe = e.Control as DataGridViewComboBoxEditingControl;
                    //this.validator1.SetType(avcbe, Itboy.Components.ValidationType.Required);

                }
                else if (dgvProduct.CurrentCell.ColumnIndex == 1)
                {
                    if (baseAttributesAtRowForProduct != null)
                    {
                        var source = new AutoCompleteStringCollection();
                        String[] stringArray = Array.ConvertAll<BaseAttribute, String>(baseAttributesAtRowForProduct.ToArray(), delegate(BaseAttribute row) { return (String)row.AttributeName; });
                        source.AddRange(stringArray);

                        ComboBox prodCode = e.Control as ComboBox;
                        if (prodCode != null)
                        {
                            prodCode.DropDownStyle = ComboBoxStyle.DropDown;
                            prodCode.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                            prodCode.AutoCompleteCustomSource = source;
                            prodCode.AutoCompleteSource = AutoCompleteSource.CustomSource;
                            prodCode.MaxDropDownItems = 5;

                        }
                        //DataGridViewComboBoxEditingControl avcbe = e.Control as DataGridViewComboBoxEditingControl;
                        //this.validator1.SetType(avcbe, Itboy.Components.ValidationType.Required);
                    }

                }
                else if (dgvProduct.CurrentCell.ColumnIndex == 2)
                {
                    TextBox numberOfUnit = e.Control as TextBox;
                    this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                    this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                }
                else
                {
                    if (e.Control is TextBox)
                    {
                        TextBox other = e.Control as TextBox;
                        this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                    }
                }
            }
            
        }

        private void dgvProduct_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;
            if (productionRequestDetailInProductions == null)
            {
                productionRequestDetailInProductions = new BindingList<ProductionRequestDetail>();
            }
            if (productionRequestDetailInProductions.Count < dgvProduct.RowCount)
            {
                ProductionRequestDetail productionRequestDetail = new ProductionRequestDetail();
                productionRequestDetail.Direction = true;
                productionRequestDetailInProductions.Add(productionRequestDetail);
            }
            if (dgv.CurrentCell.Value != null)
            {
                if (e.ColumnIndex == 0)
                {

                    productionRequestDetailInProductions[e.RowIndex].ProductId = (int)dgv.CurrentCell.Value;
                    ProductAttributeService productAttributeService = new ProductAttributeService();
                    List<ProductAttribute> productAttributes = productAttributeService.SelectProductAttributeByWhere(ba => ba.Id == (int)dgv.CurrentCell.Value);
                    baseAttributesAtRowForProduct = new BindingList<BaseAttribute>();
                    foreach (ProductAttribute pa in productAttributes)
                    {
                        baseAttributesAtRowForProduct.Add(pa.BaseAttribute);
                    }
                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dgvProduct.Rows[e.RowIndex].Cells[1];
                    currentCell.DataSource = baseAttributesAtRowForProduct;
                    if (baseAttributesAtRowForProduct.Count > e.RowIndex && baseAttributesAtRowForProduct.Count > 0)
                    {
                        productionRequestDetailInProductions[e.RowIndex].AttributeId = baseAttributesAtRowForProduct[0].Id;
                        currentCell.Value = baseAttributesAtRowForProduct[0].Id;
                    }

                }
                else if (e.ColumnIndex == 1)
                {
                    productionRequestDetailInProductions[e.RowIndex].AttributeId = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 2)
                {
                    productionRequestDetailInProductions[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 3)
                {
                    productionRequestDetailInProductions[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                }


            }
            calculateTotalForProductGrid();
            
        }

        private void dgvProduct_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.validator1.Validate())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn muốn lưu?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    ProductionRequestService prs = new ProductionRequestService();
                    ProductionRequestDetailService productionRequestDetailService = new ProductionRequestDetailService();
                    MaterialInStockService mis = new MaterialInStockService();
                    ProductInStockService pis = new ProductInStockService();

                    if (productionRequest != null)
                    {
                        bool result = prs.UpdateProductionRequest(productionRequest);
                        if (result)
                        {
                            // ProductInStock
                            foreach (ProductionRequestDetail prd in productionRequestDetailInProductions)
                            {
                                if (prd.ProductId > 0 && prd.AttributeId > 0)
                                {
                                    if (prd.Id == 0)
                                    {
                                        prd.ProductionRequestId = productionRequest.Id;
                                        result = productionRequestDetailService.AddProductionRequestDetail(prd);

                                        //Save in Production In Stock
                                        ProductInStock productInStock = new ProductInStock();
                                        List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                                        productInStock.AttributeId = prd.AttributeId;
                                        productInStock.ProductId = prd.ProductId;
                                        productInStock.LatestUpdate = DateTime.Now;
                                        productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                                        if (lstProductInStock.Count > 0)
                                        {
                                            productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput + prd.NumberUnit;
                                            productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput;
                                            productInStock.NumberOfItem = (int)productInStock.NumberOfInput;
                                        }
                                        else
                                        {
                                            productInStock.NumberOfInput = prd.NumberUnit;
                                            productInStock.NumberOfOutput = 0;
                                            productInStock.NumberOfItem = (int)prd.NumberUnit;
                                        }
                                        pis.AddProductInStock(productInStock);
                                    }
                                    else
                                    {
                                        ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                                        original = originalProductions.Where(p => p.Id == prd.Id).ToList().FirstOrDefault();
                                        result = productionRequestDetailService.UpdateProductionRequestDetail(prd);

                                        //Update so luong
                                        if (original != null)
                                        {
                                            if (prd.ProductId == original.ProductId && prd.AttributeId == original.AttributeId && prd.NumberUnit != original.NumberUnit)
                                            {
                                                ProductInStock productInStock = new ProductInStock();
                                                List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);

                                                productInStock.AttributeId = prd.AttributeId;
                                                productInStock.ProductId = prd.ProductId;
                                                productInStock.LatestUpdate = DateTime.Now;
                                                productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput + prd.NumberUnit - original.NumberUnit;
                                                productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput;
                                                productInStock.NumberOfItem = lstProductInStock.Last<ProductInStock>().NumberOfItem + prd.NumberUnit - original.NumberUnit;
                                                productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;
                                                pis.UpdateProductInStock(productInStock);
                                            }

                                            //Sua chi tiet phieu
                                            else if (prd.ProductId != original.ProductId || prd.AttributeId != original.AttributeId)
                                            {

                                                //Tao moi
                                                List<ProductInStock> lstNewProduct = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                                                ProductInStock newProductInStock = new ProductInStock();
                                                newProductInStock.AttributeId = prd.AttributeId;
                                                newProductInStock.ProductId = prd.ProductId;
                                                newProductInStock.LatestUpdate = DateTime.Now;
                                                newProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;

                                                if (lstNewProduct.Count > 0)
                                                {
                                                    newProductInStock.NumberOfInput = lstNewProduct.Last<ProductInStock>().NumberOfInput + prd.NumberUnit;
                                                    newProductInStock.NumberOfOutput = lstNewProduct.Last<ProductInStock>().NumberOfOutput;
                                                    newProductInStock.NumberOfItem = lstNewProduct.Last<ProductInStock>().NumberOfItem + prd.NumberUnit;
                                                }
                                                else
                                                {
                                                    newProductInStock.NumberOfInput = prd.NumberUnit;
                                                    newProductInStock.NumberOfOutput = 0;
                                                    newProductInStock.NumberOfItem = prd.NumberUnit;
                                                }
                                                pis.AddProductInStock(newProductInStock);

                                                //Xoa cu
                                                List<ProductInStock> lstOldProduct = pis.SelectProductByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);
                                                if (lstOldProduct.Count > 0)
                                                {
                                                    ProductInStock oldProductInStock = new ProductInStock();
                                                    oldProductInStock.AttributeId = original.AttributeId;
                                                    oldProductInStock.ProductId = original.ProductId;
                                                    oldProductInStock.LatestUpdate = DateTime.Now;
                                                    oldProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                                                    newProductInStock.NumberOfInput = lstNewProduct.Last<ProductInStock>().NumberOfInput - original.NumberUnit;
                                                    newProductInStock.NumberOfOutput = lstNewProduct.Last<ProductInStock>().NumberOfOutput;
                                                    newProductInStock.NumberOfItem = lstNewProduct.Last<ProductInStock>().NumberOfItem - original.NumberUnit;
                                                    pis.AddProductInStock(newProductInStock);
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                            //MaterialInStock
                            foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                            {
                                if (prd.ProductId > 0 && prd.AttributeId > 0)
                                {
                                    if (prd.Id == 0)
                                    {
                                        prd.ProductionRequestId = productionRequest.Id;
                                        result = productionRequestDetailService.AddProductionRequestDetail(prd);

                                        //Save in Materail In Stock
                                        MaterialInStock materialInStock = new MaterialInStock();
                                        List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                                        if (lstMaterial != null)
                                        {
                                            materialInStock.AttributeId = prd.AttributeId;
                                            materialInStock.ProductId = prd.ProductId;
                                            materialInStock.LatestUpdate = DateTime.Now;
                                            materialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                                            materialInStock.NumberOfInput = lstMaterial.Last<MaterialInStock>() != null?lstMaterial.Last<MaterialInStock>().NumberOfInput: 0;
                                            materialInStock.NumberOfOutput = lstMaterial.Last<MaterialInStock>() != null ?lstMaterial.Last<MaterialInStock>().NumberOfOutput + prd.NumberUnit: 0;
                                            materialInStock.NumberOfItem = lstMaterial.Last<MaterialInStock>() != null ?lstMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit:0;
                                            mis.AddMaterialInStock(materialInStock);
                                        }
                                    }
                                    else
                                    {
                                        ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                                        original = originalProductions.Where(p => p.Id == prd.Id).ToList().FirstOrDefault();
                                        result = productionRequestDetailService.UpdateProductionRequestDetail(prd);

                                        if (original != null)
                                        {
                                            if (prd.ProductId == original.ProductId && prd.AttributeId == original.AttributeId && prd.NumberUnit != original.NumberUnit)
                                            {

                                                //Update Qty
                                                List<MaterialInStock> lstNewMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                                                MaterialInStock newMaterialInStock = new MaterialInStock();
                                                newMaterialInStock.AttributeId = prd.AttributeId;
                                                newMaterialInStock.ProductId = prd.ProductId;
                                                newMaterialInStock.LatestUpdate = DateTime.Now;
                                                newMaterialInStock.NumberOfInput = lstNewMaterial.Last<MaterialInStock>().NumberOfInput;
                                                newMaterialInStock.NumberOfOutput = lstNewMaterial.Last<MaterialInStock>().NumberOfOutput - prd.NumberUnit;
                                                newMaterialInStock.NumberOfItem = lstNewMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit;
                                                newMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                                                mis.AddMaterialInStock(newMaterialInStock);
                                            }
                                            else if (prd.ProductId != original.ProductId || prd.AttributeId != original.AttributeId || prd.NumberUnit != original.NumberUnit)
                                            {

                                                //Tao moi
                                                List<MaterialInStock> lstNewMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                                                MaterialInStock newMaterialInStock = new MaterialInStock();
                                                newMaterialInStock.AttributeId = prd.AttributeId;
                                                newMaterialInStock.ProductId = prd.ProductId;
                                                newMaterialInStock.LatestUpdate = DateTime.Now;
                                                newMaterialInStock.NumberOfInput = lstNewMaterial.Last<MaterialInStock>().NumberOfInput;
                                                newMaterialInStock.NumberOfOutput = lstNewMaterial.Last<MaterialInStock>().NumberOfOutput - prd.NumberUnit;
                                                newMaterialInStock.NumberOfItem = lstNewMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit;
                                                newMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                                                mis.AddMaterialInStock(newMaterialInStock);

                                                //Xoa Cu
                                                List<MaterialInStock> lstOldMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);
                                                MaterialInStock oldMaterialInStock = new MaterialInStock();
                                                oldMaterialInStock.AttributeId = prd.AttributeId;
                                                oldMaterialInStock.ProductId = prd.ProductId;
                                                oldMaterialInStock.LatestUpdate = DateTime.Now;
                                                oldMaterialInStock.NumberOfInput = lstOldMaterial.Last<MaterialInStock>().NumberOfInput;
                                                oldMaterialInStock.NumberOfOutput = lstOldMaterial.Last<MaterialInStock>().NumberOfOutput - original.NumberUnit;
                                                oldMaterialInStock.NumberOfItem = lstOldMaterial.Last<MaterialInStock>().NumberOfItem + original.NumberUnit;
                                                oldMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                                                mis.AddMaterialInStock(oldMaterialInStock);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (result)
                        {
                            MessageBox.Show("Phiếu sản xuất đã được cập nhật thành công");
                            if (this.CallFromUserControll != null && this.CallFromUserControll is ProductionRequestList)
                            {
                                ((ProductionRequestList)this.CallFromUserControll).loadProductionRequestList();
                            }
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return;
                        }
                    }
                    else
                    {
                        int userId = 0;
                        if (Global.CurrentUser != null)
                        {
                            userId = Global.CurrentUser.Id;
                        }
                        else
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return;
                        }
                        productionRequest = new ProductionRequest
                        {
                            Note = txtNote.Text,
                            ReqCode = txtCode.Text,
                            RequestedDate = DateTime.Now,
                            RequestedBy = userId,

                        };

                        bool result = prs.AddProductionRequest(productionRequest);
                        if (result)
                        {
                            long newProductionRequestId = BaoHienRepository.GetMaxId<ProductionRequest>();

                            foreach (ProductionRequestDetail prd in productionRequestDetailInProductions)
                            {
                                if (prd.ProductId > 0 && prd.AttributeId > 0)
                                {
                                    prd.ProductionRequestId = (int)newProductionRequestId;
                                    bool ret = productionRequestDetailService.AddProductionRequestDetail(prd);
                                    if (!ret)
                                    {
                                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                        return;
                                    }

                                    //Save in Production In Stock
                                    ProductInStock productInStock = new ProductInStock();
                                    List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                                    productInStock.AttributeId = prd.AttributeId;
                                    productInStock.ProductId = prd.ProductId;
                                    productInStock.LatestUpdate = DateTime.Now;
                                    productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;
                                    if (lstProductInStock.Count > 0)
                                    {
                                        productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput + prd.NumberUnit;
                                        productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput;
                                        productInStock.NumberOfItem = (int)productInStock.NumberOfInput + prd.NumberUnit;
                                    }
                                    else
                                    {
                                        productInStock.NumberOfInput = prd.NumberUnit;
                                        productInStock.NumberOfOutput = 0;
                                        productInStock.NumberOfItem = (int)prd.NumberUnit;

                                    }
                                    pis.AddProductInStock(productInStock);
                                }

                            }
                            foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                            {
                                if (prd.ProductId > 0 && prd.AttributeId > 0)
                                {
                                    prd.ProductionRequestId = (int)newProductionRequestId;
                                    bool ret = productionRequestDetailService.AddProductionRequestDetail(prd);
                                    if (!ret)
                                    {
                                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                        return;
                                    }

                                    //Save in Materail In Stock
                                    MaterialInStock materialInStock = new MaterialInStock();
                                    List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);

                                    materialInStock.AttributeId = prd.AttributeId;
                                    materialInStock.ProductId = prd.ProductId;
                                    materialInStock.LatestUpdate = DateTime.Now;
                                    materialInStock.NumberOfInput = lstMaterial.Last<MaterialInStock>().NumberOfInput;
                                    materialInStock.NumberOfOutput = lstMaterial.Last<MaterialInStock>().NumberOfOutput + prd.NumberUnit;
                                    materialInStock.NumberOfItem = lstMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit;
                                    materialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                                    mis.AddMaterialInStock(materialInStock);
                                }
                            }
                            MessageBox.Show("Phiếu sản xuất đã được cập nhật thành công");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return;
                        }
                    }
                }
                
            }
            
            
        }

        public void loadDataForEditProductRequest(int productionRequestId)
        {
            isUpdating = true;
            ProductionRequestService prs = new ProductionRequestService();
            productionRequest = prs.GetProductionRequest(productionRequestId);
            if (productionRequest != null)
            {
                ProductionRequestDetailService prds = new ProductionRequestDetailService();
                productionRequestDetailInProductions = new BindingList<ProductionRequestDetail>(prds.GetProductionRequestDetails().Where(p => p.ProductionRequestId == productionRequestId && p.Direction == BHConstant.PRODUCTION_REQUEST_DETAIL_OUT).ToList());
                productionRequestDetailInMaterials = new BindingList<ProductionRequestDetail>(prds.GetProductionRequestDetails().Where(p => p.ProductionRequestId == productionRequestId && p.Direction == BHConstant.PRODUCTION_REQUEST_DETAIL_IN).ToList());
            }

            
        }
        public void updateProductionRequestDetailCells()
        {
            if (isUpdating && productionRequest != null && productionRequestDetailInProductions.Count < dgvProduct.RowCount)
            {

                for (int i = 0; i < productionRequestDetailInProductions.Count; i++)
                {
                    for (int j = 0; j < 2 && j < dgvProduct.ColumnCount; j++)
                    {
                        if (dgvProduct.Rows[i].Cells[j] is DataGridViewComboBoxCell)
                        {
                            if(j==0)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvProduct.Rows[i].Cells[j];
                                pkgBoxCell.Value = productionRequestDetailInProductions[i].ProductId;
                            }
                            if (j == 1)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvProduct.Rows[i].Cells[j];
                                pkgBoxCell.Value = productionRequestDetailInProductions[i].AttributeId;
                            }
                        }
                    }
                }



            }
            if (isUpdating && productionRequest != null && productionRequestDetailInMaterials.Count < dgvMaterial.RowCount)
            {

                for (int i = 0; i < productionRequestDetailInMaterials.Count; i++)
                {
                    for (int j = 0; j < 2 && j < dgvMaterial.ColumnCount; j++)
                    {
                        if (dgvMaterial.Rows[i].Cells[j] is DataGridViewComboBoxCell)
                        {
                            if (j == 0)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvMaterial.Rows[i].Cells[j];
                                pkgBoxCell.Value = productionRequestDetailInMaterials[i].ProductId;
                            }
                            if (j == 1)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvMaterial.Rows[i].Cells[j];
                                pkgBoxCell.Value = productionRequestDetailInMaterials[i].AttributeId;
                            }
                        }
                    }
                }



            }
        }

        private void dgvMaterial_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
           
            
            //this.validator1.SetType(sender, Itboy.Components.ValidationType.Required);
        }

        private void dgvProduct_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            
        }
        
    }
}
