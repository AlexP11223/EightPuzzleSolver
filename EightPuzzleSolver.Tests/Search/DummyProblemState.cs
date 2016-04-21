using System;
using System.Collections.Generic;
using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.Tests.Search
{
    internal class DummyProblemState : IProblemState<DummyProblemState>
    {
        public static DummyProblemState CreateGoalState(int cost = 1)
        {
            return new DummyProblemState(cost, "Goal");
        }

        public DummyProblemState(int cost, string name = "")
        {
            Cost = cost;
            Name = name;
            if (String.IsNullOrEmpty(Name))
                Name = Guid.NewGuid().ToString();
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

        public bool Equals(DummyProblemState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DummyProblemState) obj);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static bool operator ==(DummyProblemState left, DummyProblemState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DummyProblemState left, DummyProblemState right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"Name: {Name}, Cost: {Cost}";
        }
    }
}
