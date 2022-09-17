using System;
using System.Linq;
using WaveFunction;

namespace WaveFunctionTest.PropertyTesting.Tooling
{
    public class Generator<T>
    {
        public Generator(Func<IRng, T> random)
        {
            _rand = random;
        }

        public static T[] Make(int count,IRng rand, Func<IRng, T> As)
        {
            var gen = new Generator<T>(As);
            return Enumerable.Range(0, count).Select(i => gen.Value(rand)).ToArray();
        }

        public T Value(IRng rand) => _rand(rand);

        private readonly Func<IRng, T> _rand;
    }
}
