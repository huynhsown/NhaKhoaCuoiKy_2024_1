using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Helpers
{
    internal class Validate
    {
        public bool validateName(string name)
        {
            return Regex.IsMatch(name, "^[a-zA-Z\\s]+$");
        }

        public bool validateNumber(string phone)
        {
            return Regex.IsMatch(phone, "^[0-9]+$");
        }
    }
}
