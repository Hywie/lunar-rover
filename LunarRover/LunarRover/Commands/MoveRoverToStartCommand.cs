
using LunarRover.Objects;
using Spectre.Console;

namespace LunarRover.Commands
{
    public class MoveRoverToStartCommand : ICommand
    {
        private readonly ScenarioContext _context;

        public MoveRoverToStartCommand(ScenarioContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            _context.Terrain.RemoveVehicle(_context.Vehicle);
            _context.Terrain.AddVehicle(_context.Vehicle, _context.VehicleStartingPosition);
            AnsiConsole.MarkupLine($"Vehicle [green]has spawned[/] at x:[blue]{ _context.VehicleStartingPosition.xCoordinate}[/], y:[darkorange]{ _context.VehicleStartingPosition.yCoordinate}[/], facing [yellow2]{_context.Vehicle.FacingDirection}[/]");
        }
    }
}
