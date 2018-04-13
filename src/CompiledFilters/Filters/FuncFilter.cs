using System;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates the item based on a given function.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class FuncFilter<T> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="FuncFilter{T}"/> class,
        /// that evaluates the item based on the given function.
        /// </summary>
        /// <param name="predicate">The function to use.</param>
        public FuncFilter(Predicate<T> predicate)
        {
            FilterExpression = predicate.GetMethodCall(Parameter);
        }
    }
}
