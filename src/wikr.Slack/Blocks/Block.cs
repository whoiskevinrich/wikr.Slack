using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public abstract class Block
    {
        public Block(string type) => Type = type;

        [JsonProperty("type")] public string Type { get; }
        [JsonProperty("block_id")] public string BlockId { get; set; }
    }
}