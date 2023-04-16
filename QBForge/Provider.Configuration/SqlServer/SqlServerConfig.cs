namespace QBForge.Provider.Configuration.SqlServer
{
	public static class SqlServerConfig
	{
		public static IQBProvider Provider { get; set; } = Static.Provider;

		private static class Static
		{
			static Static() { }

			public readonly static IQBProvider Provider = new SqlServerProvider(new SqlServerMapping());
		}
	}
}