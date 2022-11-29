using Ticketing.Services.Customers.Models;

namespace Ticketing.Services.Customers;

// NOTE: this is all fake

public class AccountService
{
    public async ValueTask<Customer> GetByIdAsync(Uuid uuid)
    {
        await ValueTask.CompletedTask;

        // faking
		return new Customer()
		{
			AccountId = Uuid.Demo(IdClasses.Account, 1),
			OrganisationId = Uuid.Demo(IdClasses.Organisation, 1),
			CustomerReference = "FAKECUSTOMER1",
			Forename = "Joe",
			Surname = "Example",
			EMail = "joe@example.com",
			PhoneNumbers = new PhoneNumbers()
		};
    }
}
