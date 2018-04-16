using System;
using System.Collections.Generic;
using System.Text;
using CompiledFilters;
using Telegram.Bot.Extensions.Filters.CustomFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters
{
    /// <summary>
    /// Contains <see cref="Filter{T}"/>s for <see cref="User"/>s.
    /// </summary>
    public static class UserFilters
    {
        /// <summary>
        /// A <see cref="Filter{User}"/> that checks if a <see cref="User"/> is a bot.
        /// </summary>
        public static readonly Filter<User> IsBot = Filter.With<User>(user => user.IsBot);

        /// <summary>
        /// A <see cref="Filter{User}"/> that checks if a <see cref="User"/> has a lastname.
        /// </summary>
        public static readonly Filter<User> HasLastname = Filter.With<User>(user => !string.IsNullOrWhiteSpace(user.LastName));

        /// <summary>
        /// A <see cref="Filter{User}"/> that checks if a <see cref="User"/> has a username.
        /// </summary>
        public static readonly Filter<User> HasUsername = Filter.With<User>(user => !string.IsNullOrWhiteSpace(user.Username));

        /// <summary>
        /// Creates a <see cref="Filter{User}"/> that checks a <see cref="User"/>'s IETF language code.
        /// <para/>
        /// https://en.wikipedia.org/wiki/IETF_language_tag
        /// </summary>
        /// <param name="ietfCode">The IETF code to look filter for.</param>
        /// <param name="strict">Whether no set code is treated as false.</param>
        /// <returns>A <see cref="Filter{User}"/> that checks for a user's IETF language code.</returns>
        public static Filter<User> HasLanguage(string ietfCode, bool strict = true)
            => new UserLanguageFilter(ietfCode, strict);
    }
}
