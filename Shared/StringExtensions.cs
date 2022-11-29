using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

#nullable enable

namespace Kaizen.Text;

public static class StringExtensions
{
	public static string TrimEnd(
		this string str,
		string sEndValue,
		StringComparison comparisonType = StringComparison.InvariantCulture)
	{
		if (str.EndsWith(sEndValue, comparisonType))
			str = str.Remove(str.Length - sEndValue.Length, sEndValue.Length);

		return str;
	}
}