using System.Collections.Generic;
using System.Linq;
using EightPuzzleSolver.EightPuzzle;
using EightPuzzleSolver.Search;
using EightPuzzleSolver.Search.Algorithms;
using Xunit;

namespace EightPuzzleSolver.Tests.EightPuzzle
{
    public class SearchTests
    {
        [Fact]
        public void SearchTest()
        {
            var problem = new EightPuzzleProblem(new Board(new byte[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 0, 7, 8 }
            }));

            var result = problem.CreateDefaultSolver().Search(problem).ToList();

            Assert.NotEmpty(result);
        }

        public static IEnumerable<object[]> OptimalTestData
        {
            get
            {
                yield return new object[]
                {
                    new Board(new byte[,]
                    {
                        {6, 4, 7},
                        {8, 5, 0},
                        {3, 2, 1}
                    }),
                    31
                };

                yield return new object[]
                {
                    new Board(new byte[,]
                    {
                        {4, 5, 0},
                        {1, 2, 3}
                    }),
                    21
                };

                yield return new object[]
                {
                    new Board(new byte[,]
                    {
                        {0, 7, 2, 1},
                        {4, 3, 6, 5}
                    }),
                    36
                };
            }
        }

        [Theory]
        [MemberData(nameof(OptimalTestData))]
        public void ShouldFindOptimal(Board board, int optimalSolution)
        {
            Assert.True(board.IsSolvable());

            var problem = new EightPuzzleProblem(board);

            var h = new ManhattanHeuristicFunction(Board.CreateGoalBoard(board.RowCount, board.ColumnCount));

            var algorithms = new ISearch<EightPuzzleState>[]
            {
                new AStarSearch<EightPuzzleState>(h), 
                new RecursiveBestFirstSearch<EightPuzzleState>(h), 
            };

            foreach (var algorithm in algorithms)
            {
                var result = algorithm.Search(problem).ToList();

                Assert.NotEmpty(result);
                Assert.Equal(optimalSolution, result.Count - 1);
            }
        }

        [Fact]
        public void RandomBoardSearchTest()
        {
            var sizes = new[]
            {
                new { RowCount = 2, ColumnCount = 2 },
                new { RowCount = 2, ColumnCount = 3 },
                new { RowCount = 3, ColumnCount = 2 },
                new { RowCount = 3, ColumnCount = 3 },
                new { RowCount = 4, ColumnCount = 2 },
                new { RowCount = 2, ColumnCount = 4 },
                //new { RowCount = 4, ColumnCount = 4 },
            };

            foreach (var size in sizes)
            {
                for (int i = 0; i < 3; i++)
                {
                    var problem = new EightPuzzleProblem(Board.GenerateSolvableBoard(size.RowCount, size.ColumnCount));

                    var result = problem.CreateDefaultSolver().Search(problem).ToList();

                    Assert.NotEmpty(result);
                }
            }

            foreach (var size in sizes.Take(3))
            {
                for (int i = 0; i < 3; i++)
                {
                    var problem = new EightPuzzleProblem(Board.GenerateSolvableBoard(size.RowCount, size.ColumnCount));

                    var result = new IterativeDeepeningSearch<EightPuzzleState>().Search(problem).ToList();

                    Assert.NotEmpty(result);
                }
            }
        }
    }
}
