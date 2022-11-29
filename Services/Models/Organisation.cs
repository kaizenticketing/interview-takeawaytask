#nullable enable

namespace Ticketing.Services.Users.Models;

[MessagePackObject]
public class Organisation
{
#nullable disable
	public Organisation()
	{
	}
#nullable enable

	[Key(0)]
	public Uuid Id { get; set; }

	[Key(10)]
	public string Code { get; set; }

	[Key(3)]
	public string Name { get; set; }

	[Key(4)]
	public Uuid DefaultSalesChannelId { get; set; }

	[Key(13)]
	public Uuid DefaultBoxOfficeChannelId { get; set; }

	[Key(5)]
	public Uuid? DefaultVenueId { get; set; }

	[Key(14)]
	public Uuid? DefaultAttractionId { get; set; }

	[Key(7)]
	public string TelephoneBoxOffice { get; set; }

	[Key(8)]
	public string[] BoxOfficeEmails { get; set; }

	[Key(9)]
	public string[] OperationsEmails { get; set; }

	[Key(11)]
	public DateTimeZone PreferredTimezone { get; set; }

	[Key(12)]
	public string? CorporateUrl { get; set; }

	[Key(15)]
	public string? VatNumber { get; set; }
}
