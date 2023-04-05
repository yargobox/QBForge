using QBForge.Extensions.Text;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QBForge.Providers
{
    internal partial class ProviderBase : IQBProvider
	{
		public virtual IBuildQueryContext CreateBuildQueryContext(ReadabilityLevels level = ReadabilityLevels.Default, int tabSize = 0)
		{
			return new BuildQueryContext(this, level, 0);
		}

		public virtual IBuildQueryContext Build<T>(ISelectQB<T> queryBuilder, IBuildQueryContext? context = null)
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
