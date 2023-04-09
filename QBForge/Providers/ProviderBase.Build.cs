using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;

namespace QBForge.Providers
{
	internal partial class ProviderBase : IQBProvider
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

		public virtual bool Render(Clause clause, IBuildQueryContext context)
		{
			//if (clause.Key == ClauseSections.Select)
			//{
			//	SelectSectionClause.Render(clause, context);
			//	return true;
			//}

			return false;
		}
	}
}
