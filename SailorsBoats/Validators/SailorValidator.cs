using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Validators
{
    public class SailorValidator
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

        public static bool IsRatingValid(string rating, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public static bool IsAgeValid(string age, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}
