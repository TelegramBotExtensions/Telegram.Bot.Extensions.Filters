using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    /// <summary>
    /// <see cref="ExpressionVisitor"/> that returns only the body of a Lambda
    /// and replaces all references to a specific parameter in it with another one.
    /// </summary>
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
