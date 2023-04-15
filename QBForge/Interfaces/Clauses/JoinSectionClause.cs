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
					render.TryAppendLineOrAppendSpace();
				}
				else
				{
					next = true;
				}

				clause.Render(context);
			}
		}

		public override Clause Clone()
		{
			var sectionClouse = new JoinSectionClause();
			foreach (var child in this)
			{
				sectionClouse.Add(child.Clone());
			}
			return sectionClouse;
		}
	}
}
