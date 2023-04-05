using QBForge.Interfaces;
using System;
using System.Globalization;
using System.Text;

namespace QBForge.Providers
{
	internal partial class ProviderBase
	{
		private sealed class BuildQueryContext : IBuildQueryContext, IRenderContext, ICondOperator, IAggrCall, IFuncCall, IOrderBy
		{
			public IQBProvider Provider { get; }
			public ReadabilityLevels Readability { get; }
			public int TabSize { get; }
			public int CurrentIndent { get; set; }

			public IRenderContext RenderContext => this;
			private readonly StringBuilder _output;
			
			public int ParameterCount { get; private set; }
			public object? Parameters { get; private set; }

			public BuildQueryContext(IQBProvider provider, ReadabilityLevels level, int tabSize)
			{
				if (level.HasFlag(ReadabilityLevels.Indentation) && !level.HasFlag(ReadabilityLevels.LineBreaks))
				{
					throw new ArgumentException($"Indentation cannot be used separately from line breaks.", nameof(level));
				}
				if (level.HasFlag(ReadabilityLevels.AvoidSpaces) && level.HasFlag(ReadabilityLevels.LineBreaks))
				{
					throw new ArgumentException($"Avoiding the use of white spaces means that line breaks cannot be used either.", nameof(level));
				}

				Provider = provider;
				Readability = level;
				TabSize = tabSize;
				_output = new StringBuilder();
			}

			public BuildQueryContext(IQBProvider provider, ReadabilityLevels level, int tabSize, StringBuilder output)
			{
				if (level.HasFlag(ReadabilityLevels.Indentation) && !level.HasFlag(ReadabilityLevels.LineBreaks))
				{
					throw new ArgumentException($"Indentation cannot be used separately from line breaks.", nameof(level));
				}
				if (level.HasFlag(ReadabilityLevels.AvoidSpaces) && level.HasFlag(ReadabilityLevels.LineBreaks))
				{
					throw new ArgumentException($"Avoiding the use of white spaces means that line breaks cannot be used either.", nameof(level));
				}

				Provider = provider;
				Readability = level;
				TabSize = tabSize;
				_output = output;
			}

			public string MakeParamPlaceholder()
			{
				ParameterCount++;

				return ((ProviderBase)Provider)._parameterPlaceholdersCache.TryGetValue(ParameterCount, out var placeholder)
					? placeholder
					: Provider.ParameterPlaceholder + ParameterCount.ToString(CultureInfo.InvariantCulture);
			}

			public override string ToString()
			{
				return _output.ToString();
			}

			IRenderContext IRenderContext.Append(string text)
			{
				_output.Append(text);
				return this;
			}

			IRenderContext IRenderContext.Append(char ch)
			{
				_output.Append(ch);
				return this;
			}

			IRenderContext IRenderContext.Append(char ch, int repeatCount)
			{
				_output.Append(ch, repeatCount);
				return this;
			}

			IRenderContext IRenderContext.AppendLine()
			{
				_output.AppendLine();
				return this;
			}

			IRenderContext IRenderContext.Append(DataEntry de)
			{
				if (!string.IsNullOrEmpty(de.Label))
				{
					Provider.AppendLabel(_output, de.Label!, Readability);
					_output.Append('.');
				}
				Provider.AppendIdentifier(_output, de.Name, Readability);
				return this;
			}

			IRenderContext IRenderContext.AppendObject(string objectName)
			{
				Provider.AppendIdentifier(_output, objectName, Readability);
				return this;
			}

			IRenderContext IRenderContext.AppendLabel(string label)
			{
				Provider.AppendLabel(_output, label, Readability);
				return this;
			}

			IRenderContext IRenderContext.AppendAsLabel(string label)
			{
				Provider.AppendAsLabel(_output, label, Readability);
				return this;
			}
		}
	}
}
