using System;
using System.Linq.Expressions;

namespace Telegram.Bot.Extensions.Filters.CompiledFilters.Filters
{
    public sealed class FuncFilter<T> : Filter<T>
    {
        private readonly MethodCallExpression _callFunc;

        public FuncFilter(Func<T, bool> predicate)
        {
            _callFunc = Expression.Call(
                predicate.Target == null
                    ? null
                    : Expression.Constant(predicate.Target),
                predicate.Method,
                Parameter);
        }

        public static implicit operator FuncFilter<T>(Func<T, bool> predicate)
            => new FuncFilter<T>(predicate);

        protected override Expression GetFilterExpression()
            => _callFunc;
    }
}
