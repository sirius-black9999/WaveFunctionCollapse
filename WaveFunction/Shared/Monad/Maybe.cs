namespace WaveFunction.Shared.Monad
{
    public class Maybe<TIn, TOut> : IMonad<TIn, TOut>
    {
        public Maybe(Func<TIn, Maybe<TIn, TOut>, TOut> transformation) : this(transformation, new List<Exception>())
        {
        }

        public Maybe(Func<TIn, Maybe<TIn, TOut>, TOut> transformation, List<Exception> errors)
        {
            _trans = transformation;
            _result = default;
            Error = errors;
        }

        public Maybe(Func<TIn, TOut> transformation)
        {
            _trans = AddMonad(transformation);
            _result = default;
        }

        public void Run(TIn i)
        {
            try
            {
                _memory = i;
                _result = _trans(_memory, this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _invalid = true;
                Error.Add(e);
            }
        }

        public TOut Retrieve()
        {
            if (_invalid)
                throw new MethodAccessException("attempting to retrieve from monad when no data is available");

            try
            {
                return _result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _invalid = true;
                Error.Add(e);
                throw;
            }
        }


        public IMonad<TIn, TNew> Transform<TNew>(Func<TOut, TNew> transformation) =>
            TransformMaybe(AddMonad(transformation));


        public bool HasValue()
        {
            if (Error.LastOrDefault() is TimeoutException && _invalid)
            {
                _invalid = false;
                Run(_memory);
            }

            return !_invalid;
        }
        public List<Exception> Error { get; } = new List<Exception>();

        private Func<TIn, Maybe<TIn, T_New>, T_New> AddMonad<T_New>(Func<TIn, T_New> transformation)
        {
            return (test, _) => transformation(test);
        }

        private Func<TOut, Maybe<TIn, T_New>, T_New> AddMonad<T_New>(Func<TOut, T_New> transformation)
        {
            return (test, _) => transformation(test);
        }

        
        private Maybe<TIn, TNew> TransformMaybe<TNew>(Func<TOut, Maybe<TIn, TNew>, TNew> transformation)
        {
            if (transformation == null) throw new ArgumentNullException(nameof(transformation));

            return new Maybe<TIn, TNew>((test, maybe) =>
            {
                try
                {
                    if (maybe._step == null)
                    {
                        Run(test);
                        maybe._step = new MonadMemo<TOut, TNew>(Retrieve, v => transformation(v, maybe));
                    }

                    var ret = maybe._step.Retry();
                    maybe._step = null;
                    return ret;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    maybe._invalid = true;
                    maybe.Error.Add(e);
                    return default;
                }
            }, Error);
        }
        
        private readonly Func<TIn, Maybe<TIn, TOut>, TOut> _trans;
        private TOut? _result;
        private TIn _memory;
        private bool _invalid;
        private IMemo<TOut>? _step;
    }
}
