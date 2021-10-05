using LunarRover.Objects;
using LunarRover.Vehicles;

namespace LunarRover.Terrain
{
    public interface ITerrain
    {
        public void RemoveVehicle(IVehicle vehicle);

        public void AddVehicle(IVehicle vehicle, Position position);

        public Position? GetVehiclePosition(IVehicle vehicle);

        public bool AreValidCoordinates(Position position);
    }
}
