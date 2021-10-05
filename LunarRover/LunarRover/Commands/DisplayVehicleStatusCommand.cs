using LunarRover.Objects;
using Spectre.Console;

namespace LunarRover.Commands
{
    public class DisplayVehicleStatusCommand : ICommand
    {
        private readonly ScenarioContext _context;

        public DisplayVehicleStatusCommand(ScenarioContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            var vehiclePosition = _context.Terrain.GetVehiclePosition(_context.Vehicle).GetValueOrDefault();

            AnsiConsole.WriteLine();
            AnsiConsole.Render(new Rule("Vehicle Position").LeftAligned());
            AnsiConsole.WriteLine();

            AnsiConsole.MarkupLine($"Vehicle [green]X Position[/]: {vehiclePosition.xCoordinate}");
            AnsiConsole.MarkupLine($"Vehicle [green]Y Position[/]: {vehiclePosition.yCoordinate}");
            AnsiConsole.MarkupLine($"Vehicle [green]Direction[/]: {_context.Vehicle.FacingDirection}");
            AnsiConsole.MarkupLine($"[blue]Number of Scuffs[/]: {_context.Vehicle.NumberOfScuffs}");
        }
    }
}
