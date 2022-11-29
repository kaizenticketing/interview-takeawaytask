using System.ComponentModel;

#nullable enable

namespace Kaizen;

[MessagePackObject]
public struct Uuid : IEquatable<Uuid>, IComparable<Uuid>, IComparable, IEqualityComparer<Uuid>
{
	#region Statics

	public static Uuid? Create(string idClass, Guid? val)
	{
		if (!val.HasValue)
			return null;

		return new Uuid(idClass, val.Value);
	}

	public static Uuid Create(string idClass, Guid val)
	{
		return new Uuid(idClass, val);
	}

	public static Uuid Wildcard(string idClass)
	{
		// NOTE: bypass the constructor so we can set the wildcard
		var u = new Uuid
		{
			IdClass = idClass,
			InnerGuid = Guid.Empty
		};
		return u;
	}

	public static Uuid? Parse(string? s)
	{
		if (string.IsNullOrWhiteSpace(s))
			return null;

		int i = s.IndexOf("_", StringComparison.Ordinal);
		if (i < 0)
			throw new InvalidOperationException($"String to parse does not include a UUID prefix: {s}");

		string idClass = s[..i];
		string sGuid = s[(i + 1)..];

		// deal with wildcards
		if (sGuid == "*")
			return Wildcard(idClass);

		try
		{
			Guid guid = Guid.Parse(sGuid);
			return new Uuid(idClass, guid);
		}
		catch (FormatException ex)
		{
			throw new InvalidOperationException($"Invalid guid format: s = {sGuid}", ex);
		}
	}

	public static Uuid ParseExact(string s)
	{
		Uuid? uuid = Parse(s);

		if (uuid == null)
			throw new InvalidOperationException($"Could not parse null UUID: s = {s}");

		return uuid.Value;
	}

	public static Uuid Generate(string idClass)
	{
		return Create(idClass, Guid.NewGuid());
	}

	public static implicit operator Guid(Uuid id)
	{
		return id.InnerGuid;
	}

	public static implicit operator Guid?(Uuid? id)
	{
		return id?.InnerGuid;
	}

	#endregion

#nullable disable
	[SerializationConstructor]
	public Uuid()
	{
		IdClass = "";
		InnerGuid = Guid.Empty;
	}
#nullable enable

	public Uuid(string idClass, Guid value)
	{
		if (string.IsNullOrWhiteSpace(idClass))
			throw new ArgumentNullException(nameof(idClass));

		if (value == Guid.Empty)
			throw new ArgumentNullException(nameof(value), $"Value of null is not allowed for a UUID ({idClass})");

		IdClass = idClass;
		InnerGuid = value;
	}

	public Uuid(string s)
	{
		var parsed = ParseExact(s);
		this.IdClass = parsed.IdClass;
		this.InnerGuid = parsed.InnerGuid;
	}

	[Key(0)]
	public string IdClass { get; private set; }

	[Key(1)]
	public Guid InnerGuid { get; private set; }

	[IgnoreMember]
	public bool IsWildcard => InnerGuid == Guid.Empty;


	// TODO: move demo/fake stuff out

	public static Uuid Demo(string idClass, int ordinal)
	{
		var guid = GuidExtensions.CreateDemo(ordinal);
		return new Uuid(idClass, guid);
	}

	public static Uuid Fake(string idClass, int ordinal)
	{
		var guid = GuidExtensions.CreateFake(ordinal);
		return new Uuid(idClass, guid);
	}

	[IgnoreMember]
	public bool IsDemo => InnerGuid.IsDemo();

	[IgnoreMember]
	public bool IsFake => InnerGuid.IsFake();

	[System.Text.Json.Serialization.JsonIgnore]
	[IgnoreMember]
	public int? Ordinal => (IsDemo || IsFake) ? InnerGuid.GetOrdinal() : (int?)null;



	public override readonly string ToString()
	{
		if (string.IsNullOrWhiteSpace(IdClass))
			return "";
		//throw new ArgumentNullException("IdClass is null");

		// this guid is a wildcard
		if (InnerGuid == Guid.Empty)
			return string.Join("", IdClass, "_*");

		return string.Join("", IdClass, "_", InnerGuid.ToString()); // PERF: performance intensive - this is an optimisation
	}

	#region IEquatable & IComparable

	public override readonly int GetHashCode()
	{
		return HashCode.Combine(IdClass, InnerGuid);
	}

	public readonly int GetHashCode(Uuid obj)
	{
		return obj.GetHashCode();
	}

	public static bool operator ==(Uuid x, Uuid y)
	{
		return x.IdClass == y.IdClass && x.InnerGuid == y.InnerGuid;
	}

	public static bool operator !=(Uuid x, Uuid y)
	{
		return !(x == y);
	}

	public readonly bool Equals(Uuid other)
	{
		return other == this;
	}

	public readonly bool Equals(Uuid x, Uuid y)
	{
		return x == y;
	}

	public override readonly bool Equals(object? obj)
	{
		if (obj is not Uuid)
			return false;

		var ol = (Uuid)obj;
		return Equals(ol);
	}

	public readonly int CompareTo(Uuid? other)
	{
		return InnerGuid.CompareTo(other?.InnerGuid);
	}

	public readonly int CompareTo(Uuid other)
	{
		return InnerGuid.CompareTo(other.InnerGuid);
	}

	public readonly int CompareTo(object? obj)
	{
		var ol = (Uuid?)obj;
		return CompareTo(ol);
	}

	#endregion
}

public class UuidsByClass : Dictionary<string, ICollection<Uuid>>
{
	public UuidsByClass()
	{
	}

	public UuidsByClass(IDictionary<string, ICollection<Uuid>> dictionary) : base(dictionary)
	{
	}
}
