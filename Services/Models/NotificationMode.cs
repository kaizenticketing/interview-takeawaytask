#nullable enable

namespace Kaizen;

public enum NotificationMode
{
	/// <summary>
	/// Send immediately. Default.
	/// </summary>
	Immediate = 0,

	/// <summary>
	/// Suppress entirely.
	/// </summary>
	Suppress = 1,

	/// <summary>
	/// Applicable only for batches - will send rendered notifications to a batch ready to send seperately.
	/// </summary>
	SendToBatch = 2
}
