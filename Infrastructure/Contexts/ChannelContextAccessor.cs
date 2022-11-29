using System.Globalization;
using Ticketing.Services;
using Ticketing.Services.Contexts;
using Ticketing.Services.Contexts.Accessors;
using Ticketing.Services.Users.Models.Channels;

#nullable enable

namespace Ticketing.Apps.Channels.Infrastructure.Accessors;

public class ChannelContextAccessor : IChannelContextAccessor
{
    public IChannelContext? ChannelContext
    {
        get
        {
            var fakeChannel = new Channel()
            {
                Id = Uuid.Demo(IdClasses.Channel, 1),
                IsEnabled = true,
                ReportingDescription = "Fake Channel",
                Organisation = new Services.Users.Models.Organisation()
                {
                    Id = Uuid.Demo(IdClasses.Organisation, 1),
                    Name = "Fake Organisation"
                },
                UserId = Uuid.Demo(IdClasses.User, 1),
                BaseUris = new[] { "http://localhost:5000" },
                SkinName = "fakeskin",
                Flags = new ChannelFlags()
                {
                    MyAccount = new ChannelFlags.MyAccountFlags()
                    {
                        ResetPassword = new ChannelFlags.MyAccountFlags.ResetPasswordFlags()
                        {
                            IsEnabled = true
                        },
                        Reservations = new ChannelFlags.MyAccountFlags.ReservationsFlags()
                        {
                            IsEnabled = true
                        },
                        OrderHistory = new ChannelFlags.MyAccountFlags.OrderHistoryFlags()
                        {
                            IsEnabled = true
                        }
                    }
                }
            };

            // fake out which channel we are running as
            return new ChannelContext()
            {
                Language = new CultureInfo("en-GB"),
                ChannelId = Uuid.Demo(IdClasses.Channel, 1),
                TimezoneId = "Europe/London",
                NotificationMode = NotificationMode.Immediate,
                Channel = fakeChannel,
                EligibleOrganisationIds = new[] { fakeChannel.Organisation.Id }
            };
        }
    }

    public void ApplyContext(IChannelContext? context)
    {
        throw new NotSupportedException();
    }
}