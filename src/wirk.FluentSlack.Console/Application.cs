using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

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
                //BasicBlockMessage(),
                //BasicChatMessage(),
                //BlockMessageWithDivider(),
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
            new Payload(Channel, message =>
            {
                message.Blocks = new Block[]
                {
                    new SectionBlock(block =>
                    {
                        block.Text = new Text();
                    })
                };
            });

        private Payload BasicChatMessage() => 
            new Payload(Channel, options => options.Text = "TEST");

        private Payload BasicBlockMessage() =>
            new Payload(Channel)
            {
                Blocks = new List<Block>
                {
                    new SectionBlock(block =>
                    {
                        block.Text = new Text {Type = TextComponentType.MarkDown, Text = "*TEST* *TEST*"};
                    })
                }
            };

        private Payload BlockMessageWithDivider() =>
            new Payload(Channel)
            {
                Blocks = new List<Block>
                {
                    new SectionBlock(block =>
                    {
                        block.Text = new Text {Text = "Basic Section Block", Type = TextComponentType.MarkDown};
                    }),
                    new DividerBlock {BlockId = Guid.NewGuid().ToString()},
                    new SectionBlock(block =>
                    {
                        block.Text = new Text {Text = "Section Block With Fields", Type = TextComponentType.MarkDown};
                        block.Fields = new List<Text>
                        {
                            new Text {Text = "*Priority*", Type = TextComponentType.MarkDown},
                            new Text {Text = "*Type*", Type = TextComponentType.MarkDown},

                            new Text {Text = "High", Type = TextComponentType.PlainText},
                            new Text {Text = "String", Type = TextComponentType.PlainText},
                        };
                    })
                }
            };
    }
}