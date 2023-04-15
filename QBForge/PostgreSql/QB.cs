using QBForge.Interfaces;
using QBForge.Providers.Configuration;

namespace QBForge.PostgreSql
{
	public static class QB
	{
		public static ISelectQB<T> Select<T>(string tableName, string? labelAs = null, dynamic? parameters = null)
		{
			return PostgreSqlConfig.Provider.CreateSelectQB<T>().From(tableName, labelAs, parameters);
		}

		public static IWithCteQB With<TCte>(string labelCte, ISelectQB<TCte> subQuery)
		{
			return PostgreSqlConfig.Provider.CreateWithCteQB().With(labelCte, subQuery);
		}
	}
}