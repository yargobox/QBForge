namespace QBForge.Interfaces.Clauses
{
	public class FromClause : UnaryClause
	{
		public override string? Key => LabelAs;
		public string? LabelAs { get; }

		public FromClause(TableClause tableClause, string? labelAs) : base(tableClause)
		{
			LabelAs = string.IsNullOrEmpty(labelAs) ? null : labelAs;
		}

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			Left.Render(context);

			if (!string.IsNullOrEmpty(LabelAs))
			{
				render.Append(' ').AppendAsLabel(LabelAs!);
			}
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(LabelAs)
				? Left.ToString()
				: string.Concat(Left.ToString(), " AS ", LabelAs);
		}
	}
}
