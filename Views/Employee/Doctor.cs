using Dapper;
using NhaKhoaCuoiKy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Employee
{
    public partial class Doctor : Form
    {
        public Doctor()
        {
            InitializeComponent();
        }

        private MainForm mainForm;
        private NewDoctor newDoctor;
        private Validate validate = new Validate();

        public Doctor(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }



        private void btn_search_Click(object sender, EventArgs e)
        {

        }

        private void loadDoctor(DataTable dt)
        {
            // Check if data_bacSi is not null before clearing its rows
            data_bacSi.Rows.Clear();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Perform null checks and type conversions
                    int id;
                    if (int.TryParse(dt.Rows[i]["MaNhanVien"].ToString(), out id))
                    {
                        string hoTen = dt.Rows[i]["HoVaTen"].ToString();
                        string hocVi = dt.Rows[i]["HocVi"].ToString();
                        string chuyenMon = dt.Rows[i]["ChuyenMon"].ToString();
                        string soDienThoai = dt.Rows[i]["SoDienThoai"].ToString();

                        // Handle date format conversion
                        string ngaySinh = "";
                        DateTime ngaySinhDate;
                        if (DateTime.TryParse(dt.Rows[i]["NgaySinh"].ToString(), out ngaySinhDate))
                        {
                            ngaySinh = ngaySinhDate.ToShortDateString();
                        }

                        int soNha;
                        if (int.TryParse(dt.Rows[i]["SoNha"].ToString(), out soNha))
                        {
                            string duong = dt.Rows[i]["TenDuong"].ToString();
                            string phuong = dt.Rows[i]["Phuong"].ToString();
                            string thanhPho = dt.Rows[i]["ThanhPho"].ToString();
                            string gioiTinh = dt.Rows[i]["GioiTinh"].ToString();

                            // Check if address components are available before forming the address string
                            string diaChi = soNha.ToString() + " " + duong + " " + phuong + " " + thanhPho;
                            data_bacSi.Rows.Add(id, hoTen, hocVi, chuyenMon, soDienThoai, ngaySinh, gioiTinh, diaChi);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Khong co thong tin!", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void Doctor_Load(object sender, EventArgs e)
        {

         


            loadAllDoctor();
        }

        public void loadAllDoctor()
        {
            try
            {
                DataTable dt = EmployeeHelper.getAllDoctor();
                loadDoctor(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void data_bacSi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (data_bacSi.Columns[e.ColumnIndex].Name == "HanhDong")
                {
                    int doctorId = Convert.ToInt32(data_bacSi.SelectedRows[0].Cells["MaBS"].Value);
                    DialogResult dr = MessageBox.Show("Bạn chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (EmployeeHelper.removeDoctor(doctorId))
                        {
                            MessageBox.Show("Xóa thành công", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadAllDoctor();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (data_bacSi.Columns[e.ColumnIndex].Name == "ThongTin")
                {
                    int doctorId = Convert.ToInt32(data_bacSi.SelectedRows[0].Cells["MaBS"].Value);
                    loadForm(new EditDoctor(this, doctorId));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private void btn_search_Click_1(object sender, EventArgs e)
        {
            int index = cb_filter.SelectedIndex;
            int id;
            string ten;
            string soDienThoai;
            if (index == 0)
            {
                if (!Int32.TryParse(tb_filter_search.Text, out id))
                {
                    MessageBox.Show("Vui lòng nhập số", "Tìm kiếm theo mã bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                loadDoctor(EmployeeHelper.getDoctorByID(id));
            }
            else if (index == 1)
            {
                if (tb_filter_search.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên ", "Tìm kiếm theo tên bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                ten = tb_filter_search.Text.Trim();
                loadDoctor(EmployeeHelper.getDoctorByName(ten));
            }
            else if (index == 2)
            {
                if (tb_filter_search.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại ", "Tìm kiếm theo số điện thoại ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                soDienThoai = tb_filter_search.Text.Trim();
                loadDoctor(EmployeeHelper.getDoctorByPhoneNum(soDienThoai));
            }
        }

        private void btn_refresh_Click_1(object sender, EventArgs e)
        {
            loadAllDoctor();
        }

        private void guna2Button_print_Click(object sender, EventArgs e)
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
            // Define the dimensions and position of the rectangle


            // Draw the text onto the print page
            e.Graphics.DrawString("Danh sách bác sĩ", new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(330, 20));
            e.Graphics.DrawString("Sở y tê UTE", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 20));
            e.Graphics.DrawString("Nha khoa FS", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 55));
            e.Graphics.DrawString($"Ngày in: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new Font("Times New Roman", 12), Brushes.Black, new Point(600, 20));

            int x = 30;
            int y = 150;
            if (data_bacSi.Rows.Count > 0)
            {
                DataGridViewRow headerRow = data_bacSi.Rows[0];
                foreach (DataGridViewCell headerCell in headerRow.Cells)
                {
                    if (headerCell.Visible && !(data_bacSi.Columns[headerCell.ColumnIndex].ValueType is DataGridViewButtonCell))
                    {
                        Rectangle headerRect = new Rectangle(x, y, headerCell.Size.Width, headerCell.Size.Height);

                        e.Graphics.FillRectangle(Brushes.White, headerRect);
                        e.Graphics.DrawRectangle(Pens.Black, headerRect);
                        e.Graphics.DrawString(data_bacSi.Columns[headerCell.ColumnIndex].HeaderText,
                            data_bacSi.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        x += headerRect.Width;
                    }
                }
                y += data_bacSi.Rows[0].Height;

                foreach (DataGridViewRow dvr in data_bacSi.Rows)
                {
                    x = 30;
                    foreach (DataGridViewCell cell in dvr.Cells)
                    {
                        if (cell.Visible && data_bacSi.Columns[cell.ColumnIndex].Name != "col_btn_delete")
                        {
                            Rectangle headerRect = new Rectangle(x, y, cell.Size.Width, cell.Size.Height);
                            e.Graphics.DrawRectangle(Pens.Black, headerRect);
                            e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                data_bacSi.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            x += headerRect.Width;
                        }
                    }
                    y += dvr.Height;
                }
            }
        }

        private void button_themmoi_Click(object sender, EventArgs e)
        {
            newDoctor = new NewDoctor(this);
            ViewHelper.loadForm(newDoctor, mainForm);
        }

        private void data_bacSi_SelectionChanged(object sender, EventArgs e)
        {
            if (data_bacSi.SelectedCells.Count > 0)
            {
                // Hiển thị tất cả các cell trong DataGridView
                foreach (DataGridViewRow row in data_bacSi.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.ForeColor = Color.Black; // Đặt màu chữ thành màu mặc định
                        cell.Style.BackColor = Color.White; // Đặt màu nền thành màu mặc định
                    }
                }
            }
            else
            {
                // Ẩn tất cả các cell trong DataGridView
                foreach (DataGridViewRow row in data_bacSi.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.ForeColor = Color.White; // Đặt màu chữ thành màu nền (ẩn cell)
                        cell.Style.BackColor = Color.White; // Đặt màu nền thành màu nền (ẩn cell)
                    }
                }
            }
        }

        private void guna2GroupBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
