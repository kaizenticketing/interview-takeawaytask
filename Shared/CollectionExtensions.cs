using System.Diagnostics;

#nullable enable

namespace Kaizen.Collections.Generic;

public static class CollectionExtensions
{
	[DebuggerStepThrough]
	public static IReadOnlyList<T> AsList<T>(
		this T o)
	{
		return new List<T> { o };
	}
}
