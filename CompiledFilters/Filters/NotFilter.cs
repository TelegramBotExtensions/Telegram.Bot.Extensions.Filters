using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    /// <summary>
    /// Implements a filter that evaluates to true if the given <see cref="Filter{T}"/> doesn't and vise versa.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public sealed class NotFilter<T> : Filter<T>
    {
        private readonly Filter<T> filter;

        /// <summary>
        /// Creates a new instance of the <see cref="NotFilter{T}"/> class,
        /// that evaluates to true if the given <see cref="Filter{T}"/> doesn't and vise versa.
        /// </summary>
        /// <param name="lhs">The filter to evaluate.</param>
        public NotFilter(Filter<T> filter)
        {
            this.filter = filter;
        }

        protected override Expression GetFilterExpression()
        {
            return Expression.Not(GetFilterExpression(filter));
        }
    }
}
