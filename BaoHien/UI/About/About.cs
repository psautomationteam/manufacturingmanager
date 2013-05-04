using System;
using System.Windows.Forms;
using BaoHien.Common;

namespace BaoHien.UI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lbVersion.Text = BHConstant.BUILD_VERSION;
            lbReleaseDate.Text = BHConstant.BUILD_RELEASE_DATE;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
