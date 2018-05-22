using System.Collections.Generic;

namespace DronZone_UWP.Models.Reservation
{
    public class ReservationListModel
    {
        public ICollection<ReservationListItemModel> ReservationList { get; set; }
    }
}
