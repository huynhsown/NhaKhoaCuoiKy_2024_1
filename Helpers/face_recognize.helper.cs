using Emgu.CV;
using Emgu.CV.Cuda;
using Emgu.CV.Structure;
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
    public class FaceRecognizeHelper
    {
        public static DataTable getAllFace()
        {
            Database db = null;
            DataTable dt = new DataTable();
            try
            {
                db = new Database();
                db.openConnection();
                string query = "select * from KHUONMAT ORDER BY MaNhanVien DESC";
                using(SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dt.Load(dataReader);
                    dataReader.Close();
                }
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

        public static bool addFace(Image<Gray, byte> face, int label)
        {
            Database db = null;
            bool check = true;
            try
            {
                db = new Database();
                db.openConnection();
                string query = "insert into KHUONMAT values(@Face, @Label)";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    byte[] imageData;
                    using (var stream = new System.IO.MemoryStream())
                    {
                        face.ToBitmap().Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                        imageData = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@Face", imageData);
                    cmd.Parameters.AddWithValue("@Label", label);
                    if (cmd.ExecuteNonQuery() > 0) check = true;
                }
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

        public static bool updateFace(Image<Gray, byte> face, int label)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();
                string query = "UPDATE KHUONMAT SET Anh = @Face WHERE MaNhanVien = @Label";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    byte[] imageData;
                    using (var stream = new System.IO.MemoryStream())
                    {
                        face.ToBitmap().Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                        imageData = stream.ToArray();
                    }
                    cmd.Parameters.AddWithValue("@Face", imageData);
                    cmd.Parameters.AddWithValue("@Label", label);
                    if (cmd.ExecuteNonQuery() > 0)
                        check = true;
                }
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


        public static DataTable getAllFaceByEmployeeID(int employeeID)
        {
            Database db = null;
            DataTable dt = new DataTable();
            try
            {
                db = new Database();
                db.openConnection();
                string query = $"select * from KHUONMAT where MaNhanVien = {employeeID}";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    dt.Load(dataReader);
                    dataReader.Close();
                }
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
