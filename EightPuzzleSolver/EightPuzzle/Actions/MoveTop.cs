using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle.Actions
{
    /// <summary>
    /// Move the blank tile to the top
    /// </summary>
    public class MoveTop : EightPuzzleAction
    {
        public override int RowChange => -1;
    }
}