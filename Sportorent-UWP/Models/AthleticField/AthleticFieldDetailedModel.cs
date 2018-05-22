using System;
using System.Globalization;
using DronZone_UWP.Enums;

namespace DronZone_UWP.Models.AthleticField
{
    public class AthleticFieldDetailedModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public string Status { get; set; }

        public DateTime StartsWorkingAt { get; set; }
        public DateTime EndsWorkingAt { get; set; }

        public string Altitude { get; set; } = "";
        public string Longitude { get; set; } = "";

        public int KindOfSportId { get; set; }

        public decimal PricePerParticipant { get; set; }

        public string Position => $"{Altitude}; {Longitude};";
        public string WorkingHours => $"{StartsWorkingTime} - {EndsWorkingTime}";

        public string StartsWorkingTime => StartsWorkingAt.ToString("H:mm.F", CultureInfo.InvariantCulture);
        public string EndsWorkingTime => EndsWorkingAt.ToString("H:mm.F", CultureInfo.InvariantCulture);
    }
}
