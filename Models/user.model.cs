using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Models
{
    public class UserModel
    {
        public string username {  get; set; }
        public string password { get; set; }
        public int decentralization { get; set; }
        public int employeeID { get; set; }

        public UserModel(string username, string password, int decentralization, int employeeID)
        {
            this.username = username;
            this.password = password;
            this.decentralization = decentralization;
            this.employeeID = employeeID;
        }
    }
}
