using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class WhereSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.Where;

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext
				.TryAppendCurrentIndent().Append("WHERE").TryAppendLineOrAppendSpace()
				.TryAppendCurrentIndent(1);

			foreach (var clause in this)
			{
				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new WhereSectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}
