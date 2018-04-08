using System;
using System.Collections.Generic;
using CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.MessageFilters
{
    public static class MessageExtensions
    {
        public static bool SatisfiesFilter(this Message message, CompiledFilter<Message> filter)
        {
            return filter(message);
        }
    }
}
