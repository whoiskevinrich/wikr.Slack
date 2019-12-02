using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public class SectionBlock : Block
    {
        public SectionBlock(Action<SectionBlock> configuration) : base(BlockType.Section)
        {
            configuration.Invoke(this);
        }

        [JsonProperty("text")] public TextComponent Text { get; set; }

        [JsonProperty("fields")] public IEnumerable<TextComponent> Fields { get; set; }

    }
}