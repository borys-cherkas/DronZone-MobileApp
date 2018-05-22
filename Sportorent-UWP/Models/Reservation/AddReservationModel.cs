using System;

namespace DronZone_UWP.Models.Reservation
{
    public class AddReservationModel
    {
        public int AthleticFieldId { get; set; }

        public DateTime StartsAt { get; set; }

        public DateTime EndsAt { get; set; }
    }
}
