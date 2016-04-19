namespace EightPuzzleSolver.EightPuzzle
{
    public struct MoveDirection
    {
        public static readonly MoveDirection Left = new MoveDirection("Left", 0, -1);
        public static readonly MoveDirection Right = new MoveDirection("Right", 0, +1);
        public static readonly MoveDirection Top = new MoveDirection("Top", -1, 0);
        public static readonly MoveDirection Bottom = new MoveDirection("Bottom", +1, 0);

        private MoveDirection(string name, int rowChange, int columnChange)
        {
            Name = name;
            RowChange = rowChange;
            ColumnChange = columnChange;
        }

        public string Name { get; }

        public int RowChange { get; }

        public int ColumnChange { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
