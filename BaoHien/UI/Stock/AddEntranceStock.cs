﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DAL;
using BaoHien.Model;
using BaoHien.Services.ProductAttributes;
using BaoHien.Services;
using BaoHien.Services.ProductInStocks;
using BaoHien.Common;
using BaoHien.UI.Base;
using DAL.Helper;
using BaoHien.Services.MeasurementUnits;
using BaoHien.Services.Seeds;
using BaoHien.Services.ProductLogs;

namespace BaoHien.UI
{
    public partial class AddEntranceStock : BaseForm
    {
        bool isUpdating = false;
        EntranceStock entranceStock;
        ProductLogService productLogService;
        MeasurementUnitService unitService;
        BindingList<EntranceStockDetail> entranceStockDetails;
        BindingList<ProductAttributeModel> products;
        BindingList<MeasurementUnit> units;
        List<EntranceStockDetail> old_details;

        const int ProductAttrCell = 0, NumberUnitCell = 1, UnitCell = 2, NoteCell = 3;

        public AddEntranceStock()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EntranceStock_Load(object sender, EventArgs e)
        {
            productLogService = new ProductLogService();
            unitService = new MeasurementUnitService();
            loadSomeData();
            SetupColumns();
            updateProductionRequestDetailCells();
        }

        private void loadSomeData()
        {
            ProductAttributeService productAttrService = new ProductAttributeService();
            products = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
            units = new BindingList<MeasurementUnit>(unitService.GetMeasurementUnits());
            if (entranceStock != null)
            {
                txtCode.Text = entranceStock.EntranceCode;
                txtNote.Text = entranceStock.Note;
                txtDate.Text = entranceStock.CreatedDate.ToString(BHConstant.DATE_FORMAT);
                txtUser.Text = entranceStock.SystemUser.FullName;
            }
            else
            {
                if (Global.CurrentUser != null)
                {
                    txtUser.Text = Global.CurrentUser.FullName;
                }                
                txtUser.Enabled = false;
                txtDate.Text = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate().ToString(BHConstant.DATE_FORMAT);
                txtCode.Text = Global.GetTempSeedID(BHConstant.PREFIX_FOR_ENTRANCE);
            }
            txtDate.Enabled = false;
            txtUser.Enabled = false;
        }

