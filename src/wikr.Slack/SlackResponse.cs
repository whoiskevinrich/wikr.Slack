using System;
using Newtonsoft.Json;

namespace wikr.FluentSlack
{
    public partial class SlackResponse
    {
        [JsonProperty("ok")]
        public bool Ok { get; set; }

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

    public static class SlackMessageHelpers
    {
        public static bool IsSuccessful(this SlackResponse response) => response.Ok;
    }

    public partial class SlackResponse
    {
        [JsonProperty("user")] public User User { get; set; }
    }

    /// <summary>
    /// A user object contains information about a member
    /// </summary>
    /// <remarks>
    /// See: https://api.slack.com/types/user
    /// </remarks>
    public partial class User
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("deleted")]
        public bool? Deleted { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        /// <summary>
        /// The time-zone of the user
        /// </summary>
        [JsonProperty("tz")]
        public string Tz { get; set; }

        /// <summary>
        /// The description of the user's timezone
        /// </summary>
        [JsonProperty("tz_label")]
        public string TzLabel { get; set; }

        /// <summary>
        /// The UTC offset of the user's timezone in seconds
        /// </summary>
        [JsonProperty("tz_offset")]
        public long TzOffset { get; set; }

        [JsonProperty("profile")]
        public Profile Profile { get; set; }

        [JsonProperty("is_admin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("is_owner")]
        public bool IsOwner { get; set; }

        [JsonProperty("is_primary_owner")]
        public bool IsPrimaryOwner { get; set; }

        /// <summary>
        /// Indicates the user is a multi-channel guest
        /// </summary>
        [JsonProperty("is_restricted")]
        public bool IsRestricted { get; set; }

        /// <summary>
        /// Indicates the user is a single-channel guest
        /// </summary>
        [JsonProperty("is_ultra_restricted")]
        public bool IsUltraRestricted { get; set; }

        [JsonProperty("is_bot")]
        public bool IsBot { get; set; }

        [JsonProperty("is_app_user")]
        public bool IsAppUser { get; set; }

        [JsonProperty("updated")]
        public long Updated { get; set; }

        [JsonProperty("has_2fa")]
        public bool Has2Fa { get; set; }
    }

    public partial class Profile
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("skype")]
        public string Skype { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("real_name_normalized")]
        public string RealNameNormalized { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("display_name_normalized")]
        public string DisplayNameNormalized { get; set; }

        [JsonProperty("status_text")]
        public string StatusText { get; set; }

        [JsonProperty("status_emoji")]
        public string StatusEmoji { get; set; }

        [JsonProperty("status_expiration")]
        public long StatusExpiration { get; set; }

        [JsonProperty("avatar_hash")]
        public string AvatarHash { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("image_24")]
        public Uri Image24 { get; set; }

        [JsonProperty("image_32")]
        public Uri Image32 { get; set; }

        [JsonProperty("image_48")]
        public Uri Image48 { get; set; }

        [JsonProperty("image_72")]
        public Uri Image72 { get; set; }

        [JsonProperty("image_192")]
        public Uri Image192 { get; set; }

        [JsonProperty("image_512")]
        public Uri Image512 { get; set; }

        [JsonProperty("status_text_canonical")]
        public string StatusTextCanonical { get; set; }

        [JsonProperty("team")]
        public string Team { get; set; }
    }
}