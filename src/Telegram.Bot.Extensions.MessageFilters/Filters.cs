using System;
using System.Collections.Generic;

namespace Telegram.Bot.Extensions.MessageFilters
{
    public static class Filters
    {
        public static readonly IsBotFilter IsBot = new IsBotFilter();
    }
}
