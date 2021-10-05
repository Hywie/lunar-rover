using LunarRover.Objects;
using LunarRover.Vehicles;

namespace LunarRover.Terrain
{
    public class Crater : ITerrain
    {
        public readonly int GridArea;
        private IVehicle[,] _grid;

        public Crater(int gridAreaSize = 5)
        {
            GridArea = gridAreaSize;

            // _grid[x,y]
            _grid = new IVehicle[GridArea, GridArea];
        }

        public void RemoveVehicle(IVehicle vehicle)
        {
            if (vehicle is null)
                throw new ArgumentNullException(nameof(vehicle));

            var position = GetVehiclePosition(vehicle);

            if (!position.HasValue)
                return;

            _grid.SetValue(null, position.GetValueOrDefault().xCoordinate, position.GetValueOrDefault().yCoordinate);
        }

        public void AddVehicle(IVehicle vehicle, Position position)
        {
            if (vehicle is null)
                throw new ArgumentNullException(nameof(vehicle));

            _grid[position.xCoordinate, position.yCoordinate] = vehicle;
        }

        public Position? GetVehiclePosition(IVehicle vehicle)
        {
            if (vehicle is null)
                throw new ArgumentNullException(nameof(vehicle));

            for (int i = 0; i < _grid.GetLength(0); i++)
            {
                for (int j = 0; j < _grid.GetLength(1); j++)
                {
                    if (vehicle == _grid[i, j])
                        return new Position
                        {
                            xCoordinate = i,
                            yCoordinate = j
                        };
                }
            }

            return null;
        }

        public bool AreValidCoordinates(Position position)
        {
            return position.xCoordinate < GridArea && position.yCoordinate < GridArea && position.xCoordinate >= 0 && position.yCoordinate >= 0;
        }
    }
}
