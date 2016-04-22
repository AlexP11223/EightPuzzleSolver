using System;
using System.Collections.Generic;
using System.Threading;

namespace EightPuzzleSolver.Search.Algorithms
{
    public class IterativeDeepeningSearch<TProblemState> : ISearch<TProblemState>
                                        where TProblemState : IProblemState<TProblemState>
    {
        private readonly int Infinity = Int32.MaxValue;

        public IEnumerable<TProblemState> Search(Problem<TProblemState> problem, CancellationToken cancellationToken = default(CancellationToken))
        {
            for (int i = 0; i < Infinity; i++)
            {
                var dls = new DepthLimitedSearch<TProblemState>(i);

                var result = dls.Search(problem, cancellationToken);

                if (!dls.IsCutoff)
                {
                    return result;
                }
            }

            return EmptyResult();
        }

        private IEnumerable<TProblemState> EmptyResult()
        {
            return new TProblemState[0];
        }
    }
}
