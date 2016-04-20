using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EightPuzzleSolver.EightPuzzle
{
    public class Board
    {
        private readonly byte[] _data;

        private Position? _blankTilePos;

        /// <summary>
        /// Creates board from 1D array representation (row-major order). [[1, 2, 3], [4, 5, 0]] == [1, 2, 3, 4, 5, 0]
        /// </summary>
        public Board(byte[] data, int rowCount, int columnCount)
        {
            _data = data;
            RowCount = (byte) rowCount;
            ColumnCount = (byte) columnCount;
        }

        public Board(byte[,] data)
        {
            RowCount = (byte) data.GetLength(0);
            ColumnCount = (byte) data.GetLength(1);

            _data = new byte[ColumnCount * RowCount];

            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    _data[To1DCoord(i, j, ColumnCount)] = data[i, j];
                }
            }
        }

        private Board(byte[] data, int rowCount, int columnCount, Position blankTilePos)
            : this(data, rowCount, columnCount)
        {
            _blankTilePos = blankTilePos;

            Debug.Assert(this[_blankTilePos.Value] == 0);
        }

        public int ColumnCount { get; }

        public byte RowCount { get; }

        public byte this[int row, int col] => _data[To1DCoord(row, col, ColumnCount)];

        public byte this[Position pos] => this[pos.Row, pos.Column];

        /// <summary>
        /// Returns position of the blank tile
        /// </summary>
        public Position BlankTilePosition
        {
            get
            {
                if (!_blankTilePos.HasValue)
                {
                    for (int i = 0; i < RowCount; i++)
                    {
                        for (int j = 0; j < ColumnCount; j++)
                        {
                            if (this[i, j] == 0)
                            {
                                _blankTilePos = new Position(i, j);
                                return _blankTilePos.Value;
                            }
                        }
                    }

                    throw new ArgumentException("blank tile is not found");
                }

                return _blankTilePos.Value;
            }
        }

        /// <summary>
        /// Checks if it is possible to move the blank tile in the specified direction
        /// </summary>
        public bool CanMove(MoveDirection direction)
        {
            var newBlankTilePos = BlankTilePosition.Move(direction);

            return newBlankTilePos.Row >= 0 && newBlankTilePos.Row < RowCount &&
                newBlankTilePos.Column >= 0 && newBlankTilePos.Column < ColumnCount;
        }

        /// <summary>
        /// Returns new board where the blank tile is moved in the specified directtion
        /// </summary>
        public Board Move(MoveDirection direction)
        {
            if (!CanMove(direction))
                throw new ArgumentException("Cannot move in this direction");

            var newBlankTilePos = BlankTilePosition.Move(direction);

            var newData = (byte[]) _data.Clone();
            newData[To1DCoord(BlankTilePosition, ColumnCount)] = newData[To1DCoord(newBlankTilePos, ColumnCount)];
            newData[To1DCoord(newBlankTilePos, ColumnCount)] = 0;

            return new Board(newData, RowCount, ColumnCount, newBlankTilePos);
        }

        protected bool Equals(Board other)
        {
            if (other.ColumnCount != this.ColumnCount || other.RowCount != this.RowCount)
                return false;

            return other._data.SequenceEqual(_data);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Board) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 42;
                for (int i = 0; i < _data.Length; i++)
                {
                    hashCode = hashCode * 17 + _data[i];
                }
                hashCode = hashCode * 397 + ColumnCount;
                hashCode = hashCode * 397 + RowCount;
                return hashCode;
            }
        }

        public static bool operator ==(Board left, Board right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Board left, Board right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            for (int i = 0; i < RowCount; i++)
            {
                sb.Append("{ ");
                for (int j = 0; j < ColumnCount; j++)
                {
                    sb.Append(this[i, j]);
                    sb.Append(" ");
                }
                sb.Append("}");
                if (i < RowCount - 1)
                    sb.AppendLine(", ");
            }
            return $"{RowCount}x{ColumnCount}: {sb}";
        }

        private static int To1DCoord(int row, int col, int width)
        {
            return row*width + col;
        }

        private static int To1DCoord(Position pos, int width)
        {
            return pos.Row * width + pos.Column;
        }
    }
}
