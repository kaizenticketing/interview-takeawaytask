#nullable enable

using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Ticketing.Services.Users.Models.Channels;

[MessagePackObject]
[DebuggerDisplay("{IsVisible}-{IsRequired}-{IsEditable}")]
[StructLayout(LayoutKind.Auto)]
public struct CustomerFieldFlags
{
	public CustomerFieldFlags(
		bool isVisible,
		bool isRequired,
		bool isEditable)
	{
		this.IsVisible = isVisible;
		this.IsRequired = isRequired;
		this.IsEditable = isEditable;

		if (this.IsRequired && !this.IsVisible) 
			throw new InvalidOperationException("Cant make something required if its not visible!");

		if (!this.IsVisible && this.IsEditable) 
			throw new InvalidOperationException("Cant make something editable if its not visible!");
	}

		[Key(0)]
	public bool IsVisible { get; set; }

		[Key(1)]
	public bool IsRequired { get; set; }

		[Key(2)]
	public bool IsEditable { get; set; }

	public readonly bool IsAnything()
	{
		return IsVisible || IsRequired || IsEditable;
	}
}
