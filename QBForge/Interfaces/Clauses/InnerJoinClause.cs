﻿using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class InnerJoinClause : BinaryClause
	{
		public override string? Key => LabelAs;
		public string LabelAs { get; }

		public InnerJoinClause(TableClause tableClause, string labelAs) : base(tableClause, new OnClause()) => LabelAs = labelAs;
		public InnerJoinClause(SubQueryClause subQueryClause, string labelAs) : base(subQueryClause, new OnClause()) => LabelAs = labelAs;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			if (Left is TableClause)
			{
				render
					.TryAppendCurrentIndent(1).Append("INNER JOIN "); Left.Render(context); render.Append(' ').AppendAsLabel(LabelAs);
			}
			else
			{
				render
					.TryAppendCurrentIndent(1).Append("INNER JOIN").TryAppendLineOrAppendSpace()
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