using QBForge.Extensions;
using QBForge.Extensions.Text;

namespace QBForge.Provider.Clauses
{
    public class WithCteSectionClause : BlockClause
	{
		public override string? Key => ClauseSections.WithCte;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			var next = false;
			foreach (var clause in this)
			{
				if (next)
				{
					render
						.Append(',').TryAppendLineOrAppendSpace()
						.TryAppendCurrentIndent();
				}
				else
				{
					next = true;

					render.TryAppendCurrentIndent().Append("WITH ");
				}

				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new WithCteSectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}