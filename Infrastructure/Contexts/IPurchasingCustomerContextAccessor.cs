using Ticketing.Services.Customers.Models.Contexts;

#nullable enable

namespace Ticketing.Apps.Channels.Infrastructure.Accessors;

public interface IPurchasingCustomerContextAccessor
{
	ValueTask<ICustomerContext> GetPurchasingCustomerContextAsync();

	void ApplyContext(ICustomerContext? context);
}
