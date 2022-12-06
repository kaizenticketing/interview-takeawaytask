using System.Globalization;
using System.Text.Json.Serialization;

#nullable enable

namespace Ticketing.Services.Users.Models.Channels;

[MessagePackObject]
public class ChannelFlags
{
	[MessagePackObject]
	public class ChannelProtectionFlags
	{
		[Key(0)]
		public string? Username { get; set; }

		[Key(1)]
		public string? PasswordPlain { get; set; }

		[IgnoreMember]
		public bool IsProtected => !string.IsNullOrWhiteSpace(Username);
	}

	/// <summary>
	/// Does the channel need a password to be accessed (is it a private or demo channel). This does
	/// NOT mean that the channel is only accessible with a user/pass - that is what RequiresUser controls.
	/// </summary>
	[Key(0)]
	public ChannelProtectionFlags Protection { get; set; } = new();


	[Key(1)]
	public IList<CultureInfo> AvailableLanguages { get; set; } = new List<CultureInfo>();

	[JsonIgnore]
	[IgnoreMember]
	public CultureInfo DefaultLanguage =>
		AvailableLanguages.FirstOrDefault() ?? CultureInfo.InvariantCulture; // if no languages explicitly configured - use invariant

	// [Key(2)]
	// public IList<CurrencyCode> AvailableCurrencies { get; set; } = new List<CurrencyCode>();

	// [IgnoreMember]
	// public CurrencyCode? DefaultCurrency => AvailableCurrencies.Cast<CurrencyCode?>().FirstOrDefault();

	[Key(3)]
	// TODO: rename - remove the 'default'
	public Uuid? DefaultNotificationChannelId { get; set; }

	public enum LandingPageFlags
	{
		TicketHome = 0,
		ViewBasket = 1,
		Login = 2,
		Registration = 3,
		HospitalityPack = 4
	}

	[Key(4)]
	public LandingPageFlags LandingPage { get; set; }

	[Key(5)]
	public bool OpenHomeInNewTab { get; set; }


	[MessagePackObject]
	public class TicketHomeFlags
	{
		[MessagePackObject]
		public class QuickBuyFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(0)]
		public bool IsEnabled { get; set; }

		[Key(1)]
		public string? AlternativeUrl { get; set; }

