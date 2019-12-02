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
            new ChatMessage(Channel, options => options.Text = "TEST");

        private ChatMessage BasicBlockMessage() =>
            new ChatMessage(Channel)
            {
                Blocks = new List<Block>
                {
                    new SectionBlock(block =>
                    {
                        block.Text = new TextComponent {Type = TextComponentType.MarkDown, Text = "*TEST* *TEST*"};
                    })
                }
            };

        private ChatMessage BlockMessageWithDivider() =>
            new ChatMessage(Channel)
            {
                Blocks = new List<Block>
                {
                    new SectionBlock(block =>
                    {
                        block.Text = new TextComponent {Text = "Basic Section Block", Type = TextComponentType.MarkDown};
                    }),
                    new DividerBlock {BlockId = Guid.NewGuid().ToString()},
                    new SectionBlock(block =>
                    {
                        block.Text = new TextComponent {Text = "Section Block With Fields", Type = TextComponentType.MarkDown};
                        block.Fields = new List<TextComponent>
                        {
                            new TextComponent {Text = "*Priority*", Type = TextComponentType.MarkDown},
                            new TextComponent {Text = "*Type*", Type = TextComponentType.MarkDown},

                            new TextComponent {Text = "High", Type = TextComponentType.PlainText},
                            new TextComponent {Text = "String", Type = TextComponentType.PlainText},
                        };
                    })
                }
            };
    }
}