namespace WaveFunction.Shared.Monad
{
    public class Monad<TIn, TOut> : IMonad<TIn, TOut>
    {
        public Monad(Func<TIn, TOut> transformation)
        {
            _trans = transformation;
        }

        public virtual void Run(TIn i) => _input = i;
        public virtual TOut Retrieve() => _trans(_input);

        public IMonad<TIn, TNew> Transform<TNew>(Func<TOut, TNew> transformation)
        {
            return new Monad<TIn, TNew>(test =>
            {
                Run(test);
                return transformation(Retrieve());
            });
        }

        private readonly Func<TIn, TOut> _trans;
        private TIn _input;
    }
}
