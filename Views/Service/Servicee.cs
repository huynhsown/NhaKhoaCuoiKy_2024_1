using Dapper;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Printing;
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

        private void btn_search_service_Click(object sender, EventArgs e)
        {
            if (cbb_service.SelectedIndex == 0)
            {
                if (!Int32.TryParse(tb_search_service.Text, out _))
                {
                    MessageBox.Show("Vui lòng nhập số", "Tìm kiếm dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                loadService(ServiceHelper.getServiceByID(Convert.ToInt32(tb_search_service.Text)));
            }
            else
            {
                loadService(ServiceHelper.getServiceByTitle(tb_search_service.Text.Trim()));
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            printInvoice();
        }

        void printInvoice()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            pd.Document = doc;
            doc.PrintPage += Doc_PrintPage;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {

            // Draw the text onto the print page
            e.Graphics.DrawString("Danh sách loại dịch vụ", new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(330, 20));
            e.Graphics.DrawString("Sở y tê UTE", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 20));
            e.Graphics.DrawString("Nha khoa FS", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 55));
            e.Graphics.DrawString($"Ngày in: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new Font("Times New Roman", 12), Brushes.Black, new Point(600, 20));
            int x = 30;
            int y = 120;

            if (data_loaiDichvu.Rows.Count > 0)
            {
                DataGridViewRow headerRow = data_category_items.Rows[0];
                foreach (DataGridViewCell headerCell in headerRow.Cells)
                {
                    if (headerCell.Visible && data_loaiDichvu.Columns[headerCell.ColumnIndex].Name != "col_category_Info" && data_loaiDichvu.Columns[headerCell.ColumnIndex].Name != "col_active")
                    {
                        Rectangle headerRect = new Rectangle(x, y, 790 / 2, headerRow.Height);
                        e.Graphics.FillRectangle(Brushes.White, headerRect);
                        e.Graphics.DrawRectangle(Pens.Black, headerRect);
                        e.Graphics.DrawString(data_loaiDichvu.Columns[headerCell.ColumnIndex].HeaderText,
                            data_loaiDichvu.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        x += headerRect.Width;
                    }
                }
                y += data_loaiDichvu.Rows[0].Height;

                foreach (DataGridViewRow dvr in data_loaiDichvu.Rows)
                {
                    x = 30;
                    foreach (DataGridViewCell cell in dvr.Cells)
                    {
                        if (cell.Visible && data_loaiDichvu.Columns[cell.ColumnIndex].Name != "col_category_Info" && data_loaiDichvu.Columns[cell.ColumnIndex].Name != "col_active")
                        {
                            Rectangle headerRect = new Rectangle(x, y, 790 / 2, cell.Size.Height);
                            e.Graphics.DrawRectangle(Pens.Black, headerRect);
                            e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                data_loaiDichvu.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            x += headerRect.Width;
                        }
                    }
                    y += dvr.Height;
                }
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            printInvoice_1();
        }

        void printInvoice_1()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            pd.Document = doc;
            doc.PrintPage += Doc_PrintPage_1;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        void Doc_PrintPage_1(object sender, PrintPageEventArgs e)
        {

            // Draw the text onto the print page
            e.Graphics.DrawString("Danh sách dịch vụ", new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(330, 20));
            e.Graphics.DrawString("Sở y tê UTE", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 20));
            e.Graphics.DrawString("Nha khoa FS", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 55));
            e.Graphics.DrawString($"Ngày in: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new Font("Times New Roman", 12), Brushes.Black, new Point(600, 20));
            int x = 30;
            int y = 120;

            if (data_category_items.Rows.Count > 0)
            {
                DataGridViewRow headerRow = data_category_items.Rows[0];
                foreach (DataGridViewCell headerCell in headerRow.Cells)
                {
                    if (headerCell.Visible && data_category_items.Columns[headerCell.ColumnIndex].Name != "col_btn_info" && data_category_items.Columns[headerCell.ColumnIndex].Name != "col_active_item")
                    {
                        Rectangle headerRect = new Rectangle(x, y, 790 / 2, headerRow.Height);
                        e.Graphics.FillRectangle(Brushes.White, headerRect);
                        e.Graphics.DrawRectangle(Pens.Black, headerRect);
                        e.Graphics.DrawString(data_category_items.Columns[headerCell.ColumnIndex].HeaderText,
                            data_category_items.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        x += headerRect.Width;
                    }
                }
                y += data_category_items.Rows[0].Height;

                foreach (DataGridViewRow dvr in data_category_items.Rows)
                {
                    x = 30;
                    foreach (DataGridViewCell cell in dvr.Cells)
                    {
                        if (cell.Visible && data_category_items.Columns[cell.ColumnIndex].Name != "col_btn_info" && data_category_items.Columns[cell.ColumnIndex].Name != "col_active_item")
                        {
                            Rectangle headerRect = new Rectangle(x, y, 790 / 2, cell.Size.Height);
                            e.Graphics.DrawRectangle(Pens.Black, headerRect);
                            e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                data_category_items.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            x += headerRect.Width;
                        }
                    }
                    y += dvr.Height;
                }
            }
        }

    }
}
