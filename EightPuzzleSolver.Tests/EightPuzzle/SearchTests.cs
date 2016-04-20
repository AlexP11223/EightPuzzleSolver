using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightPuzzleSolver.EightPuzzle;
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
                { 0, 7, 8 },
            }), new Board(new byte[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 },
                { 7, 8, 0 },
            }));

            var result = problem.CreateDefaultSolver().Search(problem).ToList();

            Assert.NotEmpty(result);
        }
    }
}
