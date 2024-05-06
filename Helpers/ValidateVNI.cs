using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Helpers
{
    internal class ValidateVNI
    {
        public static bool validateNameVNI(string name)
        {
            List<char> specialCharacters = new List<char>
            {
                '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '+', '=',
                '{', '}', '[', ']', ';', ':', '"', '\'', ',', '.', '/', '?', '<', '>',
                '|', '\\', '~', '`', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
            };

            foreach (char x in name)
            {
                if (specialCharacters.Contains(x)) return false;
            }
            return true;
        }

        public static bool validateNumber(string phone)
        {
            return Regex.IsMatch(phone, "^[0-9]+$");
        }
    }
}
