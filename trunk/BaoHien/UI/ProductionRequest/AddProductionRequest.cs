using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DAL;
using BaoHien.Services.MeasurementUnits;
using BaoHien.Services.ProductAttributes;
using BaoHien.Common;
using BaoHien.Services.ProductionRequests;
using DAL.Helper;
using BaoHien.Services.ProductionRequestDetails;
using BaoHien.UI.Base;
using BaoHien.Model;
using BaoHien.Services.Seeds;
using BaoHien.Services.ProductLogs;

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
        List<ProductionRequestDetail> old_marterial_details;
        List<ProductionRequestDetail> old_production_details;

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
            dgvMaterial.DataSource = productionRequestDetailInMaterials;

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
            dgvProduct.DataSource = productionRequestDetailInProductions;
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
                txtDate.Text = productionRequest.CreatedDate.ToString(BHConstant.DATE_FORMAT);
                txtDate.Enabled = false;
                txtUser.Text = productionRequest.SystemUser.FullName;
                txtUser.Enabled = false;
                txtNote.Text = productionRequest.Note;
                txtCode.Text = productionRequest.ReqCode;
            }
            else
            {
                txtDate.Text = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATE_FORMAT);
                txtDate.Enabled = false;
                txtUser.Text = (Global.CurrentUser != null) ? Global.CurrentUser.FullName : "";
                txtUser.Enabled = false;

                txtCode.Text = Global.GetTempSeedID(BHConstant.PREFIX_FOR_PRODUCTION);
            }
            ProductAttributeService productAttrService = new ProductAttributeService();
            productForProducts = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
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
                                prodCode.KeyDown += DisableDropperDown;
                            }
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
                                prodCode.KeyDown += DisableDropperDown;
                            }
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

        private void dgvMaterial_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (productionRequestDetailInMaterials == null)
            {
                productionRequestDetailInMaterials = new BindingList<ProductionRequestDetail>();
            }
            if (productionRequestDetailInMaterials.Count < dgvMaterial.RowCount - 1)
            {
                ProductionRequestDetail productionRequestDetail = new ProductionRequestDetail();
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
                                prodCode.KeyDown += DisableDropperDown;
                            }
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
                                prodCode.KeyDown += DisableDropperDown;
                            }
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
            if (productionRequestDetailInProductions.Count < dgvProduct.RowCount - 1)
            {
                ProductionRequestDetail productionRequestDetail = new ProductionRequestDetail();
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

        private void dgvProduct_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.validator1.Validate() && ValidateData())
            {
                ProductionRequestService prs = new ProductionRequestService();
                ProductionRequestDetailService productionRequestDetailService = new ProductionRequestDetailService();
                DateTime systime = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate();
                if (productionRequest != null)
                {
                    #region Fix Update
                        
                    productionRequest.UpdatedDate = systime;
                    productionRequest.Note = txtNote.Text;

                    string msg = "";
                    int error = 0, amount_change = 0;
                    ProductLog pl, newpl;
                    ProductionRequestDetail prd;

                    #region Check old data

                    // Material
                    List<ProductionRequestDetail> deleted_material_details = old_marterial_details.Where(x => !productionRequestDetailInMaterials.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    List<ProductionRequestDetail> updated_material_details = old_marterial_details.Where(x => productionRequestDetailInMaterials.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    List<ProductionRequestDetail> new_material_details = productionRequestDetailInMaterials.Where(x => !old_marterial_details.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    foreach (ProductionRequestDetail item in updated_material_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        prd = productionRequestDetailInMaterials.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId &&
                            x.UnitId == item.UnitId).FirstOrDefault();
                        amount_change = Convert.ToInt32(prd.NumberUnit - item.NumberUnit);
                        if (amount_change > 0 && pl.AfterNumber - amount_change < 0) // Tang nguyen lieu
                        {
                            if (error == 0)
                            {
                                msg += "Phần NGUYÊN LIỆU\n";
                                msg += "Những sản phẩm sau đã bị SỬA nhưng không đảm bảo dữ liệu trong kho:\n";
                                error = 1;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " : " + amount_change.ToString() + "\n";
                        }
                    }
                    foreach (ProductionRequestDetail item in new_material_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        if (pl.AfterNumber - item.NumberUnit < 0)
                        {
                            if (error < 2)
                            {
                                msg += "Phần NGUYÊN LIỆU\n";
                                msg += "Những sản phẩm sau không đủ số lượng để tạo phiếu sản xuất:\n";
                                error = 2;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " còn : " + pl.AfterNumber + "\n";
                        }
                    }

                    // Production
                    List<ProductionRequestDetail> deleted_production_details = old_production_details.Where(x => !productionRequestDetailInProductions.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    List<ProductionRequestDetail> updated_production_details = old_production_details.Where(x => productionRequestDetailInProductions.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    foreach (ProductionRequestDetail item in deleted_production_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        if (pl.AfterNumber - item.NumberUnit < 0)
                        {
                            if (error < 3)
                            {
                                msg += "Phần THÀNH PHẨM\n";
                                msg += "Những sản phẩm sau đã bị XÓA nhưng không đảm bảo dữ liệu trong kho:\n";
                                error = 3;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " : " + item.NumberUnit + "\n";
                        }
                    }
                    foreach (ProductionRequestDetail item in updated_production_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        prd = productionRequestDetailInProductions.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId &&
                            x.UnitId == item.UnitId).FirstOrDefault();
                        amount_change = Convert.ToInt32(prd.NumberUnit - item.NumberUnit);
                        if (amount_change < 0 && pl.AfterNumber + amount_change < 0) // Giam so luong thanh pham
                        {
                            if (error < 4)
                            {
                                msg += "Phần THÀNH PHẨM\n";
                                msg += "Những sản phẩm sau đã bị SỬA nhưng không đảm bảo dữ liệu trong kho:\n";
                                error = 4;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " : " + amount_change.ToString() + "\n";
                        }
                    }

                    if (error > 0)
                    {
                        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    #endregion

                    #region Update Marterial

                    foreach (ProductionRequestDetail item in deleted_material_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        newpl = new ProductLog()
                        {
                            ProductId = item.ProductId,
                            AttributeId = item.AttributeId,
                            UnitId = item.UnitId,
                            BeforeNumber = pl.AfterNumber,
                            Amount = item.NumberUnit,
                            AfterNumber = pl.AfterNumber + item.NumberUnit,
                            RecordCode = productionRequest.ReqCode,
                            Status = BHConstant.ACTIVE_STATUS,
                            Direction = BHConstant.DIRECTION_IN,
                            UpdatedDate = systime
                        };
                        productLogService.AddProductLog(newpl);
                    }
                    foreach (ProductionRequestDetail od in productionRequestDetailInMaterials)
                    {
                        od.ProductionRequestId = productionRequest.Id;
                        od.Direction = BHConstant.DIRECTION_OUT;
                        if (od.ProductId > 0 && od.AttributeId > 0 && od.UnitId > 0)
                        {
                            ProductionRequestDetail tmp_ode = old_marterial_details.Where(x => x.ProductId == od.ProductId &&
                                x.AttributeId == od.AttributeId && x.UnitId == od.UnitId && x.ProductionRequestId == productionRequest.Id).FirstOrDefault();
                            if (tmp_ode != null)
                            {
                                double amount = tmp_ode.NumberUnit - od.NumberUnit;
                                productionRequestDetailService.UpdateProductionRequestDetail(od);
                                //Save in Production Log
                                if (amount != 0)
                                {
                                    pl = productLogService.GetProductLog(od.ProductId, od.AttributeId, od.UnitId);
                                    newpl = new ProductLog()
                                    {
                                        ProductId = od.ProductId,
                                        AttributeId = od.AttributeId,
                                        UnitId = od.UnitId,
                                        BeforeNumber = pl.AfterNumber,
                                        Amount = Math.Abs(amount),
                                        AfterNumber = pl.AfterNumber + amount,
                                        RecordCode = productionRequest.ReqCode,
                                        Status = BHConstant.ACTIVE_STATUS,
                                        Direction = amount > 0 ? BHConstant.DIRECTION_IN : BHConstant.DIRECTION_OUT,
                                        UpdatedDate = systime
                                    };
                                    productLogService.AddProductLog(newpl);
                                }
                            }
                            else
                            {
                                bool ret = (od.Id != null && od.Id > 0) ? productionRequestDetailService.UpdateProductionRequestDetail(od) 
                                    : productionRequestDetailService.AddProductionRequestDetail(od);
                                //Save in Production Log
                                pl = productLogService.GetProductLog(od.ProductId, od.AttributeId, od.UnitId);
                                newpl = new ProductLog()
                                {
                                    ProductId = od.ProductId,
                                    AttributeId = od.AttributeId,
                                    UnitId = od.UnitId,
                                    BeforeNumber = pl.AfterNumber,
                                    Amount = od.NumberUnit,
                                    AfterNumber = pl.AfterNumber - od.NumberUnit,
                                    RecordCode = productionRequest.ReqCode,
                                    Status = BHConstant.ACTIVE_STATUS,
                                    Direction = BHConstant.DIRECTION_OUT,
                                    UpdatedDate = systime
                                };
                                productLogService.AddProductLog(newpl);
                            }
                        }
                    }

                    #endregion

                    #region Update Production

                    foreach (ProductionRequestDetail item in deleted_production_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        newpl = new ProductLog()
                        {
                            ProductId = item.ProductId,
                            AttributeId = item.AttributeId,
                            UnitId = item.UnitId,
                            BeforeNumber = pl.AfterNumber,
                            Amount = item.NumberUnit,
                            AfterNumber = pl.AfterNumber - item.NumberUnit,
                            RecordCode = productionRequest.ReqCode,
                            Status = BHConstant.ACTIVE_STATUS,
                            Direction = BHConstant.DIRECTION_OUT,
                            UpdatedDate = systime
                        };
                        productLogService.AddProductLog(newpl);
                    }
                    foreach (ProductionRequestDetail od in productionRequestDetailInProductions)
                    {
                        od.ProductionRequestId = productionRequest.Id;
                        od.Direction = BHConstant.DIRECTION_IN;
                        if (od.ProductId > 0 && od.AttributeId > 0 && od.UnitId > 0)
                        {
                            ProductionRequestDetail tmp_ode = old_production_details.Where(x => x.ProductId == od.ProductId &&
                                x.AttributeId == od.AttributeId && x.UnitId == od.UnitId && x.ProductionRequestId == productionRequest.Id).FirstOrDefault();
                            if (tmp_ode != null)
                            {
                                double amount = od.NumberUnit - tmp_ode.NumberUnit;
                                productionRequestDetailService.UpdateProductionRequestDetail(od);
                                //Save in Production Log
                                if (amount != 0)
                                {
                                    pl = productLogService.GetProductLog(od.ProductId, od.AttributeId, od.UnitId);
                                    newpl = new ProductLog()
                                    {
                                        ProductId = od.ProductId,
                                        AttributeId = od.AttributeId,
                                        UnitId = od.UnitId,
                                        BeforeNumber = pl.AfterNumber,
                                        Amount = Math.Abs(amount),
                                        AfterNumber = pl.AfterNumber + amount,
                                        RecordCode = productionRequest.ReqCode,
                                        Status = BHConstant.ACTIVE_STATUS,
                                        Direction = amount > 0 ? BHConstant.DIRECTION_IN : BHConstant.DIRECTION_OUT,
                                        UpdatedDate = systime
                                    };
                                    productLogService.AddProductLog(newpl);
                                }
                            }
                            else
                            {
                                bool ret = (od.Id != null && od.Id > 0) ? productionRequestDetailService.UpdateProductionRequestDetail(od) 
                                    : productionRequestDetailService.AddProductionRequestDetail(od);
                                //Save in Production Log
                                pl = productLogService.GetProductLog(od.ProductId, od.AttributeId, od.UnitId);
                                newpl = new ProductLog()
                                {
                                    ProductId = od.ProductId,
                                    AttributeId = od.AttributeId,
                                    UnitId = od.UnitId,
                                    BeforeNumber = pl.AfterNumber,
                                    Amount = od.NumberUnit,
                                    AfterNumber = pl.AfterNumber + od.NumberUnit,
                                    RecordCode = productionRequest.ReqCode,
                                    Status = BHConstant.ACTIVE_STATUS,
                                    Direction = BHConstant.DIRECTION_IN,
                                    UpdatedDate = systime
                                };
                                productLogService.AddProductLog(newpl);
                            }
                        }
                    }

                    #endregion

                    bool result = prs.UpdateProductionRequest(productionRequest);
                    if (result)
                        MessageBox.Show("Phiếu sản xuất đã được cập nhật thành công");
                    else
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();

                    #endregion
                }
                else
                {
                    #region New

                    int userId = 0;
                    ProductLog pl, newpl;
                    if (Global.CurrentUser != null)
                    {
                        userId = Global.CurrentUser.Id;
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string msg = "";
                    int error = 0;
                    foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                    {
                        pl = productLogService.GetProductLog(prd.ProductId, prd.AttributeId, prd.UnitId);
                        if (pl.AfterNumber - prd.NumberUnit < 0)
                        {
                            if (error == 0)
                            {
                                msg += "Những sản phẩm sau bên phần nguyên liệu không đủ số lượng để tạo phiếu sản xuất:\n";
                                error = 1;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " còn : " + pl.AfterNumber + "\n";
                        }
                    }

                    if (error > 0)
                    {
                        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    SeedService ss = new SeedService();
                    productionRequest = new ProductionRequest
                    {
                        Note = txtNote.Text,
                        ReqCode = ss.AddSeedID(BHConstant.PREFIX_FOR_PRODUCTION),
                        CreatedDate = systime,
                        UserId = userId,
                    };

                    bool result = prs.AddProductionRequest(productionRequest);
                    if (result)
                    {
                        long newProductionRequestId = BaoHienRepository.GetMaxId<ProductionRequest>();
                        try
                        {
                            foreach (ProductionRequestDetail prd in productionRequestDetailInProductions)
                            {
                                prd.Direction = BHConstant.DIRECTION_IN;
                                if (prd.ProductId > 0 && prd.AttributeId > 0 && prd.UnitId > 0)
                                {
                                    prd.ProductionRequestId = (int)newProductionRequestId;
                                    prd.Direction = BHConstant.DIRECTION_IN;
                                    productionRequestDetailService.AddProductionRequestDetail(prd);
                                    //Save in Production Log
                                    pl = productLogService.GetProductLog(prd.ProductId, prd.AttributeId, prd.UnitId);
                                    newpl = new ProductLog()
                                    {
                                        ProductId = prd.ProductId,
                                        AttributeId = prd.AttributeId,
                                        UnitId = prd.UnitId,
                                        BeforeNumber = pl.AfterNumber,
                                        Amount = prd.NumberUnit,
                                        AfterNumber = pl.AfterNumber + prd.NumberUnit,
                                        RecordCode = productionRequest.ReqCode,
                                        Status = BHConstant.ACTIVE_STATUS,
                                        Direction = BHConstant.DIRECTION_IN,
                                        UpdatedDate = systime
                                    };
                                    productLogService.AddProductLog(newpl);
                                }
                            }

                            foreach (ProductionRequestDetail prd in productionRequestDetailInMaterials)
                            {
                                prd.Direction = BHConstant.DIRECTION_OUT;
                                if (prd.ProductId > 0 && prd.AttributeId > 0 && prd.UnitId > 0)
                                {
                                    prd.ProductionRequestId = (int)newProductionRequestId;
                                    prd.Direction = BHConstant.DIRECTION_OUT;
                                    productionRequestDetailService.AddProductionRequestDetail(prd);

                                    //Save in Product Log
                                    pl = productLogService.GetProductLog(prd.ProductId, prd.AttributeId, prd.UnitId);
                                    newpl = new ProductLog()
                                    {
                                        ProductId = prd.ProductId,
                                        AttributeId = prd.AttributeId,
                                        UnitId = prd.UnitId,
                                        BeforeNumber = pl.AfterNumber,
                                        Amount = prd.NumberUnit,
                                        AfterNumber = pl.AfterNumber - prd.NumberUnit,
                                        RecordCode = productionRequest.ReqCode,
                                        Status = BHConstant.ACTIVE_STATUS,
                                        Direction = BHConstant.DIRECTION_OUT,
                                        UpdatedDate = systime
                                    };
                                    productLogService.AddProductLog(newpl);
                                }
                            }
                        }
                        catch { }
                        MessageBox.Show("Phiếu sản xuất đã được tạo thành công");
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    #endregion
                }
            } 
        }

        public void loadDataForEditProductRequest(int productionRequestId)
        {
            this.Text = "Chỉnh sửa phiếu sản xuất";
            isUpdating = true;
            ProductionRequestService prs = new ProductionRequestService();
            productionRequest = prs.GetProductionRequest(productionRequestId);
            old_marterial_details = new List<ProductionRequestDetail>();
            old_production_details = new List<ProductionRequestDetail>();
            if (productionRequest != null)
            {
                ProductionRequestDetailService prds = new ProductionRequestDetailService();
                productionRequestDetailInProductions = new BindingList<ProductionRequestDetail>(prds.GetProductionRequestDetails()
                    .Where(p => p.ProductionRequestId == productionRequestId && p.Direction == BHConstant.PRODUCTION_REQUEST_DETAIL_OUT).ToList());
                foreach (ProductionRequestDetail od in productionRequestDetailInProductions)
                {
                    old_production_details.Add(new ProductionRequestDetail
                    {
                        AttributeId = od.AttributeId,
                        ProductId = od.ProductId,
                        UnitId = od.UnitId,
                        ProductionRequestId = od.ProductionRequestId,
                        NumberUnit = od.NumberUnit
                    });
                }
                productionRequestDetailInMaterials = new BindingList<ProductionRequestDetail>(prds.GetProductionRequestDetails()
                    .Where(p => p.ProductionRequestId == productionRequestId && p.Direction == BHConstant.PRODUCTION_REQUEST_DETAIL_IN).ToList());
                foreach (ProductionRequestDetail od in productionRequestDetailInMaterials)
                {
                    old_marterial_details.Add(new ProductionRequestDetail
                    {
                        AttributeId = od.AttributeId,
                        ProductId = od.ProductId,
                        UnitId = od.UnitId,
                        ProductionRequestId = od.ProductionRequestId,
                        NumberUnit = od.NumberUnit
                    });
                }
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
                int unitId = (int)dgv.Rows[rowIndex].Cells[UnitCell].Value;
                int numberUnit = (int)dgv.Rows[rowIndex].Cells[NumberUnitCell].Value;
                ProductLog pl = productLogService.GetProductLog(productionRequestDetailInMaterials[rowIndex].ProductId,
                    productionRequestDetailInMaterials[rowIndex].AttributeId, unitId);
                int amount = (int)pl.AfterNumber;
                if (isUpdating)
                {
                    ProductionRequestDetail prd = old_marterial_details.Where(x => x.ProductId == productionRequestDetailInMaterials[rowIndex].ProductId &&
                        x.AttributeId == productionRequestDetailInMaterials[rowIndex].AttributeId && x.UnitId == unitId).FirstOrDefault();
                    amount += prd.NumberUnit;
                }
                if (amount <= 0)
                {
                    MessageBox.Show("Số lượng sản phẩm trong kho đã hết.");
                    dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = 0;

                    productionRequestDetailInMaterials[rowIndex].UnitId = unitId;
                    productionRequestDetailInMaterials[rowIndex].NumberUnit = 0;
                }
                else if (amount < numberUnit)
                {
                    MessageBox.Show("Số lượng sản phẩm trong kho còn lại là : " + amount);
                    dgv.Rows[rowIndex].Cells[NumberUnitCell].Value = amount;

                    productionRequestDetailInMaterials[rowIndex].UnitId = unitId;
                    productionRequestDetailInMaterials[rowIndex].NumberUnit = amount;
                }
                else
                {
                    productionRequestDetailInMaterials[rowIndex].UnitId = unitId;
                    productionRequestDetailInMaterials[rowIndex].NumberUnit = numberUnit ;
                }
            }
        }
        
        private bool ValidateData()
        {
            bool result = true;
            bool isFirst = true;
            string message = "";
            List<string> errors = new List<string>();
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
                MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        private void btnCreateNK_Click(object sender, EventArgs e)
        {
            AddEntranceStock frmStock = new AddEntranceStock();
            frmStock.ShowDialog();
            ProductAttributeService productAttrService = new ProductAttributeService();
            productForProducts = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
            ((DataGridViewComboBoxColumn)dgvMaterial.Columns[0]).DataSource = productForProducts;
            ((DataGridViewComboBoxColumn)dgvProduct.Columns[0]).DataSource = productForProducts;
        }

        void DisableDropperDown(object sender, KeyEventArgs e)
        {
            Global.DisableDropDownWhenSuggesting((ComboBox)sender);
        }
    }
}
