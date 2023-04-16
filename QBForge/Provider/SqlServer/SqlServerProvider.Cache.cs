using System.Collections.Generic;

namespace QBForge.Provider
{
	partial class SqlServerProvider
	{
		/// <summary>
		/// Whether the specified value must be quoted as a keyword
		/// </summary>
		/// <param name="value">Lower case word</param>
		private static bool IsQuotedKeyword(string value)
		{
			return Cache.QuotedKeywords.Contains(value);
		}

		private static class Cache
		{
			static Cache() { }

			public static readonly HashSet<string> QuotedKeywords = new HashSet<string>()
			{
			};
		}
	}
}
