using QBForge.Provider.Clauses;

namespace QBForge.Provider
{
    partial class ProviderBase
	{
		public virtual IBuildQueryContext CreateBuildQueryContext(ReadabilityLevels level = ReadabilityLevels.Default, int tabSize = 0)
		{
			return new BuildQueryContext(this, level, 0);
		}

		public virtual IBuildQueryContext Build(IQueryBuilder queryBuilder, IBuildQueryContext? context = null)
		{
			context ??= CreateBuildQueryContext();

			queryBuilder.Context.Clause.Render(context);

			return context;
		}
	}
}
