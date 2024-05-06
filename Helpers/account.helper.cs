using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Helpers
{
    public class AccountHelper
    {
        public static bool checkUsername(string username)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();
                string query = $"select * from TAIKHOAN where TenDangNhap = '{username}'";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows) check = true;
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return check;
        }

        public static bool addUserAccount(string username, string password, int decentralization, int employeeID)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();

                // Tạo command để gọi stored procedure addUserAccount
                SqlCommand cmd = new SqlCommand("addUserAccount", db.getConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số cho stored procedure
                cmd.Parameters.AddWithValue("@TenDangNhap", username);
                cmd.Parameters.AddWithValue("@MatKhau", password);
                cmd.Parameters.AddWithValue("@Quyen", decentralization);
                cmd.Parameters.AddWithValue("@MaNhanVien", employeeID);

                // Thực thi stored procedure
                if (cmd.ExecuteNonQuery() > 0) check = true;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return true;
        }

        public static bool deleteUserAccount(int employeeID)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd  = new SqlCommand($"delete from taikhoan where MaNhanVien = {employeeID}", db.getConnection))
                {
                    if (cmd.ExecuteNonQuery() > 0) check = true;
                }
                db.closeConnection();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return check;
        }

    }
}
