using System.Diagnostics;
using System.Globalization;
using MoreLinq;
using Ticketing.Services.Customers.Models.Contexts;

#nullable enable

namespace Ticketing.Services.Customers.Models;

/// <summary>
/// The profile of a customer visible from the aspect of a particular organisation.
/// </summary>
[MessagePackObject]
[DebuggerDisplay("Customer: AccountId = {" + nameof(AccountId) + "}, Name = {" + nameof(FullName) + "}, ForeignOrgId = {" + nameof(ForeignOrganisationId) + "}")]
public class Customer :
	IEquatable<Customer>, IComparable<Customer>, IComparable,
	ICustomerContext
{
	#region Static Factories

	public static Customer BlankFor(Customer customer)
	{
		return new Customer
		{
			// references - ok
			ProfileId = customer.ProfileId,
			OrganisationId = customer.OrganisationId,
			AccountId = customer.AccountId,
			CustomerReference = customer.CustomerReference,

			// BLANK REST OF RECORD
		};
	}

	#endregion

	[Key(0)]
	public Uuid? AccountId { get; set; }

	[Key(1)]
	public Uuid? ProfileId { get; set; }

	[Key(2)]
	public Uuid OrganisationId { get; set; }

	/// <summary>
	/// Was this profile created by a foreign organisation as part of a remote sale?
	/// </summary>
	[Key(3)]
	public Uuid? ForeignOrganisationId { get; set; }

	//

	[Key(4)]
	public string CustomerReference { get; set; } = default!;

	[Key(5)]
	public string? CustomerImportedReference { get; set; } = default!;

	[Key(6)]
	// [PersonalInformation]
	public string? EMail { get; set; } = default;

	[Key(7)]
	// [PersonalInformation]
	public string? Forename { get; set; } = default!;

	[Key(8)]
	// [PersonalInformation]
	public string? Surname { get; set; } = default!;

	//

	// [PersonalInformation]
	[IgnoreMember]
	// TODO: not culturally correct
	public string FullName => $"{Forename} {Surname}";

	//


	/// <summary>
	/// NOTE: NOT 'middle initial'. This is for circumstances when a club wants to capture just initials for something - eg. a shirt.
	/// </summary>
	/// <value></value>
	[Key(9)]
	// [PersonalInformation]
	public string? Initials { get; set; } = default!;

	[Key(10)]
	// [PersonalInformation]
	public Gender? Gender { get; set; }

	[Key(11)]
	// [PersonalInformation]
	public LocalDate? DateOfBirth { get; set; }

	[Key(12)]
	// [PersonalInformation]
	public string? Company { get; set; } = default!;

	[Key(13)]
	// [PersonalInformation]
	public string? CompanyPosition { get; set; } = default!;

	[Key(14)]
	public IReadOnlyCollection<Address> Addresses { get; set; } = new List<Address>();

	[IgnoreMember]
	[System.Text.Json.Serialization.JsonIgnore] // for the purposes of diagnostics
	public Address? DefaultDeliveryAddress => Addresses.SingleOrDefault(x => x.IsDefaultDelivery);

	[IgnoreMember]
	[System.Text.Json.Serialization.JsonIgnore] // for the purposes of diagnostics
	public Address? DefaultBillingAddress => Addresses.SingleOrDefault(x => x.IsDefaultBilling);

	[Key(15)]
	[System.Text.Json.Serialization.JsonIgnore] // for the purposes of diagnostics
	public IReadOnlyCollection<Login> Logins { get; set; } = new List<Login>();

	[IgnoreMember]
	[System.Text.Json.Serialization.JsonIgnore] // for the purposes of diagnostics
	public Login? LocalLogin => Logins.SingleOrDefault(x => x.Type == LoginType.Local);

	[Key(16)]
	public PhoneNumbers PhoneNumbers { get; set; } = new PhoneNumbers();

	[Key(17)]
	public CultureInfo? PreferredLanguage { get; set; } = default;

	[Key(18)]
	public DateTimeZone? PreferredTimezone { get; set; } = default;

	/// <summary>
	/// Ip address that the account was registered from. Can be null - for instance if a profile was imported!
	/// </summary>
	[Key(19)]
	[System.Text.Json.Serialization.JsonIgnore] // hide from diagnostics
	// [PersonalInformation]
	public string? IpAddress { get; set; } = default;

	[Key(20)]
	public bool OptOutOfAutomatedProcessing { get; set; }

	/// <summary>
	/// Did another customer create this customer account on their behalf?
	/// </summary>
	[Key(22)]
	public Uuid? CreatedByAccountId { get; set; }

	[Key(23)]
	public string? ProductSpecific1 { get; set; }

	[Key(24)]
	public string? ProductSpecific2 { get; set; }

	[Key(25)]
	public Instant Created { get; set; }

	[Key(26)]
	public Instant Updated { get; set; }

	//

	// [Key(29)]
	// public IReadOnlyCollection<StoredInstrument> StoredInstruments { get; set; } = new List<StoredInstrument>();


	/// <summary>
	/// Format the customer for display as the purchaser of an order.
	/// </summary>
	/// <returns></returns>
	public (string name, string description) GetPurchaserName()
	{
		return ($"{FullName}", $"{FullName} | {CustomerReference}");
	}

	/// <summary>
	/// Format the customer for display as the attendee of an order line when the purchasing customer is as provided.
	/// </summary>
	/// <param name="purchasingCustomer"></param>
	/// <returns></returns>
	public (string name, string description) GetAttendeeName(
		Customer purchasingCustomer)
	{
		// TODO: in the future replace with heuristic based on my network relationship

		if (this == purchasingCustomer || CreatedByAccountId == purchasingCustomer.AccountId)
		{
			// if we (purchasing customer) created the attending customer... then show all their details
			return ($"{FullName}", $"{FullName} | {CustomerReference}");
		}
		else
		{
			// otherwise we're unrelated to the attending customer
			return string.IsNullOrEmpty(EMail) ?
				($"{FullName}", $"{FullName} | {CustomerReference}") :
				($"{EMail}", $"{FullName} | {CustomerReference}");
		}
	}

	#region ICustomerContext Implementation

	[IgnoreMember]
	Uuid? ICustomerContext.AccountId => this.AccountId;

	[IgnoreMember]
	bool ICustomerContext.IsEverythingOpen => false;

	#endregion

	#region IEquatable & IComparable

	public override int GetHashCode()
	{
		return HashCode.Combine(AccountId);
	}

	public static bool operator ==(Customer? x, Customer? y)
	{
		if (x is null && y is null) return true;
		if (x is null || y is null) return false;
		if (x.AccountId == null || y.AccountId == null) return false;

		return x.AccountId == y.AccountId;
	}

	public static bool operator !=(Customer? x, Customer? y)
	{
		return !(x == y);
	}

	public bool Equals(Customer? other)
	{
		if (other == null) return false;
		return other == this;
	}

	public override bool Equals(object? obj)
	{
		var ol = obj as Customer;
		if (ol == null) return false;
		return Equals(ol);
	}

	public int CompareTo(Customer? other)
	{
		if (other == null || other.AccountId == null) return 1; // nulls first
		if (AccountId == null) return -1;
		return AccountId.Value.CompareTo(other.AccountId);
	}

	public int CompareTo(object? obj)
	{
		var ol = obj as Customer;
		if (ol == null) return 1;   // nulls first
		return CompareTo(ol);
	}

	#endregion
}

public static class CustomerExtensions
{
	public static string FormatDateOfBirth(this Customer c, CultureInfo language)
	{
		return c.DateOfBirth?.ToString("d", language) ?? "";
	}
}
