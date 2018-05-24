using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronZone_UWP.Models.Area
{
    public class MapRectangle
    {
        public int Id { get; set; }

        public double West { get; set; }
        public double East { get; set; }
        public double South { get; set; }
        public double North { get; set; }

        public string ZoneId { get; set; }
    }
}
