using DronZone_UWP.Enums;

namespace DronZone_UWP.Models.Reservation
{
    public class ReservationParticipantModel
    {
        public int ReservationId { get; set; }

        public int ParticipantId { get; set; }
        public string ParticipantFullName { get; set; }

        //public ReservationParticipantStatuses Status { get; set; }

        public int ReservationBillId { get; set; }
        public ReservationBillModel ReservationBill { get; set; }
    }
}
