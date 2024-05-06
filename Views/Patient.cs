using Dapper;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.Service;
using NhaKhoaCuoiKy.Views.PatientForm;
using System.Data;

namespace NhaKhoaCuoiKy.Views
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        public EventHandler popupNewPatient;
        public EventHandler addNewRecord;
        private NewPatient newPatient;
        private Validate validate = new Validate();
        private MainForm mainForm;
        PatientModel pm;
        UserModel userAccount;

        public Patient(MainForm mainForm, UserModel userAccount)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.userAccount = userAccount;
        }

        private void loadForm(Form form)
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

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            loadForm(new NewPatient(this));

            /*            FormBackGround formBackGround = new FormBackGround(mainForm);
                        try
                        {
                            using (NewPatient newCategory = new NewPatient(this))
                            {
                                formBackGround.Owner = mainForm;
                                formBackGround.Show();
                                newCategory.Owner = formBackGround;
                                newCategory.ShowDialog();
                                formBackGround.Dispose();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }*/
        }

        private void Patient_Load(object sender, EventArgs e)
        {
            pm = new PatientModel();
            data_benhNhan.AllowUserToAddRows = false;
            addToDataGrid(pm.getAllPatient());
            /*            data_benhNhan.Columns[0].Width = 90;
                        DataGridViewButtonColumn btn_record = new DataGridViewButtonColumn();
                        btn_record.HeaderText = "Thêm bệnh án";
                        btn_record.Name = "btn_addRecord";
                        btn_record.Text = "Thêm";
                        btn_record.UseColumnTextForButtonValue = true;
                        data_benhNhan.Columns.Add(btn_record);

                        DataGridViewButtonColumn btn_info = new DataGridViewButtonColumn();
                        btn_info.HeaderText = "Xem thông tin";
                        btn_info.Name = "btn_editInfoPatient";
                        btn_info.Text = "Xem";
                        btn_info.UseColumnTextForButtonValue = true;
                        data_benhNhan.Columns.Add(btn_info);*/
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_filter_search.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui long nhap thong tin", "Thong tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            data_benhNhan.Rows.Clear();
            data_benhNhan.Refresh();
            if (cb_filter.SelectedIndex == 0)
            {
                searchByID();
            }
            else if (cb_filter.SelectedIndex == 1)
            {
                searchByName();
            }
            else if (cb_filter.SelectedIndex == 2)
            {
                searchByPhone();
            }
        }

        public void addToDataGrid(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int maBN = Convert.ToInt32(dt.Rows[i]["MaBenhNhan"]);
                    string hoTen = Convert.ToString(dt.Rows[i]["HoVaTen"]);
                    string gioiTinh = dt.Rows[i]["GioiTinh"].ToString();
                    string ngaySinh = Convert.ToDateTime(dt.Rows[i]["NgaySinh"]).ToShortDateString();
                    int soNha = int.Parse(dt.Rows[i]["SoNha"].ToString());
                    string soDienThoai = dt.Rows[i]["SoDienThoai"].ToString();
                    string duong = dt.Rows[i]["TenDuong"].ToString();
                    string phuong = dt.Rows[i]["Phuong"].ToString();
                    string thanhPho = dt.Rows[i]["ThanhPho"].ToString();

                    string diaChi = soNha.ToString() + " " + duong + " " + phuong + " " + thanhPho;
                    data_benhNhan.Rows.Add(maBN, hoTen, soDienThoai, ngaySinh, gioiTinh, diaChi);
                }
            }
        }
        private void searchByID()
        {
            try
            {
                if (!validate.validateNumber(tb_filter_search.Text.Trim()))
                {
                    MessageBox.Show("Ma benh nhan khong hop le (Chua ky tu dac biet hoac chu cai)", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = pm.getByID(int.Parse(tb_filter_search.Text.Trim()));
                addToDataGrid(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void searchByName()
        {
            try
            {
                DataTable dt = pm.getByName(tb_filter_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                addToDataGrid(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchByPhone()
        {
            try
            {
                if (!validate.validateNumber(tb_filter_search.Text.Trim()))
                {
                    MessageBox.Show("So dien thoai khong hop le (Chua ky tu dac biet hoac chu cai)", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = pm.getByPhone(tb_filter_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                addToDataGrid(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void data_benhNhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int patienID = Convert.ToInt32(data_benhNhan.Rows[e.RowIndex].Cells[0].Value);
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_addRecord")
            {                
                mainForm.openChildFormHaveData(new AddNewRecord(patienID, userAccount));
            }
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_Info")
            {
                loadForm(new EditPatient(patienID, this));
            }
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_invoice")
            {
                mainForm.openChildFormHaveData(new AddInvoice(patienID, mainForm));
            }
            if(data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_history")
            {

            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
