using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronZone_UWP.Models.Area
{
    public class AreaDetailedModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        // public MapRectangle MapRectangle { get; set; }

        public string OwnerId { get; set; }

        public bool IsConfirmed { get; set; }

        // public ZoneSettings Settings { get; set; }
    }
}
