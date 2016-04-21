using System.Collections.Generic;
using System.Linq;
using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.Tests.Search
{
    internal class DummyHeuristicFunction : IHeuristicFunction<DummyProblemState>
    {
        private readonly Dictionary<string, int> _dict;

        public DummyHeuristicFunction(Dictionary<string, int> dict)
        {
            _dict = dict;
        }

        public DummyHeuristicFunction()
        {
            _dict = new Dictionary<string, int>();
        }

        public double Calculate(DummyProblemState state)
        {
            if (_dict.Any())
                return _dict[state.Name];
            return 0;
        }
    }
}
