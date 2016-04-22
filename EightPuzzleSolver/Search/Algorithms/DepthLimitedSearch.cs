using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace EightPuzzleSolver.Search.Algorithms
{
    public class DepthLimitedSearch<TProblemState> : ISearch<TProblemState>
                                        where TProblemState : IProblemState<TProblemState>
    {
        private bool _isCutoff;

        public DepthLimitedSearch(int limit)
        {
            Limit = limit;
        }

        public int Limit { get; }

        /// <summary>
        /// True when search failed because the limit is reached
        /// </summary>
        public bool IsCutoff => _isCutoff;

        /// <summary>
        /// Returns a list of actions from the initial state to the goal ([root, s1, s2, ..., goal]).
        /// If the goal is not found returns empty list, also sets IsCutoff to true if it was because of the limit.
        /// </summary>
        public IEnumerable<TProblemState> Search(Problem<TProblemState> problem, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RecursiveDls(new Node<TProblemState>(problem.InitialState), problem, Limit, out _isCutoff, cancellationToken);
        }

        private IEnumerable<TProblemState> RecursiveDls(Node<TProblemState> node, Problem<TProblemState> problem,
            int limit, out bool isCutoff,
            CancellationToken cancellationToken)
        {
            isCutoff = false;

            cancellationToken.ThrowIfCancellationRequested();

            if (problem.IsGoalState(node.State))
            {
                return node.PathFromRootStates();
            }
            else if (limit == 0)
            {
                isCutoff = true;
                return EmptyResult();
            }
            else
            {
                bool cutoffOccurred = false;

                foreach (var child in node.ExpandNode())
                {
                    bool isChildCutoff;

                    var result = RecursiveDls(child, problem, limit - 1, out isChildCutoff, cancellationToken);

                    if (isChildCutoff)
                    {
                        cutoffOccurred = true;
                    }
                    else if (result.Any()) // success
                    {
                        return result;
                    }
                }

                isCutoff = cutoffOccurred;
                return EmptyResult();
            }
        }

        private IEnumerable<TProblemState> EmptyResult()
        {
            return new TProblemState[0];
        }
    }
}
