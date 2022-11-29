#nullable enable

using Kaizen;

namespace Ticketing.Services.Users.Models;

[MessagePackObject]
public class TicketingUser
{
#nullable disable
	public TicketingUser()
	{
	}
#nullable enable

	[Key(0)]
	public Uuid Id { get; set; }

	[Key(1)]
	public Organisation Organisation { get; set; }

	[Key(2)]
	public bool IsChannel { get; set; }

	[Key(3)]
	public string Name { get; set; }

	[Key(4)]
	public string Email { get; set; }

	[Key(5)]
	public string? PasswordHash { get; set; }

	/// <summary>
	/// What organisations can this user access? Obviously their own - but will follow the 'child' graph links.
	/// </summary>
	/// <value></value>
	[Key(6)]
	public IReadOnlyCollection<Uuid> AccessibleOrganisationIds { get; set; } = new List<Uuid>();

	[Key(7)]
	public IReadOnlyCollection<Uuid> AccessibleChannelIds { get; set; } = new List<Uuid>();


	// TODO: oidc helpers - move out? or add as interface

	[IgnoreMember]
	public string SubjectId => Id.ToString();

	[IgnoreMember]
	public string Username => Email;


	#region IDemoable Implementation

	public static TicketingUser CreateDemo(int ordinal, Organisation org, bool isChannel)
	{
		return new TicketingUser
		{
			Id = Uuid.Demo(IdClasses.User, ordinal),
			Organisation = org,
			Name = $"User #{ordinal}",
			Email = $"test+{ordinal}@example.com",
			PasswordHash = "hash",
			IsChannel = isChannel
		};
	}

	#endregion
}

[MessagePackObject]
public class UserIdsByOrganisation
{
	[Key(0)]
	public Uuid OrganisationId { get; set; }

	[Key(1)]
	public IReadOnlyCollection<Uuid> UserIds { get; set; } = new List<Uuid>();
}