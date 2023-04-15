using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
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
	}
}