		[Key(2)]
		public QuickBuyFlags QuickBuy { get; set; } = new();
	}

	[Key(6)]
	public TicketHomeFlags TicketHome { get; set; } = new();


	[MessagePackObject]
	public class LoginFlags
	{
		[Key(0)]
		public bool IsEnabled { get; set; }
	}

	[Key(7)]
	public LoginFlags Login { get; set; } = new();


	[MessagePackObject]
	public class CustomerLookupFlags
	{
	}

	[Key(8)]
	public CustomerLookupFlags CustomerLookup { get; set; } = new();


	[MessagePackObject]
	public class CompleteAccountFlags : BaseUpdateAccountFlags
	{
	}

	[Key(11)]
	public CompleteAccountFlags CompleteAccount { get; set; } = new();


	[MessagePackObject]
	public class MyAccountFlags
	{
		[MessagePackObject]
		public class SendEmailFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(0)]
		public SendEmailFlags SendEmail { get; set; } = new();

		//

		[MessagePackObject]
		public class ResendActivationFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(1)]
		public ResendActivationFlags ResendActivation { get; set; } = new();

		//

		[MessagePackObject]
		public class ResetPasswordFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }

			[Key(1)]
			public bool AllowSendForgottenPassword { get; set; }
		}


		[Key(2)]
		public ResetPasswordFlags ResetPassword { get; set; } = new();

		//

		[MessagePackObject]
		public class UpdateAccountFlags : BaseUpdateAccountFlags
		{
		}

		[Key(3)]
		public UpdateAccountFlags UpdateAccount { get; set; } = new();

		//

		[MessagePackObject]
		public class ReservationsFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(4)]
		public ReservationsFlags Reservations { get; set; } = new();

		//

		[MessagePackObject]
		public class OrderHistoryFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }

			[Key(1)]
			public bool ShowOrderStatus { get; set; }

			[Key(2)]
			public bool AllowConfirmation { get; set; }

			[Key(3)]
			public bool AllowAllocationConfirmation { get; set; }

			[Key(4)]
			public bool AllowUnissuing { get; set; }

			[Key(5)]
			public bool AllowPrinting { get; set; }

			[Key(6)]
			public bool AllowDispatching { get; set; }

			[Key(7)]
			public bool AllowTicketDownload { get; set; } // TicketDownloadModId needs to be set too

			[Key(8)]
			public bool AllowCancelling { get; set; }

			[Key(9)]
			public bool AllowMethodOfDeliveryChanges { get; set; }

			[Key(10)]
			public bool AllowMoving { get; set; }

			[Key(11)]
			public bool AllowReclass { get; set; }

			[Key(12)]
			public bool UseComplexFulfilmentFlags { get; set; } // TODO: rename - but need to rename data too

			[Key(13)]
			public bool ShowTransactions { get; set; }

			//

			[Key(14)]
			public Uuid? TicketDownloadModId { get; set; } = null;
		}

		[Key(5)]
		public OrderHistoryFlags OrderHistory { get; set; } = new();

		//

		// TODO: move these to the hospitality microsite?
		[MessagePackObject]
		public class AssignGuestsFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }

			[MessagePackObject]
			public class SendInvitationsFlags
			{
				[Key(0)]
				public bool IsEnabled { get; set; }

				[Key(1)]
				public Uuid? NotificationChannelId { get; set; }
			}

			[Key(2)]
			public SendInvitationsFlags SendInvitations { get; set; } = new();

			[MessagePackObject]
			public class SendTicketsFlags
			{
				[Key(0)]
				public bool IsEnabled { get; set; }
			}

			[Key(3)]
			public SendTicketsFlags SendTickets { get; set; } = new();
		}

		[Key(6)]
		public AssignGuestsFlags AssignGuests { get; set; } = new();

		//

		[MessagePackObject]
		public class DeleteAccountFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(7)]
		public DeleteAccountFlags DeleteAccount { get; set; } = new DeleteAccountFlags();

		//

		[MessagePackObject]
		public class WalletAccountFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(8)]
		public WalletAccountFlags Wallet { get; set; } = new WalletAccountFlags();

		//

		[MessagePackObject]
		public class TravelHistoryFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(9)]
		public TravelHistoryFlags TravelHistory { get; set; } = new TravelHistoryFlags();

		//

		// TODO: actually implement
		[MessagePackObject]
		public class ActivityLogFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(10)]
		public ActivityLogFlags ActivityLog { get; set; } = new();

        [MessagePackObject]
        public class FriendsAndFamilyFlags
        {
            [Key(0)]
            public bool IsEnabled { get; set; }
        }

		[Key(11)]
		public FriendsAndFamilyFlags FriendsAndFamily { get; set; } = new();
    }

	[Key(12)]
	public MyAccountFlags MyAccount { get; set; } = new();


	// TODO: create 'microsites' section
	[MessagePackObject]
	public class HospitalityMicrositeFlags
	{
		[Key(0)]
		public bool IsEnabled { get; set; }

		[MessagePackObject]
		public class UpdateAccountFlags : BaseUpdateAccountFlags
		{
		}

		[Key(1)]
		public UpdateAccountFlags UpdateAccount { get; set; } = new();
	}

	[Key(13)]
	public HospitalityMicrositeFlags HospitalityMicrosite { get; set; } = new();


	[MessagePackObject]
	public class ProductDetailsFlags
	{
		[Key(0)]
		public bool ShowPromotionalCodePrompt { get; set; }

		[Key(1)]
		public bool ShowHolds { get; set; }

		[Key(2)]
		public bool ShowAllowNonContiguousSeats { get; set; }

		/// <summary>
		/// What is the maximum number of items per product that can be added to a cart at one time?
		/// </summary>
		[Key(3)]
		public int MaximumQuantity { get; set; } = 12;

		[Key(4)]
		public int MinimumQuantity { get; set; } = 0;

		/// <summary>
		/// Are seat maps for events/packages enabled for the channel? They can still be turned off on an event-by-event basis.
		/// </summary>
		[Key(5)]
		public bool EnableMaps { get; set; } = false;

		/// <summary>
		/// Should we allow zooming in this channel at all times, never or on desktop only?
		/// </summary>
		[Key(9)]
		public MapZoomMode MapZoomMode { get; set; } = MapZoomMode.DesktopAndUserOnly;

		/// <summary>
		/// Should we allow panning in this channel at all times, never or on desktop only?
		/// </summary>
		[Key(10)]
		public MapPanMode MapPanMode { get; set; } = MapPanMode.DesktopOnly;

		[Key(6)]
		public bool AllowReserving { get; set; } = false;

		[Key(7)]
		public bool AllowAllocating { get; set; } = false;

		/// <summary>
		/// If enabled, will show actual counts instead of just broad indicators during inventory selection.
		/// </summary>
		[Key(8)]
		public bool ShowAggregateAvailability { get; set; } = false;
	}

	[Key(14)]
	public ProductDetailsFlags ProductDetails { get; set; } = new();


	[Key(20)]
	// TODO: right? cant share out to the manifests domain
	public IList<int> EnabledInventoryLabels { get; set; } = new List<int>();

	[MessagePackObject]
	public class CheckoutFlags
	{
		[Key(0)]
		public bool IsEnabled { get; set; }

		[MessagePackObject]
		public class CrossSellFlags
		{
			[Key(0)]
			public bool IsEnabled { get; set; }
		}

		[Key(1)]
		public CrossSellFlags CrossSells { get; set; } = new();
	}

	[Key(15)]
	public CheckoutFlags Checkout { get; set; } = new();


	[MessagePackObject]
	public class TestsFlags
	{
		[Key(0)]
		public bool IsEnabled { get; set; }
	}

	[Key(16)]
	public TestsFlags Tests { get; set; } = new();
}

public enum MapZoomMode
{
	// TODO: change to just DesktopOnly but only once we can patch the box office channel flags up in the db
	DesktopAndUserOnly = 0,
	Always = 1,
	Never = 2
}

public enum MapPanMode
{
	DesktopOnly = 0,
	Always = 1,
	Never = 2
}