using QBForge.Interfaces;

namespace QBForge.PostgreSql
{
	public static class Ag
	{
		public static IAggrCall COUNT(this IAggrCall query, DataEntry arg)
		{
			query.Append("COUNT(").Append(arg).Append(')'); return query;
		}

		public static IAggrCall SUM(this IAggrCall query, DataEntry arg)
		{
			query.Append("SUM(").Append(arg).Append(')'); return query;
		}

		public static IAggrCall MIN(this IAggrCall query, DataEntry arg)
		{
			query.Append("MIN(").Append(arg).Append(')'); return query;
		}

		public static IAggrCall MAX(this IAggrCall query, DataEntry arg)
		{
			query.Append("MAX(").Append(arg).Append(')'); return query;
		}

		public static IAggrCall AVG(this IAggrCall query, DataEntry arg)
		{
			query.Append("AVG(").Append(arg).Append(')'); return query;
		}
	}
}