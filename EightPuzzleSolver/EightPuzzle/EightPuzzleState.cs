using System.Collections.Generic;
using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle
{
    public class EightPuzzleState : IProblemState<EightPuzzleState>
    {
        public EightPuzzleState(Board board)
            : this(board, 0, null)
        {
        }

        public EightPuzzleState(Board board, MoveDirection direction)
            : this(board, 1, direction)
        {
        }

        private EightPuzzleState(Board board, int cost, MoveDirection? direction = null)
        {
            Board = board;
            Cost = cost;
            Direction = direction;
        }

        public Board Board { get; }

        /// <summary>
        /// Direction of the blank tile movement that was applied to the previous state
        /// </summary>
        public MoveDirection? Direction { get; }

        /// <summary>
        /// Cost of getting to this state from the previous state
        /// </summary>
        public int Cost { get; }
        
        public bool IsEqual(EightPuzzleState other)
        {
            return other.Board == Board;
        }

        public ISet<EightPuzzleState> NextStates()
        {
            var states = new HashSet<EightPuzzleState>();

            foreach (var direction in MoveDirection.AllDirections)
            {
                if (Board.CanMove(direction))
                {
                    states.Add(new EightPuzzleState(Board.Move(direction), 1, direction));
                }
            }

            return states;
        }

        public override string ToString()
        {
            return $"{Direction}, {Board}";
        }
    }
}
