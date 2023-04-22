using QBForge.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QBForge.Provider.Clauses
{
	public class SelectSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.Select;
		public Type DocumentType { get; }

		public SelectSectionClause(Type documentType)
		{
			DocumentType = documentType;
		}

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

				if (next)
				{
					if (sectionKey == ClauseSections.Distinct || sectionKey == ClauseSections.WithTies)
					{
						render.Append(' ');
					}
					else
					{
						render.TryAppendLineOrAppendSpace();
					}
				}
				else
				{
					next = true;
				}

				section.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new SelectSectionClause(DocumentType);
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
			ClauseSections.WithTies,
			ClauseSections.Include,
			ClauseSections.From,
			ClauseSections.Join,
			ClauseSections.Where,
			ClauseSections.GroupBy,
			ClauseSections.Having,
			ClauseSections.OrderBy,
			ClauseSections.Fetch,
			ClauseSections.Offset,
			ClauseSections.Options,
			ClauseSections.Union
		};
	}
}