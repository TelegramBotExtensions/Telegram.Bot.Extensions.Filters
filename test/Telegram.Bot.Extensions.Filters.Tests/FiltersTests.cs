using CompiledFilters;
using Telegram.Bot.Extensions.Filters.Messages;
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
            var filter = MessageFilters.IsBot.GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test2()
        {
            var filter = Filter.FromMethod<Message>(testFilter).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test3()
        {
            var filter = (Filter.Select(
                              (Message msg) => msg.From,
                              Filter.FromLambda((User user) => user.IsBot))
                         & new TextFilter("foo")).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test4()
        {
            var filter = Filter.FromLambda((Message msg) => msg.From.IsBot).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        private static bool testFilter(Message m) => true;
    }
}
