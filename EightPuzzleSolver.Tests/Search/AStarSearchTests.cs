using System.Collections.Generic;
using System.Linq;
using EightPuzzleSolver.Search;
using Xunit;

namespace EightPuzzleSolver.Tests.Search
{
    public class AStarSearchTests
    {
        [Fact]
        public void ShouldFindOptimalPath()
        {
            var states = new Dictionary<string, DummyProblemState>
            {
                ["A"] = new DummyProblemState(0, "A"),
                ["Goal from A"] = DummyProblemState.CreateGoalState(42),
                ["B from A"] = new DummyProblemState(2, "B"),
                ["H from A"] = new DummyProblemState(1, "B"),
                ["E from A"] = new DummyProblemState(3, "E"),
                ["C from B"] = new DummyProblemState(4, "C"),
                ["D from C"] = new DummyProblemState(3, "D"),
                ["Goal from D"] = DummyProblemState.CreateGoalState(4),
                ["F from E"] = new DummyProblemState(1, "F"),
                ["Goal from F"] = DummyProblemState.CreateGoalState(2)
            };

            SetPaths(states);

            var problem = new DummyProblem(states["A"]);

            var astars = new[]
            {
                new AStarSearch<DummyProblemState>(new DummyHeuristicFunction()),
                new AStarSearch<DummyProblemState>(new DummyHeuristicFunction(new Dictionary<string, int>
                {
                    ["A"] = 8,
                    ["B"] = 6,
                    ["C"] = 5,
                    ["D"] = 4,
                    ["E"] = 7,
                    ["F"] = 2,
                    [DummyProblemState.CreateGoalState().Name] = 0,
                }))
            };

            foreach (var astar in astars)
            {
                var result = astar.Search(problem).ToList();

                Assert.Equal(6, result.Sum(s => s.Cost));
                Assert.Equal(new[] { states["A"], states["E from A"], states["F from E"], states["Goal from F"] }, result);
            }
        }

        [Fact]
        public void ShouldFail()
        {
            var states = new Dictionary<string, DummyProblemState>
            {
                ["A"] = new DummyProblemState(0, "A"),
                ["B from A"] = new DummyProblemState(2, "B"),
                ["E from A"] = new DummyProblemState(3, "E"),
                ["C from B"] = new DummyProblemState(4, "C"),
                ["D from C"] = new DummyProblemState(3, "D")
            };

            SetPaths(states);

            var problem = new DummyProblem(states["A"]);

            var astar = new AStarSearch<DummyProblemState>(new DummyHeuristicFunction());

            var result = astar.Search(problem).ToList();

            Assert.Empty(result);
        }

        private static void SetPaths(Dictionary<string, DummyProblemState> states)
        {
            foreach (var item in states)
            {
                item.Value.NextStatesList =
                    states.Where(it => it.Key.EndsWith("from " + item.Value.Name)).Select(it => it.Value).ToList();
            }
        }
    }
}
