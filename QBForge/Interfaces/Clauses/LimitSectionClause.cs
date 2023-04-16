using System.Globalization;

namespace QBForge.Interfaces.Clauses
{
	public class LimitSectionClause : UnaryClause
	{
		public override string? Key => ClauseSections.Limit;

		public LimitSectionClause(long limit) : this(new ConstClause<long>(limit)) { }
		protected LimitSectionClause(Clause clause) : base(clause) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			//render.Append("FETCH NEXT ");
			render.Append("LIMIT ");

			if (Left is ConstClause<long> longLeft)
			{
				render.Append(longLeft.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				Left.Render(context);
			}

			//render.Append(" ROWS ONLY");
		}

		public override Clause Clone()
		{
			var left = Left.Clone();

			if (object.ReferenceEquals(left, Left))
			{
				return this;
			}
			else
			{
				return new LimitSectionClause(left);
			}
		}
	}
}
