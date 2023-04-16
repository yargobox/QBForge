using QBForge.Provider;
using QBForge.Provider.Configuration.SqlServer;

namespace QBForge.SqlServer
{
	public static class QB
	{
		public static ISelectQB<T> Select<T>(string tableName, string? labelAs = null, dynamic? parameters = null)
		{
			return SqlServerConfig.Provider.CreateSelectQB<T>().From(tableName, labelAs, parameters);
		}

		public static IWithCteQB With<TCte>(string labelCte, ISelectQB<TCte> subQuery)
		{
			return SqlServerConfig.Provider.CreateWithCteQB().With(labelCte, subQuery);
		}
	}
}