using EightPuzzleSolver.Search.Algorithms;

namespace EightPuzzleSolver.Search
{
    public abstract class Problem<TProblemState> where TProblemState : IProblemState<TProblemState>
    {
        protected Problem(TProblemState initialState)
        {
            InitialState = initialState;
        }

        public TProblemState InitialState { get; }

        /// <summary>
        /// Checks if the state is the goal state
        /// </summary>
        public abstract bool IsGoalState(TProblemState state);

        public virtual ISearch<TProblemState> CreateDefaultSolver()
        {
            return new IterativeDeepeningSearch<TProblemState>();
        }
    }
}
