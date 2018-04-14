using CompiledFilters;
using Telegram.Bot.Extensions.Filters.Messages;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters
{
    public static class MessageFilters
    {
        public static readonly IsBotFilter IsBot = new IsBotFilter();

        public static readonly Filter<Message> IsForwarded = Filter.With<Message>(m => m.ForwardFromChat != null || m.ForwardFrom != null);
    }
}
