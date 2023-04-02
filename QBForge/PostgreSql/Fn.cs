using QBForge.Interfaces;

namespace QBForge.PostgreSql
{
	public static class Fn
	{
		public static IFuncCall ISNULL(this IFuncCall query, DataEntry arg, string value)
		{
			query.Append("ISNULL(").Append(arg).Append(", ").Append(value).Append(')'); return query;
		}

		public static IFuncCall LOWER(this IFuncCall query, DataEntry arg)
		{
			query.Append("LOWER(").Append(arg).Append(')'); return query;
		}

		public static IFuncCall UPPER(this IFuncCall query, DataEntry arg)
		{
			query.Append("UPPER(").Append(arg).Append(')'); return query;
		}

		public static IFuncCall LEN(this IFuncCall query, DataEntry arg)
		{
			query.Append("LEN(").Append(arg).Append(')'); return query;
		}

		public static IFuncCall DATALENGTH(this IFuncCall query, DataEntry arg)
		{
			query.Append("DATALENGTH(").Append(arg).Append(')'); return query;
		}
	}
}