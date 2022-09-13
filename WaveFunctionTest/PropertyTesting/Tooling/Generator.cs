using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WaveFunction;

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
            var gen = new Generator<T>(randomizer => randomizer.NextEnum<T>());
            return Enumerable.Range(0, count).Select(i => gen.Value()).ToArray();
        }
        public static T[] Make(int count, Func<Randomizer, T> As)
        {
            var gen = new Generator<T>(As);
            return Enumerable.Range(0, count).Select(i => gen.Value()).ToArray();
        }
        public static T[] MakeRand(int min, int max, Func<Randomizer, T> As)
        {
            var intMaker = new Generator<int>(rand => rand.Next(min, max));
            var gen = new Generator<T>(As);
            return Enumerable.Range(0, intMaker.Value()).Select(i => gen.Value()).ToArray();
        }

        public T Value() => _rand(
            TestContext.CurrentContext.Random);

        private readonly Func<Randomizer, T> _rand;
    }
}
