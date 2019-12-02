using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public class ChatMessage
    {
        public ChatMessage(string channel, Action<ChatMessage> options = null)
        {
            Channel = channel;
            options?.Invoke(this);
        }

        [JsonProperty("channel")] 
        public string Channel { get; }

        [JsonProperty("text")]
        public string Text { get; set; }

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
}
