namespace WaveFunction.Shared.Monad
{
    public interface IMemo<out TOut>
    {
        TOut Retry();
    }

    public class MonadMemo<TIn, TOut> : IMemo<TOut>
    {
        public MonadMemo(Func<TIn> memory, Func<TIn, TOut> operation)
        {
            _operation = operation;
            _memory = memory;
        }

        public TOut Retry() => _operation(_memory());

        private readonly Func<TIn, TOut> _operation;
        private readonly Func<TIn> _memory;
    }
}
