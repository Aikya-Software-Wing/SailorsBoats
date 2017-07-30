using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Util
{
    class IntegerValidator
    {
        public static bool IsIntegerValid(string integer, string propertyName, out string errorMessage)
        {
            int _integer;

            if (!int.TryParse(integer, out _integer))
            {
                errorMessage = propertyName + " must be an integer";
                return false;
            }
            else
            {
                errorMessage = "";
                return true;
            }
        }

        public static bool IsIntegerValidAndPostive(string integer, string propertyName,
            out string errorMessage)
        {
            int _integer;

            if (!int.TryParse(integer, out _integer))
            {
                errorMessage = propertyName + " must be an integer";
                return false;
            }
            else
            {
                if (_integer < 0)
                {
                    errorMessage = propertyName + " must be positive";
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
}
