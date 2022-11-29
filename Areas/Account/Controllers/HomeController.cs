using Microsoft.AspNetCore.Mvc;
using Ticketing.Apps.Channels.Areas.Account.Models;
using Ticketing.Apps.Channels.Areas.Account.Models.Home;
using Ticketing.Apps.Channels.Areas.Account.Models.Home.Mappers;
using Ticketing.Apps.Channels.Infrastructure.Accessors;
using Ticketing.Services.Contexts.Accessors;
using Ticketing.Services.Customers.Models;

#nullable enable

namespace Ticketing.Apps.Channels.Areas.Account.Controllers;

[Area("Account")]
public class HomeController : Controller
{
    private readonly IChannelContextAccessor channelContextAccessor;
    private readonly IPurchasingCustomerContextAccessor purchasingCustomerContextAccessor;
	private readonly AccountMapper accountMapper;

	public HomeController(
		ILogger<HomeController> log,
		IChannelContextAccessor channelContextAccessor,
		IPurchasingCustomerContextAccessor purchasingCustomerContextAccessor,
		AccountMapper accountMapper)
	{
        this.channelContextAccessor = channelContextAccessor;
        this.purchasingCustomerContextAccessor = purchasingCustomerContextAccessor;
		this.accountMapper = accountMapper;
	}

	[Route("account")]
	// [AuthorizePurchasingCustomer] // NOTE: requires an active customer (with no holds) - if not redirects to login
	public async Task<ActionResult> Index(int? message)
	{
		// if we're in a channel that is 'user' based - show the selected customer view instead
		if (channelContextAccessor.ChannelContext == null || channelContextAccessor.ChannelContext.Channel.RequiresUser)
			return RedirectToAction("SelectedCustomer", new { message });

		var customer = (Customer)(await purchasingCustomerContextAccessor.GetPurchasingCustomerContextAsync());

		// map the purchasing customer
		var model = await accountMapper.MapAsync(customer);
		model.Message = (AccountMessage?)message;
		model.AvailableActions = GetAvailableActions(customer); // decide which actions are applicable - generates urls

		return View(model);
	}

	#region Internal Implementation

	private IReadOnlyCollection<AccountActionModel> GetAvailableActions(Customer customer)
	{
		if (channelContextAccessor.ChannelContext == null)
			throw new InvalidOperationException("Channel context must be present");

		var channel = channelContextAccessor.ChannelContext.Channel;
		var flags = channel.Flags.MyAccount;

		var actions = new List<AccountActionModel>();

		if (flags.SendEmail.IsEnabled && customer.EMail != null)
			actions.Add(new AccountActionModel("envelope", "sendemail", "", "sendemail", "mailto:" + customer.EMail));

		bool hasLocalLogin = customer.LocalLogin != null;
		if (flags.ResetPassword.IsEnabled && hasLocalLogin)
		{
			if (flags.ResetPassword.AllowSendForgottenPassword)
				actions.Add(new AccountActionModel("unlock", "forgottenpassword", "sendforgottenpw", "sendforgottenpassword", Url.Action("Send", "ForgottenPassword", new { area = "Account" })));
			else
				actions.Add(new AccountActionModel("unlock", "password", "changepw", "password", Url.Action("Index", "ResetPassword", new { area = "Account" })));
		}

		if (flags.UpdateAccount.IsEnabled)
			actions.Add(new AccountActionModel("user-edit", "details", "updatecontact", "details", Url.Action("Index", "Update", new { area = "Account" })));

		if (flags.Reservations.IsEnabled)
			actions.Add(new AccountActionModel("check-circle", "reservations", "reservations", "reservations", Url.Action("Index", "Reservations", new { area = "Account" })));

		if (flags.OrderHistory.IsEnabled)
			actions.Add(new AccountActionModel("list-ul", "history", "viewdetails", "history", Url.Action("Index", "Orders", new { area = "Account" })));

		if (flags.AssignGuests.IsEnabled)
			actions.Add(new AccountActionModel("pencil-alt", "assign", "hospguests", "assign", Url.Action("Index", "Assign", new { area = "Hospitality" })));

		if (flags.DeleteAccount.IsEnabled)
			actions.Add(new AccountActionModel("minus-circle", "delete", "deleteaccount", "delete", Url.Action("Index", "Delete", new { area = "Account" })));

		if (flags.Wallet.IsEnabled)
			actions.Add(new AccountActionModel("wallet", "wallet", "wallet", "wallet", Url.Action("Index", "Wallet", new { area = "Account" })));

		if (flags.TravelHistory.IsEnabled)
			actions.Add(new AccountActionModel("map-signs", "travelhistory", "travelhistory", "travelhistory", Url.Action("Index", "TravelHistory", new { area = "Account" })));

		return actions;
	}

	#endregion
}
