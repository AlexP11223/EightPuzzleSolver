using System;
using System.Threading;

namespace EightPuzzleSolver.Helpers
{
    internal static class StaticRandom
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> Random =
            new ThreadLocal<Random>(() => new Random(Interlocked.Increment(ref _seed)));

        public static int Next()
        {
            return Random.Value.Next();
        }

        public static int Next(int min, int max)
        {
            return Random.Value.Next(min, max);
        }
    }
}
