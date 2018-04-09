using Telegram.Bot.Extensions.Filters.CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters
{
    public static class MessageExtensions
    {
        public static bool SatisfiesFilter(this Message message, CompiledFilter<Message> filter)
        {
            return filter(message);
        }
    }
}
