#pragma warning disable 660,661 // ValueType should have suitable default Equals/GetHashCode

namespace EightPuzzleSolver.EightPuzzle
{
    public struct Position
    {
        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }

        public Position Move(MoveDirection direction)
        {
            return new Position(Row + direction.RowChange, Column + direction.ColumnChange);
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"Row: {Row}, Column: {Column}";
        }
    }
}
