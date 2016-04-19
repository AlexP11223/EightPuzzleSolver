using System.Collections.Generic;
using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.Tests.Search
{
    internal class DummyProblemState : IProblemState<DummyProblemState>
    {
        public static DummyProblemState CreateGoalState()
        {
            return new DummyProblemState(1, "Goal");
        }

        public DummyProblemState(int cost, string name = "")
        {
            Cost = cost;
            Name = name;
        }

        public DummyProblemState(int cost, IEnumerable<DummyProblemState> nextStatesList, string name = "")
            : this(cost, name)
        {
            NextStatesList = nextStatesList;
        }

        public string Name { get; }

        public int Cost { get; }

        public IEnumerable<DummyProblemState> NextStatesList { get; set; } = new DummyProblemState[0];

        public bool IsEqual(DummyProblemState other)
        {
            return other.Name == this.Name;
        }

        public ISet<DummyProblemState> NextStates()
        {
            return new HashSet<DummyProblemState>(NextStatesList);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Cost: {Cost}";
        }
    }
}
