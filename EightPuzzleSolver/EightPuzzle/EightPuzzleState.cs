using System.Collections.Generic;
using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle
{
    public class EightPuzzleState : IProblemState<EightPuzzleState>
    {
        public EightPuzzleState(Board board)
            : this(board, 0)
        {
        }

        public EightPuzzleState(Board board, EightPuzzleState parent, MoveDirection direction)
            : this(board, 1, parent, direction)
        {
        }

        private EightPuzzleState(Board board, int cost, EightPuzzleState parent = null, MoveDirection? direction = null)
        {
            Board = board;
            Cost = cost;
            Parent = parent;
            Direction = direction;
        }

        public Board Board { get; }

        public EightPuzzleState Parent { get; }

        public bool IsRootState => Parent == null;

        /// <summary>
        /// Direction of the blank tile movement that was applied to the previous state
        /// </summary>
        public MoveDirection? Direction { get; }

        /// <summary>
        /// Cost of getting to this state from the previous state
        /// </summary>
        public int Cost { get; }

        public ISet<EightPuzzleState> NextStates()
        {
            var states = new HashSet<EightPuzzleState>();

            foreach (var direction in MoveDirection.AllDirections)
            {
                if (Board.CanMove(direction))
                {
                    var board = Board.Move(direction);
                    if (IsRootState || board != Parent.Board)
                    {
                        states.Add(new EightPuzzleState(board, this, direction));
                    }
                }
            }

            return states;
        }

        public bool Equals(EightPuzzleState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return Equals(Board, other.Board);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EightPuzzleState) obj);
        }

        public override int GetHashCode()
        {
            return Board.GetHashCode();
        }

        public static bool operator ==(EightPuzzleState left, EightPuzzleState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(EightPuzzleState left, EightPuzzleState right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return $"{Direction}, {Board}";
        }
    }
}
