using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle.Actions
{
    /// <summary>
    /// Move the blank tile to the bottom
    /// </summary>
    public class MoveBottom : EightPuzzleAction
    {
        public override int RowChange => +1;
    }
}