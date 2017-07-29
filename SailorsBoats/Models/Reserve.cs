using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Models
{
    class Reserve
    {
        public int SailorId { get; set; }
        public int BoatId { get; set; }
        public DateTime Date { get; set; }
    }
}
