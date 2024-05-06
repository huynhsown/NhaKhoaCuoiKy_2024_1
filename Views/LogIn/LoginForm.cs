using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.LogIn
{
    public partial class LoginForm : Form
    {
        bool isHide = true;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void pb_hideAndSee_Click(object sender, EventArgs e)
        {
            if (isHide)
            {
                pb_hideAndSee.Image = pb_see.Image;
                tb_password.PasswordChar = '\0';
                isHide = false;
            }
            else
            {
                pb_hideAndSee.Image = pb_hide.Image;
                tb_password.PasswordChar = '*';
                isHide = true;
            }
        }

        public void LoginForm_Load(object sender, EventArgs e)
        {
            tb_username.Text = Properties.Settings.Default.username;
            tb_password.Text = Properties.Settings.Default.password;
            if (tb_username.Text.Length != 0) cb_remember.Checked = true;
        }

        public void removeInfoWhenLogout()
        {
            if(!cb_remember.Checked) {
                tb_username.Text = string.Empty;
                tb_password.Text = string.Empty;
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (cb_remember.Checked)
            {
                Properties.Settings.Default.username = tb_username.Text;
                Properties.Settings.Default.password = tb_password.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.username = string.Empty;
                Properties.Settings.Default.password = string.Empty;
                Properties.Settings.Default.Save();
            }

            try
            {
                DataTable dt = UserHelper.getUser(tb_username.Text.Trim(), tb_password.Text);
                if(dt.Rows.Count != 1)
                {
                    MessageBox.Show("Tài khoản mật khẩu không chính xác", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string username = dt.Rows[0]["TenDangNhap"].ToString();
                string password = dt.Rows[0]["MatKhau"].ToString();
                int decentralization = Convert.ToInt32(dt.Rows[0]["Quyen"]);
                int employeeID = Convert.ToInt32(dt.Rows[0]["MaNhanVien"]);
                UserModel userAccount = new UserModel(username, password, decentralization, employeeID);
                MainForm mainForm = new MainForm(userAccount, this);
                this.Owner = mainForm;
                mainForm.Show();
                this.Hide();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lb_forgot_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để được hỗ trợ", "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ quản trị viên để được hỗ trợ", "Đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
