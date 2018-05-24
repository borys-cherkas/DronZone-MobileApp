using System;
using System.Linq;
using DronZone_UWP.Enums;

namespace DronZone_UWP.Models.AreaFilters
{
    public class AreaFilterDetailedModel
    {
        public string Id { get; set; }

        public int DroneType { get; set; }

        public string DroneTypePresentation =>
            Enum.GetNames(typeof(DroneType)).ToList()[DroneType];

        public double MaxAvailableWeigth { get; set; }
        public double MaxDroneWeigth { get; set; }
        public double MaxDroneSpeed { get; set; }

        public string AreaId { get; set; }
    }
}