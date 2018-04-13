using System;
using System.Collections.Generic;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that transposes the item type for another filter and returns what it evaluates to.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    /// <typeparam name="S">The type that the items are transposed to.</typeparam>
    internal sealed class SelectFilter<T, S> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SelectFilter{T, S}"/> class,
        /// that transposes the item type for the given filter and returns what it evaluates to.
        /// </summary>
        /// <param name="transpose">The function that transposes from T to S.</param>
        /// <param name="filter">The filter to evaluate.</param>
        public SelectFilter(Func<T, S> transpose, Filter<S> filter)
        {
            var compiledFilter = filter.GetCompiledFilter();

            FilterExpression = compiledFilter.GetMethodCall(transpose.GetMethodCall(Parameter));
        }
    }
}
