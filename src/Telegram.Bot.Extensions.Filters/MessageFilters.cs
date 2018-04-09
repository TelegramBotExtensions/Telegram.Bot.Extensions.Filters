using Telegram.Bot.Extensions.Filters.Messages;

namespace Telegram.Bot.Extensions.Filters
{
    public static class MessageFilters
    {
        public static readonly IsBotFilter IsBot = new IsBotFilter();
    }
}
