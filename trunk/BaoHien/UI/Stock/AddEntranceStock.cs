using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using BaoHien.Model;
using BaoHien.Services.Products;
using BaoHien.Services.BaseAttributes;
using BaoHien.Services.ProductAttributes;
using BaoHien.Services;
using BaoHien.Services.ProductInStocks;
using BaoHien.Common;
using BaoHien.UI.Base;
using DAL.Helper;
using BaoHien.Services.ProductLogs;

namespace BaoHien.UI
{
    public partial class AddEntranceStock : BaseForm
    {
        bool isUpdating = false;
        EntranceStock entranceStock;
        ProductLogService productLogService;
        BindingList<EntranceStockDetail> entranceStockDetails;
        BindingList<ProductAttributeModel> products;
        BindingList<ProductionRequestDetailModel> originalEntranceDetail;

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
            loadSomeData();
            SetupColumns();
            updateProductionRequestDetailCells();
        }

        private void loadSomeData()
        {
            ProductAttributeService productAttrService = new ProductAttributeService();
            products = new BindingList<ProductAttributeModel>(productAttrService.GetProductAndAttribute());
            if (entranceStock != null)
            {
                txtCode.Text = entranceStock.EntranceCode;
                txtNote.Text = entranceStock.Note;
                txtDate.Text = entranceStock.EntrancedDate.ToShortDateString();
                txtUser.Text = entranceStock.SystemUser.FullName;
            }
            else
            {
                if (Global.CurrentUser != null)
                {
                    txtUser.Text = Global.CurrentUser.FullName;
                }                
                txtUser.Enabled = false;
                txtCode.Text = RandomGeneration.GeneratingCode(BHConstant.PREFIX_FOR_ENTRANCE);
                txtDate.Text = DateTime.Now.ToShortDateString();
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
            var query = from entranceStockDetail in entranceStockDetails
                        select new ProductionRequestDetailModel
                        {
                            Id=entranceStockDetail.Id,
                            ProductId = entranceStockDetail.ProductId,
                            AttributeId = entranceStockDetail.AttributeId,
                            NumberUnit = entranceStockDetail.NumberUnit,
                            Note = entranceStockDetail.Note,                            
                        };
            dgvStockEntranceDetails.DataSource = new BindingList<ProductionRequestDetailModel>(query.ToList());
            originalEntranceDetail = new BindingList<ProductionRequestDetailModel>(query.ToList());
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
                    if (dgvStockEntranceDetails.Rows[i].Cells[0] is DataGridViewComboBoxCell)
                    {
                        DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvStockEntranceDetails.Rows[i].Cells[0];
                        try
                        {
                            pkgBoxCell.Value = productAttrService.GetProductAttribute(entranceStockDetails[i].ProductId,
                                entranceStockDetails[i].AttributeId).Id;
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
            if (entranceStockDetails.Count < dgvStockEntranceDetails.RowCount)
            {
                EntranceStockDetail entranceStockDetail = new EntranceStockDetail();
                entranceStockDetails.Add(entranceStockDetail);
            }
            if (dgv.CurrentCell.Value != null)
            {
                switch (e.ColumnIndex)
                {
                    case 0:
                        {
                            ProductAttributeService productAttrService = new ProductAttributeService();
                            ProductAttribute pa = productAttrService.GetProductAttribute((int)dgv.CurrentCell.Value);
                            if (pa != null)
                            {
                                entranceStockDetails[e.RowIndex].ProductId = pa.ProductId;
                                entranceStockDetails[e.RowIndex].AttributeId = pa.AttributeId;
                            }
                        } break;
                    case 1:
                        entranceStockDetails[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
                        break;
                    case 2:
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
                case 0:
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

                        }
                        //this.validator1.SetType(prodCode, Itboy.Components.ValidationType.Required);
                    } break;
                case 1:
                    { 
                        TextBox numberOfUnit = e.Control as TextBox;
                        this.validator1.SetRegularExpression(numberOfUnit, BHConstant.REGULAR_EXPRESSION_FOR_NUMBER);
                        this.validator1.SetType(numberOfUnit, Itboy.Components.ValidationType.RegularExpression);
                    }
                    break;
                case 2:
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
            this.Text = "Xem thông tin phiếu nhập kho";
            this.btnSave.Text = "OK";
            this.btnSave.Enabled = false;
            EntranceStockService entranceStockService = new EntranceStockService();
            entranceStock = entranceStockService.GetEntranceStock(entranceId);
            if (entranceStock != null)
            {
                if (entranceStockDetails == null)
                {
                    EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();
                    entranceStockDetails = new BindingList<EntranceStockDetail>(entranceStockDetailService.SelectEntranceStockDetailByWhere(o => o.EntranceStockId == entranceStock.Id));
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.validator1.Validate())
            {
                DateTime createdDate = DateTime.Now;
                if (!DateTime.TryParse(txtDate.Text, out createdDate))
                {
                    createdDate = DateTime.Now;
                };
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
                if (entranceStock != null)//update
                {
                    #region Fix Update

                    //entranceStock.EntranceCode = txtCode.Text;
                    //entranceStock.Note = txtNote.Text;

                    //EntranceStockService entranceStockService = new EntranceStockService();
                    //bool result = entranceStockService.UpdateEntranceStock(entranceStock);
                    //if (!result)
                    //{
                    //    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    //    return;
                    //}
                    //else
                    //{

                    //    EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();

                    //    foreach (EntranceStockDetail od in entranceStockDetails)
                    //    {
                    //        if (od.ProductId > 0 && od.AttributeId > 0)
                    //        {
                    //            if (od.Id == 0)
                    //            {
                    //                od.EntranceStockId = entranceStock.Id;
                    //                result = entranceStockDetailService.AddEntranceStockDetail(od);


                    //                //Tao moi
                    //                List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                    //                MaterialInStock materialInStock = new MaterialInStock();
                    //                materialInStock.AttributeId = od.AttributeId;
                    //                materialInStock.ProductId = od.ProductId;
                    //                materialInStock.LatestUpdate = DateTime.Now;
                    //                materialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;
                    //                if (lstMaterial.Count > 0)
                    //                {
                    //                    materialInStock.NumberOfInput = lstMaterial.Last<MaterialInStock>().NumberOfInput + od.NumberUnit;
                    //                    materialInStock.NumberOfOutput = lstMaterial.Last<MaterialInStock>().NumberOfOutput;
                    //                    materialInStock.NumberOfItem = lstMaterial.Last<MaterialInStock>().NumberOfItem + od.NumberUnit;
                    //                }
                    //                else
                    //                {
                    //                    materialInStock.NumberOfInput = od.NumberUnit;
                    //                    materialInStock.NumberOfOutput = 0;
                    //                    materialInStock.NumberOfItem = od.NumberUnit;

                    //                }
                    //                mis.AddMaterialInStock(materialInStock);
                    //            }
                    //            else
                    //            {
                    //                ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                    //                original = originalEntranceDetail.Where(p => p.Id == od.Id).ToList().FirstOrDefault();
                    //                result = entranceStockDetailService.UpdateEntranceStockDetail(od);
                    //                if (original != null)
                    //                {
                    //                    if (od.ProductId == original.ProductId && od.AttributeId == original.AttributeId && od.NumberUnit != original.NumberUnit)
                    //                    {
                    //                        //Sua so luong
                    //                        List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                    //                        MaterialInStock materialInStock = new MaterialInStock();
                    //                        materialInStock.AttributeId = od.AttributeId;
                    //                        materialInStock.ProductId = od.ProductId;
                    //                        materialInStock.LatestUpdate = DateTime.Now;
                    //                        materialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                    //                        materialInStock.NumberOfInput = lstMaterial.Last<MaterialInStock>().NumberOfInput + od.NumberUnit - original.NumberUnit;
                    //                        materialInStock.NumberOfOutput = lstMaterial.Last<MaterialInStock>().NumberOfOutput;
                    //                        materialInStock.NumberOfItem = lstMaterial.Last<MaterialInStock>().NumberOfItem + od.NumberUnit - original.NumberUnit;
                    //                        mis.AddMaterialInStock(materialInStock);
                    //                    }
                    //                    else if (od.ProductId != original.ProductId || od.AttributeId != original.AttributeId)
                    //                    {
                    //                        //Tao moi
                    //                        List<MaterialInStock> lstNewMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == od.ProductId && pt.AttributeId == od.AttributeId);
                    //                        MaterialInStock newMaterialInStock = new MaterialInStock();
                    //                        newMaterialInStock.AttributeId = od.AttributeId;
                    //                        newMaterialInStock.ProductId = od.ProductId;
                    //                        newMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_INPUT;
                    //                        newMaterialInStock.LatestUpdate = DateTime.Now;
                    //                        if (lstNewMaterial.Count > 0)
                    //                        {
                    //                            newMaterialInStock.NumberOfInput = lstNewMaterial.Last<MaterialInStock>().NumberOfInput + od.NumberUnit;
                    //                            newMaterialInStock.NumberOfOutput = lstNewMaterial.Last<MaterialInStock>().NumberOfOutput;
                    //                            newMaterialInStock.NumberOfItem = lstNewMaterial.Last<MaterialInStock>().NumberOfItem + od.NumberUnit;
                    //                        }
                    //                        else
                    //                        {
                    //                            newMaterialInStock.NumberOfInput = od.NumberUnit;
                    //                            newMaterialInStock.NumberOfOutput = 0;
                    //                            newMaterialInStock.NumberOfItem = od.NumberUnit;
                    //                        }
                    //                        mis.AddMaterialInStock(newMaterialInStock);


                    //                        //Xoa cu
                    //                        List<MaterialInStock> lstOldMaterial = mis.SelectMaterialInStockByWhere(pt => pt.ProductId == original.ProductId && pt.AttributeId == original.AttributeId);

                    //                        if (lstOldMaterial.Count > 0)
                    //                        {
                    //                            MaterialInStock oldMaterialInStock = new MaterialInStock();
                    //                            oldMaterialInStock.AttributeId = original.AttributeId;
                    //                            oldMaterialInStock.ProductId = original.ProductId;
                    //                            oldMaterialInStock.StatusOfData = (byte)BHConstant.DATA_STATUS_IN_STOCK_FOR_EDIT;
                    //                            oldMaterialInStock.LatestUpdate = DateTime.Now;
                    //                            oldMaterialInStock.NumberOfInput = lstOldMaterial.Last<MaterialInStock>().NumberOfInput - original.NumberUnit;
                    //                            oldMaterialInStock.NumberOfOutput = lstOldMaterial.Last<MaterialInStock>().NumberOfOutput;
                    //                            oldMaterialInStock.NumberOfItem = lstOldMaterial.Last<MaterialInStock>().NumberOfItem - original.NumberUnit;
                    //                            mis.AddMaterialInStock(oldMaterialInStock);
                    //                        }

                    //                    }

                    //                }
                    //            }

                    //            if (!result)
                    //                break;
                    //        }
                    //    }
                    //    if (!result)
                    //    {
                    //        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    //        return;
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Phiếu nhập kho đã được cập nhật thành công");
                    //    }
                    //    if (this.CallFromUserControll != null && this.CallFromUserControll is StockEntranceList)
                    //    {
                    //        ((StockEntranceList)this.CallFromUserControll).loadEntranceStockList();
                    //    }

                    //    this.Close();
                    //}

                    #endregion

                    this.Close();
                }
                else//add new
                {
                    entranceStock = new EntranceStock
                    {

                        EntranceCode = txtCode.Text,
                        EntrancedBy = userId,
                        EntrancedDate = createdDate,
                        Note = txtNote.Text
                    };
                    EntranceStockService entranceStockService = new EntranceStockService();
                    bool result = entranceStockService.AddEntranceStock(entranceStock);
                    long newOrderId = BaoHienRepository.GetMaxId<EntranceStock>();
                    EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();
                    foreach (EntranceStockDetail od in entranceStockDetails)
                    {
                        if (od.ProductId > 0 && od.AttributeId > 0)
                        {
                            od.EntranceStockId = (int)newOrderId;
                            bool ret = entranceStockDetailService.AddEntranceStockDetail(od);
                            if (!ret)
                            {
                                MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                return;
                            }

                            //Save in Product Log
                            ProductLog pl = productLogService.GetNewestProductLog(od.ProductId, od.AttributeId);
                            ProductLog plg = new ProductLog
                            {
                                AttributeId = od.AttributeId,
                                ProductId = od.ProductId,
                                RecordCode = entranceStock.EntranceCode,
                                BeforeNumber = pl.AfterNumber,
                                Amount = od.NumberUnit,
                                AfterNumber = pl.AfterNumber + od.NumberUnit,
                                CreatedDate = DateTime.Now
                            };
                            result = productLogService.AddProductLog(plg);
                        }
                    }
                    if (result)
                    {
                        MessageBox.Show("Phiếu nhập kho đã được tạo thành công");
                        //((OrderList)this.CallFromUserControll).loadOrderList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    }
                }
            }
            
        }
    }
}
