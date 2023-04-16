using QBForge.Extensions;
using QBForge.Extensions.Text;

namespace QBForge.Provider.Clauses
{
    public class OrderBySectionClause : BlockClause
	{
		public override string? Key => ClauseSections.OrderBy;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render
				.TryAppendCurrentIndent().Append("ORDER BY").TryAppendLineOrAppendSpace()
				.TryAppendCurrentIndent(1);

			var next = false;
			foreach (var clause in this)
			{
				if (next) render.Append(',').TryAppendSpace(); else next = true;

				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new OrderBySectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}