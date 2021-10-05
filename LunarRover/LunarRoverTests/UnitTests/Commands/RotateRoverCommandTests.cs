using FluentAssertions;
using LunarRover.Commands;
using LunarRover.Objects;
using LunarRover.Terrain;
using LunarRover.Vehicles;
using Moq;
using System;
using Xunit;

namespace LunarRoverTests.UnitTests.Commands
{
    public class RotateRoverCommandTests
    {
        [Fact(DisplayName = "When the vehicle isn't on the terrain, an exception should be thrown")]
        public void When_VehicleIsNotOnTerrain_Should_ThrowEx()
        {
            // arrange
            var terrainMock = new Mock<ITerrain>();
            var vehicleMock = new Mock<IVehicle>();
            terrainMock.Setup(t => t.GetVehiclePosition(vehicleMock.Object)).Returns(null as Position?);

            // act
            var act = () => new RotateRoverCommand(
                    new ScenarioContext(
                        terrainMock.Object,
                        vehicleMock.Object,
                        new Position()
                    ), Rotation.Left
                ).Execute();

            // assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory(DisplayName = "When the vehicle is rotated, it should rotate relative to the direction it is facing")]
        [InlineData(CompassDirection.North, CompassDirection.East, Rotation.Right)]
        [InlineData(CompassDirection.East, CompassDirection.South, Rotation.Right)]
        [InlineData(CompassDirection.South, CompassDirection.West, Rotation.Right)]
        [InlineData(CompassDirection.West, CompassDirection.North, Rotation.Right)]
        [InlineData(CompassDirection.North, CompassDirection.West, Rotation.Left)]
        [InlineData(CompassDirection.West, CompassDirection.South, Rotation.Left)]
        [InlineData(CompassDirection.South, CompassDirection.East, Rotation.Left)]
        [InlineData(CompassDirection.East, CompassDirection.North, Rotation.Left)]
        public void When_VehicleIsRotatedRelative_Should_BeFacingCorrectDirection(CompassDirection startDirection, CompassDirection expectedFacing, Rotation rotation)
        {
            // arrange
            var terrainMock = new Mock<ITerrain>();
            var vehicleMock = new Mock<IVehicle>();
            terrainMock.Setup(t => t.GetVehiclePosition(vehicleMock.Object)).Returns(new Position());
            vehicleMock.SetupGet(v => v.FacingDirection).Returns(startDirection);

            // act
            new RotateRoverCommand(
                new ScenarioContext(
                    terrainMock.Object,
                    vehicleMock.Object,
                    new Position()
                ), rotation
            ).Execute();

            // assert
            vehicleMock.VerifySet(v => v.FacingDirection = expectedFacing);
        }
    }
}
