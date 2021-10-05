using FluentAssertions;
using LunarRover.Objects;
using LunarRover.Terrain;
using LunarRover.Vehicles;
using Moq;
using System;
using Xunit;

namespace LunarRoverTests.UnitTests.Objects
{
    public class ScenarioContextTests
    {
        [Fact(DisplayName = "When given a null vehicle, throw a argument null exception")]
        public void When_NullVehicle_Should_ThrowNullEx()
        {
            // act
            var act = () => new ScenarioContext(
                new Mock<ITerrain>().Object,
                null,
                new Position()
            );

            // assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact(DisplayName = "When given a null terrain, throw a argument null exception")]
        public void When_NullTerrain_Should_ThrowNullEx()
        {
            // act
            var act = () => new ScenarioContext(
                null,
                new Mock<IVehicle>().Object,
                new Position()
            );

            // assert
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
