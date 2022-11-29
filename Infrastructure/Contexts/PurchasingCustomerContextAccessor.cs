using Ticketing.Services;
using Ticketing.Services.Customers;
using Ticketing.Services.Customers.Models.Contexts;

#nullable enable

namespace Ticketing.Apps.Channels.Infrastructure.Accessors;

public class PurchasingCustomerContextAccessor : IPurchasingCustomerContextAccessor
{
    private readonly AccountService accountService;

    public PurchasingCustomerContextAccessor(
        AccountService accountService)
    {
        this.accountService = accountService;
    }

    public async ValueTask<ICustomerContext> GetPurchasingCustomerContextAsync()
    {
        // retrieve fake customer from service
        return await accountService.GetByIdAsync(Uuid.Demo(IdClasses.Account, 1));
    }

    public void ApplyContext(ICustomerContext? context)
    {
        throw new NotSupportedException();
    }
}