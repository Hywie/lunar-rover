using LunarRover.Objects;

namespace LunarRover.Vehicles
{
    public interface IVehicle
    {
        public CompassDirection FacingDirection { get; set; }

        public int NumberOfScuffs { get; set; }

    }
}
