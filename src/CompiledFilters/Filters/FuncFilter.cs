using System;
using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    internal sealed class FuncFilter<T> : Filter<T>
    {
        private readonly MethodCallExpression callFunc;

        public FuncFilter(Predicate<T> predicate)
        {
            callFunc = Expression.Call(
                predicate.Target == null
                    ? null
                    : Expression.Constant(predicate.Target),
                predicate.Method,
                Parameter);
        }

        public static implicit operator FuncFilter<T>(Predicate<T> predicate)
            => new FuncFilter<T>(predicate);

        private protected override Expression GetFilterExpression()
            => callFunc;
    }
}
