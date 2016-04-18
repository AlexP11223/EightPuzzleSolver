using System.Collections.Generic;

namespace EightPuzzleSolver.Search
{
    public class Node
    {
        public Node(object state)
        {
            State = state;
            PathCost = 0;
        }

        /// <summary>
        /// </summary>
        /// <param name="action">The action that was applied to the parent to generate the node</param>
        /// <param name="stepCost">The cost from the parent node to this node</param>
        public Node(object state, Node parent, Action action, int stepCost)
            : this(state)
        {
            Parent = parent;
            Action = action;
            if (Parent != null)
                PathCost = Parent.PathCost + stepCost;
        }

        public object State { get; }

        public Node Parent { get; }

        /// <summary>
        /// The action that was applied to the parent to generate the node
        /// </summary>
        public Action Action { get; }

        /// <summary>
        /// The cost of the path from the initial statee to the node
        /// </summary>
        public int PathCost { get; }
        
        public bool IsRootNode => Parent == null;

        public IEnumerable<Node> PathFromRoot()
        {
            var path = new Stack<Node>();

            var node = this;
            while (!node.IsRootNode)
            {
                path.Push(node);
                node = node.Parent;
            }
            path.Push(node); // root

            return path;
        }

        public override string ToString()
        {
            return $"{{Parent: {Parent}, State: {State}, Action: {Action}, PathCost: {PathCost}}}";
        }
    }
}
