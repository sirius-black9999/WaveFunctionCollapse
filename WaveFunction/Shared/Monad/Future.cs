namespace WaveFunction.Shared.Monad
{
    public class Future<TIn, TOut>
    {
        public Future(Maybe<TIn, TOut> operation)
        {
            _operation = operation;
        }


        public Future<TIn, TOut> Start(TIn i)
        {
            _started = DateTime.Now;
            _runner = new Thread(() =>
            {
                _operation.Run(i);
                int countdown = 1000;
                while (!_operation.HasValue() && countdown-- > 0)
                {
                    //wait
                }
                _ready = true;
            });
            _runner.Start();
            return this;
        }

        public TOut Retrieve(int timeout = 0)
        {
            if (_runner.Join(timeout))
                return _operation.Retrieve();

            throw new InvalidOperationException("Attempting to retrieve from a future that is not yet ready");
        }

        public bool Ready => _ready && _operation.HasValue();


        public TimeSpan Time => DateTime.Now - _started;

        public void Stop()
        {
            while (_runner.ThreadState != ThreadState.Stopped)
            {
                _runner.Interrupt();
            }
        }

        public ThreadState State => _runner.ThreadState;
        public List<Exception> Error => _operation.Error;

        private readonly Maybe<TIn, TOut> _operation;
        private bool _ready;
        private Thread _runner;

        private DateTime _started;
    }
}
