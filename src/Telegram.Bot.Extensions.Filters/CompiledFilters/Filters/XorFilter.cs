using System.Linq.Expressions;

namespace Telegram.Bot.Extensions.Filters.CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates to true if either one
    /// or the other of the other two <see cref="Filter{T}"/>s do.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class XorFilter<T> : Filter<T>
    {
        private readonly Filter<T> _lhs;
        private readonly Filter<T> _rhs;

        /// <summary>
        /// Creates a new instance of the <see cref="XorFilter{T}"/> class,
        /// that evaluates to true if either one or the other of the two given <see cref="Filter{T}"/>s does.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        public XorFilter(Filter<T> lhs, Filter<T> rhs)
        {
            _lhs = lhs;
            _rhs = rhs;
        }

        private protected override Expression GetFilterExpression()
            => Expression.ExclusiveOr(
                GetFilterExpression(_lhs),
                GetFilterExpression(_rhs));
    }
}
