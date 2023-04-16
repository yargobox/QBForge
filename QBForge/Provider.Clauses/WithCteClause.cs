using QBForge.Extensions;
using QBForge.Extensions.Text;

namespace QBForge.Provider.Clauses
{
    public class WithCteClause : UnaryClause
	{
		public override string? Key => LabelCte;
		public string LabelCte { get; }

		public WithCteClause(string labelCte, SubQueryClause subQueryClause) : this(labelCte, (Clause)subQueryClause) { }
		protected WithCteClause(string labelCte, Clause clause) : base(clause) => LabelCte = labelCte;

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

		public override Clause Clone()
		{
			var left = Left.Clone();

			if (ReferenceEquals(left, Left))
			{
				return this;
			}
			else
			{
				return new WithCteClause(LabelCte, left);
			}
		}
	}
}
