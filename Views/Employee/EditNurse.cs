using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Views.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;
using System.Xml.Linq;

namespace NhaKhoaCuoiKy.Views.Employee
{
    public partial class EditNurse : Form
    {
        Nurse nurse;
        int nurseId;
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
        public EditNurse(Nurse nurse, int nurseId)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.nurse = nurse;
            this.nurseId = nurseId;
        }
        public EditNurse()
        {
            InitializeComponent();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (!verify())
            {
                MessageBox.Show("Dữ liệu thiếu hoặc sai", "Sửa y tá", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            
            {
                string name = tb_name.Text;
                string hocVi = tb_hocvi.Text;
                string chuyenMon = tb_chuyenmon.Text;
                DateTime birth = dtp_birth.Value;
                string phone = tb_sodienthoai.Text;
                int homenum = int.Parse(tb_homenum.Text);
                string ward = tb_ward.Text;
                string city = tb_city.Text;
                string gender = "";
                string street = tb_street.Text;
                string position = tb_vitrilamviec.Text;
                DateTime beginwork = dtp_beginwork.Value;
                int salary = int.Parse(tb_tienluong.Text);
                if (rdb_male.Checked)
                {
                    gender = "Nam";
                }
                else if (rdb_female.Checked)
                {
                    gender = "Nữ";
                }
                byte[] img = null;
                
                // Chuyển đổi hình ảnh từ PictureBox thành mảng byte
                

                // Gọi phương thức để cập nhật thông tin của y tá trong cơ sở dữ liệu
                if (EmployeeHelper.updateNurse(nurseId, name, hocVi, chuyenMon, gender, birth, salary, beginwork, homenum, ward, city, position, img, phone, street))
                {
                    MessageBox.Show("Sửa y tá thành công", "Sửa y tá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Load lại danh sách y tá
                    nurse.loadAllNurse();
                    // Đóng form chỉnh sửa
                    Close();
                }
                else
                {
                    MessageBox.Show("Sửa y tá thất bại", "Sửa y tá", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }

        private bool verify()
        {
            if (tb_name.Text.Trim().Length == 0
                || dtp_birth.Value.ToString().Trim().Length == 0
                || tb_chuyenmon.Text.Trim().Length == 0
                || tb_hocvi.Text.Trim().Length == 0
                || tb_sodienthoai.Text.Trim().Length == 0
                || tb_homenum.Text.Trim().Length == 0
                || tb_ward.Text.Trim().Length == 0
                || tb_city.Text.Trim().Length == 0
                || tb_street.Text.Trim().Length == 0
                || tb_vitrilamviec.Text.Trim().Length == 0
                || dtp_beginwork.Value.ToString().Trim().Length == 0
                || tb_tienluong.Text.Trim().Length == 0
                || pb_avt.Image.ToString().Trim().Length == 0) return false;


            if (rdb_male.Checked == false && rdb_female.Checked == false && rdb_other.Checked == false) return false;


            //if (tb_discount.BorderThickness == 3
            //    || tb_price.BorderThickness == 3
            //    || tb_title.BorderThickness == 3
            //    || tb_unit.BorderThickness == 3
            //    || tb_warranty.BorderThickness == 3) return false;
            return true;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditNurse_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = EmployeeHelper.getNurseByID(nurseId);

                if (dataTable.Rows.Count > 0)
                {
                    string hoTen = dataTable.Rows[0]["HoVaTen"].ToString();
                    string gioiTinh = dataTable.Rows[0]["GioiTinh"].ToString().Trim();
                    DateTime ngaySinh = (DateTime)dataTable.Rows[0]["NgaySinh"];
                    int tienLuong = (int)dataTable.Rows[0]["TienLuong"];
                    DateTime ngayBatDauLamViec = (DateTime)dataTable.Rows[0]["NgayBatDauLamViec"];
                    int soNha = (int)dataTable.Rows[0]["SoNha"];
                    string phuong = dataTable.Rows[0]["Phuong"].ToString();
                    string thanhPho = dataTable.Rows[0]["ThanhPho"].ToString();
                    string viTriLamViec = dataTable.Rows[0]["ViTriLamViec"].ToString();
                    byte[] anh = (byte[])dataTable.Rows[0]["Anh"];
                    string soDienThoai = dataTable.Rows[0]["SoDienThoai"].ToString();
                    string tenDuong = dataTable.Rows[0]["TenDuong"].ToString();
                    string hocVi = dataTable.Rows[0]["HocVi"].ToString();
                    string chuyenMon = dataTable.Rows[0]["ChuyenMon"].ToString();

                    // Thiết lập lại giá trị của các component từ dữ liệu đã lấy ra
                    tb_name.Text = hoTen;
                    tb_hocvi.Text = hocVi;
                    tb_chuyenmon.Text = chuyenMon;
                    dtp_birth.Value = ngaySinh;
                    tb_sodienthoai.Text = soDienThoai;
                    tb_homenum.Text = soNha.ToString();
                    tb_ward.Text = phuong;
                    tb_city.Text = thanhPho;
                    tb_street.Text = tenDuong;
                    tb_vitrilamviec.Text = viTriLamViec;
                    dtp_beginwork.Value = ngayBatDauLamViec;
                    tb_tienluong.Text = tienLuong.ToString();

                    // Thiết lập giá trị của RadioButton cho giới tính
                    if (gioiTinh == "Nam")
                    {
                        rdb_male.Checked = true;
                    }
                    else if (gioiTinh == "Nữ")
                    {
                        rdb_female.Checked = true;
                    }

                    // Thiết lập hình ảnh cho PictureBox
                    using (MemoryStream ms = new MemoryStream(anh))
                    {
                        pb_avt.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    // Xử lý khi không tìm thấy thông tin y tá với nurseId đã cho
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
            }
        }


    }
}
