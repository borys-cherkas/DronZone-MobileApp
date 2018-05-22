using System.Collections.Generic;
using System.Linq;
using DronZone_UWP.Models.Reservation;

namespace DronZone_UWP.Utils
{
    public static class SortingHelper
    {
        public static IEnumerable<ReservationListItemModel> SortReservations(
            this IEnumerable<ReservationListItemModel> reservationListItems)
        {
            return reservationListItems
                .OrderBy(x => x.ReservationStartsAt);
        }
    }
}