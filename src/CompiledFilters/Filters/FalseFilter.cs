using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a <see cref="Filter{T}"/> that's always false.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class FalseFilter<T> : Filter<T>
    {
        private static readonly ConstantExpression falseExpr = Expression.Constant(false, typeof(bool));

        internal override Expression FilterExpression
        {
            get { return falseExpr; }
            private protected set { }
        }
    }
}
