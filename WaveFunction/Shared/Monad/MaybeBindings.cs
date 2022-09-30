namespace WaveFunction.Shared.Monad
{
    public static class MaybeBindings<TIn, TOut>
    {
        public static Func<TIn, Maybe<TIn, T_New>, T_New> AddMonad<T_New>(Func<TIn, T_New> transformation)
        {
            return (test, _) => transformation(test);
        }

        public static Func<TOut, Maybe<TIn, T_New>, T_New> AddMonad<T_New>(Func<TOut, T_New> transformation)
        {
            return (test, _) => transformation(test);
        }

        public static Maybe<TIn, TNew> TransformMaybe<TNew>(Maybe<TIn, TOut> self,
            Func<TOut, Maybe<TIn, TNew>, TNew> transformation)
        {
            if (transformation == null) throw new ArgumentNullException(nameof(transformation));

            return new Maybe<TIn, TNew>(BindTransform(self, transformation), self.Error);
        }

        public static Func<TIn, Maybe<TIn, TNew>, TNew> BindTransform<TNew>(Maybe<TIn, TOut> self,
            Func<TOut, Maybe<TIn, TNew>, TNew> transformation)
        {
            return (input, root) =>
            {
                try
                {
                    return Maybe<TIn,TOut>.EvaluateStep(self, transformation, root, input);
                }
                catch (Exception e)
                {
                    return Maybe<TIn,TOut>.HandleIssue(root, e);
                }
            };
        }

        public static Func<TOut, TNew> BindMaybe<TNew>(Func<TOut, Maybe<TIn, TNew>, TNew> transformation,
            Maybe<TIn, TNew> root)
        {
            return input => transformation(input, root);
        }
    }
}
