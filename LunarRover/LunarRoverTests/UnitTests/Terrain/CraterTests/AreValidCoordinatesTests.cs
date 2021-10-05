using FluentAssertions;
using LunarRover.Objects;
using LunarRover.Terrain;
using Xunit;

namespace LunarRoverTests.UnitTests.Terrain.CraterTests
{
    public class AreValidCoordinatesTests
    {
        [Theory(DisplayName = "Tests the boundary when validating coordinates ")]
        [InlineData(2, 1, 1, true)]
        [InlineData(2, 2, 1, false)]
        [InlineData(2, 1, 2, false)]
        [InlineData(2, 0, 0, true)]
        [InlineData(2, -1, 0, false)]
        [InlineData(2, 0, -1, false)]
        public void GridBoundaryTests(int gridAreaSize, int x, int y, bool expectedResult)
        {
            // act and assert
            new Crater(gridAreaSize)
                .AreValidCoordinates(new Position() { xCoordinate = x, yCoordinate = y })
                .Should().Be(expectedResult);
        }
    }
}
