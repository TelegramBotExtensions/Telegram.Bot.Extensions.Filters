using System.Linq.Expressions;
using Telegram.Bot.Extensions.Filters.CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters.Messages
{
    public sealed class IsBotFilter : Filter<Message>
    {
        internal IsBotFilter()
        { }

        private protected override Expression GetFilterExpression()
        {
            var fromProperty = Expression.Property(Parameter, nameof(Message.From));
            var isBotProperty = Expression.Property(fromProperty, nameof(User.IsBot));
            var condition = Expression.Equal(fromProperty, NullExpr);

            return Expression.Condition(condition, FalseExpr, isBotProperty);
        }
    }
}
