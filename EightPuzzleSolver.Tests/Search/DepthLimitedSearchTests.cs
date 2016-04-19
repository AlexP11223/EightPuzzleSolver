using System.Collections.Generic;
using System.Linq;
using EightPuzzleSolver.Search;
using Xunit;

namespace EightPuzzleSolver.Tests.Search
{
    public class DepthLimitedSearchTests
    {
        private readonly Dictionary<string, DummyProblemState> _states = new Dictionary<string, DummyProblemState>();

        private readonly DummyProblemState _goalState;

        public DepthLimitedSearchTests()
        {
            foreach (var name in new [] { "A", "B", "C", "D", "E", "F" })
            {
                _states[name] = new DummyProblemState(0, name);
            }
            _goalState = DummyProblemState.CreateGoalState();
            _states[_goalState.Name] = _goalState;
        }

        [Fact]
        public void ShouldFindGoal()
        {
            _states["A"].NextStatesList = new[] { _states["B"], _states["C"] };
            _states["B"].NextStatesList = new[] { _states["D"] };
            _states["C"].NextStatesList = new[] { _states["E"] };
            _states["E"].NextStatesList = new[] { _states["F"] };
            _states["F"].NextStatesList = new[] { _goalState };

            var problem = new DummyProblem(_states["A"]);

            var dls = new DepthLimitedSearch<DummyProblemState>(10);

            var result = dls.Search(problem).ToList();

            Assert.Equal(new [] { _states["A"], _states["C"], _states["E"], _states["F"], _goalState }, result);
            Assert.Equal(false, dls.IsCutoff);
        }

        [Fact]
        public void ShouldFail()
        {
            _states["A"].NextStatesList = new[] { _states["B"], _states["C"] };
            _states["B"].NextStatesList = new[] { _states["D"] };
            _states["C"].NextStatesList = new[] { _states["E"] };
            _states["E"].NextStatesList = new[] { _states["F"] };

            var problem = new DummyProblem(_states["A"]);

            var dls = new DepthLimitedSearch<DummyProblemState>(10);

            var result = dls.Search(problem).ToList();

            Assert.Empty(result);
            Assert.Equal(false, dls.IsCutoff);
        }

        [Fact]
        public void ShouldCutoff()
        {
            _states["A"].NextStatesList = new[] { _states["B"], _states["C"] };
            _states["B"].NextStatesList = new[] { _states["D"] };
            _states["C"].NextStatesList = new[] { _states["E"] };
            _states["E"].NextStatesList = new[] { _states["F"] };
            _states["F"].NextStatesList = new[] { _goalState };

            var problem = new DummyProblem(_states["A"]);

            var dls = new DepthLimitedSearch<DummyProblemState>(3);

            var result = dls.Search(problem).ToList();

            Assert.Empty(result);
            Assert.Equal(true, dls.IsCutoff);
        }
    }
}
