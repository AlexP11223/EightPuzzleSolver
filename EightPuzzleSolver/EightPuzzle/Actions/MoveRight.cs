using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle.Actions
{
    /// <summary>
    /// Move the blank tile to the right
    /// </summary>
    public class MoveRight : EightPuzzleAction
    {
        public override int ColumnChange => +1;
    }
}