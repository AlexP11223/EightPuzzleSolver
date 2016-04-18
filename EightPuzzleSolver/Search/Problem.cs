using System.Collections.Generic;

namespace EightPuzzleSolver.Search
{
    public abstract class Problem
    {
        protected Problem(object initialState)
        {
            InitialState = initialState;
        }

        public object InitialState { get; }

        /// <summary>
        /// Checks if the state is the goal state
        /// </summary>
        public abstract bool IsGoalState(object state);

        /// <summary>
        /// Returns the actions available from the state
        /// </summary>
        public abstract ISet<Action> Actions(object state);

        /// <summary>
        /// Returns the state that results after performing the action on the state
        /// </summary>
        public abstract object ResultState(object state, Action action);

        /// <summary>
        ///  Returns the cost of action to reach from state to reachedState
        /// </summary>
        /// <param name="state">The state from which the action will be performed</param>
        /// <param name="action">One of the actions available in the state</param>
        /// <param name="reachedState">The state that results after performing the action</param>
        public virtual int StepCost(object state, Action action, object reachedState)
        {
            return 1;
        }
    }

    /// <summary>
    /// Generic wrapper for Problem to avoid casts to the state type
    /// </summary>
    public abstract class ProblemGeneric<TState> : Problem
    {
        protected ProblemGeneric(TState initialState)
            : base(initialState)
        {
        }

        public new TState InitialState => (TState) base.InitialState;

        public override bool IsGoalState(object state)
        {
            return IsGoalState((TState) state);
        }

        public override ISet<Action> Actions(object state)
        {
            return Actions((TState) state);
        }

        public override object ResultState(object state, Action action)
        {
            return ResultState((TState) state, action);
        }

        public override int StepCost(object state, Action action, object reachedState)
        {
            return StepCost((TState) state, action, (TState) reachedState);
        }

        public abstract bool IsGoalState(TState state);

        public abstract ISet<Action> Actions(TState state);

        public abstract TState ResultState(TState state, Action action);

        public virtual int StepCost(TState state, Action action, TState reachedState)
        {
            return 1;
        }
    }
}
