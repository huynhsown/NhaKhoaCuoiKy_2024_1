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

        private void loadDoctor(DataTable dt)
        {
            // Check if data_bacSi is not null before clearing its rows
            if (data_bacSi != null)
            {
                data_bacSi.Rows.Clear();
            }
            else
            {

            }

            if (dt != null && dt.Rows.Count > 0)
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
                            if (!string.IsNullOrEmpty(duong) && !string.IsNullOrEmpty(phuong) && !string.IsNullOrEmpty(thanhPho))
                            {
                                string diaChi = soNha.ToString() + " " + duong + " " + phuong + " " + thanhPho;
                                data_bacSi.Rows.Add(id, hoTen, hocVi, chuyenMon, soDienThoai, ngaySinh, gioiTinh, diaChi);
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Khong co thong tin!", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void button_themmoi_Click(object sender, EventArgs e)
        {
            newDoctor?.Close();
            newDoctor = new NewDoctor();
            newDoctor.Owner = this;
            newDoctor.Show();
            newDoctor.eventAddDoctor += (s, e) =>
            {
                DynamicParameters p = new DynamicParameters();
                int maBS = p.Get<int>("@MaNhanVien");
                string hoTen = p.Get<string>("@HoVaTen");
                string hocVi = p.Get<string>("@HocVi");
                string chuyenMon = p.Get<string>("ChuyenMon");
                string gioiTinh = p.Get<string>("@GioiTinh");
                string ngaySinh = p.Get<DateTime>("@NgaySinh").ToShortDateString();
                int tienLuong = p.Get<int>("@TienLuong");
                string ngayBDLV = p.Get<DateTime>("@NgayBatDauLamViec").ToShortDateString();
                int soNha = p.Get<int>("@SoNha");
                string soDienThoai = p.Get<string>("@SoDienThoai");
                string duong = p.Get<string>("@TenDuong");
                string phuong = p.Get<string>("@Phuong");
                string thanhPho = p.Get<string>("@ThanhPho");
                string viTriLamViec = p.Get<string>("@ViTriLamViec");
                string diaChi = soNha.ToString() + " " + duong + " " + phuong + " " + thanhPho;
                data_bacSi.Rows.Add(maBS, hoTen, hocVi, chuyenMon, soDienThoai, ngaySinh, gioiTinh, diaChi);
            };
        }

        private void Doctor_Load(object sender, EventArgs e)
        {
            loadAllDoctor();
        }

        private void loadAllDoctor()
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

        private void guna2Button_save_Click(object sender, EventArgs e)
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

        private void guna2Button_print_Click_1(object sender, EventArgs e)
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
            loadAllDoctor();
        }

        private void bt_remove_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng được chọn không
            if (data_bacSi.SelectedRows.Count > 0)
            {
                // Lấy ID của bác sĩ từ cột MaBS của dòng được chọn
                int doctorId = Convert.ToInt32(data_bacSi.SelectedRows[0].Cells["MaBS"].Value);

                // Gọi phương thức removeDoctor và xử lý kết quả
                try
                {
                    bool removed = EmployeeHelper.removeDoctor(doctorId);
                    if (removed)
                    {
                        MessageBox.Show("Xóa thông tin bác sĩ thành công.");
                        loadAllDoctor();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thông tin bác sĩ không thành công.");
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
    }
}
