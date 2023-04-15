using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class FullJoinClause : BinaryClause
	{
		public override string? Key => LabelAs;
		public string LabelAs { get; }

		public FullJoinClause(TableClause tableClause, string labelAs) : this((Clause)tableClause, new OnClause(), labelAs) { }
		public FullJoinClause(SubQueryClause subQueryClause, string labelAs) : this((Clause)subQueryClause, new OnClause(), labelAs) { }
		protected FullJoinClause(Clause clause, Clause onClause, string labelAs) : base(clause, onClause) => LabelAs = labelAs;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (Left is TableClause)
			{
				render
					.TryAppendCurrentIndent(1).Append("FULL JOIN "); Left.Render(context); render.Append(' ').AppendAsLabel(LabelAs);
			}
			else
			{
				render
					.TryAppendCurrentIndent(1).Append("FULL JOIN").TryAppendLineOrAppendSpace()
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
				return new FullJoinClause(left, right, LabelAs);
			}
		}
	}
}
