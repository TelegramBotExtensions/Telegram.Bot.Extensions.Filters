using System;
using System.Linq.Expressions;
using CompiledFilters.Filters;

namespace CompiledFilters
{
    /// <summary>
    /// Delegate for the compiled filtering functions created
    /// with the <see cref="Filter{T}"/> class and its derivatives.
    /// </summary>
    /// <typeparam name="T">The type of the items that are filtered.</typeparam>
    /// <param name="item">The item to be filtered.</param>
    /// <returns>Whether the given item satisfies the filter conditions.</returns>
    // ReSharper disable once TypeParameterCanBeVariant
    public delegate bool CompiledFilter<in T>(T item);

    /// <summary>
    /// Contains factory methods for the different <see cref="Filter{T}"/>s.
    /// </summary>
    public static class Filter
    {
        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a binary and.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>The joined <see cref="Filter{T}"/>s.</returns>
        public static Filter<T> And<T>(Filter<T> lhs, Filter<T> rhs) => lhs & rhs;

        /// <summary>
        /// Gives a <see cref="Filter{T}"/> that always evaluates to false.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <returns>A <see cref="Filter{T}"/> that always evaluates to false.</returns>
        public static Filter<T> False<T>() => new FalseFilter<T>();

        /// <summary>
        /// Negates the result of a <see cref="Filter{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="filter">The filter to evaluate.</param>
        /// <returns>The negated <see cref="Filter{T}"/>.</returns>
        public static Filter<T> Not<T>(Filter<T> filter) => !filter;

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a binary or.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>The joined <see cref="Filter{T}"/>s.</returns>
        public static Filter<T> Or<T>(Filter<T> lhs, Filter<T> rhs) => lhs | rhs;

        /// <summary>
        /// Gives a <see cref="Filter{T}"/> that transposes the
        /// item type for the other filter and returns what it evaluates to.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <typeparam name="S">The type that the items are transposed to.</typeparam>
        /// <param name="transpose">The function that transposes from T to S.</param>
        /// <param name="filter">The filter to evaluate.</param>
        /// <returns>A <see cref="Filter{T}"/> that transposes the
        /// item type for the other filter and returns what it evaluates to.</returns>
        public static Filter<T> Select<T, S>(Expression<Func<T, S>> transpose, Filter<S> filter)
            => new SelectFilter<T, S>(transpose, filter);

        /// <summary>
        /// Gives a <see cref="Filter{T}"/> that transposes the
        /// item type for the other filter and returns what it evaluates to.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <typeparam name="S">The type that the items are transposed to.</typeparam>
        /// <param name="transpose">The function that transposes from T to S.</param>
        /// <param name="filter">The filter to evaluate.</param>
        /// <returns>A <see cref="Filter{T}"/> that transposes the
        /// item type for the other filter and returns what it evaluates to.</returns>
        public static Filter<T> SelectMethod<T, S>(Func<T, S> transpose, Filter<S> filter)
            => new SelectFilter<T, S>(item => transpose(item), filter);

        /// <summary>
        /// Gives a <see cref="Filter{T}"/> that always evaluates to true.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <returns>A <see cref="Filter{T}"/> that always evaluates to true.</returns>
        public static Filter<T> True<T>() => new TrueFilter<T>();

        /// <summary>
        /// Creates a <see cref="Filter{T}"/> from a Lambda Expression.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="predicate">The filter function.</param>
        /// <returns>The <see cref="Filter{T}"/> using the given function.</returns>
        public static Filter<T> With<T>(Expression<Predicate<T>> predicate) => new ExpressionFilter<T>(predicate);

        /// <summary>
        /// Creates a <see cref="Filter{T}"/> from a delegate.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="predicate">The filter function.</param>
        /// <returns>The <see cref="Filter{T}"/> using the given function.</returns>
        public static Filter<T> WithMethod<T>(Predicate<T> predicate) => new ExpressionFilter<T>(item => predicate(item));

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a binary xor.
        /// </summary>
        /// <typeparam name="T">The type of the items.</typeparam>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>The joined <see cref="Filter{T}"/>s</returns>
        public static Filter<T> Xor<T>(Filter<T> lhs, Filter<T> rhs) => lhs ^ rhs;
    }

    /// <summary>
    /// The base class for all filters. Use <see cref="CustomFilter{T}"/> to derive your own.
    /// <para/>
    /// Allows one to build easily readable expressions and compile them
    /// into a function that determines if they hold for a given input.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    public abstract class Filter<T>
    {
        /// <summary>
        /// The parameter refering to the item to be checked.
        /// </summary>
        private protected static readonly ParameterExpression Parameter = Expression.Parameter(typeof(T));

        private readonly Lazy<CompiledFilter<T>> compiledFilter;

        /// <summary>
        /// Gets the <see cref="Expression"/> representing this Filter.
        /// </summary>
        internal virtual Expression FilterExpression { get; private protected set; }

        private protected Filter()
        {
            compiledFilter = new Lazy<CompiledFilter<T>>(() =>
                Expression.Lambda<CompiledFilter<T>>(FilterExpression, Parameter).Compile());
        }

        /// <summary>
        /// Negates the result of a <see cref="Filter{T}"/>.
        /// </summary>
        /// <param name="filter">The filter to evaluate.</param>
        /// <returns>The negated <see cref="Filter{T}"/>.</returns>
        public static Filter<T> operator !(Filter<T> filter)
            => new NotFilter<T>(filter);

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a binary and.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>The joined <see cref="Filter{T}"/>s.</returns>
        public static Filter<T> operator &(Filter<T> lhs, Filter<T> rhs)
            => new AndFilter<T>(lhs, rhs);

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a binary xor.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>The joined <see cref="Filter{T}"/>s</returns>
        public static Filter<T> operator ^(Filter<T> lhs, Filter<T> rhs)
            => new XorFilter<T>(lhs, rhs);

        /// <summary>
        /// Links two <see cref="Filter{T}"/>s together using a binary or.
        /// </summary>
        /// <param name="lhs">The first filter to evaluate.</param>
        /// <param name="rhs">The second filter to evaluate.</param>
        /// <returns>The joined <see cref="Filter{T}"/>s.</returns>
        public static Filter<T> operator |(Filter<T> lhs, Filter<T> rhs)
            => new OrFilter<T>(lhs, rhs);

        /// <summary>
        /// Compiles the current filter construction into a function
        /// that determines if the conditions hold for a given input.
        /// </summary>
        /// <returns>Whether the conditions are satisfied.</returns>
        public CompiledFilter<T> GetCompiledFilter()
            => compiledFilter.Value;
    }
}
