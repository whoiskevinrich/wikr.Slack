using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public class TextComponent
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
    }
}