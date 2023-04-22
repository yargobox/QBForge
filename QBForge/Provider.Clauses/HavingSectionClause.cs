using QBForge.Extensions;

namespace QBForge.Provider.Clauses
{
	public class HavingSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.Having;

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext
				.TryAppendCurrentIndent().Append("HAVING").TryAppendLineOrAppendSpace()
				.TryAppendCurrentIndent(1);

			foreach (var clause in this)
			{
				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new HavingSectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}
