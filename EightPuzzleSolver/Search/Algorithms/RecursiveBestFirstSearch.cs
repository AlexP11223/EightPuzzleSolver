using System;
using System.Collections.Generic;

namespace EightPuzzleSolver.Search.Algorithms
{
    public class RecursiveBestFirstSearch<TProblemState> : ISearch<TProblemState>
                                        where TProblemState : IProblemState<TProblemState>
    {
        private static readonly int Infinity = Int32.MaxValue;

        private readonly IHeuristicFunction<TProblemState> _heuristicFunction;

        public RecursiveBestFirstSearch(IHeuristicFunction<TProblemState> heuristicFunction)
        {
            _heuristicFunction = heuristicFunction;
        }

        public IEnumerable<TProblemState> Search(Problem<TProblemState> problem)
        {
            var root = new Node<TProblemState>(problem.InitialState);

            var sr = Rbfs(problem, root, EvaluationFunction(root), Infinity);

            if (sr.Outcome == SearchResult.SearchOutcome.Success)
            {
                return sr.Solution.PathFromRootStates();
            }

            return EmptyResult();
        }

        private SearchResult Rbfs(Problem<TProblemState> problem, Node<TProblemState> node, double nodeF, double fLimit)
        {
            if (problem.IsGoalState(node.State))
            {
                return new SearchResult(node, fLimit);
            }

            var successors = node.ExpandNode();

            if (successors.Count == 0)
            {
                return new SearchResult(null, Infinity);
            }

            var f = new double[successors.Count];

            for (int i = 0; i < successors.Count; i++)
            {
                f[i] = Math.Max(EvaluationFunction(successors[i]), nodeF);
            }

            while (true)
            {
                int bestIndex = GetBestFValueIndex(f);

                if (f[bestIndex] > fLimit)
                {
                    return new SearchResult(null, f[bestIndex]);
                }

                int altIndex = GetNextBestFValueIndex(f, bestIndex);

                var sr = Rbfs(problem, successors[bestIndex], f[bestIndex], Math.Min(fLimit, f[altIndex]));

                f[bestIndex] = sr.FCostLimit;

                if (sr.Outcome == SearchResult.SearchOutcome.Success)
                {
                    return sr;
                }
            }
        }

        private static int GetBestFValueIndex(double[] f)
        {
            int lidx = 0;
            double lowestSoFar = Infinity;

            for (int i = 0; i < f.Length; i++)
            {
                if (f[i] < lowestSoFar)
                {
                    lowestSoFar = f[i];
                    lidx = i;
                }
            }

            return lidx;
        }

        private static int GetNextBestFValueIndex(double[] f, int bestIndex)
        {
            // array may only contain 1 item, therefore default to bestIndex
            int lidx = bestIndex;
            double lowestSoFar = Infinity;

            for (int i = 0; i < f.Length; i++)
            {
                if (i != bestIndex && f[i] < lowestSoFar)
                {
                    lowestSoFar = f[i];
                    lidx = i;
                }
            }

            return lidx;
        }

        private double EvaluationFunction(Node<TProblemState> node)
        {
            return node.PathCost + _heuristicFunction.Calculate(node.State);
        }

        private IEnumerable<TProblemState> EmptyResult()
        {
            return new TProblemState[0];
        }

        private struct SearchResult
        {
            public enum SearchOutcome
            {
                Fail, Success
            }

            public SearchResult(Node<TProblemState> solution, double fCostLimit)
            {
                Outcome = solution == null ? SearchOutcome.Fail : SearchOutcome.Success;
                Solution = solution;

                FCostLimit = fCostLimit;
            }

            public Node<TProblemState> Solution { get; }

            public SearchOutcome Outcome { get; }

            public double FCostLimit { get; }
        }
    }
}
