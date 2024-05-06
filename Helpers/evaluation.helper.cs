using NhaKhoaCuoiKy.dbs;
using NhaKhoaCuoiKy.Views.Employee;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using Guna.UI2.WinForms.Suite;
using Microsoft.VisualBasic.ApplicationServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Net;

namespace NhaKhoaCuoiKy.Helpers
{
    internal class Evaluation_Helper
    {
        public static bool insertEvaluation(int maNV, float diemDanhGia, string phanHoi,DateTime ngayDanhGia)
        {
            Database mydb = new Database();
            
            SqlCommand command = new SqlCommand("INSERT INTO DANHGIA(MaNhanVien,DiemDanhGia,PhanHoi,NgayDanhGia) VALUES (@mnv,@ddg,@phh,@ndg)", mydb.getConnection);     
            command.Parameters.AddWithValue("@ddg", SqlDbType.Float).Value = diemDanhGia;
            command.Parameters.AddWithValue("@mnv", SqlDbType.Int).Value = maNV;
            command.Parameters.AddWithValue("@phh", SqlDbType.VarChar).Value = phanHoi;
            command.Parameters.AddWithValue("@ndg", SqlDbType.DateTime).Value = ngayDanhGia;

            
            mydb.openConnection();

            try
            {
                if (command.ExecuteNonQuery() == 1)
                {
                    mydb.closeConnection();
                    return true;
                }
                else
                {
                    mydb.closeConnection();
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ghi log, hiển thị thông báo lỗi, v.v.)
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
                return false;
            }
            
        }
        public static DataTable selectEvaluationList(SqlCommand command)
        {
            Database mydb = new Database();
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
    }
}
