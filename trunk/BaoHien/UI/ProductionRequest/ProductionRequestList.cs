using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BaoHien.Services.ProductionRequests;
using DAL;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Services.ProductionRequestDetails;
using BaoHien.Services.SystemUsers;
using BaoHien.Common;
using BaoHien.Services.ProductLogs;

namespace BaoHien.UI
{
    public partial class ProductionRequestList : UserControl
    {
        List<ProductionRequest> productionRequests;
        List<SystemUser> users;

        public ProductionRequestList()
        {
            InitializeComponent();
        }

        private void ProductionRequestList_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-DateTime.Now.Day + 1);
            dtpFrom.CustomFormat = BHConstant.DATE_FORMAT;
            dtpTo.CustomFormat = BHConstant.DATE_FORMAT;
            loadProductionRequestList();
            setUpSomeData();
            SetupColumns();
        }

        private void setUpSomeData()
        {
            SystemUserService systemUserService = new SystemUserService();
            users = systemUserService.GetSystemUsers();
            users.Add(new SystemUser() { Id = 0, FullName = "Tất cả" });
            users = users.OrderBy(x => x.Id).ToList();

            cbmUsers.DataSource = users;
            cbmUsers.DisplayMember = "FullName";
            cbmUsers.ValueMember = "Id";
        }

        public void loadProductionRequestList()
        {
            ProductionRequestService productionRequestService = new ProductionRequestService();
            productionRequests = productionRequestService.GetProductionRequests();
            if (productionRequests != null)
            {
                productionRequestInTotal.Text = productionRequests.Count.ToString();
            }
            
            dgwRequestList.AutoGenerateColumns = false;
            setUpDataGrid(productionRequests);           
        }

        private void setUpDataGrid(List<ProductionRequest> productionRequests)
        {
            if (productionRequests != null)
            {
                var query = from productionRequest in productionRequests
                            select new 
                            {
                                ReqCode = productionRequest.ReqCode,
                                UserId = productionRequest.SystemUser.FullName,
                                Note = productionRequest.Note,
                                Id = productionRequest.Id,
                                CreatedBy = productionRequest.CreatedDate.ToString(BHConstant.DATE_FORMAT),
                                Status = productionRequest.Status != null?productionRequest.Status.Value: (byte)0,
                            };
                dgwRequestList.DataSource = query.ToList();
                productionRequestInTotal.Text = query.Count().ToString();
            }
        }

        private void SetupColumns()
        {
            dgwRequestList.Columns.Add(Global.CreateCell("CreatedBy", "Ngày tạo", 100));
            dgwRequestList.Columns.Add(Global.CreateCell("ReqCode", "Mã yêu cầu", 150));
            dgwRequestList.Columns.Add(Global.CreateCell("UserId", "Yêu cầu bởi", 200));
            dgwRequestList.Columns.Add(Global.CreateCell("Note", "Ghi chú", 300));
            dgwRequestList.Columns.Add(Global.CreateCellDeleteAction());
        }

        private void dgwRequestList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AddProductionRequest addProductionRequest = new AddProductionRequest();
            DataGridViewRow currentRow = dgwRequestList.Rows[e.RowIndex];

            int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
            addProductionRequest.loadDataForEditProductRequest(id);

            addProductionRequest.CallFromUserControll = this;
            addProductionRequest.ShowDialog();
        }

        private void dgwRequestList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sender is DataGridView)
            {
                DataGridViewCell cell = ((DataGridView)sender).CurrentCell;
                if (cell.ColumnIndex == ((DataGridView)sender).ColumnCount - 1)
                {
                    DialogResult result = MessageBox.Show("Bạn có muốn xóa phiếu sản xuât này?",
                    "Xoá phiếu sản xuât",
                     MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        DataGridViewRow currentRow = dgwRequestList.Rows[e.RowIndex];

                        ProductionRequestService productionRequestService = new ProductionRequestService();
                        ProductionRequestDetailService productionRequestDetailService = new ProductionRequestDetailService();
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        ProductionRequest pr = productionRequestService.GetProductionRequest(id);
                        List<ProductionRequestDetail> productionRequestDetails = productionRequestDetailService.GetProductionRequestDetails().Where(p => p.ProductionRequestId == id).ToList();
                        bool ret = false;
                        DateTime systime = BaoHienRepository.GetBaoHienDBDataContext().GetSystemDate();

                        ProductLogService productLogService = new ProductLogService();
                        string msg = "";
                        int error = 0, amount = 0;
                        ProductLog pl, newpl;
                        foreach (ProductionRequestDetail item in productionRequestDetails)
                        {
                            pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                            amount = (item.Direction == BHConstant.DIRECTION_OUT) ? -item.NumberUnit : item.NumberUnit;
                            if (pl.AfterNumber - amount < 0)
                            {
                                if (error == 0)
                                {
                                    msg += "Những sản phẩm sau đã bị XÓA nhưng không đảm bảo dữ liệu trong kho:\n";
                                    error = 1;
                                }
                                msg += "- " + productLogService.GetNameOfProductLog(pl) + " : " + item.NumberUnit + "\n";
                            }
                        }
                        if (error > 0)
                        {
                            MessageBox.Show(msg, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        foreach (ProductionRequestDetail item in productionRequestDetails)
                        {
                            pl = productLogService.GetProductLog(item.ProductId, item.AttributeId, item.UnitId);
                            amount = (item.Direction == BHConstant.DIRECTION_OUT) ? -item.NumberUnit : item.NumberUnit;
                            newpl = new ProductLog()
                            {
                                ProductId = item.ProductId,
                                AttributeId = item.AttributeId,
                                UnitId = item.UnitId,
                                BeforeNumber = pl.AfterNumber,
                                Amount = item.NumberUnit,
                                AfterNumber = pl.AfterNumber - amount,
                                RecordCode = pr.ReqCode,
                                Status = BHConstant.DEACTIVE_STATUS,
                                Direction = !item.Direction,
                                UpdatedDate = systime
                            };
                            productLogService.AddProductLog(newpl);
                        }
                        productLogService.DeactiveProductLog(pr.ReqCode);
                        if (!productionRequestService.DeleteProductionRequest(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        loadProductionRequestList();
                    }

                }

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ProductionRequestSearchCriteria productionRequestSearchCriteria = new ProductionRequestSearchCriteria
            {
                CodeRequest = txtCode.Text != null ? txtCode.Text.ToLower() : "",
                UserId = (cbmUsers.SelectedValue != null && cbmUsers.SelectedIndex != 0) ? (int?)cbmUsers.SelectedValue : (int?)null,
                From = dtpFrom.Value != null ? dtpFrom.Value : (DateTime?)null,
                To = dtpTo.Value != null ? dtpTo.Value.AddDays(1).Date : (DateTime?)null,
            };
            ProductionRequestService productionRequestService = new ProductionRequestService();
            productionRequests = productionRequestService.SearchingProductionRequest(productionRequestSearchCriteria);
            if (productionRequests == null)
            {
                productionRequests = new List<ProductionRequest>();
            }
            setUpDataGrid(productionRequests);
        }

        private void dgwRequestList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView gridView = sender as DataGridView;
            if (null != gridView)
            {
                gridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
                foreach (DataGridViewRow r in gridView.Rows)
                {
                    gridView.Rows[r.Index].HeaderCell.Value = (r.Index + 1).ToString();
                }
            }
        }
    }
}
