using QBForge.Interfaces;

namespace QBForge.PostgreSql
{
	public static class Op
	{
		public static ICondOperator IsNull(this ICondOperator query, DataEntry lhs)
		{
			query.Append(lhs).Append(" IS NULL"); return query;
		}
		public static ICondOperator NotIsNull(this ICondOperator query, DataEntry lhs)
		{
			query.Append(lhs).Append(" IS NOT NULL"); return query;
		}

		public static ICondOperator Equal(this ICondOperator query, DataEntry lhs, string value)
		{
			query.Append(lhs).Append(" = ").Append(value); return query;
		}
		public static ICondOperator Equal(this ICondOperator query, DataEntry lhs, DataEntry rhs)
		{
			query.Append(lhs).Append(" = ").Append(rhs); return query;
		}

		public static ICondOperator Greater(this ICondOperator query, DataEntry lhs, string rhs)
		{
			query.Append(lhs).Append(" > ").Append(rhs); return query;
		}
	}
}