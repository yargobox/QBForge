using QBForge.Extensions;
using QBForge.Provider;
using QBForge.Provider.Clauses;

#pragma warning disable CA1707 // Identifiers should not contain underscores

namespace QBForge.SqlServer
{
	public static class Ag
	{

		public static IAggrRender COUNT_1(this IAggrRender query)
		{
			query.Append("COUNT(1)"); return query;
		}

		public static IAggrRender COUNT_ALL(this IAggrRender query)
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

#pragma warning restore CA1707 // Identifiers should not contain underscores
