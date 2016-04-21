using System.Linq;
using EightPuzzleSolver.EightPuzzle;
using EightPuzzleSolver.Search;
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
