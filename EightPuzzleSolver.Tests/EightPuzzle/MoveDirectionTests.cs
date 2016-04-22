using EightPuzzleSolver.EightPuzzle;
using Xunit;

namespace EightPuzzleSolver.Tests.EightPuzzle
{
    public class MoveDirectionTests
    {
        [Fact]
        public void OppositeTest()
        {
            Assert.Equal(MoveDirection.Left.Opposite(), MoveDirection.Right);
            Assert.Equal(MoveDirection.Right.Opposite(), MoveDirection.Left);
            Assert.Equal(MoveDirection.Top.Opposite(), MoveDirection.Bottom);
            Assert.Equal(MoveDirection.Bottom.Opposite(), MoveDirection.Top);

            Assert.Equal(MoveDirection.Left.Opposite().Opposite(), MoveDirection.Left);
        }
    }
}
