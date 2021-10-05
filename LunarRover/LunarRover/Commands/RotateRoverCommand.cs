
using LunarRover.Objects;
using Spectre.Console;

namespace LunarRover.Commands
{
    public class RotateRoverCommand : ICommand
    {
        private readonly ScenarioContext _context;
        private readonly Rotation _rotation;

        public RotateRoverCommand(ScenarioContext context, Rotation rotation)
        {
            _context = context;
            _rotation = rotation;
        }

        public void Execute()
        {
            var vehiclePosition = _context.Terrain.GetVehiclePosition(_context.Vehicle);
            if (!vehiclePosition.HasValue)
                throw new InvalidOperationException("Unable to find vehicle position on terrain");

            var newFacingDirectionValue = ((int)_context.Vehicle.FacingDirection) + ((int)_rotation);

            if (newFacingDirectionValue < 0)
                _context.Vehicle.FacingDirection = CompassDirection.West;
            else if (newFacingDirectionValue > 3)
                _context.Vehicle.FacingDirection = CompassDirection.North;
            else
                _context.Vehicle.FacingDirection = (CompassDirection) newFacingDirectionValue;

            AnsiConsole.MarkupLine($"Vehicle has turned [green]{_rotation}[/]. Now facing [yellow2]{_context.Vehicle.FacingDirection}[/], x:[blue]{ vehiclePosition.Value.xCoordinate}[/], y:[darkorange]{ vehiclePosition.Value.yCoordinate}[/].");
        }
    }
}
