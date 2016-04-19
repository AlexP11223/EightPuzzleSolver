using System.Collections.Generic;

namespace EightPuzzleSolver.Search
{
    public interface ISearch<TProblemState> where TProblemState : IProblemState<TProblemState>
    {
        /// <summary>
        /// Returns a list of actions from the initial state to the goal ([root, s1, s2, ..., goal]).
        /// If the goal is not found returns empty list.
        /// </summary>
        IEnumerable<TProblemState> Search(Problem<TProblemState> problem);
    }
}
