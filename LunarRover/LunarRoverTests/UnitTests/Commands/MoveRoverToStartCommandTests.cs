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
    public class MoveRoverToStartCommandTests
    {
        [Fact(DisplayName = "When a valid context us used, the vehicle should be moved added to the terrain")]
        public void When_ContextIsValid_Should_MoveVehicleToInitialPosition()
        {
            // arrange
            var terrainMock = new Mock<ITerrain>();
            var vehicleMock = new Mock<IVehicle>();
            var position = new Position();

            // act
            new MoveRoverToStartCommand(new ScenarioContext(
                terrainMock.Object,
                vehicleMock.Object,
                position
            )).Execute();

            // arrange
            terrainMock.Verify(t => t.RemoveVehicle(vehicleMock.Object), Times.Once);
            terrainMock.Verify(t => t.AddVehicle(vehicleMock.Object, position), Times.Once);
        }
    }
}
