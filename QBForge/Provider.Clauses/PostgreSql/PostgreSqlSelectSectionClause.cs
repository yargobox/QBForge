using QBForge.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QBForge.Provider.Clauses.PostgreSql
{
	public class PostgreSqlSelectSectionClause : SelectSectionClause
	{
		public PostgreSqlSelectSectionClause(Type documentType) : base(documentType) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			var next = false;
			foreach (var sectionKey in _selectQueryTemplate)
			{
				var section = Clauses!.FirstOrDefault(x => x.Key == sectionKey);

				if (section == null)
				{
					if (sectionKey == ClauseSections.Include)
					{
						section = IncludeSectionClause.CreateDefault(render.Provider, DocumentType);
					}
					else
					{
						if (sectionKey == ClauseSections.Select)
						{
							if (next) render.TryAppendLineOrAppendSpace(); else next = true;

							render.TryAppendCurrentIndent().Append("SELECT");
						}

						continue;
					}
				}

				if (next) render.TryAppendLineOrAppendSpace(); else next = true;

				section.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new PostgreSqlSelectSectionClause(DocumentType);
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}

		private static readonly IReadOnlyList<string> _selectQueryTemplate = new string[]
		{
			ClauseSections.WithCte,
			ClauseSections.Select,
			ClauseSections.Distinct,
			ClauseSections.Include,
			ClauseSections.From,
			ClauseSections.Join,
			ClauseSections.Where,
			ClauseSections.GroupBy,
			ClauseSections.Having,
			ClauseSections.OrderBy,
			ClauseSections.Limit,
			ClauseSections.Offset,
			ClauseSections.WithTies,
			ClauseSections.Options,
			ClauseSections.Union
		};
	}
}
