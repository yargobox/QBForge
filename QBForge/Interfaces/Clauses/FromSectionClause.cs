using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class FromSectionClause : UnaryClause
	{
		public override string? Key => ClauseSections.From;
		public string? LabelAs { get; }

		public FromSectionClause(TableClause tableClause, string? labelAs) : base(tableClause) => LabelAs = string.IsNullOrEmpty(labelAs) ? null : labelAs;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render
				.TryAppendCurrentIndent().Append("FROM").TryAppendLineOrAppendSpace()
				.TryAppendCurrentIndent(1);

			Left.Render(context);

			if (!string.IsNullOrEmpty(LabelAs))
			{
				render.Append(' ').AppendAsLabel(LabelAs!);
			}
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(LabelAs)
				? "FROM " + Left.ToString()
				: string.Concat("FROM ", Left.ToString(), " AS ", LabelAs);
		}
	}
}
