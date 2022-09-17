using System;
using WaveFunction.MagicSystemSketch;
using WaveFunctionTest.PropertyTesting.Tooling;

namespace WaveFunctionTest.PropertyTesting.Generators
{
    public static class MagicGenerators
    {
        public static T[] Generate<T>(Func<T> genOne, int count)
        {
            var ret = new T[count];
            for (int i = 0; i < ret.Length; i++)
            {
                ret[i] = genOne();
            }

            return ret;
        }

        public static Generator<Rune> Rune =>
            new Generator<Rune>(static rng =>
                new Gesture().Record(
                        rng.NextEnum<Aspect>(),
                        rng.NextFloat(0, 1))
                    .Resolve());

        public static Generator<Incantation> Incantation =>
            new Generator<Incantation>(static rng =>
                new Incantation().Inscribe(
                    Generate(Rune.Value, rng.Next(1, 10))));
    }
}
