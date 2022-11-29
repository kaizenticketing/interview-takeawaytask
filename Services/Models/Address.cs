#nullable enable

using System.Text;

namespace Ticketing.Services.Customers.Models;

[MessagePackObject]
public class Address 
{
#nullable disable
	public Address()
	{
	}
#nullable enable

	[Key(0)]
	public Uuid? Id { get; set; }

	[Key(1)]
	public Uuid? ProfileId { get; set; }

	[Key(2)]
	public bool IsDefaultBilling { get; set; }

	[Key(3)]
	public bool IsDefaultDelivery { get; set; }

	[Key(4)]
	// [PersonalInformation]
	public string? Line1 { get; set; }

	[Key(5)]
	// [PersonalInformation]
	public string? Line2 { get; set; }

	[Key(6)]
	// [PersonalInformation]
	public string? Town { get; set; }

	[Key(7)]
	// [PersonalInformation]
	public string? County { get; set; }

	[Key(8)]
	// [PersonalInformation]
	public string? Postcode { get; set; }

	// [Key(9)]
	// [PersonalInformation]
	// public Country? Country { get; set; }

	[Key(10)]
	public AddressType Type { get; set; }
}

public static class AddressExtensions
{
	public static ICollection<string> ToLines(this Address a)
	{
		var lines = new List<string>();

		if (!string.IsNullOrWhiteSpace(a.Line1)) lines.Add(a.Line1);
		if (!string.IsNullOrWhiteSpace(a.Line2)) lines.Add(a.Line2);
		if (!string.IsNullOrWhiteSpace(a.Town)) lines.Add(a.Town);
		if (!string.IsNullOrWhiteSpace(a.County)) lines.Add(a.County);
		if (!string.IsNullOrWhiteSpace(a.Postcode)) lines.Add(a.Postcode);
		// if (a.Country != null) lines.Add(a.Country.Name);

		return lines;
	}

	public static string ToSingleLineString(this Address a)
	{
		var sb = new StringBuilder();
		if (!string.IsNullOrWhiteSpace(a.Line1)) sb.AppendFormat("{0}, ", a.Line1);
		if (!string.IsNullOrWhiteSpace(a.Line2)) sb.AppendFormat("{0}, ", a.Line2);
		if (!string.IsNullOrWhiteSpace(a.Town)) sb.AppendFormat("{0}, ", a.Town);
		if (!string.IsNullOrWhiteSpace(a.County)) sb.AppendFormat("{0}, ", a.County);
		if (!string.IsNullOrWhiteSpace(a.Postcode)) sb.AppendFormat("{0}, ", a.Postcode);
		// if (a.Country != null) sb.AppendFormat("{0}, ", a.Country.Name);

		// strip the last ', '
		return sb.ToString().TrimEnd(", ");
	}
}