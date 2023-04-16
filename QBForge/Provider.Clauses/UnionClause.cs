using QBForge.Extensions;
using QBForge.Extensions.Text;

namespace QBForge.Provider.Clauses
{
    public class UnionClause : UnaryClause
	{
#pragma warning disable CA1707 // Identifiers should not contain underscores
		public const string UNION = "UNION";
		public const string UNION_ALL = "UNION ALL";
		public const string INTERSECT = "INTERSECT";
		public const string INTERSECT_ALL = "INTERSECT ALL";
		public const string EXCEPT = "EXCEPT";
		public const string EXCEPT_ALL = "EXCEPT ALL";
#pragma warning restore CA1707 // Identifiers should not contain underscores

		public string UnionMethod { get; }

		public UnionClause(string unionMethod, SubQueryClause subQuery) : this(unionMethod, (Clause)subQuery) { }
		protected UnionClause(string unionMethod, Clause clause) : base(clause) => UnionMethod = unionMethod;

		public override void Render(IBuildQueryContext context)
		{
			var render = context.RenderContext;

			render
				.TryAppendCurrentIndent().Append(UnionMethod).TryAppendLine()
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
				return new UnionClause(UnionMethod, left);
			}
		}
	}
}
