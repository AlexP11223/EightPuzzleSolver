using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EightPuzzleSolver.EightPuzzle;
using EightPuzzleSolver.Search;
using EightPuzzleSolver.Search.Algorithms;

namespace EightPuzzleSolverApp.Model
{
    public class PuzzleSolverService : IPuzzleSolverService
    {
        public SolutionSearchResult Solve(Board initialBoard, Algorithm algorithm, HeuristicFunction heuristicFunction, CancellationToken cancellationToken)
        {
            var problem = new EightPuzzleProblem(initialBoard);

            var search = CreateSearch(initialBoard, algorithm, heuristicFunction);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var result = search.Search(problem, cancellationToken).ToList();

            stopwatch.Stop();

            return new SolutionSearchResult(result.Any(), result, stopwatch.Elapsed);
        }

        public async Task<SolutionSearchResult> SolveAsync(Board initialBoard, Algorithm algorithm, HeuristicFunction heuristicFunction, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
            {
                return Solve(initialBoard, algorithm, heuristicFunction, cancellationToken);
            }, cancellationToken);
        }

        private ISearch<EightPuzzleState> CreateSearch(Board initialBoard, Algorithm algorithm, HeuristicFunction heuristicFunction)
        {
            if (algorithm == Algorithm.AStar || algorithm == Algorithm.RecursiveBestFirstSearch)
            {
                IHeuristicFunction<EightPuzzleState> h;

                var goalBoard = Board.CreateGoalBoard(initialBoard.RowCount, initialBoard.ColumnCount);

                switch (heuristicFunction)
                {
                    case HeuristicFunction.NoHeuristic:
                        h = new NoHeuristicFunction<EightPuzzleState>();
                        break;
                    case HeuristicFunction.ManhattanDistance:
                        h = new ManhattanHeuristicFunction(goalBoard);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(heuristicFunction), heuristicFunction, null);
                }

                switch (algorithm)
                {
                    case Algorithm.AStar:
                        return new AStarSearch<EightPuzzleState>(h);
                    case Algorithm.RecursiveBestFirstSearch:
                        return new RecursiveBestFirstSearch<EightPuzzleState>(h);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
                }
            }

            switch (algorithm)
            {
                case Algorithm.IterativeDeepeningSearch:
                    return new IterativeDeepeningSearch<EightPuzzleState>();
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, null);
            }
        }
    }
}