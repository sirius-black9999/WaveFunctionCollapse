namespace WaveFunction.Shared.Monad
{
    public class Maybe<TIn, TOut> : IMonad<TIn, TOut>
    {
        public Maybe(Func<TIn, Maybe<TIn, TOut>, TOut> transformation) :
            this(transformation, new List<Exception>())
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
            _trans = MaybeBindings<TIn, TOut>.AddMonad(transformation);
            _result = default;
        }

        public IMonad<TIn, TNew> Transform<TNew>(Func<TOut, TNew> transformation) =>
            MaybeBindings<TIn, TOut>.TransformMaybe(this,
                MaybeBindings<TIn, TOut>.AddMonad(transformation));

        public void Run(TIn i)
        {
            try
            {
                _memory = i;
                _result = _trans(_memory, this);
            }
            catch (Exception e)
            {
                HandleIssue(this, e);
            }
        }

        public static TNew EvaluateStep<TNew>(Maybe<TIn, TOut> self,
            Func<TOut, Maybe<TIn, TNew>, TNew> transformation,
            Maybe<TIn, TNew> root, TIn input)
        {
            if (root._step == null)
            {
                self.Run(input);
                root._step = new MonadMemo<TOut, TNew>(self.Retrieve,
                    MaybeBindings<TIn, TOut>.BindMaybe(transformation, root));
            }

            var ret = root._step.Retry();
            root._step = null;
            return ret;
        }

        public bool HasValue()
        {
            if (Error.LastOrDefault() is TimeoutException && _invalid)
            {
                _invalid = false;
                Run(_memory);
            }

            return !_invalid;
        }

        public TOut Retrieve()
        {
            if (_invalid)
                throw new MethodAccessException(
                    "attempting to retrieve from monad when no data is available");

            try
            {
                return _result;
            }
            catch (Exception e)
            {
                return HandleIssue(this, e);
            }
        }

        public static TNew HandleIssue<TNew>(Maybe<TIn, TNew> root, Exception e)
        {
            Console.WriteLine(e);
            root._invalid = true;
            root.Error.Add(e);
            return default;
        }


        public List<Exception> Error { get; } = new List<Exception>();
        private readonly Func<TIn, Maybe<TIn, TOut>, TOut> _trans;
        private TOut? _result;
        private TIn? _memory;
        private bool _invalid;
        private IMemo<TOut>? _step;
    }
}
