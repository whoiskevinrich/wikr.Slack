using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using wikr.FluentSlack.Models;

namespace wikr.FluentSlack.Cli
{
    public class Application
    {
        private readonly ILogger<Application> _logger;
        private readonly IChatApi _chat;
        private readonly IOptions<SlackConfiguration> _options;

        private string Channel => _options.Value.Channel;

        public Application(ILogger<Application> logger, IChatApi chat, IOptions<SlackConfiguration> options)
        {
            _logger = logger;
            _chat = chat;
            _options = options;
        }

        public async Task Go()
        {
            var messages = new List<Payload>
            {
                BasicBlockMessage(),
                BasicChatMessage(),
                BlockMessageWithDivider(),
                PaperCompanyBlockKitBuilderDemo(),
            };

            foreach (var message in messages)
            {
                await TestAsync(message);
            }
        }



        public async Task TestAsync(Payload request)
        {
            var response = await _chat.PostMessage(request);
            _logger.LogInformation("\nRequest:\n{Request}\n" +
                                   "\nResult:\n{Result}\n",
                request.ToJson(Formatting.Indented),
                response.ToJson(Formatting.Indented));
        }

        private Payload PaperCompanyBlockKitBuilderDemo() =>
            new Payload(Channel, nameof(PaperCompanyBlockKitBuilderDemo), message =>
            {
                message.Blocks = new Block[]
                {
                    new SectionBlock(new PlainText(nameof(PaperCompanyBlockKitBuilderDemo) + "FallBackText"), block => { block.Text = new Text("PAPERCOMPANYDEMO TODO"); })
                };
            });

        private Payload BasicChatMessage() => 
            new Payload(Channel, nameof(BasicChatMessage));

        private Payload BasicBlockMessage() =>
            new Payload(Channel, nameof(BasicBlockMessage), payload =>
            {
                payload.Blocks = new Block[]
                {
                    new SectionBlock(new PlainText(nameof(BasicBlockMessage)))
                };
            });

        private Payload BlockMessageWithDivider() =>
            new Payload(Channel, nameof(BlockMessageWithDivider))
            {
                Blocks = new Block[]
                {
                    new SectionBlock(new Text("Basic Section Block")),
                    new DividerBlock(Guid.NewGuid().ToString()),
                    new SectionBlock(new Text("Section Block with Fields"), block =>
                    {
                        block.Fields = new[]
                        {
                            new Text("*Priority*"),
                            new Text("*Type*"), 
                            new PlainText("High"),
                            new PlainText("String") 
                        };
                    }),
                }
            };
    }
}