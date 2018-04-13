using System;
using System.Linq.Expressions;
using Telegram.Bot.Extensions.Filters.CompiledFilters.Filters;

// ReSharper disable StaticMemberInGenericType

namespace Telegram.Bot.Extensions.Filters.CompiledFilters
{
    /// <summary>
    /// Delegate for the compiled filtering functions created
    /// with the <see cref="Filter{T}"/> class and its derivatives.
    /// </summary>
    /// <typeparam name="T">The type of the items that are filtered.</typeparam>
    /// <param name="item">The item to be filtered.</param>
    /// <returns>Whether the given item satisfies the filter conditions.</returns>
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate bool CompiledFilter<T>(T item);

    /// <summary>
    /// The base class for all filters.
    /// <para/>
    /// Allows one to build easily readable expressions and compile them
    /// into a function that determines if they hold for a given input.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public abstract class Filter<T>
    {
        protected static readonly ConstantExpression FalseExpr = Expression.Constant(false, typeof(bool));
        protected static readonly ConstantExpression NullExpr = Expression.Constant(null);
        protected static readonly ParameterExpression Parameter = Expression.Parameter(typeof(T));
        protected static readonly ConstantExpression TrueExpr = Expression.Constant(true, typeof(bool));

        private protected Filter()
        { }

        public static implicit operator Filter<T>(Func<T, bool> predicate)
        {
            return (FuncFilter<T>)predicate;
        }

        public static implicit operator Filter<T>(Expression<Func<T, bool>> predicate)
            => (ExpressionFilter<T>)predicate;

        /// <summary>
        /// Negates the result of a <see cref="Filter{T}"/> using a <see cref="NotFilter{T}"/>.
        /// </summary>
        /// <param name="filter">The filter to evaluate.</param>
        /// <returns>The <see cref="NotFilter{T}"/> negating it.</returns>
        public static Filter<T> operator !(Filter<T> filter)
            => new NotFilter<T>(filter);

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using an <see cref="AndFilter{T}"/>.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>An <see cref="AndFilter{T}"/> joining them.</returns>
        public static Filter<T> operator &(Filter<T> lhs, Filter<T> rhs)
            => new AndFilter<T>(lhs, rhs);

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a <see cref="XorFilter{T}"/>.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>A <see cref="XorFilter{T}"/> joining them.</returns>
        public static Filter<T> operator ^(Filter<T> lhs, Filter<T> rhs)
            => new XorFilter<T>(lhs, rhs);

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using an <see cref="OrFilter{T}"/>.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>An <see cref="OrFilter{T}"/> joining them.</returns>
        public static Filter<T> operator |(Filter<T> lhs, Filter<T> rhs)
            => new OrFilter<T>(lhs, rhs);

        /// <summary>
        /// Compiles the current filter construction into a function
        /// that determines if the conditions hold for a given input.
        /// </summary>
        /// <returns>Whether the conditions are satisfied.</returns>
        public CompiledFilter<T> Compile()
            => Expression.Lambda<CompiledFilter<T>>(GetFilterExpression(), Parameter).Compile();

        /// <summary>
        /// Helper method to get the <see cref="Expression"/>s from other <see cref="Filter{T}"/>s in derived classes.
        /// </summary>
        /// <param name="filter">The <see cref="Filter{T}"/> to get the <see cref="Expression"/> from.</param>
        /// <returns></returns>
        private protected static Expression GetFilterExpression(Filter<T> filter)
            => filter.GetFilterExpression();

        /// <summary>
        /// Must return the <see cref="Expression"/> that represents the Filter.
        /// </summary>
        /// <returns>The <see cref="Expression"/> representing the Filter.</returns>
        private protected abstract Expression GetFilterExpression();
    }
}
