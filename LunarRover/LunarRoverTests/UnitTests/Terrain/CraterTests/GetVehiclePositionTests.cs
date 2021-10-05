using FluentAssertions;
using LunarRover.Objects;
using LunarRover.Terrain;
using LunarRover.Vehicles;
using Moq;
using System;
using Xunit;

namespace LunarRoverTests.UnitTests.Terrain.CraterTests
{
    public class GetVehiclePositionTests
    {
        [Fact(DisplayName = "When vehicle is on the terrain, expect the position to be found")]
        public void When_VehicleOnTerrain_Should_FindPosition()
        {
            // arrange
            var crater = new Crater(gridAreaSize: 1);
            var vehicleMock = new Mock<IVehicle>();
            var position = new Position() { xCoordinate = 0, yCoordinate = 0 };
            crater.AddVehicle(vehicleMock.Object, position);

            // act
            crater.GetVehiclePosition(vehicleMock.Object).Should().Be(position);
        }

        [Fact(DisplayName = "When vehicle is not on the terrain, expect null to be returned")]
        public void When_VehicleNotOnTerrain_Should_FindPosition()
        {
            // act and assert
            new Crater().GetVehiclePosition(new Mock<IVehicle>().Object).Should().BeNull();
        }

        [Fact(DisplayName = "When null is given as the vehicle, expect a null arg exception to be thrown")]
        public void When_VehicleIsNull_Should_ThrowArgEx()
        {
            // act
            var act = () => new Crater().GetVehiclePosition(null);
            
            //assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
