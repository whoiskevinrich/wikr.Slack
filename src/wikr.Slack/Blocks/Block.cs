using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    /// <remarks>
    /// https://api.slack.com/reference/block-kit/blocks
    /// </remarks>
    public abstract class Block
    {
        public Block(string type) => Type = type;

        [JsonProperty("type")] public string Type { get; }
    }
}