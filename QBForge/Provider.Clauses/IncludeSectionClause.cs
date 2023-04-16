using QBForge.Extensions;
using QBForge.Extensions.Text;
using System.Linq;
using System;
using QBForge.Provider.Configuration;

namespace QBForge.Provider.Clauses
{
    public class IncludeSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.Include;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			var next = false;
			foreach (var clause in this)
			{
				if (next)
				{
					render
						.Append(',').TryAppendLine()
						.TryAppendSpace().TryAppendCurrentIndent(1);
				}
				else
				{
					next = true;

					render
						.TryAppendCurrentIndent(1);
				}

				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new IncludeSectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}

		public static IncludeSectionClause CreateDefault(IQBProvider provider, Type documentType)
		{
			var includeSection = new IncludeSectionClause();
			var mappingInfo = provider.GetMappingInfo(documentType);

			foreach (var mi in mappingInfo.Values.Where(x => x.MapAs == MapMemberAs.Element))
			{
				var dataEntry = new DataEntry(null, mi.MappedName);

				Clause includeClause = new DataEntryClause(dataEntry);
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
