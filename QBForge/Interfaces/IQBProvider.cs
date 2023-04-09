using QBForge.Interfaces.Clauses;
using QBForge.Providers.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace QBForge.Interfaces
{
    public interface IQBProvider
	{
		/// <summary>
		/// Provider name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// Prefix for naming numerical parameters
		/// </summary>
		string ParameterPlaceholder { get; }

		// Provided interfaces
		//

		/// <summary>
		/// Create a select query builder for the document <typeparamref name="T"/>
		/// </summary>
		ISelectQB<T> CreateSelectQB<T>();

		// Cache
		//

		/// <summary>
		/// Get a database name of the property <paramref name="name"/> of the document <typeparamref name="T"/>
		/// </summary>
		string GetMappedName<T>(string name);

		/// <summary>
		/// Get a database name of the property <paramref name="name"/> of the document <paramref name="documentType"/>
		/// </summary>
		string GetMappedName(Type documentType, string name);

		/// <summary>
		/// Get mapping information for the specified document <typeparamref name="T"/>
		/// </summary>
		/// <returns><see cref="MemberMappingInfo"/> by property names</returns>
		IReadOnlyDictionary<string, MemberMappingInfo> GetMappingInfo<T>();

		/// <summary>
		/// Get mapping information for the specified document <paramref name="documentType"/>
		/// </summary>
		/// <returns><see cref="MemberMappingInfo"/> by property names</returns>
		IReadOnlyDictionary<string, MemberMappingInfo> GetMappingInfo(Type documentType);

		// Build
		//

		IBuildQueryContext CreateBuildQueryContext(ReadabilityLevels level = ReadabilityLevels.Default, int tabSize = 0);
		IBuildQueryContext Build(IQueryBuilder queryBuilder, IBuildQueryContext? context = null);
		bool Render(Clause clause, IBuildQueryContext context);

		// Syntax
		//

		string AppendObject(string objectName, ReadabilityLevels level = ReadabilityLevels.Default);
		void AppendObject(StringBuilder sb, string objectName, ReadabilityLevels level = ReadabilityLevels.Default);
		string AppendLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default);
		void AppendLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default);
		string AppendAsLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default);
		void AppendAsLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default);

		(string? schemaName, string objectName) ParseSchemaObject(string schemaObject);
	}
}