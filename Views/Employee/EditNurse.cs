using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Views.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Employee
{
    public partial class EditNurse : Form
    {
        Nurse nurse;
        int doctorId;
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
        public EditNurse(Nurse nurse, int doctorId)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.nurse = nurse;
            this.doctorId = doctorId;
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
            try
            {
                string name = tb_name.Text;
                string hocVi = tb_hocvi.Text;
                string chuyenMon = tb_chuyenmon.Text;
                DateTime birth = dtp_birth.Value;
                string phone = tb_sodienthoai.Text;
                int homenum = int.Parse(tb_homenum.Text);
                string ward = tb_ward.Text;
                string city = tb_city.Text;
                string gender = "other";
                string street = tb_street.Text;
                string position = tb_vitrilamviec.Text;
                DateTime beginwork = dtp_beginwork.Value;
                int salary = int.Parse(tb_tienluong.Text);
                if (rdb_male.Checked)
                {
                    gender = "nam";
                }
                else if (rdb_female.Checked)
                {
                    gender = "nu";
                }
                MemoryStream ms = new MemoryStream();
                pb_avt.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] img = ms.ToArray();
                if (EmployeeHelper.updateDoctor(doctorId, name, hocVi, chuyenMon, gender, birth, salary, beginwork, homenum, ward, city, position, img, phone, street))
                {
                    MessageBox.Show("Sửa y tá thành công", "Sửa y tá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    nurse.loadAllNurse();
                    Close();
                }
                else
                {
                    MessageBox.Show("Sửa y tá thất bại", "Sửa y tá", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
    }
}
