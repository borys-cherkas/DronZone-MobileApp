using System;

namespace DronZone_UWP.Models.Devices
{
    public class DeviceListItemModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int? AthleticFieldId { get; set; }
    }
}