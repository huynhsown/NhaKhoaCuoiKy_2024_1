using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;
using NhaKhoaCuoiKy.Helpers;

namespace NhaKhoaCuoiKy.Views.QualityEvaluation
{
    public partial class Evaluation : Form
    {
        Database mydb = new Database();
        private MainForm mainForm;
        public Evaluation(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        public Evaluation()
        {
            InitializeComponent();
        }

        private void Evaluation_Load(object sender, EventArgs e)
        {
            string query = "SELECT MaNhanVien FROM NHANVIEN";

            try
            {
                // Mở kết nối đến cơ sở dữ liệu
                mydb.openConnection();

                SqlCommand command = new SqlCommand(query, mydb.getConnection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string maNhanVien = reader["MaNhanVien"].ToString();
                    cbb_manv.Items.Add(maNhanVien);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi sử dụng xong
                mydb.closeConnection();
            }
            guna2DateTimePicker1.Value = DateTime.Now;
        }

        private void cbb_manv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMaNV = cbb_manv.SelectedItem.ToString();

            string query = "SELECT HoVaTen, Anh FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien";

            try
            {
                mydb.openConnection();

                SqlCommand command = new SqlCommand(query, mydb.getConnection);
                command.Parameters.AddWithValue("@MaNhanVien", selectedMaNV);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string hoVaTen = reader["HoVaTen"].ToString();
                    string picPath = reader["Anh"].ToString();

                    // Hiển thị HoVaTen vào textbox_hvt
                    tb_tennv.Text = hoVaTen;

                    // Hiển thị ảnh từ đường dẫn picPath vào picturebox
                    if (File.Exists(picPath))
                    {
                        Image image = Image.FromFile(picPath);
                        pb_anh.Image = image;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy ảnh.");
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!verify())
            {
                MessageBox.Show("Dữ liệu thiếu hoặc sai", "Gửi Đánh Giá", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                int manv = Convert.ToInt32(cbb_manv.Text);
                float diemdanhgia = 0;

                // Calculate the total rating from all RatingStar controls
                diemdanhgia += guna2RatingStar1.Value;
                diemdanhgia += guna2RatingStar2.Value;
                diemdanhgia += guna2RatingStar3.Value;
                diemdanhgia += guna2RatingStar4.Value;
                diemdanhgia += guna2RatingStar5.Value;

                float diemcuoi = diemdanhgia / 5;
                DateTime dateTime = guna2DateTimePicker1.Value;
                string phanhoi = richTextBox1.Text;
                if (Evaluation_Helper.insertEvaluation(manv, diemcuoi, phanhoi, dateTime))
                {
                    MessageBox.Show("New Evaluation Added", "Add Evaluation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    guna2RatingStar1.Value = 0;
                    guna2RatingStar2.Value = 0;
                    guna2RatingStar3.Value = 0;
                    guna2RatingStar4.Value = 0;
                    guna2RatingStar5.Value = 0;
                    tb_tennv.Text = "";
                    richTextBox1.Text = "";
                    cbb_manv.SelectedIndex = -1; // Reset selected index
                    guna2DateTimePicker1.Value = DateTime.Now;
                }
                else
                {
                    MessageBox.Show("Error", "Add Evaluation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private bool verify()
        {
            // Check if any of the RatingStar controls have a value of 0
            if (guna2RatingStar1.Value == 0 || guna2RatingStar2.Value == 0 || guna2RatingStar3.Value == 0 || guna2RatingStar4.Value == 0 || guna2RatingStar5.Value == 0)
                return false;

            return true;
        }

    }
}
