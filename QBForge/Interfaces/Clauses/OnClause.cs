using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class OnClause : BlockClause
	{
		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext
				.Append("ON ");

			foreach (var clause in this)
			{
				clause.Render(context);
			}
		}
	}
}