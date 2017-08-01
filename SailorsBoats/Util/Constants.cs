using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Util
{
    public class Constants
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SailorBoatsDatabase"].ConnectionString;
            }
        }

        public static string ValidationMessageHeader
        {
            get
            {
                return "The following error(s) prevented the data from being saved";
            }
        }
    }
}
