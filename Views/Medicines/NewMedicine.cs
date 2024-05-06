using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Views.Employee.Medicines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.Medicines
{
    public partial class NewMedicine : Form
    {
        public EventHandler eventAddMedicine;
        private Validate validate = new Validate();
        private Medicine medicine;

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
        public NewMedicine(Medicine medicine)
        {
            InitializeComponent();
            this.medicine = medicine;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            try
            {
                string maThuoc = tb_medicineid.Text;
                string tenThuoc = tb_name.Text;
                string hDSD = tb_howtouse.Text;
                string thanhPhan = tb_ingredient.Text;
                int giaNhap = int.Parse(tb_importprice.Text);
                int giaBan = int.Parse(tb_price.Text);
                int soLuong = int.Parse(tb_quantity.Text);
                string congTy = tb_company.Text;

                string id = MedicineHelper.addNewMedicine(maThuoc, tenThuoc, hDSD, thanhPhan, giaNhap, giaBan, soLuong, congTy);
                if (id != null)
                {
                    MessageBox.Show("Thêm thuốc thành công", "Thêm thuốc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    medicine.loadAllMedicine();
                    Close();
                }
                else
                {
                    MessageBox.Show("Thêm thuốc thất bại", "Thêm thuốc", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void tb_importprice_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_importprice, tb_importprice, validate.validateNumber(tb_importprice.Text));
        }

        private void tb_price_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_price, tb_price, validate.validateNumber(tb_price.Text));
        }

        private void tb_quantity_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_quantity, tb_quantity, validate.validateNumber(tb_quantity.Text));
        }
    }
}
