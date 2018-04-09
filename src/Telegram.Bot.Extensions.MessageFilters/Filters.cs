using System;
using System.Collections.Generic;
using CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.MessageFilters
{
    public static class Filters
    {
        public static readonly IsBotFilter IsBot = new IsBotFilter();

        public static readonly Filter<Message> TestFilter = (Func<Message, bool>)test;

        public static readonly ExpressionFilter<Message> TestFilter2 = (Func<Message, bool>)(m => m.Type == Types.Enums.MessageType.Video);

        private static bool test(Message m) => m.Type == Types.Enums.MessageType.Text;
    }
}
