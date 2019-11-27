using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;

namespace wikr.FluentSlack
{
    public class ChatMessage
    {
        public ChatMessage(string channel, string text = null)
        {
            Channel = channel;
            Text = text;
        }

        [JsonProperty("channel")] 
        public string Channel { get; }

        [JsonProperty("text")]
        public string Text { get; }

        [JsonProperty("as-user")]
        public bool? AsUser { get; set; }

        [JsonProperty("attachments")]
        public IEnumerable<Attachment> Attachments { get; set; }

        [JsonProperty("blocks")]
        public IEnumerable<Block> Blocks { get; set; }

        [JsonProperty("icon_emoji")]
        public string IconEmoji { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("link_names")]
        public bool? LinkNames { get; set; }

        [JsonProperty("mrkdwn")]
        public bool? UseMarkdown { get; set; }

        [JsonProperty("parse")]
        public string ParseMode { get; set; }

        [JsonProperty("reply_broadcast")]
        public bool? ReplyBroadcast { get; set; }

        [JsonProperty("thread_ts")]
        public string ThreadStamp { get; set; }

        [JsonProperty("unfurl_links")]
        public bool? UnfurlLinks { get; set; }

        [JsonProperty("unfurl_media")]
        public bool? UnfurlMedia { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        public string ToJson(Formatting formatting = Formatting.None) =>
            JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                Formatting = formatting,
                NullValueHandling = NullValueHandling.Ignore
            });
    }

    public class SlackResponse
    {
        [JsonProperty("ok")]
        public bool IsSuccessful { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("ts")]
        public string ThreadStamp { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        public static SlackResponse FromString(string message)
            => JsonConvert.DeserializeObject<SlackResponse>(message, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        public string ToJson(Formatting formatting = Formatting.None) =>
            JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = formatting
            });
    }

    public class Block
    {
        public Block(string type) => Type = type;

        [JsonProperty("type")] public string Type { get; }
        [JsonProperty("text")] public TextBlock Text { get; set; }
        [JsonProperty("block_id")] public string BlockId { get; set; }
    }

    public static class BlockType
    {
        public static string Actions => nameof(Actions).ToLowerInvariant();

        public static string Context => nameof(Context).ToLowerInvariant();

        public static string Divider => nameof(Divider).ToLowerInvariant();

        public static string File => nameof(File).ToLowerInvariant();

        public static string Image => nameof(Image).ToLowerInvariant();

        public static string Input => nameof(Input).ToLowerInvariant();

        public static string Section => nameof(Section).ToLowerInvariant();
    }

    public class TextBlock
    {
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
    }

    public class Attachment { }

    [Headers("Content-Type: application/json;charset=utf-8")]
    public interface IChatApi
    {
        [Post("/chat.postMessage")]
        Task<SlackResponse> PostMessage(ChatMessage message);
    }



    public static class TextBlockType
    {
        public static string MarkDown => "mrkdwn";
        public static string PlainText => "plain_text";
    }
}
