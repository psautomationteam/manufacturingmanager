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
            productAttributeColumn.HeaderText = "Thuộc tính sản phẩm";

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
            numberUnitColumn.ValueType = typeof(int);
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
            productAttributeColumn.HeaderText = "Thuộc tính sản phẩm";

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
            numberUnitColumn.ValueType = typeof(int);
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
                txtDate.Text = productionRequest.RequestedDate.ToLongDateString();
                txtDate.Enabled = false;
                txtUser.Text = productionRequest.SystemUser.FullName;
                txtUser.Enabled = false;
                txtNote.Text = productionRequest.Note;
                txtCode.Text = productionRequest.ReqCode;
            }
            else
            {
                txtDate.Text = DateTime.Now.ToLongDateString();
                txtDate.Enabled = false;
                txtUser.Text = (Global.CurrentUser != null) ? Global.CurrentUser.FullName : "";
                txtUser.Enabled = false;
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



                }
                else if (e.ColumnIndex == 1)
                {
                    productionRequestDetailInMaterials[e.RowIndex].AttributeId = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 2)
                {
                    productionRequestDetailInMaterials[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
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
            MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
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
            MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ProductionRequestService prs = new ProductionRequestService();
            ProductionRequestDetailService productionRequestDetailService = new ProductionRequestDetailService();
            
            if (productionRequest != null)
            {
                bool result = prs.UpdateProductionRequest(productionRequest);
                if (result)
                {
                    foreach (ProductionRequestDetail prd in productionRequestDetailInProductions)
                    {
                        if (prd.ProductId > 0 && prd.AttributeId > 0)
                        {
                            if (prd.Id == 0)
                            {
                                prd.ProductionRequestId = productionRequest.Id;
                                result = productionRequestDetailService.AddProductionRequestDetail(prd);
                            }
                            else
                            {
                                result = productionRequestDetailService.UpdateProductionRequestDetail(prd);
                            }
                        }
                        

                    }
                    foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                    {
                        if (prd.ProductId > 0 && prd.AttributeId > 0)
                        {
                            if (prd.Id == 0)
                            {
                                prd.ProductionRequestId = productionRequest.Id;
                                result = productionRequestDetailService.AddProductionRequestDetail(prd);
                            }
                            else
                            {
                                result = productionRequestDetailService.UpdateProductionRequestDetail(prd);
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
        
    }
}
