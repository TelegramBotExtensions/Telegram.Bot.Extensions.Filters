using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates to true if either one
    /// or the other of the other two <see cref="Filter{T}"/>s do.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class XorFilter<T> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="XorFilter{T}"/> class,
        /// that evaluates to true if either one or the other of the two given <see cref="Filter{T}"/>s does.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        public XorFilter(Filter<T> lhs, Filter<T> rhs)
        {
            FilterExpression = Expression.ExclusiveOr(lhs.FilterExpression, rhs.FilterExpression);
        }
    }
}
