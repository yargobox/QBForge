using System.Text;

namespace QBForge.Provider.PostgreSql
{
	partial class PostgreSqlProvider
	{
		public override string AppendObject(string objectName, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			if (level == ReadabilityLevels.Default)
			{
				level = DefaultReadabilityLevel;
			}

			if (level.HasFlag(ReadabilityLevels.AvoidQuotedIdentifiers))
			{
				if (ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(objectName) && !IsQuotedKeyword(objectName))
				{
					return objectName;
				}
			}

			return string.Concat("\"", objectName.Replace("\"", "\"\""), "\"");
		}

		public override void AppendObject(StringBuilder sb, string objectName, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			if (level == ReadabilityLevels.Default)
			{
				level = DefaultReadabilityLevel;
			}

			if (level.HasFlag(ReadabilityLevels.AvoidQuotedIdentifiers))
			{
				if (ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(objectName) && !IsQuotedKeyword(objectName))
				{
					sb.Append(objectName);
					return;
				}
			}

			sb.Append('"').Append(objectName.Replace("\"", "\"\"")).Append('"');
		}

		public override string AppendLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			if (level == ReadabilityLevels.Default)
			{
				level = DefaultReadabilityLevel;
			}

			if (level.HasFlag(ReadabilityLevels.AvoidQuotedLabels))
			{
				if (ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(label) && !IsQuotedKeyword(label))
				{
					return label;
				}
			}

			return string.Concat("\"", label.Replace("\"", "\"\""), "\"");
		}

		public override void AppendLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			if (level == ReadabilityLevels.Default)
			{
				level = DefaultReadabilityLevel;
			}

			if (level.HasFlag(ReadabilityLevels.AvoidQuotedLabels))
			{
				if (ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(label) && !IsQuotedKeyword(label))
				{
					sb.Append(label);
					return;
				}
			}

			sb.Append('"').Append(label.Replace("\"", "\"\"")).Append('"');
		}

		public override string AppendAsLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			if (level == ReadabilityLevels.Default)
			{
				level = DefaultReadabilityLevel;
			}

			if (level.HasFlag(ReadabilityLevels.AvoidQuotedLabels))
			{
				if (ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(label) && !IsQuotedKeyword(label))
				{
					if (level.HasFlag(ReadabilityLevels.AvoidAsKeyword))
					{
						return label;
					}

					return "AS " + label;
				}
			}

			if (level.HasFlag(ReadabilityLevels.AvoidAsKeyword))
			{
				return string.Concat("\"", label.Replace("\"", "\"\""), "\"");
			}

			return string.Concat("AS \"", label.Replace("\"", "\"\""), "\"");
		}

		public override void AppendAsLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default)
		{
			if (level == ReadabilityLevels.Default)
			{
				level = DefaultReadabilityLevel;
			}

			if (!level.HasFlag(ReadabilityLevels.AvoidAsKeyword))
			{
				sb.Append("AS ");
			}

			if (level.HasFlag(ReadabilityLevels.AvoidQuotedLabels))
			{
				if (ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(label) && !IsQuotedKeyword(label))
				{
					sb.Append(label);
					return;
				}
			}

			sb.Append('"').Append(label.Replace("\"", "\"\"")).Append('"');
		}

		private static bool ContainsOnlyLowercaseLatinOrNumbersOrUnderscore(string value)
		{
			foreach (var ch in value)
			{
				if ((ch >= 'a' && ch <= 'z') || ch == '_' || (ch >= '0' && ch <= '9'))
				{
					continue;
				}

				return false;
			}

			return true;
		}
	}
}
