using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    /// <summary>
    /// Block elements can be used inside of section, context, and actions layout blocks. Inputs can only be used inside of input blocks
    /// </summary>
    /// <remarks>
    /// https://api.slack.com/reference/block-kit/block-elements
    /// </remarks>
    public abstract class BlockElement
    {
        public BlockElement(string type)
        {
            Type = type;
        }

        [JsonProperty("type")] public string Type { get; set; }
    }

    public static class ElementType
    {
        public static string Button => "button";
        public static string DatePicker => "datepicker";
        public static string Image => "image";
        public static string MultiStaticSelect => "multi_static_select";
        public static string MultiExternalSelect => "multi_external_select";
        public static string MultiUserSelect => "multi_user_select";
        public static string MultiConversationSelect => "multi_conversation_select";
        public static string MultiChannelsSelect => "multi_channels_select";
        public static string OverflowMenu => "overflow";
        public static string PlainTextInput => "plain_text_input";
        public static string RadioButtons => "radio_buttons";
        public static string StaticSelect => "static_select";

    }
}