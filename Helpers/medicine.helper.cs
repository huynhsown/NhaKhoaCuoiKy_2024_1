using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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

        public static string addNewMedicine( string maThuoc,string tenThuoc, string hDSD, string thanhPhan, int giaNhap, int giaBan, int soLuong, string congTy)
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
    }
}
