using NhaKhoaCuoiKy.FaceRecognize;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.PatientForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.LogIn
{
    public partial class UserManagement : Form
    {
        MainForm mainForm;
        UserModel userAccount;
        public UserManagement(MainForm mainForm, UserModel userAccount)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.userAccount = userAccount;
        }

        private void UserManagement_Load(object sender, EventArgs e)
        {
            loadUser(EmployeeHelper.getAllEmployeeWithAccount());
        }

        public void loadUser(DataTable dt)
        {
            try
            {
                data_account.Rows.Clear();

                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["MaNhanVien"]);
                    string name = dr["HoVaTen"].ToString();
                    string position = dr["ViTriLamViec"].ToString();
                    string status = (dr["TenDangNhap"] != DBNull.Value) ? "Đã có tài khoản" : "Chưa có tài khoản";
                    data_account.Rows.Add(id, name, position, status);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void data_account_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_account.Columns[e.ColumnIndex].Name == "col_btn_delete")
            {
                if (data_account.Rows[e.RowIndex].Cells[3].Value.ToString().Trim() == "Chưa có tài khoản")
                {
                    MessageBox.Show("Nhân viên chưa có tài khoản", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int employeeID = Convert.ToInt32(data_account.Rows[e.RowIndex].Cells[0].Value);
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (AccountHelper.deleteUserAccount(employeeID))
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (employeeID == userAccount.employeeID)
                        {
                            mainForm.Close();
                        }
                        loadUser(EmployeeHelper.getAllEmployeeWithAccount());
                    }
                    else
                    {
                        MessageBox.Show("Xóa tài khoản thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            if (data_account.Columns[e.ColumnIndex].Name == "col_btn_add")
            {
                if (data_account.Rows[e.RowIndex].Cells[3].Value.ToString().Trim() == "Đã có tài khoản")
                {
                    MessageBox.Show("Nhân viên đã có tài khoản", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int employeeID = Convert.ToInt32(data_account.Rows[e.RowIndex].Cells[0].Value);
                AddUserAccount addUserAccount = new AddUserAccount(employeeID, this);
                ViewHelper.loadForm(addUserAccount, mainForm);
            }
            if(data_account.Columns[e.ColumnIndex].Name == "col_btn_addFace")
            {
                if (data_account.Rows[e.RowIndex].Cells[3].Value.ToString().Trim() == "Chưa có tài khoản")
                {
                    MessageBox.Show("Nhân viên chưa có tài khoản", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                int employeeID = Convert.ToInt32(data_account.Rows[e.RowIndex].Cells[0].Value);
                LoginRecognize loginRecognize = new LoginRecognize(employeeID, this);
                ViewHelper.loadForm(loginRecognize, mainForm);
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            loadUser(EmployeeHelper.getAllEmployeeWithAccount());
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (cb_filter.SelectedIndex == 0)
            {
                int employeeID;
                if (!Int32.TryParse(tb_filter_search.Text, out employeeID))
                {
                    MessageBox.Show("Chỉ nhập số", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                DataTable dt = EmployeeHelper.getAllEmployeeWithAccountByID(employeeID);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có nhân viên", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                loadUser(dt);
            }
            if (cb_filter.SelectedIndex == 1)
            {
                DataTable dt = EmployeeHelper.getAllEmployeeWithAccountByName(tb_filter_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có nhân viên", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                loadUser(dt);
            }
            if (cb_filter.SelectedIndex == 2)
            {
                if (!long.TryParse(tb_filter_search.Text, out _))
                {
                    MessageBox.Show("Chỉ nhập số", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                DataTable dt = EmployeeHelper.getAllEmployeeWithAccountByPhone(tb_filter_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có nhân viên", "Thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                loadUser(dt);
            }
        }
    }
}
