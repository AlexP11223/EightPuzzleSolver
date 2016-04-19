using System.Linq;
using EightPuzzleSolver.Search;
using Xunit;

namespace EightPuzzleSolver.Tests.Search
{
    public class NodeTests
    {
        [Fact]
        public void PathCostTest()
        {
            var root = new Node<DummyProblemState>(new DummyProblemState(0));
            var child1 = new Node<DummyProblemState>(new DummyProblemState(8), root);
            var child2 = new Node<DummyProblemState>(new DummyProblemState(6), child1);

            Assert.Equal(0, root.PathCost);
            Assert.Equal(8, child1.PathCost);
            Assert.Equal(14, child2.PathCost);
        }

        [Fact]
        public void PathFromRootTest()
        {
            var root = new Node<DummyProblemState>(new DummyProblemState(0));
            var child1 = new Node<DummyProblemState>(new DummyProblemState(8), root);
            var child2 = new Node<DummyProblemState>(new DummyProblemState(6), child1);

            Assert.Equal(new[] { root }, root.PathFromRoot());
            Assert.Equal(new[] { root, child1 }, child1.PathFromRoot());
            Assert.Equal(new[] { root, child1, child2 }, child2.PathFromRoot());

            Assert.Equal(new[] { root.State }, root.PathFromRootStates());
            Assert.Equal(new[] { root.State, child1.State }, child1.PathFromRootStates());
            Assert.Equal(new[] { root.State, child1.State, child2.State }, child2.PathFromRootStates());
        }

        [Fact]
        public void ExpandNodeTest()
        {
            var state1 = new DummyProblemState(8);
            var state2 = new DummyProblemState(6);

            var rootNextStates = new[] {state1, state2};

            var root = new Node<DummyProblemState>(new DummyProblemState(0, rootNextStates));

            var children = root.ExpandNode().ToList();

            Assert.Equal(2, children.Count);
            Assert.Contains(children, node => node.State == state1 && node.Parent == root);
            Assert.Contains(children, node => node.State == state2 && node.Parent == root);
        }
    }
}
