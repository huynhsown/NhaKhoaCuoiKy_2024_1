using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views
{
    public partial class ListItemService : UserControl
    {
        public ListItemService()
        {
            InitializeComponent();
        }

        #region Properties

        private string _tenDichVu;
        private string _maDichVu;
        private int _gia;
        private int _giamGia;
        private int _donVi;

        private void btn_edit_Click(object sender, EventArgs e)
        {
            MessageBox.Show(_maDichVu);
        }

        [Category("Custom Props")]
        public string TenDichVu
        {
            get { return _tenDichVu; }
            set { _tenDichVu = value; lb_tenDichvu.Text = value; }
        }

        [Category("Custom Props")]
        public string MaDichVu
        {
            get { return _maDichVu; }
            set { _maDichVu = value; lb_maDichVu.Text = value; }
        }

        [Category("Custom Props")]
        public int Gia
        {
            get { return _gia; }
            set { _gia = value; lb_giaDichVu.Text = value.ToString(); }
        }

        [Category("Custom Props")]
        public int GiamGia
        {
            get { return _giamGia; }
            set { _giamGia = value; lb_giamGia.Text = value.ToString(); }
        }

        [Category("Custom Props")]
        public int DonVi
        {
            get { return _donVi; }
            set { _donVi = value; lb_donVi.Text = value.ToString(); }
        }
        #endregion
    }
}
