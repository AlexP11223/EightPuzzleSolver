using System;
using System.Collections.Generic;
using System.Linq;
using EightPuzzleSolver.EightPuzzle;

namespace EightPuzzleSolverApp.Model
{
    public class SolutionAction
    {
        public SolutionAction(MoveDirection direction)
        {
            Direction = direction;
        }

        public MoveDirection Direction { get; }

        public override string ToString()
        {
            return Direction.Name;
        }
    }

    public class SolutionSearchResult
    {
        public SolutionSearchResult(bool success, IList<EightPuzzleState> solution, TimeSpan timeElapsed)
        {
            Success = success;
            Solution = solution;
            TimeElapsed = timeElapsed;
        }

        public bool Success { get; }

        public IList<EightPuzzleState> Solution { get; }

        public TimeSpan TimeElapsed { get; }

        public int MoveCount => Solution?.Count - 1 ?? -1;

        public IList<SolutionAction> SolutionActions
        {
            get
            {
                return Solution.Skip(1).Select(s => new SolutionAction(s.Direction.Value)).ToList();
                
            }
        }

        public override string ToString()
        {
            return $"{Success}, {Solution?.Count}";
        }
    }
}
