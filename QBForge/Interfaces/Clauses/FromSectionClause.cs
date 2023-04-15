using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class FromSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.From;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render
				.TryAppendCurrentIndent().Append("FROM").TryAppendLineOrAppendSpace();

			var next = false;
			foreach (var clause in this)
			{
				if (next)
				{
					render
						.Append(',').TryAppendLineOrTryAppendSpace()
						.TryAppendCurrentIndent(1);
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
			var sectionClouse = new FromSectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}
