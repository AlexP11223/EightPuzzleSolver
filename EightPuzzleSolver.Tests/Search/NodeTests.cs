using EightPuzzleSolver.Search;
using Xunit;

namespace EightPuzzleSolver.Tests.Search
{
    public class NodeTests
    {
        [Fact]
        public void PathCostTest()
        {
            var root = new Node(null);
            var child1 = new Node(null, root, null, 8);
            var child2 = new Node(null, child1, null, 6);

            Assert.Equal(0, root.PathCost);
            Assert.Equal(8, child1.PathCost);
            Assert.Equal(14, child2.PathCost);
        }

        [Fact]
        public void PathFromRootTest()
        {
            var root = new Node(null);
            var child1 = new Node(null, root, null, 8);
            var child2 = new Node(null, child1, null, 6);

            Assert.Equal(new[] { root }, root.PathFromRoot());
            Assert.Equal(new[] { root, child1 }, child1.PathFromRoot());
            Assert.Equal(new[] { root, child1, child2 }, child2.PathFromRoot());
        }
    }
}
