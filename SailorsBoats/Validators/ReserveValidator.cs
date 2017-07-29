using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Validators
{
    class ReserveValidator
    {
        public static bool IsSailorIdValid(string sailorId, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public static bool IsBoatIdValid(string boatId, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }

        public static bool IsDateValid(string date, out string errorMessage)
        {
            errorMessage = "";
            return true;
        }
    }
}
