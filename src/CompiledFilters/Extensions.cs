using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    /// <summary>
    /// Contains helpful extension methods for <see cref="Filter{T}"/>s.
    /// </summary>
    public static class Extensions
    {
        internal static Expression GetInvocation(this Delegate function, Expression parameter)
            => Expression.Invoke(Expression.Constant(function), parameter);

        /// <summary>
        /// Checks whether the item satisfies the given <see cref="Filter{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item to check.</param>
        /// <param name="filter">The filter to use.</param>
        /// <returns>Whether the item satisfies the given <see cref="Filter{T}"/>.</returns>
        public static bool SatisfiesFilter<T>(this T item, CompiledFilter<T> filter)
             => filter(item);

        /// <summary>
        /// Checks whether the item satisfies the given <see cref="Filter{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The item to check.</param>
        /// <param name="filter">The filter to use.</param>
        /// <returns>Whether the item satisfies the given <see cref="Filter{T}"/>.</returns>
        public static bool SatisfiesFilter<T>(this T item, Filter<T> filter)
            => filter.GetCompiledFilter()(item);
    }
}
