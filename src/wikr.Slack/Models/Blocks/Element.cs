using System;
using Newtonsoft.Json;
using wikr.FluentSlack.Models;

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
        public BlockElement(string type) => Type = type;

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

    public static class Style
    {
        public static string Primary => "primary";
        public static string Danger => "danger";
    }

    public class ButtonElement : BlockElement
    {
        public ButtonElement(PlainText text, string actionId, Action<ButtonElement> configuration = null) : base(ElementType.Button)
        {
            Text = text;
            ActionId = actionId;
            configuration?.Invoke(this);
        }

        [JsonProperty("text")]
        public Text Text { get; set; }

        [JsonProperty("action_id")]
        public string ActionId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("value")] public string Value { get; set; }

        [JsonProperty("style")] public string Style { get; set; }

        [JsonProperty("confirm")] public ConfirmationDialog Confirm { get; set; }
    }

    /// <summary>
    /// An object that defines a dialog that provides a confirmation step to any interactive element. This dialog will ask the user to confirm their action by offering a confirm and deny buttons.
    /// </summary>
    /// <remarks>
    /// https://api.slack.com/reference/block-kit/composition-objects#confirm
    /// </remarks>
    public class ConfirmationDialog
    {
        public ConfirmationDialog(PlainText title, Text text, PlainText confirm, PlainText deny)
        {
            Title = title;
            Text = text;
            Confirm = confirm;
            Deny = deny;
        }

        /// <summary>
        /// the dialog's title
        /// </summary>
        public PlainText Title { get; set; }

        /// <summary>
        /// the explanatory text that appears in the confirm dialog
        /// </summary>
        public Text Text { get; set; }

        /// <summary>
        /// the text of the button that confirms the action
        /// </summary>
        public PlainText Confirm { get; set; }

        /// <summary>
        /// the text of the button that cancels the action
        /// </summary>
        public PlainText Deny { get; set; }
    }

    public class DatePickerElement : BlockElement
    {
        public DatePickerElement(string actionId, Action<DatePickerElement> configuration = null) : base(ElementType.DatePicker)
        {
            ActionId = actionId;
            configuration?.Invoke(this);
        }

        /// <summary>
        /// An identifier for the action triggered when a menu option is selected
        /// </summary>
        [JsonProperty("action_id")] public string ActionId { get; set; }

        /// <summary>
        /// defines the placeholder text shown on the datepicker
        /// </summary>
        [JsonProperty("placeholder")] public PlainText Placeholder { get; set; }

        /// <summary>
        /// The initial date that is selected when the element is loaded. This should be in the format YYYY-MM-DD.
        /// </summary>
        [JsonProperty("initial_date")] public string InitialDate { get; set; }

        /// <summary>
        /// optional confirmation dialog that appears after a date is selected
        /// </summary>
        [JsonProperty("confirm")] public ConfirmationDialog Confirm { get; set; }
    }

    public class ImageElement : BlockElement
    {
        public ImageElement(Uri imageUrl, string altText) : base(ElementType.Image)
        {
            ImageUrl = imageUrl;
            AltText = altText;
        }

        [JsonProperty("image_url")] public Uri ImageUrl { get; }
        [JsonProperty("alt_text")] public string AltText { get; }
    }


}