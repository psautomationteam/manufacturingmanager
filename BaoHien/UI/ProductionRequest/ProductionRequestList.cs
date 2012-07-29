using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Services.ProductionRequests;
using DAL;
using DAL.Helper;
using BaoHien.Model;
using BaoHien.Services.ProductionRequestDetails;
using BaoHien.Services.SystemUsers;

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
            loadProductionRequestList();
            setUpSomeData();
            SetupColumns();
        }
        private void setUpSomeData()
        {
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
            SystemUserService systemUserService = new SystemUserService();
            users = systemUserService.GetSystemUsers();
           
        }
        private void setUpDataGrid(List<ProductionRequest> productionRequests)
        {
            if (productionRequests != null)
            {
                int index = 0;
                var query = from productionRequest in productionRequests

                            select new ProductionRequestModel
                            {
                                ReqCode = productionRequest.ReqCode,
                                RequestedBy = productionRequest.SystemUser.FullName,
                                Note = productionRequest.Note,
                                Id = productionRequest.Id,
                                Status = productionRequest.Status != null?productionRequest.Status.Value: (byte)0,
                                Index = index++
                            };
                dgwRequestList.DataSource = new BindingList<ProductionRequestModel>(query.ToList());
                dgwRequestList.AllowUserToAddRows = false;
                dgwRequestList.ReadOnly = true;
            }
        }
        private void SetupColumns()
        {

            DataGridViewTextBoxColumn checkboxColumn = new DataGridViewTextBoxColumn();
            checkboxColumn.Width = 30;
            checkboxColumn.HeaderText = "STT";
            checkboxColumn.ValueType = typeof(string);
            //checkboxColumn.Frozen = true;
            dgwRequestList.Columns.Add(checkboxColumn);

            DataGridViewTextBoxColumn reqCodeColumn = new DataGridViewTextBoxColumn();
            reqCodeColumn.Width = 150;
            reqCodeColumn.DataPropertyName = "ReqCode";
            reqCodeColumn.HeaderText = "Mã yêu cầu";
            reqCodeColumn.ValueType = typeof(string);
            //attributeNameColumn.Frozen = true;
            dgwRequestList.Columns.Add(reqCodeColumn);



            DataGridViewTextBoxColumn requestedByColumn = new DataGridViewTextBoxColumn();
            requestedByColumn.DataPropertyName = "RequestedBy";
            requestedByColumn.Width = 200;
            requestedByColumn.HeaderText = "Yêu cầu bởi";
            //attributeCodeColumn.Frozen = true;
            requestedByColumn.ValueType = typeof(string);
            dgwRequestList.Columns.Add(requestedByColumn);

            

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.DataPropertyName = "Status";
            statusColumn.Width = 120;
            statusColumn.HeaderText = "Trạng thái";
            //attributeCodeColumn.Frozen = true;
            statusColumn.ValueType = typeof(string);
            dgwRequestList.Columns.Add(statusColumn);

            DataGridViewImageColumn deleteButton = new DataGridViewImageColumn();
            deleteButton.Image = Properties.Resources.erase;
            deleteButton.Width = 150;
            deleteButton.HeaderText = "Xóa";
            //deleteButton.ReadOnly = true;
            deleteButton.ImageLayout = DataGridViewImageCellLayout.Normal;

            DataGridViewTextBoxColumn noteColumn = new DataGridViewTextBoxColumn();
            noteColumn.DataPropertyName = "Note";
            noteColumn.Width = 400;
            noteColumn.HeaderText = "Ghi chú";
            //attributeCodeColumn.Frozen = true;
            noteColumn.ValueType = typeof(string);
            dgwRequestList.Columns.Add(noteColumn);

            dgwRequestList.Columns.Add(deleteButton);

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
                        //Product mu = (Product)dgv.DataBoundItem;
                        int id = ObjectHelper.GetValueFromAnonymousType<int>(currentRow.DataBoundItem, "Id");
                        List<ProductionRequestDetail> productionRequestDetails = productionRequestDetailService.GetProductionRequestDetails().Where(p => p.ProductionRequestId == id).ToList();
                        bool ret = false;
                        if (productionRequestDetails != null)
                        {
                            foreach (ProductionRequestDetail prd in productionRequestDetails)
                            {
                                List<int> ids = new List<int>();
                                ids.Add(prd.Id);
                                ids.Add(prd.ProductionRequestId);
                                ret = productionRequestDetailService.DeleteProductionRequestDetail(ids);
                                if (!ret)
                                {
                                    MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");
                                    return;
                                }
                            }
                        }
                        if (!productionRequestService.DeleteProductionRequest(id))
                        {
                            MessageBox.Show("Hiện tại hệ thống đang có lỗi. Vui lòng thử lại sau!");

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
                CodeRequest = txtCode.Text != null ? txtCode.Text : "",
                RequestedBy = cbmUsers.SelectedValue != null ? (int?)cbmUsers.SelectedValue : (int?)null,
                From = dtpFrom.Value != null?dtpFrom.Value:(DateTime?)null,
                To = dtpTo.Value != null ? dtpTo.Value : (DateTime?)null,
            };
            ProductionRequestService productionRequestService = new ProductionRequestService();
            productionRequests = productionRequestService.SearchingProductionRequest(productionRequestSearchCriteria);
            if (productionRequests == null)
            {
                productionRequests = new List<ProductionRequest>();
            }
            setUpDataGrid(productionRequests);
        }
    }
}