        private void SetupColumns()
        {
            dgvStockEntranceDetails.AutoGenerateColumns = false;
            if (entranceStockDetails == null)
            {
                entranceStockDetails = new BindingList<EntranceStockDetail>();
            }
            dgvStockEntranceDetails.DataSource = entranceStockDetails;
            dgvStockEntranceDetails.ReadOnly = false;

            DataGridViewComboBoxColumn productColumn = new DataGridViewComboBoxColumn();
            productColumn.Width = 200;
            productColumn.AutoComplete = false;
            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = products;
            productColumn.DisplayMember = "ProductAttribute";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";
            dgvStockEntranceDetails.Columns.Add(productColumn);

            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();
            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            numberUnitColumn.ValueType = typeof(int);
            dgvStockEntranceDetails.Columns.Add(numberUnitColumn);

            DataGridViewComboBoxColumn unitColumn = new DataGridViewComboBoxColumn();
            unitColumn.Width = 140;
            productColumn.AutoComplete = false;
            unitColumn.HeaderText = "Đơn vị tính";
            unitColumn.DataSource = units;
            unitColumn.DisplayMember = "Name";
            unitColumn.ValueMember = "Id";
            dgvStockEntranceDetails.Columns.Add(unitColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 300;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgvStockEntranceDetails.Columns.Add(noteColumn);
        }

        public void updateProductionRequestDetailCells()
        {
            if (isUpdating && entranceStock != null && entranceStockDetails.Count < dgvStockEntranceDetails.RowCount)
            {
                ProductAttributeService productAttrService = new ProductAttributeService();
                for (int i = 0; i < entranceStockDetails.Count; i++)
                {
                    // Product + Attribute
                    if (dgvStockEntranceDetails.Rows[i].Cells[ProductAttrCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvStockEntranceDetails.Rows[i].Cells[ProductAttrCell];
                        try
                        {
                            pkgBoxCell.Value = productAttrService.GetProductAttribute(entranceStockDetails[i].ProductId,
                                entranceStockDetails[i].AttributeId).Id;
                        }
                        catch { }
                    }
                    // Unit
                    if (dgvStockEntranceDetails.Rows[i].Cells[UnitCell] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvStockEntranceDetails.Rows[i].Cells[UnitCell];
                        try
                        {
                            pkgBoxCell.Value = entranceStockDetails[i].UnitId;
                        }
                        catch { }
                    }
                }
            }
        }

        private void dgvStockEntranceDetails_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (entranceStockDetails == null)
            {
                entranceStockDetails = new BindingList<EntranceStockDetail>();
            }
            if (entranceStockDetails.Count < dgvStockEntranceDetails.RowCount - 1)
            {
                EntranceStockDetail entranceStockDetail = new EntranceStockDetail();
                entranceStockDetails.Add(entranceStockDetail);
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
                                entranceStockDetails[e.RowIndex].ProductId = pa.ProductId;
                                entranceStockDetails[e.RowIndex].AttributeId = pa.AttributeId;
                            }
                        } break;
                    case NumberUnitCell:
                        {
                            int value = (int)dgv.CurrentCell.Value;
                            if (value < 0)
                                entranceStockDetails[e.RowIndex].NumberUnit = 0;
                            else
                                entranceStockDetails[e.RowIndex].NumberUnit = value;
                        } break;
                    case UnitCell:
                        entranceStockDetails[e.RowIndex].UnitId = (int)dgv.CurrentCell.Value;
                        break;
                    case NoteCell:
                        entranceStockDetails[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                        break;
                }
            }
        }

