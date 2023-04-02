using QBForge.Interfaces;

namespace QBForge.PostgreSql
{
	public static class Ob
	{
		public static IOrderBy ASC(this IOrderBy query, DataEntry arg)
		{
			query.Append(arg); return query;
		}
		
		public static IOrderBy DESC(this IOrderBy query, DataEntry arg)
		{
			query.Append(arg).Append(" DESC"); return query;
		}

		public static IOrderBy RANK(this IOrderBy query, DataEntry arg)
		{
			query.Append(arg).Append(" RANK"); return query;
		}
	}
}