using System.Collections.Generic;
using EightPuzzleSolver.EightPuzzle;
using Xunit;

namespace EightPuzzleSolver.Tests.EightPuzzle
{
    public class ManhattanHeuristicFunctionTests
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                yield return new object[]
                {
                    new Board(new byte[,]
                    {
                        { 8, 1, 3 },
                        { 4, 0, 2 },
                        { 7, 6, 5 }
                    }),
                    10
                };

                yield return new object[]
                {
                    new Board(new byte[,]
                    {
                        { 4, 6, 3, 8 },
                        { 7, 12, 9, 14 },
                        { 15, 13, 1, 5 },
                        { 2, 10, 11, 0 },
                    }),
                    36
                };
            }
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void ManhattanHeuristicFunctionTest(Board board, int expectedResult)
        {
            var h = new ManhattanHeuristicFunction(Board.CreateGoalBoard(board.RowCount, board.ColumnCount));

            Assert.Equal(expectedResult, h.Calculate(new EightPuzzleState(board)));
        }
    }
}
