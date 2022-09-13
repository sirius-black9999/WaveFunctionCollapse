
namespace WaveFunction.Shared.Monad
{
    public interface IMonad<T_In, T_Out>
    {
        void Run(T_In i);
        T_Out Retrieve();
        IMonad<T_In, T_New> Transform<T_New>(Func<T_Out, T_New> transformation);
    }
}
