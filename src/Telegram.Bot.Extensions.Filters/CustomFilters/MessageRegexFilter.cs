using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters.CustomFilters
{
    internal sealed class MessageRegexFilter : CustomFilter<Message>
    {
        private readonly Regex regex;
        private readonly bool text;
        private readonly bool caption;

        private MessageRegexFilter(bool checkText, bool checkCaption)
        {
            text = checkText;
            caption = checkCaption;
        }

        public MessageRegexFilter(Regex regex, bool checkText, bool checkCaption)
            : this(checkText, checkCaption)
        {
            this.regex = regex ?? throw new ArgumentNullException(nameof(regex), "Regex can't be null!");
        }

        public MessageRegexFilter(string text, bool checkText, bool checkCaption)
            :this(checkText, checkCaption)
        {
            regex = new Regex(Regex.Escape(text ?? throw new ArgumentNullException(nameof(text), "Pattern can't be null!")));
        }

        protected override bool Matches(Message message)
        {
            return (text && message.Text != null && regex.IsMatch(message.Text))
                || (caption && message.Caption != null && regex.IsMatch(message.Caption));
        }
    }
}
