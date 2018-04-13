using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompiledFilters.Filters
{
    internal sealed class ExpressionFilter<T> : Filter<T>
    {
        private readonly Expression expression;

        public ExpressionFilter(Expression<Predicate<T>> predicate)
        {
            var parameterReplacer = new ParameterReplacer(predicate.Parameters.Single(), Parameter);

            expression = parameterReplacer.Visit(predicate);
        }

        private protected override Expression GetFilterExpression()
            => expression;
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
