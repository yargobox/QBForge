using QBForge.Provider.Clauses.PostgreSql;
using QBForge.Provider.Configuration;

namespace QBForge.Provider.PostgreSql
{
	internal partial class PostgreSqlProvider : ProviderBase
	{
		public override string Name => nameof(PostgreSql);
		public override string ParameterPlaceholder => "$";

		public PostgreSqlProvider(DocumentMapping mapping) : base(mapping) { }

		public override ISelectQB<T> CreateSelectQB<T>()
		{
			return new SelectQB<T>(new QBContext(this, new PostgreSqlSelectSectionClause(typeof(T))));
		}
	}
}