#nullable enable

namespace Ticketing.Services.Users.Models.Channels;

[MessagePackObject]
[Union(0, typeof(ChannelFlags.CompleteAccountFlags))]
[Union(1, typeof(ChannelFlags.MyAccountFlags.UpdateAccountFlags))]
public class BaseUpdateAccountFlags : BaseAccountValidationFlags, IUpdateValidationFlags
{
	[IgnoreMember]
	public override bool ConsentVisible => true;
}
