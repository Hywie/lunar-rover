
using LunarRover.Objects;
using Spectre.Console;

namespace LunarRover.Commands
{
    public class MoveRoverForwardCommand : ICommand
    {
        private readonly ScenarioContext _context;

        public MoveRoverForwardCommand(ScenarioContext context)
        {
            _context = context;
        }

        public void Execute()
        {            
            var vehiclePosition = _context.Terrain.GetVehiclePosition(_context.Vehicle);

            if (!vehiclePosition.HasValue)
                throw new InvalidOperationException("Unable to find vehicle position on terrain");

            var newVehiclePosition = vehiclePosition.Value;

            // Calculate the new vehicle position
            switch (_context.Vehicle.FacingDirection)
            {
                case (CompassDirection.North):
                    newVehiclePosition.yCoordinate++;
                    break;

                case (CompassDirection.East):
                    newVehiclePosition.xCoordinate++;
                    break;

                case (CompassDirection.South):
                    newVehiclePosition.yCoordinate--;
                    break;

                case (CompassDirection.West):
                    newVehiclePosition.xCoordinate--;
                    break;
            }

            // If the position is invalid, we need to reset the vehicle
            if (!_context.Terrain.AreValidCoordinates(newVehiclePosition)) {
                _context.Vehicle.NumberOfScuffs++;
                AnsiConsole.MarkupLine($"Vehicle [red]was unable to move forward[/] and still at x:[blue]{ vehiclePosition.Value.xCoordinate}[/], y:[darkorange]{ vehiclePosition.Value.yCoordinate}[/], facing [yellow2]{_context.Vehicle.FacingDirection}[/]");
                return;
            }

            _context.Terrain.RemoveVehicle(_context.Vehicle);
            _context.Terrain.AddVehicle(_context.Vehicle, newVehiclePosition);
            AnsiConsole.MarkupLine($"Vehicle [green]has moved forward[/] and is now at x:[blue]{ newVehiclePosition.xCoordinate}[/], y:[darkorange]{ newVehiclePosition.yCoordinate}[/], facing [yellow2]{_context.Vehicle.FacingDirection}[/]");
        }
    }
}
