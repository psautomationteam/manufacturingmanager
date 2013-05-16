using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.UI.Base;
using System.Data.SqlClient;
using BaoHien.Common;

namespace BaoHien.UI.Cleaner
{
    public partial class Cleaner : BaseForm
    {
        public Cleaner()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (chkBH.Checked == false && chkNK.Checked == false && chkSX.Checked == false && chkTT.Checked == false)
                MessageBox.Show("Bạn chưa chọn loại phiếu để xóa !");
            else
            {
                DeletingDataBase(dtpDate.Value.Date);
                DAL.Helper.BaoHienRepository.ResetDBDataContext();
                MessageBox.Show("Đã xóa dữ liệu thành công");
                this.Close();
            }
        }

        private void DeletingDataBase(DateTime from)
        {
            string sqlConnectionString = DAL.Helper.SettingManager.BuildStringConnection();
            using (SqlConnection sqlConn = new SqlConnection(sqlConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    string dateString = from.ToString("MM/dd/yyyy HH:mm:ss");
                    string sqlQuery = "";
                    if(chkBH.Checked)
                    {
                        sqlQuery += "UPDATE customerlog SET status = 1 WHERE createddate < '" + dateString + "' AND recordcode like '%" + BHConstant.PREFIX_FOR_ORDER + "%'; ";
                        sqlQuery += "UPDATE employeelog SET status = 1 WHERE createddate < '" + dateString + "' AND recordcode like '%" + BHConstant.PREFIX_FOR_ORDER + "%'; ";
                        sqlQuery += "UPDATE productlog SET status = 1 WHERE createddate < '" + dateString + "' AND recordcode like '%" + BHConstant.PREFIX_FOR_ORDER + "%'; ";
                        sqlQuery += "UPDATE dbo.[order] SET status = 3 WHERE createddate < '" + dateString + "'; ";
                    }
                    if (chkTT.Checked)
                    {
                        sqlQuery += "UPDATE customerlog SET status = 1 WHERE createddate < '" + dateString + "' AND recordcode like '%" + BHConstant.PREFIX_FOR_BILLING + "%'; ";
                        sqlQuery += "UPDATE bill SET status = 3 WHERE createddate < '" + dateString + "'; ";
                    }
                    if(chkNK.Checked)
                    {
                        sqlQuery += "UPDATE productlog SET status = 1 WHERE createddate < '" + dateString + "' AND recordcode like '%" + BHConstant.PREFIX_FOR_ENTRANCE + "%'; ";
                        sqlQuery += "UPDATE entrancestock SET status = 3  WHERE entranceddate < '" + dateString + "'; ";
                    }
                    if (chkSX.Checked)
                    {
                        sqlQuery += "UPDATE productlog SET status = 1 WHERE createddate < '" + dateString + "' AND recordcode like '%" + BHConstant.PREFIX_FOR_PRODUCTION + "%'; ";
                        sqlQuery += "UPDATE productionrequest SET status = 3  WHERE requesteddate < '" + dateString + "'; ";
                    }
                    if (!string.IsNullOrEmpty(sqlQuery))
                    {
                        cmd.Connection = sqlConn;
                        cmd.CommandText = sqlQuery;
                        try
                        {
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                        }
                        catch { }
                        finally
                        {
                            cmd.Connection.Close();
                        }
                    }
                }
            }
        }
    }
}
