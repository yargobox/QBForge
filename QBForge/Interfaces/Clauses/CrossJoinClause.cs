using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class CrossJoinClause : UnaryClause
	{
		public override string? Key => LabelAs;
		public string LabelAs { get; }

		public CrossJoinClause(TableClause tableClause, string labelAs) : this((Clause)tableClause, labelAs) { }
		public CrossJoinClause(SubQueryClause subQueryClause, string labelAs) : this((Clause)subQueryClause, labelAs) { }
		protected CrossJoinClause(Clause clause, string labelAs) : base(clause) => LabelAs = labelAs;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (Left is TableClause)
			{
				render
					.TryAppendCurrentIndent(1).Append("CROSS JOIN "); Left.Render(context); render.Append(' ').AppendAsLabel(LabelAs);
			}
			else
			{
				render
					.TryAppendCurrentIndent(1).Append("CROSS JOIN").TryAppendLineOrAppendSpace()
					.TryAppendCurrentIndent(1).Append('(').TryAppendLine();

				render.CurrentIndent += 2;
				try
				{
					Left.Render(context);
				}
				finally
				{
					render.CurrentIndent -= 2;
				}

				render
					.TryAppendLine()
					.TryAppendCurrentIndent(1).Append(") ").AppendAsLabel(LabelAs);
			}
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
				return new CrossJoinClause(left, LabelAs);
			}
		}
	}
}
