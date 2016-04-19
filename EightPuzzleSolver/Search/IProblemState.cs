using System.Collections.Generic;

namespace EightPuzzleSolver.Search
{
    public interface IProblemState<TProblemState> where TProblemState : IProblemState<TProblemState>
    {
        /// <summary>
        /// Cost of getting to this state from the previous state
        /// </summary>
        int Cost { get; }

        bool IsEqual(TProblemState other);

        /// <summary>
        /// Returns the states available from this state
        /// </summary>
        ISet<TProblemState> NextStates();
    }
}
