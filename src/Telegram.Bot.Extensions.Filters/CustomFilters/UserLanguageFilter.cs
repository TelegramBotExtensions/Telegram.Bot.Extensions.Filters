using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using CompiledFilters;
using Telegram.Bot.Types;

namespace Telegram.Bot.Extensions.Filters.CustomFilters
{
    internal sealed class UserLanguageFilter : CustomFilter<User>
    {
        private readonly CultureInfo cultureInfo;
        private readonly bool strict;

        public UserLanguageFilter(string ietfCode, bool strict)
        {
            cultureInfo = CultureInfo.GetCultureInfoByIetfLanguageTag(ietfCode);
            this.strict = strict;
        }

        protected override bool Matches(User user)
        {
            if (string.IsNullOrWhiteSpace(user.LanguageCode) && strict)
                return false;

            try
            {
                return CultureInfo.GetCultureInfoByIetfLanguageTag(user.LanguageCode).Equals(cultureInfo);
            }
            catch(CultureNotFoundException)
            {
                return false;
            }
        }
    }
}
