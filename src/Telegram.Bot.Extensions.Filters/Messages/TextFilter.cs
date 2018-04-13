using CompiledFilters;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Filters.Messages
{
    public sealed class TextFilter : CustomFilter<Message>
    {
        private readonly string _text;

        public TextFilter(string text)
        {
            _text = text;
        }

        protected override bool Matches(Message item)
        {
            return item.Type == MessageType.Text && item.Text == _text;
        }
    }
}
