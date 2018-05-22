using System;
using System.Collections.Generic;

namespace DronZone_UWP.Models.Reservation
{
    public class ReservationDetailedModel
    {
        public int Id { get; set; }

        public string SportName { get; set; }
        public string AthleticFieldName { get; set; }

        public int AthleticFieldId { get; set; }

        public DateTime ReservationStartsAt { get; set; }
        public DateTime ReservationEndsAt { get; set; }

        public ICollection<ReservationParticipantModel> ReservationParticipants { get; set; }

        public string CurrentState { get; set; }
    }
}
