using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private ParameterExpression source;
        private Expression target;

        public ParameterReplacer(ParameterExpression source, Expression target)
        {
            this.source = source;
            this.target = target;
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Visit(node.Body);
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            // Replace the source with the target, visit other params as usual.
            return node == source ? target : base.VisitParameter(node);
        }
    }
}
