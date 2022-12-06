#nullable enable

namespace Ticketing.Services.Customers.Models.Contexts;

public interface ICustomerContext
{
	Uuid? AccountId { get; }

	Uuid? ForeignOrganisationId { get; }

	bool IsEverythingOpen { get; }
}