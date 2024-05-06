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

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class EditPatient : Form
    {
        int patientID;
        Patient patient;

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

        public EditPatient(int patientID, Patient patient)
        {
            InitializeComponent();
            this.patientID = patientID;
            this.patient = patient;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void EditPatient_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = PatientHelper.getByID(patientID);
                if (dt.Rows.Count != 1) Close();
                string name = Convert.ToString(dt.Rows[0]["HoVaTen"]).Trim();
                DateTime dob = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                string h_number = dt.Rows[0]["SoNha"].ToString();
                string s_name = dt.Rows[0]["TenDuong"].ToString();
                string ward = dt.Rows[0]["Phuong"].ToString();
                string city = dt.Rows[0]["ThanhPho"].ToString();
                string address = h_number + ", " + s_name + ", " + ward + ", " + city;
                string phone = dt.Rows[0]["SoDienThoai"].ToString();
                string gender = dt.Rows[0]["GioiTinh"].ToString().Trim();
                Image img;
                using (MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Anh"]))
                {
                    img = Image.FromStream(ms);
                }
                tb_name.Text = name;
                dtp_birth.Value = dob;
                tb_phone.Text = phone;
                tb_homenum.Text = h_number;
                tb_street.Text = s_name;
                tb_ward.Text = ward;
                tb_city.Text = city;
                pb_avt.Image = img;
                if (gender == "Nam") rdb_male.Checked = true;
                else if (gender == "Nữ") rdb_female.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Chắc chắn xóa bệnh nhân này (tất cả thông tin liên quan tới bệnh nhân đều được xóa)", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                if (PatientHelper.removePatientAndRelatedRecords(patientID))
                {
                    MessageBox.Show("Xóa thông tin bệnh nhân thành công", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    patient.addToDataGrid(PatientHelper.getAllPatient());
                    Close();
                }
                else
                {
                    MessageBox.Show("Xóa thông tin bệnh nhân thất bại", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
