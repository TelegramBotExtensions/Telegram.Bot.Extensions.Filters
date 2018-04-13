using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    /// <summary>
    /// Implements a filter that evaluates the item based on a given expression.
    /// </summary>
    /// <typeparam name="T">The type of the items.</typeparam>
    internal sealed class ExpressionFilter<T> : Filter<T>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExpressionFilter{T}"/> class,
        /// that evaluates the item based on the given expression.
        /// </summary>
        /// <param name="predicate">The expression to use.</param>
        public ExpressionFilter(Expression<Predicate<T>> predicate)
        {
            var parameterReplacer = new ParameterReplacer(predicate.Parameters.Single(), Parameter);

            FilterExpression = parameterReplacer.Visit(predicate);
        }
    }

    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _source;
        private readonly Expression _target;

        internal ParameterReplacer(ParameterExpression source, Expression target)
        {
            _source = source;
            _target = target;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
            => Visit(node.Body);

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // Replace the source with the target, visit other params as usual.
            return node == _source ? _target : base.VisitParameter(node);
        }
    }
}
