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
                HandleIssue(e, this);
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
                return HandleIssue(e, this);
            }
        }

        public IMonad<TIn, TNew> Transform<TNew>(Func<TOut, TNew> transformation) =>
            TransformMaybe(this, AddMonad(transformation));


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

        private static Func<TIn, Maybe<TIn, T_New>, T_New> AddMonad<T_New>(Func<TIn, T_New> transformation)
        {
            return (test, _) => transformation(test);
        }

        private static Func<TOut, Maybe<TIn, T_New>, T_New> AddMonad<T_New>(Func<TOut, T_New> transformation)
        {
            return (test, _) => transformation(test);
        }

        private static Maybe<TIn, TNew> TransformMaybe<TNew>(Maybe<TIn, TOut> self,
            Func<TOut, Maybe<TIn, TNew>, TNew> transformation)
        {
            if (transformation == null) throw new ArgumentNullException(nameof(transformation));

            return new Maybe<TIn, TNew>(BindTransform(self, transformation), self.Error);
        }

        private static Func<TIn, Maybe<TIn, TNew>, TNew> BindTransform<TNew>(Maybe<TIn, TOut> self,
            Func<TOut, Maybe<TIn, TNew>, TNew> transformation)
        {
            return (test, maybe) =>
            {
                try
                {
                    return EvaluateStep(self, transformation, maybe, test);
                }
                catch (Exception e)
                {
                    return HandleIssue(e, maybe);
                }
            };
        }


        private static TNew EvaluateStep<TNew>(Maybe<TIn, TOut> self, Func<TOut, Maybe<TIn, TNew>, TNew> transformation,
            Maybe<TIn, TNew> maybe,
            TIn test)
        {
            if (maybe._step == null)
            {
                self.Run(test);
                maybe._step = new MonadMemo<TOut, TNew>(self.Retrieve, BindMaybe(transformation, maybe));
            }

            var ret = maybe._step.Retry();
            maybe._step = null;
            return ret;
        }

        private static TNew HandleIssue<TNew>(Exception e, Maybe<TIn, TNew> maybe)
        {
            Console.WriteLine(e);
            maybe._invalid = true;
            maybe.Error.Add(e);
            return default;
        }

        private static Func<TOut, TNew> BindMaybe<TNew>(Func<TOut, Maybe<TIn, TNew>, TNew> transformation,
            Maybe<TIn, TNew> maybe)
        {
            return v => transformation(v, maybe);
        }

        private readonly Func<TIn, Maybe<TIn, TOut>, TOut> _trans;
        private TOut? _result;
        private TIn _memory;
        private bool _invalid;
        private IMemo<TOut>? _step;
    }
}
