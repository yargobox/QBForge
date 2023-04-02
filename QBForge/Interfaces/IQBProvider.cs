using QBForge.Providers.Configuration;
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
		/// Get mapping information for the document type <typeparamref name="T"/>
		/// </summary>
		/// <returns><see cref="MemberMappingInfo"/> by property names</returns>
		IReadOnlyDictionary<string, MemberMappingInfo> GetMappingInfo<T>();

		// Build
		//

		IBuildQueryContext CreateBuildQueryContext(ReadabilityLevels level = ReadabilityLevels.Default);
		IBuildQueryContext Build<T>(ISelectQB<T> queryBuilder, IBuildQueryContext? context = null);

		// Syntax
		//

		string AppendIdentifier(string identifier, ReadabilityLevels level = ReadabilityLevels.Default);
		void AppendIdentifier(StringBuilder sb, string identifier, ReadabilityLevels level = ReadabilityLevels.Default);
		string AppendLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default);
		void AppendLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default);
		string AppendAsLabel(string label, ReadabilityLevels level = ReadabilityLevels.Default);
		void AppendAsLabel(StringBuilder sb, string label, ReadabilityLevels level = ReadabilityLevels.Default);

		(string? schemaName, string objectName) ParseSchemaObject(string schemaObject);
	}
}