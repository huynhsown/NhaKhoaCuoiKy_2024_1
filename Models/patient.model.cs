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

        public static int AddPatient( string fullName, string gender, DateTime dateOfBirth, int houseNumber, string ward, string city, Image image, string phoneNumber, string streetName)
        {
            Database db = null;
            int newPatientID = 0;
            try
            {
                db = new Database();
                db.openConnection();

                using (SqlCommand cmd = new SqlCommand("addPatient", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaBenhNhan", 0);
                    cmd.Parameters.AddWithValue("@HoVaTen", fullName);
                    cmd.Parameters.AddWithValue("@GioiTinh", gender);
                    cmd.Parameters.AddWithValue("@NgaySinh", dateOfBirth);
                    cmd.Parameters.AddWithValue("@SoNha", houseNumber);
                    cmd.Parameters.AddWithValue("@Phuong", ward);
                    cmd.Parameters.AddWithValue("@ThanhPho", city);
                    // Assuming the image parameter is of type byte array (byte[])
                    cmd.Parameters.AddWithValue("@Anh", ConvertImageToByteArray(image));
                    cmd.Parameters.AddWithValue("@SoDienThoai", phoneNumber);
                    cmd.Parameters.AddWithValue("@TenDuong", streetName);

                    cmd.ExecuteNonQuery();

                    newPatientID = Convert.ToInt32(cmd.Parameters["@MaBenhNhan"].Value);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return newPatientID;
        }

        // Helper method to convert Image to byte array
        private static byte[] ConvertImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); // Assuming the image format is JPEG
                return ms.ToArray();
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
