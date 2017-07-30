using SailorsBoats.Util;
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
            return IntegerValidator.IsIntegerValid(id, "id", out errorMessage);
        }

        public static bool IsNameValid(string name, out string errorMessage)
        {
            return StringValidator.IsStringNotNullOrEmpty(name, "Sailor Name", out errorMessage);
        }

        public static bool IsRatingValid(string rating, out string errorMessage)
        {
            return IntegerValidator.IsIntegerValidAndPostive(rating, "Rating", out errorMessage);
        }

        public static bool IsAgeValid(string age, out string errorMessage)
        {
            return IntegerValidator.IsIntegerValidAndPostive(age, "Age", out errorMessage);
        }
    }
}
