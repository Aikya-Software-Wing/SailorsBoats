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
            errorMessage = "";
            return true;
        }

        public static bool IsNameValid(string name, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public static bool IsColorValid(string color, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}
