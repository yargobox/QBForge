using QBForge.Extensions.Text;
using QBForge.Providers.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QBForge.Interfaces.Clauses
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
			if (!context.RenderContext.Provider.Render(this, context))
			{
				Render(this, context);
			}
		}

		private static void Render(SelectSectionClause clause, IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render.TryAppendCurrentIndent().Append("SELECT");

			foreach (var sectionKey in _selectQueryTemplate)
			{
				var section = clause.Clauses.FirstOrDefault(x => x.Key == sectionKey);

				if (section == null)
				{
					if (sectionKey == ClauseSections.Include)
					{
						section = MakeDefaultSectionIncludeClause(render.Provider, clause.DocumentType);
					}
					else
					{
						continue;
					}
				}

				render.TryAppendLineOrAppendSpace();

				section.Render(context);
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

		private static Clause MakeDefaultSectionIncludeClause(IQBProvider provider, Type documentType)
		{
			var includeSection = new IncludeSectionClause();
			var mappingInfo = provider.GetMappingInfo(documentType);

			foreach (var mi in mappingInfo.Values.Where(x => x.MapAs == MapMemberAs.Element))
			{
				var dataEntry = new DataEntry(null, mi.MappedName);

				Clause includeClause = new DataEntryClouse(dataEntry);
				if (dataEntry.Name != mi.Name)
				{
					includeClause = new IncludeClause(mi.Name, includeClause, mi.Name);
				}

				includeSection.Add(includeClause);
			}

			return includeSection;
		}
	}
}
