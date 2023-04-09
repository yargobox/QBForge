namespace QBForge.Interfaces.Clauses
{
	public class NegateClause : UnaryClause
	{
		public NegateClause(Clause left) : base(left) { }

		public override void Render(IBuildQueryContext context)
		{
			context.RenderContext.Append("NOT ");
			Left.Render(context);
		}
	}
}