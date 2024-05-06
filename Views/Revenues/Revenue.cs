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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace NhaKhoaCuoiKy.Views.Revenues
{
    public partial class Revenue : Form
    {
        public Revenue()
        {
            InitializeComponent();
            data_revenue.Parent = panel1;
            data_revenue.Dock = DockStyle.Fill;

        }
        private MainForm mainForm;
        public Revenue(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }
        Database db = new Database();
        Chart chart1 = new Chart();
        Chart chart2 = new Chart();

        private void Revenue_Load(object sender, EventArgs e)
        {
            cbb_month.SelectedIndex = DateTime.Now.Month - 1; // Giả sử tháng được đánh số từ 1 đến 12, trong khi chỉ số của mảng từ 0 đến 11
            cbb_year.SelectedItem = DateTime.Now.Year.ToString();
            rdb_thang.Checked = true;

            // Bước 1: Truy vấn cơ sở dữ liệu để lấy các hóa đơn trong khoảng thời gian cbb_month và cbb_year
            string query = "SELECT HoaDon.MaHoaDon, HoaDon.MaBenhNhan, HoaDon.NgayThamKham " +
                           "FROM HoaDon " +
                           "WHERE MONTH(HoaDon.NgayThamKham) = @month AND YEAR(HoaDon.NgayThamKham) = @year";
            SqlCommand cmd = new SqlCommand(query, db.getConnection);
            cmd.Parameters.AddWithValue("@month", DateTime.Now.Month); // Giả sử cbb_month là ComboBox chứa các tháng
            cmd.Parameters.AddWithValue("@year", DateTime.Now.Year); // Giả sử cbb_year là ComboBox chứa các năm
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtHoaDon = new DataTable();
            adapter.Fill(dtHoaDon);



            // Bước 2: Dựa vào các hóa đơn đã lấy, truy vấn cơ sở dữ liệu để lấy thông tin dịch vụ và thông tin chi tiết dịch vụ tương ứng
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("MaHoaDon", typeof(int));
            dtResult.Columns.Add("DichVu", typeof(string));
            dtResult.Columns.Add("NgayTaoHoaDon", typeof(DateTime));
            dtResult.Columns.Add("TriGia", typeof(decimal));


            foreach (DataRow row in dtHoaDon.Rows)
            {
                int maHoaDon = Convert.ToInt32(row["MaHoaDon"]);
                DateTime ngayTaoHoaDon = Convert.ToDateTime(row["NgayThamKham"]);

                // Truy vấn cơ sở dữ liệu để lấy tất cả các mã thông tin dịch vụ (MaThongTin) cho mỗi hóa đơn
                query = "SELECT MaThongTin FROM ThongTinDichVu WHERE MaHoaDon = @maHoaDon";
                cmd = new SqlCommand(query, db.getConnection);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                SqlDataAdapter innerAdapter = new SqlDataAdapter(cmd);
                DataTable dtMaThongTin = new DataTable();
                innerAdapter.Fill(dtMaThongTin);

                // Lặp qua từng mã thông tin dịch vụ để lấy thông tin chi tiết của từng dịch vụ
                foreach (DataRow maThongTinRow in dtMaThongTin.Rows)
                {
                    int maThongTin = Convert.ToInt32(maThongTinRow["MaThongTin"]);

                    // Truy vấn cơ sở dữ liệu để lấy thông tin chi tiết của dịch vụ từ bảng ThongTinDichVu và DichVu
                    query = "SELECT DichVu.TenDichVu, ThongTinDichVu.SoLuong, DichVu.GiaDichVu, DichVu.GiamGia " +
                            "FROM ThongTinDichVu " +
                            "INNER JOIN DichVu ON ThongTinDichVu.MaDichVu = DichVu.MaDichVu " +
                            "WHERE ThongTinDichVu.MaThongTin = @maThongTin";
                    cmd = new SqlCommand(query, db.getConnection);
                    cmd.Parameters.AddWithValue("@MaThongTin", maThongTin);
                    SqlDataAdapter detailAdapter = new SqlDataAdapter(cmd);
                    DataTable dtDetails = new DataTable();
                    detailAdapter.Fill(dtDetails);

                    // Tính toán và thêm vào dtResult
                    foreach (DataRow detailRow in dtDetails.Rows)
                    {
                        string tenDichVu = detailRow["TenDichVu"].ToString();
                        int soLuong = Convert.ToInt32(detailRow["SoLuong"]);
                        decimal giaDichVu = Convert.ToDecimal(detailRow["GiaDichVu"]);
                        decimal giamGia = Convert.ToDecimal(detailRow["GiamGia"]);

                        decimal triGia = soLuong * giaDichVu * (decimal)(1 - giamGia / 100);
                        dtResult.Rows.Add(maHoaDon, tenDichVu, ngayTaoHoaDon, triGia);

                    }



                    foreach (DataRow row2 in dtHoaDon.Rows)
                    {
                        int maHoaDon2 = Convert.ToInt32(row2["MaHoaDon"]);
                        DateTime ngayTaoHoaDon2 = Convert.ToDateTime(row2["NgayThamKham"]);

                        // Truy vấn cơ sở dữ liệu để lấy thông tin từ các bảng liên quan
                        string queryDetails = @"
                            SELECT DonThuoc.SoLuong, ThongTinSuDungThuoc.SoLuong AS SoLuongSuDung, Thuoc.GiaBan
                            FROM HOADON
                            INNER JOIN DonThuoc ON HOADON.MaHoaDon = DonThuoc.MaHoaDon
                            INNER JOIN ThongTinSuDungThuoc ON DonThuoc.MaDonThuoc = ThongTinSuDungThuoc.MaDonThuoc
                            INNER JOIN Thuoc ON ThongTinSuDungThuoc.MaThuoc = Thuoc.MaThuoc
                            WHERE HOADON.MaHoaDon = @maHoaDon";

                        SqlCommand cmdDetails = new SqlCommand(queryDetails, db.getConnection);
                        cmdDetails.Parameters.AddWithValue("@maHoaDon", maHoaDon2);
                        SqlDataAdapter detailAdapter2 = new SqlDataAdapter(cmdDetails);
                        DataTable dtDetails2 = new DataTable();
                        detailAdapter2.Fill(dtDetails2);

                        // Tính toán và thêm vào dtResult
                        foreach (DataRow detailRow in dtDetails2.Rows)
                        {
                            int soLuongDonThuoc = Convert.ToInt32(detailRow["SoLuong"]);
                            int soLuongSuDung = Convert.ToInt32(detailRow["SoLuongSuDung"]);
                            decimal giaBan = Convert.ToDecimal(detailRow["GiaBan"]);

                            decimal triGia = soLuongDonThuoc * soLuongSuDung * giaBan;

                            // Check if a row with the same MaHoaDon, TenDichVu, and NgayTaoHoaDon already exists in dtResult
                            DataRow[] existingRows = dtResult.Select($"MaHoaDon = {maHoaDon2} AND DichVu = 'Thuoc' AND NgayTaoHoaDon = '{ngayTaoHoaDon2}'");
                            if (existingRows.Length == 0)
                            {
                                dtResult.Rows.Add(maHoaDon2, "Thuoc", ngayTaoHoaDon2, triGia);
                            }
                            else
                            {
                                // Update the existing row's TriGia
                                existingRows[0]["TriGia"] = triGia;
                            }
                        }
                    }


                    // Sau khi đã thêm dữ liệu vào dtResult, gán nó vào DataSource của DataGridView

                    data_revenue.DataSource = dtResult;

                }
            }
            //DrawChartFromDataGridView();
            DrawPieChart();
            DrawChartMedicine();
            // Thêm TableLayoutPanel vào panel1
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
            panel1.Controls.Add(tableLayoutPanel);

            // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
            tableLayoutPanel.Controls.Add(chart2, 0, 0);
            chart2.Dock = DockStyle.Fill;

            // Thêm chart4 vào ô thứ hai của TableLayoutPanel
            tableLayoutPanel.Controls.Add(chart4, 1, 0);
            chart4.Dock = DockStyle.Fill;
            // Thiết lập kích thước cố định cho cả hai biểu đồ
            chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
            chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
            //InitializePieChart();
            //DrawPieChart2();
            DrawChart();
            DrawChart2();


        }


        private void RefreshDataGridView()
        {
            // Thực hiện lại quá trình lấy dữ liệu từ cơ sở dữ liệu và cập nhật DataGridView
            // Bước 1: Lấy giá trị của cbb_month và cbb_year
            int selectedMonth = Convert.ToInt32(cbb_month.SelectedItem);
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Bước 2: Thực hiện truy vấn cơ sở dữ liệu để lấy dữ liệu mới
            DataTable dtResult = GetDataFromDatabase(selectedMonth, selectedYear);

            // Bước 3: Hiển thị dữ liệu mới trong DataGridView
            data_revenue.DataSource = dtResult;

            //DrawChartFromDataGridView();
            DrawChart0();



        }
        private DataTable GetDataFromDatabase(int month, int year)
        {
            Database db = new Database();
            // Bước 1: Truy vấn cơ sở dữ liệu để lấy các hóa đơn trong khoảng thời gian cbb_month và cbb_year
            string query = "SELECT HoaDon.MaHoaDon, HoaDon.MaBenhNhan, HoaDon.NgayThamKham " +
                           "FROM HoaDon " +
                           "WHERE MONTH(HoaDon.NgayThamKham) = @month AND YEAR(HoaDon.NgayThamKham) = @year";
            SqlCommand cmd = new SqlCommand(query, db.getConnection);
            cmd.Parameters.AddWithValue("@month", month); // Giả sử cbb_month là ComboBox chứa các tháng
            cmd.Parameters.AddWithValue("@year", year); // Giả sử cbb_year là ComboBox chứa các năm
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dtHoaDon = new DataTable();
            adapter.Fill(dtHoaDon);



            // Bước 2: Dựa vào các hóa đơn đã lấy, truy vấn cơ sở dữ liệu để lấy thông tin dịch vụ và thông tin chi tiết dịch vụ tương ứng
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("MaHoaDon", typeof(int));
            dtResult.Columns.Add("DichVu", typeof(string));
            dtResult.Columns.Add("NgayTaoHoaDon", typeof(DateTime));
            dtResult.Columns.Add("TriGia", typeof(decimal));


            foreach (DataRow row in dtHoaDon.Rows)
            {
                int maHoaDon = Convert.ToInt32(row["MaHoaDon"]);
                DateTime ngayTaoHoaDon = Convert.ToDateTime(row["NgayThamKham"]);

                // Truy vấn cơ sở dữ liệu để lấy tất cả các mã thông tin dịch vụ (MaThongTin) cho mỗi hóa đơn
                query = "SELECT MaThongTin FROM ThongTinDichVu WHERE MaHoaDon = @maHoaDon";
                cmd = new SqlCommand(query, db.getConnection);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                SqlDataAdapter innerAdapter = new SqlDataAdapter(cmd);
                DataTable dtMaThongTin = new DataTable();
                innerAdapter.Fill(dtMaThongTin);

                // Lặp qua từng mã thông tin dịch vụ để lấy thông tin chi tiết của từng dịch vụ
                foreach (DataRow maThongTinRow in dtMaThongTin.Rows)
                {
                    int maThongTin = Convert.ToInt32(maThongTinRow["MaThongTin"]);

                    // Truy vấn cơ sở dữ liệu để lấy thông tin chi tiết của dịch vụ từ bảng ThongTinDichVu và DichVu
                    query = "SELECT DichVu.TenDichVu, ThongTinDichVu.SoLuong, DichVu.GiaDichVu, DichVu.GiamGia " +
                            "FROM ThongTinDichVu " +
                            "INNER JOIN DichVu ON ThongTinDichVu.MaDichVu = DichVu.MaDichVu " +
                            "WHERE ThongTinDichVu.MaThongTin = @maThongTin";
                    cmd = new SqlCommand(query, db.getConnection);
                    cmd.Parameters.AddWithValue("@MaThongTin", maThongTin);
                    SqlDataAdapter detailAdapter = new SqlDataAdapter(cmd);
                    DataTable dtDetails = new DataTable();
                    detailAdapter.Fill(dtDetails);

                    // Tính toán và thêm vào dtResult
                    foreach (DataRow detailRow in dtDetails.Rows)
                    {
                        string tenDichVu = detailRow["TenDichVu"].ToString();
                        int soLuong = Convert.ToInt32(detailRow["SoLuong"]);
                        decimal giaDichVu = Convert.ToDecimal(detailRow["GiaDichVu"]);
                        decimal giamGia = Convert.ToDecimal(detailRow["GiamGia"]);

                        decimal triGia = soLuong * giaDichVu * (decimal)(1 - giamGia / 100);
                        dtResult.Rows.Add(maHoaDon, tenDichVu, ngayTaoHoaDon, triGia);

                    }
                    // Truy vấn cơ sở dữ liệu để lấy thông tin từ các bảng liên quan
                    string queryDetails = @"SELECT DonThuoc.SoLuong, ThongTinSuDungThuoc.SoLuong AS SoLuongSuDung, Thuoc.GiaBan
                    FROM HOADON
                    INNER JOIN DonThuoc ON HOADON.MaHoaDon = DonThuoc.MaHoaDon
                    INNER JOIN ThongTinSuDungThuoc ON DonThuoc.MaDonThuoc = ThongTinSuDungThuoc.MaDonThuoc
                    INNER JOIN Thuoc ON ThongTinSuDungThuoc.MaThuoc = Thuoc.MaThuoc
                    WHERE HOADON.MaHoaDon = @maHoaDon";

                    SqlCommand cmdDetails = new SqlCommand(queryDetails, db.getConnection);
                    cmdDetails.Parameters.AddWithValue("@maHoaDon", maHoaDon);
                    SqlDataAdapter detailAdapter2 = new SqlDataAdapter(cmdDetails);
                    DataTable dtDetails2 = new DataTable();
                    detailAdapter2.Fill(dtDetails2);

                    // Tính toán và thêm vào dtResult
                    foreach (DataRow detailRow in dtDetails2.Rows)
                    {
                        int soLuongDonThuoc = Convert.ToInt32(detailRow["SoLuong"]);
                        int soLuongSuDung = Convert.ToInt32(detailRow["SoLuongSuDung"]);
                        decimal giaBan = Convert.ToDecimal(detailRow["GiaBan"]);

                        decimal triGia = soLuongDonThuoc * soLuongSuDung * giaBan;
                        dtResult.Rows.Add(maHoaDon, "Thuoc", ngayTaoHoaDon, triGia);
                    }
                    data_revenue.DataSource = dtResult;

                }
                foreach (DataRow row2 in dtHoaDon.Rows)
                {
                    int maHoaDon2 = Convert.ToInt32(row2["MaHoaDon"]);
                    DateTime ngayTaoHoaDon2 = Convert.ToDateTime(row2["NgayThamKham"]);

                    // Truy vấn cơ sở dữ liệu để lấy thông tin từ các bảng liên quan
                    string queryDetails = @"
                            SELECT DonThuoc.SoLuong, ThongTinSuDungThuoc.SoLuong AS SoLuongSuDung, Thuoc.GiaBan
                            FROM HOADON
                            INNER JOIN DonThuoc ON HOADON.MaHoaDon = DonThuoc.MaHoaDon
                            INNER JOIN ThongTinSuDungThuoc ON DonThuoc.MaDonThuoc = ThongTinSuDungThuoc.MaDonThuoc
                            INNER JOIN Thuoc ON ThongTinSuDungThuoc.MaThuoc = Thuoc.MaThuoc
                            WHERE HOADON.MaHoaDon = @maHoaDon";

                    SqlCommand cmdDetails = new SqlCommand(queryDetails, db.getConnection);
                    cmdDetails.Parameters.AddWithValue("@maHoaDon", maHoaDon2);
                    SqlDataAdapter detailAdapter2 = new SqlDataAdapter(cmdDetails);
                    DataTable dtDetails2 = new DataTable();
                    detailAdapter2.Fill(dtDetails2);

                    // Tính toán và thêm vào dtResult
                    foreach (DataRow detailRow in dtDetails2.Rows)
                    {
                        int soLuongDonThuoc = Convert.ToInt32(detailRow["SoLuong"]);
                        int soLuongSuDung = Convert.ToInt32(detailRow["SoLuongSuDung"]);
                        decimal giaBan = Convert.ToDecimal(detailRow["GiaBan"]);

                        decimal triGia = soLuongDonThuoc * soLuongSuDung * giaBan;

                        // Check if a row with the same MaHoaDon, TenDichVu, and NgayTaoHoaDon already exists in dtResult
                        DataRow[] existingRows = dtResult.Select($"MaHoaDon = {maHoaDon2} AND DichVu = 'Thuoc' AND NgayTaoHoaDon = '{ngayTaoHoaDon2}'");
                        if (existingRows.Length == 0)
                        {
                            dtResult.Rows.Add(maHoaDon2, "Thuoc", ngayTaoHoaDon2, triGia);
                        }
                        else
                        {
                            // Update the existing row's TriGia
                            existingRows[0]["TriGia"] = triGia;
                        }
                    }
                }


            }
            //decimal totalRevenue = 0;

            //foreach (DataGridViewRow row4 in data_revenue.Rows)
            //{
            //    decimal triGia = Convert.ToDecimal(row4.Cells["TriGia"].Value);
            //    totalRevenue += triGia;
            //}

            //// Hiển thị tổng doanh thu
            //MessageBox.Show("Tổng doanh thu: " + totalRevenue.ToString());
            // Fill dtResult from database query
            return dtResult;

        }
        private void DrawChart0()
        {
            chart1.Size = panel1.Size;

            // Xóa các series, chart areas, và titles cũ trước khi vẽ biểu đồ mới
            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.Titles.Clear();

            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Lấy năm từ combobox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Truy vấn dữ liệu dựa trên năm đã chọn, nhóm theo năm
            string query = "SELECT YEAR(NgayThamKham) AS Nam, SUM(SoLuong * GiaDichVu * (1 - GiamGia/100)) AS TriGia " +
                           "FROM HoaDon " +
                           "INNER JOIN ThongTinDichVu ON HoaDon.MaHoaDon = ThongTinDichVu.MaHoaDon " +
                           "INNER JOIN DichVu ON ThongTinDichVu.MaDichVu = DichVu.MaDichVu " +
                           "WHERE YEAR(NgayThamKham) IN (@PrevYear, @SelectedYear, @NextYear) " +
                           "GROUP BY YEAR(NgayThamKham)";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
            {
                cmd.Parameters.AddWithValue("@PrevYear", selectedYear - 1);
                cmd.Parameters.AddWithValue("@SelectedYear", selectedYear);
                cmd.Parameters.AddWithValue("@NextYear", selectedYear + 1);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ cột
            chart1.Parent = panel1;
            chart1.Dock = DockStyle.Fill;
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Minimum = selectedYear - 1; // Thiết lập giá trị tối thiểu của trục tung
            chartArea.AxisX.Maximum = selectedYear + 1; // Thiết lập giá trị tối đa của trục tung
            chartArea.AxisX.Interval = 1; // Đảm bảo mỗi năm đều được hiển thị
            chartArea.AxisX.Title = "Năm"; // Chú thích trục x
            chartArea.AxisY.Title = "Doanh thu (VNĐ)"; // Chú thích trục y

            chart1.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.Int32;
            series.YValueType = ChartValueType.Double;

            foreach (DataRow row in dataTable.Rows)
            {
                int year = Convert.ToInt32(row["Nam"]);
                decimal total = Convert.ToDecimal(row["TriGia"]);
                string label = string.Format("{1:N2}", year, total); // Tạo nhãn cho từng cột
                series.Points.AddXY(year, (double)total);
                series.Points[series.Points.Count - 1].Label = label; // Thêm nhãn cho cột
            }
            db.closeConnection();

            chart1.Series.Add(series);
            chart1.Titles.Add("Biểu Đồ Tổng Trị Giá Các Dịch Vụ Trong Năm " + selectedYear);
            chart1.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            // Thêm biểu đồ vào panel
            panel1.Controls.Add(chart1);

            // Đóng kết nối sau khi hoàn tất
        }







        private void data_revenue_DataSourceChanged(object sender, EventArgs e)
        {
            //DrawChartFromDataGridView();

        }

        private void cbb_month_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            RefreshDataGridView();
            DrawPieChart();
            //InitializePieChart();
            //DrawPieChart2();
            DrawChartMedicine();

            // Thêm TableLayoutPanel vào panel1
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
            panel1.Controls.Add(tableLayoutPanel);

            // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
            tableLayoutPanel.Controls.Add(chart2, 0, 0);
            chart2.Dock = DockStyle.Fill;

            // Thêm chart4 vào ô thứ hai của TableLayoutPanel
            tableLayoutPanel.Controls.Add(chart4, 1, 0);
            chart4.Dock = DockStyle.Fill;
            // Thiết lập kích thước cố định cho cả hai biểu đồ
            chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
            chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
            if (cbb_select.SelectedIndex == 1)
            {
                if (rdb_thang.Checked)
                {
                    // Ẩn DataGridView và hiển thị chart5 trên panel1
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = true;
                    chart6.Visible = false;
                    // Xóa các control khác trên panel1 trước khi thêm chart5
                    panel1.Controls.Clear();
                    DrawChart(); // Vẽ biểu đồ chart5
                }
                else if (rdb_quy.Checked)
                {
                    // Ẩn DataGridView và hiển thị chart6 trên panel1
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = true;

                    // Xóa các control khác trên panel1 trước khi thêm chart6
                    panel1.Controls.Clear();
                    DrawChart2(); // Vẽ biểu đồ chart6
                }
                else if (rdb_nam.Checked)
                {
                    // Hiển thị chart1 và ẩn các control khác
                    data_revenue.Visible = false;
                    chart1.Visible = true;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    panel1.Controls.Clear();
                    //DrawChartFromDataGridView(); // Vẽ biểu đồ chart6
                    DrawChart0();
                }
            }
            else if (cbb_select.SelectedIndex == 2)
            {
                if (rdb_thang.Checked)
                {
                    // Hiển thị chart2 và chart4 và ẩn các control khác
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = true;
                    chart4.Visible = true;
                    chart5.Visible = false;
                    panel1.Controls.Clear();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart2, 0, 0);
                    chart2.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart4, 1, 0);
                    chart4.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }
                else if (rdb_quy.Checked)
                {
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = false;
                    chart7.Visible = false;
                    chart8.Visible = true;
                    chart9.Visible = false;
                    chart10.Visible = true;
                    panel1.Controls.Clear();
                    DrawPieChart_Quarter();
                    DrawChartMedicine_Quarter();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart8, 0, 0);
                    chart8.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart10, 1, 0);
                    chart10.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart8.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart10.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }
                else if (rdb_nam.Checked)
                {
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = false;
                    chart7.Visible = true;
                    chart8.Visible = false;
                    chart9.Visible = true;
                    panel1.Controls.Clear();
                    DrawPieChart_Year();
                    DrawChartMedicine_Year();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart7, 0, 0);
                    chart7.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart9, 1, 0);
                    chart9.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart7.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart9.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);

                }
            }
            else
            {
                data_revenue.Visible = true;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
            }

        }

        private void cbb_year_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            RefreshDataGridView();
            DrawPieChart();
            //InitializePieChart();
            //DrawPieChart2();
            DrawChartMedicine();
            // Thêm TableLayoutPanel vào panel1
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.Dock = DockStyle.Fill;
            tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
            panel1.Controls.Add(tableLayoutPanel);

            // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

            // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
            tableLayoutPanel.Controls.Add(chart2, 0, 0);
            chart2.Dock = DockStyle.Fill;

            // Thêm chart4 vào ô thứ hai của TableLayoutPanel
            tableLayoutPanel.Controls.Add(chart4, 1, 0);
            chart4.Dock = DockStyle.Fill;
            // Thiết lập kích thước cố định cho cả hai biểu đồ
            chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
            chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
            if (cbb_select.SelectedIndex == 1 )
            {
                if (rdb_thang.Checked)
                {
                    // Ẩn DataGridView và hiển thị chart5 trên panel1
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = true;
                    chart6.Visible = false;
                    // Xóa các control khác trên panel1 trước khi thêm chart5
                    panel1.Controls.Clear();
                    DrawChart(); // Vẽ biểu đồ chart5
                }
                else if (rdb_quy.Checked)
                {
                    // Ẩn DataGridView và hiển thị chart6 trên panel1
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = true;

                    // Xóa các control khác trên panel1 trước khi thêm chart6
                    panel1.Controls.Clear();
                    DrawChart2(); // Vẽ biểu đồ chart6
                }
                else if (rdb_nam.Checked)
                {
                    // Hiển thị chart1 và ẩn các control khác
                    data_revenue.Visible = false;
                    chart1.Visible = true;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    panel1.Controls.Clear();
                    //DrawChartFromDataGridView(); // Vẽ biểu đồ chart6
                    DrawChart0();
                }
            }
            else if (cbb_select.SelectedIndex == 2 )
            {
                if (rdb_thang.Checked)
                {
                    // Hiển thị chart2 và chart4 và ẩn các control khác
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = true;
                    chart4.Visible = true;
                    chart5.Visible = false;
                    panel1.Controls.Clear();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart2, 0, 0);
                    chart2.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart4, 1, 0);
                    chart4.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }
                else if (rdb_quy.Checked)
                {
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = false;
                    chart7.Visible = false;
                    chart8.Visible = true;
                    chart9.Visible = false;
                    chart10.Visible = true;
                    panel1.Controls.Clear();
                    DrawPieChart_Quarter();
                    DrawChartMedicine_Quarter();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart8, 0, 0);
                    chart8.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart10, 1, 0);
                    chart10.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart8.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart10.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }
                else if (rdb_nam.Checked)
                {
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = false;
                    chart7.Visible = true;
                    chart8.Visible = false;
                    chart9.Visible = true;
                    panel1.Controls.Clear();
                    DrawPieChart_Year();
                    DrawChartMedicine_Year();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart7, 0, 0);
                    chart7.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart9, 1, 0);
                    chart9.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart7.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart9.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);

                }
            }
            else
            {
                data_revenue.Visible = true;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
            }

        }


        private void DrawPieChart()
        {
            chart2.Series.Clear();
            chart2.ChartAreas.Clear();
            chart2.Titles.Clear();
            int selectedMonth = cbb_month.SelectedIndex + 1; // Tháng được chọn từ ComboBox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem); // Năm được chọn từ ComboBox

            string queryTotal = "SELECT COUNT(*) AS TotalUsage FROM ThongTinDichVu " +
                                "INNER JOIN HoaDon ON ThongTinDichVu.MaHoaDon = HoaDon.MaHoaDon " +
                                $"WHERE MONTH(HoaDon.NgayThamKham) = {selectedMonth} AND YEAR(HoaDon.NgayThamKham) = {selectedYear}";

            // Tính tổng số lần sử dụng của tất cả các loại dịch vụ trong tháng và năm được chọn
            int totalUsage = 0;
            using (SqlCommand commandTotal = new SqlCommand(queryTotal, db.getConnection))
            {
                db.openConnection();
                totalUsage = (int)commandTotal.ExecuteScalar();
                db.closeConnection();
            }

            // Nếu tổng số lần sử dụng là 0, không vẽ biểu đồ
            if (totalUsage == 0)
            {
                //MessageBox.Show("Không có dữ liệu để vẽ biểu đồ.");
                return;
            }

            string query = "SELECT LD.TenLoaiDichVu, COUNT(*) AS SoLanSuDung " +
                           "FROM ThongTinDichVu TD " +
                           "JOIN DichVu DV ON TD.MaDichVu = DV.MaDichVu " +
                           "JOIN LoaiDichVu LD ON DV.MaLoaiDichVu = LD.MaLoaiDichVu " +
                           "JOIN HoaDon ON TD.MaHoaDon = HoaDon.MaHoaDon " +
                           $"WHERE MONTH(HoaDon.NgayThamKham) = {selectedMonth} AND YEAR(HoaDon.NgayThamKham) = {selectedYear} " +
                           "GROUP BY LD.TenLoaiDichVu";

            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Tạo đối tượng SqlCommand và thực thi truy vấn
            using (SqlCommand command = new SqlCommand(query, db.getConnection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    chart2.Dock = DockStyle.Fill;
                    chart2.ChartAreas.Add(new ChartArea());

                    Series series = new Series
                    {
                        Name = "LoaiDichVu",
                        ChartType = SeriesChartType.Pie
                    };

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string tenLoaiDichVu = row["TenLoaiDichVu"].ToString();
                        int soLanSuDung = Convert.ToInt32(row["SoLanSuDung"]);
                        double tiLe = (double)soLanSuDung / totalUsage; // Tính tỉ lệ
                        series.Points.AddXY(tenLoaiDichVu, tiLe);

                    }



                    chart2.Series.Add(series);
                    chart2.Titles.Add("Biểu Đồ Tỉ Lệ Sử Dụng Của Các Loại Dịch Vụ");
                    chart2.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
                    panel1.Controls.Add(chart2);
                }
            }

            // Đóng kết nối đến cơ sở dữ liệu
            db.closeConnection();
        }



        //    private Chart chart3;
        //    private void InitializePieChart()
        //    {
        //        chart3.Series.Clear();
        //        chart3.ChartAreas.Clear();
        //        chart3 = new Chart();
        //        chart3.Dock = DockStyle.Fill;
        //        panel1.Controls.Add(chart3);

        //        // Thêm loại biểu đồ Pie
        //        ChartArea chartArea = new ChartArea();
        //        chart3.ChartAreas.Add(chartArea);

        //        Series series = new Series();
        //        series.ChartType = SeriesChartType.Pie;
        //        series.ChartArea = chartArea.Name;
        //        chart3.Series.Add(series);
        //    }
        //    private void DrawPieChart2()
        //    {

        //        int selectedMonth = cbb_month.SelectedIndex + 1; // Tháng được chọn từ ComboBox
        //        int selectedYear = Convert.ToInt32(cbb_year.SelectedItem); // Năm được chọn từ ComboBox

        //        string query = @"
        //    SELECT LD.TenLoaiDichVu, SUM(SoLuong*GiaDichVu*(1-GiamGia/100)) AS TotalRevenue
        //    FROM HOADON HD
        //    INNER JOIN THONGTINDICHVU TD ON HD.MaHoaDon = TD.MaHoaDon
        //    INNER JOIN DICHVU DV ON TD.MaDichVu = DV.MaDichVu
        //    INNER JOIN LOAIDICHVU LD ON DV.MaLoaiDichVu = LD.MaLoaiDichVu
        //    WHERE MONTH(HD.NgayThamKham) = @Month AND YEAR(HD.NgayThamKham) = @Year
        //    GROUP BY LD.TenLoaiDichVu
        //";

        //        SqlDataReader reader = null;
        //        try
        //        {
        //            db.openConnection(); // Mở kết nối đến cơ sở dữ liệu

        //            SqlCommand command = new SqlCommand(query, db.getConnection);
        //            command.Parameters.AddWithValue("@Month", selectedMonth);
        //            command.Parameters.AddWithValue("@Year", selectedYear);

        //            reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                string serviceName = reader["TenLoaiDichVu"].ToString();
        //                double totalRevenue = Convert.ToDouble(reader["TotalRevenue"]);

        //                // Thêm dữ liệu vào biểu đồ
        //                chart3.Series[0].Points.AddXY(serviceName, totalRevenue);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("Error: ccc " + ex.Message);
        //        }
        //        finally
        //        {
        //            if (reader != null)
        //            {
        //                reader.Close();
        //            }

        //            db.closeConnection(); // Đóng kết nối
        //        }
        //    }

        Chart chart4 = new Chart();

        private void DrawChartMedicine()
        {
            chart4.Series.Clear();
            chart4.ChartAreas.Clear();
            chart4.Titles.Clear();
            db.openConnection();

            // Lấy tháng và năm từ combobox
            int selectedMonth = cbb_month.SelectedIndex + 1; // Tháng bắt đầu từ 1
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Truy vấn dữ liệu dựa trên tháng và năm đã chọn
            string query = "SELECT Thuoc.TenThuoc, SUM(ThongTinSuDungThuoc.SoLuong) AS TotalUsage " +
                           "FROM ThongTinSuDungThuoc " +
                           "INNER JOIN DonThuoc ON ThongTinSuDungThuoc.MaDonThuoc = DonThuoc.MaDonThuoc " +
                           "INNER JOIN HoaDon ON HoaDon.MaHoaDon = DonThuoc.MaHoaDon " +
                           "INNER JOIN Thuoc ON ThongTinSuDungThuoc.MaThuoc = Thuoc.MaThuoc " +
                           "WHERE MONTH(HoaDon.NgayThamKham) = @Month AND YEAR(HoaDon.NgayThamKham) = @Year " +
                           "GROUP BY Thuoc.TenThuoc";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
            {
                cmd.Parameters.AddWithValue("@Month", selectedMonth);
                cmd.Parameters.AddWithValue("@Year", selectedYear);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ tròn
            chart4.Parent = panel1;
            chart4.Dock = DockStyle.Fill;
            chart4.ChartAreas.Add(new ChartArea());

            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series.Points.DataBind(dataTable.DefaultView, "TenThuoc", "TotalUsage", "");

            chart4.Series.Add(series);
            chart4.Titles.Add("Biểu Đồ Tỉ Lệ Sử Dụng Các Loại Thuốc");
            chart4.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            // Đóng kết nối sau khi hoàn tất
            db.closeConnection();
        }
        Chart chart9 = new Chart();
        private void DrawChartMedicine_Year()
        {
            chart9.Series.Clear();
            chart9.ChartAreas.Clear();
            chart9.Titles.Clear();
            db.openConnection();

            // Lấy năm từ combobox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Truy vấn dữ liệu dựa trên năm đã chọn
            string query = "SELECT Thuoc.TenThuoc, SUM(ThongTinSuDungThuoc.SoLuong) AS TotalUsage " +
                           "FROM ThongTinSuDungThuoc " +
                           "INNER JOIN DonThuoc ON ThongTinSuDungThuoc.MaDonThuoc = DonThuoc.MaDonThuoc " +
                           "INNER JOIN HoaDon ON HoaDon.MaHoaDon = DonThuoc.MaHoaDon " +
                           "INNER JOIN Thuoc ON ThongTinSuDungThuoc.MaThuoc = Thuoc.MaThuoc " +
                           "WHERE YEAR(HoaDon.NgayThamKham) = @Year " +
                           "GROUP BY Thuoc.TenThuoc";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
            {
                cmd.Parameters.AddWithValue("@Year", selectedYear);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ tròn
            chart9.Parent = panel1;
            chart9.Dock = DockStyle.Fill;
            chart9.ChartAreas.Add(new ChartArea());

            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series.Points.DataBind(dataTable.DefaultView, "TenThuoc", "TotalUsage", "");

            chart9.Series.Add(series);
            chart9.Titles.Add($"Biểu Đồ Tỉ Lệ Sử Dụng Các Loại Thuốc Trong Năm {selectedYear}");
            chart9.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            // Đóng kết nối sau khi hoàn tất
            db.closeConnection();
        }
        Chart chart10 = new Chart();
        private void DrawChartMedicine_Quarter()
        {
            chart10.Series.Clear();
            chart10.ChartAreas.Clear();
            chart10.Titles.Clear();
            db.openConnection();

            // Lấy tháng từ combobox
            int selectedMonth = cbb_month.SelectedIndex + 1; // Tháng bắt đầu từ 1

            // Xác định quý từ tháng được chọn
            int quarter = (selectedMonth - 1) / 3 + 1;

            // Lấy năm từ combobox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Truy vấn dữ liệu dựa trên quý và năm đã chọn
            string query = "SELECT Thuoc.TenThuoc, SUM(ThongTinSuDungThuoc.SoLuong) AS TotalUsage " +
                           "FROM ThongTinSuDungThuoc " +
                           "INNER JOIN DonThuoc ON ThongTinSuDungThuoc.MaDonThuoc = DonThuoc.MaDonThuoc " +
                           "INNER JOIN HoaDon ON HoaDon.MaHoaDon = DonThuoc.MaHoaDon " +
                           "INNER JOIN Thuoc ON ThongTinSuDungThuoc.MaThuoc = Thuoc.MaThuoc " +
                           "WHERE DATEPART(QUARTER, HoaDon.NgayThamKham) = @Quarter AND YEAR(HoaDon.NgayThamKham) = @Year " +
                           "GROUP BY Thuoc.TenThuoc";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
            {
                cmd.Parameters.AddWithValue("@Quarter", quarter);
                cmd.Parameters.AddWithValue("@Year", selectedYear);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ tròn
            chart10.Parent = panel1;
            chart10.Dock = DockStyle.Fill;
            chart10.ChartAreas.Add(new ChartArea());

            Series series = new Series();
            series.ChartType = SeriesChartType.Pie;
            series.Points.DataBind(dataTable.DefaultView, "TenThuoc", "TotalUsage", "");

            chart10.Series.Add(series);
            chart10.Titles.Add($"Biểu Đồ Tỉ Lệ Sử Dụng Các Loại Thuốc Trong Quý {quarter} Năm {selectedYear}");
            chart10.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
            // Đóng kết nối sau khi hoàn tất
            db.closeConnection();
        }

        Chart chart5 = new Chart();
        private void DrawChart()
        {
            chart5.Size = panel1.Size;

            // Xóa các series, chart areas, và titles cũ trước khi vẽ biểu đồ mới
            chart5.Series.Clear();
            chart5.ChartAreas.Clear();
            chart5.Titles.Clear();

            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Lấy tháng và năm từ combobox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Truy vấn dữ liệu dựa trên năm đã chọn
            string query = "SELECT MONTH(NgayThamKham) AS Thang, SUM(SoLuong * GiaDichVu * (1 - GiamGia/100)) AS TriGia " +
                           "FROM HoaDon " +
                           "INNER JOIN ThongTinDichVu ON HoaDon.MaHoaDon = ThongTinDichVu.MaHoaDon " +
                           "INNER JOIN DichVu ON ThongTinDichVu.MaDichVu = DichVu.MaDichVu " +
                           "WHERE YEAR(NgayThamKham) = @Year " +
                           "GROUP BY MONTH(NgayThamKham)";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
            {
                cmd.Parameters.AddWithValue("@Year", selectedYear);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ cột
            chart5.Parent = panel1;
            chart5.Dock = DockStyle.Fill;
            ChartArea chartArea = new ChartArea();
            chartArea.AxisY.Minimum = 0; // Thiết lập giá trị tối thiểu của trục tung
            chartArea.AxisX.Maximum = 12; // Đảm bảo hiển thị đủ 12 cột
            chartArea.AxisX.Title = "Tháng"; // Chú thích trục x
            chartArea.AxisY.Title = "Doanh thu (VNĐ)"; // Chú thích trục y
            chart5.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.Int32;
            series.YValueType = ChartValueType.Double;

            foreach (DataRow row in dataTable.Rows)
            {
                int month = Convert.ToInt32(row["Thang"]);
                decimal total = Convert.ToDecimal(row["TriGia"]);
                DataPoint dataPoint = new DataPoint(month, (double)total);
                dataPoint.Label = total.ToString(); // Gán nhãn giá trị cho cột
                series.Points.Add(dataPoint);
            }
            db.closeConnection();

            chart5.Series.Add(series);
            chart5.Titles.Add("Biểu Đồ Tổng Trị Giá Các Dịch Vụ Trong Năm " + selectedYear);
            chart5.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            // Thêm biểu đồ vào panel
            panel1.Controls.Add(chart5);
            // Đóng kết nối sau khi hoàn tất
        }

        Chart chart6 = new Chart();
        private void DrawChart2()
        {
            chart6.Size = panel1.Size;

            // Xóa các series, chart areas, và titles cũ trước khi vẽ biểu đồ mới
            chart6.Series.Clear();
            chart6.ChartAreas.Clear();
            chart6.Titles.Clear();

            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Lấy năm từ combobox
            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem);

            // Truy vấn dữ liệu dựa trên năm đã chọn, nhóm theo quý
            string query = "SELECT DATEPART(QUARTER, NgayThamKham) AS Quy, SUM(SoLuong * GiaDichVu * (1 - GiamGia/100)) AS TriGia " +
                           "FROM HoaDon " +
                           "INNER JOIN ThongTinDichVu ON HoaDon.MaHoaDon = ThongTinDichVu.MaHoaDon " +
                           "INNER JOIN DichVu ON ThongTinDichVu.MaDichVu = DichVu.MaDichVu " +
                           "WHERE YEAR(NgayThamKham) = @Year " +
                           "GROUP BY DATEPART(QUARTER, NgayThamKham)";

            DataTable dataTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
            {
                cmd.Parameters.AddWithValue("@Year", selectedYear);
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dataTable);
                }
            }

            // Tạo biểu đồ cột
            chart6.Parent = panel1;
            chart6.Dock = DockStyle.Fill;
            ChartArea chartArea = new ChartArea();
            chartArea.AxisX.Minimum = 0; // Thiết lập giá trị tối thiểu của trục tung
            chartArea.AxisX.Maximum = 5;
            chartArea.AxisX.Title = "Quý"; // Chú thích trục x
            chartArea.AxisY.Title = "Doanh thu (VNĐ)"; // Chú thích trục y
            chart6.ChartAreas.Add(chartArea);

            Series series = new Series();
            series.ChartType = SeriesChartType.Column;
            series.XValueType = ChartValueType.Int32;
            series.YValueType = ChartValueType.Double;

            foreach (DataRow row in dataTable.Rows)
            {
                int quarter = Convert.ToInt32(row["Quy"]);
                decimal total = Convert.ToDecimal(row["TriGia"]);
                DataPoint dataPoint = new DataPoint(quarter, (double)total);
                dataPoint.Label = total.ToString(); // Thiết lập nhãn cho từng cột
                series.Points.Add(dataPoint);
            }
            db.closeConnection();

            // Thiết lập nhãn cho toàn bộ dữ liệu
            series.IsValueShownAsLabel = true;

            chart6.Series.Add(series);
            chart6.Titles.Add("Biểu Đồ Tổng Trị Giá Các Dịch Vụ Trong Năm " + selectedYear);
            chart6.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);

            // Thêm biểu đồ vào panel
            panel1.Controls.Add(chart6);

            // Đóng kết nối sau khi hoàn tất
        }

        Chart chart7 = new Chart();
        private void DrawPieChart_Year()
        {
            chart7.Series.Clear();
            chart7.ChartAreas.Clear();
            chart7.Titles.Clear();

            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem); // Năm được chọn từ ComboBox

            string queryTotal = "SELECT COUNT(*) AS TotalUsage FROM ThongTinDichVu " +
                                "INNER JOIN HoaDon ON ThongTinDichVu.MaHoaDon = HoaDon.MaHoaDon " +
                                $"WHERE YEAR(HoaDon.NgayThamKham) = {selectedYear}";

            // Tính tổng số lần sử dụng của tất cả các loại dịch vụ trong năm được chọn
            int totalUsage = 0;
            using (SqlCommand commandTotal = new SqlCommand(queryTotal, db.getConnection))
            {
                db.openConnection();
                totalUsage = (int)commandTotal.ExecuteScalar();
                db.closeConnection();
            }

            // Nếu tổng số lần sử dụng là 0, không vẽ biểu đồ
            if (totalUsage == 0)
            {
                //MessageBox.Show("Không có dữ liệu để vẽ biểu đồ.");
                return;
            }

            string query = "SELECT LD.TenLoaiDichVu, COUNT(*) AS SoLanSuDung " +
                           "FROM ThongTinDichVu TD " +
                           "JOIN DichVu DV ON TD.MaDichVu = DV.MaDichVu " +
                           "JOIN LoaiDichVu LD ON DV.MaLoaiDichVu = LD.MaLoaiDichVu " +
                           "JOIN HoaDon ON TD.MaHoaDon = HoaDon.MaHoaDon " +
                           $"WHERE YEAR(HoaDon.NgayThamKham) = {selectedYear} " +
                           "GROUP BY LD.TenLoaiDichVu";

            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Tạo đối tượng SqlCommand và thực thi truy vấn
            using (SqlCommand command = new SqlCommand(query, db.getConnection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    chart7.Dock = DockStyle.Fill;
                    chart7.ChartAreas.Add(new ChartArea());

                    Series series = new Series
                    {
                        Name = "LoaiDichVu",
                        ChartType = SeriesChartType.Pie
                    };

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string tenLoaiDichVu = row["TenLoaiDichVu"].ToString();
                        int soLanSuDung = Convert.ToInt32(row["SoLanSuDung"]);
                        double tiLe = (double)soLanSuDung / totalUsage; // Tính tỉ lệ

                        series.Points.AddXY(tenLoaiDichVu, tiLe);
                    }

                    chart7.Series.Add(series);
                    chart7.Titles.Add("Biểu Đồ Tỉ Lệ Sử Dụng Của Các Loại Dịch Vụ Trong Năm " + selectedYear);
                    chart7.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
                    panel1.Controls.Add(chart7);
                }
            }

            // Đóng kết nối đến cơ sở dữ liệu
            db.closeConnection();
        }
        Chart chart8 = new Chart();
        private void DrawPieChart_Quarter()
        {
            chart8.Series.Clear();
            chart8.ChartAreas.Clear();
            chart8.Titles.Clear();

            int selectedYear = Convert.ToInt32(cbb_year.SelectedItem); // Năm được chọn từ ComboBox
            int selectedMonth = cbb_month.SelectedIndex + 1; // Tháng được chọn từ ComboBox

            // Xác định quý từ tháng được chọn
            int quarter = (selectedMonth - 1) / 3 + 1;

            string queryTotal = "SELECT COUNT(*) AS TotalUsage FROM ThongTinDichVu " +
                                "INNER JOIN HoaDon ON ThongTinDichVu.MaHoaDon = HoaDon.MaHoaDon " +
                                $"WHERE YEAR(HoaDon.NgayThamKham) = {selectedYear}";

            // Tính tổng số lần sử dụng của tất cả các loại dịch vụ trong năm được chọn
            int totalUsage = 0;
            using (SqlCommand commandTotal = new SqlCommand(queryTotal, db.getConnection))
            {
                db.openConnection();
                totalUsage = (int)commandTotal.ExecuteScalar();
                db.closeConnection();
            }

            // Nếu tổng số lần sử dụng là 0, không vẽ biểu đồ
            if (totalUsage == 0)
            {
                //MessageBox.Show("Không có dữ liệu để vẽ biểu đồ.");
                return;
            }

            string query = "SELECT @Quarter AS Quy, LD.TenLoaiDichVu, COUNT(*) AS SoLanSuDung " +
                           "FROM ThongTinDichVu TD " +
                           "JOIN DichVu DV ON TD.MaDichVu = DV.MaDichVu " +
                           "JOIN LoaiDichVu LD ON DV.MaLoaiDichVu = LD.MaLoaiDichVu " +
                           "JOIN HoaDon ON TD.MaHoaDon = HoaDon.MaHoaDon " +
                           $"WHERE YEAR(HoaDon.NgayThamKham) = {selectedYear} AND MONTH(HoaDon.NgayThamKham) = {selectedMonth} " +
                           "GROUP BY LD.TenLoaiDichVu";

            // Mở kết nối đến cơ sở dữ liệu
            db.openConnection();

            // Tạo đối tượng SqlCommand và thực thi truy vấn
            using (SqlCommand command = new SqlCommand(query, db.getConnection))
            {
                command.Parameters.AddWithValue("@Quarter", quarter);

                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    chart8.Dock = DockStyle.Fill;
                    chart8.ChartAreas.Add(new ChartArea());

                    Series series = new Series
                    {
                        Name = "LoaiDichVu",
                        ChartType = SeriesChartType.Pie
                    };

                    foreach (DataRow row in dataTable.Rows)
                    {
                        string tenLoaiDichVu = row["TenLoaiDichVu"].ToString();
                        int soLanSuDung = Convert.ToInt32(row["SoLanSuDung"]);
                        double tiLe = (double)soLanSuDung / totalUsage; // Tính tỉ lệ

                        series.Points.AddXY($"Quý {quarter} - {tenLoaiDichVu}", tiLe);
                    }

                    chart8.Series.Add(series);
                    chart8.Titles.Add($"Biểu Đồ Tỉ Lệ Sử Dụng Của Các Loại Dịch Vụ Trong Quý {quarter} Năm {selectedYear}");
                    chart8.Titles[0].Font = new Font("Arial", 14, FontStyle.Bold);
                    panel1.Controls.Add(chart8);
                }
            }

            // Đóng kết nối đến cơ sở dữ liệu
            db.closeConnection();
        }

        // Thêm sự kiện cho control chọn biểu đồ
        private void cbb_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_select.SelectedIndex == 0)
            {
                // Hiển thị DataGridView và ẩn các biểu đồ
                data_revenue.Visible = true;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
            }
            else if (cbb_select.SelectedIndex == 1)
            {
                if (rdb_nam.Checked)
                {

                    // Hiển thị chart1 và ẩn các control khác
                    data_revenue.Visible = false;
                    chart1.Visible = true;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    panel1.Controls.Clear();
                    //DrawChartFromDataGridView(); // Vẽ biểu đồ chart6
                    DrawChart0();
                }
                else if (rdb_quy.Checked)
                {
                    // Ẩn DataGridView và hiển thị chart6 trên panel1
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = true;

                    // Xóa các control khác trên panel1 trước khi thêm chart6
                    panel1.Controls.Clear();
                    DrawChart2(); // Vẽ biểu đồ chart6
                }
                else if (rdb_thang.Checked)
                {
                    // Ẩn DataGridView và hiển thị chart5 trên panel1
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = true;
                    chart6.Visible = false;
                    // Xóa các control khác trên panel1 trước khi thêm chart5
                    panel1.Controls.Clear();
                    DrawChart(); // Vẽ biểu đồ chart5
                }

            }
            else if (cbb_select.SelectedIndex == 2)
            {
                if (rdb_thang.Checked)
                {
                    // Hiển thị chart2 và chart4 và ẩn các control khác
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = true;
                    chart4.Visible = true;
                    chart5.Visible = false;
                    panel1.Controls.Clear();
                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart2, 0, 0);
                    chart2.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart4, 1, 0);
                    chart4.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }
                else if (rdb_nam.Checked)
                {
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = false;
                    chart7.Visible = true;
                    chart8.Visible = false;
                    chart9.Visible = true;
                    panel1.Controls.Clear();
                    DrawPieChart_Year();
                    DrawChartMedicine_Year();
                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart7, 0, 0);
                    chart7.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart9, 1, 0);
                    chart9.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart7.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart9.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }
                else if (rdb_quy.Checked)
                {
                    data_revenue.Visible = false;
                    chart1.Visible = false;
                    chart2.Visible = false;
                    chart4.Visible = false;
                    chart5.Visible = false;
                    chart6.Visible = false;
                    chart7.Visible = false;
                    chart8.Visible = true;
                    chart9.Visible = false;
                    chart10.Visible = true;
                    panel1.Controls.Clear();
                    DrawPieChart_Quarter();
                    DrawChartMedicine_Quarter();
                    TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                    tableLayoutPanel.Dock = DockStyle.Fill;
                    tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                    panel1.Controls.Add(tableLayoutPanel);

                    // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                    tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart8, 0, 0);
                    chart8.Dock = DockStyle.Fill;

                    // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                    tableLayoutPanel.Controls.Add(chart10, 1, 0);
                    chart10.Dock = DockStyle.Fill;
                    // Thiết lập kích thước cố định cho cả hai biểu đồ
                    chart8.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                    chart10.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                }

            }

        }

        private void rdb_thang_CheckedChanged(object sender, EventArgs e)
        {
            if (cbb_select.SelectedIndex == 2)
            {

                // Hiển thị chart2 và chart4 và ẩn các control khác
                data_revenue.Visible = false;
                chart1.Visible = false;
                chart2.Visible = true;
                chart4.Visible = true;
                chart5.Visible = false;
                panel1.Controls.Clear();
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = DockStyle.Fill;
                tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                panel1.Controls.Add(tableLayoutPanel);

                // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                tableLayoutPanel.Controls.Add(chart2, 0, 0);
                chart2.Dock = DockStyle.Fill;

                // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                tableLayoutPanel.Controls.Add(chart4, 1, 0);
                chart4.Dock = DockStyle.Fill;
                // Thiết lập kích thước cố định cho cả hai biểu đồ
                chart2.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                chart4.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);

            }
            else if (cbb_select.SelectedIndex == 1)
            {

                // Ẩn DataGridView và hiển thị chart5 trên panel1
                data_revenue.Visible = false;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = true;
                chart6.Visible = false;
                // Xóa các control khác trên panel1 trước khi thêm chart5
                panel1.Controls.Clear();
                DrawChart(); // Vẽ biểu đồ chart5

            }

        }

        private void rdb_quy_CheckedChanged(object sender, EventArgs e)
        {
            if (cbb_select.SelectedIndex == 2)
            {
                data_revenue.Visible = false;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
                chart6.Visible = false;
                chart7.Visible = false;
                chart8.Visible = true;
                chart9.Visible = false;
                chart10.Visible = true;
                panel1.Controls.Clear();
                DrawPieChart_Quarter();
                DrawChartMedicine_Quarter();
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = DockStyle.Fill;
                tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                panel1.Controls.Add(tableLayoutPanel);

                // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                tableLayoutPanel.Controls.Add(chart8, 0, 0);
                chart8.Dock = DockStyle.Fill;

                // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                tableLayoutPanel.Controls.Add(chart10, 1, 0);
                chart10.Dock = DockStyle.Fill;
                // Thiết lập kích thước cố định cho cả hai biểu đồ
                chart8.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                chart10.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);


            }
            else if (cbb_select.SelectedIndex == 1)
            {
                // Ẩn DataGridView và hiển thị chart6 trên panel1
                data_revenue.Visible = false;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
                chart6.Visible = true;

                // Xóa các control khác trên panel1 trước khi thêm chart6
                panel1.Controls.Clear();
                DrawChart2(); // Vẽ biểu đồ chart6


            }
        }

        private void rdb_nam_CheckedChanged(object sender, EventArgs e)
        {
            if (cbb_select.SelectedIndex == 2)
            {
                data_revenue.Visible = false;
                chart1.Visible = false;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
                chart6.Visible = false;
                chart7.Visible = true;
                chart8.Visible = false;
                chart9.Visible = true;
                panel1.Controls.Clear();
                DrawPieChart_Year();
                DrawChartMedicine_Year();
                TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
                tableLayoutPanel.Dock = DockStyle.Fill;
                tableLayoutPanel.ColumnCount = 2; // Số cột là 2 để chứa cả chart2 và chart4
                panel1.Controls.Add(tableLayoutPanel);

                // Thiết lập tỷ lệ tự động cho TableLayoutPanel để chia đều không gian
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                // Thêm chart2 vào ô đầu tiên của TableLayoutPanel
                tableLayoutPanel.Controls.Add(chart7, 0, 0);
                chart7.Dock = DockStyle.Fill;

                // Thêm chart4 vào ô thứ hai của TableLayoutPanel
                tableLayoutPanel.Controls.Add(chart9, 1, 0);
                chart9.Dock = DockStyle.Fill;
                // Thiết lập kích thước cố định cho cả hai biểu đồ
                chart7.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);
                chart9.Size = new Size(tableLayoutPanel.Width / 2, tableLayoutPanel.Height);

            }
            else if (cbb_select.SelectedIndex == 1)
            {
                // Hiển thị chart1 và ẩn các control khác
                data_revenue.Visible = false;
                chart1.Visible = true;
                chart2.Visible = false;
                chart4.Visible = false;
                chart5.Visible = false;
                panel1.Controls.Clear();
                //DrawChartFromDataGridView(); // Vẽ biểu đồ chart6
                DrawChart0();
            }
        }
    }
}
