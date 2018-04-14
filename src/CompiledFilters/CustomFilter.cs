using System;
using System.Collections.Generic;

namespace CompiledFilters
{
    /// <summary>
    /// Use this class to implement your own custom <see cref="Filter{T}"/>s!
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public abstract class CustomFilter<T> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the derived <see cref="CustomFilter{T}"/>.
        /// </summary>
        protected CustomFilter()
        {
            FilterExpression = ((Predicate<T>)Matches).GetInvocation(Parameter);
        }

        /// <summary>
        /// Determines whether the given item matches the filter.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Whether the given item matches.</returns>
        protected abstract bool Matches(T item);
    }
}
