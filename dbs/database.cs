using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace NhaKhoaCuoiKy.dbs
{
    internal class Database
    {
        string strCon = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NHAKHOA;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection sqlCon;
        public Database()
        {
            try
            {
                if (sqlCon == null)
                {
                    sqlCon = new SqlConnection(strCon);

                }
            }
            catch
            {
                throw;
            }
            /*            SqlConnectionStringBuilder con = new SqlConnectionStringBuilder();
                        con.DataSource = "22110406.database.windows.net";
                        con.UserID = "sondang";
                        con.Password = "son22110406@";
                        con.InitialCatalog = "sonhuynh";
                        try
                        {
                            if(sqlCon == null)
                            {
                                sqlCon = new SqlConnection(con.ConnectionString);
                            }
                        }
                        catch(Exception ex)
                        {
                            throw;
                        }*/
        }

        public SqlConnection getConnection
        {
            get { return sqlCon; }
        }

        public void openConnection() {
            if (sqlCon.State == System.Data.ConnectionState.Closed)
            {
                sqlCon.Open();
            }
        }

        public void closeConnection() {

            if (sqlCon.State == System.Data.ConnectionState.Open)
            {
                sqlCon.Close();
            }
        } 
    }
}
