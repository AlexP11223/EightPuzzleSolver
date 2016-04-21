namespace EightPuzzleSolver.Search
{
    public interface IHeuristicFunction<TProblemState> where TProblemState : IProblemState<TProblemState>
    {
        double Calculate(TProblemState state);
    }
}
