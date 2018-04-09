using Telegram.Bot.Types;
using Xunit;

namespace Telegram.Bot.Extensions.Filters.Tests
{
    public class FiltersTests
    {
        private readonly Message _message = new Message
        {
            From = new User
            {
                IsBot = true
            },
            Text = "foo"
        };

        [Fact]
        public void Test1()
        {
            var filter = MessageFilters.IsBot.Compile();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test3()
        {

        }
    }
}
