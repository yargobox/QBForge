using QBForge.Extensions.Text;

namespace QBForge.Interfaces.Clauses
{
	public class WithCteClause : UnaryClause
	{
		public override string? Key => LabelCte;
		public string LabelCte { get; }

		public WithCteClause(string labelCte, SubQueryClause subQueryClause) : base(subQueryClause) => LabelCte = labelCte;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render
				.AppendLabel(LabelCte).Append(" AS").TryAppendLineOrTryAppendSpace()
				.TryAppendCurrentIndent().Append('(').TryAppendLine();

			render.CurrentIndent++;
			try
			{
				Left.Render(context);
			}
			finally
			{
				render.CurrentIndent--;
			}

			render
				.TryAppendLine()
				.TryAppendCurrentIndent().Append(')');
		}
	}
}
