using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Text;

namespace QBForge.Extensions.Text
{
	internal static class TextExtensions
	{
		public static bool EndsWith(this StringBuilder sb, string value)
		{
			return EndsWith(sb, value, StringComparison.CurrentCulture);
		}

		public static bool EndsWith(this StringBuilder sb, string value, StringComparison comparisonType)
		{
			if (value is null) throw new ArgumentNullException(nameof(value));
			if (comparisonType != StringComparison.Ordinal) throw new ArgumentException("Only ordinal comparison is supported", nameof(comparisonType));

			if (value.Length == 0)
			{
				return true;
			}
			if (sb.Length < value.Length)
			{
				return false;
			}

			for (int i = sb.Length - value.Length, j = 0; j < value.Length; i++, j++)
			{
				if (sb[i] != sb[j])
				{
					return false;
				}
			}

			return true;
		}

		public static StringBuilder TrimEnd(this StringBuilder sb)
		{
			if (sb.Length > 0)
			{
				int end;
				for (end = sb.Length - 1; end >= 0; end--)
				{
					if (!Char.IsWhiteSpace(sb[end])) break;
				}

				if (end + 1 < sb.Length)
				{
					sb.Remove(end + 1, sb.Length - end - 1);
				}
			}
			return sb;
		}
	}
}
