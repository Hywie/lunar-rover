using LunarRover.Objects;

namespace LunarRover.Vehicles
{
    public class RoverVehicle : IVehicle
    {
        public CompassDirection FacingDirection { get; set; }
        public int NumberOfScuffs { get; set; }
    }
}
