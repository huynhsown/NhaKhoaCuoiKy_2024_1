using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Models
{
    public class EmployeeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public EmployeeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public string toString()
        {
            return $"{Id} {Name}";
        }
    }
}
