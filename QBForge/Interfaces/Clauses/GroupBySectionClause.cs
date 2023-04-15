using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class GroupBySectionClause : BlockClause
	{
		public override string? Key => ClauseSections.GroupBy;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render
				.TryAppendCurrentIndent().Append("GROUP BY").TryAppendLineOrAppendSpace()
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
			var sectionClouse = new GroupBySectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}