
#nullable enable

namespace Ticketing.Services.Customers.Models;

[MessagePackObject]
public class PhoneNumbers
{
	// TODO: turn into a list of IPhoneNumber { type, name, number } and dates etc. Allow contact preferences to be tied to one of these?

	[Key(0)]
	// [PersonalInformation]
	public string? Mobile { get; set; }

	[Key(1)]
	// [PersonalInformation]
	public string? Home { get; set; }

	[Key(2)]
	// [PersonalInformation]
	public string? Work { get; set; }
}
