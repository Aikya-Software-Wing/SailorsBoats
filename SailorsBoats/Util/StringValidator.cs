using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Util
{
    class StringValidator
    {
        public static bool IsStringNotNullOrEmpty(string _string, string propertyName,
            out string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(_string))
            {
                errorMessage = propertyName + " can not be empty";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }
    }
}
