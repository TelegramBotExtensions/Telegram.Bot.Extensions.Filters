using System.Linq.Expressions;
using Telegram.Bot.Extensions.Filters.CompiledFilters;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Filters.Messages
{
    public sealed class TextFilter : Filter<Message>
    {
        private readonly ConstantExpression _text;

        private static readonly ConstantExpression TypeExpr
            = Expression.Constant(MessageType.Text, typeof(MessageType));

        public TextFilter(string text = default)
        {
            _text = Expression.Constant(text, typeof(string));
        }

        private protected override Expression GetFilterExpression()
        {
            var textProperty = Expression.Property(Parameter, nameof(Message.Text));
            var typeProperty = Expression.Property(Parameter, nameof(Message.Type));
            var typeCondition = Expression.NotEqual(typeProperty, TypeExpr);
            var nullTextParamCondition = Expression.Equal(textProperty, NullExpr);
            var textEqualityCondition = Expression.Equal(textProperty, _text);

            return Expression.Condition(typeCondition, FalseExpr,
                Expression.Condition(nullTextParamCondition, TrueExpr, textEqualityCondition));
        }
    }
}
