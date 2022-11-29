using Microsoft.AspNetCore.Mvc;

#nullable enable

namespace Ticketing.Apps.Channels.Controllers;

public class HomeController : Controller
{
    [Route("/")]
    public ActionResult Index(int? message)
    {
        return Redirect("/account");
    }
}
