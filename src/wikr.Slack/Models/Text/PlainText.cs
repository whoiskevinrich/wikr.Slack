using System;

namespace wikr.FluentSlack.Models
{
    public class PlainText : Text
    {
        public PlainText(string text, Action<PlainText> configuration = null) : base(text, TextType.PlainText)
        {
            TextValue = text;
            configuration?.Invoke(this);
        }

        public new bool? Verbatim => null;
    }
}