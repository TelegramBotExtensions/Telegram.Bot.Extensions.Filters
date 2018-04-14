using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates the item based on a given expression.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class ExpressionFilter<T> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExpressionFilter{T}"/> class,
        /// that evaluates the item based on the given expression.
        /// </summary>
        /// <param name="predicate">The expression to use.</param>
        public ExpressionFilter(Expression<Predicate<T>> predicate)
        {
            var parameterReplacer = new ParameterReplacer(predicate.Parameters.Single(), Parameter);

            FilterExpression = parameterReplacer.Visit(predicate);
        }
    }
}
