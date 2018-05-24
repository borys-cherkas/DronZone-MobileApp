namespace DronZone_UWP.Models.AreaFilters
{
    public class AddFilterDetailedModel
    {
        public string AreaId { get; set; }

        public int DroneType { get; set; }
        public double MaxAvailableWeigth { get; set; }
        public double MaxDroneWeigth { get; set; }
        public double MaxDroneSpeed { get; set; }
    }
}