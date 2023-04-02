using Npgsql;

namespace QBForge.Tests
{
	public class SharedTestContextFixture : IDisposable
	{
		public NpgsqlDataSource PostgreSQL { get; }

		public SharedTestContextFixture()
		{
			var defaultConnectionString = new NpgsqlConnectionStringBuilder
			{
				Host = "127.0.0.1",
				Port = 5432,
				Username = "user1",
				Password = "Pass#word1",
				Database = "qbforge"
			}
			.ToString();

			var builder = new NpgsqlDataSourceBuilder(GetConnectionString("pgsql", defaultConnectionString));
			//builder.EnableParameterLogging(true);
			//builder.UseLoggerFactory(loggerFactory);
			PostgreSQL = builder.Build();
		}

		protected static string GetConnectionString(string name, string defaultConnectionString)
		{
			return Environment.GetEnvironmentVariable(name) ?? defaultConnectionString;
		}

		public void Dispose()
		{
			PostgreSQL.Dispose();
			GC.SuppressFinalize(this);
		}
	}

	[CollectionDefinition(nameof(SharedTestContextFixture), DisableParallelization = false)]
#pragma warning disable CA1711 // Identifiers should not have incorrect suffix
	public class SharedContextCollection : ICollectionFixture<SharedTestContextFixture> { }
#pragma warning restore CA1711 // Identifiers should not have incorrect suffix
}
