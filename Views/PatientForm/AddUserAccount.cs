using Guna.UI2.WinForms;
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
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Views.LogIn;

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class AddUserAccount : Form
    {
        int employeeID;
        UserManagement userManagement;

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
        public AddUserAccount(int employeeID, UserManagement userManagement)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.employeeID = employeeID;
            this.userManagement = userManagement;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_name, tb_name, !AccountHelper.checkUsername(tb_name.Text.Trim()));
        }
        private void warningValidate(PictureBox picbox, Guna2TextBox tb, bool check)
        {
            picbox.Visible = (check || tb.Text.Length == 0) ? false : true;
            tb.BorderColor = (check || tb.Text.Length == 0) ? Color.Black : Color.Red;
            tb.BorderThickness = (check || tb.Text.Length == 0) ? 1 : 3;
        }

        private void tb_confirm_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_confirm, tb_confirm, tb_confirm.Text == tb_password.Text);
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (tb_name.Text.Trim().Length == 0)
            {
                MessageBox.Show("Nhập tên đăng nhập", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pb_name.Visible == true)
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbb_decentralization.SelectedIndex == 0)
            {
                MessageBox.Show("Chọn phân quyền", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tb_password.Text.Length == 0)
            {
                MessageBox.Show("Nhập mật khẩu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pb_confirm.Visible == true || tb_password.Text != tb_confirm.Text)
            {
                MessageBox.Show("Mật khẩu không trùng khớp", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string username = tb_name.Text.Trim();
            string password = tb_password.Text;
            int decentralization = cbb_decentralization.SelectedIndex - 1;
            if (AccountHelper.addUserAccount(username, password, decentralization, employeeID))
            {
                MessageBox.Show("Thêm tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userManagement.loadUser(EmployeeHelper.getAllEmployeeWithAccount());
                Close();
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thât bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
