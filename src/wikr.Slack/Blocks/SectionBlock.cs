using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public class SectionBlock : Block
    {
        public SectionBlock(Text text, Action<SectionBlock> configuration = null) : base(BlockType.Section)
        {
            Text = text;
            configuration?.Invoke(this);
        }

        [JsonProperty("block_id")] public string BlockId { get; set; }

        [JsonProperty("text")] public Text Text { get; set; }

        [JsonProperty("accessory")] public Accessory Accessory { get; set; }

        [JsonProperty("fields")] public IEnumerable<Text> Fields { get; set; }
    }

    public class Accessory
    {

    }

    public class ImageBlock : Block
    {
        public ImageBlock(Uri imageUrl, Action<ImageBlock> configuration = null) : base(BlockType.Image)
        {
            ImageUrl = imageUrl;
            configuration?.Invoke(this);
        }

        [JsonProperty("block_id")] public string BlockId { get; set; }

        [JsonProperty("image_url")] public Uri ImageUrl { get; set; }

        [JsonProperty("alt_text")] public string AltText { get; set; }

        [JsonProperty("Title")] public object Title { get; set; }
    }

    public class ActionsBlock : Block
    {
        public ActionsBlock(IEnumerable<BlockElement> elements, Action<ActionsBlock> configuration) : base(BlockType.Actions)
        {
            Elements = elements;
            configuration?.Invoke(this);
        }

        [JsonProperty("block_id")] public string BlockId { get; set; }

        [JsonProperty("elements")] public IEnumerable<BlockElement> Elements { get; set; }
    }

    public class ContextBlock : Block
    {
        public ContextBlock(IEnumerable<BlockElement> elements, Action<ContextBlock> configuration) : base(BlockType.Context)
        {
            Elements = elements;
            configuration?.Invoke(this);
        }

        [JsonProperty("block_id")] public string BlockId { get; set; }

        [JsonProperty("elements")] public IEnumerable<BlockElement> Elements { get; set; }
    }
}