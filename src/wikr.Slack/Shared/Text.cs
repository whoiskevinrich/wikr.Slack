using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public class Text
    {
        public Text(string text, string type = "mrkdwn")
        {
            TextValue = text;
            Type = type;
        }

        [JsonProperty("type")] public string Type { get; set; }
        
        
        [JsonProperty("text")] public string TextValue { get; set; }

        /// <summary>
        /// Indicates whether emojis in a text field should be escaped into the colon emoji format. This field is only usable when type is plain_text
        /// </summary>
        [JsonProperty("emoji")] public bool? Emoji { get; set; }

        /// <summary>
        /// When set to false (as is default) URLs will be auto-converted into links, conversation names will be link-ified, and certain mentions will be automatically parsed. 
        /// Using a value of true will skip any preprocessing of this nature, although you can still include manual parsing strings. This field is only usable when type is mrkdwn.
        /// </summary>
        [JsonProperty("verbatim")] public bool? Verbatim { get; set; }
    }
}