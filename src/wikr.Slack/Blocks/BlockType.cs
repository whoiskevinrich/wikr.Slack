namespace wikr.FluentSlack
{
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
}