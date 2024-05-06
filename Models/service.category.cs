using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Models
{
    internal class ServiceCategory
    {
        Database db;

        public ServiceCategory()
        {
            db = new Database();
        }

        public void addNewCategory(string categoryContent)
        {
            try
            {
                db.openConnection();
                using(SqlCommand cmd = new SqlCommand("addCategory", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaLoaiDichVu", 0);
                    cmd.Parameters.AddWithValue("@HoVaTen", categoryContent);
                    cmd.ExecuteNonQuery();
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
        }
    }
}
