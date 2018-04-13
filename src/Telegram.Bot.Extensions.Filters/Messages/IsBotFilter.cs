using CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters.Messages
{
    public sealed class IsBotFilter : CustomFilter<Message>
    {
        internal IsBotFilter()
        { }

        protected override bool Matches(Message item)
        {
            return item.From?.IsBot ?? false;
        }
    }
}
