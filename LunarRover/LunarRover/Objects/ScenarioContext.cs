using LunarRover.Terrain;
using LunarRover.Vehicles;

namespace LunarRover.Objects
{
    public class ScenarioContext
    {
        public readonly ITerrain Terrain;

        public readonly IVehicle Vehicle;

        public readonly Position VehicleStartingPosition;

        public ScenarioContext(ITerrain terrain, IVehicle vehicle, Position startingPosition)
        {
            if (vehicle is null)
                throw new ArgumentNullException(nameof(vehicle));

            if (terrain is null)
                throw new ArgumentNullException(nameof(terrain));

            Vehicle = vehicle;
            Terrain = terrain;
            VehicleStartingPosition = startingPosition;
        }
    }
}
