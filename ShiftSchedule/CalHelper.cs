using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.ShiftSchedule
{
    internal class CalHelper
    {
        public static DataTable getTime(DateTime monday, DateTime sunday)
        {
            Database db = new Database();
            DataTable dt = new DataTable();
            try
            {
                db.openConnection();
                string query = "SELECT * FROM PHANCA JOIN BAOVE ON PHANCA.MaNhanVien = BAOVE.MaNhanVien JOIN NHANVIEN ON BAOVE.MaNhanVien = NHANVIEN.MaNhanVien WHERE CAST(Ngay AS DATE) >= @monday AND CAST(Ngay AS DATE) <= @sunday";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@monday", monday.Date);
                    cmd.Parameters.AddWithValue("@sunday", sunday.Date);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                db.closeConnection();
            }
            return dt;
        }

        public static bool addTime(int maNhanVien, string ca, DateTime date)
        {
            Database db = new Database();
            try
            {
                db.openConnection();
                string query = "INSERT INTO PhanCa (MaNhanVien, Ca, Ngay) VALUES (@maNhanVien, @ca, @ngay)";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@maNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@ca", ca);
                    cmd.Parameters.AddWithValue("@ngay", date.Date); // Đảm bảo chỉ có phần ngày được sử dụng
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Trả về true nếu có ít nhất một hàng được chèn thành công
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
            finally
            {
                db.closeConnection();
            }
        }

    }
}
