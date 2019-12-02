using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Refit;

namespace wikr.FluentSlack.Cli
{
    public class Program
    {
        private readonly IHost _host;

        public Program(string[] args) => _host = CreateHostBuilder(args).UseConsoleLifetime().Build();

        public async Task StartAsync()
        {
            var application = _host.Services.GetRequiredService<Application>();
            await application.Go();
        }

        static async Task Main(string[] args)
        {
            var program = new Program(args);
            await program.StartAsync();
            await program.StopAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder => builder.AddConsole().AddDebug())
                .ConfigureServices((hostContext, services) =>
                {
                    hostContext.HostingEnvironment.ApplicationName = "Fluent Slack Demo";

                    services.Configure<SlackConfiguration>(hostContext.Configuration.GetSection("Slack"));

                    services.AddSingleton<Application>();


                    var slackHttpClientConfig = new Action<IServiceProvider, HttpClient>((provider, client) =>
                    {
                        var opts = provider.GetService<IOptions<SlackConfiguration>>().Value;
                        client.BaseAddress = new Uri(opts.ApiBaseAddress);
                        client.DefaultRequestHeaders.Authorization =
                            new AuthenticationHeaderValue("Bearer", opts.OAuthToken);
                    });

                    var refitSettings = new RefitSettings
                    {
                        ContentSerializer = new JsonContentSerializer(
                            new JsonSerializerSettings
                            {
                                Formatting = Formatting.None,
                                NullValueHandling = NullValueHandling.Ignore
                            })
                    };

                    services.AddRefitClient<IChatApi>(refitSettings).ConfigureHttpClient(slackHttpClientConfig);
                })
                .UseConsoleLifetime();




        public async Task StopAsync()
        {
            using var host = _host;
            await _host.StopAsync();
        }
    }
}