        private void dgvStockEntranceDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgvStockEntranceDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            switch (dgvStockEntranceDetails.CurrentCell.ColumnIndex)
            {
                case ProductAttrCell:
                    {
                        var source = new AutoCompleteStringCollection();
                        String[] stringArray = Array.ConvertAll<ProductAttributeModel, String>(products.ToArray(), delegate(ProductAttributeModel row) { return (String)row.ProductAttribute; });
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
                        //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
                    } break;
                case NumberUnitCell:
                    { 
                        TextBox numberOfUnit = e.Control as TextBox;
                        this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                        this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                    }
                    break;
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
                        //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
                    } break;
                case NoteCell:
                    {
                        if (e.Control is TextBox)
                        {
                            TextBox other = e.Control as TextBox;
                            this.validator1.SetType(other, Itboy.Components.ValidationType.None);
                        }
                    }
                    break;
            }
        }
        
        public void loadDataForEditEntranceStock(int entranceId)
        {
            isUpdating = true;
            this.Text = "Chỉnh sửa phiếu nhập kho";
            this.btnSave.Text = "OK";
            EntranceStockService entranceStockService = new EntranceStockService();
            entranceStock = entranceStockService.GetEntranceStock(entranceId);
            old_details = new List<EntranceStockDetail>();
            if (entranceStock != null)
            {
                if (entranceStockDetails == null)
                {
                    EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();
                    entranceStockDetails = new BindingList<EntranceStockDetail>(entranceStockDetailService.SelectEntranceStockDetailByWhere(o => o.EntranceStockId == entranceStock.Id));
                    foreach (EntranceStockDetail od in entranceStockDetails)
                    {
                        old_details.Add(new EntranceStockDetail
                        {
                            AttributeId = od.AttributeId,
                            ProductId = od.ProductId,
                            UnitId = od.UnitId,
                            EntranceStockId = od.EntranceStockId,
                            NumberUnit = od.NumberUnit
                        });
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.validator1.Validate() && ValidateData())
            {
                DateTime systime = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate();
                int userId = 0;
                if (Global.CurrentUser != null)
                {
                    userId = Global.CurrentUser.Id;
                }
                else
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (entranceStock != null && isUpdating)//update
                {
                    #region Fix Update

                    string msg = "";
                    int error = 0, amount_change = 0;
                    ProductLog pl, newpl;
                    EntranceStockDetail esd;
                    // Check update old data
                    List<EntranceStockDetail> deleted_details = old_details.Where(x => !entranceStockDetails.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    foreach (EntranceStockDetail item in deleted_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        if (pl.AfterNumber - item.NumberUnit < 0)
                        {
                            if (error == 0)
                            {
                                msg += "Những sản phẩm sau đã bị XÓA nhưng không đảm bảo dữ liệu trong kho:\n";
                                error = 1;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " : " + item.NumberUnit + "\n";
                        }
                    }

                    List<EntranceStockDetail> updated_details = old_details.Where(x => entranceStockDetails.Select(y => y.ProductId.ToString() + '_' +
                        y.AttributeId.ToString() + '_' + y.UnitId.ToString()).Contains(x.ProductId.ToString() + '_' +
                        x.AttributeId.ToString() + '_' + x.UnitId.ToString())).ToList();
                    foreach (EntranceStockDetail item in updated_details)
                    {
                        pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                        esd = entranceStockDetails.Where(x => x.ProductId == item.ProductId && x.AttributeId == item.AttributeId &&
                            x.UnitId == item.UnitId).FirstOrDefault();
                        amount_change = Convert.ToInt32(esd.NumberUnit - item.NumberUnit);
                        if (amount_change < 0 && pl.AfterNumber + amount_change < 0) // Giam so luong nhap
                        {
                            if (error < 2)
                            {
                                msg += "Những sản phẩm sau đã bị SỬA nhưng không đảm bảo dữ liệu trong kho:\n";
                                error = 2;
                            }
                            msg += "- " + productLogService.GetNameOfProductLog(pl) + " : " + amount_change.ToString() + "\n";
                        }
                    }

                    if (error > 0)
                    {
                        MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    try
                    {
                        entranceStock.UserId = userId;
                        entranceStock.Note = txtNote.Text;
                        entranceStock.UpdatedDate = systime;

                        EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();
                        foreach (EntranceStockDetail item in deleted_details)
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
                                RecordCode = entranceStock.EntranceCode,
                                Status = BHConstant.ACTIVE_STATUS,
                                Direction = BHConstant.DIRECTION_OUT,
                                UpdatedDate = systime
                            };
                            productLogService.AddProductLog(newpl);
                        }
                        foreach (EntranceStockDetail od in entranceStockDetails)
                        {
                            od.EntranceStockId = entranceStock.Id;
                            if (od.ProductId > 0 && od.AttributeId > 0 && od.UnitId > 0)
                            {
                                EntranceStockDetail tmp_ode = old_details.Where(x => x.ProductId == od.ProductId &&
                                    x.AttributeId == od.AttributeId && x.UnitId == od.UnitId && x.EntranceStockId == entranceStock.Id).FirstOrDefault();
                                if (tmp_ode != null)
                                {
                                    double amount = od.NumberUnit - tmp_ode.NumberUnit;
                                    entranceStockDetailService.UpdateEntranceStockDetail(od);
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
                                            RecordCode = entranceStock.EntranceCode,
                                            Status = BHConstant.ACTIVE_STATUS,
                                            Direction = amount > 0 ? BHConstant.DIRECTION_IN : BHConstant.DIRECTION_OUT,
                                            UpdatedDate = systime
                                        };
                                        productLogService.AddProductLog(newpl);
                                    }
                                }
                                else
                                {
                                    bool ret = (od.Id != null && od.Id > 0) ? entranceStockDetailService.UpdateEntranceStockDetail(od)
                                        : entranceStockDetailService.AddEntranceStockDetail(od);
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
                                        RecordCode = entranceStock.EntranceCode,
                                        Status = BHConstant.ACTIVE_STATUS,
                                        Direction = BHConstant.DIRECTION_IN,
                                        UpdatedDate = systime
                                    };
                                    productLogService.AddProductLog(newpl);
                                }
                            }
                        }

                        EntranceStockService entranceStockService = new EntranceStockService();
                        bool result = entranceStockService.UpdateEntranceStock(entranceStock);

                        if (result)
                            MessageBox.Show("Phiếu nhập kho đã được cập nhật thành công");
                        else
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                    catch { }
                    #endregion
                }
                else//add new
                {
                    #region New

                    SeedService ss = new SeedService();
                    entranceStock = new EntranceStock
                    {
                        EntranceCode = ss.AddSeedID(BHConstant.PREFIX_FOR_ENTRANCE),
                        UserId = userId,
                        CreatedDate = systime,
                        Note = txtNote.Text
                    };
                    EntranceStockService entranceStockService = new EntranceStockService();
                    bool result = entranceStockService.AddEntranceStock(entranceStock);
                    long newOrderId = BaoHienRepository.GetMaxId<EntranceStock>();
                    EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();
                    foreach (EntranceStockDetail od in entranceStockDetails)
                    {
                        if (od.ProductId > 0 && od.AttributeId > 0 && od.UnitId > 0)
                        {
                            od.EntranceStockId = (int)newOrderId;
                            bool ret = entranceStockDetailService.AddEntranceStockDetail(od);
                            if (!ret)
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            //Save in Product Log
                            ProductLog pl = productLogService.GetProductLog(od.ProductId, od.AttributeId, od.UnitId);
                            ProductLog newpl = new ProductLog()
                            {
                                ProductId = od.ProductId,
                                AttributeId = od.AttributeId,
                                UnitId = od.UnitId,
                                BeforeNumber = pl.AfterNumber,
                                Amount = od.NumberUnit,
                                AfterNumber = pl.AfterNumber + od.NumberUnit,
                                RecordCode = entranceStock.EntranceCode,
                                Status = BHConstant.ACTIVE_STATUS,
                                Direction = BHConstant.DIRECTION_IN,
                                UpdatedDate = systime
                            };
                            productLogService.AddProductLog(newpl);
                        }
                    }
                    if (result)
                        MessageBox.Show("Phiếu nhập kho đã được tạo thành công");
                    else
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();

                    #endregion
                }
            }            
        }

        private void btnCreateSP_Click(object sender, EventArgs e)
        {
            AddProduct frmProduct = new AddProduct();
            if (frmProduct.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ProductAttributeService productAttrService = new ProductAttributeService();
                products = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
                ((DataGridViewComboBoxColumn)dgvStockEntranceDetails.Columns[0]).DataSource = products;
            }
        }
        
        private bool ValidateData()
        {
            bool result = true;
            string message = "";
            List<string> errors = new List<string>();
            if (entranceStockDetails.Count <= 0)
            {
                result = false;
                message = "- Không có dữ liệu sản phẩm";
            }
            for (int i = 0; i < entranceStockDetails.Count; i++)
            {
                if ((entranceStockDetails[i].ProductId != 0 && entranceStockDetails[i].AttributeId != 0 &&
                    entranceStockDetails[i].UnitId != 0 && entranceStockDetails[i].NumberUnit > 0) ||
                    (entranceStockDetails[i].ProductId == 0 && entranceStockDetails[i].AttributeId == 0 &&
                    entranceStockDetails[i].UnitId == 0))
                    continue;
                result = false;
                message += "- Dòng " + (i + 1) + " thiếu :";
                if (entranceStockDetails[i].ProductId == 0)
                    errors.Add(" Sản phẩm");
                if (entranceStockDetails[i].NumberUnit <= 0)
                    errors.Add(" Số lượng");
                if (entranceStockDetails[i].UnitId == 0)
                    errors.Add(" Đơn vị tính");
                message += string.Join(",", errors);
                errors.Clear();
                message += "\n";
            }
            if (!result)
                MessageBox.Show(message, "Lỗi nhập kho", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return result;
        }

        void DisableDropperDown(object sender, KeyEventArgs e)
        {
            Global.DisableDropDownWhenSuggesting((ComboBox)sender);
        }
    }
}
