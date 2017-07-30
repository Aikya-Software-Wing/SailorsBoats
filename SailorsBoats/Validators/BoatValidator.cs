using SailorsBoats.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Validators
{
    class BoatValidator
    {
        public static bool IsIdValid(string id, out string errorMessage)
        {
            return IntegerValidator.IsIntegerValid(id, "ID", out errorMessage);
        }

        public static bool IsNameValid(string name, out string errorMessage)
        {
            return StringValidator.IsStringNotNullOrEmpty(name, "Boat Name", out errorMessage);
        }

        public static bool IsColorValid(string color, out string errorMessage)
        {
            return StringValidator.IsStringNotNullOrEmpty(color, "Boat Color", out errorMessage);
        }
    }
}
