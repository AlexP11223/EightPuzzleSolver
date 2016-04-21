using EightPuzzleSolver.EightPuzzle;
using Xunit;

namespace EightPuzzleSolver.Tests.EightPuzzle
{
    public class ManhattanHeuristicFunctionTests
    {
        [Fact]
        public void ManhattanHeuristicFunctionTest()
        {
            var board = new Board(new byte[,]
            {
                { 8, 1, 3 },
                { 4, 0, 2 },
                { 7, 6, 5 }
            });

            var h = new ManhattanHeuristicFunction(Board.CreateGoalBoard(board.RowCount, board.ColumnCount));

            Assert.Equal(10, h.Calculate(new EightPuzzleState(board)));
        }
    }
}
