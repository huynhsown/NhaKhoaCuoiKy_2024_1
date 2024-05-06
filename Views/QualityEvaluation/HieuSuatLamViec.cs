using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace NhaKhoaCuoiKy.Views.QualityEvaluation
{
    public partial class HieuSuatLamViec : Form
    {
        public HieuSuatLamViec()
        {
            InitializeComponent();
        }
        private MainForm mainForm;
        public HieuSuatLamViec(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        Database mydb = new Database();




        private void HieuSuatLamViec_Load_1(object sender, EventArgs e)
        {
            string query = "SELECT NHANVIEN.MaNhanVien FROM NHANVIEN INNER JOIN BACSI ON NHANVIEN.MaNhanVien = BACSI.MaNhanVien";


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

        }

        private void cbb_manv_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedMaNV = cbb_manv.SelectedItem.ToString();

            string queryNV = "SELECT HoVaTen, Anh FROM NHANVIEN WHERE MaNhanVien = @MaNhanVien";
            string queryRating = "SELECT AVG(DiemDanhGia) AS AverageRating FROM DANHGIA WHERE MaNhanVien = @MaNhanVien";

            try
            {
                mydb.openConnection();

                // Truy vấn thông tin nhân viên
                SqlCommand commandNV = new SqlCommand(queryNV, mydb.getConnection);
                commandNV.Parameters.AddWithValue("@MaNhanVien", selectedMaNV);
                SqlDataReader readerNV = commandNV.ExecuteReader();

                if (readerNV.Read())
                {
                    string hoVaTen = readerNV["HoVaTen"].ToString();
                    string picPath = readerNV["Anh"].ToString();

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

                readerNV.Close();

                // Truy vấn trung bình cộng điểm đánh giá
                SqlCommand commandRating = new SqlCommand(queryRating, mydb.getConnection);
                commandRating.Parameters.AddWithValue("@MaNhanVien", selectedMaNV);
                SqlDataReader readerRating = commandRating.ExecuteReader();

                if (readerRating.Read())
                {
                    if (readerRating["AverageRating"] != DBNull.Value)
                    {
                        double averageRating = Convert.ToDouble(readerRating["AverageRating"]);
                        // Load giá trị trung bình vào Rating Star control
                        guna2RatingStar1.Value = (int)Math.Round(averageRating);
                    }
                    else
                    {
                        // Không có điểm đánh giá nào
                        MessageBox.Show("Không có điểm đánh giá.");
                    }
                }

                readerRating.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mydb.closeConnection();
            }
            DrawBarChart();
        }

        private void cbb_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawBarChart();
        }
        Chart chart1 = new Chart();
        private void DrawBarChart()
        {
            // Xóa dữ liệu cũ trên biểu đồ
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // Mở kết nối đến cơ sở dữ liệu
            mydb.openConnection();

            // Lấy năm từ combobox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Lấy mã nhân viên từ combobox
            string selectedMaNV = cbb_manv.SelectedItem.ToString();

            // Truy vấn dữ liệu số lần bệnh nhân chữa bệnh với MaNhanVien từ cbb_manv và NgayThamKham bằng selectedYear
            string query = "SELECT COUNT(*) AS SoLanChuaBenh, NgayThamKham " +
                           "FROM HoaDon " +
                           "INNER JOIN ThongTinDichVu ON HoaDon.MaHoaDon = ThongTinDichVu.MaHoaDon " +
                           $"WHERE YEAR(NgayThamKham) = @Year AND MaNhanVien = @MaNV " +
                           "GROUP BY NgayThamKham";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, mydb.getConnection))
            {
                cmd.Parameters.AddWithValue("@Year", selectedYear);
                cmd.Parameters.AddWithValue("@MaNV", selectedMaNV);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ cột
            chart1.Parent = panel1;
            chart1.Dock = DockStyle.Fill;
            chart1.ChartAreas.Add(new ChartArea());

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.DateTime;

            foreach (DataRow row in dataTable.Rows)
            {
                DateTime ngayThamKham = Convert.ToDateTime(row["NgayThamKham"]);
                int soLanChuaBenh = Convert.ToInt32(row["SoLanChuaBenh"]);
                series.Points.AddXY(ngayThamKham, soLanChuaBenh);
            }

            chart1.Series.Add(series);
            chart1.Titles.Add($"Biểu Đồ Số Lần Bệnh Nhân Chữa Bệnh với Mã Nhân Viên {selectedMaNV} Trong Năm {selectedYear}");
            chart1.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            // Đóng kết nối đến cơ sở dữ liệu
            mydb.closeConnection();
        }

    }
}
