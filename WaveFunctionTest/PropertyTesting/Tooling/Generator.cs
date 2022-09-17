using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace WaveFunctionTest.PropertyTesting.Tooling
{
    public class Generator<T>
    {
        public Generator(Func<Randomizer, T> random)
        {
            _rand = random;
        }

        public static T[] MakeEnum(int count)
        {
            var gen = new Generator<T>(static randomizer => randomizer.NextEnum<T>());
            return Enumerable.Range(0, count).Select(_ => gen.Value()).ToArray();
        }

        public static T[] Make(int count, Func<Randomizer, T> @as)
        {
            var gen = new Generator<T>(@as);
            return Enumerable.Range(0, count).Select(_ => gen.Value()).ToArray();
        }

        public static T[] MakeRand(int min, int max, Func<Randomizer, T> @as)
        {
            var intMaker = new Generator<int>(rand => rand.Next(min, max));
            var gen = new Generator<T>(@as);
            return Enumerable.Range(0, intMaker.Value()).Select(_ => gen.Value()).ToArray();
        }

        public T Value() => _rand(
            TestContext.CurrentContext.Random);

        private readonly Func<Randomizer, T> _rand;
    }
}
