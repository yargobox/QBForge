using QBForge.Interfaces;
using QBForge.Providers.Configuration;
using System;
using System.Text;

namespace QBForge.PostgreSql
{
	public static class QB
	{
		public static ISelectQB<T> Select<T>(string tableName, string? label = null, dynamic? parameters = null)
		{
			if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

			var (schemaName, objectName) = PostgreSqlConfig.Provider.ParseSchemaObject(tableName);

			var qb = PostgreSqlConfig.Provider.CreateSelectQB<T>();

			qb.Context.SetClause(new TextClause(ClauseSections.Select, null, "SELECT"));

			var sb = new StringBuilder("FROM ");

			if (!string.IsNullOrEmpty(schemaName))
			{
				qb.Context.Provider.AppendIdentifier(sb, schemaName!);
				sb.Append('.');
			}

			qb.Context.Provider.AppendIdentifier(sb, objectName);

			if (!string.IsNullOrEmpty(label))
			{
				sb.Append(' ');
				qb.Context.Provider.AppendAsLabel(sb, label!);
			}

			qb.Context.SetClause(new TextClause(ClauseSections.From, null, sb.ToString()));//!!! parameters

			return qb;
		}
	}
}