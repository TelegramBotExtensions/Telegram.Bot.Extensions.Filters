using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    public sealed class OrFilter<T> : Filter<T>
    {
        private readonly Filter<T> lhs;
        private readonly Filter<T> rhs;

        public OrFilter(Filter<T> lhs, Filter<T> rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        protected override Expression GetFilterExpression()
        {
            return Expression.Or(GetFilterExpression(lhs), GetFilterExpression(rhs));
        }
    }
}
