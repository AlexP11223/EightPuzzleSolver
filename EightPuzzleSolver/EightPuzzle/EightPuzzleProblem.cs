using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle
{
    public class EightPuzzleProblem : Problem<EightPuzzleState>
    {
        private readonly EightPuzzleState _goalState;

        public EightPuzzleProblem(Board initialBoard, Board goalBoard)
            : base(new EightPuzzleState(initialBoard))
        {
            _goalState = new EightPuzzleState(goalBoard);
        }

        public override bool IsGoalState(EightPuzzleState state)
        {
            return _goalState.IsEqual(state);
        }
    }
}
