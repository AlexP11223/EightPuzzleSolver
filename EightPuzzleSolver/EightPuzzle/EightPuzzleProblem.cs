using System;
using System.Collections.Generic;
using EightPuzzleSolver.Search;
using Action = EightPuzzleSolver.Search.Action;

namespace EightPuzzleSolver.EightPuzzle
{
    public class EightPuzzleProblem : ProblemGeneric<byte[,]>
    {
        public EightPuzzleProblem(byte[,] initialState)
            : base(initialState)
        {
        }

        public override bool IsGoalState(byte[,] state)
        {
            return false;
        }

        public override ISet<Action> Actions(byte[,] state)
        {
            throw new NotImplementedException();
        }

        public override byte[,] ResultState(byte[,] state, Action action)
        {
            throw new NotImplementedException();
        }
    }
}