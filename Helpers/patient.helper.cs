using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaKhoaCuoiKy.dbs;
using NhaKhoaCuoiKy.Views.Appointment;
using NhaKhoaCuoiKy.Views;

namespace NhaKhoaCuoiKy.Helpers
{
    public class PatientHelper
    {
        public static DataTable getByID(int id)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaBenhNhan", id);
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
                if (db != null) { db.closeConnection();}
            }
            return dt;
        }

        public static DataTable getAllPatient()
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("select * from BENHNHAN", db.getConnection))
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
                if (db != null) { db.closeConnection(); }
            }
            return dt;
        }

        public static int addNewRecord(int patientID, int staffID, string dentalDisease, string otherDisease, string symptoms, string result_, string diagnosis, string treatmentMethod, string nextAppointment, DateTime recordDate)
        {
            Database db = null;
            int recordID = 0;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("AddNewRecord", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RecordID", 0);
                    cmd.Parameters.AddWithValue("@PatientID", patientID);
                    cmd.Parameters.AddWithValue("@StaffID", staffID);
                    cmd.Parameters.AddWithValue("@DentalDisease", dentalDisease);
                    cmd.Parameters.AddWithValue("@OtherDisease", otherDisease);
                    cmd.Parameters.AddWithValue("@Symptoms", symptoms);
                    cmd.Parameters.AddWithValue("@Result", result_);
                    cmd.Parameters.AddWithValue("@Diagnosis", diagnosis);
                    cmd.Parameters.AddWithValue("@TreatmentMethod", treatmentMethod);
                    cmd.Parameters.AddWithValue("@NextAppointment", nextAppointment);
                    cmd.Parameters.AddWithValue("@RecordDate", recordDate);

                    // Thực thi procedure và kiểm tra kết quả
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    while(dataReader.Read())
                    {
                        recordID = Convert.ToInt32(dataReader.GetValue(0));
                        break;
                    }
                    dataReader.Close();
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ
                throw ex;
            }
            finally
            {
                if (db != null) db.closeConnection();
            }
            return recordID;
        }
        public static bool removePatientAndRelatedRecords(int patienID)
        {
            Database db = null;
            bool success = false;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("removePatientAndRelatedRecords", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaBenhNhan", patienID);

                    // Thêm parameter out cho biến @Success
                    SqlParameter successParam = new SqlParameter("@Success", SqlDbType.Bit);
                    successParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(successParam);

                    cmd.ExecuteNonQuery();

                    // Kiểm tra giá trị của biến @Success
                    success = Convert.ToBoolean(successParam.Value);
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
            return success;
        }

        public static int addInvoice(int patientID, DateTime date, DataGridView medicine, DataGridView service)
        {
            Database db = null;
            int newInvoiceID = 0;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("addInvoice", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaBenhNhan", patientID);
                    cmd.Parameters.AddWithValue("@NgayThamKham", date);
                    cmd.Parameters.AddWithValue("@MaHoaDon", 0);

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        newInvoiceID = Convert.ToInt32(reader["MaHoaDon"]);
                    }

                    reader.Close();

                }

                foreach (DataGridViewRow drv in medicine.Rows)
                {
                    string medicineID = drv.Cells[0].Value.ToString();
                    int amount = Convert.ToInt32(drv.Cells[2].Value);
                    using (SqlCommand cmd = new SqlCommand("addMedicineInformation", db.getConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaThuoc", medicineID);
                        cmd.Parameters.AddWithValue("@SoLuong", amount);
                        cmd.Parameters.AddWithValue("@NgaySuDung", date);
                        cmd.Parameters.AddWithValue("@MaHoaDon", newInvoiceID);
                        cmd.ExecuteNonQuery();
                    }
                }
                foreach (DataGridViewRow drv in service.Rows)
                {
                    int employeeID = Convert.ToInt32(drv.Cells["Column9"].Value);
                    DateTime dateOfService = Convert.ToDateTime(drv.Cells[7].Value);
                    int amount = Convert.ToInt32(drv.Cells[3].Value);
                    int time = Convert.ToInt32(drv.Cells["Column10"].Value);
                    int serviceID = Convert.ToInt32(drv.Cells[0].Value);
                    using(SqlCommand cmd = new SqlCommand("addServiceInformation", db.getConnection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@MaNhanVien", employeeID);
                        cmd.Parameters.AddWithValue("@NgaySuDung", dateOfService);
                        cmd.Parameters.AddWithValue("@SoLuong", amount);
                        cmd.Parameters.AddWithValue("@ThoiGianThucHien", time);
                        cmd.Parameters.AddWithValue("@MaDichVu", serviceID);
                        cmd.Parameters.AddWithValue("@MaHoaDon", newInvoiceID);
                        cmd.ExecuteNonQuery();
                    }
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
            return newInvoiceID;
        }


        public static DataTable getRecordByPatient(int patientID)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand($"select * from THONGTINBENHAN join NHANVIEN on THONGTINBENHAN.MaNhanVien = NHANVIEN.MaNhanVien where THONGTINBENHAN.MaBenhNhan = {patientID};", db.getConnection))
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
                if (db != null) { db.closeConnection(); }
            }
            return dt;
        }

        public static DataTable getRecordByID(int recordID)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand($"select * from THONGTINBENHAN join NHANVIEN on THONGTINBENHAN.MaNhanVien = NHANVIEN.MaNhanVien where THONGTINBENHAN.MaBenhAn = {recordID};", db.getConnection))
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
                if (db != null) { db.closeConnection(); }
            }
            return dt;
        }

        public static bool removeRecordByID(int recordID)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();
                string query = $"delete from THONGTINBENHAN where THONGTINBENHAN.MaBenhAn = {recordID}";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    if (cmd.ExecuteNonQuery() > 0) check = true;
                }
                db.closeConnection();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db != null) { db.closeConnection(); }
            }
            return check;
        }

        public static bool removeInvoiceByID(int recordID)
        {
            Database db = null;
            bool check = false;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("deleteInvoiceAndRelatedInfo", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm tham số cho thủ tục lưu trữ
                    cmd.Parameters.AddWithValue("@MaHoaDon", recordID);

                    // Thực thi thủ tục lưu trữ
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra xem có hóa đơn nào bị xóa hay không
                    if (rowsAffected > 0)
                    {
                        check = true;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (db != null) { db.closeConnection(); }
            }
            return check;
        }


        public static DataTable getInvoiceByPatient(int patientID)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand($"select * from HOADON where HOADON.MaBenhNhan = {patientID};", db.getConnection))
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
                if (db != null) { db.closeConnection(); }
            }
            return dt;
        }
        public static DataTable getMedicineOfInvoice(int invoiceID)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand($"select * from THONGTINSUDUNGTHUOC join THUOC on THONGTINSUDUNGTHUOC.MaThuoc = THUOC.MaThuoc where MaHoaDon = {invoiceID};", db.getConnection))
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
                if (db != null) { db.closeConnection(); }
            }
            return dt;
        }

        public static DataTable getServiceOfInvoice(int invoiceID)
        {
            DataTable dt = new DataTable();
            Database db = null;
            try
            {
                db = new Database();
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand($"select * from THONGTINDICHVU join DICHVU on THONGTINDICHVU.MaDichVu = DICHVU.MaDichVu\r\njoin NHANVIEN on THONGTINDICHVU.MaNhanVien = NHANVIEN.MaNhanVien\r\n where MaHoaDon = {invoiceID};", db.getConnection))
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
                if (db != null) { db.closeConnection(); }
            }
            return dt;
        }
    }
}
