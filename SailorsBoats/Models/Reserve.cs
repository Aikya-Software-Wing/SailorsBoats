using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailorsBoats.Models
{
    public class Reserve
    {
        public int Id { get; set; }
        public int SailorId { get; set; }
        public int BoatId { get; set; }
        public DateTime Date { get; set; }
    }
}
