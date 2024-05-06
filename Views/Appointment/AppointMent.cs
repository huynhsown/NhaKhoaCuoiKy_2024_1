using NhaKhoaCuoiKy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Appointment
{
    public partial class AppointMent : Form
    {
        public AppointMent()
        {
            InitializeComponent();
        }

        private MainForm mainForm;

        public AppointMent(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private Validate validate = new Validate();

        private void SetAppointMent_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = AppointmentHelper.getAllDoctors();
                loadDataDoctor(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void loadDataDoctor(DataTable dt)
        {
            data_bacsi.Rows.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                int doctor_id = Convert.ToInt32(dr["MaNhanVien"]);
                string doctor_name = Convert.ToString(dr["HoVaTen"]);
                string gender = Convert.ToString(dr["GioiTinh"]);
                string degree = Convert.ToString(dr["HocVi"]);
                string expertise = Convert.ToString(dr["ChuyenMon"]);
                data_bacsi.Rows.Add(doctor_id, doctor_name, gender, degree, expertise);
            }
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (cbb_search.SelectedIndex == 0)
            {
                if (!validate.validateNumber(tb_search.Text))
                {
                    MessageBox.Show("Vui long chi nhap so!", "Tim kiem", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    return;
                }
                DataTable dt = AppointmentHelper.getAllDoctorsByID(Convert.ToInt32(tb_search.Text));
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Khong ton tai bac si", "Tim kiem", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    return;
                }
                loadDataDoctor(dt);
            }
            else if (cbb_search.SelectedIndex == 1)
            {
                if (!validate.validateName(tb_search.Text))
                {
                    MessageBox.Show("Ten khong chua ky tu dac biet hoac so", "Tim kiem", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
                    return;
                }
                DataTable dt = AppointmentHelper.getAllDoctorsByName(tb_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Khong ton tai bac si", "Tim kiem", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    return;
                }
                loadDataDoctor(dt);
            }
        }

        private void data_bacsi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            int doctor_id = Convert.ToInt32(data_bacsi.Rows[row].Cells[0].Value);
            if (data_bacsi.Columns[col].Name == "col_btn_setappointment")
            {
                mainForm.openChildFormHaveData(new SetAppointment(doctor_id, mainForm));
            }
        }
    }
}
