using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    /// <summary>
    /// Use this class to implement your own custom <see cref="Filter{T}"/>s!
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public abstract class CustomFilter<T> : Filter<T>
    {
        private readonly MethodCallExpression callFunc;

        public CustomFilter()
        {
            callFunc = Expression.Call(
                Expression.Constant(this),
                ((Predicate<T>)Matches).Method,
                Parameter);
        }

        private protected override Expression GetFilterExpression()
            => callFunc;

        /// <summary>
        /// Determines whether the given item matches the filter.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Whether the given item matches.</returns>
        protected abstract bool Matches(T item);
    }
}
