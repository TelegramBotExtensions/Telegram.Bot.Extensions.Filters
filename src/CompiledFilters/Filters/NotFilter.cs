using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates to true if the given <see cref="Filter{T}"/> doesn't and vise versa.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class NotFilter<T> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="NotFilter{T}"/> class,
        /// that evaluates to true if the given <see cref="Filter{T}"/> doesn't and vise versa.
        /// </summary>
        /// <param name="filter">The filter to evaluate.</param>
        public NotFilter(Filter<T> filter)
        {
            FilterExpression = Expression.Not(filter.FilterExpression);
        }
    }
}
