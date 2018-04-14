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
                IsBot = true,
                Id = 123456
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
            var filter = Filter.WithMethod<Message>(testFilter).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test3()
        {
            var filter = (Filter.Select(
                              (Message msg) => msg.From,
                              Filter.With((User user) => user.IsBot))
                         & new TextFilter("foo")).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        [Fact]
        public void Test4()
        {
            var filter = Filter.With((Message msg) => msg.From.IsBot).GetCompiledFilter();

            Assert.True(filter(_message));
        }

        // Compiler prevents Expression with body.
        //[Fact]
        //public void TestExpressionBody()
        //{
        //    var filter = Filter.With((Message msg) =>
        //    {
        //        var userId = msg.From.Id;
        //        ++userId;

        //        return userId > 1;
        //    }).GetCompiledFilter();

        //    Assert.True(filter(_message));
        //}

        private static bool testFilter(Message m) => true;
    }
}
