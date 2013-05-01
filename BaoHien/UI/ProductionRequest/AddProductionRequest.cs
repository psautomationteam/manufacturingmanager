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
using BaoHien.Services.ProductInStocks;
using BaoHien.Services.ProductLogs;
using BaoHien.Services.Seeds;

namespace BaoHien.UI
{
    public partial class AddProductionRequest : BaseForm
    {
        bool isUpdating = false;
        ProductionRequest productionRequest;
        ProductLogService productLogService;
        MeasurementUnitService unitService;
        BindingList<MeasurementUnit> units;
        BindingList<ProductionRequestDetail> productionRequestDetailInProductions;
        BindingList<ProductionRequestDetail> productionRequestDetailInMaterials;
        BindingList<ProductAttributeModel> productForProducts;
        BindingList<ProductAttributeModel> productForMaterials;
        BindingList<ProductionRequestDetailModel> originalMaterials;
        BindingList<ProductionRequestDetailModel> originalProductions;

        const int ProductAttrCell = 0, NumberUnitCell = 1, UnitCell = 2, NoteCell = 3;

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
            productColumn.Width = 200;
            productColumn.AutoComplete = false;
            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = productForProducts;
            productColumn.DisplayMember = "ProductAttribute";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";
            dgvMaterial.Columns.Add(productColumn);
            
            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();
            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            //numberUnitColumn.ValueType = typeof(int);
            dgvMaterial.Columns.Add(numberUnitColumn);

            DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
            unitColumn.Width = 140;
            productColumn.AutoComplete = false;
            unitColumn.HeaderText = "Đơn vị tính";
            unitColumn.DataSource = units;
            unitColumn.DisplayMember = "Name";
            unitColumn.ValueMember = "Id";
            dgvMaterial.Columns.Add(unitColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 200;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgvMaterial.Columns.Add(noteColumn);
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
            productColumn.Width = 200;
            productColumn.AutoComplete = false;
            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = productForProducts;
            productColumn.DisplayMember = "ProductAttribute";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";
            dgvProduct.Columns.Add(productColumn);

            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();
            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            //numberUnitColumn.ValueType = typeof(int);
            dgvProduct.Columns.Add(numberUnitColumn);

            DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
            unitColumn.Width = 140;
            productColumn.AutoComplete = false;
            unitColumn.HeaderText = "Đơn vị tính";
            unitColumn.DataSource = units;
            unitColumn.DisplayMember = "Name";
            unitColumn.ValueMember = "Id";
            dgvProduct.Columns.Add(unitColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();           
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 200;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgvProduct.Columns.Add(noteColumn);
        }

        private void AddProductionRequest_Load(object sender, EventArgs e)
        {
            unitService = new MeasurementUnitService();
            productLogService = new ProductLogService();
            loadSomeData();
            SetupColumnsForMaterial();
            SetupColumnsForProductRequest();
            updateProductionRequestDetailCells();
        }

        private void loadSomeData()
        {
            if (productionRequest != null)
            {
                txtDate.Text = productionRequest.RequestedDate.ToString(BHConstant.DATE_FORMAT);
                txtDate.Enabled = false;
                txtUser.Text = productionRequest.SystemUser.FullName;
                txtUser.Enabled = false;
                txtNote.Text = productionRequest.Note;
                txtCode.Text = productionRequest.ReqCode;
            }
            else
            {
                txtDate.Text = DateTime.Now.ToString(BHConstant.DATE_FORMAT);
                txtDate.Enabled = false;
                txtUser.Text = (Global.CurrentUser != null) ? Global.CurrentUser.FullName : "";
                txtUser.Enabled = false;

                SeedService ss = new SeedService();
                txtCode.Text = ss.AddSeedID(BHConstant.PREFIX_FOR_PRODUCTION); //RandomGeneration.GeneratingCode(BHConstant.PREFIX_FOR_PRODUCTION);
            }
            ProductAttributeService productAttrService = new ProductAttributeService();
            productForProducts = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
            productForMaterials = productForProducts;
            units = new BindingList<MeasurementUnit>(unitService.GetMeasurementUnits());
        }

        private void dgvMaterial_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvMaterial.CurrentCell != null)
            {
                switch (dgvMaterial.CurrentCell.ColumnIndex)
                {
                    case ProductAttrCell:
                        {
                            var source = new AutoCompleteStringCollection();
                            String[] stringArray = Array.ConvertAll<ProductAttributeModel, String>(productForProducts.ToArray(), delegate(ProductAttributeModel row) { return (String)row.ProductAttribute; });
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
                        } break;
                    case NumberUnitCell:
                        {
                            TextBox numberOfUnit = e.Control as TextBox;
                            this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                            this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                        } break;
                    case UnitCell:
                        {
                            var source = new AutoCompleteStringCollection();
                            String[] stringArray = Array.ConvertAll<MeasurementUnit, String>(units.ToArray(), delegate(MeasurementUnit row) { return (String)row.Name; });
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
                            //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
                        } break;
                    case NoteCell:
                        {
                            if (e.Control is TextBox)
                            {
                                TextBox other = e.Control as TextBox;
                                this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                            }
                        } break;
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
                switch (e.ColumnIndex)
                {
                    case ProductAttrCell:
                        {
                            ProductAttributeService productAttrService = new ProductAttributeService();
                            ProductAttribute pa = productAttrService.GetProductAttribute((int)dgv.CurrentCell.Value);
                            if (pa != null)
                            {
                                productionRequestDetailInMaterials[e.RowIndex].ProductId = pa.ProductId;
                                productionRequestDetailInMaterials[e.RowIndex].AttributeId = pa.AttributeId;
                            }
                        } break;
                    case NoteCell:
                        productionRequestDetailInMaterials[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                        break;
                }
            }
            calculateNumberUnit(dgv, e.RowIndex, e.ColumnIndex);
        }

        private void dgvMaterial_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgvProduct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvProduct.CurrentCell != null)
            {
                switch (dgvProduct.CurrentCell.ColumnIndex)
                {
                    case ProductAttrCell:
                        {
                            var source = new AutoCompleteStringCollection();
                            String[] stringArray = Array.ConvertAll<ProductAttributeModel, String>(productForProducts.ToArray(), delegate(ProductAttributeModel row) { return (String)row.ProductAttribute; });
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
                        } break;
                    case NumberUnitCell:
                        {
                            TextBox numberOfUnit = e.Control as TextBox;
                            this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                            this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                        } break;
                    case UnitCell:
                        {
                            var source = new AutoCompleteStringCollection();
                            String[] stringArray = Array.ConvertAll<MeasurementUnit, String>(units.ToArray(), delegate(MeasurementUnit row) { return (String)row.Name; });
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
                            //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
                        } break;
                    case NoteCell :
                        {
                            if (e.Control is TextBox)
                            {
                                TextBox other = e.Control as TextBox;
                                this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                            }
                        } break;
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
                switch (e.ColumnIndex)
                {
                    case ProductAttrCell:
                        {
                            ProductAttributeService productAttrService = new ProductAttributeService();
                            ProductAttribute pa = productAttrService.GetProductAttribute((int)dgv.CurrentCell.Value);
                            if (pa != null)
                            {
                                productionRequestDetailInProductions[e.RowIndex].ProductId = pa.ProductId;
                                productionRequestDetailInProductions[e.RowIndex].AttributeId = pa.AttributeId;
                            }
                        } break;
                    case NumberUnitCell:
                        productionRequestDetailInProductions[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
                        break;
                    case UnitCell:
                        productionRequestDetailInProductions[e.RowIndex].UnitId = (int)dgv.CurrentCell.Value;
                        break;
                    case NoteCell:
                        productionRequestDetailInProductions[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                        break;
                }
            }           
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
            if (this.validator1.Validate() && ValidateData())
            {
                DialogResult dialogResult = MessageBox.Show("Bạn muốn lưu?", "Xác nhận", MessageBoxButtons.YesNo);
                if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                {
                    ProductionRequestService prs = new ProductionRequestService();
                    ProductionRequestDetailService productionRequestDetailService = new ProductionRequestDetailService();

                    if (productionRequest != null)
                    {
                        #region Fix Update

                        //bool result = prs.UpdateProductionRequest(productionRequest);
                        //if (result)
                        //{
                        //    // ProductInStock
                        //    foreach (ProductionRequestDetail prd in productionRequestDetailInProductions)
                        //    {
                        //        if (prd.ProductId > 0 && prd.AttributeId > 0)
                        //        {
                        //            if (prd.Id == 0)
                        //            {
                        //                prd.ProductionRequestId = productionRequest.Id;
                        //                result = productionRequestDetailService.AddProductionRequestDetail(prd);

                        //                //Save in Production In Stock
                        //                ProductInStock productInStock = new ProductInStock();
                        //                List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                        //                productInStock.AttributeId = prd.AttributeId;
                        //                productInStock.ProductId = prd.ProductId;
                        //                productInStock.LatestUpdate = DateTime.Now;
                        //                productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                        //                if (lstProductInStock.Count > 0)
                        //                {
                        //                    productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput + prd.NumberUnit;
                        //                    productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput;
                        //                    productInStock.NumberOfItem = (int)productInStock.NumberOfInput;
                        //                }
                        //                else
                        //                {
                        //                    productInStock.NumberOfInput = prd.NumberUnit;
                        //                    productInStock.NumberOfOutput = 0;
                        //                    productInStock.NumberOfItem = (int)prd.NumberUnit;
                        //                }
                        //                pis.AddProductInStock(productInStock);
                        //            }
                        //            else
                        //            {
                        //                ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                        //                original = originalProductions.Where(p => p.Id == prd.Id).ToList().FirstOrDefault();
                        //                result = productionRequestDetailService.UpdateProductionRequestDetail(prd);

                        //                //Update so luong
                        //                if (original != null)
                        //                {
                        //                    if (prd.ProductId == original.ProductId && prd.AttributeId == original.AttributeId && prd.NumberUnit != original.NumberUnit)
                        //                    {
                        //                        ProductInStock productInStock = new ProductInStock();
                        //                        List<ProductInStock> lstProductInStock = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);

                        //                        productInStock.AttributeId = prd.AttributeId;
                        //                        productInStock.ProductId = prd.ProductId;
                        //                        productInStock.LatestUpdate = DateTime.Now;
                        //                        productInStock.NumberOfInput = lstProductInStock.Last<ProductInStock>().NumberOfInput + prd.NumberUnit - original.NumberUnit;
                        //                        productInStock.NumberOfOutput = lstProductInStock.Last<ProductInStock>().NumberOfOutput;
                        //                        productInStock.NumberOfItem = lstProductInStock.Last<ProductInStock>().NumberOfItem + prd.NumberUnit - original.NumberUnit;
                        //                        productInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;
                        //                        pis.UpdateProductInStock(productInStock);
                        //                    }

                        //                    //Sua chi tiet phieu
                        //                    else if (prd.ProductId != original.ProductId || prd.AttributeId != original.AttributeId)
                        //                    {

                        //                        //Tao moi
                        //                        List<ProductInStock> lstNewProduct = pis.SelectProductByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                        //                        ProductInStock newProductInStock = new ProductInStock();
                        //                        newProductInStock.AttributeId = prd.AttributeId;
                        //                        newProductInStock.ProductId = prd.ProductId;
                        //                        newProductInStock.LatestUpdate = DateTime.Now;
                        //                        newProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;

                        //                        if (lstNewProduct.Count > 0)
                        //                        {
                        //                            newProductInStock.NumberOfInput = lstNewProduct.Last<ProductInStock>().NumberOfInput + prd.NumberUnit;
                        //                            newProductInStock.NumberOfOutput = lstNewProduct.Last<ProductInStock>().NumberOfOutput;
                        //                            newProductInStock.NumberOfItem = lstNewProduct.Last<ProductInStock>().NumberOfItem + prd.NumberUnit;
                        //                        }
                        //                        else
                        //                        {
                        //                            newProductInStock.NumberOfInput = prd.NumberUnit;
                        //                            newProductInStock.NumberOfOutput = 0;
                        //                            newProductInStock.NumberOfItem = prd.NumberUnit;
                        //                        }
                        //                        pis.AddProductInStock(newProductInStock);

                        //                        //Xoa cu
                        //                        List<ProductInStock> lstOldProduct = pis.SelectProductByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);
                        //                        if (lstOldProduct.Count > 0)
                        //                        {
                        //                            ProductInStock oldProductInStock = new ProductInStock();
                        //                            oldProductInStock.AttributeId = original.AttributeId;
                        //                            oldProductInStock.ProductId = original.ProductId;
                        //                            oldProductInStock.LatestUpdate = DateTime.Now;
                        //                            oldProductInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                        //                            newProductInStock.NumberOfInput = lstNewProduct.Last<ProductInStock>().NumberOfInput - original.NumberUnit;
                        //                            newProductInStock.NumberOfOutput = lstNewProduct.Last<ProductInStock>().NumberOfOutput;
                        //                            newProductInStock.NumberOfItem = lstNewProduct.Last<ProductInStock>().NumberOfItem - original.NumberUnit;
                        //                            pis.AddProductInStock(newProductInStock);
                        //                        }
                        //                    }
                        //                }
                        //            }

                        //        }
                        //    }

                        //    //MaterialInStock
                        //    foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                        //    {
                        //        if (prd.ProductId > 0 && prd.AttributeId > 0)
                        //        {
                        //            if (prd.Id == 0)
                        //            {
                        //                prd.ProductionRequestId = productionRequest.Id;
                        //                result = productionRequestDetailService.AddProductionRequestDetail(prd);

                        //                //Save in Materail In Stock
                        //                MaterialInStock materialInStock = new MaterialInStock();
                        //                List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                        //                if (lstMaterial != null)
                        //                {
                        //                    materialInStock.AttributeId = prd.AttributeId;
                        //                    materialInStock.ProductId = prd.ProductId;
                        //                    materialInStock.LatestUpdate = DateTime.Now;
                        //                    materialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                        //                    materialInStock.NumberOfInput = lstMaterial.Last<MaterialInStock>() != null?lstMaterial.Last<MaterialInStock>().NumberOfInput: 0;
                        //                    materialInStock.NumberOfOutput = lstMaterial.Last<MaterialInStock>() != null ?lstMaterial.Last<MaterialInStock>().NumberOfOutput + prd.NumberUnit: 0;
                        //                    materialInStock.NumberOfItem = lstMaterial.Last<MaterialInStock>() != null ?lstMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit:0;
                        //                    mis.AddMaterialInStock(materialInStock);
                        //                }
                        //            }
                        //            else
                        //            {
                        //                ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                        //                original = originalProductions.Where(p => p.Id == prd.Id).ToList().FirstOrDefault();
                        //                result = productionRequestDetailService.UpdateProductionRequestDetail(prd);

                        //                if (original != null)
                        //                {
                        //                    if (prd.ProductId == original.ProductId && prd.AttributeId == original.AttributeId && prd.NumberUnit != original.NumberUnit)
                        //                    {

                        //                        //Update Qty
                        //                        List<MaterialInStock> lstNewMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                        //                        MaterialInStock newMaterialInStock = new MaterialInStock();
                        //                        newMaterialInStock.AttributeId = prd.AttributeId;
                        //                        newMaterialInStock.ProductId = prd.ProductId;
                        //                        newMaterialInStock.LatestUpdate = DateTime.Now;
                        //                        newMaterialInStock.NumberOfInput = lstNewMaterial.Last<MaterialInStock>().NumberOfInput;
                        //                        newMaterialInStock.NumberOfOutput = lstNewMaterial.Last<MaterialInStock>().NumberOfOutput - prd.NumberUnit;
                        //                        newMaterialInStock.NumberOfItem = lstNewMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit;
                        //                        newMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                        //                        mis.AddMaterialInStock(newMaterialInStock);
                        //                    }
                        //                    else if (prd.ProductId != original.ProductId || prd.AttributeId != original.AttributeId || prd.NumberUnit != original.NumberUnit)
                        //                    {

                        //                        //Tao moi
                        //                        List<MaterialInStock> lstNewMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == prd.ProductId && pt.AttributeId == prd.AttributeId);
                        //                        MaterialInStock newMaterialInStock = new MaterialInStock();
                        //                        newMaterialInStock.AttributeId = prd.AttributeId;
                        //                        newMaterialInStock.ProductId = prd.ProductId;
                        //                        newMaterialInStock.LatestUpdate = DateTime.Now;
                        //                        newMaterialInStock.NumberOfInput = lstNewMaterial.Last<MaterialInStock>().NumberOfInput;
                        //                        newMaterialInStock.NumberOfOutput = lstNewMaterial.Last<MaterialInStock>().NumberOfOutput - prd.NumberUnit;
                        //                        newMaterialInStock.NumberOfItem = lstNewMaterial.Last<MaterialInStock>().NumberOfItem - prd.NumberUnit;
                        //                        newMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_OUTPUT;
                        //                        mis.AddMaterialInStock(newMaterialInStock);

                        //                        //Xoa Cu
                        //                        List<MaterialInStock> lstOldMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);
                        //                        MaterialInStock oldMaterialInStock = new MaterialInStock();
                        //                        oldMaterialInStock.AttributeId = prd.AttributeId;
                        //                        oldMaterialInStock.ProductId = prd.ProductId;
                        //                        oldMaterialInStock.LatestUpdate = DateTime.Now;
                        //                        oldMaterialInStock.NumberOfInput = lstOldMaterial.Last<MaterialInStock>().NumberOfInput;
                        //                        oldMaterialInStock.NumberOfOutput = lstOldMaterial.Last<MaterialInStock>().NumberOfOutput - original.NumberUnit;
                        //                        oldMaterialInStock.NumberOfItem = lstOldMaterial.Last<MaterialInStock>().NumberOfItem + original.NumberUnit;
                        //                        oldMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                        //                        mis.AddMaterialInStock(oldMaterialInStock);
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}

                        //if (result)
                        //{
                        //    MessageBox.Show("Phiếu sản xuất đã được cập nhật thành công");
                        //    if (this.CallFromUserControll != null && this.CallFromUserControll is ProductionRequestList)
                        //    {
                        //        ((ProductionRequestList)this.CallFromUserControll).loadProductionRequestList();
                        //    }
                        //    this.Close();
                        //}
                        //else
                        //{
                        //    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        //    return;
                        //}

                        #endregion

                        this.Close();
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
                                if (prd.ProductId > 0 && prd.AttributeId > 0 && prd.UnitId > 0)
                                {
                                    prd.ProductionRequestId = (int)newProductionRequestId;
                                    bool ret = productionRequestDetailService.AddProductionRequestDetail(prd);
                                    if (!ret)
                                    {
                                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                        return;
                                    }

                                    //Save in Production Log
                                    ProductLog pl = productLogService.GetNewestProductUnitLog(prd.ProductId, prd.AttributeId, prd.UnitId);
                                    ProductLog plg = new ProductLog
                                    {
                                        AttributeId = prd.AttributeId,
                                        ProductId = prd.ProductId,
                                        UnitId = prd.UnitId,
                                        RecordCode = productionRequest.ReqCode,
                                        BeforeNumber = pl.AfterNumber,
                                        Amount = prd.NumberUnit,
                                        AfterNumber = pl.AfterNumber + prd.NumberUnit,
                                        CreatedDate = DateTime.Now
                                    };
                                    result = productLogService.AddProductLog(plg);
                                }
                            }

                            foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                            {
                                if (prd.ProductId > 0 && prd.AttributeId > 0 && prd.UnitId > 0)
                                {
                                    prd.ProductionRequestId = (int)newProductionRequestId;
                                    bool ret = productionRequestDetailService.AddProductionRequestDetail(prd);
                                    if (!ret)
                                    {
                                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                        return;
                                    }

                                    //Save in Product Log
                                    ProductLog pl = productLogService.GetNewestProductUnitLog(prd.ProductId, prd.AttributeId, prd.UnitId);
                                    ProductLog plg = new ProductLog
                                    {
                                        AttributeId = prd.AttributeId,
                                        ProductId = prd.ProductId,
                                        UnitId = prd.UnitId,
                                        RecordCode = productionRequest.ReqCode,
                                        BeforeNumber = pl.AfterNumber,
                                        Amount = prd.NumberUnit,
                                        AfterNumber = pl.AfterNumber - prd.NumberUnit,
                                        CreatedDate = DateTime.Now
                                    };
                                    result = productLogService.AddProductLog(plg);
                                }
                            }
                            MessageBox.Show("Phiếu sản xuất đã được tạo thành công");
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
            this.Text = "Xem thông tin phiếu sản xuất";
            isUpdating = true;
            this.btnSave.Enabled = false;
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
            ProductAttributeService productAttrService = new ProductAttributeService();
            if (isUpdating && productionRequest != null && productionRequestDetailInProductions.Count < dgvProduct.RowCount)
            {
                for (int i = 0; i < productionRequestDetailInProductions.Count; i++)
                {
                    if (dgvProduct.Rows[i].Cells[ProductAttrCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvProduct.Rows[i].Cells[ProductAttrCell];
                        try
                        {
                            pkgBoxCell.Value = productAttrService.GetProductAttribute(productionRequestDetailInProductions[i].ProductId,
                                productionRequestDetailInProductions[i].AttributeId).Id;
                        }
                        catch { }
                    }
                    // Unit
                    if (dgvProduct.Rows[i].Cells[UnitCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvProduct.Rows[i].Cells[UnitCell];
                        try
                        {
                            pkgBoxCell.Value = productionRequestDetailInProductions[i].UnitId;
                        }
                        catch { }
                    }
                }
            }
            if (isUpdating && productionRequest != null && productionRequestDetailInMaterials.Count < dgvMaterial.RowCount)
            {
                for (int i = 0; i < productionRequestDetailInMaterials.Count; i++)
                {
                    if (dgvMaterial.Rows[i].Cells[ProductAttrCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvMaterial.Rows[i].Cells[ProductAttrCell];
                        try
                        {
                            pkgBoxCell.Value = productAttrService.GetProductAttribute(productionRequestDetailInMaterials[i].ProductId,
                                productionRequestDetailInMaterials[i].AttributeId).Id;
                        }
                        catch { }
                    }
                    // Unit
                    if (dgvMaterial.Rows[i].Cells[UnitCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvMaterial.Rows[i].Cells[UnitCell];
                        try
                        {
                            pkgBoxCell.Value = productionRequestDetailInMaterials[i].UnitId;
                        }
                        catch { }
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

        private void calculateNumberUnit(DataGridView dgv, int rowIndex, int colIndex)
        {
            if (dgv.Rows[rowIndex].Cells[UnitCell].Value != null &&
                dgv.Rows[rowIndex].Cells[NumberUnitCell].Value != null &&
                (colIndex == UnitCell || colIndex == NumberUnitCell || colIndex == ProductAttrCell))
            {
                ProductLog pl = productLogService.GetNewestProductUnitLog(
                    productionRequestDetailInMaterials[rowIndex].ProductId,
                    productionRequestDetailInMaterials[rowIndex].AttributeId,
                    (int)dgv.Rows[rowIndex].Cells[UnitCell].Value);
                if (pl.Id == 0)
                {
                    MessageBox.Show("Sản phẩm với đơn vị tính này hiện chưa có trong kho.");
                    dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = 0;

                    productionRequestDetailInMaterials[rowIndex].UnitId = (int)dgv.Rows[rowIndex].Cells[UnitCell].Value;
                    productionRequestDetailInMaterials[rowIndex].NumberUnit = 0;
                }
                else
                {
                    if (pl.AfterNumber <= 0)
                    {
                        MessageBox.Show("Số lượng sản phẩm trong kho đã hết.");
                        dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = 0;

                        productionRequestDetailInMaterials[rowIndex].UnitId = (int)dgv.Rows[rowIndex].Cells[UnitCell].Value;
                        productionRequestDetailInMaterials[rowIndex].NumberUnit = 0;
                    }
                    else if (pl.AfterNumber < (int)dgv.Rows[rowIndex].Cells[NumberUnitCell].Value)
                    {
                        MessageBox.Show("Số lượng sản phẩm trong kho còn lại là : " + pl.AfterNumber);
                        dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = pl.AfterNumber;

                        productionRequestDetailInMaterials[rowIndex].UnitId = (int)dgv.Rows[rowIndex].Cells[UnitCell].Value;
                        productionRequestDetailInMaterials[rowIndex].NumberUnit = (int)pl.AfterNumber;
                    }
                    else
                    {
                        productionRequestDetailInMaterials[rowIndex].UnitId = (int)dgv.Rows[rowIndex].Cells[UnitCell].Value;
                        productionRequestDetailInMaterials[rowIndex].NumberUnit = (int)dgv.Rows[rowIndex].Cells[NumberUnitCell].Value;
                    }
                }
            }
        }
        
        private bool ValidateData()
        {
            bool result = true;
            bool isFirst = true;
            string message = "";
            List<string> errors = new List<string>();
            if (productionRequestDetailInMaterials.Count <= 0)
            {
                result = false;
                message += "- Không có dữ liệu sản phẩm bên phần nguyên liệu";
            }
            for (int i = 0; i < productionRequestDetailInMaterials.Count; i++)
            {
                if ((productionRequestDetailInMaterials[i].ProductId != 0 && productionRequestDetailInMaterials[i].AttributeId != 0 &&
                    productionRequestDetailInMaterials[i].UnitId != 0 && productionRequestDetailInMaterials[i].NumberUnit > 0) ||
                    (productionRequestDetailInMaterials[i].ProductId == 0 && productionRequestDetailInMaterials[i].AttributeId == 0 &&
                    productionRequestDetailInMaterials[i].UnitId == 0))
                    continue;
                result = false;
                if (isFirst)
                {
                    message += "PHẦN NGUYÊN LIỆU\n";
                    isFirst = false;
                }
                message += "- Dòng " + (i + 1) + " thiếu :";
                if (productionRequestDetailInMaterials[i].ProductId == 0)
                    errors.Add(" Sản phẩm");
                if (productionRequestDetailInMaterials[i].NumberUnit <= 0)
                    errors.Add(" Số lượng");
                if (productionRequestDetailInMaterials[i].UnitId == 0)
                    errors.Add(" Đơn vị tính");
                message += string.Join(",", errors);
                errors.Clear();
                message += "\n";
            }
            isFirst = true;
            if (productionRequestDetailInProductions.Count <= 0)
            {
                result = false;
                message += "\n- Không có dữ liệu sản phẩm bên phần thành phẩm";
            }
            for (int i = 0; i < productionRequestDetailInProductions.Count; i++)
            {
                if ((productionRequestDetailInProductions[i].ProductId != 0 && productionRequestDetailInProductions[i].AttributeId != 0 &&
                    productionRequestDetailInProductions[i].UnitId != 0 && productionRequestDetailInProductions[i].NumberUnit > 0) ||
                    (productionRequestDetailInProductions[i].ProductId == 0 && productionRequestDetailInProductions[i].AttributeId == 0 &&
                    productionRequestDetailInProductions[i].UnitId == 0))
                    continue;
                result = false;
                if (isFirst)
                {
                    message += "PHẦN THÀNH PHẨM\n";
                    isFirst = false;
                }
                message += "- Dòng " + (i + 1) + " thiếu :";
                if (productionRequestDetailInProductions[i].ProductId == 0)
                    errors.Add(" Sản phẩm");
                if (productionRequestDetailInProductions[i].NumberUnit <= 0)
                    errors.Add(" Số lượng");
                if (productionRequestDetailInProductions[i].UnitId == 0)
                    errors.Add(" Đơn vị tính");
                message += string.Join(",", errors);
                errors.Clear();
                message += "\n";
            }
            if (!result)
                MessageBox.Show(message, "Lỗi phiếu sản xuất", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        private void btnCreateNK_Click(object sender, EventArgs e)
        {
            AddEntranceStock frmStock = new AddEntranceStock();
            frmStock.ShowDialog();
        }
    }
}
