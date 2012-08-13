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
using BaoHien.Services.MaterialInStocks;

namespace BaoHien.UI
{
    public partial class AddEntranceStock : BaseForm
    {
        bool isUpdating = false;
        EntranceStock entranceStock;
        BindingList<EntranceStockDetail> entranceStockDetails;
        BindingList<Product> products;
        BindingList<BaseAttribute> baseAttributesAtRow;
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
            loadSomeData();
            SetupColumns();
            updateProductionRequestDetailCells();
        }
        private void loadSomeData()
        {
            

            ProductService productService = new ProductService();
            products = new BindingList<Product>(productService.GetProducts());
            BaseAttributeService baseAttributeService = new BaseAttributeService();
            baseAttributesAtRow = new BindingList<BaseAttribute>(baseAttributeService.GetBaseAttributes());
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
            productColumn.Width = 150;
            productColumn.AutoComplete = false;

            productColumn.HeaderText = "Sản phẩm";
            productColumn.DataSource = products;
            productColumn.DisplayMember = "ProductName";
            //productColumn.Frozen = true;
            productColumn.ValueMember = "Id";

            dgvStockEntranceDetails.Columns.Add(productColumn);

            DataGridViewComboBoxColumn productAttributeColumn = new DataGridViewComboBoxColumn();
            productAttributeColumn.Width = 150;
            productAttributeColumn.HeaderText = "Thuộc tính sản phẩm";

            productAttributeColumn.DataSource = baseAttributesAtRow;

            productAttributeColumn.DisplayMember = "AttributeName";
            //productColumn.Frozen = true;
            productAttributeColumn.ValueMember = "Id";

            dgvStockEntranceDetails.Columns.Add(productAttributeColumn);



            DataGridViewTextBoxColumn numberUnitColumn = new DataGridViewTextBoxColumn();

            numberUnitColumn.Width = 100;
            numberUnitColumn.DataPropertyName = "NumberUnit";
            numberUnitColumn.HeaderText = "Số lượng";
            //numberUnitColumn.Frozen = true;
            numberUnitColumn.ValueType = typeof(int);
            dgvStockEntranceDetails.Columns.Add(numberUnitColumn);

            //DataGridViewTextBoxColumn priceColumn = new DataGridViewTextBoxColumn();

            //priceColumn.Width = 100;
            //priceColumn.DataPropertyName = "Price";
            //priceColumn.HeaderText = "Số lượng";
            ////numberUnitColumn.Frozen = true;
            //priceColumn.ValueType = typeof(int);
            //dgwOrderDetails.Columns.Add(priceColumn);

            //DataGridViewTextBoxColumn totalColumn = new DataGridViewTextBoxColumn();

            //totalColumn.Width = 100;
            //totalColumn.DataPropertyName = "Total";
            //totalColumn.HeaderText = "Số lượng";
            ////numberUnitColumn.Frozen = true;
            //totalColumn.ValueType = typeof(int);
            //dgvStockEntranceDetails.Columns.Add(totalColumn);

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();

            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 100;
            noteColumn.HeaderText = "Ghi chú";
            //numberUnitColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgvStockEntranceDetails.Columns.Add(noteColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.DataPropertyName = "DeleteButton";
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 100;
            deleteButton.HeaderText = "Xóa";
            deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;
        }
        public void updateProductionRequestDetailCells()
        {
            if (isUpdating && entranceStock != null && entranceStockDetails.Count < dgvStockEntranceDetails.RowCount)
            {

                for (int i = 0; i < entranceStockDetails.Count; i++)
                {
                    for (int j = 0; j < 2 && j < dgvStockEntranceDetails.ColumnCount; j++)
                    {
                        if (dgvStockEntranceDetails.Rows[i].Cells[j] is DataGridViewComboBoxCell)
                        {
                            if (j == 0)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvStockEntranceDetails.Rows[i].Cells[j];
                                pkgBoxCell.Value = entranceStockDetails[i].ProductId;
                            }
                            if (j == 1)
                            {
                                DataGridViewComboBoxCell pkgBoxCell = (DataGridViewComboBoxCell)dgvStockEntranceDetails.Rows[i].Cells[j];
                                pkgBoxCell.Value = entranceStockDetails[i].AttributeId;
                            }
                        }
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
                if (e.ColumnIndex == 0)
                {

                    entranceStockDetails[e.RowIndex].ProductId = (int)dgv.CurrentCell.Value;
                    ProductAttributeService productAttributeService = new ProductAttributeService();
                    List<ProductAttribute> productAttributes = productAttributeService.SelectProductAttributeByWhere(ba => ba.Id == entranceStockDetails[e.RowIndex].ProductId);
                    baseAttributesAtRow = new BindingList<BaseAttribute>();
                    foreach (ProductAttribute pa in productAttributes)
                    {
                        baseAttributesAtRow.Add(pa.BaseAttribute);
                    }
                    DataGridViewComboBoxCell currentCell = (DataGridViewComboBoxCell)dgvStockEntranceDetails.Rows[e.RowIndex].Cells[1];
                    currentCell.DataSource = baseAttributesAtRow;
                    if (baseAttributesAtRow.Count > e.RowIndex && baseAttributesAtRow.Count > 0)
                    {
                        entranceStockDetails[e.RowIndex].AttributeId = baseAttributesAtRow[0].Id;
                        currentCell.Value = baseAttributesAtRow[0].Id;
                    }


                }
                else if (e.ColumnIndex == 1)
                {
                    entranceStockDetails[e.RowIndex].AttributeId = (int)dgv.CurrentCell.Value;
                }
                else if (e.ColumnIndex == 2)
                {
                    entranceStockDetails[e.RowIndex].NumberUnit = (int)dgv.CurrentCell.Value;
                }
                
                else if (e.ColumnIndex == 3)
                {
                    entranceStockDetails[e.RowIndex].Note = (string)dgv.CurrentCell.Value;
                }
            }
        }

        private void dgvStockEntranceDetails_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Có lỗi nhập liệu xảy ra,vui lòng kiểm tra lại!");
        }

