namespace Ticketing.Apps.Channels.Areas.Account.Models.Home;

public class AccountActionModel
{
	public AccountActionModel(
		string icon,
		string nameKey,
		string descriptionKey,
		string linkTextKey,
		string linkUrl)
	{
		this.Icon = icon;
		this.NameKey = nameKey;
		this.DescriptionKey = descriptionKey;
		this.LinkTextKey = linkTextKey;
		this.LinkUrl = linkUrl;
	}

	public string NameKey { get; set; }
	public string DescriptionKey { get; set; }
	public string LinkTextKey { get; set; }
	public string LinkUrl { get; set; }
	public string Icon { get; set; }
}
