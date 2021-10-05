using FluentAssertions;
using LunarRover.Objects;
using LunarRover.Terrain;
using System;
using Xunit;

namespace LunarRoverTests.UnitTests.Terrain.CraterTests
{
    // Other unit tests already verify this method adds a vehicle, the following just cover edge cases
    public class AddVehicle
    {
        [Fact(DisplayName = "When null is given as the vehicle, expect a null arg exception to be thrown")]
        public void When_VehicleIsNull_Should_ThrowArgEx()
        {
            // act
            var act = () => new Crater().AddVehicle(null, new Position());

            //assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
