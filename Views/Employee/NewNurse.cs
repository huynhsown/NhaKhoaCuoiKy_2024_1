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
    public partial class NewNurse : Form
    {
        public DynamicParameters p { get; set; }
        public EventHandler eventAddNurse;
        private Validate validate = new Validate();
        private Nurse nurse;


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


        public NewNurse()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.nurse = nurse;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

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


                if (EmployeeHelper.addNewNurse(name, hocVi, chuyenMon, gender, birth, salary, beginwork, homenum, ward, city, position, img, phone, street))
                {
                    MessageBox.Show("Thêm y tá thành công", "Thêm y tá", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Thêm y tá thất bại", "Thêm y tá", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void warningValidate(PictureBox picbox, Guna2TextBox tb, bool check)
        {
            picbox.Visible = (check || tb.Text.Length == 0) ? false : true;
            tb.BorderColor = (check || tb.Text.Length == 0) ? Color.Black : Color.Red;
            tb.BorderThickness = (check || tb.Text.Length == 0) ? 1 : 3;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tb_name_TextChanged_1(object sender, EventArgs e)
        {
            warningValidate(pb_name, tb_name, validate.validateName(tb_name.Text));
        }

        private void tb_sodienthoai_TextChanged_1(object sender, EventArgs e)
        {
            warningValidate(pb_phone, tb_sodienthoai, validate.validateNumber(tb_sodienthoai.Text));
        }

        private void tb_homenum_TextChanged_1(object sender, EventArgs e)
        {
            warningValidate(pb_homenum, tb_homenum, validate.validateNumber(tb_homenum.Text));
        }

        private void tb_tienluong_TextChanged_1(object sender, EventArgs e)
        {
            warningValidate(pb_tienluong, tb_tienluong, validate.validateNumber(tb_tienluong.Text));
        }
    }
}
