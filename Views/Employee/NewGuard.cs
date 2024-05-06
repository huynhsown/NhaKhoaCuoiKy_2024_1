using Dapper;
using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.dbs;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.Service;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
namespace NhaKhoaCuoiKy.Views.Employee
{
    public partial class NewGuard : Form
    {
        public DynamicParameters p { get; set; }
        public EventHandler eventAddGuard;
        private Validate validate = new Validate();
        private Guard guard;

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

        public NewGuard()
        {
            InitializeComponent();
        }

        public NewGuard(Guard guard)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.guard = guard;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.FromArgb(100, 17, 34, 71), ButtonBorderStyle.Solid);
        }

        private void NewGuard_Load(object sender, EventArgs e)
        {
            try
            {
                Database db = new Database();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }



        private void btn_add_Click(object sender, EventArgs e)
        {

        }

        private void btn_uploadImg_Click_1(object sender, EventArgs e)
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

        private void btn_add_Click_1(object sender, EventArgs e)
        {
            try
            {
                string name = tb_name.Text;
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

                int id = EmployeeHelper.addNewEmployee(name, gender, birth, salary, beginwork, homenum, ward, city, position, img, phone, street);
                if (id != -1)
                {
                    MessageBox.Show("Thêm bảo vệ thành công", "Thêm nhân viên", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Thêm bảo vệ thất bại", "Thêm dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_name, tb_name, validate.validateName(tb_name.Text));
        }

        private void tb_tienluong_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_tienluong, tb_tienluong, validate.validateNumber(tb_tienluong.Text));
        }

        private void tb_homenum_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_homenum, tb_homenum, validate.validateNumber(tb_homenum.Text));
        }

        private void tb_sodienthoai_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_phone, tb_sodienthoai, validate.validateNumber(tb_sodienthoai.Text));
        }
    }
}