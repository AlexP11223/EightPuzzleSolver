using EightPuzzleSolver.EightPuzzle;
using Xunit;

namespace EightPuzzleSolver.Tests.EightPuzzle
{
    public class PositionTests
    {
        [Fact]
        public void EqualsTest()
        {
            var pos1 = new Position(1, 2);
            var pos2 = new Position(1, 2);
            var pos3 = new Position(2, 2);
            var pos4 = new Position(1, 3);

            Assert.Equal(pos1, pos2);
            Assert.True(pos1 == pos2);
            Assert.NotEqual(pos1, pos3);
            Assert.NotEqual(pos1, pos4);
            Assert.True(pos1 != pos4);
        }
    }
}
