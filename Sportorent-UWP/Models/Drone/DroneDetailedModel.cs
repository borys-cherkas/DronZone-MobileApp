using System;
using DronZone_UWP.Enums;

namespace DronZone_UWP.Models.Drone
{
    public class DroneDetailedModel
    {
        public string Code { get; set; }

        public DateTime? AttachedDateTime { get; set; }

        public string OwnerId { get; set; }

        public DroneType Type { get; set; }

        public double MaxAvailableWeigth { get; set; }

        public double Weigth { get; set; }

        public double MaxSpeed { get; set; }
    }
}
