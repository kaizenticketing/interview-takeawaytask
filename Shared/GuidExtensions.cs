using System.Security.Cryptography;
using System.Text;

#nullable enable

namespace Kaizen;

public static class GuidExtensions
{
	/// <summary>
	/// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
	/// </summary>
	/// <param name="namespaceId">The ID of the namespace.</param>
	/// <param name="name">The name (within that namespace).</param>
	/// <returns>A UUID derived from the namespace and name.</returns>
	/// <remarks>See <a href="http://code.logos.com/blog/2011/04/generating_a_deterministic_guid.html">Generating a deterministic GUID</a>.</remarks>
	public static Guid Create(Guid namespaceId, string name)
	{
		return Create(namespaceId, name, 5);
	}

	/// <summary>
	/// Creates a name-based UUID using the algorithm from RFC 4122 §4.3.
	/// </summary>
	/// <param name="namespaceId">The ID of the namespace.</param>
	/// <param name="name">The name (within that namespace).</param>
	/// <param name="version">The version number of the UUID to create; this value must be either
	/// 3 (for MD5 hashing) or 5 (for SHA-1 hashing).</param>
	/// <returns>A UUID derived from the namespace and name.</returns>
	/// <remarks>See <a href="http://code.logos.com/blog/2011/04/generating_a_deterministic_guid.html">Generating a deterministic GUID</a>.</remarks>
	public static Guid Create(
		Guid namespaceId,
		string name,
		int version)
	{
		if (name == null)
			throw new ArgumentNullException(nameof(name));

		if (version != 3 && version != 5)
			throw new ArgumentOutOfRangeException(nameof(version), "version must be either 3 or 5.");

		// convert the name to a sequence of octets (as defined by the standard or conventions of its namespace) (step 3)
		// ASSUME: UTF-8 encoding is always appropriate
		byte[] nameBytes = Encoding.UTF8.GetBytes(name);

		// convert the namespace UUID to network order (step 3)
		byte[] namespaceBytes = namespaceId.ToByteArray();
		SwapByteOrder(namespaceBytes);

		// compute the hash of the name space ID concatenated with the name (step 4)
#pragma warning disable CA5350, CA5351 // part of the Guid specifications!
		byte[] hash;
		using (HashAlgorithm algorithm = version == 3 ? MD5.Create() : SHA1.Create())
		{
			algorithm.TransformBlock(namespaceBytes, 0, namespaceBytes.Length, outputBuffer: null, 0);
			algorithm.TransformFinalBlock(nameBytes, 0, nameBytes.Length);
			hash = algorithm.Hash ?? throw new InvalidOperationException();
		}
#pragma warning restore CA5350, CA5351

		// most bytes from the hash are copied straight to the bytes of the new GUID (steps 5-7, 9, 11-12)
		byte[] newGuid = new byte[16];
		Array.Copy(hash, 0, newGuid, 0, 16);

		// set the four most significant bits (bits 12 through 15) of the time_hi_and_version field to the appropriate 4-bit version number from Section 4.1.3 (step 8)
		newGuid[6] = (byte)((newGuid[6] & 0x0F) | (version << 4));

		// set the two most significant bits (bits 6 and 7) of the clock_seq_hi_and_reserved to zero and one, respectively (step 10)
		newGuid[8] = (byte)((newGuid[8] & 0x3F) | 0x80);

		// convert the resulting UUID to local byte order (step 13)
		SwapByteOrder(newGuid);
		return new Guid(newGuid);
	}

	#region Fake IDs

	public const string FakePrefix = "00000000-0000-0000-0000-";

	public static Guid CreateFake(int ordinal)
	{
		if (ordinal < 1)
			throw new InvalidOperationException("Ordinal must be 1 or greater");

		return Guid.Parse(FakePrefix + string.Format("{0:000000000000}", ordinal));
	}

	public static bool IsFake(this Guid g)
	{
		return g.ToString().StartsWith(FakePrefix, StringComparison.Ordinal);
	}

	#endregion

	#region Demo IDs

	public const string DemoPrefix = "dddddddd-dddd-dddd-dddd-";

	public static Guid CreateDemo(int ordinal)
	{
		if (ordinal < 1)
			throw new InvalidOperationException("Ordinal must be 1 or greater");

		return Guid.Parse(DemoPrefix + string.Format("{0:000000000000}", ordinal));
	}

	public static bool IsDemo(this Guid g)
	{
		return g.ToString().StartsWith(DemoPrefix, StringComparison.Ordinal);
	}

	#endregion

	public static int GetOrdinal(this Guid g)
	{
		if (!g.IsDemo() && !g.IsFake())
			throw new InvalidOperationException("Not a demo or fake uuid");

		return int.Parse(g.ToString()
			.Replace(FakePrefix, "", StringComparison.Ordinal)
			.Replace(DemoPrefix, "", StringComparison.Ordinal)
		);
	}

	public static Uuid ToUuid(this Guid g, string idClass)
	{
		return Uuid.Create(idClass, g);
	}

	public static Uuid? ToNullableUuid(this Guid? g, string idClass)
	{
		return Uuid.Create(idClass, g);
	}

	#region Internal Implementation

	/// <summary>
	/// Converts a GUID (expressed as a byte array) to/from network order (MSB-first).
	/// </summary>
	/// <param name="guid"></param>
	internal static void SwapByteOrder(byte[] guid)
	{
		SwapBytes(guid, 0, 3);
		SwapBytes(guid, 1, 2);
		SwapBytes(guid, 4, 5);
		SwapBytes(guid, 6, 7);
	}

	private static void SwapBytes(byte[] guid, int left, int right)
	{
		(guid[right], guid[left]) = (guid[left], guid[right]);
	}

	#endregion
}
