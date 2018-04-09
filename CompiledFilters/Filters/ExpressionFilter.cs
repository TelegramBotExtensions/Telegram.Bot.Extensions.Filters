using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CompiledFilters
{
    public sealed class ExpressionFilter<T> : Filter<T>
    {
        private readonly Expression expression;

        public ExpressionFilter(Expression<Func<T, bool>> expression)
        {
            var parameterReplacer = new ParameterReplacer(expression.Parameters.Single(), parameter);

            this.expression = parameterReplacer.Visit(expression);
        }

        public static implicit operator ExpressionFilter<T>(Expression<Func<T, bool>> predicate)
        {
            return new ExpressionFilter<T>(predicate);
        }

        protected override Expression GetFilterExpression()
        {
            return expression;
        }
    }
}
