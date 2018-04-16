using System.Text.RegularExpressions;
using CompiledFilters;
using Telegram.Bot.Extensions.Filters.CustomFilters;
using Telegram.Bot.Extensions.Filters.Messages;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.Extensions.Filters
{
    /// <summary>
    /// Contains <see cref="Filter{T}"/>s for <see cref="Message"/>s.
    /// </summary>
    public static class MessageFilters
    {
        /// <summary>
        /// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> came from a bot.
        /// </summary>
        public static readonly Filter<Message> FromBot = UserFilters.IsBot.Using((Message msg) => msg.From);

        /// <summary>
        /// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> was forwarded from a <see cref="User"/>.
        /// </summary>
        public static readonly Filter<Message> ForwardedFromUser = Filter.With<Message>(msg => msg.ForwardFrom != null);

        /// <summary>
        /// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> was forward from a Channel.
        /// </summary>
        public static readonly Filter<Message> ForwardedFromChannel = Filter.With<Message>(msg => msg.ForwardFromChat != null);

        /// <summary>
        /// A <see cref="Filter{T}"/> that checks if a <see cref="Message"/> was forwarded at all.
        /// </summary>
        public static readonly Filter<Message> IsForwarded = ForwardedFromUser | ForwardedFromChannel;

        /// <summary>
        /// Creates a <see cref="Filter{T}"/> that matches the
        /// <see cref="Message.Text"/> and/or <see cref="Message.Caption"/> against a regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to match against.</param>
        /// <param name="checkText">Whether to check the <see cref="Message.Text"/>.</param>
        /// <param name="checkCaption">Whether to check the <see cref="Message.Caption"/>.</param>
        /// <returns>A <see cref="Filter{T}"/> that matches a <see cref="Message"/>'s text and/or caption against a regular expression.</returns>
        public static Filter<Message> Regex(Regex regex, bool checkText = true, bool checkCaption = true)
            => new MessageRegexFilter(regex, checkText, checkCaption);

        /// <summary>
        /// Creates a <see cref="Filter{T}"/> that matches the
        /// <see cref="Message.Text"/> and/or <see cref="Message.Caption"/> against a regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to match against.</param>
        /// <param name="checkText">Whether to check the <see cref="Message.Text"/>.</param>
        /// <param name="checkCaption">Whether to check the <see cref="Message.Caption"/>.</param>
        /// <returns>A <see cref="Filter{T}"/> that matches a <see cref="Message"/>'s text and/or caption against a regular expression.</returns>
        public static Filter<Message> Regex(string regex, bool checkText = true, bool checkCaption = true)
            => new MessageRegexFilter(new Regex(regex), checkText, checkCaption);

        /// <summary>
        /// Creates a <see cref="Filter{T}"/> that matches the
        /// <see cref="Message.Text"/> and/or <see cref="Message.Caption"/> against a literal text.
        /// </summary>
        /// <param name="text">The literal text to match against.</param>
        /// <param name="checkText">Whether to check the <see cref="Message.Text"/>.</param>
        /// <param name="checkCaption">Whether to check the <see cref="Message.Caption"/>.</param>
        /// <returns>A <see cref="Filter{T}"/> that matches a <see cref="Message"/>'s text and/or caption against a literal text.</returns>
        public static Filter<Message> Text(string text, bool checkText = true, bool checkCaption = true)
            => new MessageRegexFilter(text, checkText, checkCaption);

        /// <summary>
        /// Creates a <see cref="Filter{T}"/> that checks the <see cref="Message.Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="MessageType"/> to match.</param>
        /// <returns>A <see cref="Filter{T}"/> that checks the <see cref="Message.Type"/>.</returns>
        public static Filter<Message> Type(MessageType type)
            => Filter.With<Message>(msg => msg.Type == type);
    }
}
