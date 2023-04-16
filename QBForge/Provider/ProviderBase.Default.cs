using QBForge.Provider.Configuration;
using System.Text;

namespace QBForge.Provider
{
    partial class ProviderBase
	{
		public static IQBProvider Default => StaticDefaultProvider.Default;

		private static class StaticDefaultProvider
		{
			public static readonly IQBProvider Default = new DefaultProvider(new DocumentMapping());

			static StaticDefaultProvider() { }

			private sealed class DefaultProvider : ProviderBase
			{
				public DefaultProvider(DocumentMapping mapping) : base(mapping) { }

				public override string AppendObject(string objectName, ReadabilityLevels level = ReadabilityLevels.Default)
				{
					return objectName;
				}

				public override void AppendObject(StringBuilder sb, string objectName, ReadabilityLevels level = ReadabilityLevels.Default)
				{
					sb.Append(objectName);
				}

				public override string AppendLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default)
				{
					return label;
				}

				public override void AppendLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default)
				{
					sb.Append(label);
				}

				public override string AppendAsLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default)
				{
					return "AS " + label;
				}

				public override void AppendAsLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default)
				{
					sb.Append("AS ").Append(label);
				}
			}
		}
	}
}
