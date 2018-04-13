using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a <see cref="Filter{T}"/> that's always true.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class TrueFilter<T> : Filter<T>
    {
        private static readonly ConstantExpression trueExpr = Expression.Constant(true, typeof(bool));

        internal override Expression FilterExpression
        {
            get { return trueExpr; }
            private protected set { }
        }
    }
}
