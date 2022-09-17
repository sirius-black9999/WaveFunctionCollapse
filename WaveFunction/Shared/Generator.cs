namespace WaveFunction.Shared
{
    public class Generator<T>
    {
        public Generator(Func<IRng, T> random)
        {
            _rand = random;
        }

        public static T[] Make(int count, IRng rand, Func<IRng, T> @as)
        {
            var gen = new Generator<T>(@as);
            return Enumerable.Range(0, count).Select(_ => gen.Value(rand)).ToArray();
        }

        public T Value(IRng rand) => _rand(rand);

        private readonly Func<IRng, T> _rand;
    }
}
