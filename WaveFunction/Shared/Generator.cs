using System;
using System.Linq;
using WaveFunction;

namespace WaveFunctionTest.PropertyTesting.Tooling
{
    public class Generator<T>
    {
        public Generator(Func<RNG, T> random)
        {
            _rand = random;
        }

        public static T[] Make(int count,RNG rand, Func<RNG, T> As)
        {
            var gen = new Generator<T>(As);
            return Enumerable.Range(0, count).Select(i => gen.Value(rand)).ToArray();
        }

        public T Value(RNG rand) => _rand(rand);

        private readonly Func<RNG, T> _rand;
    }
}
