using System.Diagnostics;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

#nullable enable

namespace Ticketing.Services.Users.Models.Channels;

[DebuggerDisplay("{" + nameof(ReportingDescription) + "}")]
[MessagePackObject]
public class Channel : System.ComponentModel.DataAnnotations.IValidatableObject
{
	[Key(0)]
	public Uuid Id { get; set; }

	[Key(1)]
	[System.Text.Json.Serialization.JsonIgnore] // hide parents in diagnostics
	public Channel? ParentChannel { get; set; }

	[Key(2)]
	public required bool IsEnabled { get; set; }

	[Key(3)]
	public required string ReportingDescription { get; set; }

	[Key(10)]
	public string SiteDescription => ReportingDescription;

	[Key(4)]
	public required Organisation Organisation { get; set; }

	[Key(5)]
	public required Uuid UserId { get; set; }

	/// <summary>
	/// These are *ordered* so that they can be x-referenced between channels (ie. when going from a main
	/// channel to a notification channel).
	/// </summary>
	[Key(6)]
	public required ICollection<string> BaseUris { get; set; } = new List<string>();

	[Key(7)]
	public bool RequiresUser { get; set; }

	[Key(8)]
	public required string SkinName { get; set; }


	[Key(9)]
	public required ChannelFlags Flags { get; set; } = new ChannelFlags();


	#region Validation

	public IEnumerable<ValidationResult> Validate(
		System.ComponentModel.DataAnnotations.ValidationContext validationContext)
	{
		var f = Flags;
		var ca = f.CompleteAccount;
		var ua = f.MyAccount.UpdateAccount;

		if (ca.IsEnabled)
		{
			if (ca.CustomerReference.IsEditable || ca.CustomerReference.IsRequired)
				yield return new ValidationResult("Customer reference can be made visible only");

			if (ca.Fields.Any(x => x.IsRequired && !x.IsEditable))
				yield return new ValidationResult("At any stage, any field that is required at that stage must be editable");
		}

		if (ua.IsEnabled)
		{
			if (ua.CustomerReference.IsEditable || ua.CustomerReference.IsRequired)
				yield return new ValidationResult("Customer reference can be made visible only");

			if (ua.Fields.Any(x => x.IsRequired && !x.IsEditable))
				yield return new ValidationResult("At any stage, any field that is required at that stage must be editable");

			if (ua.Password.IsAnything())
				yield return new ValidationResult("Password is not 'editable' etc in my account. Controlled by separate reset password flag");
		}

		if (f.MyAccount.OrderHistory.AllowTicketDownload)
		{
			if (f.MyAccount.OrderHistory.TicketDownloadModId == null)
				yield return new ValidationResult("No ticket download MOD specified");
		}
	}

	#endregion
}
