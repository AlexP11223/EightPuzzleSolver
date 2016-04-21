using System.Collections.Generic;
using System.Linq;

namespace EightPuzzleSolver.Search
{
    public class Node<TProblemState> where TProblemState : IProblemState<TProblemState>
    {
        public Node(TProblemState state)
        {
            State = state;
            PathCost = 0;
        }

        public Node(TProblemState state, Node<TProblemState> parent)
            : this(state)
        {
            Parent = parent;
            if (Parent != null)
                PathCost = Parent.PathCost + state.Cost;
        }

        public TProblemState State { get; }

        public Node<TProblemState> Parent { get; }

        /// <summary>
        /// The cost of the path from the initial state to the node
        /// </summary>
        public int PathCost { get; }
        
        public bool IsRootNode => Parent == null;

        /// <summary>
        /// Returns the nodes available from this node
        /// </summary>
        public IList<Node<TProblemState>> ExpandNode()
        {
            var children = new List<Node<TProblemState>>();

            foreach (var childState in State.NextStates())
            {
                children.Add(new Node<TProblemState>(childState, this));
            }

            return children;
        }

        public IEnumerable<Node<TProblemState>> PathFromRoot()
        {
            var path = new Stack<Node<TProblemState>>();

            var node = this;
            while (!node.IsRootNode)
            {
                path.Push(node);
                node = node.Parent;
            }
            path.Push(node); // root

            return path;
        }

        public IEnumerable<TProblemState> PathFromRootStates()
        {
            return PathFromRoot().Select(n => n.State);
        }

        public override string ToString()
        {
            return $"State: {State}, PathCost: {PathCost}";
        }
    }
}
