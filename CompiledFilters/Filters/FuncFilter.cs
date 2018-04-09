using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    public sealed class FuncFilter<T> : Filter<T>
    {
        private MethodCallExpression callFunc;

        public FuncFilter(Func<T, bool> predicate)
        {
            callFunc = Expression.Call(predicate.Target == null ? null : Expression.Constant(predicate.Target), predicate.Method, parameter);
        }

        public static implicit operator FuncFilter<T>(Func<T, bool> predicate)
        {
            return new FuncFilter<T>(predicate);
        }

        protected override Expression GetFilterExpression()
        {
            return callFunc;
        }
    }
}
