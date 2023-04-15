using QBForge.Extensions.Text;
using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;

namespace QBForge.PostgreSql
{
	public static class Ag
	{
#pragma warning disable CA1707 // Identifiers should not contain underscores
		public static IAggrRender COUNT_1(this IAggrRender query)
#pragma warning restore CA1707 // Identifiers should not contain underscores
		{
			query.Append("COUNT(1)"); return query;
		}

#pragma warning disable CA1707 // Identifiers should not contain underscores
		public static IAggrRender COUNT_ALL(this IAggrRender query)
#pragma warning restore CA1707 // Identifiers should not contain underscores
		{
			query.Append("COUNT(*)"); return query;
		}

		public static IAggrRender COUNT(this IAggrRender query, Clause arg)
		{
			query.Append("COUNT(").AppendArgument(arg).Append(')'); return query;
		}

		public static IAggrRender SUM(this IAggrRender query, Clause arg)
		{
			query.Append("SUM(").AppendArgument(arg).Append(')'); return query;
		}

		public static IAggrRender MIN(this IAggrRender query, Clause arg)
		{
			query.Append("MIN(").AppendArgument(arg).Append(')'); return query;
		}

		public static IAggrRender MAX(this IAggrRender query, Clause arg)
		{
			query.Append("MAX(").AppendArgument(arg).Append(')'); return query;
		}

		public static IAggrRender AVG(this IAggrRender query, Clause arg)
		{
			query.Append("AVG(").AppendArgument(arg).Append(')'); return query;
		}
	}
}