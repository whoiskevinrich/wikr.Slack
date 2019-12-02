namespace wikr.FluentSlack.Cli
{
    public class SlackConfiguration
    {
        public string ApiBaseAddress { get; set; } = "https://slack.com/api/";
        public string OAuthToken { get; set; }

        public string Channel { get; set; }
    }
}