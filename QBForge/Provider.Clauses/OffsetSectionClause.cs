using System.Globalization;

namespace QBForge.Provider.Clauses
{
	public class OffsetSectionClause : UnaryClause
	{
		public override string? Key => ClauseSections.Offset;

		public OffsetSectionClause(long offset, bool parametrized) : this(parametrized ? new ParameterClause(offset) : new ConstClause<long>(offset)) { }
		protected OffsetSectionClause(Clause clause) : base(clause) { }

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render.Append("OFFSET ");

			if (Left is ConstClause<long> longLeft)
			{
				render.Append(longLeft.Value.ToString(CultureInfo.InvariantCulture));
			}
			else if (Left is ParameterClause paramLeft)
			{
				render.Append(render.MakeParamPlaceholder(paramLeft.Value));
			}
			else
			{
				Left.Render(context);
			}

			render.Append(" ROWS");
		}

		public override string ToString()
		{
			if (Left is ConstClause<long> longLeft)
			{
				return $"OFFSET {longLeft.Value} ROWS";
			}
			else if (Left is ParameterClause paramLeft)
			{
				return $"OFFSET $?(={paramLeft.Value}) ROWS";
			}
			else
			{
				return string.Concat("OFFSET ", Left.ToString(), " ROWS");
			}
		}

		public override Clause Clone()
		{
			var left = Left.Clone();

			if (ReferenceEquals(left, Left))
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
