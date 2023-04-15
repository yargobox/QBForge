namespace QBForge.Interfaces.Clauses
{
	public class AndAlsoClause : BinaryClause
	{
		public AndAlsoClause(Clause left, Clause right) : base(left, right) { }

		public override void Render(IBuildQueryContext context)
		{
			Left.Render(context);
			context.RenderContext.Append(" AND ");
			Right.Render(context);
		}

		public override string ToString()
		{
			return string.Concat(Left.ToString(), " AND ", Right.ToString());
		}

		public override Clause Clone()
		{
			var left = Left.Clone();
			var right = Right.Clone();

			if (object.ReferenceEquals(left, Left) && object.ReferenceEquals(right, Right))
			{
				return this;
			}
			else
			{
				return new AndAlsoClause(left, right);
			}
		}
	}
}