using System.Collections.Generic;
using Priority_Queue;

namespace EightPuzzleSolver.Search.Algorithms
{
    public class AStarSearch<TProblemState> : ISearch<TProblemState>
                                        where TProblemState : IProblemState<TProblemState>
    {
        private readonly IHeuristicFunction<TProblemState> _heuristicFunction;

        public AStarSearch(IHeuristicFunction<TProblemState> heuristicFunction)
        {
            _heuristicFunction = heuristicFunction;
        }

        public IEnumerable<TProblemState> Search(Problem<TProblemState> problem)
        {
            var frontier = new SimplePriorityQueue<Node<TProblemState>>();

            var explored = new HashSet<TProblemState>();

            var root = new Node<TProblemState>(problem.InitialState);

            frontier.Enqueue(root, EvaluationFunction(root));

            while (frontier.Count > 0)
            {
                var node = frontier.Dequeue();
                explored.Add(node.State);

                if (problem.IsGoalState(node.State))
                {
                    return node.PathFromRootStates();
                }

                foreach (var child in node.ExpandNode())
                {
                    if (!explored.Contains(child.State))
                    {
                        frontier.Enqueue(child, EvaluationFunction(child));
                    }
                }
                
                while (frontier.Count > 0 && explored.Contains(frontier.First.State))
                {
                    frontier.Dequeue();
                }
            }

            return EmptyResult();
        }

        private double EvaluationFunction(Node<TProblemState> node)
        {
            return node.PathCost + _heuristicFunction.Calculate(node.State);
        }

        private IEnumerable<TProblemState> EmptyResult()
        {
            return new TProblemState[0];
        }
    }
}
