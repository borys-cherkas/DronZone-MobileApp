using System;
using System.Globalization;
using DronZone_UWP.Enums;

namespace DronZone_UWP.Models.AthleticField
{
    public class AthleticFieldListItemModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        //public AthleticFieldStatuses Status { get; set; }

        public DateTime StartsWorkingAt { get; set; }
        public DateTime EndsWorkingAt { get; set; }

        public decimal PricePerParticipant { get; set; }

        public string StartsWorkingTime => StartsWorkingAt.ToString("H:mm.F", CultureInfo.InvariantCulture);
        public string EndsWorkingTime => EndsWorkingAt.ToString("H:mm.F", CultureInfo.InvariantCulture);
    }
}
