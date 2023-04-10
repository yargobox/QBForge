using QBForge.Extensions.Text;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;

namespace QBForge.PostgreSql
{
	public static class Ob
	{
		public static IOrderByRender ASC(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" ASC"); return query;
		}

		public static IOrderByRender DESC(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" DESC"); return query;
		}

		public static IOrderByRender RANK(this IOrderByRender query, Clause arg)
		{
			query.AppendArgument(arg).Append(" RANK"); return query;
		}
	}
}