using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle.Actions
{
    /// <summary>
    /// Move the blank tile to the left
    /// </summary>
    public class MoveLeft : EightPuzzleAction
    {
        public override int ColumnChange => -1;
    }
}