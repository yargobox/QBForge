using QBForge.Extensions.Text;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;

#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace QBForge.PostgreSql
{
	public static class Ob
	{
		public static IOrderByRender ASC(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" ASC"); return query;
		}

		public static IOrderByRender ASC_NULLS_FIRST(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" ASC NULLS FIRST"); return query;
		}

		public static IOrderByRender DESC(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" DESC"); return query;
		}

		public static IOrderByRender DESC_NULLS_LAST(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" DESC NULLS LAST"); return query;
		}

		public static IOrderByRender RANK(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" RANK"); return query;
		}
	}
}

#pragma warning restore CA1707 // Identifiers should not contain underscores