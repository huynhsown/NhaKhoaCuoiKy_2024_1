using Dapper;
using Guna.UI2.WinForms;
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

namespace NhaKhoaCuoiKy.Views.Employee
{
    public partial class NewDoctor : Form
    {
        private Validate validate = new Validate();
        public EventHandler eventAddDoctor;
        public DynamicParameters p { get; set; }
        private Doctor doctor;

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


        public NewDoctor()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.doctor = doctor;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
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

                int id = EmployeeHelper.addNewDoctor(name, hocVi, chuyenMon, gender, birth, salary, beginwork, homenum, ward, city, position, img, phone, street);
                if (id != -1)
                {
                    MessageBox.Show("Thêm bác sĩ thành công", "Thêm bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Thêm bác sĩ thất bại", "Thêm bác sĩ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //DataTable dt = EmployeeHelper.getEmployeeByID(id);
                //DataRow dr = dt.Rows[0];
                //eventAddGuard?.Invoke(sender, e);

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR::" + ex.Message);
            }
        }

        private void btn_uploadImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog opf = new OpenFileDialog())
            {
                opf.Filter = "Select Image(*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
                if ((opf.ShowDialog() == DialogResult.OK))
                {
                    pb_avt.Image = Image.FromFile(opf.FileName);
                }
            }
        }



        private void warningValidate(PictureBox picbox, Guna2TextBox tb, bool check)
        {
            picbox.Visible = (check || tb.Text.Length == 0) ? false : true;
            tb.BorderColor = (check || tb.Text.Length == 0) ? Color.Black : Color.Red;
            tb.BorderThickness = (check || tb.Text.Length == 0) ? 1 : 3;
        }

        private void warningValidate2(PictureBox picbox, Guna2DateTimePicker tb, bool check)
        {
            picbox.Visible = (check || tb.Text.Length == 0) ? false : true;
            tb.BorderColor = (check || tb.Text.Length == 0) ? Color.Black : Color.Red;
            tb.BorderThickness = (check || tb.Text.Length == 0) ? 1 : 3;
        }

        private void tb_sodienthoai_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_phone, tb_sodienthoai, validate.validateNumber(tb_sodienthoai.Text));

        }

        private void tb_homenum_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_homenum, tb_homenum, validate.validateNumber(tb_homenum.Text));
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_name, tb_name, validate.validateName(tb_name.Text));

        }

        private void tb_tienluong_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_tienluong, tb_tienluong, validate.validateNumber(tb_tienluong.Text));
        }

        private void dtp_birth_ValueChanged(object sender, EventArgs e)
        {
            DateTime birthDate = dtp_birth.Value;
            TimeSpan ageDifference = DateTime.Now - birthDate;
            int ageInYears = (int)(ageDifference.TotalDays / 365.25);

            if (ageInYears < 18)
            {
                warningValidate2(pb_birth, dtp_birth, false); // Hiển thị cảnh báo khi tuổi dưới 18
            }
            else
            {
                warningValidate2(pb_birth, dtp_birth, true); // Ẩn cảnh báo khi tuổi từ 18 trở lên
            }
        }
    }
}
