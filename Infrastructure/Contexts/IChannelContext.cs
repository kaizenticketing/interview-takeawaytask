using System.Globalization;
using NodaTime;
using Ticketing.Services.Users.Models;
using Ticketing.Services.Users.Models.Channels;

#nullable enable

namespace Ticketing.Services.Contexts;

public interface IChannelContext //:  IContentContext // NOTE: a channel context is also a content context - will match 1:1 unless overridden
{
    CultureInfo Language { get; }

    Channel Channel { get; }

    DateTimeZone Timezone { get; }

    TicketingUser? User { get; }

    NotificationMode NotificationMode { get; }

    ICollection<Uuid> EligibleOrganisationIds { get; }

    string CacheFingerprint { get; }
}
