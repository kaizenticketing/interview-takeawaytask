#nullable enable

namespace Ticketing.Services.Users.Models.Channels;

[MessagePackObject]
[Union(1, typeof(BaseUpdateAccountFlags))]
public abstract class BaseAccountValidationFlags : IAccountValidationFlags
{
	[Key(0)]
	public bool IsEnabled { get; set; } = false;

	[Key(1)]
	public CustomerFieldFlags Email { get; set; } = new CustomerFieldFlags();

	[Key(2)]
	public CustomerFieldFlags ConfirmEmail { get; set; } = new CustomerFieldFlags();

	[Key(3)]
	public CustomerFieldFlags CustomerReference { get; set; } = new CustomerFieldFlags();

	[Key(4)]
	public CustomerFieldFlags Firstname { get; set; } = new CustomerFieldFlags();

	[Key(5)]
	public CustomerFieldFlags Surname { get; set; } = new CustomerFieldFlags();

	[Key(6)]
	public CustomerFieldFlags Initials { get; set; } = new CustomerFieldFlags();

	[Key(7)]
	public CustomerFieldFlags Password { get; set; } = new CustomerFieldFlags();

	[Key(8)]
	public CustomerFieldFlags Gender { get; set; } = new CustomerFieldFlags();

	[Key(9)]
	public CustomerFieldFlags DateOfBirth { get; set; } = new CustomerFieldFlags();

	[Key(10)]
	public CustomerFieldFlags Company { get; set; } = new CustomerFieldFlags();

	[Key(11)]
	public CustomerFieldFlags CompanyPosition { get; set; } = new CustomerFieldFlags();


	[Key(12)]
	public CustomerFieldFlags AddressLine1 { get; set; } = new CustomerFieldFlags();

	[Key(13)]
	public CustomerFieldFlags AddressLine2 { get; set; } = new CustomerFieldFlags();

	[Key(14)]
	public CustomerFieldFlags AddressTown { get; set; } = new CustomerFieldFlags();

	[Key(15)]
	public CustomerFieldFlags AddressCounty { get; set; } = new CustomerFieldFlags();

	[Key(16)]
	public CustomerFieldFlags AddressPostcode { get; set; } = new CustomerFieldFlags();

	[Key(17)]
	public CustomerFieldFlags AddressCountry { get; set; } = new CustomerFieldFlags();


	[Key(18)]
	public CustomerFieldFlags PhoneMobile { get; set; } = new CustomerFieldFlags();

	[Key(19)]
	public CustomerFieldFlags PhoneHome { get; set; } = new CustomerFieldFlags();

	[Key(20)]
	public CustomerFieldFlags PhoneWork { get; set; } = new CustomerFieldFlags();


	[Key(21)]
	public CustomerFieldFlags PreferredLanguage { get; set; } = new CustomerFieldFlags();

	[Key(22)]
	public CustomerFieldFlags PreferredTimezone { get; set; } = new CustomerFieldFlags();


	[Key(23)]
	public CustomerFieldFlags ProductSpecific1 { get; set; } = new CustomerFieldFlags();

	[Key(24)]
	public CustomerFieldFlags ProductSpecific2 { get; set; } = new CustomerFieldFlags();

	[IgnoreMember]
	public abstract bool ConsentVisible { get; }


	[IgnoreMember]
	[System.Text.Json.Serialization.JsonIgnore]
	internal virtual IEnumerable<CustomerFieldFlags> Fields
	{
		get
		{
			yield return Firstname;
			yield return Surname;
			yield return Email;
			yield return ConfirmEmail;
			yield return Password;
			yield return Gender;
			yield return DateOfBirth;
			yield return Company;
			yield return CompanyPosition;

			yield return AddressLine1;
			yield return AddressLine2;
			yield return AddressTown;
			yield return AddressCounty;
			yield return AddressPostcode;
			yield return AddressCountry;

			yield return PhoneMobile;
			yield return PhoneHome;
			yield return PhoneWork;

			yield return PreferredLanguage;
			yield return PreferredTimezone;

			yield return ProductSpecific1;
			yield return ProductSpecific2;
		}
	}
}
