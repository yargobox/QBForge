using QBForge.Interfaces;

namespace QBForge.Providers.Configuration
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