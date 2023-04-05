using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class JoinSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.Join;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			var next = false;
			foreach (var clause in this)
			{
				if (next)
				{
					render
						.TryAppendLineOrAppendSpace()
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
	}
}
