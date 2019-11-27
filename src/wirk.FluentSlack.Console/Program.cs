using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Refit;

namespace wikr.FluentSlack.Cli
{
    public class Program
    {
        private readonly IHost _host;

        public Program() => _host = CreateHostBuilder().UseConsoleLifetime().Build();

        public async Task StartAsync()
        {
            var application = _host.Services.GetRequiredService<Application>();
            await application.Go();
        }

        static async Task Main(string[] args)
        {
            var program = new Program();
            await program.StartAsync();
            await program.StopAsync();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .ConfigureHostConfiguration(config => config.AddUserSecrets<Program>())
                .ConfigureLogging(builder => builder.AddConsole().AddDebug())
                .ConfigureServices((hostContext, services) =>
                {
                    hostContext.HostingEnvironment.ApplicationName = "Fluent Slack Demo";

                    services.AddSingleton<Application>();

                    services.AddRefitClient<IChatApi>(new RefitSettings
                    {
                        ContentSerializer = new JsonContentSerializer(new JsonSerializerSettings
                            {Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore})
                    }).ConfigureHttpClient(client =>
                    {
                        client.BaseAddress = new Uri("https://slack.com/api/");
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", hostContext.Configuration["Slack:OAuthToken"]);
                    });
                })
                .UseConsoleLifetime();


        public async Task StopAsync()
        {
            using (_host)
            {
                await _host.StopAsync();
            }
        }
    }

    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IChatApi _chat;

        private const string Channel = "#wikr-slack-testing";

        public Application(ILogger<Application> logger, IChatApi chat)
        {
            _logger = logger;
            _chat = chat;
        }

        public async Task Go()
        {
            var messages = new List<ChatMessage>
            {
                //BasicBlockMessage(),
                //BasicChatMessage(),
                BlockMessageWithDivider()
            };

            foreach (var message in messages)
            {
                await TestAsync(message);
            }
        }



        public async Task TestAsync(ChatMessage request)
        {
            var response = await _chat.PostMessage(request);
            _logger.LogInformation("\nRequest:\n{Request}\n" +
                                   "\nResult:\n{Result}\n",
                request.ToJson(Formatting.Indented),
                response.ToJson(Formatting.Indented));
        }

        private ChatMessage BasicChatMessage() => 
            new ChatMessage(Channel, "TEST");

        private ChatMessage BasicBlockMessage() =>
            new ChatMessage(Channel)
            {
                Blocks = new List<Block>
                {
                    new Block(BlockType.Section)
                    {
                        Text = new TextBlock
                        {
                            Type = TextBlockType.MarkDown,
                            Text = "*TEST* with some content"
                        }
                    }
                }
            };

        private ChatMessage BlockMessageWithDivider() => 
            new ChatMessage(Channel)
            {
                Blocks = new List<Block>
                {
                    //new Block(BlockType.Section)
                    //{
                    //    Text = new TextBlock{Text = "Section 1", Type = TextBlockType.MarkDown}
                    //},
                    new Block(BlockType.Divider){BlockId = "blockid"},
                    //new Block(BlockType.Section)
                    //{
                    //    Text = new TextBlock{Text = "Section 2", Type = TextBlockType.MarkDown}
                    //}
                }
            };
    }
}
