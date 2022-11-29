using System.Globalization;
using Ticketing.Services.Users.Models;
using Ticketing.Services.Users.Models.Channels;

#nullable enable

namespace Ticketing.Services.Contexts;

[MessagePackObject]
public class ChannelContext : IChannelContext
{
    [Key(0)]
    public required CultureInfo Language { get; set; }

    [Key(1)]
    public required Uuid ChannelId { get; set; }

    [Key(2)]
    [System.Text.Json.Serialization.JsonIgnore] // exclude in diagnostics AND in jobs
    public required Channel Channel { get; set; }

    [Key(3)]
    public Uuid? UserId { get; internal set; }

    [Key(4)]
    [System.Text.Json.Serialization.JsonIgnore] // exclude in diagnostics AND in jobs
    public TicketingUser? User { get; set; }

    //

    [Key(5)]
    public required string TimezoneId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    [IgnoreMember]
    public DateTimeZone Timezone => DateTimeZoneProviders.Tzdb.GetZoneOrNull(TimezoneId)!;

    [Key(6)]
    public required NotificationMode NotificationMode { get; set; }

    //

    /// <summary>
    /// Under the current context, which organisation's entities can I see.
    /// </summary>
    [Key(7)]
    public required ICollection<Uuid> EligibleOrganisationIds { get; set; }

    /// <summary>
    /// Provide a fingerprint for cache - if this is equal then the same objects/output can be shared between contexts.
    /// NOTE: make sure GetRelevantContextsAsync is kept in sync with this
    /// 
    /// NOTE: timezone is not included in this BECAUSE IT MAKES THE COMBINATIONS too numerous to invalidate on objects
    /// that are scoped to a channel context
    /// </summary>
    [IgnoreMember]
    public string CacheFingerprint => $"{Language},{ChannelId},{UserId},{NotificationMode}";
}
