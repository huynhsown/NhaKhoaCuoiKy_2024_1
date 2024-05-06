using Dapper;
using NhaKhoaCuoiKy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Service
{
    public partial class NewCategory : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );

        public DynamicParameters p;
        private Servicee serviceForm;
        private int categoryID;
        private string title;
        public NewCategory()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        public NewCategory(Servicee serviceForm)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.serviceForm = serviceForm;
        }

        public NewCategory(Servicee serviceForm, int categoryID, string title)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.serviceForm = serviceForm;
            this.categoryID = categoryID;
            btn_edit.Location = new Point(289, 125);
            btn_edit.Visible = true;
            btn_edit.Enabled = true;
            btn_add.Visible = false;
            btn_add.Enabled = false;
            this.title = title;
            tb_category.Text = title;
        }

        private void NewCategory_Load(object sender, EventArgs e)
        {

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_category.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập loại dịch vụ!", "Loại dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (ServiceHelper.checkCategoryByName(tb_category.Text.Trim()))
            {
                MessageBox.Show("Loại dịch vụ đã tồn tại", "Loại dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                p = ServiceHelper.addNewCategory(tb_category.Text.Trim());
                MessageBox.Show("Thêm thành công", "Dich vụ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                int id = p.Get<int>("@MaLoaiDichVu");
                string txt = p.Get<string>("@TenLoaiDichVu");
                serviceForm.data_loaiDichvu.Rows.Add(id, txt);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lôi", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            if(tb_category.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui lòng nhập loại dịch vụ!", "Loại dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if(ServiceHelper.updateCategory(categoryID, tb_category.Text.Trim()))
                {
                    MessageBox.Show("Thanh cong");
                }
                else
                {
                    MessageBox.Show("That bai");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
