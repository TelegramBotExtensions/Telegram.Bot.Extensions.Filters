using System;
using System.Linq;
using System.Linq.Expressions;

namespace Telegram.Bot.Extensions.Filters.CompiledFilters.Filters
{
    public sealed class ExpressionFilter<T> : Filter<T>
    {
        private readonly Expression _expression;

        public ExpressionFilter(Expression<Func<T, bool>> expression)
        {
            var parameterReplacer = new ParameterReplacer(expression.Parameters.Single(), Parameter);

            _expression = parameterReplacer.Visit(expression);
        }

        public static implicit operator ExpressionFilter<T>(Expression<Func<T, bool>> predicate)
            => new ExpressionFilter<T>(predicate);

        private protected override Expression GetFilterExpression()
            => _expression;
    }
}
