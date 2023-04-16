using System.Text;

namespace QBForge.Provider.Clauses
{
	public class OrAlsoClause : BinaryClause
	{
		public OrAlsoClause(Clause left, Clause right) : base(left, right) { }

		public override void Render(IBuildQueryContext context)
		{
			var parenthesize = Left is AndAlsoClause;

			if (parenthesize)
			{
				context.RenderContext.Append('(');
			}

			Left.Render(context);

			if (parenthesize)
			{
				context.RenderContext.Append(')');
			}

			context.RenderContext.Append(" OR ");

			Right.Render(context);
		}

		public override string ToString()
		{
			var parenthesize = Left is AndAlsoClause;
			var sb = new StringBuilder();

			if (parenthesize)
			{
				sb.Append('(');
			}

			sb.Append(Left.ToString());

			if (parenthesize)
			{
				sb.Append(')');
			}

			sb.Append(" OR ");

			sb.Append(Right.ToString());

			return sb.ToString();
		}

		public override Clause Clone()
		{
			var left = Left.Clone();
			var right = Right.Clone();

			if (ReferenceEquals(left, Left) && ReferenceEquals(right, Right))
			{
				return this;
			}
			else
			{
				return new OrAlsoClause(left, right);
			}
		}
	}
}