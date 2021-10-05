using FluentAssertions;
using LunarRover.Terrain;
using LunarRover.Vehicles;
using Moq;
using Xunit;
using System;
using LunarRover.Objects;

namespace LunarRoverTests.UnitTests.Terrain.CraterTests
{
    public class RemoveVehicleTests
    {
        [Fact(DisplayName = "When vehicle not found, expect no exception to be thrown")]
        public void When_VehicleNotFound_Should_DoNothing()
        {
            // arrange
            var crater = new Crater(gridAreaSize: 1);
            var vehicleMock = new Mock<IVehicle>();
            crater.AddVehicle(vehicleMock.Object, new Position() { xCoordinate = 0, yCoordinate = 0 });

            // act
            var act = () => crater.RemoveVehicle(new Mock<IVehicle>().Object);

            // assert
            act.Should().NotThrow("failing to find a vehicle is a valid flow.");
            crater.GetVehiclePosition(vehicleMock.Object).Should().NotBeNull(" vehicle should not have been removed.");
        }

        [Fact(DisplayName = "When vehicle found, expect it to be removed")]
        public void When_VehicleFound_Should_RemoveVehicle()
        {
            // arrange
            var crater = new Crater(gridAreaSize: 1);
            var vehicleMock = new Mock<IVehicle>();
            var vehiclePosition = new Position() { xCoordinate = 0, yCoordinate = 0 };
            crater.AddVehicle(vehicleMock.Object, vehiclePosition);

            // act
            var act = () => crater.RemoveVehicle(vehicleMock.Object);

            // assert
            act.Should().NotThrow();
            crater.GetVehiclePosition(vehicleMock.Object).Should().BeNull();
        }

        [Fact(DisplayName = "When vehicle given is null, expect a null arg exception to be thrown")]
        public void When_VehicleGivenIsNull_Should_ThrowNullArgEx()
        {
            // act
            var act = () => new Crater().RemoveVehicle(null);

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

    }
}
