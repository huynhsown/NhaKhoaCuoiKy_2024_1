using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace NhaKhoaCuoiKy.Helpers
{
    public class UserHelper
    {
        public static DataTable getUser(string username, string password)
        {
            Database db = null;
            DataTable dt = new DataTable();
            try
            {
                db = new Database();
                db.openConnection();
                using(SqlCommand cmd = new SqlCommand("getUser", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
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
            return dt;
        }
        
        public static DataTable getUserByEmployeeID(int employeeID)
        {

            Database db = null;
            DataTable dt = new DataTable();
            try
            {
                db = new Database();
                db.openConnection();
                string query = $"SELECT TAIKHOAN.* \r\nFROM NHANVIEN \r\njoin TAIKHOAN on NHANVIEN.MaNhanVien = TAIKHOAN.MaNhanVien \r\njoin KHUONMAT on NHANVIEN.MaNhanVien = KHUONMAT.MaNhanVien\r\nWHERE KHUONMAT.MaNhanVien = {employeeID}";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                    dr.Close();
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
            return dt;
        }
    }
}
