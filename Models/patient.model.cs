using Dapper;
using NhaKhoaCuoiKy.dbs;
using System.Data;
using System.Data.SqlClient;


namespace NhaKhoaCuoiKy.Models
{
    internal class PatientModel
    {
        Database db;

        public PatientModel()
        {
            db = new Database();
        }

        public DynamicParameters addPatient(string name, string gender, DateTime birth, int homenumber, string ward, string city, byte[] img, string phone, string street)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("@MaBenhNhan", 0, DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@HoVaTen", name);
                p.Add("@GioiTinh", gender);
                p.Add("@NgaySinh", birth);
                p.Add("@SoNha", homenumber);
                p.Add("@Phuong", ward);
                p.Add("@ThanhPho", city);
                p.Add("@Anh", img);
                p.Add("@SoDienThoai", phone);
                p.Add("@TenDuong", street);

                using (IDbConnection connection = db.getConnection)
                {
                    connection.Execute("addPatient", p, commandType: CommandType.StoredProcedure);
                }
                MessageBox.Show(p.Get<int>("@MaBenhNhan").ToString());
                return p;
            }
            catch
            {
                throw;
            }
        }

        public DataTable getByID(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                db.openConnection();
                using(SqlCommand cmd = new SqlCommand("getByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaBenhNhan", id);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable getAllPatient()
        {
            DataTable dt = new DataTable();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("select * from BENHNHAN", db.getConnection))
                {
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            return dt;
        }

        public DataTable getByName(string name)
        {
            DataTable dt = new DataTable();
            try
            {
                db.openConnection();
                using(SqlCommand cmd = new SqlCommand("getByName", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HoVaTen", name);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                db.closeConnection();
            }
            catch { throw; }
            return dt;
        }

        public DataTable getByPhone(string phone)
        {
            DataTable dt = new DataTable();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getByPhone", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SoDienThoai", phone);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dt.Load(dr);
                }
                db.closeConnection();
            }
            catch { throw; }
            return dt;
        }
    }
}
