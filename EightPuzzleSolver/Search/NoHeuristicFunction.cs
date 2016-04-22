namespace EightPuzzleSolver.Search
{
    public class NoHeuristicFunction<TProblemState> : IHeuristicFunction<TProblemState>
                                    where TProblemState : IProblemState<TProblemState>
    {
        public double Calculate(TProblemState state)
        {
            return 0;
        }
    }
}