        private void dgvStockEntranceDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvStockEntranceDetails.CurrentCell.ColumnIndex == 0)
            {
                var source = new AutoCompleteStringCollection();
                String[] stringArray = Array.ConvertAll<Product, String>(products.ToArray(), delegate(Product row) { return (String)row.ProductName; });
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
            else if (dgvStockEntranceDetails.CurrentCell.ColumnIndex == 1)
            {
                if (baseAttributesAtRow != null)
                {
                    var source = new AutoCompleteStringCollection();
                    String[] stringArray = Array.ConvertAll<BaseAttribute, String>(baseAttributesAtRow.ToArray(), delegate(BaseAttribute row) { return (String)row.AttributeName; });
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

        private void UpdateMaterialInStock()
        {
            foreach (EntranceStockDetail esd in entranceStockDetails)
            {
 
            }
        }

        public void loadDataForEditEntranceStock(int entranceId)
        {
            isUpdating = true;
            this.Text = "Chỉnh sửa  đơn hàng này";
            this.btnSave.Text = "Cập nhật";
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
            MaterialInStockService mis = new MaterialInStockService();
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
                entranceStock.EntranceCode = txtCode.Text;
                entranceStock.Note = txtNote.Text;

                EntranceStockService entranceStockService = new EntranceStockService();
                bool result = entranceStockService.UpdateEntranceStock(entranceStock);
                if (!result)
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                    return;
                }
                else
                {

                    EntranceStockDetailService entranceStockDetailService = new EntranceStockDetailService();
                    
                    foreach (EntranceStockDetail od in entranceStockDetails)
                    {
                        if (od.ProductId > 0 && od.AttributeId > 0)
                        {
                            if (od.Id == 0)
                            {
                                od.EntranceStockId = entranceStock.Id;
                                result = entranceStockDetailService.AddEntranceStockDetail(od);

                                
                                List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.Id == od.ProductId && pt.AttributeId == od.AttributeId);
                                MaterialInStock materialInStock = new MaterialInStock();
                                materialInStock.AttributeId = od.AttributeId;
                                materialInStock.Id = od.ProductId;
                                materialInStock.NumberOfItem = od.NumberUnit;
                                materialInStock.LatestUpdate = DateTime.Now;
                                if (lstMaterial.Count > 0)
                                    materialInStock.NumberOfItem += lstMaterial.Last<MaterialInStock>().NumberOfItem;
                                mis.AddMaterialInStock(materialInStock);
                            }
                            else
                            {
                                ProductionRequestDetailModel original = new ProductionRequestDetailModel();
                                original = originalEntranceDetail.Where(p => p.Id == od.Id).ToList().FirstOrDefault(); 
                                result = entranceStockDetailService.UpdateEntranceStockDetail(od);
                                if (od.ProductId == original.ProductId && od.AttributeId == original.AttributeId && od.NumberUnit != original.NumberUnit)
                                {
                                    List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.Id == od.ProductId && pt.AttributeId == od.AttributeId);
                                    MaterialInStock materialInStock = new MaterialInStock();
                                    materialInStock.AttributeId = od.AttributeId;
                                    materialInStock.Id = od.ProductId;
                                    materialInStock.NumberOfItem = od.NumberUnit - original.NumberUnit;
                                    materialInStock.LatestUpdate = DateTime.Now;
                                    if (lstMaterial.Count > 0)
                                        materialInStock.NumberOfItem += lstMaterial.Last<MaterialInStock>().NumberOfItem;
                                    mis.AddMaterialInStock(materialInStock);
                                }
                                else if (od.ProductId != original.ProductId || od.AttributeId != original.AttributeId)
                                {
                                    List<MaterialInStock> lstNewMaterial = mis.SelectMaterialInStockByWhere(pt => pt.Id == od.ProductId && pt.AttributeId == od.AttributeId);
                                    MaterialInStock newMaterialInStock = new MaterialInStock();
                                    newMaterialInStock.AttributeId = od.AttributeId;
                                    newMaterialInStock.Id = od.ProductId;
                                    newMaterialInStock.NumberOfItem = od.NumberUnit;
                                    newMaterialInStock.LatestUpdate = DateTime.Now;
                                    if (lstNewMaterial.Count > 0)
                                        newMaterialInStock.NumberOfItem += lstNewMaterial.Last<MaterialInStock>().NumberOfItem;
                                    mis.AddMaterialInStock(newMaterialInStock);

                                    List<MaterialInStock> lstoldMaterial = mis.SelectMaterialInStockByWhere(pt => pt.Id == original.ProductId && pt.AttributeId == original.AttributeId);
                                    MaterialInStock oldMaterialInStock = new MaterialInStock();
                                    newMaterialInStock.AttributeId = original.AttributeId;
                                    newMaterialInStock.Id = original.ProductId;
                                    newMaterialInStock.LatestUpdate = DateTime.Now;
                                    if (lstNewMaterial.Count > 0)
                                        newMaterialInStock.NumberOfItem = lstNewMaterial.Last<MaterialInStock>().NumberOfItem - original.NumberUnit;
                                    mis.AddMaterialInStock(newMaterialInStock);

 
                                }
                            }

                            if (!result)
                                break;
                        }
                    }
                    if (!result)
                    {
                        MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Sản phẩm đã được cập nhật thành công");
                    }
                    if (this.CallFromUserControll != null && this.CallFromUserControll is StockEntranceList)
                    {
                        ((StockEntranceList)this.CallFromUserControll).loadEntranceStockList();
                    }

                    this.Close();
                }
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
                    if (od.ProductId > 0)
                    {
                        od.EntranceStockId = (int)newOrderId;
                        bool ret = entranceStockDetailService.AddEntranceStockDetail(od);
                        if (!ret)
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                            return;
                        }

                        //Save in Materail In Stock
                        MaterialInStock materialInStock = new MaterialInStock();
                        List<MaterialInStock> lstMaterial = mis.SelectMaterialInStockByWhere(pt => pt.Id == od.ProductId && pt.AttributeId == od.AttributeId);

                        materialInStock.AttributeId = od.AttributeId;
                        materialInStock.Id = od.ProductId;
                        materialInStock.NumberOfItem = od.NumberUnit;
                        materialInStock.LatestUpdate = DateTime.Now;
                        if (lstMaterial.Count > 0)
                            materialInStock.NumberOfItem += lstMaterial.Last<MaterialInStock>().NumberOfItem;
                        mis.AddMaterialInStock(materialInStock);
                    }
                }

               
                if (result)
                {
                    MessageBox.Show("Sản phẩm đã được tạo thành công");
                    //((OrderList)this.CallFromUserControll).loadOrderList();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                }
            }
        }

        private void AddMaterialInStock()
        { 
        }
    }
}
