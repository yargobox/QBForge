using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
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

		internal static IRenderContext TryAppendCurrentIndent(this IRenderContext render, int indentCountAfterCurrent = 0)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks | ReadabilityLevels.Indentation))
			{
				if (render.TabSize > 0)
				{
					render.Append(' ', render.TabSize * (render.CurrentIndent + indentCountAfterCurrent));
				}
				else
				{
					render.Append('\t', (render.CurrentIndent + indentCountAfterCurrent));
				}
			}

			return render;
		}
		internal static IRenderContext AppendCurrentIndent(this IRenderContext render, int indentCountAfterCurrent = 0)
		{
			if (render.TabSize > 0)
			{
				render.Append(' ', render.TabSize * (render.CurrentIndent + indentCountAfterCurrent));
			}
			else
			{
				render.Append('\t', (render.CurrentIndent + indentCountAfterCurrent));
			}

			return render;
		}

		internal static IRenderContext TryAppendIndent(this IRenderContext render, int count = 1)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks | ReadabilityLevels.Indentation))
			{
				if (render.TabSize > 0)
				{
					render.Append(' ', render.TabSize * count);
				}
				else
				{
					render.Append('\t', count);
				}
			}

			return render;
		}

		internal static IRenderContext AppendIndent(this IRenderContext render, int count = 1)
		{
			if (render.TabSize > 0)
			{
				render.Append(' ', render.TabSize * count);
			}
			else
			{
				render.Append('\t', count);
			}

			return render;
		}

		internal static IRenderContext TryAppendLineOrAppendSpace(this IRenderContext render)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks))
			{
				render.AppendLine();
			}
			else
			{
				render.Append(' ');
			}

			return render;
		}

		internal static IRenderContext TryAppendLine(this IRenderContext render)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks))
			{
				render.AppendLine();
			}

			return render;
		}

		internal static IRenderContext TryAppendSpace(this IRenderContext render)
		{
			if (!render.Readability.HasFlag(ReadabilityLevels.AvoidSpaces))
			{
				render.Append(' ');
			}

			return render;
		}

		internal static IRenderContext AppendArgument(this IRenderContext query, Clause arg)
		{
			if (arg is DataEntryClouse dataEntryClause)
			{
				query.Append(dataEntryClause.Value);
			}
			else if (arg is ParameterClouse parameterClause)
			{
				query.Append(query.MakeParamPlaceholder(parameterClause.Value));
			}
			else if (arg is null)
			{
				throw new ArgumentNullException(nameof(arg));
			}
			else
			{
				throw new ArgumentException($"{arg?.GetType().FullName} is not supported", nameof(arg));
			}

			return query;
		}
	}
}
