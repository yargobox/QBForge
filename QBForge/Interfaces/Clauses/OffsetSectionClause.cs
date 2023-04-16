using System.Globalization;

namespace QBForge.Interfaces.Clauses
{
	public class OffsetSectionClause : UnaryClause
	{
		public override string? Key => ClauseSections.Offset;

		public OffsetSectionClause(long offset) : this(new ConstClause<long>(offset)) { }
		protected OffsetSectionClause(Clause clause) : base(clause) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render.Append("OFFSET ");

			if (Left is ConstClause<long> longLeft)
			{
				render.Append(longLeft.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				Left.Render(context);
			}

			//render.Append(" ROWS");
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
				return new OffsetSectionClause(left);
			}
		}
	}
}
