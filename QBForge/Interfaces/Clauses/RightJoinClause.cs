using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class RightJoinClause : BinaryClause
	{
		public override string? Key => LabelAs;
		public string LabelAs { get; }

		public RightJoinClause(TableClause tableClause, string labelAs) : this((Clause)tableClause, new OnClause(), labelAs) { }
		public RightJoinClause(SubQueryClause subQueryClause, string labelAs) : this((Clause)subQueryClause, new OnClause(), labelAs) { }
		protected RightJoinClause(Clause clause, Clause onClause, string labelAs) : base(clause, onClause) => LabelAs = labelAs;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (Left is TableClause)
			{
				render
					.TryAppendCurrentIndent(1).Append("RIGHT JOIN "); Left.Render(context); render.Append(' ').AppendAsLabel(LabelAs);
			}
			else
			{
				render
					.TryAppendCurrentIndent(1).Append("RIGHT JOIN").TryAppendLineOrAppendSpace()
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

			render
				.TryAppendLineOrAppendSpace()
				.TryAppendCurrentIndent(2);

			render.CurrentIndent += 1;
			try
			{
				Right.Render(context);
			}
			finally
			{
				render.CurrentIndent -= 1;
			}
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
				return new RightJoinClause(left, right, LabelAs);
			}
		}
	}
}
