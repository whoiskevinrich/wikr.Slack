using Xunit;

namespace wikr.FluentSlack.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var slack = new Slack();
            slack.CreateMessage("test");
        }
    }
}
