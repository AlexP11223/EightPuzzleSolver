using EightPuzzleSolver.Search;
using EightPuzzleSolver.Search.Algorithms;

namespace EightPuzzleSolver.EightPuzzle
{
    public class EightPuzzleProblem : Problem<EightPuzzleState>
    {
        private readonly EightPuzzleState _goalState;

        public EightPuzzleProblem(Board initialBoard, Board goalBoard)
            : base(new EightPuzzleState(initialBoard))
        {
            GoalBoard = goalBoard;
            _goalState = new EightPuzzleState(goalBoard);
        }

        public EightPuzzleProblem(Board initialBoard)
            : this(initialBoard, Board.CreateGoalBoard(initialBoard.RowCount, initialBoard.ColumnCount))
        {
        }

        public Board GoalBoard { get; }

        public override bool IsGoalState(EightPuzzleState state)
        {
            return _goalState.Equals(state);
        }

        public override ISearch<EightPuzzleState> CreateDefaultSolver()
        {
            return new AStarSearch<EightPuzzleState>(new ManhattanHeuristicFunction(GoalBoard));
        }
    }
}
