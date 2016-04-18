using System.Collections.Generic;
using System.Linq;
using EightPuzzleSolver.EightPuzzle.Actions;
using EightPuzzleSolver.Search;
using Xunit;

namespace EightPuzzleSolver.Tests.Search
{
    public class ActionTests
    {
        [Fact]
        public void ShouldNotHaveDuplicates()
        {
            var set = new HashSet<Action>();
            set.Add(new NoOp());
            set.Add(new NoOp());
            set.Add(new MoveLeft());
            set.Add(new MoveLeft());
            set.Add(new MoveRight());
            set.Add(new MoveRight());
            set.Add(new MoveTop());
            set.Add(new MoveTop());
            set.Add(new MoveBottom());
            set.Add(new MoveBottom());

            Assert.Equal(new HashSet<Action> { new NoOp(), new MoveLeft(), new MoveRight(), new MoveTop(), new MoveBottom()}, set );
        }
    }
}
