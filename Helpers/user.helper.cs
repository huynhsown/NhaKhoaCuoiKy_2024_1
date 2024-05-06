using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
