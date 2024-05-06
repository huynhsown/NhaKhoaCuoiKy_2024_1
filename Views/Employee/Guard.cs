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
    public partial class Guard : Form
    {
        public Guard()
        {
            InitializeComponent();
        }
        private MainForm mainForm;
        private NewGuard newGuard;
        private Validate validate = new Validate();


        public Guard(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void guna2Button_themmoi_Click(object sender, EventArgs e)
        {
            newGuard = new NewGuard(this);
            ViewHelper.loadForm(newGuard, mainForm);
        } 
        private void btn_search_Click(object sender, EventArgs e)
        {
            int index = cb_filter.SelectedIndex;
            int id;
            string ten;
            string soDienThoai;
            if (index == 0)
            {
                if (!Int32.TryParse(tb_filter_search.Text, out id))
                {
                    MessageBox.Show("Vui lòng nhập số", "Tìm kiếm theo mã bảo vệ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                loadGuard(EmployeeHelper.getEmployeeByID(id));
            }
            else if (index == 1)
            {
                if (tb_filter_search.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập tên ", "Tìm kiếm theo tên bảo vệ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                ten = tb_filter_search.Text.Trim();
                loadGuard(EmployeeHelper.getEmployeeByName(ten));
            }
            else if (index == 2)
            {
                if (tb_filter_search.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập số điện thoại ", "Tìm kiếm theo số điện thoại ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                soDienThoai = tb_filter_search.Text.Trim();
                loadGuard(EmployeeHelper.getEmployeeByPhoneNum(soDienThoai));
            }

        }

        public void loadGuard(DataTable dt)
        {
            // Clear existing rows in data_baoVe DataTable
            data_baoVe.Rows.Clear();

            // Check if dt is not null and contains rows
            if (dt != null && dt.Rows.Count > 0)
            {
                // Loop through each row in the DataTable
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    try
                    {
                        // Retrieve guard information from the current row
                        int id = Convert.ToInt32(dt.Rows[i]["MaNhanVien"]);
                        string hoTen = dt.Rows[i]["HoVaTen"].ToString();
                        string soDienThoai = dt.Rows[i]["SoDienThoai"].ToString();

                        // Convert date of birth to short date string
                        string ngaySinh = Convert.ToDateTime(dt.Rows[i]["NgaySinh"]).ToShortDateString();

                        // Convert house number to integer
                        int soNha = Convert.ToInt32(dt.Rows[i]["SoNha"]);

                        // Retrieve address components
                        string duong = dt.Rows[i]["TenDuong"].ToString();
                        string phuong = dt.Rows[i]["Phuong"].ToString();
                        string thanhPho = dt.Rows[i]["ThanhPho"].ToString();

                        // Assemble the address string
                        string diaChi = $"{soNha} {duong} {phuong} {thanhPho}";

                        // Retrieve gender
                        string gioiTinh = dt.Rows[i]["GioiTinh"].ToString();

                        // Add guard information to the data_baoVe DataTable
                        data_baoVe.Rows.Add(id, hoTen, soDienThoai, ngaySinh, diaChi, gioiTinh);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that occur during data retrieval
                        MessageBox.Show($"Error processing row {i + 1}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Display a message if there is no information available
                MessageBox.Show("Khong co thong tin!", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void Guard_Load(object sender, EventArgs e)
        {
            loadAllGuard();
        }

        public void loadAllGuard()
        {
            try
            {
                DataTable dt = EmployeeHelper.getAllGuard();
                loadGuard(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void guna2Button_save_Click(object sender, EventArgs e)
        {
            if (data_baoVe.Rows.Count == 0 || data_baoVe.Columns.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để lưu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            saveFileDialog.Title = "Save File";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog.FileName;

                using (var writer = new StreamWriter(path))
                {
                    for (int i = 0; i < data_baoVe.Rows.Count; i++)
                    {
                        for (int j = 0; j < data_baoVe.Columns.Count - 1; j++)
                        {
                            if (data_baoVe.Rows[i].Cells[j].Value != null)
                            {
                                writer.Write("\t" + data_baoVe.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
                            }
                            else
                            {
                                writer.Write("\t" + "\t" + "|"); // Ghi một dấu cách nếu giá trị là null
                            }
                        }
                        writer.WriteLine("");
                        writer.WriteLine("--------------------------------------------------------------------------------------");
                    }
                }
            }
        }

        private void guna2Button_print_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printdoc = new PrintDocument();
            printdoc.DocumentName = "Print Document";
            printDialog.Document = printdoc;
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;

            if (printDialog.ShowDialog() == DialogResult.OK) printdoc.Print();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            loadAllGuard();
        }

        private void bt_remove_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng được chọn không
            if (data_baoVe.SelectedRows.Count > 0)
            {
                // Lấy ID của bác sĩ từ cột MaBS của dòng được chọn
                int doctorId = Convert.ToInt32(data_baoVe.SelectedRows[0].Cells["MaBV"].Value);

                // Gọi phương thức removeDoctor và xử lý kết quả
                try
                {
                    bool removed = EmployeeHelper.removeGuard(doctorId);
                    if (removed)
                    {
                        MessageBox.Show("Xóa thông tin bảo vệ thành công.");
                        loadAllGuard();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin bảo vệ không thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa.");
            }
        }

        private void data_baoVe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (data_baoVe.Columns[e.ColumnIndex].Name == "HanhDong")
                {
                    int guardId = Convert.ToInt32(data_baoVe.SelectedRows[0].Cells["MaBV"].Value);
                    DialogResult dr = MessageBox.Show("Bạn chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        if (EmployeeHelper.removeGuard(guardId))
                        {
                            MessageBox.Show("Xóa thành công", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            loadAllGuard();
                        }
                        else
                        {
                            MessageBox.Show("Xóa thất bại", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                if (data_baoVe.Columns[e.ColumnIndex].Name == "ThongTin")
                {
                    int guardId = Convert.ToInt32(data_baoVe.SelectedRows[0].Cells["MaBV"].Value);
                    loadForm(new EditGuard(this, guardId));
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

        private void data_baoVe_SelectionChanged(object sender, EventArgs e)
        {
            if (data_baoVe.SelectedCells.Count > 0)
            {
                // Hiển thị tất cả các cell trong DataGridView
                foreach (DataGridViewRow row in data_baoVe.Rows)
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
                foreach (DataGridViewRow row in data_baoVe.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.Style.ForeColor = Color.White; // Đặt màu chữ thành màu nền (ẩn cell)
                        cell.Style.BackColor = Color.White; // Đặt màu nền thành màu nền (ẩn cell)
                    }
                }
            }
        }
    }
}
