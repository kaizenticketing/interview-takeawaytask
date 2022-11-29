namespace Ticketing.Services.Users.Models.Channels;

public interface IAccountValidationFlags
{
	bool IsEnabled { get; set; }

	CustomerFieldFlags CustomerReference { get; }

	CustomerFieldFlags Email { get; }
	CustomerFieldFlags ConfirmEmail { get; }
	CustomerFieldFlags Firstname { get; }
	CustomerFieldFlags Surname { get; }
	CustomerFieldFlags Initials { get; }
	CustomerFieldFlags Password { get; }

	CustomerFieldFlags Company { get; }
	CustomerFieldFlags CompanyPosition { get; }

	CustomerFieldFlags DateOfBirth { get; }
	CustomerFieldFlags Gender { get; }

	CustomerFieldFlags AddressLine1 { get; }
	CustomerFieldFlags AddressLine2 { get; }
	CustomerFieldFlags AddressTown { get; }
	CustomerFieldFlags AddressCounty { get; }
	CustomerFieldFlags AddressPostcode { get; }
	CustomerFieldFlags AddressCountry { get; }

	CustomerFieldFlags PhoneMobile { get; }
	CustomerFieldFlags PhoneHome { get; }
	CustomerFieldFlags PhoneWork { get; }

	CustomerFieldFlags PreferredLanguage { get; }
	CustomerFieldFlags PreferredTimezone { get; }

	CustomerFieldFlags ProductSpecific1 { get; }
	CustomerFieldFlags ProductSpecific2 { get; }

	bool ConsentVisible { get; }
}
