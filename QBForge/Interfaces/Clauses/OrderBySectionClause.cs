using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
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
	}
}