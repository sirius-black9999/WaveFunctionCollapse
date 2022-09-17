
namespace WaveFunction.Shared.Monad
{
    public interface IMonad<in TIn, out TOut>
    {
        void Run(TIn i);
        TOut Retrieve();
        IMonad<TIn, TNew> Transform<TNew>(Func<TOut, TNew> transformation);
    }
}
