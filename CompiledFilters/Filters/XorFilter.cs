using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    /// <summary>
    /// Implements a filter that evaluates to true if either one
    /// or the other of the other two <see cref="Filter{T}"/>s do.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public sealed class XorFilter<T> : Filter<T>
    {
        private readonly Filter<T> lhs;
        private readonly Filter<T> rhs;

        /// <summary>
        /// Creates a new instance of the <see cref="XorFilter{T}"/> class,
        /// that evaluates to true if either one or the other of the two given <see cref="Filter{T}"/>s does.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        public XorFilter(Filter<T> lhs, Filter<T> rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        protected override Expression GetFilterExpression()
        {
            return Expression.ExclusiveOr(GetFilterExpression(lhs), GetFilterExpression(rhs));
        }
    }
}
