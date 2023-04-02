using QBForge.Providers.Configuration;

namespace QBForge.Providers
{
	internal partial class PostgreSqlProvider : ProviderBase
	{
		public override string Name => nameof(PostgreSql);
		public override string ParameterPlaceholder => "$";

		public PostgreSqlProvider(DocumentMapping mapping) : base(mapping) { }
	}
}
