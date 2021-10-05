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
    public class MoveRoverForwardCommandTests
    {
        [Fact(DisplayName = "When the vehicle isn't on the terrain, an exception should be thrown")]
        public void When_VehicleIsNotOnTerrain_Should_ThrowEx()
        {
            // arrange
            var terrainMock = new Mock<ITerrain>();
            var vehicleMock = new Mock<IVehicle>();
            terrainMock.Setup(t => t.GetVehiclePosition(vehicleMock.Object)).Returns(null as Position?);

            // act
            var act = () => new MoveRoverForwardCommand(new ScenarioContext(
                terrainMock.Object,
                vehicleMock.Object,
                new Position()
            )).Execute();

            // assert
            act.Should().Throw<InvalidOperationException>();
        }

        [Theory(DisplayName = "When the vehicle has an empty grid space in front, it should move into that space")]
        [InlineData(0, 0, 0, 1, CompassDirection.North)]
        [InlineData(0, 0, 1, 0, CompassDirection.East)]
        [InlineData(0, 1, 0, 0, CompassDirection.South)]
        [InlineData(1, 0, 0, 0, CompassDirection.West)]
        public void When_VehicleCanMoveForward_Should_MoveForwardRelativeToDirection(int currentX, int currentY, int expectedX, int expectedY, CompassDirection facing)
        {
            // arrange
            var terrainMock = new Mock<ITerrain>();
            var vehicleMock = new Mock<IVehicle>();
            var currentPosition = new Position() { xCoordinate = currentX, yCoordinate = currentY };
            var expectedPosition = new Position() { xCoordinate = expectedX, yCoordinate = expectedY };

            terrainMock.Setup(t => t.GetVehiclePosition(vehicleMock.Object)).Returns(currentPosition);
            terrainMock.Setup(t => t.AreValidCoordinates(expectedPosition)).Returns(true);
            vehicleMock.SetupGet(v => v.FacingDirection).Returns(facing);

            // act
            new MoveRoverForwardCommand(new ScenarioContext(
                terrainMock.Object,
                vehicleMock.Object,
                new Position()
            )).Execute();

            // assert
            terrainMock.Verify(t => t.AddVehicle(vehicleMock.Object, expectedPosition), Times.Once);
        }

        [Fact(DisplayName = "When the vehicle does not have an empty grid space in front, it should not move but increment the vehicle scuff instead")]
        public void When_VehicleCannotMoveForward_Should_AddScuff()
        {
            // arrange
            var terrainMock = new Mock<ITerrain>();
            var vehicleMock = new Mock<IVehicle>();

            terrainMock.Setup(t => t.GetVehiclePosition(vehicleMock.Object)).Returns(new Position());
            terrainMock.Setup(t => t.AreValidCoordinates(It.IsAny<Position>())).Returns(false);
            vehicleMock.SetupGet(v => v.NumberOfScuffs).Returns(0);

            // act
            new MoveRoverForwardCommand(new ScenarioContext(
                terrainMock.Object,
                vehicleMock.Object,
                new Position()
            )).Execute();

            // assert
            terrainMock.Verify(t => t.AddVehicle(vehicleMock.Object, It.IsAny<Position>()), Times.Never);
            vehicleMock.VerifySet(v => v.NumberOfScuffs = 1);
        }
    }
}
