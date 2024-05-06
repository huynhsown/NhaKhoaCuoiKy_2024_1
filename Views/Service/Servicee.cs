using Dapper;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Service
{
    public partial class Servicee : Form
    {
        private MainForm mainForm;
        private DynamicParameters p;
        UserModel userAccount;
        public Servicee()
        {
            InitializeComponent();
        }

        public Servicee(MainForm mainForm, UserModel userAccount)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.userAccount = userAccount;
        }

        private void Servicee_Load(object sender, EventArgs e)
        {
            loadAllCategory();
            loadAllService();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            FormBackGround formBackGround = new FormBackGround(mainForm);
            try
            {
                using (NewCategory newCategory = new NewCategory(this))
                {
                    formBackGround.Owner = mainForm;
                    formBackGround.Show();
                    newCategory.Owner = formBackGround;
                    newCategory.ShowDialog();
                    formBackGround.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadAllCategory()
        {
            try
            {
                DataTable dt = ServiceHelper.getAllServiceCategory();
                loadCategory(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadCategory(DataTable dt)
        {
            data_loaiDichvu.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr[0]);
                string txt = dr[1].ToString();
                data_loaiDichvu.Rows.Add(id, txt);
            }
        }

        public void loadAllService()
        {
            try
            {
                DataTable dt = ServiceHelper.getAllService();
                loadService(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void loadService(DataTable dt)
        {
            data_category_items.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr["MaDichVu"]);
                string txt = dr["TenDichVu"].ToString();
                data_category_items.Rows.Add(id, txt);
            }
        }

        private void btn_category_refresh_Click(object sender, EventArgs e)
        {
            loadAllCategory();
        }



        private void btn_add_category_item_Click(object sender, EventArgs e)
        {
            FormBackGround formBackGround = new FormBackGround(mainForm);
            try
            {
                using (NewService newService = new NewService(this))
                {
                    formBackGround.Owner = mainForm;
                    formBackGround.Show();
                    newService.Owner = formBackGround;
                    newService.ShowDialog();
                    formBackGround.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void loadForm(Form form)
        {
            FormBackGround formBackGround = new FormBackGround(mainForm);
            try
            {
                using (form)
                {
                    formBackGround.Owner = mainForm;
                    formBackGround.Show();
                    form.Owner = formBackGround;
                    form.ShowDialog();
                    formBackGround.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void data_loaiDichvu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (data_loaiDichvu.Columns[e.ColumnIndex].Name == "col_active")
                {
                    int index = e.RowIndex;
                    int categoryID = Convert.ToInt32(data_loaiDichvu.Rows[index].Cells[0].Value);
                    int count = ServiceHelper.countCategoryItems(categoryID);
                    DialogResult dr;
                    if (count > 0)
                    {
                        dr = MessageBox.Show($"Danh sách đang chứa {count} dịch vụ. Bạn chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    }
                    else
                    {
                        dr = MessageBox.Show("Bạn chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    }

                    if (dr == DialogResult.Yes)
                    {
                        if (ServiceHelper.removeCategory(categoryID))
                        {
                            MessageBox.Show("Xóa thành công", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadAllCategory();
                            loadAllService();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                if (data_loaiDichvu.Columns[e.ColumnIndex].Name == "col_category_Info")
                {
                    loadForm(new NewCategory(this, Convert.ToInt32(data_loaiDichvu.Rows[e.RowIndex].Cells[0].Value), data_loaiDichvu.Rows[e.RowIndex].Cells[1].Value.ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR");
                MessageBox.Show(ex.Message);
            }
        }

        private void data_category_items_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (data_category_items.Columns[e.ColumnIndex].Name == "col_active_item")
                {
                    int serviceID = Convert.ToInt32(data_category_items.Rows[e.RowIndex].Cells[0].Value);
                    DialogResult dr = MessageBox.Show("Bạn chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (ServiceHelper.removeService(serviceID))
                        {
                            MessageBox.Show("Xóa thành công", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadAllService();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (data_category_items.Columns[e.ColumnIndex].Name == "col_btn_info")
                {
                    int serviceID = Convert.ToInt32(data_category_items.Rows[e.RowIndex].Cells[0].Value);
                    loadForm(new EditService(this, serviceID));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void data_loaiDichvu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int categoryId = Convert.ToInt32(data_loaiDichvu.Rows[e.RowIndex].Cells[0].Value);
                DataTable dt = ServiceHelper.getServiceByCategoryID(categoryId);
                data_category_items.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr[0]);
                    string txt = Convert.ToString(dr[1]);
                    data_category_items.Rows.Add(id, txt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_refesh_service_Click(object sender, EventArgs e)
        {
            loadAllService();
        }

        private void btn_search_category_Click(object sender, EventArgs e)
        {
            int index = cb_category_search.SelectedIndex;
            int id;
            if (index == 0)
            {
                if (!Int32.TryParse(tb_category_search.Text, out id))
                {
                    MessageBox.Show("Vui lòng nhập số", "Tìm kiếm loại dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                loadCategory(ServiceHelper.getCategoryByID(id));
            }
            else if (index == 1)
            {
                loadCategory(ServiceHelper.getCategoryByTitle(tb_category_search.Text.Trim()));
            }
        }
    }
}
