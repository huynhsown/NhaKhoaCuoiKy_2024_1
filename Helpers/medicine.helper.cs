using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using NhaKhoaCuoiKy.Views.Employee;
using System.Xml.Linq;

namespace NhaKhoaCuoiKy.Helpers
{
    internal class MedicineHelper
    {
        public static DataTable getAllMedicine()
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database();
                db.openConnection();
                string query = "SELECT * FROM THUOC";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Thiết lập mã hóa UTF-8 cho đọc dữ liệu
                        dt.Load(reader);
                    }
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public static string addNewMedicine(string maThuoc, string tenThuoc, string hDSD, string thanhPhan, int giaNhap, int giaBan, int soLuong, string congTy)
        {
            Database db = new Database();
            string id = null;
            try
            {
                int check;
                var p = new DynamicParameters();
                p.Add("@MaThuoc", maThuoc);
                p.Add("@TenThuoc", tenThuoc);
                p.Add("@HuongDanSD", hDSD);
                p.Add("@ThanhPhan", thanhPhan);
                p.Add("@GiaNhap", giaNhap);
                p.Add("@GiaBan", giaBan);
                p.Add("@SoLuong", soLuong);
                p.Add("@CongTy", congTy);

                using (IDbConnection connection = db.getConnection)
                {
                    check = connection.Execute("addMedicine", p, commandType: CommandType.StoredProcedure);
                }
                if (check == 1) id = p.Get<string>("@MaThuoc");
            }
            catch
            {
                throw;
            }
            finally
            {

            }
            return id;
        }

        internal static DataTable getMedicineByID(string filter)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getMedicineByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaThuoc", filter);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.closeConnection();
            }
            return dt;
        }

        internal static DataTable getMedicineByName(string filter)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getMedicineByName", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenThuoc", filter);
                    SqlDataReader reader = cmd.ExecuteReader();
                    dt.Load(reader);
                    reader.Close();
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.closeConnection();
            }
            return dt;
        }

        internal static bool removeMedicine(string medID)
        {
            Database db = new Database();
            bool check = false;
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("removeMedicine", db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@MaThuoc", medID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (cmd.ExecuteNonQuery() >= 1)
                    {
                        check = true;
                    }
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            finally
            {
                db.closeConnection();
            }
            return check;
        }

        internal static bool updateMedicine(string maThuoc, string tenThuoc, string hDSD, string thanhPhan, int giaNhap, int giaBan, int soLuong, string congTy)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("updateMedicine", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaThuoc", maThuoc);
                    cmd.Parameters.AddWithValue("@TenThuoc", tenThuoc);
                    cmd.Parameters.AddWithValue("@HuongDanSD", hDSD);
                    cmd.Parameters.AddWithValue("@ThanhPhan", thanhPhan);
                    cmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
                    cmd.Parameters.AddWithValue("@GiaBan", giaBan);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmd.Parameters.AddWithValue("@CongTy", congTy);



                    // Execute the command
                    if (cmd.ExecuteNonQuery() > 0) check = true;
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                throw ex;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return check;
        }
    }
}
