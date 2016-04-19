using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.Tests.Search
{
    internal class DummyProblem : Problem<DummyProblemState>
    {
        public DummyProblem(DummyProblemState initialState) : base(initialState)
        {
        }

        public override bool IsGoalState(DummyProblemState state)
        {
            return state.IsEqual(DummyProblemState.CreateGoalState());
        }
    }
}
