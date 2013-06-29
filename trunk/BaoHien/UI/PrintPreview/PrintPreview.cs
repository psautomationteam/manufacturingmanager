using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BaoHien.Common;

namespace BaoHien.UI.PrintPreviewCustom
{
    public partial class PrintPreview : Form
    {
        public static int XKNumber = 0;
        public static int BHNumber = 0;

        public PrintPreview(string filePath)
        {
            InitializeComponent();
            ShowFile(filePath);
        }

        private void ShowFile(string filePath)
        {
            wbReader.Navigate(filePath);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                XKNumber = Convert.ToInt32(txtXKNumber.Text);
                BHNumber = Convert.ToInt32(txtBHNumber.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Chỉ cho phép nhập số vào các ô dữ liệu để in!", "Lỗi nhập liệu");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
            if (XKNumber == 0 && BHNumber == 0)
            {
                MessageBox.Show("Chưa nhập số lượng bản in!", "Lỗi nhập liệu");
                this.DialogResult = System.Windows.Forms.DialogResult.None;
            }
        }
    }
}
