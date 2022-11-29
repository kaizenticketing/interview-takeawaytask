using System.ComponentModel;

namespace Ticketing.Services.Customers.Models;

public enum Gender
{
	[Description("M")]
	Male = 0,
	
	[Description("F")]
	Female = 1
}
