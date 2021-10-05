using LunarRover.Commands;
using LunarRover.Invokers;
using System.Collections.Generic;
using LunarRover.Objects;
using Xunit;
using LunarRover.Terrain;
using LunarRover.Vehicles;
using FluentAssertions;

namespace LunarRoverTests.IntegrationTests
{
    public class LunarRoverScenario
    {
        public static IEnumerable<object[]> Scenarios =>
        new List<object[]>
        {
            new object[] { 0, 2, CompassDirection.East, new List<string> { "start", "F", "L", "F", "R", "F", "F", "F", "R", "F", "F", "R", "R" }, 4, 1, CompassDirection.North, 0 },
            new object[] { 4, 4, CompassDirection.South, new List<string> { "start", "L", "F", "L", "L", "F", "F", "L", "F", "F", "F", "R", "F", "F" }, 0, 1, CompassDirection.West, 1 },
            new object[] { 2, 2, CompassDirection.West, new List<string> { "start", "F", "L", "F", "L", "F", "L", "F", "R", "F", "R", "F", "R", "F", "R", "F" }, 2, 2, CompassDirection.North, 0 },
            new object[] { 1, 3, CompassDirection.North, new List<string> { "start", "F", "F", "L", "F", "F", "L", "F", "F", "F", "F", "F" }, 0, 0, CompassDirection.South, 3 }
        };


        [Theory(DisplayName = "Scenarios : Run the given 4 scenarios")]
        [MemberData(nameof(Scenarios))]
        public void When_AddingCommandUsingTemplate_ShouldAddCommand(int xStart, int yStart, CompassDirection facing, IList<string> listOfCommands, int xExpected, int yExpected, CompassDirection expectedFacing, int numberOfExpectedScuffs)
        {
            // arrange
            var context = new ScenarioContext(
                new Crater(),
                new RoverVehicle { FacingDirection = facing },
                new Position
                {
                    xCoordinate = xStart,
                    yCoordinate = yStart
                }
            );

            var invoker = new VehicleInvoker()
                .AddCommand<MoveRoverForwardCommand>("F", context)
                .AddCommand("L", new RotateRoverCommand(context, Rotation.Left))
                .AddCommand("R", new RotateRoverCommand(context, Rotation.Right))
                .AddCommand<MoveRoverToStartCommand>("start", context);

            // act
            invoker.RunCommandList(listOfCommands);

            // assert
            var endPosition = context.Terrain.GetVehiclePosition(context.Vehicle);
            endPosition.Value.Should().NotBeNull();
            endPosition.Value.xCoordinate.Should().Be(xExpected);
            endPosition.Value.yCoordinate.Should().Be(yExpected);
            context.Vehicle.FacingDirection.Should().Be(expectedFacing);
            context.Vehicle.NumberOfScuffs.Should().Be(numberOfExpectedScuffs);
        }
    }
}

