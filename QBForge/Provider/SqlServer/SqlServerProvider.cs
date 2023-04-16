using QBForge.Provider.Clauses;
using QBForge.Provider.Configuration;

namespace QBForge.Provider
{
	internal partial class SqlServerProvider : ProviderBase
	{
		public override string Name => nameof(SqlServer);
		public override string ParameterPlaceholder => "@";

		public SqlServerProvider(DocumentMapping mapping) : base(mapping) { }

		public override ISelectQB<T> CreateSelectQB<T>()
		{
			return new SelectQB<T>(new QBContext(this, new /*SqlServer*/SelectSectionClause(typeof(T))));//!!!
		}
	}
}