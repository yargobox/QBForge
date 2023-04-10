using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class RightJoinClause : BinaryClause
	{
		public override string? Key => LabelAs;
		public string LabelAs { get; }

		public RightJoinClause(TableClause tableClause, string lableAs) : base(tableClause, new OnClause()) => LabelAs = lableAs;
		public RightJoinClause(SubQueryClause subQueryClause, string lableAs) : base(subQueryClause, new OnClause()) => LabelAs = lableAs;

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
	}
}
