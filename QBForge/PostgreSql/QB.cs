﻿using QBForge.Interfaces;
using QBForge.Interfaces.Clauses;
using QBForge.Providers.Configuration;
using System;

namespace QBForge.PostgreSql
{
	public static class QB
	{
		public static ISelectQB<T> Select<T>(string tableName, string? labelAs = null, dynamic? parameters = null)
		{
			if (string.IsNullOrEmpty(tableName)) throw new ArgumentNullException(nameof(tableName));

			var (schemaName, objectName) = PostgreSqlConfig.Provider.ParseSchemaObject(tableName);

			var qb = PostgreSqlConfig.Provider.CreateSelectQB<T>();

			qb.Context.Clause.Add(new FromSectionClause(new TableClause(schemaName, objectName), labelAs));//!!! parameters

			return qb;
		}
	}
}