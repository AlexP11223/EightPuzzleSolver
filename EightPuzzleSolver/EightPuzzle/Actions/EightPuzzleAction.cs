using EightPuzzleSolver.Search;

namespace EightPuzzleSolver.EightPuzzle.Actions
{
    public abstract class EightPuzzleAction : Action
    {
        public virtual int RowChange => 0;
        public virtual int ColumnChange => 0;
    }
}