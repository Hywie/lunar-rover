using LunarRover.Commands;
using LunarRover.Invokers;
using LunarRover.Objects;
using LunarRover.Terrain;
using LunarRover.Vehicles;
using Spectre.Console;

namespace LunarRover
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            RenderTitle();

            var scenarioContext = GetScenarioContext();
            if (scenarioContext is null)
                return;

            var invoker = new VehicleInvoker()
                .AddCommand<MoveRoverForwardCommand>("F", scenarioContext)
                .AddCommand("L", new RotateRoverCommand(scenarioContext, Rotation.Left))
                .AddCommand("R", new RotateRoverCommand(scenarioContext, Rotation.Right))
                .AddCommand<MoveRoverToStartCommand>("start", scenarioContext)
                .AddCommand<DisplayVehicleStatusCommand>("status", scenarioContext);

            var movementInstructions = AnsiConsole.Ask<string>("What's the movement instructions? > ")
                .ToCharArray()
                .Select(s => s.ToString())
                .ToList();

            movementInstructions.Insert(0, "start");
            movementInstructions.Add("status");

            AnsiConsole.WriteLine();
            AnsiConsole.Render(new Rule("Movements instructions").LeftAligned());
            AnsiConsole.WriteLine();
            invoker.RunCommandList(movementInstructions);
        }

        public static void RenderTitle()
        {
            AnsiConsole.Render(
                new FigletText("Mars Lunar Rover Coding Challenge")
                    .Centered()
                    .Color(Color.Green));

            AnsiConsole.Render(new Rule("Created by Hywie Martin"));

            AnsiConsole.WriteLine();
        }

        public static ScenarioContext? GetScenarioContext()
        {
            var initialPositionArgs = AnsiConsole.Ask<string>("What's the initial position? [grey](x, y, Compass Heading Letter)[/] > ").Split(',');

            if (!initialPositionArgs.Any())
            {
                AnsiConsole.Prompt(new TextPrompt<string>("[red]No valid initial position given [/]").AllowEmpty());
                return null;
            }

            if (!int.TryParse(initialPositionArgs[0].Trim(), result: out int xCoord))
            {
                AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid x coordinate given [/]").AllowEmpty());
                return null;
            }

            if (!int.TryParse(initialPositionArgs[1].Trim(), result: out int yCoord))
            {
                AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid y coordinate given [/]").AllowEmpty());
                return null;
            }

            CompassDirection direction;
            switch (initialPositionArgs[2].Trim().ToUpper())
            {
                case "N":
                    direction = CompassDirection.North;
                    break;
                case "E":
                    direction = CompassDirection.East;
                    break;
                case "S":
                    direction = CompassDirection.South;
                    break;
                case "W":
                    direction = CompassDirection.West;
                    break;
                default:
                    AnsiConsole.Prompt(new TextPrompt<string>("[red]Invalid direction given [/]").AllowEmpty());
                    return null;
            }


            return new ScenarioContext(
                new Crater(),
                new RoverVehicle { FacingDirection = direction },
                new Position
                {
                    xCoordinate = xCoord,
                    yCoordinate = yCoord
                }
            );
        }
    }
}