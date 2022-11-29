#nullable enable

namespace Ticketing.Services.Contexts.Accessors;

public interface IChannelContextAccessor
{
	IChannelContext? ChannelContext { get; }

	void ApplyContext(IChannelContext? context);
}

