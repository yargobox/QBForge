namespace QBForge.Interfaces.Clauses
{
	public class IncludeClause : UnaryClause
	{
		public override string? Key { get; }
		public string? LabelAs { get; }

		public IncludeClause(string? key, Clause clause, string? labelAs = null) : base(clause)
		{
			Key = key;
			LabelAs = labelAs;
		}

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			Left.Render(context);
			if (!string.IsNullOrEmpty(LabelAs))
			{
				render.Append(' ');
				render.AppendAsLabel(LabelAs!);
			}
		}

		public static string MakeKey(string? tableLabel, string nameOrLabelAs)
		{
			return string.IsNullOrEmpty(tableLabel) ? nameOrLabelAs : string.Concat(tableLabel, "\b", nameOrLabelAs);
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(LabelAs)
				? Left.ToString()
				: string.Concat(Left.ToString(), " AS ", LabelAs!);
		}
	}
}
