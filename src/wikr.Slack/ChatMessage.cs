using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>https://api.slack.com/reference/messaging/payload</remarks>
    public class Payload
    {
        public Payload(string channel, string text, Action<Payload> options = null)
        {
            Channel = channel;
            Text = text;
            options?.Invoke(this);
        }

        [JsonProperty("channel")] 
        public string Channel { get; }

        /// <summary>
        /// The usage of this field changes depending on whether you're using blocks or not.
        ///  If you are, this is used as a fallback string to display in notifications.
        ///   If you aren't, this is the main body text of the message.
        ///    It can be formatted as plain text, or with mrkdwn.
        ///     This field is not enforced as required when using blocks, however it is highly recommended that you include it as the aforementioned fallback.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("as-user")]
        public bool? AsUser { get; set; }

        /// <summary>
        /// An array of legacy secondary attachments. We recommend you use blocks instead.
        /// </summary>
        [JsonProperty("attachments")]
        [Obsolete("Use Blocks")]
        public IEnumerable<Attachment> Attachments { get; set; }

        /// <summary>
        /// An array of layout blocks in the same format as described in the building blocks guide.
        /// </summary>
        [JsonProperty("blocks")]
        public IEnumerable<Block> Blocks { get; set; }

        [JsonProperty("icon_emoji")]
        public string IconEmoji { get; set; }

        [JsonProperty("icon_url")]
        public string IconUrl { get; set; }

        [JsonProperty("link_names")]
        public bool? LinkNames { get; set; }

        /// <summary>
        /// Determines whether the text field is rendered according to mrkdwn formatting or not. Defaults to true.
        /// </summary>
        [JsonProperty("mrkdwn")]
        public bool? UseMarkdown { get; set; }

        [JsonProperty("parse")]
        public string ParseMode { get; set; }

        [JsonProperty("reply_broadcast")]
        public bool? ReplyBroadcast { get; set; }

        /// <summary>
        /// The ID of another un-threaded message to reply to.
        /// </summary>
        [JsonProperty("thread_ts")]
        public string ThreadStamp { get; set; }

        [JsonProperty("unfurl_links")]
        public bool? UnfurlLinks { get; set; }

        [JsonProperty("unfurl_media")]
        public bool? UnfurlMedia { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }


        [JsonProperty("thread_ts")] public string ThreadTs { get; set; }

        public string ToJson(Formatting formatting = Formatting.None) =>
            JsonConvert.SerializeObject(this, new JsonSerializerSettings
            {
                Formatting = formatting,
                NullValueHandling = NullValueHandling.Ignore
            });
    }
}
