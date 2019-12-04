using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public class DividerBlock : Block
    {
        public DividerBlock(string blockId = null) : base(BlockType.Divider) => BlockId = blockId;

        [JsonProperty("block_id")] public string BlockId { get; set; }

    }
}