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
            PrintDialog printDialog = new PrintDialog();
            PrintDocument printdoc = new PrintDocument();
            printdoc.DocumentName = "Print Document";
            printDialog.Document = printdoc;
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;

            if (printDialog.ShowDialog() == DialogResult.OK) printdoc.Print();
        }

        private void guna2Button_save_Click_1(object sender, EventArgs e)
        {
            if (data_bacSi.Rows.Count == 0 || data_bacSi.Columns.Count == 0)
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
                    for (int i = 0; i < data_bacSi.Rows.Count; i++)
                    {
                        for (int j = 0; j < data_bacSi.Columns.Count - 1; j++)
                        {
                            if (data_bacSi.Rows[i].Cells[j].Value != null)
                            {
                                writer.Write("\t" + data_bacSi.Rows[i].Cells[j].Value.ToString() + "\t" + "|");
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
