using System;
using System.Globalization;

namespace DronZone_UWP.Models.Reservation
{
    public class ReservationListItemModel
    {
        public int Id { get; set; }

        public string SportKindName { get; set; }

        public string AthleticFieldName { get; set; }

        public DateTime ReservationStartsAt { get; set; }
        public DateTime ReservationEndsAt { get; set; }

        public DateTime ReservationDate => ReservationStartsAt.Date;
        public string ReservationDateFormatted => ReservationStartsAt.Date.ToString("D");

        public string ReservationStartsTime => ReservationStartsAt.ToString("H:mm.F", CultureInfo.InvariantCulture);
        public string ReservationEndsTime => ReservationEndsAt.ToString("H:mm.F", CultureInfo.InvariantCulture);


        public string CurrentState { get; }
    }
}
