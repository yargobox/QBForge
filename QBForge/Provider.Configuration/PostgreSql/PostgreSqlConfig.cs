using QBForge.Provider.PostgreSql;

namespace QBForge.Provider.Configuration.PostgreSql
{
	public static class PostgreSqlConfig
	{
		public static IQBProvider Provider { get; set; } = Static.Provider;

		private static class Static
		{
			static Static() { }

			public readonly static IQBProvider Provider = new PostgreSqlProvider(new PostgreSqlMapping());
		}
	}
}