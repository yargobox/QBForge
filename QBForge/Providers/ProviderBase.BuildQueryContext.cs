using QBForge.Interfaces;
using System.Globalization;
using System.Text;

namespace QBForge.Providers
{
	internal partial class ProviderBase
	{
		private sealed class BuildQueryContext : IBuildQueryContext, IRenderContext, ICondOperator, IAggrCall, IFuncCall, IOrderBy
		{
			public IQBProvider Provider { get; }
			public ReadabilityLevels ReadabilityLevel { get; }

			public IRenderContext RenderContext => this;
			public StringBuilder Output { get; }
			
			public int ParameterCount { get; private set; }
			public object? Parameters { get; private set; }

			public BuildQueryContext(IQBProvider provider, ReadabilityLevels level)
			{
				Provider = provider;
				ReadabilityLevel = level;
				Output = new StringBuilder();
			}

			public BuildQueryContext(IQBProvider provider, ReadabilityLevels level, StringBuilder output)
			{
				Provider = provider;
				ReadabilityLevel = level;
				Output = output;
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
				return Output.ToString();
			}

			IRenderContext IRenderContext.Append(string text)
			{
				Output.Append(text);
				return this;
			}

			IRenderContext IRenderContext.Append(char ch)
			{
				Output.Append(ch);
				return this;
			}

			IRenderContext IRenderContext.AppendLine()
			{
				Output.AppendLine();
				return this;
			}

			IRenderContext IRenderContext.Append(DataEntry de)
			{
				if (!string.IsNullOrEmpty(de.Label))
				{
					Provider.AppendLabel(Output, de.Label!, ReadabilityLevel);
					Output.Append('.');
				}
				Provider.AppendIdentifier(Output, de.Name, ReadabilityLevel);
				return this;
			}

			IRenderContext IRenderContext.AppendIdentifier(string identifier)
			{
				Provider.AppendIdentifier(Output, identifier, ReadabilityLevel);
				return this;
			}

			IRenderContext IRenderContext.AppendLabel(string label)
			{
				Provider.AppendLabel(Output, label, ReadabilityLevel);
				return this;
			}

			IRenderContext IRenderContext.AppendAsLabel(string label)
			{
				Provider.AppendAsLabel(Output, label, ReadabilityLevel);
				return this;
			}
		}
	}
}
