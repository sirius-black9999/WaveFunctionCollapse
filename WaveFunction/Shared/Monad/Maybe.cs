using System.Diagnostics;

namespace WaveFunction.Shared.Monad
{
    public class Maybe<T_In, T_Out> : IMonad<T_In, T_Out>
    {
        public Maybe(Func<T_In, Maybe<T_In, T_Out>, T_Out> transformation) : this(transformation, new List<Exception>())
        {
        }

        public Maybe(Func<T_In, Maybe<T_In, T_Out>, T_Out> transformation, List<Exception> errors)
        {
            _trans = transformation;
            _result = default;
            Error = errors;
        }

        public Maybe(Func<T_In, T_Out> transformation)
        {
            _trans = AddMonad(transformation);
            _result = default;
        }

        public void Run(T_In i)
        {
            try
            {
                _result = _trans(i, this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _invalid = true;
                Error.Add(e);
            }
        }

        public T_Out Retrieve()
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


        Func<T_In, Maybe<T_In, T_New>, T_New> AddMonad<T_New>(Func<T_In, T_New> transformation)
        {
            return (test, maybe) => transformation(test);
        }

        Func<T_Out, Maybe<T_In, T_New>, T_New> AddMonad<T_New>(Func<T_Out, T_New> transformation)
        {
            return (test, maybe) => transformation(test);
        }

        public IMonad<T_In, T_New> Transform<T_New>(Func<T_Out, T_New> transformation) =>
            TransformMaybe(AddMonad(transformation));

        public Maybe<T_In, T_New> TransformMaybe<T_New>(Func<T_Out, Maybe<T_In, T_New>, T_New> transformation)
        {
            if (transformation == null) throw new ArgumentNullException(nameof(transformation));

            return new Maybe<T_In, T_New>((test, maybe) =>
            {
                try
                {
                    Run(test);
                    return transformation(Retrieve(), maybe);
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

        public bool HasValue() => !_invalid;

        private readonly Func<T_In, Maybe<T_In, T_Out>, T_Out> _trans;
        private T_Out? _result;
        private bool _invalid;

        public List<Exception> Error { get; } = new List<Exception>();
    }
}
