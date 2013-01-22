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
        public PrintPreview(string filePath)
        {
            InitializeComponent();
            ShowFile(filePath);
        }

        private void ShowFile(string filePath)
        {
            //reader.setShowScrollbars(true);
            //reader.setShowToolbar(false);
            //reader.setView("FitH");
            wbReader.Navigate(BHConstant.SAVE_IN_DIRECTORY + @"\BHang.pdf");
        }
    }
}
