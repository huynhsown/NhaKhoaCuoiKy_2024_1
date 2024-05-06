﻿using Dapper;
using NhaKhoaCuoiKy.dbs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace NhaKhoaCuoiKy.Helpers
{
    internal class EmployeeHelper
    {
        public static DataTable getAllEmployee()
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database();
                db.openConnection();
                string query = "SELECT * FROM NHANVIEN";
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

        public static DataTable getAllEmployeeWithAccount()
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database();
                db.openConnection();
                string query = "select * from NHANVIEN left join TAIKHOAN on NHANVIEN.MaNhanVien = TAIKHOAN.MaNhanVien";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Thiết lập mã hóa UTF-8 cho đọc dữ liệu
                        dt.Load(reader);
                        reader.Close();
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

        public static DataTable getAllEmployeeWithAccountByID(int employeeID)
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database();
                db.openConnection();
                string query = $"select * from NHANVIEN left join TAIKHOAN on NHANVIEN.MaNhanVien = TAIKHOAN.MaNhanVien where NHANVIEN.MaNhanVien = {employeeID}";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Thiết lập mã hóa UTF-8 cho đọc dữ liệu
                        dt.Load(reader);
                        reader.Close();
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

        public static DataTable getAllEmployeeWithAccountByName(string employeeName)
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database();
                db.openConnection();
                string query = $"SELECT * \r\nFROM NHANVIEN \r\nLEFT JOIN TAIKHOAN ON NHANVIEN.MaNhanVien = TAIKHOAN.MaNhanVien \r\nWHERE UPPER(NHANVIEN.HoVaTen) COLLATE SQL_Latin1_General_CP1_CI_AI LIKE UPPER(N'%{employeeName}%');\r\n";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Thiết lập mã hóa UTF-8 cho đọc dữ liệu
                        dt.Load(reader);
                        reader.Close();
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

        public static DataTable getAllEmployeeWithAccountByPhone(string phone)
        {
            DataTable dt = new DataTable();
            try
            {
                Database db = new Database();
                db.openConnection();
                string query = $"SELECT * \r\nFROM NHANVIEN \r\nLEFT JOIN TAIKHOAN ON NHANVIEN.MaNhanVien = TAIKHOAN.MaNhanVien \r\nWHERE NHANVIEN.SoDienThoai = '{phone}'";
                using (SqlCommand cmd = new SqlCommand(query, db.getConnection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Thiết lập mã hóa UTF-8 cho đọc dữ liệu
                        dt.Load(reader);
                        reader.Close();
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

        public static int addNewEmployee(string name, string gender, DateTime birth,int salary, DateTime beginworkdate, int homenumber, string ward, string city,string positionwork, byte[] img, string phone, string street)
        {
            Database db = new Database();
            int id = -1;
            try
            {
                int check;
                var p = new DynamicParameters();
                p.Add("@MaNhanVien", 0, DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@HoVaTen", name);
                p.Add("@GioiTinh", gender);
                p.Add("@NgaySinh", birth);
                p.Add("@TienLuong", salary);
                p.Add("@NgayBatDauLamViec", beginworkdate);
                p.Add("@SoNha", homenumber);
                p.Add("@Phuong", ward);
                p.Add("@TenDuong", street);
                p.Add("@ThanhPho", city);
                p.Add("@ViTriLamViec", positionwork);
                p.Add("@Anh", img);
                p.Add("@SoDienThoai", phone);

                using (IDbConnection connection = db.getConnection)
                {
                    check = connection.Execute("addGuard", p, commandType: CommandType.StoredProcedure);
                }
                if (check == 1) id = p.Get<int>("@MaNhanVien");
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

        internal static DataTable getEmployeeByID(int id)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getGuardByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNhanVien", id);
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

        internal static DataTable getEmployeeByName(string ten)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getGuardByName", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HoVaTen", ten);
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

        internal static DataTable getEmployeeByPhoneNum(string soDienThoai)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getGuardByPhoneNum", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
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

        internal static DataTable getAllGuard()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getAllGuard", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        internal static int addNewDoctor(string name, string hocVi, string chuyenMon, string gender, DateTime birth, int salary, DateTime beginworkdate, int homenumber, string ward, string city, string positionwork, byte[] img, string phone, string street)
        {
            Database db = new Database();
            int id = -1;
            try
            {
                int check;
                var p = new DynamicParameters();
                p.Add("@MaNhanVien", 0, DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@HoVaTen", name);
                p.Add("@HocVi", hocVi);
                p.Add("@ChuyenMon", chuyenMon);
                p.Add("@GioiTinh", gender);
                p.Add("@NgaySinh", birth);
                p.Add("@TienLuong", salary);
                p.Add("@NgayBatDauLamViec", beginworkdate);
                p.Add("@SoNha", homenumber);
                p.Add("@Phuong", ward);
                p.Add("@TenDuong", street);
                p.Add("@ThanhPho", city);
                p.Add("@ViTriLamViec", positionwork);
                p.Add("@Anh", img);
                p.Add("@SoDienThoai", phone);

                using (IDbConnection connection = db.getConnection)
                {
                    check = connection.Execute("addDoctor", p, commandType: CommandType.StoredProcedure);
                }

                if (check == 1)
                    id = p.Get<int>("@MaNhanVien");
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                // Ví dụ: Ghi log hoặc hiển thị thông báo cho người dùng
                throw ex;
            }

            return id;
        }


        internal static DataTable getDoctorByID(int id)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getDoctorByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNhanVien", id);
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

        internal static DataTable getDoctorByName(string ten)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getDoctorByName", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HoVaTen", ten);
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

        internal static DataTable getDoctorByPhoneNum(string soDienThoai)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getDoctorByPhoneNum", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
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


        internal static DataTable getAllDoctor()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getAllDoctor", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        internal static bool addNewNurse(string name, string hocVi, string chuyenMon, string gender, DateTime birth, int salary, DateTime beginworkdate, int homenumber, string ward, string city, string positionwork, byte[] img, string phone, string street)
        {
            Database db = new Database();
            try
            {
                int check;
                var p = new DynamicParameters();
                p.Add("@MaNhanVien", 0, DbType.Int32, direction: ParameterDirection.Output);
                p.Add("@HoVaTen", name);
                p.Add("@HocVi", hocVi);
                p.Add("@ChuyenMon", chuyenMon);
                p.Add("@GioiTinh", gender);
                p.Add("@NgaySinh", birth);
                p.Add("@TienLuong", salary);
                p.Add("@NgayBatDauLamViec", beginworkdate);
                p.Add("@SoNha", homenumber);
                p.Add("@Phuong", ward);
                p.Add("@TenDuong", street);
                p.Add("@ThanhPho", city);
                p.Add("@ViTriLamViec", positionwork);
                p.Add("@Anh", img);
                p.Add("@SoDienThoai", phone);

                using (IDbConnection connection = db.getConnection)
                {
                    check = connection.Execute("addNurse", p, commandType: CommandType.StoredProcedure);
                }
                if (check == 1) return true;
                else return false;
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        internal static DataTable getNurseByID(int id)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getNurseByID", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNhanVien", id);
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

        internal static DataTable getNurseByName(string ten)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getNurseByName", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HoVaTen", ten);
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

        internal static DataTable getNurseByPhoneNum(string soDienThoai)
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getNurseByPhoneNum", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SoDienThoai", soDienThoai);
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

        internal static DataTable getAllNurse()
        {
            DataTable dt = new DataTable();
            Database db = new Database();
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("getAllNurse", db.getConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public static bool removeDoctor(int id)
        {
            Database db = new Database();
            bool check = false;
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("removeDoctor", db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", id);
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

        public static bool removeNurse(int id)
        {
            Database db = new Database();
            bool check = false;
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("removeNurse", db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", id);
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

        public static bool removeGuard(int id)
        {
            Database db = new Database();
            bool check = false;
            try
            {
                db.openConnection();
                using (SqlCommand cmd = new SqlCommand("removeGuard", db.getConnection))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", id);
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
    }

    
}
