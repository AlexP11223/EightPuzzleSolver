using System.ComponentModel;

namespace EightPuzzleSolverApp.Model
{
    public enum Algorithm
    {
        [Description("A*")]
        AStar,
        [Description("Recursive Best-First Search")]
        RecursiveBestFirstSearch,
        [Description("Iterative Deepening Search")]
        IterativeDeepeningSearch,
        [Description("Depth-Limited Search")]
        DepthLimitedSearch
    }
}
