using QBForge.Provider;
using QBForge.Provider.Clauses;
using System;

namespace QBForge.Extensions
{
	public static class RenderContextExtensions
	{
		public static IRenderContext TryAppendCurrentIndent(this IRenderContext render, int indentCountAfterCurrent = 0)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks | ReadabilityLevels.Indentation))
			{
				if (render.TabSize > 0)
				{
					render.Append(' ', render.TabSize * (render.CurrentIndent + indentCountAfterCurrent));
				}
				else
				{
					render.Append('\t', render.CurrentIndent + indentCountAfterCurrent);
				}
			}

			return render;
		}
		public static IRenderContext AppendCurrentIndent(this IRenderContext render, int indentCountAfterCurrent = 0)
		{
			if (render.TabSize > 0)
			{
				render.Append(' ', render.TabSize * (render.CurrentIndent + indentCountAfterCurrent));
			}
			else
			{
				render.Append('\t', render.CurrentIndent + indentCountAfterCurrent);
			}

			return render;
		}

		public static IRenderContext TryAppendIndent(this IRenderContext render, int count = 1)
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

		public static IRenderContext AppendIndent(this IRenderContext render, int count = 1)
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

		public static IRenderContext TryAppendLineOrAppendSpace(this IRenderContext render)
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

		public static IRenderContext TryAppendLineOrTryAppendSpace(this IRenderContext render)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks))
			{
				render.AppendLine();
			}
			else if (!render.Readability.HasFlag(ReadabilityLevels.AvoidSpaces))
			{
				render.Append(' ');
			}

			return render;
		}

		public static IRenderContext TryAppendLine(this IRenderContext render)
		{
			if (render.Readability.HasFlag(ReadabilityLevels.LineBreaks))
			{
				render.AppendLine();
			}

			return render;
		}

		public static IRenderContext TryAppendSpace(this IRenderContext render)
		{
			if (!render.Readability.HasFlag(ReadabilityLevels.AvoidSpaces))
			{
				render.Append(' ');
			}

			return render;
		}

		public static IRenderContext AppendArgument(this IRenderContext render, Clause arg)
		{
			if (arg is DataEntryClause dataEntryClause)
			{
				render.Append(dataEntryClause.Value);
			}
			else if (arg is ParameterClause parameterClause)
			{
				render.Append(render.MakeParamPlaceholder(parameterClause.Value));
			}
			else if (arg is null)
			{
				throw new ArgumentNullException(nameof(arg));
			}
			else
			{
				arg.Render((IBuildQueryContext)render);
				//throw new ArgumentException($"{arg?.GetType().FullName} is not supported", nameof(arg));
			}

			return render;
		}
	}
}
