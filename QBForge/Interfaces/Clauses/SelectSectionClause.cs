using QBForge.Extensions.Text;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QBForge.Interfaces.Clauses
{
    public class SelectSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.Select;

		public override void Render(IBuildQueryContext context)
		{
			if (!context.RenderContext.Provider.Render(this, context))
			{
				Render(this, context);
			}
		}

		internal static void Render(Clause clause, IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render.Append("SELECT");

			foreach (var child in _selectQueryTemplate.Select(key => clause.Clauses.FirstOrDefault(x => x.Key == key)).Where(x => x != null))
			{
				render.TryAppendLineOrAppendSpace();

				child.Render(context);
			}
		}

		private static readonly IReadOnlyList<string> _selectQueryTemplate = new string[]
		{
			ClauseSections.WithCte,
			ClauseSections.Select,
			ClauseSections.Distinct,
			ClauseSections.WithTies,
			ClauseSections.Include,
			ClauseSections.From,
			ClauseSections.Join,
			ClauseSections.Where,
			ClauseSections.GroupBy,
			ClauseSections.Having,
			ClauseSections.OrderBy,
			ClauseSections.Skip,
			ClauseSections.Take,
			ClauseSections.Options,
			ClauseSections.Union
		};
	}
}
