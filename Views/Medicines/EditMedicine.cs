using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Views.Employee;
using NhaKhoaCuoiKy.Views.Employee.Medicines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Guna.UI2.WinForms.Helpers.GraphicsHelper;
using System.Xml.Linq;

namespace NhaKhoaCuoiKy.Views.Medicines
{
    public partial class EditMedicine : Form
    {
        Medicine medicine;
        string medID;
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
        public EditMedicine(Medicine medicine, string medID)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.medicine = medicine;
            this.medID = medID;
        }
        public EditMedicine()
        {
            InitializeComponent();
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

                if (MedicineHelper.updateMedicine(maThuoc, tenThuoc, hDSD, thanhPhan, giaNhap, giaBan, soLuong, congTy))
                {
                    MessageBox.Show("Sửa thuốc thành công", "Sửa thuốc", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    medicine.loadAllMedicine();
                    Close();
                }
                else
                {
                    MessageBox.Show("Sửa thuốc thất bại", "Sửa thuốc", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditMedicine_Load(object sender, EventArgs e)
        {
            try { 
                DataTable dataTable = MedicineHelper.getMedicineByID(medID);
                if (dataTable.Rows.Count == 0)
                {
                    Close();
                    return;
                }
            
                string tenThuoc = dataTable.Rows[0]["TenThuoc"].ToString();
                string hDSD = dataTable.Rows[0]["HuongDanSD"].ToString();
                string thanhPhan = dataTable.Rows[0]["ThanhPhan"].ToString();
                int giaNhap = Convert.ToInt32(dataTable.Rows[0]["GiaNhap"].ToString());
                int giaBan = Convert.ToInt32(dataTable.Rows[0]["GiaBan"].ToString());
                int soLuong = Convert.ToInt32(dataTable.Rows[0]["SoLuong"].ToString());
                string congTy = dataTable.Rows[0]["CongTy"].ToString();
                tb_medicineid.Text = medID;
                tb_medicineid.Enabled = false;
                tb_name.Text = tenThuoc;
                tb_howtouse.Text = hDSD;
                tb_ingredient.Text = thanhPhan;
                tb_importprice.Text = giaNhap.ToString();
                tb_price.Text = giaBan.ToString();
                tb_quantity.Text = soLuong.ToString();
                tb_company.Text = congTy;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
