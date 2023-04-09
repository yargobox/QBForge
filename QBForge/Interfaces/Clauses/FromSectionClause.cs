using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class FromSectionClause : UnaryClause
	{
		public override string? Key => ClauseSections.From;

		public FromSectionClause(TableClouse tableClouse) : base(tableClouse) { }

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext
				.TryAppendCurrentIndent().Append("FROM").TryAppendLineOrAppendSpace()
				.TryAppendCurrentIndent(1);

			Left.Render(context);
		}

		public override string ToString()
		{
			return "FROM " + Left.ToString();
		}
	}
}
