using System;
using System.Collections.Generic;
using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle
{
    public class ManhattanHeuristicFunction : IHeuristicFunction<EightPuzzleState>
    {
        private readonly Dictionary<int, Position> _tileExpectedPosDict = new Dictionary<int, Position>();

        public ManhattanHeuristicFunction(Board goalBoard)
        {
            for (int row = 0; row < goalBoard.RowCount; row++)
            {
                for (int col = 0; col < goalBoard.ColumnCount; col++)
                {
                    int val = goalBoard[row, col];

                    _tileExpectedPosDict[val] = new Position(row, col);
                }
            }
        }

        public double Calculate(EightPuzzleState state)
        {
            int result = 0;

            int expected = 0;

            for (int row = 0; row < state.Board.RowCount; row++)
            {
                for (int col = 0; col < state.Board.ColumnCount; col++)
                {
                    int val = state.Board[row, col];
                    expected++;

                    var expectedPos = _tileExpectedPosDict[val];

                    if (val != 0 && val != expected)
                    {
                        result += Math.Abs(row - expectedPos.Row) +
                            Math.Abs(col - expectedPos.Column);
                    }
                }
            }

            return result;
        }
    }
}
