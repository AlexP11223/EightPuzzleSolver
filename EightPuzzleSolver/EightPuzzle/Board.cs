using System;
using System.Diagnostics;
using System.Text;

namespace EightPuzzleSolver.EightPuzzle
{
    public class Board
    {
        private readonly byte[,] _data;

        private Position? _blankTilePos;

        public Board(byte[,] data)
        {
            _data = data;
        }

        private Board(byte[,] data, Position blankTilePos)
            : this(data)
        {
            _blankTilePos = blankTilePos;

            Debug.Assert(this[_blankTilePos.Value] == 0);
        }

        public int Width => _data.GetLength(0);

        public int Height => _data.GetLength(1);

        public byte this[int row, int col] => _data[row, col];

        public byte this[Position pos] => this[pos.Row, pos.Column];

        public byte[,] GetData()
        {
            return (byte[,]) _data.Clone();
        }

        /// <summary>
        /// Returns position of the blank tile
        /// </summary>
        public Position BlankTilePosition
        {
            get
            {
                if (!_blankTilePos.HasValue)
                {
                    for (int i = 0; i < Width; i++)
                    {
                        for (int j = 0; j < Height; j++)
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

            return newBlankTilePos.Row >= 0 && newBlankTilePos.Row < Width &&
                newBlankTilePos.Column >= 0 && newBlankTilePos.Column < Height;
        }

        /// <summary>
        /// Returns new board where the blank tile is moved in the specified directtion
        /// </summary>
        public Board Move(MoveDirection direction)
        {
            var newBlankTilePos = BlankTilePosition.Move(direction);

            var newData = GetData();
            newData[BlankTilePosition.Row, BlankTilePosition.Column] = newData[newBlankTilePos.Row, newBlankTilePos.Column];
            newData[newBlankTilePos.Row, newBlankTilePos.Column] = 0;

            return new Board(newData, newBlankTilePos);
        }

        protected bool Equals(Board other)
        {
            if (other.Width != this.Width || other.Height != this.Height)
                return false;

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (other[i, j] != this[i, j])
                        return false;
                }
            }

            return true;
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
            int hash = 41;
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    hash = hash*7 + this[i, j];
                }
            }

            return hash;
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
            for (int i = 0; i < Width; i++)
            {
                sb.Append("{ ");
                for (int j = 0; j < Height; j++)
                {
                    sb.Append(_data[i, j]);
                    sb.Append(" ");
                }
                sb.Append("}");
                if (i < Width - 1)
                    sb.AppendLine(", ");
            }
            return $"{Width}x{Height}: {sb}";
        }
    }
}
