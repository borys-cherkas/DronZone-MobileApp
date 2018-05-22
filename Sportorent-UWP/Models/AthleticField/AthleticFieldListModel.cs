using System.Collections.Generic;

namespace DronZone_UWP.Models.AthleticField
{
    public class AthleticFieldListModel
    {
        public string KindOfSportName { get; set; }

        public ICollection<AthleticFieldListItemModel> AthleticFieldList { get; set; }
    }
}
