using Ticketing.Services.Contexts.Accessors;
using Ticketing.Services.Customers.Models;

#nullable enable

namespace Ticketing.Apps.Channels.Areas.Account.Models.Home.Mappers;

public class AccountMapper // M -> VM
{
	private readonly IChannelContextAccessor channelContextAccessor;

	public AccountMapper(
		IChannelContextAccessor channelContextAccessor)
	{
		this.channelContextAccessor = channelContextAccessor;
	}

	public async ValueTask<List<AccountModel>> MapListAsync(List<Customer> customers)
	{
		var result = new List<AccountModel>();

		foreach (var customer in customers)
		{
			result.Add(await MapAsync(customer));
		}

		return result;
	}

	public async ValueTask<AccountModel> MapAsync(Customer s)
	{
		await ValueTask.CompletedTask;

		if (channelContextAccessor.ChannelContext == null)
			throw new InvalidOperationException("Channel context is required");

		// build out the billing address, making sure there's always at least one line
		var addressLines = s.DefaultBillingAddress?.ToLines() ?? Array.Empty<string>();
		if (!addressLines.Any())
			addressLines = new[] { "-none-" };

		var d = new AccountModel
		{
			CustomerReference = s.CustomerReference,
			IsForeignOrganisation = s.ForeignOrganisationId != null,
			Name = s.FullName,
			EMail = s.EMail ?? "-none-",
			DateOfBirth = s.DateOfBirth == null
				? "-none-"
				: s.FormatDateOfBirth(channelContextAccessor.ChannelContext.Language),
			Gender = s.Gender?.ToString() ?? "-none-",

			DefaultBillingAddress = addressLines.Aggregate((cur, x) => cur + "<br/>" + x),

			HomeTelephone = string.IsNullOrWhiteSpace(s.PhoneNumbers.Home) ? "-none-" : s.PhoneNumbers.Home,
			MobileTelephone = string.IsNullOrWhiteSpace(s.PhoneNumbers.Mobile) ? "-none-" : s.PhoneNumbers.Mobile
		};

		return d;
	}
}