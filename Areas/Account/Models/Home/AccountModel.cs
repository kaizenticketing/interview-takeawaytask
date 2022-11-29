using System.Collections.Generic;

#nullable enable

namespace Ticketing.Apps.Channels.Areas.Account.Models.Home
{
	public class AccountModel
	{
#nullable disable
		public AccountModel()
		{
		}
#nullable enable

		public AccountMessage? Message { get; set; }

		public string CustomerReference { get; set; }
		public bool IsForeignOrganisation { get; set; }
		public string Name { get; set; }
		public string EMail { get; set; }
		public string DateOfBirth { get; set; }
		public string Gender { get; set; }

		public string DefaultBillingAddress { get; set; }

		public string HomeTelephone { get; set; }
		public string MobileTelephone { get; set; }

		public IReadOnlyCollection<AccountActionModel> AvailableActions { get; set; }
	}
}