using CompiledFilters;
using Telegram.Bot.Extensions.Filters;
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
                IsBot = true,
                Id = 123456
            },
            Text = "foo"
        };

        [Fact]
        public void Test1()
        {
            var filter = MessageFilters.FromBot.GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test2()
        {
            var filter = Filter.WithMethod<Message>(_testFilter).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test3()
        {
            var filter = (Filter.Using(
                            Filter.With((User user) => user.IsBot),
                            (Message msg) => msg.From)
                         & MessageFilters.Text("foo"));

            var compliedFilter = filter..GetCompiledFilter();

            Assert.True(compliedFilter(_message));
        }

        [Fact]
        public void Test4()
        {
            var filter = Filter.With((Message msg) => msg.From.IsBot).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        private static bool _testFilter(Message m) => true;
    }
}
