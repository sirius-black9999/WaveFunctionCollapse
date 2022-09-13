namespace WaveFunction.Shared.Monad
{
    public class Monad<T_In, T_Out> : IMonad<T_In, T_Out>
    {
        public Monad(Func<T_In, T_Out> transformation)
        {
            _trans = transformation;
        }
        public virtual void Run(T_In i) => input = i;
        public virtual T_Out Retrieve() => _trans(input);
        public IMonad<T_In, T_New> Transform<T_New>(Func<T_Out, T_New> transformation)
        {
            return new Monad<T_In, T_New>(test =>
            {
                Run(test);
                return transformation(Retrieve());
            });
        }
        
        private readonly Func<T_In, T_Out> _trans;
        private T_In input;
    }
}
