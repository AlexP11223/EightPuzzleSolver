using System.Threading;
using System.Threading.Tasks;
using EightPuzzleSolver.EightPuzzle;

namespace EightPuzzleSolverApp.Model
{
    public interface IPuzzleSolverService
    {
        SolutionSearchResult Solve(Board initialBoard, Algorithm algorithm, HeuristicFunction heuristicFunction, CancellationToken cancellationToken);

        Task<SolutionSearchResult> SolveAsync(Board initialBoard, Algorithm algorithm, HeuristicFunction heuristicFunction, CancellationToken cancellationToken);
    }
}
